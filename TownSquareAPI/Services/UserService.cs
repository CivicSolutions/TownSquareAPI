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

            _dbContext.User.Add(user);
            _dbContext.SaveChanges();
        }

        public User? GetUserById(int userId)
        {
            return _dbContext.User.FirstOrDefault(u => u.Id == userId);
        }

        public User? GetUserByEmailAndPassword(string email, string password)
        {
            return _dbContext.User.FirstOrDefault(u => u.Email == email && u.Password == password);
        }

        public int GetUserIdByEmail(string email)
        {
            var user = _dbContext.User.FirstOrDefault(u => u.Email == email);
            return user?.Id ?? -1;
        }

        public void DeleteUserById(int userId)
        {
            var user = _dbContext.User.Find(userId);
            if (user != null)
            {
                _dbContext.User.Remove(user);
                _dbContext.SaveChanges();
            }
        }


        public void UpdateUser(int userId, string newUsername, string newDescription)
        {
            var user = _dbContext.User.Find(userId);
            if (user != null)
            {
                user.Name = newUsername;
                user.Description = newDescription;
                _dbContext.SaveChanges();
            }
        }

        public void DeleteUser(int userId)
        {
            var user = _dbContext.User.Find(userId);
            if (user != null)
            {
                _dbContext.User.Remove(user);
                _dbContext.SaveChanges();
            }
            else
            {
                throw new ArgumentException("User not found");
            }
        }

        public List<User> GetAllUsers()
        {
            return _dbContext.User.ToList();
        }

        public User? GetUserByEmail(string email)
        {
            return _dbContext.User.FirstOrDefault(u => u.Email == email);
        }
    }
}