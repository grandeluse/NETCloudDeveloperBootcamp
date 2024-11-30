using GameStore.Api.Models;

namespace GameStore.Api.Data;

public class GameStoreData
{
    private readonly List<Genre> _genres =
    [
        new(){Id = new Guid("c78cc485-4174-48cd-afa3-06c623075867"), Name = "Fighting"},
        new(){Id = new Guid("fc28eda1-cc34-4d6e-b300-02e52867a492"), Name = "Kids and Family"},
        new(){Id = new Guid("b2fc74da-10bb-4fb2-af32-f21a7f9f071f"), Name = "Racing"},
        new(){Id = new Guid("be68c3d5-5cef-4eb0-ab3e-b3a913a77d9b"), Name = "Roleplaying"},
        new(){Id = new Guid("e759edd3-f83d-4c8e-b110-993e30b1edc5"), Name = "Sports"}
    ];

    private readonly List<Game> _games;

    public GameStoreData()
    {
        _games =
        [
            new Game
            {
                Id = Guid.NewGuid(),
                Name = "Street Fighter II",
                Genre = _genres[0],
                Price = 19.99m,
                ReleaseDate = new DateOnly(1992, 7, 15),
                Description = "Street Fighter II: The World Warrior is a 1991 fighting game produced by Capcom for arcades, and their fourteenth game to use the CP System arcade system board"
            },
            new Game
            {
                Id = Guid.NewGuid(),
                Name = "Final Fantasy XIV",
                Genre = _genres[3],
                Price = 59.99m,
                ReleaseDate = new DateOnly(2010, 9, 30),
                Description = "Final Fantasy XIV is a massively multiplayer online role-playing game (MMORPG) developed and published by Square Enix"
            },
            new Game
            {
                Id = Guid.NewGuid(),
                Name = "FIFA 23",
                Genre = _genres[4],
                Price = 69.99m,
                ReleaseDate = new DateOnly(2022, 9, 27),
                Description = "FIFA 23 is a football video game published by EA Sports. It is the 30th and final installment in the FIFA series that is developed by EA Sports"
            }
        ];
    }

    public IEnumerable<Game> GetGames() => _games;
    
    public Game? GetGame(Guid id) => _games.Find(game => game.Id == id);

    public void AddGame(Game game)
    {
        game.Id = Guid.NewGuid();
        _games.Add(game);
    }

    public void RemoveGame(Guid id)
    {
        _games.RemoveAll(game => game.Id == id);
    }
    
    public IEnumerable<Genre> GetGenres() => _genres;

    public Genre? GetGenre(Guid id) => _genres.Find(genre => genre.Id == id);
}