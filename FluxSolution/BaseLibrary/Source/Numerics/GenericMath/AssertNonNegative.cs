#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>PREVIEW! Asserts the number is non-negative (throws an exception if it's negative).</summary>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static TSelf AssertNonNegative<TSelf>(this TSelf number, string? paramName = null)
      where TSelf : System.Numerics.INumber<TSelf>
      => IsNonNegative(number) ? number : throw new System.ArgumentOutOfRangeException(paramName ?? nameof(number), "Must be a non-negative.");

    /// <summary>PREVIEW! Returns whether the number is non-negative.</summary>
    public static bool IsNonNegative<TSelf>(this TSelf number)
      where TSelf : System.Numerics.INumber<TSelf>
      => !TSelf.IsNegative(number);
  }
}
#endif
