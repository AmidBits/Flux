namespace Flux
{
  public static partial class GenericMath
  {
#if NET7_0_OR_GREATER

    /// <summary>Asserts the number is non-negative (throws an exception if it's negative).</summary>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static TSelf AssertNonNegative<TSelf>(this TSelf value, string? paramName = null)
      where TSelf : System.Numerics.INumber<TSelf>
      => IsNonNegative(value) ? value : throw new System.ArgumentOutOfRangeException(paramName ?? nameof(value), "Must be a non-negative.");

    /// <summary>Returns whether the number is non-negative.</summary>
    public static bool IsNonNegative<TSelf>(this TSelf value)
      where TSelf : System.Numerics.INumber<TSelf>
      => !TSelf.IsNegative(value);

#else

    /// <summary>Asserts the number is non-negative (throws an exception if it's negative).</summary>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static System.Numerics.BigInteger AssertNonNegative(this System.Numerics.BigInteger number, string? paramName = null)
      => IsNonNegative(number) ? number : throw new System.ArgumentOutOfRangeException(paramName ?? nameof(number), "Must be a non-negative.");

    /// <summary>Asserts the number is non-negative (throws an exception if it's negative).</summary>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static System.Numerics.BigInteger AssertNonNegative(this int number, string? paramName = null)
      => IsNonNegative(number) ? number : throw new System.ArgumentOutOfRangeException(paramName ?? nameof(number), "Must be a non-negative.");

    /// <summary>Asserts the number is non-negative (throws an exception if it's negative).</summary>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static System.Numerics.BigInteger AssertNonNegative(this long number, string? paramName = null)
      => IsNonNegative(number) ? number : throw new System.ArgumentOutOfRangeException(paramName ?? nameof(number), "Must be a non-negative.");

    /// <summary>Returns whether the number is non-negative.</summary>
    public static bool IsNonNegative(this System.Numerics.BigInteger number)
      => number >= System.Numerics.BigInteger.Zero;

    /// <summary>Returns whether the number is non-negative.</summary>
    public static bool IsNonNegative(this int number)
      => number >= 0;

    /// <summary>Returns whether the number is non-negative.</summary>
    public static bool IsNonNegative(this long number)
      => number >= 0L;

#endif
  }
}
