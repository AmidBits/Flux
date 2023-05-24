namespace Flux
{
  public static partial class GenericMath
  {
#if NET7_0_OR_GREATER

    /// <summary>
    /// <para>Modular exponentiation of <paramref name="dividend"/> and <paramref name="divisor"/>.</para>
    /// <see href="https://en.wikipedia.org/wiki/Modular_arithmetic"/>
    /// </summary>
    public static TSelf PowMod<TSelf>(this TSelf dividend, TSelf divisor, TSelf modulus)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      if (dividend < TSelf.Zero) throw new System.ArgumentOutOfRangeException(nameof(dividend));
      if (divisor < TSelf.Zero) throw new System.ArgumentOutOfRangeException(nameof(divisor));
      if (modulus < TSelf.Zero) throw new System.ArgumentOutOfRangeException(nameof(modulus));

      var r = modulus == TSelf.One ? TSelf.Zero : TSelf.One;

      while (divisor > TSelf.Zero)
      {
        if (TSelf.IsOddInteger(divisor)) // if ((b & TSelf.One) != TSelf.Zero)
          r = MulMod(r, dividend, modulus);

        divisor >>= 1;
        dividend = MulMod(dividend, dividend, modulus);
      }

      return r;
    }

#else

    /// <summary>
    /// <para>Modular exponentiation of <paramref name="dividend"/> and <paramref name="divisor"/>.</para>
    /// <see href="https://en.wikipedia.org/wiki/Modular_arithmetic"/>
    /// </summary>
    public static System.Numerics.BigInteger PowMod(this System.Numerics.BigInteger dividend, System.Numerics.BigInteger divisor, System.Numerics.BigInteger modulus)
    {
      if (dividend < 0) throw new System.ArgumentOutOfRangeException(nameof(dividend));
      if (divisor < 0) throw new System.ArgumentOutOfRangeException(nameof(divisor));
      if (modulus < 0) throw new System.ArgumentOutOfRangeException(nameof(modulus));

      var r = modulus == System.Numerics.BigInteger.One ? System.Numerics.BigInteger.Zero : System.Numerics.BigInteger.One;

      while (divisor > System.Numerics.BigInteger.Zero)
      {
        if (!divisor.IsEven) // if ((b & TSelf.One) != TSelf.Zero)
          r = MulMod(r, dividend, modulus);

        divisor >>= 1;
        dividend = MulMod(dividend, dividend, modulus);
      }

      return r;
    }

#endif
  }
}
