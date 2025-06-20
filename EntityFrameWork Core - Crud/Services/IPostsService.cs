namespace BACKEND02.Services
{
    public interface IPostsService
    {
        public Task<IEnumerable<DTOs.PostsDTOs>> Get();
    }
}
