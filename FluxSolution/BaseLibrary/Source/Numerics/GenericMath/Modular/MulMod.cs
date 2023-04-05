namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>
    /// <para>Modular multiplication of <paramref name="dividend"/> and <paramref name="divisor"/>.</para>
    /// <see href="https://en.wikipedia.org/wiki/Modular_arithmetic"/>
    /// </summary>
    public static TSelf MulMod<TSelf>(this TSelf dividend, TSelf divisor, TSelf modulus)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      if (dividend < TSelf.Zero) throw new System.ArgumentOutOfRangeException(nameof(dividend));
      if (divisor < TSelf.Zero) throw new System.ArgumentOutOfRangeException(nameof(divisor));
      if (modulus < TSelf.Zero) throw new System.ArgumentOutOfRangeException(nameof(modulus));

      if (dividend >= modulus)
        dividend %= modulus;
      if (divisor >= modulus)
        divisor %= modulus;

      var x = dividend;
      var c = x * divisor / modulus;
      var r = (dividend * divisor - c * modulus) % modulus;

      return r < TSelf.Zero ? r + modulus : r;
    }
  }
}
