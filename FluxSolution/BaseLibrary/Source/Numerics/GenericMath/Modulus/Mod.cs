namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Returns the Euclidean modulus of <paramref name="dividend"/> and <paramref name="divisor"/>, i.e. the remainder of modular division of a and b.</summary>
    public static TSelf Mod<TSelf>(this TSelf dividend, TSelf divisor)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      if (TSelf.IsZero(divisor)) throw new System.DivideByZeroException();

      if (divisor == -TSelf.One)
        return TSelf.Zero;

      var m = dividend % divisor;

      if (m < TSelf.Zero)
        return divisor < TSelf.Zero ? m - divisor : m + divisor;

      return m;
    }
  }
}
