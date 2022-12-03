namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>The function Cdf(x) is the cumulative density function(CDF) of a standard normal (Gaussian) random variable. It is closely related to the error function Erf(x).</summary>
    public static TSelf Cdf<TSelf>(TSelf x)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.IExponentialFunctions<TSelf>, System.Numerics.IRootFunctions<TSelf>
    {
      var a1 = TSelf.CreateChecked(0.254829592);
      var a2 = TSelf.CreateChecked(-0.284496736);
      var a3 = TSelf.CreateChecked(1.421413741);
      var a4 = TSelf.CreateChecked(-1.453152027);
      var a5 = TSelf.CreateChecked(1.061405429);

      var p = TSelf.CreateChecked(0.3275911);

      var signX = x < TSelf.Zero ? -TSelf.One : TSelf.One;

      x = TSelf.Abs(x) / TSelf.Sqrt(TSelf.One.Multiply(2));

      // A&S formula 7.1.26
      var t = TSelf.One / (TSelf.One + p * x);
      var y = TSelf.One - (((((a5 * t + a4) * t) + a3) * t + a2) * t + a1) * t * TSelf.Exp(-x * x);

      return TSelf.One.Divide(2) * (TSelf.One + signX * y);
    }

    /// <summary></summary>
    /// <see href="https://www.johndcook.com/blog/normal_cdf_inverse/"/>
    /// <seealso href="https://en.wikipedia.org/wiki/Horner%27s_method"/>
    public static TSelf RationalApproximation<TSelf>(TSelf t)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
    {
      // Abramowitz and Stegun formula 26.2.23. The absolute value of the error should be less than 4.5 e-4.

      var c0 = TSelf.CreateChecked(2.515517);
      var c1 = TSelf.CreateChecked(0.802853);
      var c2 = TSelf.CreateChecked(0.010328);

      var d0 = TSelf.CreateChecked(1.432788);
      var d1 = TSelf.CreateChecked(0.189269);
      var d2 = TSelf.CreateChecked(0.001308);

      return t - ((c2 * t + c1) * t + c0) / (((d2 * t + d1) * t + d0) * t + TSelf.One);
    }

    /// <summary>Compute the inverse of the normal (Gaussian) CDF. </summary>
    /// <see href="https://www.johndcook.com/blog/normal_cdf_inverse/"/>
    public static TSelf NormalCdfInverse<TSelf>(TSelf p)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.ILogarithmicFunctions<TSelf>, System.Numerics.IRootFunctions<TSelf>
    {
      if (p <= TSelf.Zero || p >= TSelf.One) throw new System.ArgumentOutOfRangeException(nameof(p));

      var two = TSelf.One.Multiply(2);

      // See article above for explanation of the following section.
      return (p < TSelf.One.Divide(2))
        ? -RationalApproximation(TSelf.Sqrt(-two * TSelf.Log(p))) // F^-1(p) = - G^-1(p)
        : RationalApproximation(TSelf.Sqrt(-two * TSelf.Log(TSelf.One - p))); // F^-1(p) = G^-1(1-p)
    }
  }
}
