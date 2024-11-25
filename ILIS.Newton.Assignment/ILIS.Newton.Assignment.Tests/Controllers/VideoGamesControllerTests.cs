using ILIS.Newton.Assignment.Application.Models.Dto;
using ILIS.Newton.Assignment.Application.Models;
using ILIS.Newton.Assignment.Application.Services.Abstraction;
using Microsoft.AspNetCore.JsonPatch;
using Moq;
using ILIS.Newton.Assignment.API.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace ILIS.Newton.Assignment.Tests.Controllers
{
    public class VideoGamesControllerTests
    {
        private readonly Mock<IVideoGameService> _mockService;
        private readonly VideoGamesController _controller;

        public VideoGamesControllerTests()
        {
            _mockService = new Mock<IVideoGameService>();
            _controller = new VideoGamesController(_mockService.Object);
        }

        [Fact]
        public async Task GetVideoGameById_ShouldReturnOk_WhenVideoGameExists()
        {
            // Arrange
            var videoGame = new VideoGameDto { Id = 1, Title = "Test Game", Genre = "RPG" };
            _mockService.Setup(service => service.GetByIdAsync(1)).ReturnsAsync(videoGame);

            // Act
            var result = await _controller.GetVideoGameById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(videoGame, okResult.Value);
            _mockService.Verify(service => service.GetByIdAsync(1), Times.Once);
        }

        [Fact]
        public async Task GetVideoGameById_ShouldReturnNotFound_WhenVideoGameDoesNotExist()
        {
            // Arrange
            _mockService.Setup(service => service.GetByIdAsync(1)).ReturnsAsync((VideoGameDto)null);

            // Act
            var result = await _controller.GetVideoGameById(1);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, notFoundResult.StatusCode);
            Assert.Equal("VideoGame with ID 1 not found.", notFoundResult.Value);
            _mockService.Verify(service => service.GetByIdAsync(1), Times.Once);
        }

        [Fact]
        public async Task GetPaged_ShouldReturnOkWithPagedResult()
        {
            // Arrange
            var pagedResult = new PagedResult<VideoGameDto>
            {
                Items = new List<VideoGameDto>
            {
                new VideoGameDto { Id = 1, Title = "Game 1", Genre = "RPG" },
                new VideoGameDto { Id = 2, Title = "Game 2", Genre = "Action" }
            },
                TotalCount = 2,
                CurrentPage = 1,
                PageSize = 10,
                TotalPages = 1
            };

            _mockService.Setup(service => service.GetPagedVideoGamesAsync(1, 10, null))
                        .ReturnsAsync(pagedResult);

            // Act
            var result = await _controller.GetPaged(1, 10, null);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(pagedResult, okResult.Value);
            _mockService.Verify(service => service.GetPagedVideoGamesAsync(1, 10, null), Times.Once);
        }

        [Fact]
        public async Task Patch_ShouldReturnOk_WhenVideoGameIsUpdated()
        {
            // Arrange
            var videoGameDto = new VideoGameDto { Id = 1, Title = "Updated Title", Genre = "RPG" };
            var patchDoc = new JsonPatchDocument<VideoGameDto>();
            patchDoc.Replace(v => v.Title, "Updated Title");

            _mockService.Setup(service => service.UpdatePartialVideoGameAsync(1, patchDoc))
                        .ReturnsAsync(videoGameDto);

            // Act
            var result = await _controller.Patch(1, patchDoc);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(videoGameDto, okResult.Value);
            _mockService.Verify(service => service.UpdatePartialVideoGameAsync(1, patchDoc), Times.Once);
        }

        [Fact]
        public async Task Patch_ShouldReturnBadRequest_WhenPatchDocumentIsNull()
        {
            // Arrange
            JsonPatchDocument<VideoGameDto> patchDoc = null;

            // Act
            var result = await _controller.Patch(1, patchDoc);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequestResult.StatusCode);
            Assert.Equal("Patch document cannot be null.", badRequestResult.Value);
            _mockService.Verify(service => service.UpdatePartialVideoGameAsync(It.IsAny<int>(), It.IsAny<JsonPatchDocument<VideoGameDto>>()), Times.Never);
        }

        [Fact]
        public async Task Patch_ShouldReturnNotFound_WhenVideoGameDoesNotExist()
        {
            // Arrange
            var patchDoc = new JsonPatchDocument<VideoGameDto>();
            patchDoc.Replace(v => v.Title, "Updated Title");

            _mockService.Setup(service => service.UpdatePartialVideoGameAsync(1, patchDoc))
                        .ReturnsAsync((VideoGameDto)null);

            // Act
            var result = await _controller.Patch(1, patchDoc);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, notFoundResult.StatusCode);
            Assert.Equal("VideoGame with ID 1 not found.", notFoundResult.Value);
            _mockService.Verify(service => service.UpdatePartialVideoGameAsync(1, patchDoc), Times.Once);
        }

    }
}