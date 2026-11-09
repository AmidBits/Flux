namespace Flux
{
  #region enum GenericMath..

  [System.Flags]
  public enum GenericMathConstants : long
  {
    NoConstants = 0,

    IFloatingPointConstants = 0x0000_0000_0000_0001,
  }

  [System.Flags]
  public enum GenericMathCore : long
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

  [System.Flags]
  public enum GenericMathFormattable : long
  {
    NoFormattable = 0,

    IFormattable = 0x0000_0000_0000_0001,
    ISpanFormattable = 0x0000_0000_0000_0002,
    IUtf8SpanFormattable = 0x0000_0000_0000_0004,
  }

  [System.Flags]
  public enum GenericMathFunctions : long
  {
    NoFunctions = 0,

    IExponentialFunctions = 0x0000_0000_0000_0001,
    IHyperbolicFunctions = 0x0000_0000_0000_0002,
    ILogarithmicFunctions = 0x0000_0000_0000_0004,
    IPowerFunctions = 0x0000_0000_0000_0008,
    IRootFunctions = 0x0000_0000_0000_0010,
    ITrigonometricFunctions = 0x0000_0000_0000_0020,
  }

  [System.Flags]
  public enum GenericMathIdentity : long
  {
    NoIdentity = 0,

    IAdditiveIdentity = 0x0000_0000_0000_0001,
    IMultiplicativeIdentity = 0x0000_0000_0000_0002,
  }

  [System.Flags]
  public enum GenericMathOperators : long
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

  [System.Flags]
  public enum GenericMathParsable : long
  {
    NoParsable = 0,

    IParsable = 0x0000_0000_0000_0001,
    ISpanParsable = 0x0000_0000_0000_0002,
    IUtf8SpanParsable = 0x0000_0000_0000_0004,
  }

  #endregion

  public sealed class GenericMathInfo
  {
    private static readonly System.Collections.Concurrent.ConcurrentDictionary<Type, GenericMathInfo> m_cache = new();
    public static System.Collections.Generic.IReadOnlyDictionary<Type, GenericMathInfo> Cache => m_cache;

    private System.Type m_type;

    public GenericMathInfo(System.Type type)
    {
      m_type = type;

      Constants = GetGenericMathConstants(m_type);
      Core = GetGenericMathCore(m_type);
      Formattable = GetGenericMathFormattable(m_type);
      Functions = GetGenericMathFunctions(m_type);
      Identity = GetGenericMathIdentity(m_type);
      Operators = GetGenericMathOperators(m_type);
      Parsable = GetGenericMathParsable(m_type);
    }

    public System.Type Type
      => m_type;

    public System.Text.Rune NumberSystemClassificationSymbol
      => IsSignedInteger ? (System.Text.Rune)'\u2124' // "DOUBLE-STRUCK CAPITAL Z" for integers (includes negative whole numbers).
      : IsUnsignedInteger ? (System.Text.Rune)'\u2115' // "DOUBLE-STRUCK CAPITAL N" for natural numbers (non-negative or positive whole numbers, depending on convention).
      : ImplementsIFloatingPoint ? (System.Text.Rune)'\u211D' // "DOUBLE-STRUCK CAPITAL R" for real numbers (floating point numbers).
      : IsComplex ? (System.Text.Rune)'\u2102' // "DOUBLE-STRUCK CAPITAL C" for complex numbers.
      : default;


    #region Groups

    public GenericMathConstants Constants { get; }
    public GenericMathCore Core { get; }
    public GenericMathFormattable Formattable { get; }
    public GenericMathFunctions Functions { get; }
    public GenericMathIdentity Identity { get; }
    public GenericMathOperators Operators { get; }
    public GenericMathParsable Parsable { get; }

    #endregion

    #region Implements Capability Constants

    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IFloatingPointConstants{TSelf}"/>.</summary>
    public bool ImplementsIFloatingPointConstants => (Constants & GenericMathConstants.IFloatingPointConstants) != 0;

    #endregion

    #region Implements Capability Core

    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IBinaryFloatingPointIeee754{TSelf}"/>.</summary>
    public bool ImplementsIBinaryFloatingPointIeee754 => (Core & GenericMathCore.IBinaryFloatingPointIeee754) != 0;
    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IBinaryInteger{TSelf}"/>.</summary>
    public bool ImplementsIBinaryInteger => (Core & GenericMathCore.IBinaryInteger) != 0;
    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IBinaryNumber{TSelf}"/>.</summary>
    public bool ImplementsIBinaryNumber => (Core & GenericMathCore.IBinaryNumber) != 0;
    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IDecimalFloatingPointIeee754{TSelf}"/>.</summary>
    public bool ImplementsIDecimalFloatingPointIeee754 => (Core & GenericMathCore.IDecimalFloatingPointIeee754) != 0;
    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IFloatingPoint{TSelf}"/>.</summary>
    public bool ImplementsIFloatingPoint => (Core & GenericMathCore.IFloatingPoint) != 0;
    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IFloatingPointIeee754{TSelf}"/>.</summary>
    public bool ImplementsIFloatingPointIeee754 => (Core & GenericMathCore.IFloatingPointIeee754) != 0;
    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IMinMaxValue{TSelf}"/>.</summary>
    public bool ImplementsIMinMaxValue => (Core & GenericMathCore.IMinMaxValue) != 0;
    /// <summary>Indicates whether the type implements <see cref="System.Numerics.INumber{TSelf}"/>.</summary>
    public bool ImplementsINumber => (Core & GenericMathCore.INumber) != 0;
    /// <summary>Indicates whether the type implements <see cref="System.Numerics.INumberBase{TSelf}"/>.</summary>
    public bool ImplementsINumberBase => (Core & GenericMathCore.INumberBase) != 0;
    /// <summary>Indicates whether the type implements <see cref="System.Numerics.ISignedNumber{TSelf}"/>.</summary>
    public bool ImplementsISignedNumber => (Core & GenericMathCore.ISignedNumber) != 0;
    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IUnsignedNumber{TSelf}"/>.</summary>
    public bool ImplementsIUnsignedNumber => (Core & GenericMathCore.IUnsignedNumber) != 0;

    #endregion

    #region Implements Capability Formattable

    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IFormattable"/>.</summary>
    public bool ImplementsIFormattable => (Formattable & GenericMathFormattable.IFormattable) != 0;
    /// <summary>Indicates whether the type implements <see cref="System.Numerics.ISpanFormattable"/>.</summary>
    public bool ImplementsISpanFormattable => (Formattable & GenericMathFormattable.ISpanFormattable) != 0;
    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IUtf8SpanFormattable"/>.</summary>
    public bool ImplementsIUtf8SpanFormattable => (Formattable & GenericMathFormattable.IUtf8SpanFormattable) != 0;

    #endregion

    #region Implements Capability Functions

    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IExponentialFunctions{TSelf}"/>.</summary>
    public bool ImplementsIExponentialFunctions => (Functions & GenericMathFunctions.IExponentialFunctions) != 0;
    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IHyperbolicFunctions{TSelf}"/>.</summary>
    public bool ImplementsIHyperbolicFunctions => (Functions & GenericMathFunctions.IHyperbolicFunctions) != 0;
    /// <summary>Indicates whether the type implements <see cref="System.Numerics.ILogarithmicFunctions{TSelf}"/>.</summary>
    public bool ImplementsILogarithmicFunctions => (Functions & GenericMathFunctions.ILogarithmicFunctions) != 0;
    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IPowerFunctions{TSelf}"/>.</summary>
    public bool ImplementsIPowerFunctions => (Functions & GenericMathFunctions.IPowerFunctions) != 0;
    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IRootFunctions{TSelf}"/>.</summary>
    public bool ImplementsIRootFunctions => (Functions & GenericMathFunctions.IRootFunctions) != 0;
    /// <summary>Indicates whether the type implements <see cref="System.Numerics.ITrigonometricFunctions{TSelf}"/>.</summary>
    public bool ImplementsITrigonometricFunctions => (Functions & GenericMathFunctions.ITrigonometricFunctions) != 0;

    #endregion

    #region Implements Capability Identity

    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IAdditiveIdentity{TSelf, TResult}"/>.</summary>
    public bool ImplementsIAdditiveIdentity => (Identity & GenericMathIdentity.IAdditiveIdentity) != 0;
    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IMultiplicativeIdentity{TSelf, TResult}"/>.</summary>
    public bool ImplementsIMultiplicativeIdentity => (Identity & GenericMathIdentity.IMultiplicativeIdentity) != 0;

    #endregion

    #region Implements Capability Operators

    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IAdditionOperators{TSelf, TOther, TResult}"/>.</summary>
    public bool ImplementsIAdditionOperators => (Operators & GenericMathOperators.IAdditionOperators) != 0;
    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IBitwiseOperators{TSelf, TOther, TResult}"/>.</summary>
    public bool ImplementsIBitwiseOperators => (Operators & GenericMathOperators.IBitwiseOperators) != 0;
    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IComparisonOperators{TSelf, TOther, TResult}"/>.</summary>
    public bool ImplementsIComparisonOperators => (Operators & GenericMathOperators.IComparisonOperators) != 0;
    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IDecrementOperators{TSelf}"/>.</summary>
    public bool ImplementsIDecrementOperators => (Operators & GenericMathOperators.IDecrementOperators) != 0;
    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IDivisionOperators{TSelf, TOther, TResult}"/>.</summary>
    public bool ImplementsIDivisionOperators => (Operators & GenericMathOperators.IDivisionOperators) != 0;
    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IEqualityOperators{TSelf, TOther, TResult}"/>.</summary>
    public bool ImplementsIEqualityOperators => (Operators & GenericMathOperators.IEqualityOperators) != 0;
    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IIncrementOperators{TSelf}"/>.</summary>
    public bool ImplementsIIncrementOperators => (Operators & GenericMathOperators.IIncrementOperators) != 0;
    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IModulusOperators{TSelf, TOther, TResult}"/>.</summary>
    public bool ImplementsIModulusOperators => (Operators & GenericMathOperators.IModulusOperators) != 0;
    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IMultiplyOperators{TSelf, TOther, TResult}"/>.</summary>
    public bool ImplementsIMultiplyOperators => (Operators & GenericMathOperators.IMultiplyOperators) != 0;
    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IShiftOperators{TSelf, TOther, TResult}"/>.</summary>
    public bool ImplementsIShiftOperators => (Operators & GenericMathOperators.IShiftOperators) != 0;
    /// <summary>Indicates whether the type implements <see cref="System.Numerics.ISubtractionOperators{TSelf, TOther, TResult}"/>.</summary>
    public bool ImplementsISubtractionOperators => (Operators & GenericMathOperators.ISubtractionOperators) != 0;
    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IUnaryNegationOperators{TSelf, TResult}"/>.</summary>
    public bool ImplementsIUnaryNegationOperators => (Operators & GenericMathOperators.IUnaryNegationOperators) != 0;
    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IUnaryPlusOperators{TSelf, TResult}"/>.</summary>
    public bool ImplementsIUnaryPlusOperators => (Operators & GenericMathOperators.IUnaryPlusOperators) != 0;

    #endregion

    #region Implements Capability Parsable

    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IParsable{TSelf}"/>.</summary>
    public bool ImplementsIParsable => (Parsable & GenericMathParsable.IParsable) != 0;
    /// <summary>Indicates whether the type implements <see cref="System.Numerics.ISpanParsable{TSelf}"/>.</summary>
    public bool ImplementsISpanParsable => (Parsable & GenericMathParsable.ISpanParsable) != 0;
    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IUtf8SpanParsable{TSelf}"/>.</summary>
    public bool ImplementsIUtf8SpanParsable => (Parsable & GenericMathParsable.IUtf8SpanParsable) != 0;

    #endregion

    #region Is CLR Concrete Types

    /// <summary>Indicates whether the type represents <see cref="System.Numerics.BFloat16"/>.</summary>
#if NET11_0_OR_GREATER
    public bool IsBFloat16
      => m_type == typeof(System.Numerics.BFloat16);
#else
    public bool IsBFloat16
      => false;
#endif

    /// <summary>Indicates whether the type represents <see cref="System.Numerics.BigInteger"/>.</summary>
    public bool IsBigInteger
      => m_type == typeof(System.Numerics.BigInteger);

    /// <summary>Indicates whether the type represents <see cref="System.Numerics.Complex"/>.</summary>
    public bool IsComplex
      => m_type == typeof(System.Numerics.Complex);

    /// <summary>Indicates whether the type represents <see cref="System.Decimal"/>.</summary>
    public bool IsDecimal
      => m_type == typeof(System.Decimal);

    #endregion

    #region IsBinary.. IEEE Formats

    /// <summary>Indicates whether the type represents an IEEE binary16 floating-point, such as <see cref="System.Half"/></summary>
    public bool IsBinary16
      => m_type == typeof(System.Half);

    /// <summary>Indicates whether the type represents an IEEE binary32 floating-point, such as <see cref="System.Single"/> or <see cref="System.Runtime.InteropServices.NFloat"/> when <see cref="System.Runtime.InteropServices.NFloat.Size"/> is 4.</summary>
    public bool IsBinary32
      => m_type == typeof(System.Single) || (m_type == typeof(System.Runtime.InteropServices.NFloat) && System.Runtime.InteropServices.NFloat.Size == 4);

    /// <summary>Indicates whether the type represents an IEEE binary64 floating-point, such as <see cref="System.Double"/> or <see cref="System.Runtime.InteropServices.NFloat"/> when <see cref="System.Runtime.InteropServices.NFloat.Size"/> is 8.</summary>
    public bool IsBinary64
      => m_type == typeof(System.Double) || (m_type == typeof(System.Runtime.InteropServices.NFloat) && System.Runtime.InteropServices.NFloat.Size == 8);

    /// <summary>Indicates whether the type represents an IEEE binary128 floating-point. There are currently no such types in .NET.</summary>
    public bool IsBinary128
      => false;

    /// <summary>Indicates whether the type represents an IEEE decimal32 floating-point, such as <see cref="System.Decimal32"/>.</summary>
#if NET11_0_OR_GREATER
    public bool IsDecimal32
      => m_type == typeof(System.Numerics.Decimal32);
#else
    public bool IsDecimal32
      => false;
#endif

    /// <summary>Indicates whether the type represents an IEEE decimal64 floating-point, such as <see cref="System.Decimal64"/>.</summary>
#if NET11_0_OR_GREATER
    public bool IsDecimal64
      => m_type == typeof(System.Numerics.Decimal64);
#else
    public bool IsDecimal64
      => false;
#endif

    /// <summary>Indicates whether the type represents an IEEE decimal128 floating-point, such as <see cref="System.Decimal128"/>.</summary>
#if NET11_0_OR_GREATER
    public bool IsDecimal128
      => m_type == typeof(System.Numerics.Decimal128);
#else
    public bool IsDecimal128
      => false;
#endif

    #endregion

    #region IsInteger.. Fixed Width

    /// <summary>Indicates whether the type represents an 8-bit integer type, such as <see cref="System.Byte"/> or <see cref="System.SByte"/>.</summary>
    public bool IsInteger8
      => m_type == typeof(System.Byte) || m_type == typeof(System.SByte);

    /// <summary>Indicates whether the type represents a 16-bit integer type, such as <see cref="System.Int16"/> or <see cref="System.UInt16"/>.</summary>
    public bool IsInteger16
      => m_type == typeof(System.Int16) || m_type == typeof(System.UInt16);

    /// <summary>Indicates whether the type represents a 32-bit integer type, such as <see cref="System.Int32"/>, <see cref="System.UInt32"/>, <see cref="System.IntPtr"/> (when <see cref="System.IntPtr.Size"/> is 4), or <see cref="System.UIntPtr"/> (when <see cref="System.UIntPtr.Size"/> is 4).</summary>
    public bool IsInteger32
      => m_type == typeof(System.Int32) || m_type == typeof(System.UInt32) || (m_type == typeof(System.IntPtr) && System.IntPtr.Size == 4) || (m_type == typeof(System.UIntPtr) && System.UIntPtr.Size == 4);

    /// <summary>Indicates whether the type represents a 64-bit integer type, such as <see cref="System.Int64"/>, <see cref="System.UInt64"/>, <see cref="System.IntPtr"/> (when <see cref="System.IntPtr.Size"/> is 8), or <see cref="System.UIntPtr"/> (when <see cref="System.UIntPtr.Size"/> is 8).</summary>
    public bool IsInteger64
      => m_type == typeof(System.Int64) || m_type == typeof(System.UInt64) || (m_type == typeof(System.IntPtr) && System.IntPtr.Size == 8) || (m_type == typeof(System.UIntPtr) && System.UIntPtr.Size == 8);

    /// <summary>Indicates whether the type represents a 128-bit integer type, such as <see cref="System.Int128"/> or <see cref="System.UInt128"/>.</summary>
    public bool IsInteger128
      => m_type == typeof(System.Int128) || m_type == typeof(System.UInt128);

    #endregion

    #region Derived Capabilities

    /// <summary>The number of storage bytes used by the type.</summary>
    /// <remarks>This does not include implementation-dependent padding, object headers, or additional heap allocations (for example, <see cref="System.Numerics.BigInteger"/>).</remarks>
    public int StorageBytes
      => IsInteger8 ? 1
      : (IsBFloat16 || IsBinary16 || IsInteger16) ? 2
      : (IsBinary32 || IsDecimal32 || IsInteger32) ? 4
      : (IsBinary64 || IsDecimal64 || IsInteger64) ? 8
      : (IsBinary128 || IsDecimal128 || IsInteger128) ? 16
      : SizeOf(m_type);

    /// <summary>The width of the type in bits.</summary>
    /// <remarks>For fixed-width types, this equals <see cref="StorageBytes"/> × 8. If <see cref="IsArbitraryPrecision"/> is <see langword="true"/>, this value is 0, indicating an unbounded bit-width.</remarks>
    public int BitWidth => IsArbitraryPrecision ? 0 : StorageBytes * 8;

    /// <summary>Indicates whether the type has an unbounded bit-width, such as <see cref="System.Numerics.BigInteger"/>.</summary>
    public bool IsArbitraryPrecision => IsBigInteger;

    /// <summary>Indicates whether the type has a fixed bit-width, such as <see cref="System.Decimal"/>, <see cref="System.Decimal128"/>, <see cref="System.Int32"/>, or <see cref="System.Single"/>.</summary>
    public bool IsFixedWidth => !IsBigInteger;

    /// <summary>Indicates whether the type represents a signed integer, such as <see cref="System.Int128"/>, <see cref="System.Int16"/>, <see cref="System.Int32"/>, <see cref="System.Int64"/>, <see cref="System.SByte"/>, or <see cref="System.Numerics.BigInteger"/>.</summary>
    public bool IsSignedInteger => (Core & (GenericMathCore.IBinaryInteger | GenericMathCore.ISignedNumber)) == (GenericMathCore.IBinaryInteger | GenericMathCore.ISignedNumber);

    /// <summary>Indicates whether the type represents an unsigned integer , such as <see cref="System.Byte"/>, <see cref="System.UInt128"/>, <see cref="System.UInt16"/>, <see cref="System.UInt32"/>, or <see cref="System.UInt64"/>.</summary>
    public bool IsUnsignedInteger => (Core & (GenericMathCore.IBinaryInteger | GenericMathCore.IUnsignedNumber)) == (GenericMathCore.IBinaryInteger | GenericMathCore.IUnsignedNumber);

    #endregion

    #region Static methods

    public static GenericMathInfo Get(Type type) => m_cache.GetOrAdd(type, static t => new GenericMathInfo(t));
    public static GenericMathInfo Get<TNumberBase>() where TNumberBase : System.Numerics.INumberBase<TNumberBase>
      => Get(typeof(TNumberBase));

    public static GenericMathConstants GetGenericMathConstants(System.Type type)
    {
      var numericCapabilityOperators = GenericMathConstants.NoConstants;

      foreach (var itype in type.GetInterfacesWithCache())
      {
        if (itype == typeof(System.Numerics.IFloatingPointConstants<>))
          numericCapabilityOperators |= GenericMathConstants.IFloatingPointConstants;
      }

      return numericCapabilityOperators;
    }
    public static GenericMathCore GetGenericMathCore(System.Type type)
    {
      var numericCapabilityOperators = GenericMathCore.NoCore;

      foreach (var itype in type.GetInterfacesWithCache())
      {
#if NET11_0_OR_GREATER
          if (itype == typeof(System.Numerics.Complex<>))
            numericCapabilities |= NumericCapabilityCore.ComplexT;
          else 
#endif
        if (itype == typeof(System.Numerics.IBinaryFloatingPointIeee754<>))
          numericCapabilityOperators |= GenericMathCore.IBinaryFloatingPointIeee754;
        else if (itype == typeof(System.Numerics.IBinaryInteger<>))
          numericCapabilityOperators |= GenericMathCore.IBinaryInteger;
        else if (itype == typeof(System.Numerics.IBinaryNumber<>))
          numericCapabilityOperators |= GenericMathCore.IBinaryNumber;
#if NET11_0_OR_GREATER
          else if (itype == typeof(System.Numerics.IDecimalFloatingPointIeee754<>))
            numericCapabilities |= NumericCapabilityCore.IDecimalFloatingPointIeee754;
#endif
        else if (itype == typeof(System.Numerics.IFloatingPoint<>))
          numericCapabilityOperators |= GenericMathCore.IFloatingPoint;
        else if (itype == typeof(System.Numerics.IFloatingPointIeee754<>))
          numericCapabilityOperators |= GenericMathCore.IFloatingPointIeee754;
        else if (itype == typeof(System.Numerics.IMinMaxValue<>))
          numericCapabilityOperators |= GenericMathCore.IMinMaxValue;
        else if (itype == typeof(System.Numerics.INumber<>))
          numericCapabilityOperators |= GenericMathCore.INumber;
        else if (itype == typeof(System.Numerics.INumberBase<>))
          numericCapabilityOperators |= GenericMathCore.INumberBase;
        else if (itype == typeof(System.Numerics.ISignedNumber<>))
          numericCapabilityOperators |= GenericMathCore.ISignedNumber;
        else if (itype == typeof(System.Numerics.IUnsignedNumber<>))
          numericCapabilityOperators |= GenericMathCore.IUnsignedNumber;
      }

      return numericCapabilityOperators;
    }
    public static GenericMathFormattable GetGenericMathFormattable(System.Type type)
    {
      var numericCapabilityOperators = GenericMathFormattable.NoFormattable;

      foreach (var itype in type.GetInterfacesWithCache())
      {
        if (itype == typeof(System.IFormattable))
          numericCapabilityOperators |= GenericMathFormattable.IFormattable;
        else if (itype == typeof(System.ISpanFormattable))
          numericCapabilityOperators |= GenericMathFormattable.ISpanFormattable;
        else if (itype == typeof(System.IUtf8SpanFormattable))
          numericCapabilityOperators |= GenericMathFormattable.IUtf8SpanFormattable;
      }

      return numericCapabilityOperators;
    }
    public static GenericMathFunctions GetGenericMathFunctions(System.Type type)
    {
      var numericCapabilityOperators = GenericMathFunctions.NoFunctions;

      foreach (var itype in type.GetInterfacesWithCache())
      {
        if (itype == typeof(System.Numerics.IExponentialFunctions<>))
          numericCapabilityOperators |= GenericMathFunctions.IExponentialFunctions;
        else if (itype == typeof(System.Numerics.IHyperbolicFunctions<>))
          numericCapabilityOperators |= GenericMathFunctions.IHyperbolicFunctions;
        else if (itype == typeof(System.Numerics.ILogarithmicFunctions<>))
          numericCapabilityOperators |= GenericMathFunctions.ILogarithmicFunctions;
        else if (itype == typeof(System.Numerics.IPowerFunctions<>))
          numericCapabilityOperators |= GenericMathFunctions.IPowerFunctions;
        else if (itype == typeof(System.Numerics.IRootFunctions<>))
          numericCapabilityOperators |= GenericMathFunctions.IRootFunctions;
        else if (itype == typeof(System.Numerics.ITrigonometricFunctions<>))
          numericCapabilityOperators |= GenericMathFunctions.ITrigonometricFunctions;
      }

      return numericCapabilityOperators;
    }
    public static GenericMathIdentity GetGenericMathIdentity(System.Type type)
    {
      var numericCapabilityOperators = GenericMathIdentity.NoIdentity;

      foreach (var itype in type.GetInterfacesWithCache())
      {
        if (itype == typeof(System.Numerics.IAdditiveIdentity<,>))
          numericCapabilityOperators |= GenericMathIdentity.IAdditiveIdentity;
        else if (itype == typeof(System.Numerics.IMultiplicativeIdentity<,>))
          numericCapabilityOperators |= GenericMathIdentity.IMultiplicativeIdentity;
      }

      return numericCapabilityOperators;
    }
    public static GenericMathOperators GetGenericMathOperators(System.Type type)
    {
      var numericCapabilityOperators = GenericMathOperators.NoOperators;

      foreach (var itype in type.GetInterfacesWithCache())
      {
        if (itype == typeof(System.Numerics.IAdditionOperators<,,>))
          numericCapabilityOperators |= GenericMathOperators.IAdditionOperators;
        else if (itype == typeof(System.Numerics.IBitwiseOperators<,,>))
          numericCapabilityOperators |= GenericMathOperators.IBitwiseOperators;
        else if (itype == typeof(System.Numerics.IComparisonOperators<,,>))
          numericCapabilityOperators |= GenericMathOperators.IComparisonOperators;
        else if (itype == typeof(System.Numerics.IDecrementOperators<>))
          numericCapabilityOperators |= GenericMathOperators.IDecrementOperators;
        else if (itype == typeof(System.Numerics.IDivisionOperators<,,>))
          numericCapabilityOperators |= GenericMathOperators.IDivisionOperators;
        else if (itype == typeof(System.Numerics.IEqualityOperators<,,>))
          numericCapabilityOperators |= GenericMathOperators.IEqualityOperators;
        else if (itype == typeof(System.Numerics.IIncrementOperators<>))
          numericCapabilityOperators |= GenericMathOperators.IIncrementOperators;
        else if (itype == typeof(System.Numerics.IModulusOperators<,,>))
          numericCapabilityOperators |= GenericMathOperators.IModulusOperators;
        else if (itype == typeof(System.Numerics.IMultiplyOperators<,,>))
          numericCapabilityOperators |= GenericMathOperators.IMultiplyOperators;
        else if (itype == typeof(System.Numerics.IShiftOperators<,,>))
          numericCapabilityOperators |= GenericMathOperators.IShiftOperators;
        else if (itype == typeof(System.Numerics.ISubtractionOperators<,,>))
          numericCapabilityOperators |= GenericMathOperators.ISubtractionOperators;
        else if (itype == typeof(System.Numerics.IUnaryNegationOperators<,>))
          numericCapabilityOperators |= GenericMathOperators.IUnaryNegationOperators;
        else if (itype == typeof(System.Numerics.IUnaryPlusOperators<,>))
          numericCapabilityOperators |= GenericMathOperators.IUnaryPlusOperators;
      }

      return numericCapabilityOperators;
    }
    public static GenericMathParsable GetGenericMathParsable(System.Type type)
    {
      var numericCapabilityOperators = GenericMathParsable.NoParsable;

      foreach (var itype in type.GetInterfacesWithCache())
      {
        if (itype == typeof(System.IParsable<>))
          numericCapabilityOperators |= GenericMathParsable.IParsable;
        else if (itype == typeof(System.ISpanParsable<>))
          numericCapabilityOperators |= GenericMathParsable.ISpanParsable;
        else if (itype == typeof(System.IUtf8SpanParsable<>))
          numericCapabilityOperators |= GenericMathParsable.IUtf8SpanParsable;
      }

      return numericCapabilityOperators;
    }

    public static void PreloadNumericTypes()
    {
      foreach (var type in typeof(System.Numerics.INumberBase<>).GetDerived().Where(t => !t.IsInterface).OrderBy(t => t.FullName))
      {
        var s = PreloadType(type);

#if DEBUG
        System.Diagnostics.Debug.WriteLine(s);
#endif
      }

      static string? PreloadType(System.Type numericType)
        => Flux.GenericMathInfo.Get(numericType).ToString();
    }

    private static class SizeOfCache<T> { public static readonly int Value = System.Runtime.CompilerServices.Unsafe.SizeOf<T>(); }
    private static int SizeOf(System.Type type) => (int)typeof(SizeOfCache<>).MakeGenericType(type).GetField(nameof(SizeOfCache<>.Value))!.GetValue(null)!;

    #endregion

    public override string ToString()
    {
      var sb = new System.Text.StringBuilder();

      sb.Append(m_type.FullName);

      // Derived capabilities
      if (IsArbitraryPrecision) sb.AppendJoined(nameof(IsArbitraryPrecision));
      else if (IsFixedWidth) sb.AppendJoined($"{nameof(IsFixedWidth)} {BitWidth}-bit ({StorageBytes} byte{(Number.IsConsideredPlural(StorageBytes) ? "s" : string.Empty)})");
      if (IsSignedInteger) sb.AppendJoined(nameof(IsSignedInteger));
      else if (IsUnsignedInteger) sb.AppendJoined(nameof(IsUnsignedInteger));

      // Represented CLR types
      if (IsBFloat16) sb.AppendJoined(nameof(IsBFloat16));
      else if (IsBigInteger) sb.AppendJoined(nameof(IsBigInteger));
      else if (IsComplex) sb.AppendJoined(nameof(IsComplex));
      else if (IsDecimal) sb.AppendJoined(nameof(IsDecimal));

      // IEEE formats
      if (IsBinary16) sb.AppendJoined(nameof(IsBinary16));
      else if (IsBinary32) sb.AppendJoined(nameof(IsBinary32));
      else if (IsBinary64) sb.AppendJoined(nameof(IsBinary64));
      else if (IsDecimal128) sb.AppendJoined(nameof(IsDecimal128));
      else if (IsDecimal32) sb.AppendJoined(nameof(IsDecimal32));
      else if (IsDecimal64) sb.AppendJoined(nameof(IsDecimal64));

      // Integer types
      if (IsInteger128) sb.AppendJoined(nameof(IsInteger128));
      else if (IsInteger16) sb.AppendJoined(nameof(IsInteger16));
      else if (IsInteger32) sb.AppendJoined(nameof(IsInteger32));
      else if (IsInteger64) sb.AppendJoined(nameof(IsInteger64));
      else if (IsInteger8) sb.AppendJoined(nameof(IsInteger8));

      // Implemented capabilities
      sb.AppendJoined($"{Constants}");
      sb.AppendJoined($"{Core}");
      sb.AppendJoined($"{Formattable}");
      sb.AppendJoined($"{Functions}");
      sb.AppendJoined($"{Identity}");
      sb.AppendJoined($"{Operators}");
      sb.AppendJoined($"{Parsable}");

      return sb.ToString();
    }
  }
}
