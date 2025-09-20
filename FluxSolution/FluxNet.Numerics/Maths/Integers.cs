namespace FluxNet.Numerics
{
  public static partial class Maths
  {
    /// <summary>
    /// <para>Asserts that a <paramref name="value"/> is a non-negative integer, i.e. greater than or equal to zero. Throws an exception if not.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Natural_number"/></para>
    /// </summary>
    /// <typeparam name="TInteger"></typeparam>
    /// <param name="value"></param>
    /// <param name="paramName"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static TInteger AssertNonNegativeInteger<TInteger>(this TInteger value, string? paramName = null)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      => IsNonNegativeInteger(value) ? value : throw new System.ArgumentOutOfRangeException(paramName ?? nameof(value), "Must be greater than or equal to zero.");

    /// <summary>
    /// <para>Returns whether a <paramref name="value"/> is a non-negative integer, i.e. greater than or equal to zero.</para>
    /// </summary>
    /// <typeparam name="TInteger"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    public static bool IsNonNegativeInteger<TInteger>(this TInteger value)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      => value >= TInteger.Zero;

    /// <summary>
    /// <para>Asserts that a <paramref name="value"/> is a non-positive integer, i.e. less than or equal to zero. Throws an exception if not.</para>
    /// </summary>
    /// <typeparam name="TInteger"></typeparam>
    /// <param name="value"></param>
    /// <param name="paramName"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static TInteger AssertNonPositiveInteger<TInteger>(this TInteger value, string? paramName = null)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      => IsNonPositiveInteger(value) ? value : throw new System.ArgumentOutOfRangeException(paramName ?? nameof(value), $"Must be less than or equal to zero.");

    /// <summary>
    /// <para>Returns whether a <paramref name="value"/> is a non-positive integer, i.e. less than or equal to zero.</para>
    /// </summary>
    /// <typeparam name="TInteger"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    public static bool IsNonPositiveInteger<TInteger>(this TInteger value)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      => value <= TInteger.Zero;

    /// <summary>
    /// <para>Asserts that a <paramref name="value"/> is a positive integer, i.e. greater than zero. Throws an exception if not.</para>
    /// </summary>
    /// <typeparam name="TInteger"></typeparam>
    /// <param name="value"></param>
    /// <param name="paramName"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static TInteger AssertPositiveInteger<TInteger>(this TInteger value, string? paramName = null)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      => IsPositiveInteger(value) ? value : throw new System.ArgumentOutOfRangeException(paramName ?? nameof(value), "Must be greater than zero.");

    /// <summary>
    /// <para>Returns whether a <paramref name="value"/> is a positive integer, i.e. greater than zero.</para>
    /// </summary>
    /// <typeparam name="TInteger"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    public static bool IsPositiveInteger<TInteger>(this TInteger value)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      => value > TInteger.Zero;
  }
}
