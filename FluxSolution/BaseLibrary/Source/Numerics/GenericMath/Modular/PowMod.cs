namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Modular exponentiation of <paramref name="dividend"/> and <paramref name="divisor"/>.</summary>
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
  }
}
