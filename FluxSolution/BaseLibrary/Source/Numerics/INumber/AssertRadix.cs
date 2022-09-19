#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class BinaryInteger
  {
    public static TSelf AssertRadix<TSelf>(TSelf radix)
      where TSelf : System.Numerics.INumber<TSelf>
      => IsRadix(radix) ? radix : throw new System.ArgumentOutOfRangeException(nameof(radix));

    public static bool IsRadix<TSelf>(TSelf radix)
      where TSelf : System.Numerics.INumber<TSelf>
      => radix > TSelf.One && TSelf.IsInteger(radix);
  }
}
#endif
