namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>
    /// <para>Asserts that a <paramref name="number"/> is a non-negative real number, i.e. greater-than-or-equal to zero. Throws an exception if not.</para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="number"></param>
    /// <param name="paramName"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static TNumber AssertNonNegativeNumber<TNumber>(this TNumber number, string? paramName = null)
      where TNumber : System.Numerics.INumberBase<TNumber>
      => IsNonNegativeNumber(number) ? number : throw new System.ArgumentOutOfRangeException(paramName ?? nameof(number), "The number must be greater than or equal to zero.");

    /// <summary>
    /// <para>Returns whether a <paramref name="number"/> is a non-negative real number, i.e. greater-than-or-equal to zero.</para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="number"></param>
    /// <returns></returns>
    public static bool IsNonNegativeNumber<TNumber>(this TNumber number)
      where TNumber : System.Numerics.INumberBase<TNumber>
      => !TNumber.IsNegative(number);
  }
}
