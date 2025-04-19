namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>
    /// <para>Asserts that a <paramref name="value"/> is a positive number, i.e. greater-than zero. Throws an exception if not.</para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="value"></param>
    /// <param name="paramName"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static TNumber AssertPositiveNumber<TNumber>(this TNumber value, string? paramName = null)
      where TNumber : System.Numerics.INumberBase<TNumber>
      => value.IsPositiveNumber() ? value : throw new System.ArgumentOutOfRangeException(paramName ?? nameof(value), "The number must be greater than zero.");

    /// <summary>
    /// <para>Returns whether a <paramref name="value"/> is a positive number, i.e. greater-than zero.</para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static bool IsPositiveNumber<TNumber>(this TNumber value)
      where TNumber : System.Numerics.INumberBase<TNumber>
      => !TNumber.IsNegative(value) && !TNumber.IsZero(value);
  }
}
