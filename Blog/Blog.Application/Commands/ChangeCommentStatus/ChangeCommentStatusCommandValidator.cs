using FluentValidation;

namespace Blog.Application.Commands.ChangeCommentStatus
{
    public class ChangeCommentStatusCommandValidator : AbstractValidator<ChangeCommentStatusCommand>
    {
        public ChangeCommentStatusCommandValidator()
        {
            RuleFor(x => x.CommentStatus)
                .Must(EnumIsDefined);
        }

        private bool EnumIsDefined(Shared.Enums.CommentStatus commentStatus)
        {
            bool enumIsDefined = Enum.IsDefined(typeof(Domain.Enums.CommentStatus), (int)commentStatus);
            return enumIsDefined;
        }
    }
}
