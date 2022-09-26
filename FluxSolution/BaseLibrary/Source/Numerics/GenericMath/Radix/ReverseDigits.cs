#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class Radix
  {
    public static TSelf ReverseDigits<TSelf>(this TSelf value, TSelf radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      GenericMath.AssertRadix(radix);

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
