using Game.UI;
using Game.Core;
using System.Windows;
using Game.Players.Star;
using Game.Players.Dummy;
using Game.Players.Neural;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Controls;

namespace Game;

public partial class MainWindow
{
    private readonly int _rows = 20;
    private readonly int _columns = 20;
    private readonly Image[,] _gridImages;
    private GameState _game;
    private PlayerMode _playerMode = PlayerMode.Human;

    private readonly GameMode _mode = GameMode.Classic;

	public MainWindow()
    {
        InitializeComponent();
        _gridImages = SetupGridImages();
        _game = new GameState(_rows, _columns, _mode);
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
		_game = new GameState(_rows, _columns, _mode);
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
			    await Task.Delay(60);
				DummyPlayer.Decide(_game.GetData(), _game.SnakeGoTo);
			    _game.MoveSnake();
			    Draw();
		    }
	    }

	    if (_playerMode == PlayerMode.Neural)
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

		    while (!_game.GameOver & !_game.Zerou)
		    {
			    await Task.Delay(60);
				NeuralPlayer.Decide(_game.GetData(), _game.SnakeGoTo, neural);
			    _game.MoveSnake();
			    Draw();
		    }
	    }

	    if (_playerMode == PlayerMode.Star)
	    {
		    while (!_game.GameOver & !_game.Zerou)
		    {
			    await Task.Delay(1);
				var path = StarPlayer.Decide(_game.GetData(), _game.SnakeGoTo);
			    _game.MoveSnake();
			    Draw();
				DrawStarPath(path);
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
		var tax = 0.5 / (_rows * _columns);
		var opct = 1.0;
		var cells = _game.Snake.CellsPositions.ToList().ConvertAll(x =>
		{
			opct -= tax;
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
