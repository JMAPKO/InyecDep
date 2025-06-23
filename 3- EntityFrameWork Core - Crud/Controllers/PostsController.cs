using BACKEND02.DTOs;
using BACKEND02.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BACKEND02.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {

        private IPostsService _postsService;

        public PostsController(IPostsService postsService)
        {
            _postsService = postsService;
        }

        [HttpGet("post")]
        public async Task<IEnumerable<PostsDTOs>> getPosts()
        {

            return await _postsService.Get();
          
        }    
    }
}
