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
            games.Add(new GameState(10, 10, GameMode.FixedSize));
        }

        Parallel.ForEach(games, game =>
        {
            double[][] intermediateNeurons = [
                [-56.933482202470486, 308.8554309279973, -761.4885668332234, -289.4644747579566],
                [150.77592609213684, 300.0518378629072, -922.3477586444404, 148.3763411042844],
                [-837.9899704115165, 952.7711354604207, 802.0985806353433, 182.27643708275787],
                [-512.9015610712986, -943.8229581070236, -554.546756413125, -275.93056996744906],
                [-703.7404224679387, -794.9871977046108, 636.9550430677641, 306.00408283781894],
                [536.7381746683584, -256.0405569633499, -257.1893928844404, -407.48162330693845],
                [146.4082424916985, 127.04313157058368, -399.2910954371191, -977.9968580498935],
                [37.80363351985716, -294.0642963122515, -650.0602065033172, 675.6286804966876]
            ];
            double[][] outputNeurons = [
                [-849.1382389572366, -36.48061746194162, 836.0697371222427, -171.67919095893149, -51.99844417518261, 910.5816854888285, -520.7383067843199, 248.67720998556842],
                [749.5668252346213, -949.7220334290886, -152.4750463429532, 561.1923980754602, -928.3592904442162, -136.92260429431724, 275.7029168353463, -465.83870196666805],
                [816.5997920694135, -301.3471895405386, 6.95492391169239, 980.5427803158384, -15.88608952789832, 223.70170873635698, -210.37578160092482, 811.5411466101598],
                [64.81885748715422, -819.9728396069705, -386.6213199551205, 890.5773489267708, -736.3196014848816, 744.9908800347914, -146.28179386178397, 697.556524052623]
            ];

            var neural = new NeuralNetwork(intermediateNeurons, outputNeurons);

            while (!game.GameOver & !game.Zerou & game.Score < 97 & game.Steps < 1500)
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
