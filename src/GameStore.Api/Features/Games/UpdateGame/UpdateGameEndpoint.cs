using GameStore.Api.Data;

namespace GameStore.Api.Features.Games.UpdateGame;

public static class UpdateGameEndpoint
{
    public static void MapUpdateGame(this IEndpointRouteBuilder app)
    {
        // PUT /games/{id}
        app.MapPut("/{id}", (Guid id, UpdateGameDto gameDto, GameStoreContext context) =>
        {
            var existingGame = context.Games.Find(id);
            if (existingGame is null)
                return Results.NotFound();

            existingGame.Name = gameDto.Name;
            existingGame.GenreId = gameDto.GenreId;
            existingGame.Price = gameDto.Price;
            existingGame.ReleaseDate = gameDto.ReleaseDate;
            existingGame.Description = gameDto.Description;

            context.SaveChanges();

            return Results.NoContent();
        }).WithParameterValidation();
    }
}