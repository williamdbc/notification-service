using NotificationService.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureSwagger();
builder.ConfigureSettings();
builder.ConfigureSerilog("notification-service");
builder.ConfigureMvc();
builder.ConfigureServices();
builder.ConfigureCors();
builder.ConfigureDatabase();
builder.ConfigureHangfire();

var app = builder.Build();

app.ConfigureMiddlewares();
app.InitializeDatabase();
app.UseHangfire();

app.Run();