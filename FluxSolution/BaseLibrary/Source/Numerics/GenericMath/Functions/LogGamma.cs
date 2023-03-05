namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary></summary>
    /// <param name="x">Any positive value.</param>
    /// <see href="https://www.johndcook.com/blog/stand_alone_code/"/>
    /// <see href="https://www.johndcook.com/blog/csharp_gamma/"/>
    public static TSelf LogGamma<TSelf>(this TSelf x)
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
    {
      if (x <= TSelf.Zero)
      {
        throw new System.ArgumentOutOfRangeException(nameof(x), @"Must be positive.");
      }

      if (x < TSelf.CreateChecked(12.0))
      {
        return TSelf.Log(TSelf.Abs(Gamma(x)));
      }

      // Abramowitz and Stegun 6.1.41
      // Asymptotic series should be good to at least 11 or 12 figures
      // For error analysis, see Whittiker and Watson
      // A Course in Modern Analysis (1927), page 252

      var z = TSelf.One / (x * x);

      var sum = TSelf.CreateChecked(-3617.0 / 122400.0);

      sum *= z;
      sum += TSelf.CreateChecked(1.0 / 156.0);

      sum *= z;
      sum += TSelf.CreateChecked(-691.0 / 360360.0);

      sum *= z;
      sum += TSelf.CreateChecked(1.0 / 1188.0);

      sum *= z;
      sum += TSelf.CreateChecked(-1.0 / 1680.0);

      sum *= z;
      sum += TSelf.CreateChecked(1.0 / 1260.0);

      sum *= z;
      sum += TSelf.CreateChecked(-1.0 / 360.0);

      sum *= z;
      sum += TSelf.CreateChecked(1.0 / 12.0);

      var series = sum / x;

      var halfLogTwoPi = TSelf.CreateChecked(0.91893853320467274178032973640562);

      var logGamma = (x - TSelf.One.Divide(2)) * TSelf.Log(x) - x + halfLogTwoPi + series;

      return logGamma;
    }
  }
}
