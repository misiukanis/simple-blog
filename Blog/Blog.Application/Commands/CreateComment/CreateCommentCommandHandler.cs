﻿using Blog.Application.Exceptions;
using Blog.Domain.Core;
using Blog.Domain.Entities.PostAggregate;
using Blog.Domain.Repositories.Interfaces;
using Blog.Domain.ValueObjects;
using MediatR;

namespace Blog.Application.Commands.CreateComment
{
    public class CreateCommentCommandHandler(
        IPostsRepository postsRepository, 
        IUnitOfWork unitOfWork) : IRequestHandler<CreateCommentCommand>
    {
        private readonly IPostsRepository _postsRepository = postsRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            var post = await _postsRepository.GetByIdAsync(request.PostId);
            if (post == null)
            {
                throw new NotFoundException(nameof(Post), nameof(Post.PostId), request.PostId);
            }

            var author = new Author(request.AuthorName, request.AuthorEmail);
            post.AddComment(request.CommentId, author, request.Content);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
