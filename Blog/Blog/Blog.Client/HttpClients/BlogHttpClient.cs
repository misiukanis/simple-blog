using Blog.Client.Models;
using Blog.Shared.Constants;
using Blog.Shared.DTOs;
using Blog.Shared.Enums;
using Blog.Shared.Metadata;
using Blog.Shared.Params;
using Microsoft.AspNetCore.WebUtilities;
using System.Net.Http.Json;
using System.Text.Json;

namespace Blog.Client.HttpClients
{
    public class BlogHttpClient(HttpClient httpClient)
    {
        private readonly HttpClient _httpClient = httpClient;


        public async Task SendEmailAsync(CreateEmailMessageDTO emailMessage)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Emails", emailMessage);
            response.EnsureSuccessStatusCode();
        }

        public async Task<PagingResponse<PostDTO>> GetPaginatedPostsAsync(PaginationParams paginationParams, string? searchTerm = null)
        {
            var queryStringParams = new Dictionary<string, string>
            {
                [nameof(PaginationParams.Page)] = paginationParams.Page.ToString(),
                [nameof(PaginationParams.ItemsCountPerPage)] = paginationParams.ItemsCountPerPage.ToString(),
                [nameof(SearchParams.SearchTerm)] = searchTerm == null ? string.Empty : searchTerm
            };

            var response = await _httpClient.GetAsync(QueryHelpers.AddQueryString("api/Posts", queryStringParams));
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            var jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var pagingResponse = new PagingResponse<PostDTO>
            {
                Items = JsonSerializer.Deserialize<List<PostDTO>>(content, jsonSerializerOptions),
                MetaData = JsonSerializer.Deserialize<PaginationMetadata>(response.Headers.GetValues(HeaderConstants.Pagination).First(), jsonSerializerOptions)
            };

            return pagingResponse;
        }

        public async Task<PostWithCommentsDTO> GetPostByIdAsync(Guid postId)
        {
            var item = await _httpClient.GetFromJsonAsync<PostWithCommentsDTO>($"api/Posts/{postId}");
            return item;
        }

        public async Task<Guid> CreatePostAsync(CreatePostDTO postDTO)
        {
            var response = await _httpClient.PostAsJsonAsync($"api/Posts", postDTO);
            response.EnsureSuccessStatusCode();
            
            var data = await response.Content.ReadFromJsonAsync<JsonElement>();
            var postId = data.GetProperty("postId").GetGuid();
            return postId;
        }

        public async Task UpdatePostAsync(UpdatePostDTO postDTO)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Posts/{postDTO.PostId}", postDTO);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeletePostAsync(Guid postId)
        {
            var response = await _httpClient.DeleteAsync($"api/Posts/{postId}");
            response.EnsureSuccessStatusCode();
        }

        public async Task CreateCommentAsync(Guid postId, CreateCommentDTO commentDTO)
        {
            var response = await _httpClient.PostAsJsonAsync($"api/Posts/{postId}/Comments", commentDTO);
            response.EnsureSuccessStatusCode();
        }

        public async Task<List<CommentDTO>> GetCommentsAsync()
        {
            var items = await _httpClient.GetFromJsonAsync<List<CommentDTO>>("api/Comments");
            return items;
        }

        public async Task ChangeCommentStatusAsync(Guid commentId, CommentStatus commentStatus)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Comments/{commentId}/Status", Enum.GetName(typeof(CommentStatus), commentStatus));
            response.EnsureSuccessStatusCode();
        }

        public async Task<UploadedFileDTO> UploadImageAsync(MultipartFormDataContent image)
        {
            var response = await _httpClient.PostAsync("api/Images", image);
            response.EnsureSuccessStatusCode();

            var data = await response.Content.ReadFromJsonAsync<UploadedFileDTO>();
            return data;
        }
    }
}
