#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>PREVIEW! Asserts the number is a valid radix (throws an exception if it's negative).</summary>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static TSelf AssertRadix<TSelf>(TSelf radix, string? paramName = null)
      where TSelf : System.Numerics.INumber<TSelf>
      => IsRadix(radix) ? radix : throw new System.ArgumentOutOfRangeException(paramName ?? nameof(radix), "Must be an integer, greater than or equal to 2.");

    /// <summary>PREVIEW! Returns whether the number is a valid radix.</summary>
    public static bool IsRadix<TSelf>(TSelf radix)
      where TSelf : System.Numerics.INumber<TSelf> // Accomodate INumber so that other types than integer can be used if needed.
      => radix > TSelf.One && TSelf.IsInteger(radix);
  }
}
#endif
