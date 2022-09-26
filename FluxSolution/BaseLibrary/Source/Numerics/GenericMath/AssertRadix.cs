#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class GenericMath
  {
    public static TSelf AssertRadix<TSelf>(TSelf radix)
      where TSelf : System.Numerics.INumber<TSelf>
      => IsRadix(radix) ? radix : throw new System.ArgumentOutOfRangeException(nameof(radix), "Radix must be 2 or greater.");

    public static TRadix AssertRadix<TSelf, TRadix>(TSelf radix, out TRadix asserted)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      where TRadix : System.Numerics.INumber<TRadix>
      => IsRadix(radix) ? asserted = TRadix.CreateChecked(radix) : throw new System.ArgumentOutOfRangeException(nameof(radix), "Radix must be 2 or greater.");

    public static bool IsRadix<TSelf>(TSelf radix)
      where TSelf : System.Numerics.INumber<TSelf> // Accomodate INumber so that other types than integer can be used if needed.
      => radix > TSelf.One && TSelf.IsInteger(radix);
  }
}
#endif
