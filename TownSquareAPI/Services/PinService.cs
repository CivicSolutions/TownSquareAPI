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

        public List<PinData> GetPins()
        {
            return _dbContext.Pins.ToList();
        }

        public void InsertPin(PinData pin)
        {
            _dbContext.Pins.Add(pin);
            _dbContext.SaveChanges();
        }
    }
}