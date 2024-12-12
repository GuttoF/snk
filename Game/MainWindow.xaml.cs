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
    private readonly int _rows = 10;
    private readonly int _columns = 10;
    private readonly Image[,] _gridImages;
    private GameState _game;
    private PlayerMode _playerMode = PlayerMode.Human;

    private readonly GameMode _mode = GameMode.FixedSize;

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
			    await Task.Delay(60);
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
