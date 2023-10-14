namespace Flux
{
  public static partial class NumericsExtensionMethods
  {
#if NET7_0_OR_GREATER

    /// <summary>Strips the integer part of the floating point value, resulting in only the fractional part.</summary>
    public static TSelf GetFraction<TSelf, TInteger>(this TSelf source, out TInteger integer)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      var whole = TSelf.Truncate(source);

      integer = TInteger.CreateChecked(whole);

      return source - whole;
    }

#else

    /// <summary>Strips the integer part of the floating point value, resulting in only the fractional part.</summary>
    public static double GetFraction(this double source, out System.Numerics.BigInteger integer)
    {
      var whole = System.Math.Truncate(source);

      integer = new System.Numerics.BigInteger(whole);

      return source - whole;
    }

#endif
  }
}
