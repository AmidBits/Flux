namespace Flux
{
  public static class NumericTypeInfo<TNumberBase>
    where TNumberBase : System.Numerics.INumberBase<TNumberBase>
  {
    static NumericTypeInfo()
    {
      foreach (var itype in typeof(TNumberBase).GetCachedInterfaces()) // GetCachedInterfaces expands generic type descriptions.
      {
        #region Constants

        if (itype == typeof(System.Numerics.IFloatingPointConstants<>))
          Constants |= NumericConstants.IFloatingPointConstants;

        #endregion

        #region Core

        if (itype == typeof(System.Numerics.Complex<>))
          Core |= NumericCore.ComplexT;
        else if (itype == typeof(System.Numerics.IBinaryFloatingPointIeee754<>))
          Core |= NumericCore.IBinaryFloatingPointIeee754;
        else if (itype == typeof(System.Numerics.IBinaryInteger<>))
          Core |= NumericCore.IBinaryInteger;
        else if (itype == typeof(System.Numerics.IBinaryNumber<>))
          Core |= NumericCore.IBinaryNumber;
        else if (itype == typeof(System.Numerics.IDecimalFloatingPointIeee754<>))
          Core |= NumericCore.IDecimalFloatingPointIeee754;
        else if (itype == typeof(System.Numerics.IFloatingPoint<>))
          Core |= NumericCore.IFloatingPoint;
        else if (itype == typeof(System.Numerics.IFloatingPointIeee754<>))
          Core |= NumericCore.IFloatingPointIeee754;
        else if (itype == typeof(System.Numerics.IMinMaxValue<>))
          Core |= NumericCore.IMinMaxValue;
        else if (itype == typeof(System.Numerics.INumber<>))
          Core |= NumericCore.INumber;
        else if (itype == typeof(System.Numerics.INumberBase<>))
          Core |= NumericCore.INumberBase;
        else if (itype == typeof(System.Numerics.ISignedNumber<>))
          Core |= NumericCore.ISignedNumber;
        else if (itype == typeof(System.Numerics.IUnsignedNumber<>))
          Core |= NumericCore.IUnsignedNumber;

        #endregion

        #region Formattable

        if (itype == typeof(System.IFormattable))
          Formattable |= NumericFormattable.IFormattable;
        else if (itype == typeof(System.ISpanFormattable))
          Formattable |= NumericFormattable.ISpanFormattable;
        else if (itype == typeof(System.IUtf8SpanFormattable))
          Formattable |= NumericFormattable.IUtf8SpanFormattable;

        #endregion

        #region Functions

        if (itype == typeof(System.Numerics.IExponentialFunctions<>))
          Functions |= NumericFunctions.IExponentialFunctions;
        else if (itype == typeof(System.Numerics.IHyperbolicFunctions<>))
          Functions |= NumericFunctions.IHyperbolicFunctions;
        else if (itype == typeof(System.Numerics.ILogarithmicFunctions<>))
          Functions |= NumericFunctions.ILogarithmicFunctions;
        else if (itype == typeof(System.Numerics.IPowerFunctions<>))
          Functions |= NumericFunctions.IPowerFunctions;
        else if (itype == typeof(System.Numerics.IRootFunctions<>))
          Functions |= NumericFunctions.IRootFunctions;
        else if (itype == typeof(System.Numerics.ITrigonometricFunctions<>))
          Functions |= NumericFunctions.ITrigonometricFunctions;

        #endregion

        #region Identity

        if (itype == typeof(System.Numerics.IAdditiveIdentity<,>))
          Identity |= NumericIdentity.IAdditiveIdentity;
        else if (itype == typeof(System.Numerics.IMultiplicativeIdentity<,>))
          Identity |= NumericIdentity.IMultiplicativeIdentity;

        #endregion

        #region Operators

        if (itype == typeof(System.Numerics.IAdditionOperators<,,>))
          Operators |= NumericOperators.IAdditionOperators;
        else if (itype == typeof(System.Numerics.IBitwiseOperators<,,>))
          Operators |= NumericOperators.IBitwiseOperators;
        else if (itype == typeof(System.Numerics.IComparisonOperators<,,>))
          Operators |= NumericOperators.IComparisonOperators;
        else if (itype == typeof(System.Numerics.IDecrementOperators<>))
          Operators |= NumericOperators.IDecrementOperators;
        else if (itype == typeof(System.Numerics.IDivisionOperators<,,>))
          Operators |= NumericOperators.IDivisionOperators;
        else if (itype == typeof(System.Numerics.IEqualityOperators<,,>))
          Operators |= NumericOperators.IEqualityOperators;
        else if (itype == typeof(System.Numerics.IIncrementOperators<>))
          Operators |= NumericOperators.IIncrementOperators;
        else if (itype == typeof(System.Numerics.IModulusOperators<,,>))
          Operators |= NumericOperators.IModulusOperators;
        else if (itype == typeof(System.Numerics.IMultiplyOperators<,,>))
          Operators |= NumericOperators.IMultiplyOperators;
        else if (itype == typeof(System.Numerics.IShiftOperators<,,>))
          Operators |= NumericOperators.IShiftOperators;
        else if (itype == typeof(System.Numerics.ISubtractionOperators<,,>))
          Operators |= NumericOperators.ISubtractionOperators;
        else if (itype == typeof(System.Numerics.IUnaryNegationOperators<,>))
          Operators |= NumericOperators.IUnaryNegationOperators;
        else if (itype == typeof(System.Numerics.IUnaryPlusOperators<,>))
          Operators |= NumericOperators.IUnaryPlusOperators;

        #endregion

        #region Parsable

        if (itype == typeof(System.IParsable<>))
          Parsable |= NumericParsable.IParsable;
        else if (itype == typeof(System.ISpanParsable<>))
          Parsable |= NumericParsable.ISpanParsable;
        else if (itype == typeof(System.IUtf8SpanParsable<>))
          Parsable |= NumericParsable.IUtf8SpanParsable;

        #endregion
      }

      IsSignedInteger = ImplementsIBinaryInteger && ImplementsISignedNumber;
      IsUnsignedInteger = ImplementsIBinaryInteger && ImplementsIUnsignedNumber;

      IsArbitraryPrecision = IsBigInteger;
      IsFixedWidth = !IsArbitraryPrecision;
      StorageBytes = System.Runtime.CompilerServices.Unsafe.SizeOf<TNumberBase>();
      BitWidth = IsFixedWidth ? StorageBytes * 8 : null;
    }

    #region NumericConstants

    [System.Flags]
    public enum NumericConstants : long
    {
      NoConstants = 0,

      IFloatingPointConstants = 0x0000_0000_0000_0001,
    }

    public static readonly NumericConstants Constants;

    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IFloatingPointConstants{TSelf}"/>.</summary>
    public static bool ImplementsIFloatingPointConstants => (Constants & NumericConstants.IFloatingPointConstants) != 0;

    #endregion

    #region NumericCore

    [System.Flags]
    public enum NumericCore : long
    {
      NoCore = 0,

      ComplexT = 0x0000_0000_0000_0001,
      IBinaryFloatingPointIeee754 = 0x0000_0000_0000_0002,
      IBinaryInteger = 0x0000_0000_0000_0004,
      IBinaryNumber = 0x0000_0000_0000_0008,
      IDecimalFloatingPointIeee754 = 0x0000_0000_0000_0010,
      IFloatingPoint = 0x0000_0000_0000_0020,
      IFloatingPointIeee754 = 0x0000_0000_0000_0040,
      IMinMaxValue = 0x0000_0000_0000_0080,
      INumber = 0x0000_0000_0000_0100,
      INumberBase = 0x0000_0000_0000_0200,
      ISignedNumber = 0x0000_0000_0000_0400,
      IUnsignedNumber = 0x0000_0000_0000_0800,
    }

    public static readonly NumericCore Core;

    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IBinaryFloatingPointIeee754{TSelf}"/>.</summary>
    public static bool ImplementsIBinaryFloatingPointIeee754 => (Core & NumericCore.IBinaryFloatingPointIeee754) != 0;

    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IBinaryInteger{TSelf}"/>.</summary>
    public static bool ImplementsIBinaryInteger => (Core & NumericCore.IBinaryInteger) != 0;

    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IBinaryNumber{TSelf}"/>.</summary>
    public static bool ImplementsIBinaryNumber => (Core & NumericCore.IBinaryNumber) != 0;

    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IDecimalFloatingPointIeee754{TSelf}"/>.</summary>
    public static bool ImplementsIDecimalFloatingPointIeee754 => (Core & NumericCore.IDecimalFloatingPointIeee754) != 0;

    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IFloatingPoint{TSelf}"/>.</summary>
    public static bool ImplementsIFloatingPoint => (Core & NumericCore.IFloatingPoint) != 0;

    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IFloatingPointIeee754{TSelf}"/>.</summary>
    public static bool ImplementsIFloatingPointIeee754 => (Core & NumericCore.IFloatingPointIeee754) != 0;

    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IMinMaxValue{TSelf}"/>.</summary>
    public static bool ImplementsIMinMaxValue => (Core & NumericCore.IMinMaxValue) != 0;

    /// <summary>Indicates whether the type implements <see cref="System.Numerics.INumber{TSelf}"/>.</summary>
    public static bool ImplementsINumber => (Core & NumericCore.INumber) != 0;

    /// <summary>Indicates whether the type implements <see cref="System.Numerics.INumberBase{TSelf}"/>.</summary>
    public static bool ImplementsINumberBase => (Core & NumericCore.INumberBase) != 0;

    /// <summary>Indicates whether the type implements <see cref="System.Numerics.ISignedNumber{TSelf}"/>.</summary>
    public static bool ImplementsISignedNumber => (Core & NumericCore.ISignedNumber) != 0;

    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IUnsignedNumber{TSelf}"/>.</summary>
    public static bool ImplementsIUnsignedNumber => (Core & NumericCore.IUnsignedNumber) != 0;

    #endregion

    #region NumericFormattable

    [System.Flags]
    public enum NumericFormattable : long
    {
      NoFormattable = 0,

      IFormattable = 0x0000_0000_0000_0001,
      ISpanFormattable = 0x0000_0000_0000_0002,
      IUtf8SpanFormattable = 0x0000_0000_0000_0004,
    }

    public static readonly NumericFormattable Formattable;

    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IFormattable"/>.</summary>
    public static bool ImplementsIFormattable => (Formattable & NumericFormattable.IFormattable) != 0;

    /// <summary>Indicates whether the type implements <see cref="System.Numerics.ISpanFormattable"/>.</summary>
    public static bool ImplementsISpanFormattable => (Formattable & NumericFormattable.ISpanFormattable) != 0;

    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IUtf8SpanFormattable"/>.</summary>
    public static bool ImplementsIUtf8SpanFormattable => (Formattable & NumericFormattable.IUtf8SpanFormattable) != 0;

    #endregion

    #region NumericFunctions

    [System.Flags]
    public enum NumericFunctions : long
    {
      NoFunctions = 0,

      IExponentialFunctions = 0x0000_0000_0000_0001,
      IHyperbolicFunctions = 0x0000_0000_0000_0002,
      ILogarithmicFunctions = 0x0000_0000_0000_0004,
      IPowerFunctions = 0x0000_0000_0000_0008,
      IRootFunctions = 0x0000_0000_0000_0010,
      ITrigonometricFunctions = 0x0000_0000_0000_0020,
    }

    public static readonly NumericFunctions Functions;

    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IExponentialFunctions{TSelf}"/>.</summary>
    public static bool ImplementsIExponentialFunctions => (Functions & NumericFunctions.IExponentialFunctions) != 0;

    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IHyperbolicFunctions{TSelf}"/>.</summary>
    public static bool ImplementsIHyperbolicFunctions => (Functions & NumericFunctions.IHyperbolicFunctions) != 0;

    /// <summary>Indicates whether the type implements <see cref="System.Numerics.ILogarithmicFunctions{TSelf}"/>.</summary>
    public static bool ImplementsILogarithmicFunctions => (Functions & NumericFunctions.ILogarithmicFunctions) != 0;

    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IPowerFunctions{TSelf}"/>.</summary>
    public static bool ImplementsIPowerFunctions => (Functions & NumericFunctions.IPowerFunctions) != 0;

    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IRootFunctions{TSelf}"/>.</summary>
    public static bool ImplementsIRootFunctions => (Functions & NumericFunctions.IRootFunctions) != 0;

    /// <summary>Indicates whether the type implements <see cref="System.Numerics.ITrigonometricFunctions{TSelf}"/>.</summary>
    public static bool ImplementsITrigonometricFunctions => (Functions & NumericFunctions.ITrigonometricFunctions) != 0;

    #endregion

    #region NumericIdentity

    [System.Flags]
    public enum NumericIdentity : long
    {
      NoIdentity = 0,

      IAdditiveIdentity = 0x0000_0000_0000_0001,
      IMultiplicativeIdentity = 0x0000_0000_0000_0002,
    }

    public static readonly NumericIdentity Identity;

    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IAdditiveIdentity{TSelf, TResult}"/>.</summary>
    public static bool ImplementsIAdditiveIdentity => (Identity & NumericIdentity.IAdditiveIdentity) != 0;

    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IMultiplicativeIdentity{TSelf, TResult}"/>.</summary>
    public static bool ImplementsIMultiplicativeIdentity => (Identity & NumericIdentity.IMultiplicativeIdentity) != 0;

    #endregion

    #region NumericOperators

    [System.Flags]
    public enum NumericOperators : long
    {
      NoOperators = 0,

      IAdditionOperators = 0x0000_0000_0000_0001,
      IBitwiseOperators = 0x0000_0000_0000_0002,
      IComparisonOperators = 0x0000_0000_0000_0004,
      IDecrementOperators = 0x0000_0000_0000_0008,
      IDivisionOperators = 0x0000_0000_0000_0010,
      IEqualityOperators = 0x0000_0000_0000_0020,
      IIncrementOperators = 0x0000_0000_0000_0040,
      IModulusOperators = 0x0000_0000_0000_0080,
      IMultiplyOperators = 0x0000_0000_0000_0100,
      IShiftOperators = 0x0000_0000_0000_0200,
      ISubtractionOperators = 0x0000_0000_0000_0400,
      IUnaryNegationOperators = 0x0000_0000_0000_0800,
      IUnaryPlusOperators = 0x0000_0000_0000_1000,
    }

    public static readonly NumericOperators Operators;

    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IAdditionOperators{TSelf, TOther, TResult}"/>.</summary>
    public static bool ImplementsIAdditionOperators => (Operators & NumericOperators.IAdditionOperators) != 0;

    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IBitwiseOperators{TSelf, TOther, TResult}"/>.</summary>
    public static bool ImplementsIBitwiseOperators => (Operators & NumericOperators.IBitwiseOperators) != 0;

    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IComparisonOperators{TSelf, TOther, TResult}"/>.</summary>
    public static bool ImplementsIComparisonOperators => (Operators & NumericOperators.IComparisonOperators) != 0;

    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IDecrementOperators{TSelf}"/>.</summary>
    public static bool ImplementsIDecrementOperators => (Operators & NumericOperators.IDecrementOperators) != 0;

    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IDivisionOperators{TSelf, TOther, TResult}"/>.</summary>
    public static bool ImplementsIDivisionOperators => (Operators & NumericOperators.IDivisionOperators) != 0;

    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IEqualityOperators{TSelf, TOther, TResult}"/>.</summary>
    public static bool ImplementsIEqualityOperators => (Operators & NumericOperators.IEqualityOperators) != 0;

    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IIncrementOperators{TSelf}"/>.</summary>
    public static bool ImplementsIIncrementOperators => (Operators & NumericOperators.IIncrementOperators) != 0;

    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IModulusOperators{TSelf, TOther, TResult}"/>.</summary>
    public static bool ImplementsIModulusOperators => (Operators & NumericOperators.IModulusOperators) != 0;

    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IMultiplyOperators{TSelf, TOther, TResult}"/>.</summary>
    public static bool ImplementsIMultiplyOperators => (Operators & NumericOperators.IMultiplyOperators) != 0;

    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IShiftOperators{TSelf, TOther, TResult}"/>.</summary>
    public static bool ImplementsIShiftOperators => (Operators & NumericOperators.IShiftOperators) != 0;

    /// <summary>Indicates whether the type implements <see cref="System.Numerics.ISubtractionOperators{TSelf, TOther, TResult}"/>.</summary>
    public static bool ImplementsISubtractionOperators => (Operators & NumericOperators.ISubtractionOperators) != 0;

    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IUnaryNegationOperators{TSelf, TResult}"/>.</summary>
    public static bool ImplementsIUnaryNegationOperators => (Operators & NumericOperators.IUnaryNegationOperators) != 0;

    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IUnaryPlusOperators{TSelf, TResult}"/>.</summary>
    public static bool ImplementsIUnaryPlusOperators => (Operators & NumericOperators.IUnaryPlusOperators) != 0;

    #endregion

    #region NumericParsable

    [System.Flags]
    public enum NumericParsable : long
    {
      NoParsable = 0,

      IParsable = 0x0000_0000_0000_0001,
      ISpanParsable = 0x0000_0000_0000_0002,
      IUtf8SpanParsable = 0x0000_0000_0000_0004,
    }

    public static readonly NumericParsable Parsable;

    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IParsable{TSelf}"/>.</summary>
    public static bool ImplementsIParsable => (Parsable & NumericParsable.IParsable) != 0;

    /// <summary>Indicates whether the type implements <see cref="System.Numerics.ISpanParsable{TSelf}"/>.</summary>
    public static bool ImplementsISpanParsable => (Parsable & NumericParsable.ISpanParsable) != 0;

    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IUtf8SpanParsable{TSelf}"/>.</summary>
    public static bool ImplementsIUtf8SpanParsable => (Parsable & NumericParsable.IUtf8SpanParsable) != 0;

    #endregion

    #region Is.. Type Checks

    /// <summary>Indicates whether the type represents <see cref="System.Numerics.BFloat16"/>.</summary>
    public static readonly bool IsBFloat16 = typeof(TNumberBase) == typeof(System.Numerics.BFloat16);

    /// <summary>Indicates whether the type represents <see cref="System.Numerics.BigInteger"/>.</summary>
    public static readonly bool IsBigInteger = typeof(TNumberBase) == typeof(System.Numerics.BigInteger);

    /// <summary>Indicates whether the type represents <see cref="System.Numerics.Complex"/>.</summary>
    public static readonly bool IsComplex = typeof(TNumberBase) == typeof(System.Numerics.Complex);

    /// <summary>Indicates whether the type represents <see cref="System.Decimal"/>.</summary>
    public static readonly bool IsDecimal = typeof(TNumberBase) == typeof(System.Decimal);

    /// <summary>Indicates whether the type represents <see cref="Flux.Numerics.BigRational"/>.</summary>
    public static readonly bool IsBigRational = typeof(TNumberBase) == typeof(Flux.Numerics.BigRational);

    /// <summary>Indicates whether the type represents an IEEE binary16 floating-point, such as <see cref="System.Half"/></summary>
    public static readonly bool IsBinary16 = typeof(TNumberBase) == typeof(System.Half);

    /// <summary>Indicates whether the type represents an IEEE binary32 floating-point, such as <see cref="System.Single"/> or <see cref="System.Runtime.InteropServices.NFloat"/> when <see cref="System.Runtime.InteropServices.NFloat.Size"/> is 4.</summary>
    public static readonly bool IsBinary32 = typeof(TNumberBase) == typeof(System.Single) || (typeof(TNumberBase) == typeof(System.Runtime.InteropServices.NFloat) && System.Runtime.InteropServices.NFloat.Size == 4);

    /// <summary>Indicates whether the type represents an IEEE binary64 floating-point, such as <see cref="System.Double"/> or <see cref="System.Runtime.InteropServices.NFloat"/> when <see cref="System.Runtime.InteropServices.NFloat.Size"/> is 8.</summary>
    public static readonly bool IsBinary64 = typeof(TNumberBase) == typeof(System.Double) || (typeof(TNumberBase) == typeof(System.Runtime.InteropServices.NFloat) && System.Runtime.InteropServices.NFloat.Size == 8);

    /// <summary>Indicates whether the type represents an IEEE binary128 floating-point. There are currently no such types in .NET.</summary>
    public static readonly bool IsBinary128 = false;

    /// <summary>Indicates whether the type represents an IEEE decimal32 floating-point, such as <see cref="System.Decimal32"/>.</summary>
    public static readonly bool IsDecimal32 = typeof(TNumberBase) == typeof(System.Numerics.Decimal32);

    /// <summary>Indicates whether the type represents an IEEE decimal64 floating-point, such as <see cref="System.Decimal64"/>.</summary>
    public static readonly bool IsDecimal64 = typeof(TNumberBase) == typeof(System.Numerics.Decimal64);

    /// <summary>Indicates whether the type represents an IEEE decimal128 floating-point, such as <see cref="System.Decimal128"/>.</summary>
    public static readonly bool IsDecimal128 = typeof(TNumberBase) == typeof(System.Numerics.Decimal128);

    /// <summary>Indicates whether the type represents an 8-bit integer type, such as <see cref="System.Byte"/> or <see cref="System.SByte"/>.</summary>
    public static readonly bool IsInteger8 = typeof(TNumberBase) == typeof(System.Byte) || typeof(TNumberBase) == typeof(System.SByte);

    /// <summary>Indicates whether the type represents a 16-bit integer type, such as <see cref="System.Int16"/> or <see cref="System.UInt16"/>.</summary>
    public static readonly bool IsInteger16 = typeof(TNumberBase) == typeof(System.Int16) || typeof(TNumberBase) == typeof(System.UInt16);

    /// <summary>Indicates whether the type represents a 32-bit integer type, such as <see cref="System.Int32"/>, <see cref="System.UInt32"/>, <see cref="System.IntPtr"/> (when <see cref="System.IntPtr.Size"/> is 4), or <see cref="System.UIntPtr"/> (when <see cref="System.UIntPtr.Size"/> is 4).</summary>
    public static readonly bool IsInteger32 = typeof(TNumberBase) == typeof(System.Int32) || typeof(TNumberBase) == typeof(System.UInt32) || (typeof(TNumberBase) == typeof(System.IntPtr) && System.IntPtr.Size == 4) || (typeof(TNumberBase) == typeof(System.UIntPtr) && System.UIntPtr.Size == 4);

    /// <summary>Indicates whether the type represents a 64-bit integer type, such as <see cref="System.Int64"/>, <see cref="System.UInt64"/>, <see cref="System.IntPtr"/> (when <see cref="System.IntPtr.Size"/> is 8), or <see cref="System.UIntPtr"/> (when <see cref="System.UIntPtr.Size"/> is 8).</summary>
    public static readonly bool IsInteger64 = typeof(TNumberBase) == typeof(System.Int64) || typeof(TNumberBase) == typeof(System.UInt64) || (typeof(TNumberBase) == typeof(System.IntPtr) && System.IntPtr.Size == 8) || (typeof(TNumberBase) == typeof(System.UIntPtr) && System.UIntPtr.Size == 8);

    /// <summary>Indicates whether the type represents a 128-bit integer type, such as <see cref="System.Int128"/> or <see cref="System.UInt128"/>.</summary>
    public static readonly bool IsInteger128 = typeof(TNumberBase) == typeof(System.Int128) || typeof(TNumberBase) == typeof(System.UInt128);

    #endregion

    #region Additional Information

    /// <summary>The width of the type in bits.</summary>
    /// <remarks>For fixed-width types, this equals <see cref="StorageBytes"/> × 8. If <see cref="IsArbitraryPrecision"/> is <see langword="true"/>, this value is null, indicating an unbounded/not applicable bit-width.</remarks>
    public static readonly int? BitWidth;

    /// <summary>Indicates whether the type has an unbounded bit-width, such as <see cref="System.Numerics.BigInteger"/>.</summary>
    public static readonly bool IsArbitraryPrecision;

    /// <summary>Indicates whether the type has a fixed bit-width, such as <see cref="System.Decimal"/>, <see cref="System.Decimal128"/>, <see cref="System.Int32"/>, or <see cref="System.Single"/>.</summary>
    public static readonly bool IsFixedWidth;

    /// <summary>Indicates whether the type represents a signed integer, such as <see cref="System.Int128"/>, <see cref="System.Int16"/>, <see cref="System.Int32"/>, <see cref="System.Int64"/>, <see cref="System.SByte"/>, or <see cref="System.Numerics.BigInteger"/>.</summary>
    public static readonly bool IsSignedInteger;

    /// <summary>Indicates whether the type represents an unsigned integer , such as <see cref="System.Byte"/>, <see cref="System.UInt128"/>, <see cref="System.UInt16"/>, <see cref="System.UInt32"/>, or <see cref="System.UInt64"/>.</summary>
    public static readonly bool IsUnsignedInteger;

    /// <summary>The number of storage bytes used by the type.</summary>
    /// <remarks>This does not include implementation-dependent padding, object headers, or additional heap allocations (for example, <see cref="System.Numerics.BigInteger"/>).</remarks>
    public static readonly int StorageBytes;

    #endregion
  }

  //  public interface INumericTypeInfo
  //  {
  //    #region Implements

  //    #region Implements ..Constants Interfaces

  //    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IFloatingPointConstants{TSelf}"/>.</summary>
  //    public bool ImplementsIFloatingPointConstants { get; }

  //    #endregion

  //    #region Implements "Core" Interfaces

  //    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IBinaryFloatingPointIeee754{TSelf}"/>.</summary>
  //    public bool ImplementsIBinaryFloatingPointIeee754 { get; }

  //    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IBinaryInteger{TSelf}"/>.</summary>
  //    public bool ImplementsIBinaryInteger { get; }

  //    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IBinaryNumber{TSelf}"/>.</summary>
  //    public bool ImplementsIBinaryNumber { get; }

  //    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IDecimalFloatingPointIeee754{TSelf}"/>.</summary>
  //    public bool ImplementsIDecimalFloatingPointIeee754 { get; }

  //    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IFloatingPoint{TSelf}"/>.</summary>
  //    public bool ImplementsIFloatingPoint { get; }

  //    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IFloatingPointIeee754{TSelf}"/>.</summary>
  //    public bool ImplementsIFloatingPointIeee754 { get; }

  //    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IMinMaxValue{TSelf}"/>.</summary>
  //    public bool ImplementsIMinMaxValue { get; }

  //    /// <summary>Indicates whether the type implements <see cref="System.Numerics.INumber{TSelf}"/>.</summary>
  //    public bool ImplementsINumber { get; }

  //    /// <summary>Indicates whether the type implements <see cref="System.Numerics.INumberBase{TSelf}"/>.</summary>
  //    public bool ImplementsINumberBase { get; }

  //    /// <summary>Indicates whether the type implements <see cref="System.Numerics.ISignedNumber{TSelf}"/>.</summary>
  //    public bool ImplementsISignedNumber { get; }

  //    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IUnsignedNumber{TSelf}"/>.</summary>
  //    public bool ImplementsIUnsignedNumber { get; }

  //    #endregion

  //    #region Implements ..Formattable Interfaces

  //    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IFormattable"/>.</summary>
  //    public bool ImplementsIFormattable { get; }

  //    /// <summary>Indicates whether the type implements <see cref="System.Numerics.ISpanFormattable"/>.</summary>
  //    public bool ImplementsISpanFormattable { get; }

  //    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IUtf8SpanFormattable"/>.</summary>
  //    public bool ImplementsIUtf8SpanFormattable { get; }

  //    #endregion

  //    #region Implements ..Functions Interfaces

  //    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IExponentialFunctions{TSelf}"/>.</summary>
  //    public bool ImplementsIExponentialFunctions { get; }

  //    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IHyperbolicFunctions{TSelf}"/>.</summary>
  //    public bool ImplementsIHyperbolicFunctions { get; }

  //    /// <summary>Indicates whether the type implements <see cref="System.Numerics.ILogarithmicFunctions{TSelf}"/>.</summary>
  //    public bool ImplementsILogarithmicFunctions { get; }

  //    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IPowerFunctions{TSelf}"/>.</summary>
  //    public bool ImplementsIPowerFunctions { get; }

  //    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IRootFunctions{TSelf}"/>.</summary>
  //    public bool ImplementsIRootFunctions { get; }

  //    /// <summary>Indicates whether the type implements <see cref="System.Numerics.ITrigonometricFunctions{TSelf}"/>.</summary>
  //    public bool ImplementsITrigonometricFunctions { get; }

  //    #endregion

  //    #region Implements ..Identity Interfaces

  //    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IAdditiveIdentity{TSelf, TResult}"/>.</summary>
  //    public bool ImplementsIAdditiveIdentity { get; }

  //    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IMultiplicativeIdentity{TSelf, TResult}"/>.</summary>
  //    public bool ImplementsIMultiplicativeIdentity { get; }

  //    #endregion

  //    #region Implements ..Operators Interfaces

  //    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IAdditionOperators{TSelf, TOther, TResult}"/>.</summary>
  //    public bool ImplementsIAdditionOperators { get; }

  //    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IBitwiseOperators{TSelf, TOther, TResult}"/>.</summary>
  //    public bool ImplementsIBitwiseOperators { get; }

  //    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IComparisonOperators{TSelf, TOther, TResult}"/>.</summary>
  //    public bool ImplementsIComparisonOperators { get; }

  //    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IDecrementOperators{TSelf}"/>.</summary>
  //    public bool ImplementsIDecrementOperators { get; }

  //    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IDivisionOperators{TSelf, TOther, TResult}"/>.</summary>
  //    public bool ImplementsIDivisionOperators { get; }

  //    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IEqualityOperators{TSelf, TOther, TResult}"/>.</summary>
  //    public bool ImplementsIEqualityOperators { get; }

  //    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IIncrementOperators{TSelf}"/>.</summary>
  //    public bool ImplementsIIncrementOperators { get; }

  //    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IModulusOperators{TSelf, TOther, TResult}"/>.</summary>
  //    public bool ImplementsIModulusOperators { get; }

  //    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IMultiplyOperators{TSelf, TOther, TResult}"/>.</summary>
  //    public bool ImplementsIMultiplyOperators { get; }

  //    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IShiftOperators{TSelf, TOther, TResult}"/>.</summary>
  //    public bool ImplementsIShiftOperators { get; }

  //    /// <summary>Indicates whether the type implements <see cref="System.Numerics.ISubtractionOperators{TSelf, TOther, TResult}"/>.</summary>
  //    public bool ImplementsISubtractionOperators { get; }

  //    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IUnaryNegationOperators{TSelf, TResult}"/>.</summary>
  //    public bool ImplementsIUnaryNegationOperators { get; }

  //    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IUnaryPlusOperators{TSelf, TResult}"/>.</summary>
  //    public bool ImplementsIUnaryPlusOperators { get; }

  //    #endregion

  //    #region Implements ..Parsable Interfaces

  //    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IParsable{TSelf}"/>.</summary>
  //    public bool ImplementsIParsable { get; }

  //    /// <summary>Indicates whether the type implements <see cref="System.Numerics.ISpanParsable{TSelf}"/>.</summary>
  //    public bool ImplementsISpanParsable { get; }

  //    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IUtf8SpanParsable{TSelf}"/>.</summary>
  //    public bool ImplementsIUtf8SpanParsable { get; }

  //    #endregion

  //    #endregion

  //    #region Is.. for Concrete Types

  //    /// <summary>Indicates whether the type represents <see cref="System.Numerics.BFloat16"/>.</summary>
  //    public bool IsBFloat16 { get; }

  //    /// <summary>Indicates whether the type represents <see cref="System.Numerics.BigInteger"/>.</summary>
  //    public bool IsBigInteger { get; }

  //    /// <summary>Indicates whether the type represents <see cref="System.Numerics.Complex"/>.</summary>
  //    public bool IsComplex { get; }

  //    /// <summary>Indicates whether the type represents <see cref="System.Decimal"/>.</summary>
  //    public bool IsDecimal { get; }

  //    /// <summary>Indicates whether the type represents <see cref="Flux.Numerics.BigRational"/>.</summary>
  //    public bool IsBigRational { get; }

  //    #endregion

  //    #region IsBinary.. IEEE Formats

  //    /// <summary>Indicates whether the type represents an IEEE binary16 floating-point, such as <see cref="System.Half"/></summary>
  //    public bool IsBinary16 { get; }

  //    /// <summary>Indicates whether the type represents an IEEE binary32 floating-point, such as <see cref="System.Single"/> or <see cref="System.Runtime.InteropServices.NFloat"/> when <see cref="System.Runtime.InteropServices.NFloat.Size"/> is 4.</summary>
  //    public bool IsBinary32 { get; }

  //    /// <summary>Indicates whether the type represents an IEEE binary64 floating-point, such as <see cref="System.Double"/> or <see cref="System.Runtime.InteropServices.NFloat"/> when <see cref="System.Runtime.InteropServices.NFloat.Size"/> is 8.</summary>
  //    public bool IsBinary64 { get; }

  //    /// <summary>Indicates whether the type represents an IEEE binary128 floating-point. There are currently no such types in .NET.</summary>
  //    public bool IsBinary128 { get; }

  //    #endregion

  //    #region IsDecimal.. IEEE Formats

  //    /// <summary>Indicates whether the type represents an IEEE decimal32 floating-point, such as <see cref="System.Decimal32"/>.</summary>
  //    public bool IsDecimal32 { get; }

  //    /// <summary>Indicates whether the type represents an IEEE decimal64 floating-point, such as <see cref="System.Decimal64"/>.</summary>
  //    public bool IsDecimal64 { get; }

  //    /// <summary>Indicates whether the type represents an IEEE decimal128 floating-point, such as <see cref="System.Decimal128"/>.</summary>
  //    public bool IsDecimal128 { get; }

  //    #endregion

  //    #region IsInteger.. Fixed Width

  //    /// <summary>Indicates whether the type represents an 8-bit integer type, such as <see cref="System.Byte"/> or <see cref="System.SByte"/>.</summary>
  //    public bool IsInteger8 { get; }

  //    /// <summary>Indicates whether the type represents a 16-bit integer type, such as <see cref="System.Int16"/> or <see cref="System.UInt16"/>.</summary>
  //    public bool IsInteger16 { get; }

  //    /// <summary>Indicates whether the type represents a 32-bit integer type, such as <see cref="System.Int32"/>, <see cref="System.UInt32"/>, <see cref="System.IntPtr"/> (when <see cref="System.IntPtr.Size"/> is 4), or <see cref="System.UIntPtr"/> (when <see cref="System.UIntPtr.Size"/> is 4).</summary>
  //    public bool IsInteger32 { get; }

  //    /// <summary>Indicates whether the type represents a 64-bit integer type, such as <see cref="System.Int64"/>, <see cref="System.UInt64"/>, <see cref="System.IntPtr"/> (when <see cref="System.IntPtr.Size"/> is 8), or <see cref="System.UIntPtr"/> (when <see cref="System.UIntPtr.Size"/> is 8).</summary>
  //    public bool IsInteger64 { get; }

  //    /// <summary>Indicates whether the type represents a 128-bit integer type, such as <see cref="System.Int128"/> or <see cref="System.UInt128"/>.</summary>
  //    public bool IsInteger128 { get; }

  //    #endregion

  //    #region Other Properties

  //    /// <summary>The width of the type in bits.</summary>
  //    /// <remarks>For fixed-width types, this equals <see cref="StorageBytes"/> × 8. If <see cref="IsArbitraryPrecision"/> is <see langword="true"/>, this value is 0, indicating an unbounded bit-width.</remarks>
  //    public int BitWidth { get; }

  //    /// <summary>Indicates whether the type has an unbounded bit-width, such as <see cref="System.Numerics.BigInteger"/>.</summary>
  //    public bool IsArbitraryPrecision { get; }

  //    /// <summary>Indicates whether the type has a fixed bit-width, such as <see cref="System.Decimal"/>, <see cref="System.Decimal128"/>, <see cref="System.Int32"/>, or <see cref="System.Single"/>.</summary>
  //    public bool IsFixedWidth { get; }

  //    /// <summary>Indicates whether the type represents a signed integer, such as <see cref="System.Int128"/>, <see cref="System.Int16"/>, <see cref="System.Int32"/>, <see cref="System.Int64"/>, <see cref="System.SByte"/>, or <see cref="System.Numerics.BigInteger"/>.</summary>
  //    public bool IsSignedInteger { get; }

  //    /// <summary>Indicates whether the type represents an unsigned integer , such as <see cref="System.Byte"/>, <see cref="System.UInt128"/>, <see cref="System.UInt16"/>, <see cref="System.UInt32"/>, or <see cref="System.UInt64"/>.</summary>
  //    public bool IsUnsignedInteger { get; }

  //    /// <summary>The number of storage bytes used by the type.</summary>
  //    /// <remarks>This does not include implementation-dependent padding, object headers, or additional heap allocations (for example, <see cref="System.Numerics.BigInteger"/>).</remarks>
  //    public int StorageBytes { get; }

  //    #endregion
  //  }

  //  //public interface IIntegerTypeInfo<T>
  //  //  //: INumericTypeInfo
  //  //  where T : System.Numerics.IBinaryInteger<T>
  //  //{
  //  //  T MinPrime { get; }
  //  //  T MaxPrime { get; }
  //  //}

  //  //public interface IFloatingPointTypeInfo<T>
  //  //  : INumericTypeInfo
  //  //  where T : System.Numerics.IFloatingPoint<T>
  //  //{
  //  //  T MinExactInteger { get; }
  //  //  T MaxExactInteger { get; }

  //  //  T MinExactPrime { get; }
  //  //  T MaxExactPrime { get; }
  //  //}

  //  //public interface IFloatingPointBinaryTypeInfo<T>
  //  //  : IFloatingPointTypeInfo<T>
  //  //  where T : System.Numerics.IFloatingPoint<T>
  //  //{
  //  //  (System.Int128 Sign, System.Int128 Exponent, System.Int128 Significand) BinaryWidth { get; }
  //  //}

  //  //public interface IFloatingPointDecimalTypeInfo<T>
  //  //  : IFloatingPointTypeInfo<T>
  //  //  where T : System.Numerics.IFloatingPoint<T>
  //  //{
  //  //  (System.Int128 Sign, System.Int128 Combination, System.Int128 ExponentContinuation, System.Int128 CoefficientContinuation) DecimalWidth { get; }
  //  //}

  //  #region GenericMathConstants enum flags

  //  [System.Flags]
  //  public enum GenericMathConstants : long
  //  {
  //    NoConstants = 0,

  //    IFloatingPointConstants = 0x0000_0000_0000_0001,
  //  }

  //  #endregion

  //  #region GenericMathCore enum flags

  //  [System.Flags]
  //  public enum GenericMathCore : long
  //  {
  //    NoCore = 0,

  //    ComplexT = 0x0000_0000_0000_0001,
  //    IBinaryFloatingPointIeee754 = 0x0000_0000_0000_0002,
  //    IBinaryInteger = 0x0000_0000_0000_0004,
  //    IBinaryNumber = 0x0000_0000_0000_0008,
  //    IDecimalFloatingPointIeee754 = 0x0000_0000_0000_0010,
  //    IFloatingPoint = 0x0000_0000_0000_0020,
  //    IFloatingPointIeee754 = 0x0000_0000_0000_0040,
  //    IMinMaxValue = 0x0000_0000_0000_0080,
  //    INumber = 0x0000_0000_0000_0100,
  //    INumberBase = 0x0000_0000_0000_0200,
  //    ISignedNumber = 0x0000_0000_0000_0400,
  //    IUnsignedNumber = 0x0000_0000_0000_0800,
  //  }

  //  #endregion

  //  #region GenericMathFormattable enum flags

  //  [System.Flags]
  //  public enum GenericMathFormattable : long
  //  {
  //    NoFormattable = 0,

  //    IFormattable = 0x0000_0000_0000_0001,
  //    ISpanFormattable = 0x0000_0000_0000_0002,
  //    IUtf8SpanFormattable = 0x0000_0000_0000_0004,
  //  }

  //  #endregion

  //  #region GenericMathFunctions enum flags

  //  [System.Flags]
  //  public enum GenericMathFunctions : long
  //  {
  //    NoFunctions = 0,

  //    IExponentialFunctions = 0x0000_0000_0000_0001,
  //    IHyperbolicFunctions = 0x0000_0000_0000_0002,
  //    ILogarithmicFunctions = 0x0000_0000_0000_0004,
  //    IPowerFunctions = 0x0000_0000_0000_0008,
  //    IRootFunctions = 0x0000_0000_0000_0010,
  //    ITrigonometricFunctions = 0x0000_0000_0000_0020,
  //  }

  //  #endregion

  //  #region GenericMathIdentity enum flags

  //  [System.Flags]
  //  public enum GenericMathIdentity : long
  //  {
  //    NoIdentity = 0,

  //    IAdditiveIdentity = 0x0000_0000_0000_0001,
  //    IMultiplicativeIdentity = 0x0000_0000_0000_0002,
  //  }

  //  #endregion

  //  #region GenericMathOperators enum flags

  //  [System.Flags]
  //  public enum GenericMathOperators : long
  //  {
  //    NoOperators = 0,

  //    IAdditionOperators = 0x0000_0000_0000_0001,
  //    IBitwiseOperators = 0x0000_0000_0000_0002,
  //    IComparisonOperators = 0x0000_0000_0000_0004,
  //    IDecrementOperators = 0x0000_0000_0000_0008,
  //    IDivisionOperators = 0x0000_0000_0000_0010,
  //    IEqualityOperators = 0x0000_0000_0000_0020,
  //    IIncrementOperators = 0x0000_0000_0000_0040,
  //    IModulusOperators = 0x0000_0000_0000_0080,
  //    IMultiplyOperators = 0x0000_0000_0000_0100,
  //    IShiftOperators = 0x0000_0000_0000_0200,
  //    ISubtractionOperators = 0x0000_0000_0000_0400,
  //    IUnaryNegationOperators = 0x0000_0000_0000_0800,
  //    IUnaryPlusOperators = 0x0000_0000_0000_1000,
  //  }

  //  #endregion

  //  #region GenericMathParsable enum flags

  //  [System.Flags]
  //  public enum GenericMathParsable : long
  //  {
  //    NoParsable = 0,

  //    IParsable = 0x0000_0000_0000_0001,
  //    ISpanParsable = 0x0000_0000_0000_0002,
  //    IUtf8SpanParsable = 0x0000_0000_0000_0004,
  //  }

  //  #endregion

  //  //public class IntegerTypeInfo<TInteger>
  //  //  : IIntegerTypeInfo<TInteger>
  //  //  where TInteger : System.Numerics.IBinaryInteger<TInteger>
  //  //{
  //  //  private static readonly System.Collections.Concurrent.ConcurrentDictionary<Type, IntegerTypeInfo<TInteger>> m_cache = new();

  //  //  public IntegerTypeInfo()
  //  //  {
  //  //    MinPrime
  //  //      = TInteger.CreateChecked(2);

  //  //    MaxPrime
  //  //      = typeof(TInteger) == typeof(System.Byte)
  //  //      ? TInteger.CreateChecked(251)
  //  //      : typeof(TInteger) == typeof(System.Int128)
  //  //      ? TInteger.CreateChecked(System.Int128.Parse("170141183460469231731687303715884105727"))
  //  //      : typeof(TInteger) == typeof(System.Int16)
  //  //      ? TInteger.CreateChecked(32749)
  //  //      : typeof(TInteger) == typeof(System.Int32) || (typeof(TInteger) == typeof(System.IntPtr) && System.IntPtr.Size == 4)
  //  //      ? TInteger.CreateChecked(2147483647)
  //  //      : typeof(TInteger) == typeof(System.Int64) || (typeof(TInteger) == typeof(System.IntPtr) && System.IntPtr.Size == 8)
  //  //      ? TInteger.CreateChecked(9223372036854775783)
  //  //      : typeof(TInteger) == typeof(System.SByte)
  //  //      ? TInteger.CreateChecked(127)
  //  //      : typeof(TInteger) == typeof(System.UInt128)
  //  //      ? TInteger.CreateChecked(System.UInt128.Parse("340282366920938463463374607431768211297"))
  //  //      : typeof(TInteger) == typeof(System.UInt16)
  //  //      ? TInteger.CreateChecked(65521)
  //  //      : typeof(TInteger) == typeof(System.UInt32) || (typeof(TInteger) == typeof(System.UIntPtr) && System.UIntPtr.Size == 4)
  //  //      ? TInteger.CreateChecked(4294967291u)
  //  //      : typeof(TInteger) == typeof(System.UInt64) || (typeof(TInteger) == typeof(System.UIntPtr) && System.UIntPtr.Size == 8)
  //  //      ? TInteger.CreateChecked(18446744073709551557ul)
  //  //      : throw new System.NotImplementedException(typeof(TInteger).Name);

  //  //    NumericTypeInfo = new NumericTypeInfo(typeof(TInteger));
  //  //  }

  //  //  public TInteger MinPrime { get; }

  //  //  public TInteger MaxPrime { get; }

  //  //  public NumericTypeInfo NumericTypeInfo { get; }
  //  //}

  //  //public class IntegerTypeInfo
  //  //{

  //  //  public static IntegerTypeInfo<TInteger> Get<TInteger>(Type type)
  //  //    where TInteger : System.Numerics.IBinaryInteger<TInteger>
  //  //    => (IntegerTypeInfo<TInteger>)m_cache.GetOrAdd(type, static t => new IntegerTypeInfo<TInteger>());

  //  //  public static IntegerTypeInfo<TInteger> Get<TInteger>()
  //  //    where TInteger : System.Numerics.IBinaryInteger<TInteger>
  //  //    => Get(typeof(TInteger));

  //  //}

  //  //public class IntegerTypeInfo
  //  //{
  //  //  public IntegerTypeInfo<TInteger> Create(System.Type integerType)
  //  //  {
  //  //    var genericType = typeof(IntegerTypeInfo<>).MakeGenericType(integerType);

  //  //    return Activator.CreateInstance(genericType)!;
  //  //  }
  //  //}

  //  public class NumericTypeInfo
  //    : INumericTypeInfo
  //  {
  //    private static readonly System.Collections.Concurrent.ConcurrentDictionary<Type, NumericTypeInfo> m_cache = new();
  //    public static System.Collections.Generic.IReadOnlyDictionary<Type, NumericTypeInfo> Cache => m_cache;

  //    private readonly System.Type m_type;

  //    public NumericTypeInfo(System.Type type)
  //    {
  //      m_type = type;

  //      Constants = GetGenericMathConstants(m_type);
  //      Core = GetGenericMathCore(m_type);
  //      Formattable = GetGenericMathFormattable(m_type);
  //      Functions = GetGenericMathFunctions(m_type);
  //      Identity = GetGenericMathIdentity(m_type);
  //      Operators = GetGenericMathOperators(m_type);
  //      Parsable = GetGenericMathParsable(m_type);
  //    }

  //    public System.Type Type
  //      => m_type;

  //    #region Groups

  //    public GenericMathConstants Constants { get; }
  //    public GenericMathCore Core { get; }
  //    public GenericMathFormattable Formattable { get; }
  //    public GenericMathFunctions Functions { get; }
  //    public GenericMathIdentity Identity { get; }
  //    public GenericMathOperators Operators { get; }
  //    public GenericMathParsable Parsable { get; }

  //    #endregion

  //    #region Implements ..Constants Interfaces

  //    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IFloatingPointConstants{TSelf}"/>.</summary>
  //    public bool ImplementsIFloatingPointConstants => (Constants & GenericMathConstants.IFloatingPointConstants) != 0;

  //    #endregion

  //    #region Implements "Core" Interfaces

  //    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IBinaryFloatingPointIeee754{TSelf}"/>.</summary>
  //    public bool ImplementsIBinaryFloatingPointIeee754 => (Core & GenericMathCore.IBinaryFloatingPointIeee754) != 0;
  //    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IBinaryInteger{TSelf}"/>.</summary>
  //    public bool ImplementsIBinaryInteger => (Core & GenericMathCore.IBinaryInteger) != 0;
  //    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IBinaryNumber{TSelf}"/>.</summary>
  //    public bool ImplementsIBinaryNumber => (Core & GenericMathCore.IBinaryNumber) != 0;
  //    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IDecimalFloatingPointIeee754{TSelf}"/>.</summary>
  //    public bool ImplementsIDecimalFloatingPointIeee754 => (Core & GenericMathCore.IDecimalFloatingPointIeee754) != 0;
  //    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IFloatingPoint{TSelf}"/>.</summary>
  //    public bool ImplementsIFloatingPoint => (Core & GenericMathCore.IFloatingPoint) != 0;
  //    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IFloatingPointIeee754{TSelf}"/>.</summary>
  //    public bool ImplementsIFloatingPointIeee754 => (Core & GenericMathCore.IFloatingPointIeee754) != 0;
  //    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IMinMaxValue{TSelf}"/>.</summary>
  //    public bool ImplementsIMinMaxValue => (Core & GenericMathCore.IMinMaxValue) != 0;
  //    /// <summary>Indicates whether the type implements <see cref="System.Numerics.INumber{TSelf}"/>.</summary>
  //    public bool ImplementsINumber => (Core & GenericMathCore.INumber) != 0;
  //    /// <summary>Indicates whether the type implements <see cref="System.Numerics.INumberBase{TSelf}"/>.</summary>
  //    public bool ImplementsINumberBase => (Core & GenericMathCore.INumberBase) != 0;
  //    /// <summary>Indicates whether the type implements <see cref="System.Numerics.ISignedNumber{TSelf}"/>.</summary>
  //    public bool ImplementsISignedNumber => (Core & GenericMathCore.ISignedNumber) != 0;
  //    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IUnsignedNumber{TSelf}"/>.</summary>
  //    public bool ImplementsIUnsignedNumber => (Core & GenericMathCore.IUnsignedNumber) != 0;

  //    #endregion

  //    #region Implements ..Formattable Interfaces

  //    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IFormattable"/>.</summary>
  //    public bool ImplementsIFormattable => (Formattable & GenericMathFormattable.IFormattable) != 0;
  //    /// <summary>Indicates whether the type implements <see cref="System.Numerics.ISpanFormattable"/>.</summary>
  //    public bool ImplementsISpanFormattable => (Formattable & GenericMathFormattable.ISpanFormattable) != 0;
  //    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IUtf8SpanFormattable"/>.</summary>
  //    public bool ImplementsIUtf8SpanFormattable => (Formattable & GenericMathFormattable.IUtf8SpanFormattable) != 0;

  //    #endregion

  //    #region Implements ..Functions Interfaces

  //    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IExponentialFunctions{TSelf}"/>.</summary>
  //    public bool ImplementsIExponentialFunctions => (Functions & GenericMathFunctions.IExponentialFunctions) != 0;
  //    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IHyperbolicFunctions{TSelf}"/>.</summary>
  //    public bool ImplementsIHyperbolicFunctions => (Functions & GenericMathFunctions.IHyperbolicFunctions) != 0;
  //    /// <summary>Indicates whether the type implements <see cref="System.Numerics.ILogarithmicFunctions{TSelf}"/>.</summary>
  //    public bool ImplementsILogarithmicFunctions => (Functions & GenericMathFunctions.ILogarithmicFunctions) != 0;
  //    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IPowerFunctions{TSelf}"/>.</summary>
  //    public bool ImplementsIPowerFunctions => (Functions & GenericMathFunctions.IPowerFunctions) != 0;
  //    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IRootFunctions{TSelf}"/>.</summary>
  //    public bool ImplementsIRootFunctions => (Functions & GenericMathFunctions.IRootFunctions) != 0;
  //    /// <summary>Indicates whether the type implements <see cref="System.Numerics.ITrigonometricFunctions{TSelf}"/>.</summary>
  //    public bool ImplementsITrigonometricFunctions => (Functions & GenericMathFunctions.ITrigonometricFunctions) != 0;

  //    #endregion

  //    #region Implements ..Identity Interfaces

  //    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IAdditiveIdentity{TSelf, TResult}"/>.</summary>
  //    public bool ImplementsIAdditiveIdentity => (Identity & GenericMathIdentity.IAdditiveIdentity) != 0;
  //    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IMultiplicativeIdentity{TSelf, TResult}"/>.</summary>
  //    public bool ImplementsIMultiplicativeIdentity => (Identity & GenericMathIdentity.IMultiplicativeIdentity) != 0;

  //    #endregion

  //    #region Implements ..Operators Interfaces

  //    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IAdditionOperators{TSelf, TOther, TResult}"/>.</summary>
  //    public bool ImplementsIAdditionOperators => (Operators & GenericMathOperators.IAdditionOperators) != 0;
  //    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IBitwiseOperators{TSelf, TOther, TResult}"/>.</summary>
  //    public bool ImplementsIBitwiseOperators => (Operators & GenericMathOperators.IBitwiseOperators) != 0;
  //    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IComparisonOperators{TSelf, TOther, TResult}"/>.</summary>
  //    public bool ImplementsIComparisonOperators => (Operators & GenericMathOperators.IComparisonOperators) != 0;
  //    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IDecrementOperators{TSelf}"/>.</summary>
  //    public bool ImplementsIDecrementOperators => (Operators & GenericMathOperators.IDecrementOperators) != 0;
  //    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IDivisionOperators{TSelf, TOther, TResult}"/>.</summary>
  //    public bool ImplementsIDivisionOperators => (Operators & GenericMathOperators.IDivisionOperators) != 0;
  //    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IEqualityOperators{TSelf, TOther, TResult}"/>.</summary>
  //    public bool ImplementsIEqualityOperators => (Operators & GenericMathOperators.IEqualityOperators) != 0;
  //    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IIncrementOperators{TSelf}"/>.</summary>
  //    public bool ImplementsIIncrementOperators => (Operators & GenericMathOperators.IIncrementOperators) != 0;
  //    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IModulusOperators{TSelf, TOther, TResult}"/>.</summary>
  //    public bool ImplementsIModulusOperators => (Operators & GenericMathOperators.IModulusOperators) != 0;
  //    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IMultiplyOperators{TSelf, TOther, TResult}"/>.</summary>
  //    public bool ImplementsIMultiplyOperators => (Operators & GenericMathOperators.IMultiplyOperators) != 0;
  //    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IShiftOperators{TSelf, TOther, TResult}"/>.</summary>
  //    public bool ImplementsIShiftOperators => (Operators & GenericMathOperators.IShiftOperators) != 0;
  //    /// <summary>Indicates whether the type implements <see cref="System.Numerics.ISubtractionOperators{TSelf, TOther, TResult}"/>.</summary>
  //    public bool ImplementsISubtractionOperators => (Operators & GenericMathOperators.ISubtractionOperators) != 0;
  //    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IUnaryNegationOperators{TSelf, TResult}"/>.</summary>
  //    public bool ImplementsIUnaryNegationOperators => (Operators & GenericMathOperators.IUnaryNegationOperators) != 0;
  //    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IUnaryPlusOperators{TSelf, TResult}"/>.</summary>
  //    public bool ImplementsIUnaryPlusOperators => (Operators & GenericMathOperators.IUnaryPlusOperators) != 0;

  //    #endregion

  //    #region Implements ..Parsable Interfaces

  //    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IParsable{TSelf}"/>.</summary>
  //    public bool ImplementsIParsable => (Parsable & GenericMathParsable.IParsable) != 0;
  //    /// <summary>Indicates whether the type implements <see cref="System.Numerics.ISpanParsable{TSelf}"/>.</summary>
  //    public bool ImplementsISpanParsable => (Parsable & GenericMathParsable.ISpanParsable) != 0;
  //    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IUtf8SpanParsable{TSelf}"/>.</summary>
  //    public bool ImplementsIUtf8SpanParsable => (Parsable & GenericMathParsable.IUtf8SpanParsable) != 0;

  //    #endregion

  //    #region Is.. for Concrete Types

  //    /// <summary>Indicates whether the type represents <see cref="System.Numerics.BFloat16"/>.</summary>
  //#if NET11_0_OR_GREATER
  //    public bool IsBFloat16 => m_type == typeof(System.Numerics.BFloat16);
  //#else
  //    public bool IsBFloat16 => false;
  //#endif

  //    /// <summary>Indicates whether the type represents <see cref="System.Numerics.BigInteger"/>.</summary>
  //    public bool IsBigInteger => m_type == typeof(System.Numerics.BigInteger);

  //    /// <summary>Indicates whether the type represents <see cref="System.Numerics.Complex"/>.</summary>
  //    public bool IsComplex => m_type == typeof(System.Numerics.Complex);

  //    /// <summary>Indicates whether the type represents <see cref="System.Decimal"/>.</summary>
  //    public bool IsDecimal => m_type == typeof(System.Decimal);

  //    /// <summary>Indicates whether the type represents <see cref="Flux.Numerics.BigRational"/>.</summary>
  //    public bool IsBigRational => m_type == typeof(Flux.Numerics.BigRational);

  //    #endregion

  //    #region IsBinary.. IEEE Formats

  //    /// <summary>Indicates whether the type represents an IEEE binary16 floating-point, such as <see cref="System.Half"/></summary>
  //    public bool IsBinary16
  //      => m_type == typeof(System.Half);

  //    /// <summary>Indicates whether the type represents an IEEE binary32 floating-point, such as <see cref="System.Single"/> or <see cref="System.Runtime.InteropServices.NFloat"/> when <see cref="System.Runtime.InteropServices.NFloat.Size"/> is 4.</summary>
  //    public bool IsBinary32
  //      => m_type == typeof(System.Single) || (m_type == typeof(System.Runtime.InteropServices.NFloat) && System.Runtime.InteropServices.NFloat.Size == 4);

  //    /// <summary>Indicates whether the type represents an IEEE binary64 floating-point, such as <see cref="System.Double"/> or <see cref="System.Runtime.InteropServices.NFloat"/> when <see cref="System.Runtime.InteropServices.NFloat.Size"/> is 8.</summary>
  //    public bool IsBinary64
  //      => m_type == typeof(System.Double) || (m_type == typeof(System.Runtime.InteropServices.NFloat) && System.Runtime.InteropServices.NFloat.Size == 8);

  //    /// <summary>Indicates whether the type represents an IEEE binary128 floating-point. There are currently no such types in .NET.</summary>
  //    public bool IsBinary128
  //      => false;

  //    #endregion

  //    #region IsDecimal.. IEEE Formats

  //    /// <summary>Indicates whether the type represents an IEEE decimal32 floating-point, such as <see cref="System.Decimal32"/>.</summary>
  //#if NET11_0_OR_GREATER
  //    public bool IsDecimal32
  //      => m_type == typeof(System.Numerics.Decimal32);
  //#else
  //    public bool IsDecimal32
  //      => false;
  //#endif

  //    /// <summary>Indicates whether the type represents an IEEE decimal64 floating-point, such as <see cref="System.Decimal64"/>.</summary>
  //#if NET11_0_OR_GREATER
  //    public bool IsDecimal64
  //      => m_type == typeof(System.Numerics.Decimal64);
  //#else
  //    public bool IsDecimal64
  //      => false;
  //#endif

  //    /// <summary>Indicates whether the type represents an IEEE decimal128 floating-point, such as <see cref="System.Decimal128"/>.</summary>
  //#if NET11_0_OR_GREATER
  //    public bool IsDecimal128
  //      => m_type == typeof(System.Numerics.Decimal128);
  //#else
  //    public bool IsDecimal128
  //      => false;
  //#endif

  //    #endregion

  //    #region IsInteger.. Fixed Width

  //    /// <summary>Indicates whether the type represents an 8-bit integer type, such as <see cref="System.Byte"/> or <see cref="System.SByte"/>.</summary>
  //    public bool IsInteger8
  //      => m_type == typeof(System.Byte) || m_type == typeof(System.SByte);

  //    /// <summary>Indicates whether the type represents a 16-bit integer type, such as <see cref="System.Int16"/> or <see cref="System.UInt16"/>.</summary>
  //    public bool IsInteger16
  //      => m_type == typeof(System.Int16) || m_type == typeof(System.UInt16);

  //    /// <summary>Indicates whether the type represents a 32-bit integer type, such as <see cref="System.Int32"/>, <see cref="System.UInt32"/>, <see cref="System.IntPtr"/> (when <see cref="System.IntPtr.Size"/> is 4), or <see cref="System.UIntPtr"/> (when <see cref="System.UIntPtr.Size"/> is 4).</summary>
  //    public bool IsInteger32
  //      => m_type == typeof(System.Int32) || m_type == typeof(System.UInt32) || (m_type == typeof(System.IntPtr) && System.IntPtr.Size == 4) || (m_type == typeof(System.UIntPtr) && System.UIntPtr.Size == 4);

  //    /// <summary>Indicates whether the type represents a 64-bit integer type, such as <see cref="System.Int64"/>, <see cref="System.UInt64"/>, <see cref="System.IntPtr"/> (when <see cref="System.IntPtr.Size"/> is 8), or <see cref="System.UIntPtr"/> (when <see cref="System.UIntPtr.Size"/> is 8).</summary>
  //    public bool IsInteger64
  //      => m_type == typeof(System.Int64) || m_type == typeof(System.UInt64) || (m_type == typeof(System.IntPtr) && System.IntPtr.Size == 8) || (m_type == typeof(System.UIntPtr) && System.UIntPtr.Size == 8);

  //    /// <summary>Indicates whether the type represents a 128-bit integer type, such as <see cref="System.Int128"/> or <see cref="System.UInt128"/>.</summary>
  //    public bool IsInteger128
  //      => m_type == typeof(System.Int128) || m_type == typeof(System.UInt128);

  //    #endregion

  //    #region Other Properties

  //    /// <summary>The width of the type in bits.</summary>
  //    /// <remarks>For fixed-width types, this equals <see cref="StorageBytes"/> × 8. If <see cref="IsArbitraryPrecision"/> is <see langword="true"/>, this value is 0, indicating an unbounded bit-width.</remarks>
  //    public int BitWidth => IsArbitraryPrecision ? 0 : StorageBytes * 8;

  //    /// <summary>Indicates whether the type has an unbounded bit-width, such as <see cref="System.Numerics.BigInteger"/>.</summary>
  //    public bool IsArbitraryPrecision => IsBigInteger;

  //    /// <summary>Indicates whether the type has a fixed bit-width, such as <see cref="System.Decimal"/>, <see cref="System.Decimal128"/>, <see cref="System.Int32"/>, or <see cref="System.Single"/>.</summary>
  //    public bool IsFixedWidth => !IsBigInteger;

  //    /// <summary>Indicates whether the type represents a signed integer, such as <see cref="System.Int128"/>, <see cref="System.Int16"/>, <see cref="System.Int32"/>, <see cref="System.Int64"/>, <see cref="System.SByte"/>, or <see cref="System.Numerics.BigInteger"/>.</summary>
  //    public bool IsSignedInteger => (Core & (GenericMathCore.IBinaryInteger | GenericMathCore.ISignedNumber)) == (GenericMathCore.IBinaryInteger | GenericMathCore.ISignedNumber);

  //    /// <summary>Indicates whether the type represents an unsigned integer , such as <see cref="System.Byte"/>, <see cref="System.UInt128"/>, <see cref="System.UInt16"/>, <see cref="System.UInt32"/>, or <see cref="System.UInt64"/>.</summary>
  //    public bool IsUnsignedInteger => (Core & (GenericMathCore.IBinaryInteger | GenericMathCore.IUnsignedNumber)) == (GenericMathCore.IBinaryInteger | GenericMathCore.IUnsignedNumber);

  //    public string CSharpLiteralSuffix
  //      => m_type == typeof(decimal) ? "m" :
  //      m_type == typeof(double) ? "d" :
  //      m_type == typeof(float) ? "f" :
  //      m_type == typeof(long) ? "l" :
  //      m_type == typeof(ulong) ? "ul" :
  //      m_type == typeof(uint) ? "u" :
  //      string.Empty;

  //    public NumericDomain NumberSystem
  //      => IsUnsignedInteger ? NumericDomain.NaturalNumbers
  //      : IsSignedInteger ? NumericDomain.Integers
  //      : ImplementsIFloatingPoint ? NumericDomain.RealNumbers
  //      : IsComplex ? NumericDomain.ComplexNumbers
  //      : IsBigRational ? NumericDomain.RationalNumbers
  //      : NumericDomain.Unknown;

  //    public System.Text.Rune NumberSystemSymbol
  //      => (System.Text.Rune)(char)NumberSystem;

  //    /// <summary>The number of storage bytes used by the type.</summary>
  //    /// <remarks>This does not include implementation-dependent padding, object headers, or additional heap allocations (for example, <see cref="System.Numerics.BigInteger"/>).</remarks>
  //    public int StorageBytes
  //      => IsInteger8 ? 1
  //      : (IsBFloat16 || IsBinary16 || IsInteger16) ? 2
  //      : (IsBinary32 || IsDecimal32 || IsInteger32) ? 4
  //      : (IsBinary64 || IsDecimal64 || IsInteger64) ? 8
  //      : (IsBinary128 || IsDecimal128 || IsInteger128) ? 16
  //      : SizeOf(m_type);

  //    #endregion

  //    #region Static methods

  //    public static NumericTypeInfo Get(Type type)
  //      => m_cache.GetOrAdd(type, static t => new NumericTypeInfo(t));

  //    public static NumericTypeInfo Get<TNumberBase>()
  //      where TNumberBase : System.Numerics.INumberBase<TNumberBase>
  //      => Get(typeof(TNumberBase));

  //    public static GenericMathConstants GetGenericMathConstants(System.Type type)
  //    {
  //      var numericCapabilityOperators = GenericMathConstants.NoConstants;

  //      foreach (var itype in type.GetCachedInterfaces())
  //      {
  //        if (itype == typeof(System.Numerics.IFloatingPointConstants<>))
  //          numericCapabilityOperators |= GenericMathConstants.IFloatingPointConstants;
  //      }

  //      return numericCapabilityOperators;
  //    }

  //    public static GenericMathCore GetGenericMathCore(System.Type type)
  //    {
  //      var numericCapabilityCore = GenericMathCore.NoCore;

  //      foreach (var itype in type.GetCachedInterfaces())
  //      {
  //#if NET11_0_OR_GREATER
  //        if (itype == typeof(System.Numerics.Complex<>))
  //          numericCapabilityCore |= GenericMathCore.ComplexT;
  //        else
  //#endif
  //          if (itype == typeof(System.Numerics.IBinaryFloatingPointIeee754<>))
  //            numericCapabilityCore |= GenericMathCore.IBinaryFloatingPointIeee754;
  //          else if (itype == typeof(System.Numerics.IBinaryInteger<>))
  //            numericCapabilityCore |= GenericMathCore.IBinaryInteger;
  //          else if (itype == typeof(System.Numerics.IBinaryNumber<>))
  //            numericCapabilityCore |= GenericMathCore.IBinaryNumber;
  //#if NET11_0_OR_GREATER
  //          else if (itype == typeof(System.Numerics.IDecimalFloatingPointIeee754<>))
  //            numericCapabilityCore |= GenericMathCore.IDecimalFloatingPointIeee754;
  //#endif
  //          else if (itype == typeof(System.Numerics.IFloatingPoint<>))
  //            numericCapabilityCore |= GenericMathCore.IFloatingPoint;
  //          else if (itype == typeof(System.Numerics.IFloatingPointIeee754<>))
  //            numericCapabilityCore |= GenericMathCore.IFloatingPointIeee754;
  //          else if (itype == typeof(System.Numerics.IMinMaxValue<>))
  //            numericCapabilityCore |= GenericMathCore.IMinMaxValue;
  //          else if (itype == typeof(System.Numerics.INumber<>))
  //            numericCapabilityCore |= GenericMathCore.INumber;
  //          else if (itype == typeof(System.Numerics.INumberBase<>))
  //            numericCapabilityCore |= GenericMathCore.INumberBase;
  //          else if (itype == typeof(System.Numerics.ISignedNumber<>))
  //            numericCapabilityCore |= GenericMathCore.ISignedNumber;
  //          else if (itype == typeof(System.Numerics.IUnsignedNumber<>))
  //            numericCapabilityCore |= GenericMathCore.IUnsignedNumber;
  //      }

  //      return numericCapabilityCore;
  //    }
  //    public static GenericMathFormattable GetGenericMathFormattable(System.Type type)
  //    {
  //      var numericCapabilityOperators = GenericMathFormattable.NoFormattable;

  //      foreach (var itype in type.GetCachedInterfaces())
  //      {
  //        if (itype == typeof(System.IFormattable))
  //          numericCapabilityOperators |= GenericMathFormattable.IFormattable;
  //        else if (itype == typeof(System.ISpanFormattable))
  //          numericCapabilityOperators |= GenericMathFormattable.ISpanFormattable;
  //        else if (itype == typeof(System.IUtf8SpanFormattable))
  //          numericCapabilityOperators |= GenericMathFormattable.IUtf8SpanFormattable;
  //      }

  //      return numericCapabilityOperators;
  //    }
  //    public static GenericMathFunctions GetGenericMathFunctions(System.Type type)
  //    {
  //      var numericCapabilityOperators = GenericMathFunctions.NoFunctions;

  //      foreach (var itype in type.GetCachedInterfaces())
  //      {
  //        if (itype == typeof(System.Numerics.IExponentialFunctions<>))
  //          numericCapabilityOperators |= GenericMathFunctions.IExponentialFunctions;
  //        else if (itype == typeof(System.Numerics.IHyperbolicFunctions<>))
  //          numericCapabilityOperators |= GenericMathFunctions.IHyperbolicFunctions;
  //        else if (itype == typeof(System.Numerics.ILogarithmicFunctions<>))
  //          numericCapabilityOperators |= GenericMathFunctions.ILogarithmicFunctions;
  //        else if (itype == typeof(System.Numerics.IPowerFunctions<>))
  //          numericCapabilityOperators |= GenericMathFunctions.IPowerFunctions;
  //        else if (itype == typeof(System.Numerics.IRootFunctions<>))
  //          numericCapabilityOperators |= GenericMathFunctions.IRootFunctions;
  //        else if (itype == typeof(System.Numerics.ITrigonometricFunctions<>))
  //          numericCapabilityOperators |= GenericMathFunctions.ITrigonometricFunctions;
  //      }

  //      return numericCapabilityOperators;
  //    }
  //    public static GenericMathIdentity GetGenericMathIdentity(System.Type type)
  //    {
  //      var numericCapabilityOperators = GenericMathIdentity.NoIdentity;

  //      foreach (var itype in type.GetCachedInterfaces())
  //      {
  //        if (itype == typeof(System.Numerics.IAdditiveIdentity<,>))
  //          numericCapabilityOperators |= GenericMathIdentity.IAdditiveIdentity;
  //        else if (itype == typeof(System.Numerics.IMultiplicativeIdentity<,>))
  //          numericCapabilityOperators |= GenericMathIdentity.IMultiplicativeIdentity;
  //      }

  //      return numericCapabilityOperators;
  //    }
  //    public static GenericMathOperators GetGenericMathOperators(System.Type type)
  //    {
  //      var numericCapabilityOperators = GenericMathOperators.NoOperators;

  //      foreach (var itype in type.GetCachedInterfaces())
  //      {
  //        if (itype == typeof(System.Numerics.IAdditionOperators<,,>))
  //          numericCapabilityOperators |= GenericMathOperators.IAdditionOperators;
  //        else if (itype == typeof(System.Numerics.IBitwiseOperators<,,>))
  //          numericCapabilityOperators |= GenericMathOperators.IBitwiseOperators;
  //        else if (itype == typeof(System.Numerics.IComparisonOperators<,,>))
  //          numericCapabilityOperators |= GenericMathOperators.IComparisonOperators;
  //        else if (itype == typeof(System.Numerics.IDecrementOperators<>))
  //          numericCapabilityOperators |= GenericMathOperators.IDecrementOperators;
  //        else if (itype == typeof(System.Numerics.IDivisionOperators<,,>))
  //          numericCapabilityOperators |= GenericMathOperators.IDivisionOperators;
  //        else if (itype == typeof(System.Numerics.IEqualityOperators<,,>))
  //          numericCapabilityOperators |= GenericMathOperators.IEqualityOperators;
  //        else if (itype == typeof(System.Numerics.IIncrementOperators<>))
  //          numericCapabilityOperators |= GenericMathOperators.IIncrementOperators;
  //        else if (itype == typeof(System.Numerics.IModulusOperators<,,>))
  //          numericCapabilityOperators |= GenericMathOperators.IModulusOperators;
  //        else if (itype == typeof(System.Numerics.IMultiplyOperators<,,>))
  //          numericCapabilityOperators |= GenericMathOperators.IMultiplyOperators;
  //        else if (itype == typeof(System.Numerics.IShiftOperators<,,>))
  //          numericCapabilityOperators |= GenericMathOperators.IShiftOperators;
  //        else if (itype == typeof(System.Numerics.ISubtractionOperators<,,>))
  //          numericCapabilityOperators |= GenericMathOperators.ISubtractionOperators;
  //        else if (itype == typeof(System.Numerics.IUnaryNegationOperators<,>))
  //          numericCapabilityOperators |= GenericMathOperators.IUnaryNegationOperators;
  //        else if (itype == typeof(System.Numerics.IUnaryPlusOperators<,>))
  //          numericCapabilityOperators |= GenericMathOperators.IUnaryPlusOperators;
  //      }

  //      return numericCapabilityOperators;
  //    }
  //    public static GenericMathParsable GetGenericMathParsable(System.Type type)
  //    {
  //      var numericCapabilityOperators = GenericMathParsable.NoParsable;

  //      foreach (var itype in type.GetCachedInterfaces())
  //      {
  //        if (itype == typeof(System.IParsable<>))
  //          numericCapabilityOperators |= GenericMathParsable.IParsable;
  //        else if (itype == typeof(System.ISpanParsable<>))
  //          numericCapabilityOperators |= GenericMathParsable.ISpanParsable;
  //        else if (itype == typeof(System.IUtf8SpanParsable<>))
  //          numericCapabilityOperators |= GenericMathParsable.IUtf8SpanParsable;
  //      }

  //      return numericCapabilityOperators;
  //    }

  //    public static void PreloadNumericTypes()
  //    {
  //      foreach (var type in typeof(System.Numerics.INumberBase<>).GetDerived().Where(t => !t.IsInterface).OrderBy(t => t.FullName))
  //      {
  //        var gmi = Get(type);

  //#if DEBUG
  //        System.Diagnostics.Debug.WriteLine(gmi.ToString());
  //#endif
  //      }
  //    }

  //    private static class SizeOfCache<T> { public static readonly int Value = System.Runtime.CompilerServices.Unsafe.SizeOf<T>(); }
  //    private static int SizeOf(System.Type type) => (int)typeof(SizeOfCache<>).MakeGenericType(type).GetField(nameof(SizeOfCache<>.Value))!.GetValue(null)!;

  //    #endregion

  //    public override string ToString()
  //    {
  //      var sb = new System.Text.StringBuilder();

  //      sb.Append(m_type.FullName);

  //      if (m_type.GetCsharpAlias() is var keyword && !string.IsNullOrEmpty(keyword))
  //        sb.Append($" ({keyword})");

  //      // Main number system (classification) symbol
  //      if (NumberSystem != NumericDomain.Unknown)
  //        sb.AppendJoined($"{NumberSystemSymbol}");

  //      // Derived capabilities
  //      if (IsArbitraryPrecision) sb.AppendJoined(nameof(IsArbitraryPrecision));
  //      else if (IsFixedWidth) sb.AppendJoined($"{nameof(IsFixedWidth)} {BitWidth}-bit ({StorageBytes} byte{(Number.IsConsideredPlural(StorageBytes) ? "s" : string.Empty)})");
  //      if (IsSignedInteger) sb.AppendJoined(nameof(IsSignedInteger));
  //      else if (IsUnsignedInteger) sb.AppendJoined(nameof(IsUnsignedInteger));

  //      // Represented CLR types
  //      if (IsBFloat16) sb.AppendJoined(nameof(IsBFloat16));
  //      else if (IsBigInteger) sb.AppendJoined(nameof(IsBigInteger));
  //      else if (IsComplex) sb.AppendJoined(nameof(IsComplex));
  //      else if (IsDecimal) sb.AppendJoined(nameof(IsDecimal));

  //      // IEEE formats
  //      if (IsBinary16) sb.AppendJoined(nameof(IsBinary16));
  //      else if (IsBinary32) sb.AppendJoined(nameof(IsBinary32));
  //      else if (IsBinary64) sb.AppendJoined(nameof(IsBinary64));
  //      else if (IsDecimal128) sb.AppendJoined(nameof(IsDecimal128));
  //      else if (IsDecimal32) sb.AppendJoined(nameof(IsDecimal32));
  //      else if (IsDecimal64) sb.AppendJoined(nameof(IsDecimal64));

  //      // Integer types
  //      if (IsInteger128) sb.AppendJoined(nameof(IsInteger128));
  //      else if (IsInteger16) sb.AppendJoined(nameof(IsInteger16));
  //      else if (IsInteger32) sb.AppendJoined(nameof(IsInteger32));
  //      else if (IsInteger64) sb.AppendJoined(nameof(IsInteger64));
  //      else if (IsInteger8) sb.AppendJoined(nameof(IsInteger8));

  //      // Implemented capabilities
  //      sb.AppendJoined($"{Constants}");
  //      sb.AppendJoined($"{Core}");
  //      sb.AppendJoined($"{Formattable}");
  //      sb.AppendJoined($"{Functions}");
  //      sb.AppendJoined($"{Identity}");
  //      sb.AppendJoined($"{Operators}");
  //      sb.AppendJoined($"{Parsable}");

  //      return sb.ToString();
  //    }
  //  }
}
