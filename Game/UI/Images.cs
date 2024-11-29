using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Game.UI;

public static class Images
{
    public static readonly ImageSource Empty = LoadPng(nameof(Empty));
    public static readonly ImageSource Food = LoadPng(nameof(Food));
    public static readonly ImageSource Head = LoadPng(nameof(Head));
    public static readonly ImageSource Body = LoadPng(nameof(Body));
    public static readonly ImageSource DeadHead = LoadPng(nameof(DeadHead));
    public static readonly ImageSource DeadBody = LoadPng(nameof(DeadBody));

	private static BitmapImage LoadPng(string fileName)
	{
		return new BitmapImage(new Uri($"Assets/{fileName}.png", UriKind.Relative));
	}
}
