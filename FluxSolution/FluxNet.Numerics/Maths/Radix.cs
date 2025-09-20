namespace FluxNet.Numerics
{
  public static partial class Maths
  {
    /// <summary>
    /// <para>Asserts that a <paramref name="radix"/> is a radix, i.e. greater than or equal to 2. Throws an exception if not.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Natural_number"/></para>
    /// </summary>
    /// <typeparam name="TInteger"></typeparam>
    /// <param name="radix"></param>
    /// <param name="paramName"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static TInteger AssertRadix<TInteger>(this TInteger radix, string? paramName = null)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      => IsRadix(radix) ? radix : throw new System.ArgumentOutOfRangeException(paramName ?? nameof(radix), "Must be greater than or equal to 2.");

    /// <summary>
    /// <para>Asserts that a <paramref name="radix"/> is a radix, i.e. greater than or equal to 2. Throws an exception if not.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Natural_number"/></para>
    /// </summary>
    /// <typeparam name="TInteger"></typeparam>
    /// <param name="radix"></param>
    /// <param name="paramName"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static TInteger AssertRadix<TInteger>(this TInteger radix, TInteger upperLimit, string? paramName = null)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      => IsRadix(radix, upperLimit) ? radix : throw new System.ArgumentOutOfRangeException(paramName ?? nameof(radix), $"Must be greater than or equal to 2 or less than or equal to {upperLimit}.");

    /// <summary>
    /// <para>Returns whether a <paramref name="radix"/> is valid, i.e. greater than or equal to 2.</para>
    /// </summary>
    /// <typeparam name="TInteger"></typeparam>
    /// <param name="radix"></param>
    /// <returns></returns>
    public static bool IsRadix<TInteger>(this TInteger radix)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      => radix > TInteger.One && radix <= TInteger.CreateChecked(byte.MaxValue);

    /// <summary>
    /// <para>Returns whether a <paramref name="radix"/> is valid, i.e. greater than or equal to 2 and less than or equal to <paramref name="upperLimit"/>.</para>
    /// </summary>
    /// <typeparam name="TInteger"></typeparam>
    /// <param name="radix"></param>
    /// <returns></returns>
    public static bool IsRadix<TInteger>(this TInteger radix, TInteger upperLimit)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      => radix > TInteger.One && radix <= upperLimit;
  }
}
