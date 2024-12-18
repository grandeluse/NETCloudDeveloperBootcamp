using GameStore.Api.Data;

namespace GameStore.Api.Features.Games.UpdateGame;

public static class UpdateGameEndpoint
{
    public static void MapUpdateGame(this IEndpointRouteBuilder app)
    {
        // PUT /games/{id}
        app.MapPut("/{id}", (Guid id, UpdateGameDto gameDto, GameStoreData data) =>
        {
            var existingGame = data.GetGame(id);
            if (existingGame is null)
                return Results.NotFound();

            var genre = data.GetGenre(gameDto.GenreId);
            if(genre is null)
                return Results.BadRequest("Invalid Genre id");

            existingGame.Name = gameDto.Name;
            existingGame.Genre = genre;
            existingGame.GenreId = genre.Id;
            existingGame.Price = gameDto.Price;
            existingGame.ReleaseDate = gameDto.ReleaseDate;
            existingGame.Description = gameDto.Description;

            return Results.NoContent();
        }).WithParameterValidation();
    }
}