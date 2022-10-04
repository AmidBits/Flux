#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>PREVIEW! Drop the leading digit of x using base b.</summary>
    public static TSelf GetLeastSignificantDigit<TSelf>(this TSelf x, TSelf b)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => x % AssertRadix(b);
  }
}
#endif
