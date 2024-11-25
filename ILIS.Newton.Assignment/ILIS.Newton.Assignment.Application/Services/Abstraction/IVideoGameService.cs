using ILIS.Newton.Assignment.Application.Models.Dto;
using ILIS.Newton.Assignment.Application.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace ILIS.Newton.Assignment.Application.Services.Abstraction
{
    public interface IVideoGameService
    {
        Task<VideoGameDto> GetByIdAsync(int id);

        Task<PagedResult<VideoGameDto>> GetPagedVideoGamesAsync(int pageNumber, int pageSize, string? genre = null);

        Task<VideoGameDto> UpdatePartialVideoGameAsync(int id, JsonPatchDocument<VideoGameDto> patchDoc);
    }
}
