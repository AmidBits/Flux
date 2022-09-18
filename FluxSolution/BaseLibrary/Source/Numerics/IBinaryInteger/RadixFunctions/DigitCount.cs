#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class BinaryInteger
  {
    /// <summary>PREVIEW! Returns the count of all digits in the value using the specified radix.</summary>
    public static TSelf DigitCount<TSelf>(this TSelf value, TSelf radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      AssertRadix(radix);

      var count = TSelf.Zero;

      while (value != TSelf.Zero)
      {
        count++;

        value /= radix;
      }

      return count;
    }
  }
}
#endif
