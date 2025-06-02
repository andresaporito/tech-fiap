using FluentAssertions;
using Moq;
using Tech.Domain.Entities;
using Tech.Domain.Interfaces.Repositories;
using Tech.Services.Services;

namespace Tech.Tests.Services
{
    public class GameServiceTests
    {
        private readonly Mock<IGameRepository> _mockRepo;
        private readonly GameService _gameService;

        public GameServiceTests()
        {
            _mockRepo = new Mock<IGameRepository>();
            _gameService = new GameService(_mockRepo.Object);
        }

        [Fact]
        public async Task GetAll_ShouldReturnAllGames()
        {
            // Arrange
            var games = new List<Game>
            {
                new Game("God Of War", "Action", "PS5", DateTime.UtcNow),
                new Game("Call of Duty", "Action", "PS5", DateTime.UtcNow)
            };
            _mockRepo.Setup(r => r.GetAll()).ReturnsAsync(games);

            // Act
            var result = await _gameService.GetAll();

            // Assert
            result.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetId_ShouldReturnGame_WhenFound()
        {
            // Arrange
            var game = new Game("Fifa", "Action", "PS4", DateTime.UtcNow);
            typeof(Game).GetProperty("Id").SetValue(game, 1);
            _mockRepo.Setup(r => r.GetId(1)).ReturnsAsync(game);

            // Act
            var result = await _gameService.GetId(1);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(1);
        }

    }
}
