using Blog.Application.Services.Interfaces;
using FluentValidation;

namespace Blog.Application.Commands.CreateComment
{
    public class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
    {
        private readonly ILookupService _lookupService;

        public CreateCommentCommandValidator(ILookupService lookupService)
        {
            _lookupService = lookupService;

            RuleFor(x => x.Author)
                .NotEmpty()
                .MaximumLength(20);

            RuleFor(x => x.Content)
                .NotEmpty()
                .MaximumLength(100000)
                .MustAsync(async (content, cancellation) => 
                {
                    var forbiddenWords = await _lookupService.GetForbiddenWordsAsync();
                    var contentHasForbiddenWords = false;

                    if (forbiddenWords.Any(content.Contains))
                    {
                        contentHasForbiddenWords = true;
                    }

                    return !contentHasForbiddenWords;
                });
        }
    }
}
