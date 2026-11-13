namespace Flux
{
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

    /// <summary>Yields the integer alue 10.</summary>
    public static readonly TNumberBase Ten = TNumberBase.CreateChecked(10);

    /// <summary>Yields the integer value 63.</summary>
    public static readonly TNumberBase SixtyThree = TNumberBase.CreateChecked(63);

    /// <summary>Yields the minimum prime number (2).</summary>
    public static readonly TNumberBase MinPrime = Two;

    /// <summary>Yields the maximum prime number.</summary>
    public static readonly TNumberBase MaxPrime
      = typeof(TNumberBase) == typeof(System.Byte)
      ? TNumberBase.CreateChecked(251)
      : typeof(TNumberBase) == typeof(System.Int128)
      ? TNumberBase.CreateChecked(System.Int128.Parse("170141183460469231731687303715884105727"))
      : typeof(TNumberBase) == typeof(System.Int16)
      ? TNumberBase.CreateChecked(32749)
      : typeof(TNumberBase) == typeof(System.Int32) || (typeof(TNumberBase) == typeof(System.IntPtr) && System.IntPtr.Size == 4)
      ? TNumberBase.CreateChecked(2147483647)
      : typeof(TNumberBase) == typeof(System.Int64) || (typeof(TNumberBase) == typeof(System.IntPtr) && System.IntPtr.Size == 8)
      ? TNumberBase.CreateChecked(9223372036854775783)
      : typeof(TNumberBase) == typeof(System.SByte)
      ? TNumberBase.CreateChecked(127)
      : typeof(TNumberBase) == typeof(System.UInt128)
      ? TNumberBase.CreateChecked(System.UInt128.Parse("340282366920938463463374607431768211297"))
      : typeof(TNumberBase) == typeof(System.UInt16)
      ? TNumberBase.CreateChecked(65521)
      : typeof(TNumberBase) == typeof(System.UInt32) || (typeof(TNumberBase) == typeof(System.UIntPtr) && System.UIntPtr.Size == 4)
      ? TNumberBase.CreateChecked(4294967291u)
      : typeof(TNumberBase) == typeof(System.UInt64) || (typeof(TNumberBase) == typeof(System.UIntPtr) && System.UIntPtr.Size == 8)
      ? TNumberBase.CreateChecked(18446744073709551557ul)
      : typeof(TNumberBase) == typeof(System.Decimal)
      ? TNumberBase.CreateChecked(79228162514264337593543950297m)
      : typeof(TNumberBase) == typeof(System.Double) || (typeof(TNumberBase) == typeof(System.Runtime.InteropServices.NFloat) && System.Runtime.InteropServices.NFloat.Size == 8)
      ? TNumberBase.CreateChecked(9007199254740881d)
      : typeof(TNumberBase) == typeof(System.Half)
      ? TNumberBase.CreateChecked(2039)
      : typeof(TNumberBase) == typeof(System.Single) || (typeof(TNumberBase) == typeof(System.Runtime.InteropServices.NFloat) && System.Runtime.InteropServices.NFloat.Size == 4)
      ? TNumberBase.CreateChecked(16777213)
#if NET11_0_OR_GREATER
      : typeof(TNumberBase) == typeof(System.Numerics.BFloat16)
      ? TNumberBase.CreateChecked(127)
      : typeof(TNumberBase) == typeof(System.Numerics.Decimal128)
      ? TNumberBase.CreateChecked(System.Numerics.Decimal128.Parse("9999999999999999999999999999999989"))
      : typeof(TNumberBase) == typeof(System.Numerics.Decimal32)
      ? TNumberBase.CreateChecked(System.Numerics.Decimal32.Parse("9999991"))
      : typeof(TNumberBase) == typeof(System.Numerics.Decimal64)
      ? TNumberBase.CreateChecked(System.Numerics.Decimal64.Parse("9999999999999989"))
#endif
      : MinPrime;

    #endregion
  }
}
