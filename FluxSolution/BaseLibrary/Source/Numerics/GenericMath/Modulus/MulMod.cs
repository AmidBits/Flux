#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Modular multiplication of <paramref name="dividend"/> and <paramref name="divisor"/>.</summary>
    public static TSelf MulMod<TSelf>(this TSelf dividend, TSelf divisor, TSelf mod)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      if (dividend < TSelf.Zero) throw new System.ArgumentOutOfRangeException(nameof(dividend));
      if (divisor < TSelf.Zero) throw new System.ArgumentOutOfRangeException(nameof(divisor));
      if (mod < TSelf.Zero) throw new System.ArgumentOutOfRangeException(nameof(mod));

      if (dividend >= mod)
        dividend %= mod;
      if (divisor >= mod)
        divisor %= mod;

      var x = dividend;
      var c = x * divisor / mod;
      var r = (dividend * divisor - c * mod) % mod;

      return r < TSelf.Zero ? r + mod : r;
    }
  }
}
#endif
