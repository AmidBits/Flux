namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Asserts that a <paramref name="number"/> is a non-negative real number, i.e. greater-than-or-equal to zero. Throws an exception if not.</summary>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static TNumber AssertNonNegativeRealNumber<TNumber>(this TNumber number, string? paramName = null)
      where TNumber : System.Numerics.INumber<TNumber>
      => IsNonNegativeRealNumber(number) ? number : throw new System.ArgumentOutOfRangeException(paramName ?? nameof(number), "Value must be greater than or equal to zero.");

    /// <summary>Returns whether a <paramref name="number"/> is a non-negative real number, i.e. greater-than-or-equal to zero.</summary>
    public static bool IsNonNegativeRealNumber<TNumber>(this TNumber number)
      where TNumber : System.Numerics.INumber<TNumber>
      => !TNumber.IsNegative(number);
  }
}
