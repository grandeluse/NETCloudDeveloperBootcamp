using System.ComponentModel.DataAnnotations;
using GameStore.Api.Models;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

const string GetGameEndpointName = "GetGame";

List<Genre> genres =
[
    new(){Id = new Guid("c78cc485-4174-48cd-afa3-06c623075867"), Name = "Fighting"},
    new(){Id = new Guid("fc28eda1-cc34-4d6e-b300-02e52867a492"), Name = "Kids and Family"},
    new(){Id = new Guid("b2fc74da-10bb-4fb2-af32-f21a7f9f071f"), Name = "Racing"},
    new(){Id = new Guid("be68c3d5-5cef-4eb0-ab3e-b3a913a77d9b"), Name = "Roleplaying"},
    new(){Id = new Guid("e759edd3-f83d-4c8e-b110-993e30b1edc5"), Name = "Sports"}
];

List<Game> games =
[
    new()
    {
        Id = Guid.NewGuid(),
        Name = "Street Fighter II",
        Genre = genres[0],
        Price = 19.99m,
        ReleaseDate = new DateOnly(1992, 7, 15),
        Description = "Street Fighter II: The World Warrior is a 1991 fighting game produced by Capcom for arcades, and their fourteenth game to use the CP System arcade system board"
    },
    new()
    {
        Id = Guid.NewGuid(),
        Name = "Final Fantasy XIV",
        Genre = genres[3],
        Price = 59.99m,
        ReleaseDate = new DateOnly(2010, 9, 30),
        Description = "Final Fantasy XIV is a massively multiplayer online role-playing game (MMORPG) developed and published by Square Enix"
    },
    new()
    {
        Id = Guid.NewGuid(),
        Name = "FIFA 23",
        Genre = genres[4],
        Price = 69.99m,
        ReleaseDate = new DateOnly(2022, 9, 27),
        Description = "FIFA 23 is a football video game published by EA Sports. It is the 30th and final installment in the FIFA series that is developed by EA Sports"
    }
];

// GET /games
app.MapGet("/games", () => games.Select(game => new GameSummaryDto(
    game.Id, 
    game.Name, 
    game.Genre.Name, 
    game.Price, 
    game.ReleaseDate)));

// GET /games/{id}
app.MapGet("/games/{id}", (Guid id) =>
{
    Game? game = games.Find(game => game.Id == id);
    return game is null ? Results.NotFound() : Results.Ok(
        new GameDetailsDto(
            game.Id, 
            game.Name, 
            game.Genre.Id, 
            game.Price, 
            game.ReleaseDate, 
            game.Description));
}).WithName(GetGameEndpointName);

// POST /games
app.MapPost("/games", (CreateGameDto gameDto) =>
{
    var genre = genres.Find(genre => genre.Id == gameDto.GenreId);
    if(genre is null)
        return Results.BadRequest("Invalid Genre id");

    var game = new Game()
    {
        Id = Guid.NewGuid(),
        Name = gameDto.Name,
        Genre = genre,
        Price = gameDto.Price,
        ReleaseDate = gameDto.ReleaseDate,
        Description = gameDto.Description
    };
    
    games.Add(game);
    
    return Results.CreatedAtRoute(
        GetGameEndpointName, 
        new { id = game.Id }, 
        new GameDetailsDto(
            game.Id,
            game.Name,
            game.Genre.Id,
            game.Price,game.ReleaseDate,
            game.Description));
}).WithParameterValidation();

// PUT /games/{id}
app.MapPut("/games/{id}", (Guid id, Game updatedGame) =>
{
    var existingGame = games.Find(game => game.Id == id);
    if (existingGame is null)
        return Results.NotFound();

    existingGame.Name = updatedGame.Name;
    existingGame.Genre = updatedGame.Genre;
    existingGame.Price = updatedGame.Price;
    existingGame.ReleaseDate = updatedGame.ReleaseDate;

    return Results.NoContent();
}).WithParameterValidation();

// DELETE /games/{id}
app.MapDelete("/games/{id}", (Guid id) =>
{
    games.RemoveAll(game => game.Id == id);
    return Results.NoContent();
});

// GET /genres
app.MapGet("/genres", () => 
    genres.Select(genre => new GenreDto(genre.Id, genre.Name)));

app.Run();

public record GameDetailsDto(
    Guid Id, 
    string Name, 
    Guid GenreId, 
    decimal Price, 
    DateOnly ReleaseDate, 
    string Description);

public record GameSummaryDto(
    Guid Id,
    string Name,
    string Genre,
    decimal Price,
    DateOnly ReleaseDate);

public record CreateGameDto(
    [Required] [StringLength(50)] string Name,
    Guid GenreId,
    [Range(1, 100)] decimal Price,
    DateOnly ReleaseDate,
    [Required] [StringLength(500)] string Description);

public record GenreDto(Guid Id, string Name);

