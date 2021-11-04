#if ZAMPLEZ
namespace Flux
{
  public static partial class Zamplez
  {
    /// <summary>Run the colors zample.</summary>
    public static void RunColors()
    {
      var rgb = Flux.Colors.Rgb.FromRandom();
      //rgb = new Flux.Colors.Rgb(0xF0, 0xC8, 0x0E);
      //rgb = new Flux.Colors.Rgb(0xB4, 0x30, 0xE5);
      //rgb = new Flux.Media.Colors.Rgb(0x0, 0x0, 0x0);
      //rgb = new Flux.Media.Colors.Rgb(0x1, 0x1, 0x1);
      //rgb = new Flux.Media.Colors.Rgb(0xFF, 0xFF, 0xFF);
      //rgb = new Flux.Media.Colors.Rgb(0xFE, 0xFE, 0xFE);

      System.Console.WriteLine($"{rgb}");
      var hue = rgb.GetHue(out var _, out var _, out var r, out var g, out var b, out var chroma);
      rgb.GetSecondaryChromaAndHue(out var chroma2, out var hue2);
      var cmyk = rgb.ToCmyk();
      System.Console.WriteLine($"{cmyk} ({cmyk.ToRgb()})");
      var hsi = rgb.ToHsi();
      System.Console.WriteLine($"{hsi} ({hsi.ToRgb()})");
      var hsl = rgb.ToHsl();
      System.Console.WriteLine($"{hsl} ({hsl.ToRgb()}) ({hsl.ToHsv()})");
      var hsv = rgb.ToHsv();
      System.Console.WriteLine($"{hsv} ({hsv.ToRgb()}) ({hsv.ToHsl()}) ({hsv.ToHwb()})");
      var hwb = rgb.ToHwb();
      System.Console.WriteLine($"{hwb} ({hwb.ToRgb()}) ({hwb.ToHsv()})");

      System.Console.WriteLine($"{rgb.ToStringHtmlHex()} | {(r * 100):N1}%, {(g * 100):N1}%, {(b * 100):N1}% | {hue:N1}, {hue2} | {(chroma * 100):N1}, {(chroma2 * 100):N1} | {(hsv.Value * 100):N1}%, {(hsl.Lightness * 100):N1}%, {(hsi.Intensity * 100):N1}% | Y={rgb.GetLuma601()} | {(hsv.Saturation * 100):N1}%, {(hsl.Saturation * 100):N1}%, {(hsi.Saturation * 100):N1}%");
      System.Console.WriteLine();
    }
  }
}
#endif
