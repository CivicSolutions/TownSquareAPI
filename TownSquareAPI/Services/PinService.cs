using TownSquareAPI.Data;
using TownSquareAPI.Models;

namespace TownSquareAPI.Services
{
    public class PinService
    {
        private readonly ApplicationDbContext _dbContext;

        public PinService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Pin> GetPins()
        {
            return _dbContext.Pin.ToList();
        }

        public void InsertPin(Pin pin)
        {
            _dbContext.Pin.Add(pin);
            _dbContext.SaveChanges();
        }
    }
}