using FluentResults;

using Microsoft.EntityFrameworkCore;

using Server.Endpoints.Contracts;
using Server.Persistence;

namespace Server.Endpoints;

public class Statistics : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("statistics", Handler).WithTags("Statistics");
    }

    public static async Task<IResult> Handler(
    StatisticsRepository repo
)
    {
        var totalPlayers = await repo.GetTotalPlayersAsync();
        var totalGames = await repo.GetTotalGamesAsync();

        var result = new
        {
            TotalPlayers = totalPlayers,
            TotalGames = totalGames
        };

        return Results.Ok(result);
    }

}

