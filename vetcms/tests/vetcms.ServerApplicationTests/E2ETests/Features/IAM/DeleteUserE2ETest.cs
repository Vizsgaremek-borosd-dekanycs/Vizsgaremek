using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
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
    public class DeleteUserE2ETest : IClassFixture<WebApplicationFactory<WebApi.Program>>
    {
        private readonly WebApplicationFactory<WebApi.Program> _factory;
        private IServiceScope _scope;
        private ApplicationDbContext _dbContext;
        private IAuthenticationCommon _authenticationCommon;

        public DeleteUserE2ETest(WebApplicationFactory<WebApi.Program> factory)
        {
            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    // Replace the real database with an in-memory database for testing
                    services.AddDbContext<ApplicationDbContext>(options =>
                    {
                        options.UseInMemoryDatabase("TestDb_AssignPermission");
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
            var guid = Guid.NewGuid().ToString();
            var adminUser = new User
            {
                Email = $"{guid}@admin.com",
                Password = PasswordUtility.HashPassword("AdminPassword123"),
                PhoneNumber = "06111111111",
                VisibleName = "Admin User",
            };

            adminUser.OverwritePermissions(new EntityPermissions().AddFlag(PermissionFlags.CAN_DELETE_USERS));
            _dbContext.Set<User>().Add(adminUser);
            _dbContext.SaveChangesAsync();
            return guid;
        }

        private string SeedDatabase()
        {
            var guid = Guid.NewGuid().ToString();
            var user = new User
            {
                Email = $"{guid}@example.com",
                Password = PasswordUtility.HashPassword("ValidPassword123"),
                PhoneNumber = "1234567890",
                VisibleName = guid
            };
            _dbContext.Set<User>().Add(user);
            _dbContext.SaveChanges();

            return guid;
        }

        private string GenerateBearerToken(string guid)
        {
            var user = _dbContext.Set<User>().First(u => u.Email.Contains(guid));
            return _authenticationCommon.GenerateAccessToken(user);
        }

        [Fact]
        public async Task DeleteUserById_Success()
        {
            // Arrange

            var client = _factory.CreateClient();
            var adminUserGuid = SeedAdminUser(); // Create an admin user

            string userGuid = SeedDatabase();
            int userId = _dbContext.Set<User>().First(x => x.Email.Contains(userGuid)).Id;

            var deleteUserCommand = new DeleteUserApiCommand(userId);

            // Add authorization
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", GenerateBearerToken(adminUserGuid));

            // Act
            var request = new HttpRequestMessage
            {
                Content = JsonContent.Create(deleteUserCommand), // Ensure correct serialization
                Method = HttpMethod.Delete,
                RequestUri = new Uri("/api/v1/iam/user")
            };

            var response = await client.SendAsync(request);
            //response.EnsureSuccessStatusCode(); // Ensure the request was successful

            var result = await response.Content.ReadFromJsonAsync<DeleteUserApiCommandResponse>();

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Equal("", result.Message);

            // Verify the user is deleted
            var userDeleted = _dbContext.Set<User>().Where(x => x.Deleted).Select(x => x.Id);
            bool deleted = userDeleted.Contains(userId);
            Assert.False(deleted);
        }


        [Fact]
        public async Task DeleteUserById_NotFound()
        {
            await _dbContext.Database.EnsureDeletedAsync();
            // Arrange
            var adminUser = SeedAdminUser();
            var userId = 999; // Assuming this user does not exist
            var deleteUserCommand = new DeleteUserApiCommand(new List<int> { userId });
            var client = _factory.CreateClient();

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", GenerateBearerToken(adminUser));

            // Act
            var request = new HttpRequestMessage(HttpMethod.Delete, "/api/v1/iam/user")
            {
                Content = JsonContent.Create(deleteUserCommand), // Ensure correct serialization
            };

            var response = await client.SendAsync(request);
            var result = await response.Content.ReadFromJsonAsync<DeleteUserApiCommandResponse>();

            Assert.NotNull(result);
            Assert.False(result.Success);
            Assert.Equal($"Nem létező felhasználó ID(s): {userId}", result.Message);
        }

        [Fact]
        public async Task DeleteMultipleUsersByIds_Success()
        {
            await _dbContext.Database.EnsureDeletedAsync();
            // Arrange
            var adminGuid = SeedAdminUser();
            var userIds = new List<int> { 1, 2, 3 };
            var client = _factory.CreateClient();
            foreach (var userId in userIds)
            {
                _dbContext.Set<User>().Add(new User
                {
                    Id = userId,
                    Email = $"test{userId}@test.com",
                    Password = PasswordUtility.HashPassword("test"),
                    PhoneNumber = "06111111111",
                    VisibleName = "test"
                });
            }
            await _dbContext.SaveChangesAsync();

            var deleteUserCommand = new DeleteUserApiCommand(userIds);

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", GenerateBearerToken(adminGuid));

            var request = new HttpRequestMessage(HttpMethod.Delete, "/api/v1/iam/user")
            {
                Content = JsonContent.Create(deleteUserCommand), // Ensure correct serialization
            };

            var response = await client.SendAsync(request);

            var result = await response.Content.ReadFromJsonAsync<DeleteUserApiCommandResponse>();

            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Equal("", result.Message);
            var deletedUserIds = _dbContext.Set<User>().Where(x => x.Deleted).Select(x => x.Id);
            foreach (var userId in userIds)
            {
                bool deleted = deletedUserIds.Contains(userId);
                Assert.False(deleted);
            }
        }

        [Fact]
        public async Task DeleteMultipleUsersByIds_NotFound()
        {
            await _dbContext.Database.EnsureDeletedAsync();
            // Arrange
            var adminGuid = SeedAdminUser();
            var userIds = new List<int> { 999, 1000, 1001 }; // Assuming these users do not exist
            var deleteUserCommand = new DeleteUserApiCommand(userIds);
            var client = _factory.CreateClient();

            // Add authorization header
            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", GenerateBearerToken(adminGuid));

            // Act
            var request = new HttpRequestMessage(HttpMethod.Delete, "/api/v1/iam/user")
            {
                Content = JsonContent.Create(deleteUserCommand), // Ensure correct serialization
            };

            var response = await client.SendAsync(request);
            var result = await response.Content.ReadFromJsonAsync<DeleteUserApiCommandResponse>();

            Assert.NotNull(result);
            Assert.False(result.Success);
            Assert.Equal($"Nem létező felhasználó ID(s): {string.Join(",", userIds)}", result.Message);
        }



    }
}
