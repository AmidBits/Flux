namespace Flux.Wpf
{
  public static partial class Media
  {
    /// <summary>Returns a random Windows.UI.Color, optionally randomizeAlpha channel as well.</summary>
    public static System.Windows.Media.Color RandomColor(bool randomizeAlpha = false)
    {
      var bytes = Flux.Random.NumberGenerator.Crypto.GetRandomBytes(4);

      if (!randomizeAlpha)
      {
        bytes[0] = 0xFF;
      }

      return System.Windows.Media.Color.FromArgb(bytes[0], bytes[1], bytes[2], bytes[3]);
    }
    /// <summary>Returns a random Windows.UI.Xaml.Media.SolidColorBrush, optionally randomizeAlpha channel as well.</summary>
    public static System.Windows.Media.SolidColorBrush RandomSolidColorBrush(bool randomizeAlpha = false) => new System.Windows.Media.SolidColorBrush(RandomColor(randomizeAlpha));
  }
}
