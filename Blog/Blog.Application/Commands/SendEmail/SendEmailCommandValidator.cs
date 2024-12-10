using FluentValidation;

namespace Blog.Application.Commands.SendEmail
{
    public class SendEmailCommandValidator : AbstractValidator<SendEmailCommand>
    {
        public SendEmailCommandValidator()
        {
            RuleFor(x => x.FromAddress)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(100);

            RuleFor(x => x.Subject)
                .NotEmpty()                
                .MaximumLength(100);

            RuleFor(x => x.Message)
                .NotEmpty()
                .MaximumLength(10000);
        }
    }
}
