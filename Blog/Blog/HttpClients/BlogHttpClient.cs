using Blog.Application.DTOs;
using Blog.Domain.Enums;
using Blog.Models.Comments;
using Blog.Models.Emails;
using Blog.Models.Files;
using Blog.Models.General;
using Blog.Models.Pagination;
using Blog.Models.Posts;
using Blog.Shared.Constants;
using Microsoft.AspNetCore.WebUtilities;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Blog.HttpClients
{
    public class BlogHttpClient(HttpClient httpClient)
    {
        private readonly HttpClient _httpClient = httpClient;


        public async Task SendEmailAsync(SendEmailRequest emailRequest)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Emails", emailRequest);
            response.EnsureSuccessStatusCode();
        }

        public async Task<PaginationResponse<PostDTO>> GetPaginatedPostsAsync(PaginationRequest paginationRequest, string? searchTerm = null)
        {
            var queryStringParams = new Dictionary<string, string>
            {
                [nameof(PaginationRequest.Page)] = paginationRequest.Page.ToString(),
                [nameof(PaginationRequest.ItemsCountPerPage)] = paginationRequest.ItemsCountPerPage.ToString(),
                [nameof(SearchRequest.SearchTerm)] = searchTerm == null ? string.Empty : searchTerm
            };

            var response = await _httpClient.GetAsync(QueryHelpers.AddQueryString("api/Posts", queryStringParams));
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions(JsonSerializerDefaults.Web);

            var paginationResponse = new PaginationResponse<PostDTO>
            {
                Items = JsonSerializer.Deserialize<List<PostDTO>>(content, options),
                Metadata = JsonSerializer.Deserialize<PaginationMetadata>(response.Headers.GetValues(HeaderConstants.Pagination).First(), options)
            };

            return paginationResponse;
        }

        public async Task<PostWithCommentsDTO> GetPostByIdAsync(Guid postId)
        {
            var options = new JsonSerializerOptions(JsonSerializerDefaults.Web)
            {
                Converters = { new JsonStringEnumConverter() }
            };

            var item = await _httpClient.GetFromJsonAsync<PostWithCommentsDTO>($"api/Posts/{postId}", options);
            return item;
        }

        public async Task<Guid> CreatePostAsync(CreatePostRequest postRequest)
        {
            var response = await _httpClient.PostAsJsonAsync($"api/Posts", postRequest);
            response.EnsureSuccessStatusCode();
            
            var data = await response.Content.ReadFromJsonAsync<JsonElement>();
            var postId = data.GetProperty("postId").GetGuid();
            return postId;
        }

        public async Task UpdatePostAsync(UpdatePostRequest postRequest)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Posts/{postRequest.PostId}", postRequest);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeletePostAsync(Guid postId)
        {
            var response = await _httpClient.DeleteAsync($"api/Posts/{postId}");
            response.EnsureSuccessStatusCode();
        }

        public async Task CreateCommentAsync(Guid postId, CreateCommentRequest commentRequest)
        {
            var response = await _httpClient.PostAsJsonAsync($"api/Comments", commentRequest);
            response.EnsureSuccessStatusCode();
        }

        public async Task<List<CommentDTO>> GetCommentsAsync()
        {
            var options = new JsonSerializerOptions(JsonSerializerDefaults.Web)
            {
                Converters = { new JsonStringEnumConverter() }
            };

            var items = await _httpClient.GetFromJsonAsync<List<CommentDTO>>("api/Comments", options);
            return items ?? new List<CommentDTO>();
        }

        public async Task ChangeCommentStatusAsync(Guid commentId, CommentStatus commentStatus)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Comments/{commentId}/Status", commentStatus);
            response.EnsureSuccessStatusCode();
        }

        public async Task<FileResponse> UploadImageAsync(MultipartFormDataContent image)
        {
            var response = await _httpClient.PostAsync("api/Images", image);
            response.EnsureSuccessStatusCode();

            var data = await response.Content.ReadFromJsonAsync<FileResponse>();
            return data;
        }
    }
}
