#if ZAMPLEZ
namespace Flux
{
  public static partial class Zamplez
  {
    /// <summary>Run the colors zample.</summary>
    public static void RunColors()
    {
      System.Console.WriteLine(nameof(RunColors));
      System.Console.WriteLine();

      var argb = Flux.Colors.Argb.FromRandom();
      //rgb = new Flux.Colors.Rgb(0xF0, 0xC8, 0x0E);
      //rgb = new Flux.Colors.Rgb(0xB4, 0x30, 0xE5);
      //rgb = new Flux.Media.Colors.Rgb(0x0, 0x0, 0x0);
      //rgb = new Flux.Media.Colors.Rgb(0x1, 0x1, 0x1);
      //rgb = new Flux.Media.Colors.Rgb(0xFF, 0xFF, 0xFF);
      //rgb = new Flux.Media.Colors.Rgb(0xFE, 0xFE, 0xFE);

      System.Console.WriteLine($"{argb}");
      var hue = argb.RGB.GetHue(out var _, out var _, out var r, out var g, out var b, out var chroma);
      argb.RGB.GetSecondaryChromaAndHue(out var chroma2, out var hue2);
      var cmyk = argb.ToAcmyk();
      System.Console.WriteLine($"{cmyk} ({cmyk.ToArgb()})");
      var ahsi = argb.ToAhsi();
      System.Console.WriteLine($"{ahsi} ({ahsi.ToArgb()})");
      var ahsl = argb.ToAhsl();
      System.Console.WriteLine($"{ahsl} ({ahsl.ToArgb()}) ({ahsl.ToAhsv()})");
      var ahsv = argb.ToAhsv();
      System.Console.WriteLine($"{ahsv} ({ahsv.ToArgb()}) ({ahsv.ToAhsl()})");// ({hsv.ToAhwb()})");
      var ahwb = argb.ToAhwb();
      System.Console.WriteLine($"{ahwb} ({ahwb.ToArgb()}) ({ahwb.ToAhsv()})");

      System.Console.WriteLine($"{argb.ToHtmlHexString()} | {(r * 100):N1}%, {(g * 100):N1}%, {(b * 100):N1}% | {hue:N1}, {hue2} | {(chroma * 100):N1}, {(chroma2 * 100):N1} | {(ahsv.HSV.Value * 100):N1}%, {(ahsl.HSL.Lightness * 100):N1}%, {(ahsi.HSI.Intensity * 100):N1}% | Y={argb.RGB.GetLuma601()} | {(ahsv.HSV.Saturation * 100):N1}%, {(ahsl.HSL.Saturation * 100):N1}%, {(ahsi.HSI.Saturation * 100):N1}%");
      System.Console.WriteLine();
    }
  }
}
#endif
