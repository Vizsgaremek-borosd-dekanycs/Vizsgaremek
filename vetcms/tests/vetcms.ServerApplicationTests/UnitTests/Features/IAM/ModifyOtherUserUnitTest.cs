using Moq;
using System.Threading;
using System.Threading.Tasks;
using vetcms.ServerApplication.Common.Abstractions;
using vetcms.ServerApplication.Domain.Entity;
using vetcms.ServerApplication.Infrastructure.Communication.Mail;
using vetcms.ServerApplication.Features.IAM.ModifyOtherUser;
using Xunit;
using MediatR;
using vetcms.ServerApplication.Common.Abstractions.Data;
using vetcms.SharedModels.Common.Abstract;
using vetcms.SharedModels.Features.IAM;

namespace vetcms.ServerApplicationTests.UnitTests.Features.IAM
{
    public class ModifyOtherUserUnitTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IMailService> _mailServiceMock;
        private readonly ModifyOtherUserCommandHandler _modifyHandler;

        public ModifyOtherUserUnitTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _mailServiceMock = new Mock<IMailService>();
            _modifyHandler = new ModifyOtherUserCommandHandler(_userRepositoryMock.Object, _mailServiceMock.Object);
        }

        [Fact]
        public async Task ModifyOtherUser_ShouldReturnSuccess_WhenUserIsModified()
        {
            // Arrange
            var user = new User
            {
                Id = 1,
                Email = "test@example.com",
                PhoneNumber = "1234567890",
                VisibleName = "Test User",
                Password = "oldPassword123",
            };
            _userRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>(), false)).ReturnsAsync(user);
            _userRepositoryMock.Setup(repo => repo.ExistAsync(It.IsAny<int>())).ReturnsAsync(true);

            var request = new ModifyOtherUserApiCommand
            {
                Id = 1,
                Email = "test@example.com",
                PhoneNumber = "1234567890",
                VisibleName = "Test User",
                Password = "newPassword123",
            };

            // Act
            var result = await _modifyHandler.Handle(request, CancellationToken.None);

            // Assert
            Assert.True(result.Success);
            _mailServiceMock.Verify(m => m.SendModifyOtherUserEmailAsync(
                It.Is<User>(u => u.Email == "test@example.com" && u.VisibleName == "Test User"),
                "newPassword123"), Times.Once);
        }

        [Fact]
        public async Task ModifyOtherUser_ShouldReturnFailure_WhenUserDoesNotExist()
        {
            // Arrange
            var user = new User
            {
                Id = 2, // Different ID to simulate non-existent user
                Email = "test@example.com",
                PhoneNumber = "1234567890",
                VisibleName = "Test User",
                Password = "oldPassword123",
                PasswordResets = new List<PasswordReset>()
            };
            _userRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>(), false)).ReturnsAsync(user);

            var request = new ModifyOtherUserApiCommand
            {
                Id = 100, // Requested ID that does not match the returned user's ID
                Password = "newPassword123"
            };

            // Act
            var result = await _modifyHandler.Handle(request, CancellationToken.None);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Nem létező felhasznló", result.Message);
        }
    }
}
