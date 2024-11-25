using ILIS.Newton.Assignment.Entities.Entities;

namespace ILIS.Newton.Assignment.Infrastructure.Repositories.Abstraction
{
    public interface IVideoGamesRepository : IGenericRepository<VideoGame>
    {
        Task<(IEnumerable<VideoGame> Items, int TotalCount)> GetPagedVideoGamesAsync(int pageNumber, int pageSize, string genre = null);
    }
}
