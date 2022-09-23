#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class BinaryInteger
  {
    public static TSelf ReverseDigits<TSelf>(this TSelf value, TSelf radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      Number.AssertRadix(radix);

      var reverse = TSelf.Zero;

      while (value != TSelf.Zero)
      {
        value /= radix;

        reverse = reverse * radix + (value % radix);
      }

      return reverse;
    }
  }
}
#endif
