using System.Diagnostics;
using System.Net;
using NotificationService.API.Settings;
using Serilog;
using Serilog.Sinks.Http;

namespace NotificationService.Infrastructure.Clients;

public class LogstashHttpClient : IHttpClient, IDisposable
{
    private readonly HttpClient _client;
    private readonly LogstashSettings _settings;

    public LogstashHttpClient(LogstashSettings settings)
    {
        _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        _client = CreateHttpClient();
    }

    public async Task<HttpResponseMessage> PostAsync(string requestUri, Stream logData, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(requestUri)) throw new ArgumentNullException(nameof(requestUri));
        if (logData == null) throw new ArgumentNullException(nameof(logData));

        var stopwatch = Stopwatch.StartNew();

        try
        {
            using var content = new StreamContent(logData);
            var response = await _client.PostAsync(requestUri, content, cancellationToken).ConfigureAwait(false);

            stopwatch.Stop();
            await HandleResponseAsync(response, requestUri, logData, stopwatch.Elapsed).ConfigureAwait(false);

            return response;
        }
        catch (TaskCanceledException ex) when (!cancellationToken.IsCancellationRequested)
        {
            Log.Warning(ex, "Tempo esgotado ao enviar log para {RequestUri}", requestUri);
            throw;
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Erro inesperado ao enviar log para {RequestUri}", requestUri);
            throw;
        }
    }

    public void Configure(IConfiguration configuration)
    {
        // Placeholder para configuração futura, se necessário
    }

    public void Dispose()
    {
        _client?.Dispose();
    }

    private HttpClient CreateHttpClient()
    {
        var handler = new SocketsHttpHandler
        {
            MaxConnectionsPerServer = 500,
            PooledConnectionLifetime = TimeSpan.FromMinutes(2),
            PooledConnectionIdleTimeout = TimeSpan.FromMinutes(1)
        };

        if (!string.IsNullOrEmpty(_settings.Username))
        {
            handler.Credentials = new NetworkCredential(_settings.Username, _settings.Password);
        }

        var client = new HttpClient(handler)
        {
            Timeout = TimeSpan.FromMinutes(5)
        };

        return client;
    }

    private async Task HandleResponseAsync(HttpResponseMessage response, string requestUri, Stream logData, TimeSpan elapsed)
    {
        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine($"Log enviado com sucesso para {requestUri} (Status: {response.StatusCode})");

            if (elapsed > TimeSpan.FromMinutes(1))
            {
                LogSlowRequest(elapsed, requestUri, logData, response.StatusCode);
            }

            return;
        }

        var errorContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        Log.Error("Falha ao enviar log para {RequestUri} (Status: {StatusCode}): {ErrorContent}", 
            requestUri, response.StatusCode, errorContent);
        throw new HttpRequestException($"Falha ao enviar log para Logstash. Status: {response.StatusCode}, Conteúdo: {errorContent}");
    }

    private void LogSlowRequest(TimeSpan elapsed, string requestUri, Stream logData, HttpStatusCode statusCode)
    {
        long? contentLength = logData.CanSeek ? logData.Length : null;
        double elapsedSeconds = Math.Round(elapsed.TotalSeconds, 2);
        double contentLengthKb = contentLength.HasValue ? contentLength.Value / 1024.0 : -1;
        double contentLengthMb = contentLength.HasValue ? contentLength.Value / (1024.0 * 1024.0) : -1;
        double speedKbPerSec = contentLength.HasValue ? Math.Round(contentLengthKb / elapsed.TotalSeconds, 2) : -1;

        Log.Warning(
            "Requisição lenta: {ElapsedSeconds}s para {RequestUri}. Tamanho: {ContentLengthKb} KB (~{ContentLengthMb} MB). Velocidade: {SpeedKbPerSec} KB/s. Status: {StatusCode}",
            elapsedSeconds, requestUri, contentLengthKb, contentLengthMb, speedKbPerSec, statusCode);
    }
}