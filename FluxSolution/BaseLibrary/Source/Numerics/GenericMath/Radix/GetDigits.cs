#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>PREVIEW! Returns the digits (as numbers) of x using base b.</summary>
    public static System.Span<TSelf> GetDigits<TSelf>(this TSelf x, TSelf b)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      var reversed = GetDigitsReversed(x, b);
      reversed.Reverse();
      return reversed;
    }
  }
}
#endif
