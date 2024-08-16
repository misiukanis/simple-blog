using Blog.Application.Services.Interfaces;
using FluentValidation;

namespace Blog.Application.Commands.CreateComment
{
    public class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
    {
        private readonly IForbiddenWordsService _forbiddenWordsService;

        public CreateCommentCommandValidator(IForbiddenWordsService forbiddenWordsService)
        {
            _forbiddenWordsService = forbiddenWordsService;

            RuleFor(x => x.Author)
                .NotEmpty()
                .MaximumLength(20);

            RuleFor(x => x.Content)
                .NotEmpty()
                .MaximumLength(100000)
                .MustAsync(NotContainForbiddenWords);
        }

        public async Task<bool> NotContainForbiddenWords(string content, CancellationToken cancellationToken)
        {
            var forbiddenWords = await _forbiddenWordsService.GetForbiddenWordsAsync();
            var contentHasForbiddenWords = false;

            if (forbiddenWords.Any(content.Contains))
            {
                contentHasForbiddenWords = true;
            }

            return !contentHasForbiddenWords;
        }
    }
}
