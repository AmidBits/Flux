namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Modular exponentiation of <paramref name="dividend"/> and <paramref name="divisor"/>.</summary>
    public static TSelf PowMod<TSelf>(this TSelf dividend, TSelf divisor, TSelf mod)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      if (dividend < TSelf.Zero) throw new System.ArgumentOutOfRangeException(nameof(dividend));
      if (divisor < TSelf.Zero) throw new System.ArgumentOutOfRangeException(nameof(divisor));
      if (mod < TSelf.Zero) throw new System.ArgumentOutOfRangeException(nameof(mod));

      var r = mod == TSelf.One ? TSelf.Zero : TSelf.One;

      while (divisor > TSelf.Zero)
      {
        if (TSelf.IsOddInteger(divisor)) // if ((b & TSelf.One) != TSelf.Zero)
          r = MulMod(r, dividend, mod);

        divisor >>= 1;
        dividend = MulMod(dividend, dividend, mod);
      }

      return r;
    }
  }
}
