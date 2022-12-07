namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Determines if <paramref name="value"/> is a power of <paramref name="radix"/>.</summary>
    public static bool IsPow<TSelf, TRadix>(this TSelf value, TRadix radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
    {
      AssertNonNegative(value);
      var tradix = TSelf.CreateChecked(AssertRadix(radix));

      if (value == tradix) // If the value is equal to the radix, then it's a power of the radix.
        return true;

      if (radix == (TRadix.One + TRadix.One)) // Special case for binary numbers, we can use dedicated IsPow2().
        return TSelf.IsPow2(value);

      if (value > TSelf.One)
        while (TSelf.IsZero(value % tradix))
          value /= tradix;

      return value == TSelf.One;
    }
  }
}
