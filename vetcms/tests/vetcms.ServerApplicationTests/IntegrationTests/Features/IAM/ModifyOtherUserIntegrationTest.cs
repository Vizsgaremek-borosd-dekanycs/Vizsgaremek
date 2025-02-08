using System;
using System.Linq;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using vetcms.ServerApplication.Common.Abstractions;
using vetcms.ServerApplication.Common.Abstractions.IAM;
using vetcms.ServerApplication.Common.IAM;
using vetcms.ServerApplication.Domain.Entity;
using vetcms.ServerApplication.Features.IAM;
using vetcms.ServerApplication.Features.IAM.ModifyOtherUser;
using vetcms.ServerApplication.Infrastructure.Communication.Mail;
using vetcms.ServerApplication.Infrastructure.Presistence;
using vetcms.ServerApplication.Infrastructure.Presistence.Repository;
using vetcms.SharedModels.Common.Dto;
using vetcms.SharedModels.Common.IAM.Authorization;
using vetcms.SharedModels.Features.IAM;
using Xunit;

namespace vetcms.ServerApplicationTests.IntegrationTests.Features.IAM
{
    public class ModifyOtherUserIntegrationTest : IClassFixture<WebApplicationFactory<WebApi.Program>>
    {
        private readonly WebApplicationFactory<WebApi.Program> _factory;
        private readonly ApplicationDbContext _dbContext;
        private readonly IAuthenticationCommon _authenticationCommon;
        private readonly IMailService _mailService;
        private readonly IRequestHandler<ModifyOtherUserApiCommand, ModifyOtherUserApiCommandResponse> _handler;

        public ModifyOtherUserIntegrationTest(WebApplicationFactory<WebApi.Program> factory)
        {
            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    var descriptor = services.SingleOrDefault(
                        d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

                    if (descriptor != null)
                    {
                        services.Remove(descriptor);
                    }

                    services.AddDbContext<ApplicationDbContext>(options =>
                    {
                        options.UseInMemoryDatabase("TestDatabase");
                    });

                    services.AddScoped<IAuthenticationCommon, AuthenticationCommon>();

                    var sp = services.BuildServiceProvider();

                    using (var scope = sp.CreateScope())
                    {
                        var scopedServices = scope.ServiceProvider;
                        var db = scopedServices.GetRequiredService<ApplicationDbContext>();
                        db.Database.EnsureCreated();
                    }
                });
            });

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("TestDatabase")
                .Options;
            _mailService = new Mock<IMailService>().Object;

            _dbContext = new ApplicationDbContext(options);

            using (var scope = _factory.Services.CreateScope())
            {
                _authenticationCommon = scope.ServiceProvider.GetRequiredService<IAuthenticationCommon>();
            }

            _handler = new ModifyOtherUserCommandHandler(new UserRepository(_dbContext), _mailService);
        }

        private string SeedAdminUser()
        {
            var adminUserId = 10000;
            var guid = Guid.NewGuid().ToString();
            var adminUser = new User
            {
                Id = adminUserId,
                Email = $"{guid}@admin.com",
                Password = PasswordUtility.HashPassword("AdminPassword123"),
                PhoneNumber = "06111111111",
                VisibleName = "Admin User",
            };

            adminUser.OverwritePermissions(new EntityPermissions().AddFlag(PermissionFlags.CAN_MODIFY_OTHER_USER));
            _dbContext.Set<User>().Add(adminUser);
            _dbContext.SaveChanges();
            return guid;
        }

        [Fact]
        public async Task ModifyOtherUser_Success()
        {
            // Ensure the database is in a clean state before the test
            await _dbContext.Database.EnsureDeletedAsync();

            // Arrange
            var adminUserGuid = SeedAdminUser(); // Create an admin user
            int userId = 1; // ID of the user to be modified

            // Add a user to the database
            var permission = new EntityPermissions().AddFlag(PermissionFlags.CAN_LOGIN);
            var user = new User
            {
                Id = userId,
                Email = $"test{userId}@test.com",
                Password = PasswordUtility.HashPassword("oldPassword123"),
                PhoneNumber = "06111111111",
                VisibleName = "Test User",
            };
            user.OverwritePermissions(permission);
            _dbContext.Set<User>().Add(user);
            await _dbContext.SaveChangesAsync();

            var modifyUserCommand = new ModifyOtherUserApiCommand
            {
                Id = userId,
                ModifiedUser = new UserDto()
                {
                    Id = userId,
                    Email = $"test{userId}@test.com",
                    PhoneNumber = "06111111111",
                    VisibleName = "Modified User",
                    Password = "newPassword123",
                }
            };
            modifyUserCommand.ModifiedUser.OverwritePermissions(permission);

            // Act
            var result = await _handler.Handle(modifyUserCommand, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Contains("Felhasználó módosítva.", result.Message);

            // Verify the user is modified
            var modifiedUser = await _dbContext.Set<User>().FindAsync(userId);
            Assert.NotNull(modifiedUser);
            Assert.Equal("Modified User", modifiedUser.VisibleName);
            Assert.True(PasswordUtility.VerifyPassword("newPassword123", modifiedUser.Password));
        }

        [Fact]
        public async Task ModifyOtherUser_NotFound()
        {
            // Arrange
            var adminUserGuid = SeedAdminUser(); // Create an admin user
            int userId = 999; // Assuming this user does not exist


            var modifyUserCommand = new ModifyOtherUserApiCommand
            {
                Id = userId,
                ModifiedUser = new UserDto()
                {
                    Id = userId,
                    Email = $"test{userId}@test.com",
                    PhoneNumber = "06111111111",
                    VisibleName = "Modified User",
                    Password = "newPassword123",
                }
            };

            // Act
            var result = await _handler.Handle(modifyUserCommand, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.Success);
            Assert.Equal("Nem létező felhasznló", result.Message);
        }
    }
}
