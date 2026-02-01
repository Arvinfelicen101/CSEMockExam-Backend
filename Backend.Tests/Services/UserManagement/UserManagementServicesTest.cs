using Backend.DTOs.Auth;
using Backend.DTOs.UserManagement;
using Backend.Exceptions;
using Backend.Models;
using Backend.Repository.UserManagement;
using Backend.Services.UserManagement;
using Microsoft.Extensions.Caching.Memory;
using Moq;


namespace Backend.Tests.Services.UserManagement;

public class UserManagementServicesTest
{
    private readonly Mock<IUserManagementRepository> _repoMock;
    private readonly Mock<IMemoryCache> _cacheMock;
    private readonly UserManagementServices _service;

    public UserManagementServicesTest()
    {
        _repoMock = new Mock<IUserManagementRepository>();
        _cacheMock = new Mock<IMemoryCache>();

        _service = new UserManagementServices(
            _repoMock.Object,
            _cacheMock.Object

         );
    }

    [Fact]
    public async Task CreateUserAsync_WhenEmailExists_ShouldThrowBadRequest()
    {
        // Arrange
        _repoMock
            .Setup(r => r.FindEmailAsync(It.IsAny<string>()))
            .ReturnsAsync(new Users());

        var dto = new UserManagementCreateDTO
        {
            email = "existing@gmail.com",
            Password = "Password123!"
        };

        // Act & Assert

        await Assert.ThrowsAsync<BadRequestException>(
                () => _service.CreateUserAsync(dto)
            );
        }

    [Fact]
    public async Task GetUsersAsync_WhenNotCached_ShouldFetchFromRepositoryAndCache()
    {
        // Arrange
        var users = new List<Users>
    {
        new Users
        {
            Id = "1",
            Email = "user@gmail.com",
            FirstName = "Jane",
            LastName = "Doe"
        }
    };

        _repoMock
            .Setup(r => r.GetAllAsync())
            .ReturnsAsync(users);

        _cacheMock
            .Setup(c => c.TryGetValue(CacheKeys.UsersAll, out It.Ref<object>.IsAny))
            .Returns(false);

        _cacheMock
            .Setup(c => c.CreateEntry(CacheKeys.UsersAll))
            .Returns(Mock.Of<ICacheEntry>);

        // Act
        var result = await _service.GetUsersAsync();

        // Assert
        Assert.Single(result);
        Assert.Equal("Jane Doe", result[0].FullName);

        _repoMock.Verify(r => r.GetAllAsync(), Times.Once);
        _cacheMock.Verify(c => c.CreateEntry(CacheKeys.UsersAll), Times.Once);
    }


    [Fact]
    public async Task UpdateUsersAsync_WhenUserExists_ShouldUpdateAndClearCache()
    {
        var user = new Users
        {
            Id = "1",
            FirstName = "Old",
            LastName = "Name"
        };

        _repoMock.Setup(r => r.FindByIdAsync("1")).ReturnsAsync(user);

        var dto = new UserManagementUpdateDTO
        {
            FirstName = "New",
            LastName = "Name",
            MiddleName = "M"
        };

        // Act
        await _service.UpdateUserAsync("1", dto);

        // Assert
        Assert.Equal("New", user.FirstName);
        _repoMock.Verify(r => r.UpdateUser(user), Times.Once);
        _cacheMock.Verify(c => c.Remove(CacheKeys.UsersAll), Times.Once);
    }

    [Fact]
    public async Task DeleteUserAsync_WhenUserExists_ShouldDeleteAndClearCache()
    {
        // Arrange
        var user = new Users { Id = "1" };
        _repoMock.Setup(r => r.FindByIdAsync("1")).ReturnsAsync(user);

        // Act
        await _service.DeleteUserAsync("1");

        // Assert
        _repoMock.Verify(r => r.DeleteUser(user), Times.Once);
        _cacheMock.Verify(c => c.Remove(CacheKeys.UsersAll), Times.Once);
    }

}