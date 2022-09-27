#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>PREVIEW! Returns the sum of all digits in the value using the specified radix.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Digit_sum"/>
    public static TSelf DigitSum<TSelf>(this TSelf value, TSelf radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      AssertRadix(radix);

      var sum = TSelf.Zero;

      while (!TSelf.IsZero(value))
      {
        sum += value % radix;

        value /= radix;
      }

      return sum;
    }
  }
}
#endif
