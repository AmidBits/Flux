namespace Flux.Dsp
{
  public sealed class Perlin2
  {
    public double Noise(double x, double y)
    {
      var x0 = (int)System.Math.Floor(x);
      var x1 = x0 + 1;
      var y0 = (int)System.Math.Floor(y);
      var y1 = y0 + 1;

      var sx = x - x0;
      var sy = y - y0;

      return Lerp(
        Lerp(
          DotGridGradient(x0, y0, x, y),
          DotGridGradient(x1, y0, x, y),
          sx
        ),
        Lerp(
          DotGridGradient(x0, y1, x, y),
          DotGridGradient(x1, y1, x, y),
          sx
        ),
        sy
      );
    }

    #region Static methods
    public static CartesianCoordinateR2 RandomGradient(int ix, int iy)
    {
      // No precomputed gradients mean this works for any number of grid coordinates
      const int w = 8 * 4;
      const int s = w / 2; // rotation width

      var a = unchecked((uint)ix);
      var b = unchecked((uint)iy);

      a *= 3284157443; b ^= a << s | a >> w - s;
      b *= 1911520717; a ^= b << s | b >> w - s;
      a *= 2048419325;

      var random = a * (3.14159265 / ~(~0u >> 1)); // in [0, 2*Pi]

      return new CartesianCoordinateR2(System.Math.Cos(random), System.Math.Sin(random));
    }
    public static double DotGridGradient(int ix, int iy, double x, double y)
    {
      var gradient = RandomGradient(ix, iy);

      var dx = x - ix;
      var dy = y - iy;

      return dx * gradient.X + dy * gradient.Y; // Dot product.
    }
    public static double Lerp(double a0, double a1, double mu)
      => (a1 - a0) * mu + a0;
    #endregion Static methods
  }
}
