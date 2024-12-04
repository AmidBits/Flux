//namespace Flux
//{
//  public static partial class Fx
//  {
//    ///// <summary>
//    ///// <para>Rounds a value to the nearest integer, resolving halfway cases using the specified rounding <paramref name="mode"/>.</para>
//    ///// </summary>
//    ///// <typeparam name="TValue"></typeparam>
//    ///// <param name="value"></param>
//    ///// <param name="mode"></param>
//    ///// <returns></returns>
//    ///// <exception cref="System.ArgumentOutOfRangeException"></exception>
//    //public static TValue Round<TValue>(this TValue value, HalfRounding mode)
//    //  where TValue : System.Numerics.IFloatingPoint<TValue>
//    //  => mode switch
//    //  {
//    //    HalfRounding.ToEven or
//    //    HalfRounding.AwayFromZero or
//    //    HalfRounding.TowardZero or
//    //    HalfRounding.ToNegativeInfinity or
//    //    HalfRounding.ToPositiveInfinity => TValue.Round(value, (MidpointRounding)(int)mode),
//    //    HalfRounding.ToRandom => RoundHalfToRandom(value),
//    //    HalfRounding.Alternating => RoundHalfAlternating(value),
//    //    HalfRounding.ToOdd => RoundHalfToOdd(value),
//    //    _ => throw new System.ArgumentOutOfRangeException(mode.ToString()),
//    //  };

//    ///// <summary>
//    ///// <para>Returns the <paramref name="value"/> rounded according to the strategy <paramref name="mode"/>.</para>
//    ///// </summary>
//    ///// <typeparam name="TValue"></typeparam>
//    ///// <param name="value"></param>
//    ///// <param name="mode"></param>
//    ///// <returns></returns>
//    ///// <exception cref="System.ArgumentOutOfRangeException"></exception>
//    //public static TValue Round<TValue>(this TValue value, UniversalRounding mode)
//    //  where TValue : System.Numerics.IFloatingPoint<TValue>
//    //  => mode switch
//    //  {
//    //    UniversalRounding.WholeToEven => throw new NotImplementedException(),
//    //    UniversalRounding.WholeAwayFromZero => Envelop(value),
//    //    UniversalRounding.WholeTowardZero => TValue.Truncate(value),
//    //    UniversalRounding.WholeToNegativeInfinity => TValue.Floor(value),
//    //    UniversalRounding.WholeToPositiveInfinity => TValue.Ceiling(value),
//    //    UniversalRounding.WholeToRandom => value.RoundToNearestRandom(TValue.Floor(value), TValue.Ceiling(value)),
//    //    UniversalRounding.WholeAlternating => value.RoundToNearestAlternating(TValue.Floor(value), TValue.Ceiling(value)),
//    //    UniversalRounding.WholeToOdd => value.RoundWholeToOdd(),
//    //    UniversalRounding.HalfToEven or
//    //    UniversalRounding.HalfAwayFromZero or
//    //    UniversalRounding.HalfTowardZero or
//    //    UniversalRounding.HalfToNegativeInfinity or
//    //    UniversalRounding.HalfToPositiveInfinity => TValue.Round(value, (MidpointRounding)(int)mode),
//    //    UniversalRounding.HalfToRandom => value.RoundHalfRandom(),
//    //    UniversalRounding.HalfAlternating => value.RoundHalfAlternating(),
//    //    UniversalRounding.HalfToOdd => value.RoundHalfToOdd(),
//    //    _ => throw new System.ArgumentOutOfRangeException(mode.ToString()), // value.Round((HalfRounding)(int)mode),
//    //  };

//    #region Halfway (additional midpoint) rounding (use built-in instead)

//    ///// <summary>
//    ///// <para>Common rounding: round half, bias: even.</para>
//    ///// </summary>
//    ///// <remarks>
//    ///// <para><see cref="UniversalRounding.HalfToEven"/></para>
//    ///// </remarks>
//    //public static TValue RoundHalfToEven<TValue>(this TValue value)
//    //  where TValue : System.Numerics.IFloatingPoint<TValue>
//    //  => TValue.CreateChecked(0.5) is var half && TValue.Floor(value + half) is var xh && TValue.IsOddInteger(xh) && value - TValue.Floor(value) == half ? xh - TValue.One : xh;

//    ///// <summary>
//    ///// <para>Symmetric rounding: round half up, bias: away from zero.</para>
//    ///// </summary>
//    ///// <remarks>
//    ///// <para><see cref="UniversalRounding.HalfAwayFromZero"/></para>
//    ///// </remarks>
//    //public static TValue RoundHalfAwayFromZero<TValue>(this TValue value)
//    //  where TValue : System.Numerics.IFloatingPoint<TValue>
//    //  => TValue.CopySign(TValue.Floor(TValue.Abs(value) + TValue.CreateChecked(0.5)), value);

//    ///// <summary>
//    ///// <para>Symmetric rounding: round half down, bias: towards zero.</para>
//    ///// </summary>
//    ///// <remarks>
//    ///// <para><see cref="UniversalRounding.HalfTowardZero"/></para>
//    ///// </remarks>
//    //public static TValue RoundHalfTowardZero<TValue>(this TValue value)
//    //  where TValue : System.Numerics.IFloatingPoint<TValue>
//    //  => TValue.CopySign(TValue.Ceiling(TValue.Abs(value) - TValue.CreateChecked(0.5)), value);

//    ///// <summary>
//    ///// <para>Common rounding: round half down, bias: negative infinity.</para>
//    ///// </summary>
//    ///// <remarks>
//    ///// <para><see cref="UniversalRounding.HalfToNegativeInfinity"/></para>
//    ///// </remarks>
//    //public static TValue RoundHalfToNegativeInfinity<TValue>(this TValue value)
//    //  where TValue : System.Numerics.IFloatingPoint<TValue>
//    //  => TValue.Ceiling(value - TValue.CreateChecked(0.5));

//    ///// <summary>
//    ///// <para>Common rounding: round half up, bias: positive infinity.</para>
//    ///// </summary>
//    ///// <remarks>
//    ///// <para><see cref="UniversalRounding.HalfToPositiveInfinity"/></para>
//    ///// </remarks>
//    //public static TValue RoundHalfToPositiveInfinity<TValue>(this TValue value)
//    //  where TValue : System.Numerics.IFloatingPoint<TValue>
//    //  => TValue.Floor(value + TValue.CreateChecked(0.5));

//    #endregion // Halfway (additional midpoint) rounding (use built-in instead)

//    ///// <summary>
//    ///// <para></para>
//    ///// </summary>
//    ///// <remarks>
//    ///// <para><see cref="HalfRounding.HalfToRandom"/></para>
//    ///// </remarks>
//    ///// <typeparam name="TValue"></typeparam>
//    ///// <param name="value"></param>
//    ///// <param name="rng"></param>
//    ///// <returns></returns>
//    //public static TValue RoundHalfRandom<TValue>(this TValue value, System.Random? rng = null)
//    //  where TValue : System.Numerics.IFloatingPoint<TValue>
//    //  => value.CompareToFractionMidpoint() is var comparison
//    //  && (TValue.Floor(value) is var floor && comparison <= -1)
//    //  ? floor
//    //  : (TValue.Ceiling(value) is var ceiling && comparison >= 1)
//    //  ? ceiling
//    //  : value.RoundToNearestRandom(floor, ceiling);

//    ///// <summary>
//    ///// <para></para>
//    ///// </summary>
//    ///// <typeparam name="TValue"></typeparam>
//    ///// <param name="value"></param>
//    ///// <returns></returns>
//    //public static TValue RoundHalfAlternating<TValue>(this TValue value)
//    //  where TValue : System.Numerics.IFloatingPoint<TValue>
//    //  => value.CompareToFractionMidpoint() is var cmp
//    //  && (TValue.Floor(value) is var floor && cmp <= -1)
//    //  ? floor
//    //  : (TValue.Ceiling(value) is var ceiling && cmp >= 1)
//    //  ? ceiling
//    //  : value.RoundToNearestAlternating(floor, ceiling);

//    ///// <summary>
//    ///// <para>Common rounding: round half, bias: odd.</para>
//    ///// </summary>
//    ///// <remarks>
//    ///// <para><see cref="HalfRounding.HalfToOdd"/></para>
//    ///// </remarks>
//    ///// <typeparam name="TValue"></typeparam>
//    ///// <param name="value"></param>
//    ///// <returns></returns>
//    //public static TValue RoundHalfToOdd<TValue>(this TValue value)
//    //  where TValue : System.Numerics.IFloatingPoint<TValue>
//    //  => value.CompareToFractionMidpoint() is var cmp
//    //  && (cmp <= -1)
//    //  ? TValue.Floor(value)
//    //  : (cmp >= 1)
//    //  ? TValue.Ceiling(value)
//    //  : value.RoundWholeToOdd();

//    ///// <summary>
//    ///// <para></para>
//    ///// </summary>
//    ///// <typeparam name="TValue"></typeparam>
//    ///// <param name="value"></param>
//    ///// <returns></returns>
//    //public static TValue RoundWholeToEven<TValue>(this TValue value)
//    //  where TValue : System.Numerics.IFloatingPoint<TValue>
//    //  => TValue.Floor(value) is var floor && TValue.IsEvenInteger(floor)
//    //  ? floor
//    //  : TValue.Ceiling(value);

//    ///// <summary>
//    ///// <para></para>
//    ///// </summary>
//    ///// <typeparam name="TValue"></typeparam>
//    ///// <param name="value"></param>
//    ///// <returns></returns>
//    //public static TValue RoundWholeToOdd<TValue>(this TValue value)
//    //  where TValue : System.Numerics.IFloatingPoint<TValue>
//    //  => TValue.Floor(value) is var floor && TValue.IsOddInteger(floor)
//    //  ? floor
//    //  : TValue.Ceiling(value);
//  }
//}
