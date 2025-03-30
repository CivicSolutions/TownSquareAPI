using Microsoft.EntityFrameworkCore;
using TownSquareAPI.Data;
using TownSquareAPI.Models;

namespace TownSquareAPI.Services;

public class PinService
{
    private readonly ApplicationDbContext _dbContext;

    public PinService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Pin>> GetAll(CancellationToken cancellationToken)
    {
        return await _dbContext.Pin.ToListAsync(cancellationToken);
    }

    public async Task<Pin?> GetById(int id, CancellationToken cancellationToken)
    {
        return await _dbContext.Pin.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<Pin> Create(Pin pin, CancellationToken cancellationToken)
    {
        _dbContext.Pin.Add(pin);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return pin;
    }

    public async Task<Pin?> Update(int pinId, Pin pin, CancellationToken cancellationToken)
    {
        Pin? pinToUpdate = await _dbContext.Pin.FirstOrDefaultAsync(p => p.Id == pinId, cancellationToken);

        if (pinToUpdate == null)
        {
            return null;
        }

        _dbContext.Pin.Update(pin);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return pin;
    }

    public async Task<bool> Delete(int pinId, CancellationToken cancellationToken)
    {
        var pin = await _dbContext.Pin.FirstOrDefaultAsync(p => p.Id == pinId, cancellationToken);

        if (pin == null)
        {
            return false;
        }

        _dbContext.Pin.Remove(pin);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }
}