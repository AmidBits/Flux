#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>PREVIEW! Asserts the number is non-negative, or throws an exception if it's negative.</summary>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static TSelf AssertNonNegativeValue<TSelf>(this TSelf value)
      where TSelf : System.Numerics.INumber<TSelf>
      => IsNonNegativeValue(value) ? value : throw new System.ArgumentOutOfRangeException(nameof(value), "Non-negative value required.");

    /// <summary>PREVIEW! Determines if the number is non-negative.</summary>
    public static bool IsNonNegativeValue<TSelf>(this TSelf value)
      where TSelf : System.Numerics.INumber<TSelf>
      => value >= TSelf.Zero;
  }
}
#endif
