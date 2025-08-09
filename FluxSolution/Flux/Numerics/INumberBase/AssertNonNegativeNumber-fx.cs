namespace Flux
{
  public static partial class NumberBase
  {
    /// <summary>
    /// <para>Asserts that a <paramref name="value"/> is a non-negative real number, i.e. greater-than-or-equal to zero. Throws an exception if not.</para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="value"></param>
    /// <param name="paramName"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static TNumber AssertNonNegativeNumber<TNumber>(this TNumber value, string? paramName = null)
      where TNumber : System.Numerics.INumberBase<TNumber>
      => IsNonNegativeNumber(value) ? value : throw new System.ArgumentOutOfRangeException(paramName ?? nameof(value), "The number must be greater than or equal to zero.");

    /// <summary>
    /// <para>Returns whether a <paramref name="value"/> is a non-negative real number, i.e. greater-than-or-equal to zero.</para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static bool IsNonNegativeNumber<TNumber>(this TNumber value)
      where TNumber : System.Numerics.INumberBase<TNumber>
      => !TNumber.IsNegative(value);
  }
}
