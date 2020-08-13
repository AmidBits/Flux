
namespace Flux
{
  public static partial class Maths
  {
    /// <see cref="http://paulbourke.net/miscellaneous/interpolation/"/>
    public static partial class Interpolate
    {
      /// <summary>Cosine interpolation is a smoother and perhaps simplest function. A suitable orientated piece of a cosine function serves to provide a smooth transition between adjacent segments.</summary>
      /// <param name="mu">The parameter mu defines where to estimate the value on the interpolated line, it is 0 at the first point and 1 and the second point. For interpolated values between the two points mu ranges between 0 and 1. Values of mu outside the range result in extrapolation.</param>
      public static double Cosine(double y1, double y2, double mu) 
        => ((1.0 - System.Math.Cos(mu * System.Math.PI)) / 2.0) is double mu2 ? (y1 * (1.0 - mu2) + y2 * mu2) : throw new System.ArgumentException();
    }
  }
}
