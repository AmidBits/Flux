namespace Flux
{
  public static partial class GenericMath
  {
#if NET7_0_OR_GREATER

    /// <summary>
    /// <para>Modular multiplication of <paramref name="dividend"/> and <paramref name="divisor"/>.</para>
    /// <see href="https://en.wikipedia.org/wiki/Modular_arithmetic"/>
    /// </summary>
    public static TSelf MulMod<TSelf>(this TSelf dividend, TSelf divisor, TSelf modulus)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      if (TSelf.IsNegative(dividend)) throw new System.ArgumentOutOfRangeException(nameof(dividend));
      if (TSelf.IsNegative(divisor)) throw new System.ArgumentOutOfRangeException(nameof(divisor));
      if (TSelf.IsNegative(modulus)) throw new System.ArgumentOutOfRangeException(nameof(modulus));

      if (dividend >= modulus) dividend %= modulus;
      if (divisor >= modulus) divisor %= modulus;

      var x = dividend;
      var c = x * divisor / modulus;
      var r = (dividend * divisor - c * modulus) % modulus;

      return TSelf.IsNegative(r) ? r + modulus : r;
    }

#else

    /// <summary>
    /// <para>Modular multiplication of <paramref name="dividend"/> and <paramref name="divisor"/>.</para>
    /// <see href="https://en.wikipedia.org/wiki/Modular_arithmetic"/>
    /// </summary>
    public static System.Numerics.BigInteger MulMod(this System.Numerics.BigInteger dividend, System.Numerics.BigInteger divisor, System.Numerics.BigInteger modulus)
    {
      if (dividend < 0) throw new System.ArgumentOutOfRangeException(nameof(dividend));
      if (divisor < 0) throw new System.ArgumentOutOfRangeException(nameof(divisor));
      if (modulus < 0) throw new System.ArgumentOutOfRangeException(nameof(modulus));

      if (dividend >= modulus)
        dividend %= modulus;
      if (divisor >= modulus)
        divisor %= modulus;

      var x = dividend;
      var c = x * divisor / modulus;
      var r = (dividend * divisor - c * modulus) % modulus;

      return r < 0 ? r + modulus : r;
    }

#endif
  }
}
