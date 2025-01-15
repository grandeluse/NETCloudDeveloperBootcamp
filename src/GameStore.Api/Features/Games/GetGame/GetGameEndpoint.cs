using System.Diagnostics;
using GameStore.Api.Data;
using GameStore.Api.Features.Games.Constants;
using GameStore.Api.Models;
using Microsoft.Data.Sqlite;

namespace GameStore.Api.Features.Games.GetGame;

public static class GetGameEndpoint
{
    public static void MapGetGame(this IEndpointRouteBuilder app)
    {
        // GET /games/{id}
        app.MapGet("/{id}", async (Guid id, GameStoreContext dbContext, ILogger<Program> logger) =>
        {
            try
            {
                var game = await FindGameAsync(dbContext, id);

                return game is null
                    ? Results.NotFound()
                    : Results.Ok(
                        new GameDetailsDto(
                            game.Id,
                            game.Name,
                            game.GenreId,
                            game.Price,
                            game.ReleaseDate,
                            game.Description));
            }
            catch (Exception ex)
            {
                var traceId = Activity.Current?.TraceId;
                logger.LogError(
                    ex,
                    "could not process a request on machine {Machine}. TraceId: {TraceId}",
                    Environment.MachineName,
                    traceId);
                return Results.Problem(
                    title: "An error occurred while processing your request",
                    statusCode: StatusCodes.Status500InternalServerError,
                    extensions: new Dictionary<string, object?>
                    {
                        { "traceId", traceId.ToString() }
                    }
                );
            }
        })
        .WithName(EndpointNames.GetGame);
    }

    private static async Task<Game?> FindGameAsync(GameStoreContext dbContext, Guid id)
    {
        throw new SqliteException("The database is not available!", 14);
        return await dbContext.Games.FindAsync(id);
    }
}