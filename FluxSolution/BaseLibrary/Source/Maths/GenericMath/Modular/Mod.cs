namespace Flux
{
  public static partial class Maths
  {
#if NET7_0_OR_GREATER

    /// <summary>Returns the remainder of the Euclidean division of <paramref name="dividend"/> and <paramref name="divisor"/>, i.e. the remainder of modular division of a and b.</summary>
    public static TSelf Mod<TSelf>(this TSelf dividend, TSelf divisor)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      if (TSelf.IsZero(divisor)) throw new System.DivideByZeroException();

      if (divisor == -TSelf.One)
        return TSelf.Zero;

      var m = dividend % divisor;

      if (TSelf.IsNegative(m))
        return TSelf.IsNegative(divisor) ? m - divisor : m + divisor;

      return m;
    }

#else

    /// <summary>Returns the remainder of the Euclidean division of <paramref name="dividend"/> and <paramref name="divisor"/>, i.e. the remainder of modular division of a and b.</summary>
    public static System.Numerics.BigInteger Mod(this System.Numerics.BigInteger dividend, System.Numerics.BigInteger divisor)
    {
      if (divisor == System.Numerics.BigInteger.Zero) throw new System.DivideByZeroException();

      if (divisor == System.Numerics.BigInteger.MinusOne)
        return 0;

      var m = dividend % divisor;

      if (m < 0)
        return divisor < 0 ? m - divisor : m + divisor;

      return m;
    }

#endif
  }
}
