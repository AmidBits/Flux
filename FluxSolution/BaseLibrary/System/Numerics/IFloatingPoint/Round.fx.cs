namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Returns the <paramref name="source"/> rounded according to the strategy <paramref name="mode"/>.</para>
    /// </summary>
    /// <typeparam name="TSelf"></typeparam>
    /// <param name="source"></param>
    /// <param name="mode"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static TSelf Round<TSelf>(this TSelf source, RoundingMode mode)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => mode switch // First, handle the direct rounding strategies.
      {
        RoundingMode.AwayFromZero => RoundAwayFromZero(source),
        RoundingMode.TowardsZero => RoundTowardZero(source),
        RoundingMode.ToPositiveInfinity => RoundToPositiveInfinity(source),
        RoundingMode.ToNegativeInfinity => RoundToNegativeInfinity(source),
        //RoundingMode.ToPowOf2AwayFromZero => TSelf.CreateChecked(RoundToPowOf2AwayFromZero(source)),
        //RoundingMode.ToPowOf2TowardZero => TSelf.CreateChecked(RoundToPowOf2TowardZero(source)),
        _ => mode switch  // Second, handle the halfway rounding strategies.
        {
          RoundingMode.HalfAwayFromZero => RoundHalfAwayFromZero(source),
          RoundingMode.HalfTowardZero => RoundHalfTowardZero(source),
          RoundingMode.HalfToEven => RoundHalfToEven(source),
          RoundingMode.HalfToNegativeInfinity => RoundHalfToNegativeInfinity(source),
          RoundingMode.HalfToOdd => RoundHalfToOdd(source),
          RoundingMode.HalfToPositiveInfinity => RoundHalfToPositiveInfinity(source),
          _ => throw new System.ArgumentOutOfRangeException(mode.ToString()),
        }
      };

    #region Halfway rounding functions

    /// <summary>
    /// <para>Common rounding: round half, bias: odd.</para>
    /// </summary>
    /// <remarks>
    /// <para><see cref="RoundingMode.HalfToOdd"/></para>
    /// </remarks>
    public static TSelf RoundHalfToOdd<TSelf>(this TSelf x)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => TSelf.CreateChecked(0.5) is var half && TSelf.Floor(x + half) is var xh && TSelf.IsEvenInteger(xh) && x - TSelf.Floor(x) == half ? xh - TSelf.One : xh;

    /// <summary>
    /// <para>Common rounding: round half, bias: even.</para>
    /// </summary>
    /// <remarks>
    /// <para><see cref="RoundingMode.HalfToEven"/></para>
    /// </remarks>
    public static TSelf RoundHalfToEven<TSelf>(this TSelf x)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => TSelf.CreateChecked(0.5) is var half && TSelf.Floor(x + half) is var xh && TSelf.IsOddInteger(xh) && x - TSelf.Floor(x) == half ? xh - TSelf.One : xh;

    /// <summary>
    /// <para>Symmetric rounding: round half up, bias: away from zero.</para>
    /// </summary>
    /// <remarks>
    /// <para><see cref="RoundingMode.HalfAwayFromZero"/></para>
    /// </remarks>
    public static TSelf RoundHalfAwayFromZero<TSelf>(this TSelf x)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => TSelf.CopySign(RoundHalfToPositiveInfinity(TSelf.Abs(x)), x);

    /// <summary>
    /// <para>Symmetric rounding: round half down, bias: towards zero.</para>
    /// </summary>
    /// <remarks>
    /// <para><see cref="RoundingMode.HalfTowardZero"/></para>
    /// </remarks>
    public static TSelf RoundHalfTowardZero<TSelf>(this TSelf x)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => TSelf.CopySign(RoundHalfToNegativeInfinity(TSelf.Abs(x)), x);

    /// <summary>
    /// <para>Common rounding: round half down, bias: negative infinity.</para>
    /// </summary>
    /// <remarks>
    /// <para><see cref="RoundingMode.HalfToNegativeInfinity"/></para>
    /// </remarks>
    public static TSelf RoundHalfToNegativeInfinity<TSelf>(this TSelf x)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => TSelf.Ceiling(x - TSelf.CreateChecked(0.5));

    /// <summary>
    /// <para>Common rounding: round half up, bias: positive infinity.</para>
    /// </summary>
    /// <remarks>
    /// <para><see cref="RoundingMode.HalfToPositiveInfinity"/></para>
    /// </remarks>
    public static TSelf RoundHalfToPositiveInfinity<TSelf>(this TSelf x)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => TSelf.Floor(x + TSelf.CreateChecked(0.5));

    #endregion Halfway rounding functions

    #region Unconditional rounding functions

    /// <summary>
    /// <para>Equivalent to the opposite effect of the Truncate() function (also <see cref="RoundTowardZero{TSelf}(TSelf)"/>).</para>
    /// </summary>
    public static TSelf RoundAwayFromZero<TSelf>(this TSelf x)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => TSelf.IsNegative(x) ? TSelf.Floor(x) : TSelf.Ceiling(x);

    /// <summary>
    /// <para>Equivalent to the Truncate() function.</para>
    /// </summary>
    public static TSelf RoundTowardZero<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => TSelf.Truncate(value);

    /// <summary>
    /// <para>Equivalent to the Floor() function.</para>
    /// </summary>
    public static TSelf RoundToNegativeInfinity<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => TSelf.Floor(value);

    /// <summary>
    /// <para>Equivalent to the ceil()/Ceiling() function.</para>
    /// </summary>
    public static TSelf RoundToPositiveInfinity<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => TSelf.Ceiling(value);

    #region Specialty

    /// <summary>
    /// <para></para>
    /// </summary>
    /// <typeparam name="TSelf"></typeparam>
    /// <param name="value"></param>
    /// <param name="multiple"></param>
    /// <param name="unequal"></param>
    /// <returns></returns>
    public static TSelf RoundToMultipleOfAwayFromZero<TSelf>(this TSelf value, TSelf multiple, bool unequal = false)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => MultipleOfAwayFromZero(value, multiple, unequal || !TSelf.IsInteger(value)); // TSelf.CopySign(multiple, value) is var msv && value - (value % multiple) is var motz && (motz != value || unequal) ? motz + msv : motz;

    /// <summary>
    /// <para></para>
    /// </summary>
    /// <typeparam name="TSelf"></typeparam>
    /// <param name="value"></param>
    /// <param name="multiple"></param>
    /// <param name="unequal"></param>
    /// <returns></returns>
    public static TSelf RoundToMultipleOfTowardZero<TSelf>(this TSelf value, TSelf multiple, bool unequal = false)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => MultipleOfTowardZero(value, multiple, unequal && TSelf.IsInteger(value)); // value - (value % multiple) is var motz && unequal && motz == value ? motz - TSelf.CopySign(multiple, value) : motz;

    /// <summary>
    /// <para></para>
    /// </summary>
    /// <typeparam name="TSelf"></typeparam>
    /// <param name="value"></param>
    /// <param name="unequal"></param>
    /// <returns></returns>
    public static TSelf RoundToPow2AwayFromZero<TSelf>(this TSelf value, bool unequal = false)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => TSelf.CreateChecked(System.Numerics.BigInteger.CreateChecked(value).Pow2AwayFromZero(unequal || !TSelf.IsInteger(value)));

    /// <summary>
    /// <para></para>
    /// </summary>
    /// <typeparam name="TSelf"></typeparam>
    /// <param name="value"></param>
    /// <param name="unequal"></param>
    /// <returns></returns>
    public static TSelf RoundToPow2TowardZero<TSelf>(this TSelf value, bool unequal = false)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => TSelf.CreateChecked(System.Numerics.BigInteger.CreateChecked(value).Pow2TowardZero(unequal && TSelf.IsInteger(value)));

    /// <summary>
    /// <para></para>
    /// </summary>
    /// <typeparam name="TSelf"></typeparam>
    /// <param name="value"></param>
    /// <param name="radix"></param>
    /// <param name="unequal"></param>
    /// <returns></returns>
    public static TSelf RoundToPowOfAwayFromZero<TSelf, TRadix>(this TSelf value, TRadix radix, bool unequal = false)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      => TSelf.CreateChecked(System.Numerics.BigInteger.CreateChecked(value).PowOfAwayFromZero(System.Numerics.BigInteger.CreateChecked(radix), unequal || !TSelf.IsInteger(value)));

    /// <summary>
    /// <para></para>
    /// </summary>
    /// <typeparam name="TSelf"></typeparam>
    /// <param name="value"></param>
    /// <param name="radix"></param>
    /// <param name="unequal"></param>
    /// <returns></returns>
    public static TSelf RoundToPowOfTowardZero<TSelf, TRadix>(this TSelf value, TRadix radix, bool unequal = false)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      => TSelf.CreateChecked(System.Numerics.BigInteger.CreateChecked(value).PowOfTowardZero(System.Numerics.BigInteger.CreateChecked(radix), unequal && TSelf.IsInteger(value)));

    #endregion // Specialty

    #endregion // Unconditional rounding functions


  }
}
