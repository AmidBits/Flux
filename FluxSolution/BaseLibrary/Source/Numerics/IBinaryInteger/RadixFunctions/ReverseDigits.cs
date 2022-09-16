#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static TSelf ReverseDigits<TSelf>(this TSelf value, TSelf radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      AssertRadix(radix);

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
