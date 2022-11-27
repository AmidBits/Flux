namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Determines if <paramref name="number"/> is a power of <paramref name="radix"/>.</summary>
    public static bool IsIntegerPowOf<TSelf, TRadix>(this TSelf number, TRadix radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
    {
      AssertNonNegative(number);
      AssertRadix(radix, out TSelf tradix);

      if (number == tradix) // If the value is equal to the radix, then it's a power of the radix.
        return true;

      if (radix == (TRadix.One + TRadix.One)) // Special case for binary numbers, we can use dedicated IsPow2().
        return TSelf.IsPow2(number);

      if (number > TSelf.One)
        while (TSelf.IsZero(number % tradix))
          number /= tradix;

      return number == TSelf.One;
    }
  }
}
