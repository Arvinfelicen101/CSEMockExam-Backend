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

}