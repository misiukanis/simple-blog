using Blog.Application.Commands.ChangeCommentStatus;
using Blog.Application.Commands.CreateComment;
using Blog.Application.DTOs;
using Blog.Application.Queries.GetCommentById;
using Blog.Application.Queries.GetComments;
using Blog.Domain.Enums;
using Blog.Models.Comments;
using Blog.Shared.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class CommentsController(
        ISender mediator) : ControllerBase
    {
        private readonly ISender _mediator = mediator;


        #if !DEBUG
        [Authorize] 
        #endif
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<CommentDTO>>> GetComments()
        {
            var commentsDTO = await _mediator.Send(new GetCommentsQuery());
            return Ok(commentsDTO);
        }

        #if !DEBUG
        [Authorize] 
        #endif
        [HttpGet("{commentId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CommentDTO>> GetCommentById(Guid commentId)
        {
            var commentDTO = await _mediator.Send(new GetCommentByIdQuery(commentId));
            if (commentDTO == null)
            {
                return NotFound();
            }

            return Ok(commentDTO);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateComment(CreateCommentRequest commentRequest)
        {
            var commentCommand = new CreateCommentCommand(commentRequest.PostId, Guid.NewGuid(), commentRequest.AuthorName, commentRequest.AuthorEmail, commentRequest.Content);
            await _mediator.Send(commentCommand);

            return CreatedAtAction(nameof(GetCommentById), ControllerConstants.Comments,
                new { commentId = commentCommand.CommentId }, commentCommand);
        }

        #if !DEBUG
        [Authorize] 
        #endif
        [HttpPut("{commentId}/Status")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ChangeCommentStatus(Guid commentId, [FromBody] CommentStatus commentStatus)
        {
            await _mediator.Send(new ChangeCommentStatusCommand(commentId, commentStatus));

            return NoContent();
        }
    }
}
