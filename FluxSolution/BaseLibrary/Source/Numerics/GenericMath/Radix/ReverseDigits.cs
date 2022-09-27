#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>PREVIEW! Reverse the digits of the number in the specified radix, obtaining a new number.</summary>
    public static TSelf ReverseDigits<TSelf>(this TSelf value, TSelf radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      AssertRadix(radix);

      var reverse = TSelf.Zero;

      while (!TSelf.IsZero(value))
      {
        reverse = reverse * radix + (value % radix);

        value /= radix;
      }

      return reverse;
    }
  }
}
#endif
