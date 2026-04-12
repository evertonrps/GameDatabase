using GameDatabase.Data.Context;
using GameDatabase.Domain.AggregatesModel.GameAggregate;
using GameDatabase.Domain.AggregatesModel.GameAggregate.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GameDatabase.Data.Repository;

public class DeveloperRepository : Repository<Developer>, IDeveloperRepository
{
    private readonly GameDatabaseContext _context;

    public DeveloperRepository(GameDatabaseContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Developer?> GetByName(string name)
    {
        return await _context.Developers
            .FirstOrDefaultAsync(d => d.Name.ToLower() == name.ToLower());
    }
}