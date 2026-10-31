namespace Flux
{
  public static partial class FloatingPointConstants<TFloat>
    where TFloat : System.Numerics.IFloatingPoint<TFloat>
  {
    /// <summary>Cube root of 2, Delian constant</summary>
    public static readonly TFloat Cbrt2 = TFloat.CreateChecked(1.2599210498948731647672106072782283505702514647015m);

    /// <summary>Cube root of 3</summary>
    public static readonly TFloat Cbrt3 = TFloat.CreateChecked(1.44224957030740838232163831078010958839186925349935m);

    /// <summary>Euler's constant</summary>
    public static readonly TFloat EulersConstant = TFloat.CreateChecked(0.57721566490153286060651209008240243m);

    /// <summary>Golden ratio</summary>
    public static readonly TFloat GoldenRatio = TFloat.CreateChecked(1.6180339887498948482045868343656381177203091798057628621354486227052604628189024497072072041893911374m); // (TFloat.One + Sqrt5) / Two

    /// <summary>One half</summary>
    public static readonly TFloat OneHalf = TFloat.One / NumericConstants<TFloat>.Two;

    /// <summary>PI / 2</summary>
    public static readonly TFloat PiOver2 = TFloat.Pi / NumericConstants<TFloat>.Two;

    /// <summary>PI / 4</summary>
    public static readonly TFloat PiOver4 = TFloat.Pi / NumericConstants<TFloat>.Four;

    /// <summary>Square root of 2, Pythagoras constant</summary>
    public static readonly TFloat Sqrt2 = TFloat.CreateChecked(1.414213562373095048801688724209698078569671875376948073176679737990732478462m);

    /// <summary>Square root of 3, Theodorus' constant</summary>
    public static readonly TFloat Sqrt3 = TFloat.CreateChecked(1.732050807568877293527446341505872366942805253810380628055806979451933016909m);

    /// <summary>Square root of 5</summary>
    public static readonly TFloat Sqrt5 = TFloat.CreateChecked(2.236067977499789696409173668731276235440618359611525724270897m);

    /// <summary>Twelfth root of 2</summary>
    public static readonly TFloat TwelfthRootOf2 = TFloat.CreateChecked(1.05946309435929526456182529494634170077920431749418562855921m);
  }

  public static partial class NumericConstants<TNumberBase>
    where TNumberBase : System.Numerics.INumberBase<TNumberBase>
  {
    #region Counting numbers

    public static readonly TNumberBase Two = TNumberBase.CreateChecked(2);
    public static readonly TNumberBase Three = TNumberBase.CreateChecked(3);
    public static readonly TNumberBase Four = TNumberBase.CreateChecked(4);
    public static readonly TNumberBase Five = TNumberBase.CreateChecked(5);
    public static readonly TNumberBase Six = TNumberBase.CreateChecked(6);
    public static readonly TNumberBase Eight = TNumberBase.CreateChecked(8);
    public static readonly TNumberBase Nine = TNumberBase.CreateChecked(9);
    public static readonly TNumberBase Ten = TNumberBase.CreateChecked(10);
    public static readonly TNumberBase SixtyThree = TNumberBase.CreateChecked(63);

    #endregion

    #region NumericCapabilties

    public static readonly NumericCapabilities NumericCapabilties = typeof(TNumberBase).GetNumericCapabilitiesWithCache();

    public static readonly bool IsIBinaryFloatingPointIeee754 = System.Enum.HasAnyFlags(NumericCapabilties, NumericCapabilities.IBinaryFloatingPointIeee754);
    public static readonly bool IsIBinaryInteger = System.Enum.HasAnyFlags(NumericCapabilties, NumericCapabilities.IBinaryInteger);
    public static readonly bool IsIBinaryNumber = System.Enum.HasAnyFlags(NumericCapabilties, NumericCapabilities.IBinaryNumber);
#if NET11_0_OR_GREATER
      public static readonly bool IsIDecimalFloatingPointIeee754() => System.Enum.HasAnyFlags(NumericCapabilties, NumericCapabilities.IDecimalFloatingPointIeee754);
#endif
    public static readonly bool IsIFloatingPoint = System.Enum.HasAnyFlags(NumericCapabilties, NumericCapabilities.IFloatingPoint);
    public static readonly bool IsIFloatingPointIeee754 = System.Enum.HasAnyFlags(NumericCapabilties, NumericCapabilities.IFloatingPointIeee754);
    public static readonly bool IsIMinMaxValue = System.Enum.HasAnyFlags(NumericCapabilties, NumericCapabilities.IMinMaxValue);
    public static readonly bool IsINumber = System.Enum.HasAnyFlags(NumericCapabilties, NumericCapabilities.INumber);
    public static readonly bool IsINumberBase = System.Enum.HasAnyFlags(NumericCapabilties, NumericCapabilities.INumberBase);
    public static readonly bool IsISignedNumber = System.Enum.HasAnyFlags(NumericCapabilties, NumericCapabilities.ISignedNumber);
    public static readonly bool IsIUnsignedNumber = System.Enum.HasAnyFlags(NumericCapabilties, NumericCapabilities.IUnsignedNumber);

    /// <summary>Represents an arbitrary-precision numeric type, such as <see cref="System.Numerics.BigInteger"/>.</summary>
    public static readonly bool IsArbitraryPrecision = System.Enum.HasAnyFlags(NumericCapabilties, NumericCapabilities.ArbitraryPrecision);

    /// <summary>Represents the <see cref="System.Numerics.BFloat16"/> type.</summary>
    public static readonly bool IsBFloat16 = false; // typeof(TNumberBase) == typeof(System.Numerics.BFloat16);

    /// <summary>Represents the <see cref="System.Numerics.BigInteger"/> type.</summary>
    public static readonly bool IsBigInteger = typeof(TNumberBase) == typeof(System.Numerics.BigInteger);

    /// <summary>Represents an IEEE binary32 floating-point type, such as <see cref="System.Single"/> or <see cref="System.Runtime.InteropServices.NFloat"/> when <see cref="System.Runtime.InteropServices.NFloat.Size"/> is 4.</summary>
    public static readonly bool IsBinary32 = typeof(TNumberBase) == typeof(System.Single) || (typeof(TNumberBase) == typeof(System.Runtime.InteropServices.NFloat) && System.Runtime.InteropServices.NFloat.Size == 4);

    /// <summary>Represents an IEEE binary64 floating-point type, such as <see cref="System.Double"/> or <see cref="System.Runtime.InteropServices.NFloat"/> when <see cref="System.Runtime.InteropServices.NFloat.Size"/> is equal to 8.</summary>
    public static readonly bool IsBinary64 = typeof(TNumberBase) == typeof(System.Double) || (typeof(TNumberBase) == typeof(System.Runtime.InteropServices.NFloat) && System.Runtime.InteropServices.NFloat.Size == 8);

    /// <summary>Represents the <see cref="System.Decimal"/> type.</summary>
    public static readonly bool IsDecimal = typeof(TNumberBase) == typeof(System.Decimal);

    /// <summary>Represents an IEEE decimal128 floating-point type, such as <see cref="System.Decimal128"/>.</summary>
    public static readonly bool IsDecimal128 = false; // typeof(TNumberBase) == typeof(System.Numerics.Decimal128);

    /// <summary>Represents an IEEE decimal32 floating-point type, such as <see cref="System.Decimal32"/>.</summary>
    public static readonly bool IsDecimal32 = false; // typeof(TNumberBase) == typeof(System.Numerics.Decimal32);

    /// <summary>Represents an IEEE decimal64 floating-point type, such as <see cref="System.Decimal64"/>.</summary>
    public static readonly bool IsDecimal64 = false; // typeof(TNumberBase) == typeof(System.Numerics.Decimal64);

    /// <summary>Represents a 128-bit integer type, such as <see cref="System.Int128"/>, or <see cref="System.UInt128"/>.</summary>
    public static readonly bool IsInteger128 = typeof(TNumberBase) == typeof(System.Int128) || typeof(TNumberBase) == typeof(System.UInt128);

    /// <summary>Represents a 16-bit integer type, such as <see cref="System.Int16"/>, or <see cref="System.UInt16"/>.</summary>
    public static readonly bool IsInteger16 = typeof(TNumberBase) == typeof(System.Int16) || typeof(TNumberBase) == typeof(System.UInt16);

    /// <summary>Represents a 32-bit integer type, such as <see cref="System.Int32"/>, <see cref="System.UInt32"/>, <see cref="System.IntPtr"/> (when <see cref="System.IntPtr.Size"/> is 4), or <see cref="System.UIntPtr"/> (when <see cref="System.UIntPtr.Size"/> is 4).</summary>
    public static readonly bool IsInteger32 = typeof(TNumberBase) == typeof(System.Int32) || typeof(TNumberBase) == typeof(System.UInt32) || (typeof(TNumberBase) == typeof(System.IntPtr) && System.IntPtr.Size == 4) || (typeof(TNumberBase) == typeof(System.UIntPtr) && System.UIntPtr.Size == 4);

    /// <summary>Represents a 64-bit integer type, such as <see cref="System.Int64"/>, <see cref="System.UInt64"/>, <see cref="System.IntPtr"/> (when <see cref="System.IntPtr.Size"/> is 8), or <see cref="System.UIntPtr"/> (when <see cref="System.UIntPtr.Size"/> is 8).</summary>
    public static readonly bool IsInteger64 = typeof(TNumberBase) == typeof(System.Int64) || typeof(TNumberBase) == typeof(System.UInt64) || (typeof(TNumberBase) == typeof(System.IntPtr) && System.IntPtr.Size == 8) || (typeof(TNumberBase) == typeof(System.UIntPtr) && System.UIntPtr.Size == 8);

    /// <summary>Represents an 8-bit integer type, such as <see cref="System.Byte"/>, or <see cref="System.SByte"/>.</summary>
    public static readonly bool IsInteger8 = typeof(TNumberBase) == typeof(System.Byte) || typeof(TNumberBase) == typeof(System.SByte);

    /// <summary>Represents a signed integer type, such as <see cref="System.Int128"/>, <see cref="System.Int16"/>, <see cref="System.Int32"/>, <see cref="System.Int64"/>, <see cref="System.SByte"/>, or <see cref="System.Numerics.BigInteger"/>.</summary>
    public static readonly bool IsSignedInteger = System.Enum.HasAllFlags(NumericCapabilties, NumericCapabilities.IBinaryInteger | NumericCapabilities.ISignedNumber);

    /// <summary>Represents a fixed width numeric type, such as <see cref="System.Decimal"/>, <see cref="System.Int32"/>, or <see cref="System.Single"/>.</summary>
    public static readonly bool IsFixedWidth = !IsArbitraryPrecision;

    /// <summary>Represents an unsigned integer type, such as <see cref="System.Byte"/>, <see cref="System.UInt128"/>, <see cref="System.UInt16"/>, <see cref="System.UInt32"/>, or <see cref="System.UInt64"/>.</summary>
    public static readonly bool IsUnsignedInteger = System.Enum.HasAllFlags(NumericCapabilties, NumericCapabilities.IBinaryInteger | NumericCapabilities.IUnsignedNumber);

    public static readonly int BitWidth = IsInteger8 ? 8 : IsInteger16 ? 16 : IsBinary32 || IsDecimal32 || IsInteger32 ? 32 : IsBinary64 || IsDecimal64 || IsInteger64 ? 64 : IsDecimal128 || IsInteger128 ? 128 : -1;
    public static readonly int ByteWidth = IsInteger8 ? 1 : IsInteger16 ? 2 : IsBinary32 || IsDecimal32 || IsInteger32 ? 4 : IsBinary64 || IsDecimal64 || IsInteger64 ? 8 : IsDecimal128 || IsInteger128 ? 16 : -1;

    #endregion
  }

  public static partial class Number
  {
    extension<TNumber>(TNumber)
      where TNumber : System.Numerics.INumber<TNumber>
    {
      /// <summary>
      /// <para>Main classification number system:</para>
      /// <para>Integers (signed integers, i.e. includes negative numbers): "DOUBLE-STRUCK CAPITAL Z" = U+2124 = '&#x2124;'</para>
      /// <para>Natural numbers (unsigned integers, i.e. non-negative or positive integers, depending on convention): "DOUBLE-STRUCK CAPITAL N" = U+2115 = '&#x2115;'</para>
      /// <para>Complex numbers: "DOUBLE-STRUCK CAPITAL C" = U+2102 = '&#x2102;'</para>
      /// <para>Real numbers (floating point): "DOUBLE-STRUCK CAPITAL R" = U+211D = '&#x211D;'</para>
      /// </summary>
      public static char NumberSystemClassificationSymbol()
        => NumericConstants<TNumber>.IsSignedInteger ? '\u2124' // "DOUBLE-STRUCK CAPITAL Z" for integers (includes negative numbers).
        : NumericConstants<TNumber>.IsUnsignedInteger ? '\u2115' // "DOUBLE-STRUCK CAPITAL N" for natural numbers (non-negative or positive integers, depending on convention).
        : typeof(TNumber) == typeof(System.Numerics.Complex) ? '\u2102' // "DOUBLE-STRUCK CAPITAL C" for complex numbers.
        : NumericConstants<TNumber>.IsIFloatingPoint ? '\u211D' // "DOUBLE-STRUCK CAPITAL R" for real numbers.
        : throw new System.NotImplementedException($"{typeof(TNumber)}");
    }
  }
}
