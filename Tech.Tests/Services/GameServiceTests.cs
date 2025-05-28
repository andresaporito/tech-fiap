using AutoFixture;
using FluentAssertions;
using Moq;
using Tech.Domain.Entities;
using Tech.Domain.Interfaces.Repositories;
using Tech.Domain.Requests;
using Tech.Services.Services;

namespace Tech.Tests.Services
{
    public class GameServiceTests
    {
        private readonly Fixture _fixture;
        private readonly GameService _gameService;
        private readonly Mock<IGameRepository> _mockGameRepository;

        public GameServiceTests()
        {
            _mockGameRepository = new Mock<IGameRepository>();
            _gameService = new GameService(_mockGameRepository.Object);
            _fixture = new Fixture();
        }

        [Fact]
        public async Task AddGame_ShouldReturnGameResponse()
        {
            // Arrange
            var request = _fixture.Create<GameRequest>();
            var expectedGame = new Game(request.Nome, request.Categoria, request.Plataforma, request.DataLancamento);

            _mockGameRepository
                .Setup(repo => repo.AddGame(It.IsAny<Game>()))
                .ReturnsAsync(expectedGame);

            // Act
            var response = await _gameService.AddGame(request);

            // Assert
            response.Should().NotBeNull();
            response.Nome.Should().Be(expectedGame.Nome);
            response.Categoria.Should().Be(expectedGame.Categoria);
            response.Plataforma.Should().Be(expectedGame.Plataforma);
            response.DataLancamento.Should().Be(expectedGame.DataLancamento);

            _mockGameRepository.Verify(repo => repo.AddGame(It.Is<Game>(g =>
                g.Nome == request.Nome &&
                g.Categoria == request.Categoria &&
                g.Plataforma == request.Plataforma &&
                g.DataLancamento == request.DataLancamento)), Times.Once);
        }

        [Fact]
        public async Task Delete_WhenGameExists_ShouldReturnGameResponse()
        {
            // Arrange
            var game = _fixture.Create<Game>();
            _mockGameRepository.Setup(repo => repo.GetId(It.IsAny<int>())).ReturnsAsync(game);
            _mockGameRepository.Setup(repo => repo.DeleteGame(game)).ReturnsAsync(game);

            // Act
            var result = await _gameService.Delete(It.IsAny<int>());

            // Assert
            result.Should().NotBeNull();
            result.Nome.Should().Be(game.Nome);
            result.Categoria.Should().Be(game.Categoria);
            result.Plataforma.Should().Be(game.Plataforma);
            result.DataLancamento.Should().Be(game.DataLancamento);
        }

        [Fact]
        public async Task Delete_WhenGameDoesNotExist_ShouldThrowKeyNotFoundException()
        {
            // Arrange
            int idFix = It.IsAny<int>();
            _mockGameRepository.Setup(repo => repo.GetId(idFix)).ReturnsAsync((Game)null);

            // Act
            var act = async () => await _gameService.Delete(idFix);

            // Assert
            await act.Should().ThrowAsync<KeyNotFoundException>()
                .WithMessage($"Jogo com Id {idFix} não foi encontrado.");
        }

        [Fact]
        public async Task GetAll_ShouldReturnListOfGameResponse()
        {
            // Arrange
            var games = _fixture.Create<List<Game>>();
            _mockGameRepository.Setup(repo => repo.GetAll()).ReturnsAsync(games);

            // Act
            var result = await _gameService.GetAll();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(games.Count);

            for (int i = 0; i < games.Count; i++)
            {
                result[i].Nome.Should().Be(games[i].Nome);
                result[i].Categoria.Should().Be(games[i].Categoria);
                result[i].Plataforma.Should().Be(games[i].Plataforma);
                result[i].DataLancamento.Should().Be(games[i].DataLancamento);
            }
        }




    }
}
