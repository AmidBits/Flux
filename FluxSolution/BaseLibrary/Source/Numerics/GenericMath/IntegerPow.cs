#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>PREVIEW! Returns <paramref name="x"/> raised to the power of <paramref name="n"/>, using exponentiation by squaring.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Exponentiation"/>
    /// <see cref="https://en.wikipedia.org/wiki/Exponentiation_by_squaring"/>
    public static TBase IntegerPow<TBase, TExponent>(this TBase x, TExponent n)
      where TBase : System.Numerics.IBinaryInteger<TBase>
      where TExponent : System.Numerics.IBinaryInteger<TExponent>
    {
      AssertNonNegativeValue(n);

      if (TExponent.IsZero(n))
        return TBase.One;

      var y = TBase.One;

      while (n > TExponent.One)
        checked
        {
          if (TExponent.IsOddInteger(n))
            y *= x;

          x *= x;
          n >>= 1;
        }

      return x * y;
    }
  }
}
#endif
