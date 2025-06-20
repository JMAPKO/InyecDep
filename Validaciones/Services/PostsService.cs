using BACKEND02.DTOs;

namespace BACKEND02.Services
{
    public class PostsService : IPostsService
    {
        private HttpClient _httpClient;

        public PostsService(HttpClient http)
        {
            _httpClient = http;
        }

        public async Task<IEnumerable<PostsDTOs>> Get()
        {
            
            var value = await _httpClient.GetFromJsonAsync<IEnumerable<PostsDTOs>>(_httpClient.BaseAddress);

            if (value == null)
            {
                return [];
            }
            return value;
        }
    }
}
