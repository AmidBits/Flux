namespace Flux
{
  public static partial class Maths
  {
    /// <summary>The function Cdf(x) is the cumulative density function(CDF) of a standard normal (Gaussian) random variable. It is closely related to the error function Erf(x).</summary>
    public static double Cdf(double x)
    {
      // constants
      var a1 = 0.254829592;
      var a2 = -0.284496736;
      var a3 = 1.421413741;
      var a4 = -1.453152027;
      var a5 = 1.061405429;
      var p = 0.3275911;

      // Save the sign of x
      var sign = x < 0 ? -1 : 1;

      x = System.Math.Abs(x) / System.Math.Sqrt(2d);

      // A&S formula 7.1.26
      var t = 1d / (1d + p * x);
      var y = 1d - (((((a5 * t + a4) * t) + a3) * t + a2) * t + a1) * t * System.Math.Exp(-x * x);

      return 0.5 * (1d + sign * y);
    }

    public static double RationalApproximation(double t)
    {
      // Abramowitz and Stegun formula 26.2.23.
      // The absolute value of the error should be less than 4.5 e-4.
      double[] c = { 2.515517, 0.802853, 0.010328 };
      double[] d = { 1.432788, 0.189269, 0.001308 };

      return t - ((c[2] * t + c[1]) * t + c[0]) / (((d[2] * t + d[1]) * t + d[0]) * t + 1d);
    }

    /// <summary>Compute the inverse of the normal (Gaussian) CDF. </summary>
    public static double NormalCdfInverse(double p)
    {
      if (p <= 0d || p >= 1d)
      {
        throw new System.ArgumentOutOfRangeException(nameof(p));
      }

      // See article above for explanation of the following section.
      if (p < 0.5)
      {
        // F^-1(p) = - G^-1(p)
        return -RationalApproximation(System.Math.Sqrt(-2d * System.Math.Log(p)));
      }
      else
      {
        // F^-1(p) = G^-1(1-p)
        return RationalApproximation(System.Math.Sqrt(-2d * System.Math.Log(1d - p)));
      }
    }
  }
}
