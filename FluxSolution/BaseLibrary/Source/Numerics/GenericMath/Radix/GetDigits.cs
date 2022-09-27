#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>PREVIEW! Returns the digits (as numbers) of a value in the specified radix.</summary>
    public static System.Span<TSelf> GetDigits<TSelf>(this TSelf value, TSelf radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      var reversed = GetDigitsReversed(value, radix);
      reversed.Reverse();
      return reversed;
    }
  }
}
#endif
