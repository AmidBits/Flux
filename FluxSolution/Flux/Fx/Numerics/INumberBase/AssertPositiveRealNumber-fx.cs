namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Asserts that a <paramref name="number"/> is a positive number, i.e. greater-than zero. Throws an exception if not.</summary>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static TNumber AssertPositiveNumber<TNumber>(this TNumber number, string? paramName = null)
      where TNumber : System.Numerics.INumberBase<TNumber>
      => number.IsPositiveNumber() ? number : throw new System.ArgumentOutOfRangeException(paramName ?? nameof(number), "The number must be greater than zero.");

    /// <summary>Returns whether a <paramref name="number"/> is a positive number, i.e. greater-than zero.</summary>
    public static bool IsPositiveNumber<TNumber>(this TNumber number)
      where TNumber : System.Numerics.INumberBase<TNumber>
      => !TNumber.IsNegative(number) && !TNumber.IsZero(number);
  }
}
