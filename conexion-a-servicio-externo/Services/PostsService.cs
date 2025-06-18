using BACKEND02.DTOs;

namespace BACKEND02.Services
{
    public class PostsService : IPostsService
    {
        private HttpClient _httpClient;

        public PostsService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<IEnumerable<PostsDTOs>> Get()
        {
            var url = "https://jsonplaceholder.typicode.com/posts";
            var value = await _httpClient.GetFromJsonAsync<IEnumerable<PostsDTOs>>(url);

            if (value == null)
            {
                return [];
            }
            return value;
        }
    }
}
