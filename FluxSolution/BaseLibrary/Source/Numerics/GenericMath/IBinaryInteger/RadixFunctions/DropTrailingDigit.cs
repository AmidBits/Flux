#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class BinaryInteger
  {
    /// <summary>PREVIEW! Drop the trailing digit of the number.</summary>
    public static TSelf DropTrailingDigit<TSelf>(this TSelf value, TSelf radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => value / Number.AssertRadix(radix);
  }
}
#endif
