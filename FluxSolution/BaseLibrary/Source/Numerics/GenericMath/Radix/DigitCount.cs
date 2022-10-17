#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>PREVIEW! Returns the count of all single digits in <paramref name="number"/> using base <paramref name="radix"/>.</summary>
    public static TSelf DigitCount<TSelf>(this TSelf number, TSelf radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      AssertRadix(radix);

      var count = TSelf.Zero;

      while (!TSelf.IsZero(number))
      {
        count++;

        number /= radix;
      }

      return count;
    }
  }
}
#endif
