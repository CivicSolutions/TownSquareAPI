//using Microsoft.EntityFrameworkCore;
//using TownSquareAPI.Data;
//using TownSquareAPI.Models;
//using TownSquareAPI.Services;

//public class UserServiceTests : IDisposable
//{
//    private readonly UserService _userService;
//    private readonly ApplicationDbContext _dbContext;

//    public UserServiceTests()
//    {
//        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
//            .UseInMemoryDatabase(databaseName: "TestDatabase")
//            .Options;
//        _dbContext = new ApplicationDbContext(options);
//        _userService = new UserService(_dbContext);
//    }

//    public void Dispose()
//    {
//        _dbContext.Database.EnsureDeleted();
//        _dbContext.Dispose();
//    }

//    [Fact]
//    public void GetUserById_ShouldReturnUser_WhenUserExists()
//    {
//        // Arrange
//        var userId = 1;
//        var user = new User
//        {
//            Id = userId,
//            Name = "Test User",
//            Email = "testuser@example.com",
//            Password = "password123",
//            Description = "This is a descriptiono."
//        };
//        _dbContext.User.Add(user);
//        _dbContext.SaveChanges();

//        // Act
//        var result = _userService.GetUserById(userId);

//        // Assert
//        Assert.NotNull(result);
//        Assert.Equal(userId, result.Id);
//    }

//    [Fact]
//    public void GetUserById_ShouldReturnNull_WhenUserDoesNotExist()
//    {
//        // Arrange
//        _dbContext.Database.EnsureDeleted();
//        _dbContext.Database.EnsureCreated();
//        var userId = 1;

//        // Act
//        var result = _userService.GetUserById(userId);

//        // Assert
//        Assert.Null(result);
//    }

//    [Fact]
//    public void CreateUser_ShouldAddUserToDatabase()
//    {
//        // Arrange
//        var user = new User
//        {
//            Name = "New User",
//            Email = "newuser@example.com",
//            Password = "password123",
//            Description = "This is a description."
//        };

//        // Act
//        _userService.CreateUser(user);

//        // Assert
//        var createdUser = _dbContext.User.FirstOrDefault(u => u.Name == "New User");
//        Assert.NotNull(createdUser);
//    }

//    [Fact]
//    public void DeleteUser_ShouldRemoveUserFromDatabase()
//    {
//        // Arrange
//        var userId = 1;
//        var user = new User
//        {
//            Id = userId,
//            Name = "Test User",
//            Email = "testuser@example.com",
//            Password = "password123",
//            Description = "This is a description."
//        };
//        _dbContext.User.Add(user);
//        _dbContext.SaveChanges();

//        // Act
//        _userService.DeleteUser(userId);

//        // Assert
//        var deletedUser = _dbContext.User.Find(userId);
//        Assert.Null(deletedUser);
//    }

//    [Fact]
//    public void UpdateUser_ShouldModifyUserInDatabase()
//    {
//        // Arrange
//        var userId = 1;
//        var newUsername = "Updated User";
//        var newDescription = "Updated Description";
//        var user = new User
//        {
//            Id = userId,
//            Name = "Test User",
//            Email = "testuser@example.com",
//            Password = "password123",
//            Description = "This is a description."
//        };
//        _dbContext.User.Add(user);
//        _dbContext.SaveChanges();

//        // Act
//        _userService.UpdateUser(userId, newUsername, newDescription);

//        // Assert
//        var updatedUser = _dbContext.User.Find(userId);
//        Assert.Equal(newUsername, updatedUser.Name);
//        Assert.Equal(newDescription, updatedUser.Description);
//    }

//    [Fact]
//    public void GetAllUsers_ShouldReturnAllUsers()
//    {
//        // Arrange
//        var users = new List<User>
//        {
//            new User
//            {
//                Name = "User1",
//                Email = "user1@example.com",
//                Password = "password123",
//                Description = "Description for User1"
//            },
//            new User
//            {
//                Name = "User2",
//                Email = "user2@example.com",
//                Password = "password123",
//                Description = "Description for User2"
//            }
//        };
//        _dbContext.User.AddRange(users);
//        _dbContext.SaveChanges();

//        // Act
//        var result = _userService.GetAllUsers();

//        // Assert
//        Assert.Equal(users.Count, result.Count);
//    }

//    [Fact]
//    public void GetUserByEmail_ShouldReturnUser_WhenUserExists()
//    {
//        // Arrange
//        var email = "test@example.com";
//        var user = new User
//        {
//            Email = email,
//            Name = "Test User",
//            Password = "password123",
//            Description = "This is a description."
//        };
//        _dbContext.User.Add(user);
//        _dbContext.SaveChanges();

//        // Act
//        var result = _userService.GetUserByEmail(email);

//        // Assert
//        Assert.NotNull(result);
//        Assert.Equal(email, result.Email);
//    }

//    [Fact]
//    public void GetUserByEmail_ShouldReturnNull_WhenUserDoesNotExist()
//    {
//        // Arrange
//        _dbContext.Database.EnsureDeleted();
//        _dbContext.Database.EnsureCreated();
//        var email = "nonexistent@example.com";

//        // Act
//        var result = _userService.GetUserByEmail(email);

//        // Assert
//        Assert.Null(result);
//    }

//    [Fact]
//    public void CreateUser_ShouldThrowException_WhenUserIsNull()
//    {
//        // Act & Assert
//        Assert.Throws<ArgumentNullException>(() => _userService.CreateUser(null));
//    }

//    [Fact]
//    public void DeleteUser_ShouldThrowException_WhenUserDoesNotExist()
//    {
//        // Arrange
//        var userId = 10;

//        // Act & Assert
//        Assert.Throws<ArgumentException>(() => _userService.DeleteUser(userId));
//    }
//}