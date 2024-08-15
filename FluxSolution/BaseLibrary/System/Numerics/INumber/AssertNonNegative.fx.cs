namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Asserts the number is non-negative (throws an exception if it's negative).</summary>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static TSelf AssertNonNegative<TSelf>(this TSelf value, string? paramName = null)
      where TSelf : System.Numerics.INumber<TSelf>
      => IsNonNegative(value) ? value : throw new System.ArgumentOutOfRangeException(paramName ?? nameof(value), "Must be a non-negative.");

    /// <summary>Returns whether the number is non-negative.</summary>
    public static bool IsNonNegative<TSelf>(this TSelf value)
      where TSelf : System.Numerics.INumber<TSelf>
      => !TSelf.IsNegative(value);
  }
}
