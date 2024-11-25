using ILIS.Newton.Assignment.DataAccess;
using ILIS.Newton.Assignment.Entities.Entities;
using ILIS.Newton.Assignment.Infrastructure.Repositories.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace ILIS.Newton.Assignment.Infrastructure.Repositories
{
    public class VideoGamesRepository : GenericRepository<VideoGame>, IVideoGamesRepository
    {
        public VideoGamesRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<(IEnumerable<VideoGame> Items, int TotalCount)> GetPagedVideoGamesAsync(int pageNumber, int pageSize, string genre = null)
        {
            var query = _dbSet.AsQueryable();


            if (!string.IsNullOrEmpty(genre))
            {
                query = query.Where(v => v.Genre == genre);
            }

            var totalCount = await query.CountAsync();

            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (Items: items, TotalCount: totalCount);
        }
    }
}
