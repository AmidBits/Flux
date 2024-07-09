namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Returns the chroma and associated values for the RGB value.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Chrominance"/>
    public (double r, double g, double b, double min, double max, double chroma) GetChroma(this System.Drawing.Color source)
    {
      var r = System.Math.Clamp(source.R / 255d, 0, 1);
      var g = System.Math.Clamp(source.G / 255d, 0, 1);
      var b = System.Math.Clamp(source.B / 255d, 0, 1);

      var min = System.Math.Min(System.Math.Min(r, g), b);
      var max = System.Math.Max(System.Math.Max(r, g), b);

      var chroma = System.Math.Clamp(max - min, 0, 1);

      return (r, g, b, min, max, chroma);
    }
  }
}
