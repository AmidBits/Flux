#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>PREVIEW! Returns the count of all digits in x using base b.</summary>
    public static TSelf DigitCount<TSelf>(this TSelf x, TSelf b)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      AssertRadix(b);

      var count = TSelf.Zero;

      while (!TSelf.IsZero(x))
      {
        count++;

        x /= b;
      }

      return count;
    }
  }
}
#endif
