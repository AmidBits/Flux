namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Asserts the number is non-negative (throws an exception if it's negative).</summary>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static TNumber AssertNonNegativeRealNumber<TNumber>(this TNumber number, string? paramName = null)
      where TNumber : System.Numerics.INumber<TNumber>
      => IsNonNegativeRealNumber(number) ? number : throw new System.ArgumentOutOfRangeException(paramName ?? nameof(number), "Value must be greater than or equal to zero.");

    /// <summary>Returns whether the number is non-negative.</summary>
    public static bool IsNonNegativeRealNumber<TNumber>(this TNumber number)
      where TNumber : System.Numerics.INumber<TNumber>
      => !TNumber.IsNegative(number);
  }
}
