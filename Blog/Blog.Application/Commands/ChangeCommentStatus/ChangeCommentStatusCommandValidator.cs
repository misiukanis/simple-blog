using Blog.Shared.Enums;
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

        private bool EnumIsDefined(CommentStatus commentStatus)
        {
            bool enumIsDefined = Enum.IsDefined(typeof(Domain.Entities.PostAggregate.CommentStatus), (int)commentStatus);
            return enumIsDefined;
        }
    }
}
