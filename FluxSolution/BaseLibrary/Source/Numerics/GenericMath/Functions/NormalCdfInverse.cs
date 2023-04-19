namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Implementation see reference.</summary>
    /// <see href="https://www.johndcook.com/blog/stand_alone_code/"/>
    /// <see href="https://www.johndcook.com/blog/normal_cdf_inverse/"/>
    /// <seealso href="https://en.wikipedia.org/wiki/Horner%27s_method"/>
    public static double RationalApproximation(double t)
    {
      // Abramowitz and Stegun formula 26.2.23. The absolute value of the error should be less than 4.5 e-4.

      var c0 = 2.515517;
      var c1 = 0.802853;
      var c2 = 0.010328;

      var d0 = 1.432788;
      var d1 = 0.189269;
      var d2 = 0.001308;

      return t - ((c2 * t + c1) * t + c0) / (((d2 * t + d1) * t + d0) * t + 1);
    }

    /// <summary>Compute the inverse of the normal (Gaussian) CDF. </summary>
    /// <see href="https://www.johndcook.com/blog/stand_alone_code/"/>
    /// <see href="https://www.johndcook.com/blog/normal_cdf_inverse/"/>
    public static double NormalCdfInverse(this double p)
    {
      if (p <= 0 || p >= 1) throw new System.ArgumentOutOfRangeException(nameof(p));

      // See article above for explanation of the following section.
      return (p < 0.5)
        ? -RationalApproximation(System.Math.Sqrt(-2 * System.Math.Log(p))) // F^-1(p) = - G^-1(p)
        : RationalApproximation(System.Math.Sqrt(-2 * System.Math.Log(1 - p))); // F^-1(p) = G^-1(1-p)
    }
  }
}
