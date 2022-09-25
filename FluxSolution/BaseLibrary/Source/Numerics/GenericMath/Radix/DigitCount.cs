#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class Radix
  {
    /// <summary>PREVIEW! Returns the count of all digits in the value using the specified radix.</summary>
    public static TSelf DigitCount<TSelf>(this TSelf value, TSelf radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      GenericMath.AssertRadix(radix);

      var count = TSelf.Zero;

      while (!TSelf.IsZero(value))
      {
        count++;

        value /= radix;
      }

      return count;
    }
  }
}
#endif
