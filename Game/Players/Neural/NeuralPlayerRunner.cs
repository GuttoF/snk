using Game.Core;
using System.IO;
using System.Text.Json;
using System.Diagnostics;

namespace Game.Players.Neural;

public static class NeuralPlayerRunner
{
    public static void Run()
    {
        const int size = 1_000;

        var timer = new Stopwatch();
        timer.Start();

        var scores = new List<int>();
        var steps = new List<int>();

        var games = new List<GameState>();
        for (int i = 0; i < size; i++)
        {
            games.Add(new GameState(10, 10, GameMode.Classic));
        }

        Parallel.ForEach(games, game =>
        {
            double[][] intermediateNeurons =
            [
                [67.54288818999294, -548.1159192783662, -855.339826182369, -528.9363177984254],
                [887.186117968668, -314.0492025483969, -702.1564386320085, 289.9927501163297],
                [-625.6187526805905, -898.0586544044611, -563.2173702419375, 440.52402377886324],
                [-743.9970521267126, 690.189131209379, -671.7564095537721, -377.6354627482019],
                [108.64483650149873, -726.9321109948754, 569.1573187074075, -292.6640763135897],
                [-82.50682343623248, -194.44043391446382, -915.3888465785485, -575.9550995624913],
                [422.09688066339277, 149.0630581188741, -788.1989987102176, -744.4681708110652],
                [97.06869900806623, 743.3822286286902, 662.8684553649894, 276.60152310758053]
            ];
            double[][] outputNeurons =
            [
                [-503.57092135828066, 909.5157916338417, -717.2236198142683, -793.1364783499062, -672.5053182533098, 923.229028471455, -38.16191087976949, -39.23703663262802],
                [64.60935786973187, 485.4174010121344, 864.2836694278565, -710.8731510232558, 571.9266731496148, 462.72970231238355, -833.7117016719351, -827.7753417442815],
                [-558.8512643647343, -610.5891931722178, 14.918802514423533, -683.9816610408416, 517.9277568339148, 16.22444398184291, 918.4105172529135, 276.35366103940487],
                [-599.4933835593446, 646.3659472914239, -606.9825809223972, 36.27473422909111, -863.7047052110398, -437.7301890106711, 890.1909002417087, -248.99457325010064]
            ];

            var neural = new NeuralNetwork(intermediateNeurons, outputNeurons);

            while (!game.GameOver & !game.Zerou & game.Steps < 2500)
            {
                NeuralPlayer.Decide(game.GetData(), game.SnakeGoTo, neural);
                game.MoveSnake();
            }
        });

        scores = games.ConvertAll(x => x.Score);
        steps = games.ConvertAll(x => x.Steps);

        timer.Stop();

        Console.WriteLine(JsonSerializer.Serialize(scores));
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
