#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class GenericMath
  {
    public static TSelf AssertRadix<TSelf>(TSelf radix, string? paramName = null)
      where TSelf : System.Numerics.INumber<TSelf>
      => IsRadix(radix) ? radix : throw new System.ArgumentOutOfRangeException(paramName ?? nameof(radix), "Must be an integer value greater than or equal to 2.");

    public static bool IsRadix<TSelf>(TSelf radix)
      where TSelf : System.Numerics.INumber<TSelf> // Accomodate INumber so that other types than integer can be used if needed.
      => radix > TSelf.One && TSelf.IsInteger(radix);
  }
}
#endif
