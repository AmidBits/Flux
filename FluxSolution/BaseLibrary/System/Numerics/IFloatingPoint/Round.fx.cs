namespace Flux
{
  public static partial class Fx
  {
    //public static TValue Round<TValue>(this TValue value, WholeRounding mode)
    //  where TValue : System.Numerics.IFloatingPoint<TValue>
    //  => mode switch
    //  {
    //    WholeRounding.AwayFromZero => RoundFullAwayFromZero(value),
    //    WholeRounding.TowardZero => RoundFullTowardZero(value),
    //    WholeRounding.ToNegativeInfinity => RoundFullToNegativeInfinity(value),
    //    WholeRounding.ToPositiveInfinity => RoundFullToPositiveInfinity(value),
    //    WholeRounding.ToPowOf2AwayFromZero => RoundToPowOfAwayFromZero(value, 2),
    //    WholeRounding.ToPowOf2TowardZero => RoundToPowOfTowardZero(value, 2),
    //    WholeRounding.ToPowOf10AwayFromZero => RoundToPowOfAwayFromZero(value, 10),
    //    WholeRounding.ToPowOf10TowardZero => RoundToPowOfTowardZero(value, 10),
    //    _ => value,
    //  };

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
        UniversalRounding.WholeAwayFromZero => Envelop(value),
        UniversalRounding.WholeTowardZero => TValue.Truncate(value),
        UniversalRounding.WholeToNegativeInfinity => TValue.Floor(value),
        UniversalRounding.WholeToPositiveInfinity => TValue.Ceiling(value),
        UniversalRounding.WholeToPowOf2AwayFromZero => RoundToPowOfAwayFromZero(value, 2),
        UniversalRounding.WholeToPowOf2TowardZero => RoundToPowOfTowardZero(value, 2),
        UniversalRounding.WholeToPowOf10AwayFromZero => RoundToPowOfAwayFromZero(value, 10),
        UniversalRounding.WholeToPowOf10TowardZero => RoundToPowOfTowardZero(value, 10),
        UniversalRounding.HalfToEven => TValue.Round(value, MidpointRounding.ToEven),
        UniversalRounding.HalfAwayFromZero => TValue.Round(value, MidpointRounding.AwayFromZero),
        UniversalRounding.HalfTowardZero => TValue.Round(value, MidpointRounding.ToZero),
        UniversalRounding.HalfToNegativeInfinity => TValue.Round(value, MidpointRounding.ToNegativeInfinity),
        UniversalRounding.HalfToPositiveInfinity => TValue.Round(value, MidpointRounding.ToPositiveInfinity),
        UniversalRounding.HalfToOdd => RoundHalfToOdd(value),
        _ => throw new System.ArgumentOutOfRangeException(mode.ToString()),
      };

    #region Halfway (additional midpoint) rounding

    ///// <summary>
    ///// <para>Common rounding: round half, bias: even.</para>
    ///// </summary>
    ///// <remarks>
    ///// <para><see cref="UniversalRounding.HalfToEven"/></para>
    ///// </remarks>
    //public static TValue RoundHalfToEven<TValue>(this TValue value)
    //  where TValue : System.Numerics.IFloatingPoint<TValue>
    //  => TValue.CreateChecked(0.5) is var half && TValue.Floor(value + half) is var xh && TValue.IsOddInteger(xh) && value - TValue.Floor(value) == half ? xh - TValue.One : xh;

    ///// <summary>
    ///// <para>Symmetric rounding: round half up, bias: away from zero.</para>
    ///// </summary>
    ///// <remarks>
    ///// <para><see cref="UniversalRounding.HalfAwayFromZero"/></para>
    ///// </remarks>
    //public static TValue RoundHalfAwayFromZero<TValue>(this TValue value)
    //  where TValue : System.Numerics.IFloatingPoint<TValue>
    //  => TValue.CopySign(TValue.Floor(TValue.Abs(value) + TValue.CreateChecked(0.5)), value);

    ///// <summary>
    ///// <para>Symmetric rounding: round half down, bias: towards zero.</para>
    ///// </summary>
    ///// <remarks>
    ///// <para><see cref="UniversalRounding.HalfTowardZero"/></para>
    ///// </remarks>
    //public static TValue RoundHalfTowardZero<TValue>(this TValue value)
    //  where TValue : System.Numerics.IFloatingPoint<TValue>
    //  => TValue.CopySign(TValue.Ceiling(TValue.Abs(value) - TValue.CreateChecked(0.5)), value);

    ///// <summary>
    ///// <para>Common rounding: round half down, bias: negative infinity.</para>
    ///// </summary>
    ///// <remarks>
    ///// <para><see cref="UniversalRounding.HalfToNegativeInfinity"/></para>
    ///// </remarks>
    //public static TValue RoundHalfToNegativeInfinity<TValue>(this TValue value)
    //  where TValue : System.Numerics.IFloatingPoint<TValue>
    //  => TValue.Ceiling(value - TValue.CreateChecked(0.5));

    ///// <summary>
    ///// <para>Common rounding: round half up, bias: positive infinity.</para>
    ///// </summary>
    ///// <remarks>
    ///// <para><see cref="UniversalRounding.HalfToPositiveInfinity"/></para>
    ///// </remarks>
    //public static TValue RoundHalfToPositiveInfinity<TValue>(this TValue value)
    //  where TValue : System.Numerics.IFloatingPoint<TValue>
    //  => TValue.Floor(value + TValue.CreateChecked(0.5));

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
