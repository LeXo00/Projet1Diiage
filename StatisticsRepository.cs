using Microsoft.EntityFrameworkCore;

using Server.Models;

namespace Server.Persistence;

public class StatisticsRepository
{
    private readonly WssDbContext _context;

    public StatisticsRepository(WssDbContext context)
    {
        _context = context;
    }

    public async Task<int> GetTotalPlayersAsync()
    {
        return await _context.Players.CountAsync();
    }

    public async Task<int> GetTotalGamesAsync()
    {
        return await _context.Games.CountAsync();
    }
}

