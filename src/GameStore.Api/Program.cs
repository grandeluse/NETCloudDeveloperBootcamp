using System.Diagnostics;
using GameStore.Api.Data;
using GameStore.Api.Features.Games;
using GameStore.Api.Features.Genres;

var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration.GetConnectionString("GameStore");
builder.Services.AddSqlite<GameStoreContext>(connString);

var app = builder.Build();

app.MapGames();
app.MapGenres();

app.Use(async(context, next) =>
{
    var stopwatch = new Stopwatch();

    try
    {
        stopwatch.Start();

        await next(context);
    }
    finally
    {
        stopwatch.Stop();
        var elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
        app.Logger.LogInformation("{RequestMethod} {RequestPath} completed with {Status} in {ElapsedMilliseconds} ms.",
            context.Request.Method,
            context.Request.Path,
            context.Response.StatusCode,
            elapsedMilliseconds
        );
    }
});



await app.InitializeDbAsync();

app.Run();




