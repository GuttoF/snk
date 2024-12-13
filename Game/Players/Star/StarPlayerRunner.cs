using Game.Core;
using System.IO;
using System.Text.Json;
using System.Diagnostics;

namespace Game.Players.Star;

public static class StarPlayerRunner
{
    public static void Run()
    {
        const int size = 1;

        var timer = new Stopwatch();
        timer.Start();

        var scores = new List<int>();
        var steps = new List<int>();

        var games = new List<GameState>();
        for (int i = 0; i < size; i++)
        {
            games.Add(new GameState(50, 50, GameMode.Classic));
        }

        Parallel.ForEach(games, game =>
        {
            while (!game.GameOver & !game.Zerou)
            {
                StarPlayer.Decide(game.GetData(), game.SnakeGoTo);
                game.MoveSnake();
            }
        });

        scores = games.ConvertAll(x => x.Score);
        steps = games.ConvertAll(x => x.Steps);

        timer.Stop();

        Console.WriteLine(JsonSerializer.Serialize(scores.Distinct()));
        Console.WriteLine(JsonSerializer.Serialize(steps));

        TimeSpan timeTaken = timer.Elapsed;
        Console.WriteLine(">>>>> Duration: " + timeTaken.ToString(@"mm\:ss"));

        string myfile = @"result.txt";
        using StreamWriter sw = File.CreateText(myfile);
        sw.WriteLine(JsonSerializer.Serialize(scores));
        sw.WriteLine("---------");
        sw.WriteLine(JsonSerializer.Serialize(steps));
    }
}
