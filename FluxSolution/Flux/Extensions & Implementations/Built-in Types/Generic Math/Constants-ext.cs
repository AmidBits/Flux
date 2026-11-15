namespace Flux
{
  /// <summary>
  /// <para>The values in this static class represent various constants for types implementing <see cref="System.Numerics.IFloatingPoint{TSelf}"/>.</para>
  /// </summary>
  /// <typeparam name="TFloat"></typeparam>
  public static partial class FloatingPointConstants<TFloat>
    where TFloat : System.Numerics.IFloatingPoint<TFloat>
  {
    #region Floating Points

    /// <summary>Yields the value cbrt(2), Delian constant</summary>
    public static readonly TFloat Cbrt2 = TFloat.CreateChecked(1.2599210498948731647672106072782283505702514647015m);

    /// <summary>Yields the value  cbrt(3)</summary>
    public static readonly TFloat Cbrt3 = TFloat.CreateChecked(1.44224957030740838232163831078010958839186925349935m);

    /// <summary>Yields the value 0.577.., Euler's constant</summary>
    public static readonly TFloat EulersConstant = TFloat.CreateChecked(0.57721566490153286060651209008240243m);

    /// <summary>Yields the 1.618.., Golden ratio</summary>
    public static readonly TFloat GoldenRatio = TFloat.CreateChecked(1.6180339887498948482045868343656381177203091798057628621354486227052604628189024497072072041893911374m); // (TFloat.One + Sqrt5) / Two

    /// <summary>Yields the value 0.5</summary>
    public static readonly TFloat OneHalf = TFloat.One / NumberBaseConstants<TFloat>.Two;

    /// <summary>Yields the value Pi/2</summary>
    public static readonly TFloat PiOver2 = TFloat.Pi / NumberBaseConstants<TFloat>.Two;

    /// <summary>Yields the value Pi/4</summary>
    public static readonly TFloat PiOver4 = TFloat.Pi / NumberBaseConstants<TFloat>.Four;

    /// <summary>Yields the value sqrt(2), Pythagoras constant</summary>
    public static readonly TFloat Sqrt2 = TFloat.CreateChecked(1.414213562373095048801688724209698078569671875376948073176679737990732478462m);

    /// <summary>Yields the value sqrt(3), Theodorus' constant</summary>
    public static readonly TFloat Sqrt3 = TFloat.CreateChecked(1.732050807568877293527446341505872366942805253810380628055806979451933016909m);

    /// <summary>Yields the value sqrt(5)</summary>
    public static readonly TFloat Sqrt5 = TFloat.CreateChecked(2.236067977499789696409173668731276235440618359611525724270897m);

    /// <summary>Yields the value of rootn(2, 12), Twelfth root of 2</summary>
    public static readonly TFloat TwelfthRootOf2 = TFloat.CreateChecked(1.05946309435929526456182529494634170077920431749418562855921m);

    #endregion
  }

  //public static partial class NumberTypeInfo<T>
  //{
  //  extension<TNumber>(TNumber)
  //    where TNumber : System.Numerics.INumber<TNumber>
  //  {
  //  }
  //}

  /// <summary>
  /// <para>The values in this static class represent various constants for types implementing <see cref="System.Numerics.INumberBase{TSelf}"/>.</para>
  /// </summary>
  /// <typeparam name="TNumberBase"></typeparam>
  public static partial class NumberBaseConstants<TNumberBase>
    where TNumberBase : System.Numerics.INumberBase<TNumberBase>
  {
    #region Integers

    /// <summary>Yields the integer value 2.</summary>
    public static readonly TNumberBase Two = TNumberBase.CreateChecked(2);

    /// <summary>Yields the integer value 3.</summary>
    public static readonly TNumberBase Three = TNumberBase.CreateChecked(3);

    /// <summary>Yields the integer value 4.</summary>
    public static readonly TNumberBase Four = TNumberBase.CreateChecked(4);

    /// <summary>Yields the integer value 5.</summary>
    public static readonly TNumberBase Five = TNumberBase.CreateChecked(5);

    /// <summary>Yields the integer value 6.</summary>
    public static readonly TNumberBase Six = TNumberBase.CreateChecked(6);

    /// <summary>Yields the integer value 8.</summary>
    public static readonly TNumberBase Eight = TNumberBase.CreateChecked(8);

    /// <summary>Yields the integer value 9.</summary>
    public static readonly TNumberBase Nine = TNumberBase.CreateChecked(9);

    /// <summary>Yields the integer value 10.</summary>
    public static readonly TNumberBase Ten = TNumberBase.CreateChecked(10);

    /// <summary>Yields the integer value 63.</summary>
    public static readonly TNumberBase SixtyThree = TNumberBase.CreateChecked(63);

    #region LargestInteger/SmallestInteger

    public static readonly TNumberBase? LargestInteger
      = NumericTypeInfo<TNumberBase>.IsByte
      ? TNumberBase.CreateChecked(System.Byte.MaxValue)
      : NumericTypeInfo<TNumberBase>.IsInt16
      ? TNumberBase.CreateChecked(System.Int16.MaxValue)
      : NumericTypeInfo<TNumberBase>.IsInt32
      ? TNumberBase.CreateChecked(System.Int32.MaxValue)
      : NumericTypeInfo<TNumberBase>.IsInt64
      ? TNumberBase.CreateChecked(System.Int64.MaxValue)
      : NumericTypeInfo<TNumberBase>.IsInt128
      ? TNumberBase.CreateChecked(System.Int128.MaxValue)
      : NumericTypeInfo<TNumberBase>.IsSByte
      ? TNumberBase.CreateChecked(System.SByte.MaxValue)
      : NumericTypeInfo<TNumberBase>.IsUInt16
      ? TNumberBase.CreateChecked(System.UInt16.MaxValue)
      : NumericTypeInfo<TNumberBase>.IsUInt32
      ? TNumberBase.CreateChecked(System.UInt32.MaxValue)
      : NumericTypeInfo<TNumberBase>.IsUInt64
      ? TNumberBase.CreateChecked(System.UInt64.MaxValue)
      : NumericTypeInfo<TNumberBase>.IsUInt128
      ? TNumberBase.CreateChecked(System.UInt128.MaxValue)
      : NumericTypeInfo<TNumberBase>.IsHalf
      ? TNumberBase.CreateChecked(2048)
      : NumericTypeInfo<TNumberBase>.IsBinary32
      ? TNumberBase.CreateChecked(16777216)
      : NumericTypeInfo<TNumberBase>.IsBinary64
      ? TNumberBase.CreateChecked(9007199254740992)
      : NumericTypeInfo<TNumberBase>.IsBFloat16
      ? TNumberBase.CreateChecked(128)
      : NumericTypeInfo<TNumberBase>.IsDecimal
      ? TNumberBase.CreateChecked(79228162514264337593543950335m)
      : NumericTypeInfo<TNumberBase>.IsDecimal32
      ? TNumberBase.CreateChecked(System.Numerics.Decimal32.Parse("9999999"))
      : NumericTypeInfo<TNumberBase>.IsDecimal64
      ? TNumberBase.CreateChecked(System.Numerics.Decimal64.Parse("9999999999999999"))
      : NumericTypeInfo<TNumberBase>.IsDecimal128
      ? TNumberBase.CreateChecked(System.Numerics.Decimal128.Parse("9999999999999999999999999999999999"))
      : default;

    public static readonly TNumberBase? SmallestInteger
      = NumericTypeInfo<TNumberBase>.IsUnsignedInteger
      ? TNumberBase.Zero
      : NumericTypeInfo<TNumberBase>.IsSignedInteger
      ? -LargestInteger! - TNumberBase.One
      : NumericTypeInfo<TNumberBase>.ImplementsIFloatingPoint
      ? -LargestInteger!
      : default;

    #endregion

    #region LargestPrime/SmallestPrime

    /// <summary>
    /// <para>Gets the largest prime number exactly representable by <typeparamref name="TNumberBase"/></para>
    /// </summary>
    public static readonly TNumberBase? LargestPrime
      = NumericTypeInfo<TNumberBase>.IsByte
      ? TNumberBase.CreateChecked(251)
      : NumericTypeInfo<TNumberBase>.IsInt16
      ? TNumberBase.CreateChecked(32749)
      : NumericTypeInfo<TNumberBase>.IsInt32
      ? TNumberBase.CreateChecked(System.Int32.MaxValue) // int.MaxValue is a prime
      : NumericTypeInfo<TNumberBase>.IsInt64
      ? TNumberBase.CreateChecked(9223372036854775783)
      : NumericTypeInfo<TNumberBase>.IsInt128
      ? TNumberBase.CreateChecked(System.Int128.MaxValue) // Int128.MaxValue is a prime
      : NumericTypeInfo<TNumberBase>.IsSByte
      ? TNumberBase.CreateChecked(System.SByte.MaxValue) // SByte.MaxValue is a prime
      : NumericTypeInfo<TNumberBase>.IsUInt16
      ? TNumberBase.CreateChecked(65521)
      : NumericTypeInfo<TNumberBase>.IsUInt32
      ? TNumberBase.CreateChecked(4294967291u)
      : NumericTypeInfo<TNumberBase>.IsUInt64
      ? TNumberBase.CreateChecked(18446744073709551557ul)
      : NumericTypeInfo<TNumberBase>.IsUInt128
      ? TNumberBase.CreateChecked(System.UInt128.MaxValue - 158)
      : NumericTypeInfo<TNumberBase>.IsHalf
      ? TNumberBase.CreateChecked(2039)
      : NumericTypeInfo<TNumberBase>.IsBinary32
      ? TNumberBase.CreateChecked(16777213)
      : NumericTypeInfo<TNumberBase>.IsBinary64
      ? TNumberBase.CreateChecked(9007199254740881ul)
      : NumericTypeInfo<TNumberBase>.IsBFloat16
      ? TNumberBase.CreateChecked(127)
      : NumericTypeInfo<TNumberBase>.IsDecimal
      ? TNumberBase.CreateChecked(79228162514264337593543950319m)
      : NumericTypeInfo<TNumberBase>.IsDecimal32
      ? TNumberBase.CreateChecked(System.Numerics.Decimal32.Parse("9999991"))
      : NumericTypeInfo<TNumberBase>.IsDecimal64
      ? TNumberBase.CreateChecked(System.Numerics.Decimal64.Parse("9999999999999937"))
      : NumericTypeInfo<TNumberBase>.IsDecimal128
      ? TNumberBase.CreateChecked(System.Numerics.Decimal128.Parse("9999999999999999999999999999999589"))
      : default;

    /// <summary>
    /// <para>Gets the smallest prime number exactly representable by <typeparamref name="TNumberBase"/>, which is 2.</para>
    /// </summary>
    public static readonly TNumberBase SmallestPrime = Two;

    #endregion

    #endregion
  }
}
