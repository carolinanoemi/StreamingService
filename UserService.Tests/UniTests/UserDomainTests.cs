using FluentAssertions;
using Xunit;
using UserService.Domain.Entities;

public class UserDomainTests
{
    [Fact]
    public void CreateUser_WithInvalidEmail_ShouldThrow()
    {
        // Arrange + Act
        var act = () => new User("Caro", "not-an-email", "Password123!");

        // Assert
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void ChangeEmail_ShouldUpdateEmail()
    {
        // Arrange
        var u = new User("Caro", "caro@x.com", "Password123!");

        // Act
        u.ChangeEmail("new@x.com");

        // Assert
        u.Email.Should().Be("new@x.com");
    }

    [Fact]
    public void Deactivate_ShouldSetIsActiveFalse()
    {
        // Arrange
        var u = new User("Caro", "caro@x.com", "Password123!");

        // Act
        u.Deactivate();

        // Assert
        u.IsActive.Should().BeFalse();
    }
}
