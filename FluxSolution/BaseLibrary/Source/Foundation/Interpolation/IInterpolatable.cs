namespace Flux
{
  public interface IInterpolatable
  {
    /// <summary>Computes an interpolated value between segment V1 and V2 based on the various members available in the class.</summary>
    /// <param name="mu">Defines where to estimate the value on the interpolated line, it is 0 at the first point (V1) and 1 and the second point (V2). For interpolated values between the two points mu ranges between 0 and 1. Values of mu outside this range result in extrapolation.</param>
    double GetInterpolation(double mu);
  }
}
