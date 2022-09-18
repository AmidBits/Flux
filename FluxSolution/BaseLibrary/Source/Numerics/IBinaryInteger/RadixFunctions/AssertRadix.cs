#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class BinaryInteger
  {
    private static TSelf AssertRadix<TSelf>(TSelf radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => radix > TSelf.One ? radix : throw new System.ArgumentOutOfRangeException(nameof(radix));
  }
}
#endif
