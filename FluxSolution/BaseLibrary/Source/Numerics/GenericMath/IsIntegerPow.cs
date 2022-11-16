namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Determines if <paramref name="x"/> is a power of <paramref name="b"/>.</summary>
    public static bool IsIntegerPow<TSelf>(this TSelf x, TSelf b)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      AssertNonNegative(x);
      AssertRadix(b);

      if (x == b) // If the value is equal to the radix, then it's a power of the radix.
        return true;

      if (b == (TSelf.One + TSelf.One)) // Special case for binary numbers, we can use dedicated IsPow2().
        return TSelf.IsPow2(x);

      if (x > TSelf.One)
        while (TSelf.IsZero(x % b))
          x /= b;

      return x == TSelf.One;
    }
  }
}
