using ILIS.Newton.Assignment.Application.Models;
using ILIS.Newton.Assignment.Application.Models.Dto;
using ILIS.Newton.Assignment.Application.Services.Abstraction;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace ILIS.Newton.Assignment.API.Controllers
{
    [ApiController]
    [Route("api/videogames")]
    public class VideoGamesController : ControllerBase
    {
        private readonly IVideoGameService _videoGameService;

        public VideoGamesController(IVideoGameService videoGameService)
        {
            _videoGameService = videoGameService;
        }


        /// <summary>
        /// Retrieves a video game by Id.
        /// </summary>
        /// <returns>
        /// A 200 OK with object, 
        /// 404 Not Found if the video game is not found.
        /// </returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVideoGameById(int id)
        {
            var videoGame = await _videoGameService.GetByIdAsync(id);

            if (videoGame == null)
            {
                return NotFound($"VideoGame with ID {id} not found.");
            }

            return Ok(videoGame);
        }

        /// <summary>
        /// Retrieves a paginated list of video games, optionally filtered by genre.
        /// </summary>
        [HttpGet("paged")]
        public async Task<ActionResult<PagedResult<VideoGameDto>>> GetPaged(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? genre = null)
        {
            var result = await _videoGameService.GetPagedVideoGamesAsync(pageNumber, pageSize, genre);
            return Ok(result);
        }

        /// <summary>
        /// Partially updates a video game entity using a JSON Patch document.
        /// </summary>
        /// /// <param name="id">The ID of the video game to update.</param>
        /// <param name="patchDoc">The JSON Patch document specifying the updates.</param>
        /// <returns>
        /// A 200 OK with updated object, 
        /// 400 Bad Request for invalid input, or 
        /// 404 Not Found if the video game is not found.
        /// </returns>
        [HttpPatch("{id}")]
        public async Task<ActionResult> Patch(int id, [FromBody] JsonPatchDocument<VideoGameDto> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest("Patch document cannot be null.");
            }

            var videoGame = await _videoGameService.UpdatePartialVideoGameAsync(id, patchDoc);
            if (videoGame == null)
            {
                return NotFound($"VideoGame with ID {id} not found.");
            }

            return Ok(videoGame);
        }
    }
}
