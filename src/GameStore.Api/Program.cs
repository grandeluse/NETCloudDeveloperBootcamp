using GameStore.Api.Data;
using GameStore.Api.Features.Games;
using GameStore.Api.Features.Genres;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<GameStoreData>();

var app = builder.Build();

app.MapGames();
app.MapGenres();

app.Run();




