using Blog.Application.Commands.ChangeCommentStatus;
using Blog.Application.Queries.GetCommentById;
using Blog.Application.Queries.GetComments;
using Blog.Shared.DTOs;
using Blog.Shared.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

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

        #if !DEBUG
        [Authorize] 
        #endif
        [HttpPut("{commentId}/Status")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ChangeCommentStatus(Guid commentId, [FromBody][EnumDataType(typeof(CommentStatus))] CommentStatus commentStatus)
        {
            await _mediator.Send(new ChangeCommentStatusCommand(commentId, commentStatus));

            return NoContent();
        }
    }
}
