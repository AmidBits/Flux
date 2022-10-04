#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>PREVIEW! Returns the sum of all digits in x using base b.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Digit_sum"/>
    public static TSelf DigitSum<TSelf>(this TSelf x, TSelf b)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      AssertRadix(b);

      var sum = TSelf.Zero;

      while (!TSelf.IsZero(x))
      {
        sum += x % b;

        x /= b;
      }

      return sum;
    }
  }
}
#endif
