using Game.UI;
using Game.Core;
using System.Windows;
using Game.Players.Dummy;
using Game.Players.Star;
using Game.Players.Neural;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Controls;

namespace Game;

public partial class MainWindow
{
    private readonly int _rows = 10;
    private readonly int _columns = 10;
    private readonly Image[,] _gridImages;
    private GameState _game;
    private PlayerMode _playerMode = PlayerMode.Human;

	public MainWindow()
    {
        InitializeComponent();
        _gridImages = SetupGridImages();
        _game = new GameState(_rows, _columns);
    }

	private Image[,] SetupGridImages()
    {
        GameGrid.Rows = _rows;
        GameGrid.Columns = _columns;
        GameGrid.Width = GameGrid.Height * (_columns / (double) _rows);

        var images = new Image[_rows, _columns];
		for (int row = 0; row < _rows; row++)
		{
			for (int column = 0; column < _columns; column++)
			{
                var image = new Image
                {
                    Source = Images.Empty,
                    RenderTransformOrigin = new Point(0.5, 0.5),
                };
                images[row, column] = image;
                GameGrid.Children.Add(image);
			}
		}

        return images;
	}

	private async Task RunGame()
	{
        Draw();
        await DrawCountDown();
        Overlay.Visibility = Visibility.Hidden;

        await GameLoop();

        if (_game.GameOver)
        {
			await DrawGameOver();
        }
        if (_game.Zerou)
        {
	        await DrawZerou();
        }
		_game = new GameState(_rows, _columns);
	}

    private async Task GameLoop()
    {
	    if (_playerMode == PlayerMode.Human)
	    {
	        while (!_game.GameOver & !_game.Zerou)
	        {
	            await Task.Delay(1000);
				// Human async decision
	            _game.MoveSnake();
	            Draw();
	        }
	    }

	    if (_playerMode == PlayerMode.Dummy)
	    {
		    while (!_game.GameOver & !_game.Zerou)
		    {
			    await Task.Delay(100);
				DummyPlayer.Decide(_game.GetData(), _game.SnakeGoTo);
			    _game.MoveSnake();
			    Draw();
		    }
	    }

	    if (_playerMode == PlayerMode.Star)
	    {
		    while (!_game.GameOver & !_game.Zerou)
		    {
			    await Task.Delay(100);
				var path = StarPlayer.Decide(_game.GetData(), _game.SnakeGoTo);
			    _game.MoveSnake();
			    Draw();
				DrawStarPath(path);
		    }
	    }

	    if (_playerMode == PlayerMode.Neural)
	    {
			// double[][] intermediateNeurons =
			// [
			// 	[-681.8215669644367, 182.74898150314266, -42.96751310598677, 95.03080828311272],
			// 	[797.3447287940512, 598.8619585007541, -259.21732028096403, -374.2183274317455],
			// 	[-61.990819127849136, 549.6383478525056, -892.7231209321332, -755.0842639069233],
			// 	[-759.2578948934694, -561.314179051491, 576.5577502691576, 448.0722779829323],
			// 	[6.970614972307203, 346.54419282831986, -941.551384378606, -107.80182106875634],
			// 	[-996.3328357527556, -204.05317459002515, -509.43509079372285, -428.05374048950614],
			// 	[-42.88128149811723, -712.8484163557065, 775.5508668410207, 422.6671800119159],
			// 	[-162.67932747976374, -564.9144705615845, 356.46412285623956, -227.10475727171],
			// ];
			// double[][] outputNeurons =
			// [
			// 	[653.8780469427857, 919.7019122624895, -45.056283863274416, 961.031356292692, 226.92138296115218, 912.7332175726317, -38.54547201603941, -565.4592732848258],
			// 	[770.4337605160504, -136.89203099565782, -480.11074513008793, -183.84893618208855, -584.1556100240723, 106.45730206111443, -908.3961227381827, -810.1231839031931],
			// 	[407.1157976035347, -970.9002779956646, -606.659898438362, 224.93242140701477, 588.84481201527, 740.3926222619812, -387.2817857718169, -259.11768272023323],
			// 	[-680.7406599322883, 727.7909535451688, 765.2114146127458, 338.340365870152, -24.078764148021378, 340.40406631134874, 134.2002164906762, 620.7429448092291],
			// ];
			// _game.NeuralNetwork = new NeuralNetwork(intermediateNeurons, outputNeurons);

		    while (!_game.GameOver & !_game.Zerou)
		    {
			    await Task.Delay(50);
				NeuralPlayer.Decide(_game.GetData(), _game.SnakeGoTo);
			    _game.MoveSnake();
			    Draw();
		    }
	    }
    }

    // EVENTS - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - //

	private async void Window_PreviewKeyDown(object _, KeyEventArgs e)
	{
		if (Overlay.Visibility == Visibility.Visible)
		{
			e.Handled = true;
		}

		if (!_game.IsRunning)
		{
			_playerMode = GetPlayerMode(e);
			
			_game.IsRunning = true;
			await RunGame();
			_game.IsRunning = false;
		}
	}

	private void Window_KeyDown(object _, KeyEventArgs e)
	{
		if (_game.GameOver) return;
		if (_playerMode != PlayerMode.Human) return;

        switch (e.Key)
        {
            case Key.Up:
                _game.Snake.GoTo(Direction.Up); break;
            case Key.Right:
                _game.Snake.GoTo(Direction.Right); break;
            case Key.Down:
                _game.Snake.GoTo(Direction.Down); break;
            case Key.Left:
                _game.Snake.GoTo(Direction.Left); break;
        }
	}

	private static PlayerMode GetPlayerMode(KeyEventArgs e)
	{
		return e.Key switch
		{
			Key.H => PlayerMode.Human,
			Key.D => PlayerMode.Dummy,
			Key.S => PlayerMode.Star,
			Key.N => PlayerMode.Neural,
			_ => PlayerMode.Human
		};
	}

    // DRAW - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - //

    private void Draw()
    {
        DrawGrid();
        DrawSnakeHead();
		ScoreText.Text = $"{_playerMode} | SCORE = {_game.Score} | STEPS = {_game.Steps}";
    }

	private void DrawGrid()
    {
		var opct = 1.0;
		var cells = _game.Snake.CellsPositions.ToList().ConvertAll(x =>
		{
			opct -= 0.007;
			return new { x.Row, x.Column, Opacity = opct };
		});

		for (int row = 0; row < _rows; row++)
		{
			for (int column = 0; column < _columns; column++)
			{
				var cell = _game.Grid.GetCellAt(row, column);
                _gridImages[row, column].Source = cell.ToImage();
                _gridImages[row, column].RenderTransform = Transform.Identity;

				var position = cells.FirstOrDefault(x => x.Row == row && x.Column == column);
                _gridImages[row, column].Opacity = position?.Opacity ?? 1.0;
			}
		}
	}

	private void DrawSnakeHead()
	{
		var headPosition = _game.Snake.GetHeadPosition();
        var image = _gridImages[headPosition.Row, headPosition.Column];
        image.Source = Images.Head;

        var rotation = _game.Snake.HeadDirection.ToRotation();
        image.RenderTransform = new RotateTransform(rotation);
	}

	private void DrawStarPath(string path)
	{
		var currentPosition = _game.Snake.GetHeadPosition();
		var foodPosition = _game.Grid.GetFoodPosition();

		foreach (var dir in path[1..])
		{
			var direction = dir.ToDirection();
			var nextPosition = currentPosition.MoveTo(direction);

			if (nextPosition == foodPosition) break;

			var image = _gridImages[nextPosition.Row, nextPosition.Column];
			image.Source = Images.Path;

			var rotation = direction.ToRotation();
			image.RenderTransform = new RotateTransform(rotation);

			currentPosition = nextPosition;
		}
	}

	private async Task DrawCountDown()
    {
        for (int i = 3; i >= 1; i--)
        {
            OverlayText.Text = i.ToString();
            await Task.Delay(500);
        }
    }

	private async Task DrawGameOver()
	{
        await DrawDeadSnake();
		await Task.Delay(1000);
		Overlay.Visibility = Visibility.Visible;
        OverlayText.Text = "Human | Dummy | Neural | Star";
	}

	private async Task DrawZerou()
	{
		await Task.Delay(1000);
		Overlay.Visibility = Visibility.Visible;
		OverlayText.Text = "🐍 ZEROU 🐍";
		await Task.Delay(2000);
		OverlayText.Text = "Human | Dummy | Neural | Star";
	}

    private async Task DrawDeadSnake()
    {
        var positions = _game.Snake.CellsPositions.ToList();

        for (int i = 0; i < positions.Count; i++)
        {
            var position = positions[i];
            var source = (i == 0) ? Images.DeadHead : Images.DeadBody;
            _gridImages[position.Row, position.Column].Source = source;
            await Task.Delay(50);
        }
    }
}
