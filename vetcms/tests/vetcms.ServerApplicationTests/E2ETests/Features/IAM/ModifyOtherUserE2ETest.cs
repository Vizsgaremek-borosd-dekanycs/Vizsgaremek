using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using vetcms.ServerApplication;
using vetcms.ServerApplication.Common.Abstractions.IAM;
using vetcms.ServerApplication.Common.IAM;
using vetcms.ServerApplication.Domain.Entity;
using vetcms.ServerApplication.Features.IAM;
using vetcms.ServerApplication.Infrastructure.Presistence;
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

        public ModifyOtherUserE2ETest(WebApplicationFactory<WebApi.Program> factory)
        {
            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    // Replace the real database with an in-memory database for testing
                    services.AddDbContext<ApplicationDbContext>(options =>
                    {
                        options.UseInMemoryDatabase("TestDb_ModifyOtherUser");
                    });

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

        private string GenerateBearerToken(string guid)
        {
            var user = _dbContext.Set<User>().First(u => u.Email.Contains(guid));
            return _authenticationCommon.GenerateAccessToken(user);
        }

        private EntityPermissions AddPermission()
        {
            var permission = new EntityPermissions().AddFlag(PermissionFlags.CAN_LOGIN);
            return permission;
        }

        private int CreateTestUser()
        {
            var user = new User
            {
                Id = 1,
                Email = $"test1@test.com",
                Password = PasswordUtility.HashPassword("oldPassword123"),
                PhoneNumber = "06111111111",
                VisibleName = "Test User"
            };
            user.OverwritePermissions(AddPermission());
            _dbContext.Set<User>().Add(user);
            _dbContext.SaveChangesAsync();
            return user.Id;
        }

        [Fact]
        public async Task ModifyOtherUser_Success()
        {
            // Ensure the database is in a clean state before the test
            //await _dbContext.Database.EnsureDeletedAsync();

            // Arrange
            var adminUserGuid = SeedAdminUser(); // Create an admin user
            var client = _factory.CreateClient();
            int userId = CreateTestUser(); // ID of the user to be modified

            var modifyUserCommand = new ModifyOtherUserApiCommand
            {
                Id = userId,
                Email = $"test{userId}@test.com",
                PhoneNumber = "06111111111",
                VisibleName = "Modified User",
                Password = "newPassword123",
                PermissionSet = AddPermission().ToString()
            };

            // Add authorization
            var token = GenerateBearerToken(adminUserGuid);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            // Act
            var response = await client.PutAsJsonAsync("/api/v1/iam/modify-other-user", modifyUserCommand);
            //response.EnsureSuccessStatusCode(); // Ensure the request was successful

            var result = await response.Content.ReadFromJsonAsync<ModifyOtherUserApiCommandResponse>();

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Equal("Felhasználó módosítva.", result.Message);

            // Verify the user is modified
            var modifiedUser = await _dbContext.Set<User>().FindAsync(userId);
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

            var modifyUserCommand = new ModifyOtherUserApiCommand()
            {
                Id = userId,
                Email = $"test{userId}@test.com",
                PhoneNumber = "06111111111",
                VisibleName = "Modified User",
                Password = "newPassword123",
                PermissionSet = AddPermission().ToString()
            };

            // Add authorization
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", GenerateBearerToken(adminUserGuid));

            // Act
            var response = await client.PutAsJsonAsync("/api/v1/iam/modify-other-user", modifyUserCommand);
            var result = await response.Content.ReadFromJsonAsync<ModifyOtherUserApiCommandResponse>();

            // Assert
            Assert.NotNull(result);
            Assert.False(result.Success);
            Assert.Equal("Nem létező felhasznló", result.Message);
        }

        [Fact]
        public async Task ModifyOtherUser_Unauthorized()
        {
            // Arrange
            await _dbContext.Database.EnsureDeletedAsync();
            var client = _factory.CreateClient();
            var adminUserGuid = SeedAdminUser();
            var admin = _dbContext.Set<User>().First(u => u.Email.Contains(adminUserGuid));

            // Remove the required permission
            admin.OverwritePermissions(new EntityPermissions().RemoveFlag(PermissionFlags.CAN_MODIFY_OTHER_USER));
            await _dbContext.SaveChangesAsync(); // Ensure changes persist

            int userId = 1; // ID of the user to be modified

            var modifyUserCommand = new ModifyOtherUserApiCommand
            {
                Id = userId,
                Email = $"test{userId}@test.com",
                PhoneNumber = "06111111111",
                VisibleName = "Modified User",
                Password = "newPassword123",
                PermissionSet = AddPermission().ToString()
            };

            // Act
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", GenerateBearerToken(adminUserGuid));
            var response = await client.PutAsJsonAsync("/api/v1/iam/modify-other-user", modifyUserCommand);
            var responseBody = await response.Content.ReadAsStringAsync(); // Capture response for debugging

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.Forbidden, response.StatusCode); // Corrected from Unauthorized to Forbidden
            Assert.Contains("Nem megfelelő hozzáférés.", responseBody); // Ensure meaningful error message
        }
    }
}

