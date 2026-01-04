namespace NotificationService.Application.UseCases.CreateEmail;

public class CreateEmailNotificationValidator
    : AbstractValidator<CreateEmailNotificationCommand>
{
    public CreateEmailNotificationValidator()
    {
        RuleFor(x => x.FromKey)
            .NotEmpty()
            .WithMessage("FromKey é obrigatório");

        RuleFor(x => x.Subject)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Body)
            .NotEmpty();

        RuleFor(x => x.To)
            .NotEmpty()
            .WithMessage("Informe ao menos um destinatário");

        RuleForEach(x => x.To)
            .EmailAddress()
            .WithMessage("E-mail inválido");

        RuleForEach(x => x.Cc)
            .EmailAddress()
            .When(x => x.Cc != null);

        RuleForEach(x => x.Bcc)
            .EmailAddress()
            .When(x => x.Bcc != null);

        RuleFor(x => x.ScheduledAt)
            .Must(BeInTheFuture)
            .When(x => x.ScheduledAt.HasValue)
            .WithMessage("ScheduledAt deve estar no futuro");

        RuleForEach(x => x.Attachments)
            .SetValidator(new CreateEmailAttachmentValidator());
    }

    private bool BeInTheFuture(DateTime? date)
        => date > DateTime.UtcNow;
}