using AutoMapper;
using Blog.Application.Commands.CreateComment;
using Blog.Application.Commands.CreatePost;
using Blog.Application.Commands.DeletePost;
using Blog.Application.Commands.UpdatePost;
using Blog.Application.DTOs;
using Blog.Application.Queries.GetPaginatedPosts;
using Blog.Application.Queries.GetPostById;
using Blog.Models.Comments;
using Blog.Models.General;
using Blog.Models.Pagination;
using Blog.Models.Posts;
using Blog.Shared.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using X.PagedList;

namespace Blog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class PostsController(
        ISender mediator, 
        IMapper mapper) : ControllerBase
    {
        private readonly ISender _mediator = mediator;
        private readonly IMapper _mapper = mapper;


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IPagedList<PostDTO>>> GetPaginatedPosts([FromQuery] PaginationRequest paginationRequest, [FromQuery] SearchRequest searchRequest)
        {
            var postsDTO = await _mediator.Send(new GetPaginatedPostsQuery(paginationRequest.Page, paginationRequest.ItemsCountPerPage, searchRequest.SearchTerm));

            var paginationMetadata = _mapper.Map<PaginationMetadata>(postsDTO);
            Response.Headers[HeaderConstants.Pagination] = JsonSerializer.Serialize(paginationMetadata);

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
        public async Task<IActionResult> CreatePost(CreatePostRequest postRequest)
        {
            var postCommand = new CreatePostCommand(Guid.NewGuid(), postRequest.Title, postRequest.Introduction, postRequest.Content);
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
        public async Task<IActionResult> UpdatePost(Guid postId, UpdatePostRequest postRequest)
        {
            if (postId != postRequest.PostId)
            {
                return BadRequest();
            }

            var postCommand = new UpdatePostCommand(postRequest.PostId, postRequest.Title, postRequest.Introduction, postRequest.Content);
            await _mediator.Send(postCommand);

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
            await _mediator.Send(new DeletePostCommand(postId));

            return NoContent();
        }        
    }
}
