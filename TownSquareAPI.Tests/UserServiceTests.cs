using Microsoft.EntityFrameworkCore;
using TownSquareAPI.Services;
using TownSquareAPI.Data;
using TownSquareAPI.Models;
using Xunit;

public class UserServiceTests : IDisposable
{
    private readonly UserService _userService;
    private readonly ApplicationDbContext _dbContext;

    public UserServiceTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        _dbContext = new ApplicationDbContext(options);
        _userService = new UserService(_dbContext);
    }
    
    public void Dispose()
    {
        _dbContext.Database.EnsureDeleted();
        _dbContext.Dispose();
    }

    [Fact]
    public void GetUserById_ShouldReturnUser_WhenUserExists()
    {
        // Arrange
        var userId = 1;
        var user = new User
        {
            id = userId,
            name = "Test User",
            email = "testuser@example.com",
            password = "password123",
            bio = "This is a bio."
        };
        _dbContext.Users.Add(user);
        _dbContext.SaveChanges();

        // Act
        var result = _userService.GetUserById(userId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(userId, result.id);
    }

    [Fact]
    public void GetUserById_ShouldReturnNull_WhenUserDoesNotExist()
    {
        // Arrange
        _dbContext.Database.EnsureDeleted();
        _dbContext.Database.EnsureCreated();
        var userId = 1;

        // Act
        var result = _userService.GetUserById(userId);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void CreateUser_ShouldAddUserToDatabase()
    {
        // Arrange
        var user = new User 
        { 
            name = "New User", 
            email = "newuser@example.com", 
            password = "password123", 
            bio = "This is a bio." 
        };

        // Act
        _userService.CreateUser(user);

        // Assert
        var createdUser = _dbContext.Users.FirstOrDefault(u => u.name == "New User");
        Assert.NotNull(createdUser);
    }

    [Fact]
    public void DeleteUser_ShouldRemoveUserFromDatabase()
    {
        // Arrange
        var userId = 1;
        var user = new User 
        { 
            id = userId, 
            name = "Test User", 
            email = "testuser@example.com", 
            password = "password123", 
            bio = "This is a bio." 
        };
        _dbContext.Users.Add(user);
        _dbContext.SaveChanges();

        // Act
        _userService.DeleteUser(userId);

        // Assert
        var deletedUser = _dbContext.Users.Find(userId);
        Assert.Null(deletedUser);
    }

    [Fact]
    public void UpdateUser_ShouldModifyUserInDatabase()
    {
        // Arrange
        var userId = 1;
        var newUsername = "Updated User";
        var newBio = "Updated Bio";
        var user = new User
        {
            id = userId,
            name = "Test User",
            email = "testuser@example.com",
            password = "password123",
            bio = "This is a bio."
        };
        _dbContext.Users.Add(user);
        _dbContext.SaveChanges();

        // Act
        _userService.UpdateUser(userId, newUsername, newBio);

        // Assert
        var updatedUser = _dbContext.Users.Find(userId);
        Assert.Equal(newUsername, updatedUser.name);
        Assert.Equal(newBio, updatedUser.bio);
    }

    [Fact]
    public void GetAllUsers_ShouldReturnAllUsers()
    {
        // Arrange
        var users = new List<User>
        {
            new User
            {
                name = "User1",
                email = "user1@example.com",
                password = "password123",
                bio = "Bio for User1"
            },
            new User
            {
                name = "User2",
                email = "user2@example.com",
                password = "password123",
                bio = "Bio for User2"
            }
        };
        _dbContext.Users.AddRange(users);
        _dbContext.SaveChanges();

        // Act
        var result = _userService.GetAllUsers();

        // Assert
        Assert.Equal(users.Count, result.Count);
    }

    [Fact]
    public void GetUserByEmail_ShouldReturnUser_WhenUserExists()
    {
        // Arrange
        var email = "test@example.com";
        var user = new User
        {
            email = email,
            name = "Test User",
            password = "password123",
            bio = "This is a bio."
        };
        _dbContext.Users.Add(user);
        _dbContext.SaveChanges();

        // Act
        var result = _userService.GetUserByEmail(email);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(email, result.email);
    }

    [Fact]
    public void GetUserByEmail_ShouldReturnNull_WhenUserDoesNotExist()
    {
        // Arrange
        _dbContext.Database.EnsureDeleted();
        _dbContext.Database.EnsureCreated();
        var email = "nonexistent@example.com";

        // Act
        var result = _userService.GetUserByEmail(email);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void CreateUser_ShouldThrowException_WhenUserIsNull()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => _userService.CreateUser(null));
    }

    [Fact]
    public void DeleteUser_ShouldThrowException_WhenUserDoesNotExist()
    {
        // Arrange
        var userId = 10;

        // Act & Assert
        Assert.Throws<ArgumentException>(() => _userService.DeleteUser(userId));
    }
}