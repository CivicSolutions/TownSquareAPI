using Microsoft.EntityFrameworkCore;
using TownSquareAPI.Data;
using TownSquareAPI.Models;

namespace TownSquareAPI.Services
{
    public class UserService
    {
        private readonly ApplicationDbContext _dbContext;

        public UserService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void CreateUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }

        public User? GetUserById(int userId)
        {
            return _dbContext.Users.FirstOrDefault(u => u.id == userId);
        }

        public User? GetUserByEmailAndPassword(string email, string password)
        {
            return _dbContext.Users.FirstOrDefault(u => u.email == email && u.password == password);
        }

        public int GetUserIdByEmail(string email)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.email == email);
            return user?.id ?? -1;
        }

        public void DeleteUserById(int userId)
        {
            var user = _dbContext.Users.Find(userId);
            if (user != null)
            {
                _dbContext.Users.Remove(user);
                _dbContext.SaveChanges();
            }
        }


        public void UpdateUser(int userId, string newUsername, string newBio)
        {
            var user = _dbContext.Users.Find(userId);
            if (user != null)
            {
                user.name = newUsername;
                user.bio = newBio;
                _dbContext.SaveChanges();
            }
        }

        public void DeleteUser(int userId)
        {
            var user = _dbContext.Users.Find(userId);
            if (user != null)
            {
                _dbContext.Users.Remove(user);
                _dbContext.SaveChanges();
            }
            else
            {
                throw new ArgumentException("User not found");
            }
        }

        public List<User> GetAllUsers()
        {
            return _dbContext.Users.ToList();
        }

        public User? GetUserByEmail(string email)
        {
            return _dbContext.Users.FirstOrDefault(u => u.email == email);
        }
    }
}