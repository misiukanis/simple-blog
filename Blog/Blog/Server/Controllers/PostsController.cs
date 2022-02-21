using AutoMapper;
using Blog.Application.Commands.ChangeCommentStatus;
using Blog.Application.Commands.CreateComment;
using Blog.Application.Commands.CreatePost;
using Blog.Application.Commands.DeletePost;
using Blog.Application.Commands.UpdatePost;
using Blog.Application.Exceptions;
using Blog.Application.Queries.GetCommentById;
using Blog.Application.Queries.GetPaginatedPosts;
using Blog.Application.Queries.GetPostById;
using Blog.Common.Constants;
using Blog.Common.Pagination;
using Blog.Shared.DTOs;
using Blog.Shared.Enums;
using Blog.Shared.Pagination;
using Blog.Shared.Search;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Blog.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class PostsController : ControllerBase
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public PostsController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PaginatedList<PostDTO>>> GetPaginatedPosts([FromQuery] PaginationParams paginationParams, [FromQuery] SearchParams searchParams)
        {
            var postsDTO = await _mediator.Send(new GetPaginatedPostsQuery(paginationParams.Page, paginationParams.ItemsCountPerPage, searchParams.SearchTerm));
            
            var paginationMetadata = _mapper.Map<PaginationMetadata>(postsDTO);
            Response.Headers.Add(HeaderConstants.Pagination, JsonConvert.SerializeObject(paginationMetadata));

            return Ok(postsDTO);
        }

        [HttpGet("{postId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PostDTO>> GetPostById(Guid postId)
        {
            var postDTO = await _mediator.Send(new GetPostByIdQuery(postId));
            if (postDTO == null)
            {
                return NotFound();
            }

            return Ok(postDTO);
        }

        #if !DEBUG
        [Authorize] 
        #endif
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreatePost(CreatePostDTO postDTO)
        {
            var postCommand = new CreatePostCommand(Guid.NewGuid(), postDTO.Title, postDTO.Introduction, postDTO.Content);
            await _mediator.Send(postCommand);

            return CreatedAtAction(nameof(GetPostById), ControllerConstants.Posts, new { postId = postCommand.PostId }, postCommand);
        }

        #if !DEBUG
        [Authorize] 
        #endif
        [HttpPut("{postId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdatePost(Guid postId, UpdatePostDTO postDTO)
        {
            if (postId != postDTO.PostId)
            {
                return BadRequest();
            }

            var postCommand = new UpdatePostCommand(postDTO.PostId, postDTO.Title, postDTO.Introduction, postDTO.Content);

            try
            {
                await _mediator.Send(postCommand);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }

            return NoContent();
        }

        #if !DEBUG
        [Authorize] 
        #endif
        [HttpDelete("{postId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeletePost(Guid postId)
        {
            try
            {
                await _mediator.Send(new DeletePostCommand(postId));
            }
            catch (NotFoundException)
            {
                return NotFound();
            }

            return NoContent();
        }

        #if !DEBUG
        [Authorize] 
        #endif
        [HttpGet("Comments")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<PostDTO>>> GetComments()
        {
            var commentsDTO = await _mediator.Send(new GetCommentsQuery());
            return Ok(commentsDTO);
        }

        #if !DEBUG
        [Authorize] 
        #endif
        [HttpGet("{postId}/Comments/{commentId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PostDTO>> GetCommentById(Guid postId, Guid commentId)
        {
            var commentDTO = await _mediator.Send(new GetCommentByIdQuery(postId, commentId));
            if (commentDTO == null)
            {
                return NotFound();
            }

            return Ok(commentDTO);
        }

        [HttpPost("{postId}/Comments")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateComment(Guid postId, CreateCommentDTO commentDTO)
        {
            var commentCommand = new CreateCommentCommand(postId, Guid.NewGuid(), commentDTO.Author, commentDTO.Content);

            try
            {
                await _mediator.Send(commentCommand);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }

            return CreatedAtAction(nameof(PostsController.GetCommentById), ControllerConstants.Posts, 
                new { postId = commentCommand.PostId, commentId = commentCommand.CommentId }, commentCommand);
        }

        #if !DEBUG
        [Authorize] 
        #endif
        [HttpPut("{postId}/Comments/{commentId}/Status")]        
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ChangeCommentStatus(Guid postId, Guid commentId, [FromBody][EnumDataType(typeof(CommentStatus))] CommentStatus commentStatus)
        {
            try
            {
                await _mediator.Send(new ChangeCommentStatusCommand(postId, commentId, commentStatus));
            }
            catch (NotFoundException)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
