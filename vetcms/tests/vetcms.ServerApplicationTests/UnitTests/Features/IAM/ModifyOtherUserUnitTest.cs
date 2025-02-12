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
using vetcms.SharedModels.Common.IAM.Authorization;
using vetcms.SharedModels.Common.Dto;
using System.Security;
using vetcms.ServerApplication.Common.Abstractions.IAM;
using Microsoft.EntityFrameworkCore;
using vetcms.ServerApplication.Features.IAM;

namespace vetcms.ServerApplicationTests.UnitTests.Features.IAM
{
    public class ModifyOtherUserUnitTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IMailService> _mailServiceMock;
        private readonly Mock<IAuthenticationCommon> _authenticationCommonMock;
        private readonly ModifyOtherUserCommandHandler _modifyHandler;

        public ModifyOtherUserUnitTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _mailServiceMock = new Mock<IMailService>();
            _authenticationCommonMock = new Mock<IAuthenticationCommon>();
            _modifyHandler = new ModifyOtherUserCommandHandler(_userRepositoryMock.Object, _mailServiceMock.Object, _authenticationCommonMock.Object);

            User adminUser = GenerateAdminUser();
            _authenticationCommonMock.Setup(x => x.GetUser(It.IsAny<string>())).ReturnsAsync(adminUser);
        }

        private User GenerateAdminUser()
        {
            var guid = Guid.NewGuid().ToString();
            var adminUser = new User
            {
                Email = $"{guid}@admin.com",
                Password = PasswordUtility.HashPassword(guid),
                PhoneNumber = guid,
                VisibleName = $"Admin{guid}",
            };

            adminUser.OverwritePermissions(new EntityPermissions().AddFlag(Enum.GetValues<PermissionFlags>()));
            return adminUser;
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
            var permission = new EntityPermissions().AddFlag(PermissionFlags.CAN_LOGIN);
            user.OverwritePermissions(permission);
            _userRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>(), false)).ReturnsAsync(user);
            _userRepositoryMock.Setup(repo => repo.ExistAsync(It.IsAny<int>(), false)).ReturnsAsync(true);

            var modifyUserCommand = new ModifyOtherUserApiCommand
            {
                Id = 1,
                ModifiedUser = new UserDto()
                {
                    Id = 1,
                    Email = "test@example.com",
                    PhoneNumber = "1234567890",
                    VisibleName = "Test User",
                    Password = "newPassword123",
                }
            };
            modifyUserCommand.ModifiedUser.OverwritePermissions(permission);

            // Act
            var result = await _modifyHandler.Handle(modifyUserCommand, CancellationToken.None);

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
            _userRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>(), false)).ReturnsAsync((User)null);

            var modifyUserCommand = new ModifyOtherUserApiCommand
            {
                Id = 100,
                ModifiedUser = new UserDto()
                {
                    Id = 100,
                    Email = "test@example.com",
                    PhoneNumber = "1234567890",
                    VisibleName = "Test User",
                    Password = "newPassword123",
                }
            };
            modifyUserCommand.ModifiedUser.OverwritePermissions(new EntityPermissions());

            // Act
            var result = await _modifyHandler.Handle(modifyUserCommand, CancellationToken.None);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Nem létező felhasznló", result.Message);
        }
    }
}
