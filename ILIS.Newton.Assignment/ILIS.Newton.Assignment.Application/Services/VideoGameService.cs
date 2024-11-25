using AutoMapper;
using ILIS.Newton.Assignment.Application.Models;
using ILIS.Newton.Assignment.Application.Models.Dto;
using ILIS.Newton.Assignment.Application.Services.Abstraction;
using ILIS.Newton.Assignment.Infrastructure.Repositories.Abstraction;
using Microsoft.AspNetCore.JsonPatch;

namespace ILIS.Newton.Assignment.Application.Services
{
    public class VideoGameService : IVideoGameService
    {
        private readonly IVideoGamesRepository _videoGamesRepository;
        private readonly IMapper _mapper;

        public VideoGameService(IVideoGamesRepository videoGamesRepository, IMapper mapper)
        {
            _videoGamesRepository = videoGamesRepository;
            _mapper = mapper;
        }

        public async Task<VideoGameDto> GetByIdAsync(int id)
        {
            var videoGame = await _videoGamesRepository.GetByIdAsync(id);

            if (videoGame == null) 
            {
                return null;
            }

            return _mapper.Map<VideoGameDto>(videoGame);
        }

        public async Task<PagedResult<VideoGameDto>> GetPagedVideoGamesAsync(int pageNumber, int pageSize, string? genre = null)
        {
            var (items, totalCount) = await _videoGamesRepository.GetPagedVideoGamesAsync(pageNumber, pageSize, genre);

            var mappedItems = _mapper.Map<IEnumerable<VideoGameDto>>(items);

            return new PagedResult<VideoGameDto>
            {
                Items = mappedItems,
                TotalCount = totalCount,
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
            };
        }

        public async Task<VideoGameDto> UpdatePartialVideoGameAsync(int id, JsonPatchDocument<VideoGameDto> patchDoc)
        {
            var videoGame = await _videoGamesRepository.GetByIdAsync(id);
            if (videoGame == null)
            {
                return null;
            }

            var videoGameDto = _mapper.Map<VideoGameDto>(videoGame);
            patchDoc.ApplyTo(videoGameDto);

            //alternatively use update method and no tracking aproach
            _mapper.Map(videoGameDto, videoGame);

            await _videoGamesRepository.SaveChangesAsync();

            return videoGameDto;
        }
    }
}
