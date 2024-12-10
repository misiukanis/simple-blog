using FluentValidation;

namespace Blog.Application.Commands.CreateComment
{
    public class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
    {
        public CreateCommentCommandValidator()
        {
            RuleFor(x => x.AuthorName)
                .NotEmpty()
                .MaximumLength(20);

            RuleFor(x => x.AuthorEmail)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(100);

            RuleFor(x => x.Content)
                .NotEmpty()
                .MaximumLength(100000);
        }
    }
}
