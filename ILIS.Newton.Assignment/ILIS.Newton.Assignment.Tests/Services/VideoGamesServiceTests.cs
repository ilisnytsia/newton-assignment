using AutoMapper;
using ILIS.Newton.Assignment.Application.Models.Dto;
using ILIS.Newton.Assignment.Application.Services;
using ILIS.Newton.Assignment.Infrastructure.Repositories.Abstraction;
using Microsoft.AspNetCore.JsonPatch;
using Moq;
using ILIS.Newton.Assignment.Entities.Entities;

namespace ILIS.Newton.Assignment.Tests.Services;

public class VideoGameServiceTests
{
    private readonly Mock<IVideoGamesRepository> _mockRepository;
    private readonly Mock<IMapper> _mockMapper;
    private readonly VideoGameService _service;

    public VideoGameServiceTests()
    {
        _mockRepository = new Mock<IVideoGamesRepository>();
        _mockMapper = new Mock<IMapper>();
        _service = new VideoGameService(_mockRepository.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnVideoGameDto_WhenVideoGameExists()
    {
        // Arrange
        var videoGame = new VideoGame { Id = 1, Title = "Test Game", Genre = "RPG" };
        var videoGameDto = new VideoGameDto { Id = 1, Title = "Test Game", Genre = "RPG" };

        _mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(videoGame);
        _mockMapper.Setup(mapper => mapper.Map<VideoGameDto>(videoGame)).Returns(videoGameDto);

        // Act
        var result = await _service.GetByIdAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Test Game", result.Title);
        Assert.Equal("RPG", result.Genre);
        _mockRepository.Verify(repo => repo.GetByIdAsync(1), Times.Once);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnNull_WhenVideoGameDoesNotExist()
    {
        // Arrange
        _mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((VideoGame)null);

        // Act
        var result = await _service.GetByIdAsync(1);

        // Assert
        Assert.Null(result);
        _mockRepository.Verify(repo => repo.GetByIdAsync(1), Times.Once);
    }

    [Fact]
    public async Task GetPagedVideoGamesAsync_ShouldReturnPagedResult()
    {
        // Arrange
        var videoGames = new List<VideoGame>
        {
            new VideoGame { Id = 1, Title = "Game 1", Genre = "RPG" },
            new VideoGame { Id = 2, Title = "Game 2", Genre = "Action" }
        };

        var videoGameDtos = new List<VideoGameDto>
        {
            new VideoGameDto { Id = 1, Title = "Game 1", Genre = "RPG" },
            new VideoGameDto { Id = 2, Title = "Game 2", Genre = "Action" }
        };

        _mockRepository.Setup(repo => repo.GetPagedVideoGamesAsync(1, 2, null))
            .ReturnsAsync((videoGames, 10));
        _mockMapper.Setup(mapper => mapper.Map<IEnumerable<VideoGameDto>>(videoGames))
            .Returns(videoGameDtos);

        // Act
        var result = await _service.GetPagedVideoGamesAsync(1, 2);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(10, result.TotalCount);
        Assert.Equal(2, result.Items.Count());
        _mockRepository.Verify(repo => repo.GetPagedVideoGamesAsync(1, 2, null), Times.Once);
    }

    [Fact]
    public async Task UpdatePartialVideoGameAsync_ShouldUpdateVideoGame_WhenVideoGameExists()
    {
        // Arrange
        var videoGame = new VideoGame { Id = 1, Title = "Old Title", Genre = "RPG" };
        var videoGameDto = new VideoGameDto { Id = 1, Title = "New Title", Genre = "RPG" };

        _mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(videoGame);
        _mockMapper.Setup(mapper => mapper.Map<VideoGameDto>(videoGame)).Returns(videoGameDto);
        _mockMapper.Setup(mapper => mapper.Map(videoGameDto, videoGame));

        var patchDoc = new JsonPatchDocument<VideoGameDto>();
        patchDoc.Replace(v => v.Title, "New Title");

        // Act
        var result = await _service.UpdatePartialVideoGameAsync(1, patchDoc);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("New Title", result.Title);
        _mockRepository.Verify(repo => repo.GetByIdAsync(1), Times.Once);
        _mockRepository.Verify(repo => repo.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task UpdatePartialVideoGameAsync_ShouldReturnNull_WhenVideoGameDoesNotExist()
    {
        // Arrange
        _mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((VideoGame)null);

        var patchDoc = new JsonPatchDocument<VideoGameDto>();
        patchDoc.Replace(v => v.Title, "New Title");

        // Act
        var result = await _service.UpdatePartialVideoGameAsync(1, patchDoc);

        // Assert
        Assert.Null(result);
        _mockRepository.Verify(repo => repo.GetByIdAsync(1), Times.Once);
        _mockRepository.Verify(repo => repo.SaveChangesAsync(), Times.Never);
    }
}
