namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Asserts that a <paramref name="number"/> is a positive real number, i.e. greater-than zero. Throws an exception if not.</summary>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static TNumber AssertPositiveRealNumber<TNumber>(this TNumber number, string? paramName = null)
      where TNumber : System.Numerics.INumber<TNumber>
      => number.IsPositiveRealNumber() ? number : throw new System.ArgumentOutOfRangeException(paramName ?? nameof(number), "Value must be greater than zero.");

    /// <summary>Returns whether a <paramref name="number"/> is a positive real number, i.e. greater-than zero.</summary>
    public static bool IsPositiveRealNumber<TNumber>(this TNumber number)
      where TNumber : System.Numerics.INumber<TNumber>
      => !TNumber.IsNegative(number) && !TNumber.IsZero(number);
  }
}
