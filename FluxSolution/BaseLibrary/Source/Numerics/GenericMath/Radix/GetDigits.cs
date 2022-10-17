#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>PREVIEW! Returns the individual digits (as numbers) of <paramref name="number"/> using base <paramref name="radix"/>.</summary>
    public static System.Span<TSelf> GetDigits<TSelf>(this TSelf number, TSelf radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      var reversed = GetDigitsReversed(number, radix);
      reversed.Reverse();
      return reversed;
    }
  }
}
#endif
