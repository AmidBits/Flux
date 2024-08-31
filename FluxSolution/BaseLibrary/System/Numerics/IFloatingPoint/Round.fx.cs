namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Returns the <paramref name="value"/> rounded according to the strategy <paramref name="mode"/>.</para>
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="value"></param>
    /// <param name="mode"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static TValue Round<TValue>(this TValue value, RoundingMode mode)
      where TValue : System.Numerics.IFloatingPoint<TValue>
      => mode switch // First, handle the unconditional rounding strategies.
      {
        RoundingMode.AwayFromZero => RoundAwayFromZero(value),
        RoundingMode.TowardsZero => RoundTowardZero(value),
        RoundingMode.ToPositiveInfinity => RoundToPositiveInfinity(value),
        RoundingMode.ToNegativeInfinity => RoundToNegativeInfinity(value),
        _ => mode switch  // Second, handle the halfway rounding strategies.
        {
          RoundingMode.HalfAwayFromZero => RoundHalfAwayFromZero(value),
          RoundingMode.HalfTowardZero => RoundHalfTowardZero(value),
          RoundingMode.HalfToEven => RoundHalfToEven(value),
          RoundingMode.HalfToNegativeInfinity => RoundHalfToNegativeInfinity(value),
          RoundingMode.HalfToOdd => RoundHalfToOdd(value),
          RoundingMode.HalfToPositiveInfinity => RoundHalfToPositiveInfinity(value),
          _ => throw new System.ArgumentOutOfRangeException(mode.ToString()),
        }
      };

    #region Halfway rounding

    /// <summary>
    /// <para>Symmetric rounding: round half up, bias: away from zero.</para>
    /// </summary>
    /// <remarks>
    /// <para><see cref="RoundingMode.HalfAwayFromZero"/></para>
    /// </remarks>
    public static TValue RoundHalfAwayFromZero<TValue>(this TValue value)
      where TValue : System.Numerics.IFloatingPoint<TValue>
      => TValue.CopySign(RoundHalfToPositiveInfinity(TValue.Abs(value)), value);

    /// <summary>
    /// <para>Symmetric rounding: round half down, bias: towards zero.</para>
    /// </summary>
    /// <remarks>
    /// <para><see cref="RoundingMode.HalfTowardZero"/></para>
    /// </remarks>
    public static TValue RoundHalfTowardZero<TValue>(this TValue value)
      where TValue : System.Numerics.IFloatingPoint<TValue>
      => TValue.CopySign(RoundHalfToNegativeInfinity(TValue.Abs(value)), value);

    /// <summary>
    /// <para>Common rounding: round half, bias: even.</para>
    /// </summary>
    /// <remarks>
    /// <para><see cref="RoundingMode.HalfToEven"/></para>
    /// </remarks>
    public static TValue RoundHalfToEven<TValue>(this TValue value)
      where TValue : System.Numerics.IFloatingPoint<TValue>
      => TValue.CreateChecked(0.5) is var half && TValue.Floor(value + half) is var xh && TValue.IsOddInteger(xh) && value - TValue.Floor(value) == half ? xh - TValue.One : xh;

    /// <summary>
    /// <para>Common rounding: round half down, bias: negative infinity.</para>
    /// </summary>
    /// <remarks>
    /// <para><see cref="RoundingMode.HalfToNegativeInfinity"/></para>
    /// </remarks>
    public static TValue RoundHalfToNegativeInfinity<TValue>(this TValue value)
      where TValue : System.Numerics.IFloatingPoint<TValue>
      => TValue.Ceiling(value - TValue.CreateChecked(0.5));

    /// <summary>
    /// <para>Common rounding: round half, bias: odd.</para>
    /// </summary>
    /// <remarks>
    /// <para><see cref="RoundingMode.HalfToOdd"/></para>
    /// </remarks>
    public static TValue RoundHalfToOdd<TValue>(this TValue value)
      where TValue : System.Numerics.IFloatingPoint<TValue>
      => TValue.CreateChecked(0.5) is var half && TValue.Floor(value + half) is var xh && TValue.IsEvenInteger(xh) && value - TValue.Floor(value) == half ? xh - TValue.One : xh;

    /// <summary>
    /// <para>Common rounding: round half up, bias: positive infinity.</para>
    /// </summary>
    /// <remarks>
    /// <para><see cref="RoundingMode.HalfToPositiveInfinity"/></para>
    /// </remarks>
    public static TValue RoundHalfToPositiveInfinity<TValue>(this TValue value)
      where TValue : System.Numerics.IFloatingPoint<TValue>
      => TValue.Floor(value + TValue.CreateChecked(0.5));

    #endregion // Halfway rounding

    #region Unconditional rounding

    /// <summary>
    /// <para>Equivalent to the opposite effect of the Truncate() function (also <see cref="RoundTowardZero{TValue}(TValue)"/>).</para>
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <remarks>Unconditional rounding is applied to all values (unevaluated).</remarks>
    public static TValue RoundAwayFromZero<TValue>(this TValue value)
      where TValue : System.Numerics.IFloatingPoint<TValue>
      => TValue.IsNegative(value) ? TValue.Floor(value) : TValue.Ceiling(value);

    /// <summary>
    /// <para>Equivalent to the Truncate() function.</para>
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <remarks>Unconditional rounding is applied to all values (unevaluated).</remarks>
    public static TValue RoundTowardZero<TValue>(this TValue value)
      where TValue : System.Numerics.IFloatingPoint<TValue>
      => TValue.Truncate(value);

    /// <summary>
    /// <para>Equivalent to the Floor() function.</para>
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <remarks>Unconditional rounding is applied to all values (unevaluated).</remarks>
    public static TValue RoundToNegativeInfinity<TValue>(this TValue value)
      where TValue : System.Numerics.IFloatingPoint<TValue>
      => TValue.Floor(value);

    /// <summary>
    /// <para>Equivalent to the ceil()/Ceiling() function.</para>
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <remarks>Unconditional rounding is applied to all values (unevaluated).</remarks>
    public static TValue RoundToPositiveInfinity<TValue>(this TValue value)
      where TValue : System.Numerics.IFloatingPoint<TValue>
      => TValue.Ceiling(value);

    #endregion // Unconditional rounding
  }
}
