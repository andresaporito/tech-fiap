
using FluentAssertions;
using Moq;
using Tech.Domain.Entities;
using Tech.Domain.Interfaces.Repositories;
using Tech.Domain.Interfaces.Services.Tech.Domain.Interfaces.Repositories;
using Tech.Domain.Requests;
using Tech.Services.Services;

namespace Tech.Tests.Services
{
    public class UsersServiceTests
    {
        private readonly Mock<IUsersRepository> _mockUsersRepository;
        private readonly Mock<IGameRepository> _mockGameRepository;
        private readonly Mock<IUserGameRepository> _mockUserGameRepository;
        private readonly UsersService _usersService;

        public UsersServiceTests()
        {
            _mockUsersRepository = new Mock<IUsersRepository>();
            _mockGameRepository = new Mock<IGameRepository>();
            _mockUserGameRepository = new Mock<IUserGameRepository>();
            _usersService = new UsersService(_mockUsersRepository.Object, _mockGameRepository.Object, _mockUserGameRepository.Object);
        }

        [Fact]
        public async Task AddUsers_ShouldAddUserAndReturnResponse()
        {
            // Arrange
            var request = new NewUsersRequest
            {
                Name = "Fabio",
                Password = "1234",
                Email = "fabio@fabio.com",
                Permission = Tech.Domain.Enums.TypePermissionEnum.User
            };

            // Act
            var response = await _usersService.AddUsers(request);

            // Assert
            response.Should().NotBeNull();
            response.Name.Should().Be(request.Name);
        }

        [Fact]
        public async Task GetAll_ShouldReturnAllUsers()
        {
            // Arrange
            var users = new List<Users>
            {
                new Users("Carol", "pass", "user1@mail.com", Tech.Domain.Enums.TypePermissionEnum.User),
                new Users("Joana", "pass", "user2@mail.com", Tech.Domain.Enums.TypePermissionEnum.User),
            };

            _mockUsersRepository.Setup(r => r.GetAll()).ReturnsAsync(users);

            // Act
            var result = await _usersService.GetAll();

            // Assert
            result.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetId_ShouldReturnUser_WhenExists()
        {
            // Arrange
            var user = new Users("Joao", "pass", "joao@joao.com", Tech.Domain.Enums.TypePermissionEnum.User);
            _mockUsersRepository.Setup(r => r.GetId(1)).ReturnsAsync(user);

            // Act
            var result = await _usersService.GetId(1);

            // Assert
            result.Should().NotBeNull();
            result.Name.Should().Be(user.Name);
        }

    }
}
