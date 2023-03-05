namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Implementation see reference.</summary>
    /// <see href="https://www.johndcook.com/blog/stand_alone_code/"/>
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
    /// <see href="https://www.johndcook.com/blog/stand_alone_code/"/>
    /// <see href="https://www.johndcook.com/blog/normal_cdf_inverse/"/>
    public static TSelf NormalCdfInverse<TSelf>(this TSelf p)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.ILogarithmicFunctions<TSelf>, System.Numerics.IRootFunctions<TSelf>
    {
      if (p <= TSelf.Zero || p >= TSelf.One) throw new System.ArgumentOutOfRangeException(nameof(p));

      // See article above for explanation of the following section.
      return (p < TSelf.One.Divide(2))
        ? -RationalApproximation(TSelf.Sqrt(TSelf.NegativeOne.Multiply(2) * TSelf.Log(p))) // F^-1(p) = - G^-1(p)
        : RationalApproximation(TSelf.Sqrt(TSelf.NegativeOne.Multiply(2) * TSelf.Log(TSelf.One - p))); // F^-1(p) = G^-1(1-p)
    }
  }
}
