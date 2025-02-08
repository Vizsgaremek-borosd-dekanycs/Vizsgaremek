using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using vetcms.ServerApplication;
using vetcms.ServerApplication.Common.Abstractions;
using vetcms.ServerApplication.Common.Abstractions.IAM;
using vetcms.ServerApplication.Common.IAM;
using vetcms.ServerApplication.Domain.Entity;
using vetcms.ServerApplication.Features.IAM;
using vetcms.ServerApplication.Infrastructure.Presistence;
using vetcms.SharedModels.Common.Dto;
using vetcms.SharedModels.Common.IAM.Authorization;
using vetcms.SharedModels.Features.IAM;
using Xunit;

namespace vetcms.ServerApplicationTests.E2ETests.Features.IAM
{
    public class ModifyOtherUserE2ETest : IClassFixture<WebApplicationFactory<WebApi.Program>>
    {
        private readonly WebApplicationFactory<WebApi.Program> _factory;
        private IServiceScope _scope;
        private ApplicationDbContext _dbContext;
        private IAuthenticationCommon _authenticationCommon;
        private Mock<IMailService> _mockMailService;

        public ModifyOtherUserE2ETest(WebApplicationFactory<WebApi.Program> factory)
        {
            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    // Replace the real database with an in-memory database for testing
                    services.AddDbContext<ApplicationDbContext>(options =>
                    {
                        options.UseInMemoryDatabase("TestDb_ModifyOtherUserE2E");
                    });

                    _mockMailService = new Mock<IMailService>();
                    services.AddSingleton(_mockMailService.Object);

                    // Build the service provider
                    var serviceProvider = services.BuildServiceProvider();
                    _scope = serviceProvider.CreateScope();
                    _dbContext = _scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                    _authenticationCommon = _scope.ServiceProvider.GetRequiredService<IAuthenticationCommon>();

                    // Ensure the database is created
                    _dbContext.Database.EnsureCreated();
                });
            });

            // Ensure the web host is created before running any test cases
            _factory.CreateClient();
        }

        private string SeedAdminUser()
        {
            var guid = Guid.NewGuid().ToString();
            var adminUser = new User
            {
                Email = $"{guid}@admin.com",
                Password = PasswordUtility.HashPassword("AdminPassword123"),
                PhoneNumber = "06111111111",
                VisibleName = guid,
            };

            adminUser.OverwritePermissions(new EntityPermissions().AddFlag(PermissionFlags.CAN_MODIFY_OTHER_USER));
            _dbContext.Set<User>().Add(adminUser);
            _dbContext.SaveChanges();
            return guid;
        }

        private async Task<string> GenerateBearerToken(string guid)
        {
            User user = _dbContext.Set<User>().First(u => u.Email.Contains(guid));
            await _dbContext.Entry(user).ReloadAsync();

            return _authenticationCommon.GenerateAccessToken(user);
        }

        private EntityPermissions GetDefaultPermissions()
        {
            var permission = new EntityPermissions().AddFlag(PermissionFlags.CAN_LOGIN);
            return permission;
        }

        private async Task<string> CreateTestUser()
        {
            var guid = Guid.NewGuid().ToString();
            var user = new User
            {
                Email = $"test{guid}@test.com",
                Password = PasswordUtility.HashPassword("oldPassword123"),
                PhoneNumber = "06111111111",
                VisibleName = guid
            };
            user.OverwritePermissions(GetDefaultPermissions());
            _dbContext.Set<User>().Add(user);
            await _dbContext.SaveChangesAsync();
            return guid;
        }

        [Fact]
        public async Task ModifyOtherUser_Success()
        {
            // Ensure the database is in a clean state before the test
            //await _dbContext.Database.EnsureDeletedAsync();

            // Arrange
            var adminUserGuid = SeedAdminUser(); // Create an admin user
            var client = _factory.CreateClient();
            string userGuid = await CreateTestUser(); // Create a user to be modified
            int id = _dbContext.Set<User>().First(u => u.Email.Contains(userGuid)).Id;

            var modifyUserCommand = new ModifyOtherUserApiCommand
            {
                Id = id,
                ModifiedUser = new UserDto()
                {
                    Email = $"test{userGuid}@test.com",
                    PhoneNumber = "06111111111",
                    VisibleName = "Modified User",
                    Password = "newPassword123"
                }
            };
            modifyUserCommand.ModifiedUser.OverwritePermissions(GetDefaultPermissions());

            // Add authorization
            var token = await GenerateBearerToken(adminUserGuid);
            
            Assert.NotNull(token);

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            // Act
            var response = await client.PutAsJsonAsync("/api/v1/iam/modify-other-user", modifyUserCommand);
            //response.EnsureSuccessStatusCode(); // Ensure the request was successful
            var ANYÁD = await response.Content.ReadAsStringAsync();
            Console.WriteLine(ANYÁD);


            var result = await response.Content.ReadFromJsonAsync<ModifyOtherUserApiCommandResponse>();

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Contains("Felhasználó módosítva.", result.Message);

            _mockMailService.Verify(m => m.SendModifyOtherUserEmailAsync(It.IsAny<User>(),It.IsAny<string>()), Times.Once);

            // Verify the user is modified
            _dbContext = _scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var modifiedUser = await _dbContext.Set<User>().FindAsync(id);
            await _dbContext.Entry(modifiedUser).ReloadAsync();
            Assert.NotNull(modifiedUser);
            Assert.Equal("Modified User", modifiedUser.VisibleName);
            Assert.True(PasswordUtility.VerifyPassword("newPassword123", modifiedUser.Password));
        }

        [Fact]
        public async Task ModifyOtherUser_NotFound()
        {

            var adminUserGuid = SeedAdminUser();
            var client = _factory.CreateClient();
            int userId = 999; // A user ID that does not exist

            var modifyUserCommand = new ModifyOtherUserApiCommand
            {
                Id = userId,
                ModifiedUser = new UserDto()
                {
                    Email = $"test{userId}@test.com",
                    PhoneNumber = "06111111111",
                    VisibleName = "Modified User",
                    Password = "newPassword123",
                }
            };
            modifyUserCommand.ModifiedUser.OverwritePermissions(GetDefaultPermissions());

            // Add authorization
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", await GenerateBearerToken(adminUserGuid));

            // Act
            var response = await client.PutAsJsonAsync("/api/v1/iam/modify-other-user", modifyUserCommand);
            var result = await response.Content.ReadFromJsonAsync<ModifyOtherUserApiCommandResponse>();

            // Assert
            Assert.NotNull(result);
            Assert.False(result.Success);
            Assert.Equal("Nem létező felhasznló", result.Message);
        }

        [Fact]
        public async Task ModifyOtherUser_Forbidden()
        {
            // Arrange
            string adminUserGuid = await CreateTestUser(); // Create an admin user
            var client = _factory.CreateClient();
            string userGuid = await CreateTestUser(); // Create a user to be modified
            int id = _dbContext.Set<User>().First(u => u.Email.Contains(userGuid)).Id;

            var modifyUserCommand = new ModifyOtherUserApiCommand
            {
                Id = id,
                ModifiedUser = new UserDto()
                {
                    Id = id,
                    Email = $"test{id}@test.com",
                    PhoneNumber = "06111111111",
                    VisibleName = "Modified User",
                    Password = "newPassword123",
                }
            };
            modifyUserCommand.ModifiedUser.OverwritePermissions(GetDefaultPermissions());

            // Act
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", await GenerateBearerToken(adminUserGuid));
            var response = await client.PutAsJsonAsync("/api/v1/iam/modify-other-user", modifyUserCommand);
            var responseBody = await response.Content.ReadAsStringAsync(); // Capture response for debugging

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.Forbidden, response.StatusCode); // Corrected from Unauthorized to Forbidden
            Assert.Contains("Nem megfelelő hozzáférés.", responseBody); // Ensure meaningful error message
        }
    }
}

