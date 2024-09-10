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
    public static TValue Round<TValue>(this TValue value, UniversalRounding mode)
      where TValue : System.Numerics.IFloatingPoint<TValue>
      => mode switch
      {
        UniversalRounding.FullAwayFromZero => RoundFullAwayFromZero(value),
        UniversalRounding.FullTowardZero => RoundFullTowardZero(value),
        UniversalRounding.FullToNegativeInfinity => RoundFullToNegativeInfinity(value),
        UniversalRounding.FullToPositiveInfinity => RoundFullToPositiveInfinity(value),
        UniversalRounding.HalfToEven => TValue.Round(value, MidpointRounding.ToEven),
        UniversalRounding.HalfAwayFromZero => TValue.Round(value, MidpointRounding.AwayFromZero),
        UniversalRounding.HalfTowardZero => TValue.Round(value, MidpointRounding.ToZero),
        UniversalRounding.HalfToNegativeInfinity => TValue.Round(value, MidpointRounding.ToNegativeInfinity),
        UniversalRounding.HalfToPositiveInfinity => TValue.Round(value, MidpointRounding.ToPositiveInfinity),
        UniversalRounding.HalfToOdd => RoundHalfToOdd(value),
        _ => throw new System.ArgumentOutOfRangeException(mode.ToString()),
      };

    #region Full rounding

    /// <summary>
    /// <para>Equivalent to the opposite effect of the Truncate() function (also <see cref="RoundTowardZero{TValue}(TValue)"/>).</para>
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <remarks>Unconditional rounding is applied to all values (unevaluated).</remarks>
    public static TValue RoundFullAwayFromZero<TValue>(this TValue value)
      where TValue : System.Numerics.IFloatingPoint<TValue>
      => TValue.IsNegative(value) ? TValue.Floor(value) : TValue.Ceiling(value);

    /// <summary>
    /// <para>Equivalent to the Truncate() function.</para>
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <remarks>Unconditional rounding is applied to all values (unevaluated).</remarks>
    public static TValue RoundFullTowardZero<TValue>(this TValue value)
      where TValue : System.Numerics.IFloatingPoint<TValue>
      => TValue.Truncate(value);

    /// <summary>
    /// <para>Equivalent to the Floor() function.</para>
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <remarks>Unconditional rounding is applied to all values (unevaluated).</remarks>
    public static TValue RoundFullToNegativeInfinity<TValue>(this TValue value)
      where TValue : System.Numerics.IFloatingPoint<TValue>
      => TValue.Floor(value);

    /// <summary>
    /// <para>Equivalent to the ceil()/Ceiling() function.</para>
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <remarks>Unconditional rounding is applied to all values (unevaluated).</remarks>
    public static TValue RoundFullToPositiveInfinity<TValue>(this TValue value)
      where TValue : System.Numerics.IFloatingPoint<TValue>
      => TValue.Ceiling(value);

    #endregion // Full rounding

    #region Halfway (additional midpoint) rounding

    /// <summary>
    /// <para>Common rounding: round half, bias: even.</para>
    /// </summary>
    /// <remarks>
    /// <para><see cref="UniversalRounding.HalfToEven"/></para>
    /// </remarks>
    public static TValue RoundHalfToEven<TValue>(this TValue value)
      where TValue : System.Numerics.IFloatingPoint<TValue>
      => TValue.CreateChecked(0.5) is var half && TValue.Floor(value + half) is var xh && TValue.IsOddInteger(xh) && value - TValue.Floor(value) == half ? xh - TValue.One : xh;

    /// <summary>
    /// <para>Symmetric rounding: round half up, bias: away from zero.</para>
    /// </summary>
    /// <remarks>
    /// <para><see cref="UniversalRounding.HalfAwayFromZero"/></para>
    /// </remarks>
    public static TValue RoundHalfAwayFromZero<TValue>(this TValue value)
      where TValue : System.Numerics.IFloatingPoint<TValue>
      => TValue.CopySign(TValue.Floor(TValue.Abs(value) + TValue.CreateChecked(0.5)), value);

    /// <summary>
    /// <para>Symmetric rounding: round half down, bias: towards zero.</para>
    /// </summary>
    /// <remarks>
    /// <para><see cref="UniversalRounding.HalfTowardZero"/></para>
    /// </remarks>
    public static TValue RoundHalfTowardZero<TValue>(this TValue value)
      where TValue : System.Numerics.IFloatingPoint<TValue>
      => TValue.CopySign(TValue.Ceiling(TValue.Abs(value) - TValue.CreateChecked(0.5)), value);

    /// <summary>
    /// <para>Common rounding: round half down, bias: negative infinity.</para>
    /// </summary>
    /// <remarks>
    /// <para><see cref="UniversalRounding.HalfToNegativeInfinity"/></para>
    /// </remarks>
    public static TValue RoundHalfToNegativeInfinity<TValue>(this TValue value)
      where TValue : System.Numerics.IFloatingPoint<TValue>
      => TValue.Ceiling(value - TValue.CreateChecked(0.5));

    /// <summary>
    /// <para>Common rounding: round half up, bias: positive infinity.</para>
    /// </summary>
    /// <remarks>
    /// <para><see cref="UniversalRounding.HalfToPositiveInfinity"/></para>
    /// </remarks>
    public static TValue RoundHalfToPositiveInfinity<TValue>(this TValue value)
      where TValue : System.Numerics.IFloatingPoint<TValue>
      => TValue.Floor(value + TValue.CreateChecked(0.5));

    /// <summary>
    /// <para>Common rounding: round half, bias: odd.</para>
    /// </summary>
    /// <remarks>
    /// <para><see cref="UniversalRounding.HalfToOdd"/></para>
    /// </remarks>
    public static TValue RoundHalfToOdd<TValue>(this TValue value)
      where TValue : System.Numerics.IFloatingPoint<TValue>
      => TValue.CreateChecked(0.5) is var half && TValue.Floor(value + half) is var xh && TValue.IsEvenInteger(xh) && value - TValue.Floor(value) == half ? xh - TValue.One : xh;

    #endregion // Halfway (additional midpoint) rounding
  }
}
