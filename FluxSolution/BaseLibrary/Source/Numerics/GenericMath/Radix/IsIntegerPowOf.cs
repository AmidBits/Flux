namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Determines if <paramref name="number"/> is a power of <paramref name="radix"/>.</summary>
    public static bool IsIntegerPowOf<TSelf>(this TSelf number, TSelf radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      AssertNonNegative(number);
      AssertRadix(radix);

      if (number == radix) // If the value is equal to the radix, then it's a power of the radix.
        return true;

      if (radix == (TSelf.One + TSelf.One)) // Special case for binary numbers, we can use dedicated IsPow2().
        return TSelf.IsPow2(number);

      if (number > TSelf.One)
        while (TSelf.IsZero(number % radix))
          number /= radix;

      return number == TSelf.One;
    }
  }
}
