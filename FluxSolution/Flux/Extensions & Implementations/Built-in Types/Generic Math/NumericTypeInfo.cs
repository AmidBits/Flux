namespace Flux
{
  using System.Collections.Frozen;

  public static class NumericTypeInfo<TNumberBase>
    where TNumberBase : System.Numerics.INumberBase<TNumberBase>
  {
    private static readonly System.Type m_type = typeof(TNumberBase);

    #region Lookup Tables

    public enum NumberBaseType : long
    {
      Unknown = 0,

      BFloat16,
      BigInteger,
      BigRational,
      Byte,
      Complex,
      ComplexT,
      Decimal,
      Decimal32,
      Decimal64,
      Decimal128,
      Double,
      Half,
      Int16,
      Int32,
      Int64,
      Int128,
      IntPtr,
      NFloat,
      SByte,
      Single,
      UInt16,
      UInt32,
      UInt64,
      UInt128,
      UIntPtr,
    }

    private static readonly FrozenDictionary<Type, NumberBaseType> m_numberBaseType
      = new Dictionary<Type, NumberBaseType>
      {
        [typeof(System.Numerics.BFloat16)] = NumberBaseType.BFloat16,
        [typeof(System.Numerics.BigInteger)] = NumberBaseType.BigInteger,
        [typeof(System.Byte)] = NumberBaseType.Byte,
        [typeof(System.Numerics.Complex)] = NumberBaseType.Complex,
        [typeof(System.Numerics.Complex<>)] = NumberBaseType.ComplexT,
        [typeof(System.Decimal)] = NumberBaseType.Decimal,
        [typeof(System.Numerics.Decimal32)] = NumberBaseType.Decimal32,
        [typeof(System.Numerics.Decimal64)] = NumberBaseType.Decimal64,
        [typeof(System.Numerics.Decimal128)] = NumberBaseType.Decimal128,
        [typeof(System.Double)] = NumberBaseType.Double,
        [typeof(System.Half)] = NumberBaseType.Half,
        [typeof(System.Int16)] = NumberBaseType.Int16,
        [typeof(System.Int32)] = NumberBaseType.Int32,
        [typeof(System.Int64)] = NumberBaseType.Int64,
        [typeof(System.Int128)] = NumberBaseType.Int128,
        [typeof(System.IntPtr)] = NumberBaseType.IntPtr,
        [typeof(System.Runtime.InteropServices.NFloat)] = NumberBaseType.NFloat,
        [typeof(System.SByte)] = NumberBaseType.SByte,
        [typeof(System.Single)] = NumberBaseType.Single,
        [typeof(System.UInt16)] = NumberBaseType.UInt16,
        [typeof(System.UInt32)] = NumberBaseType.UInt32,
        [typeof(System.UInt64)] = NumberBaseType.UInt64,
        [typeof(System.UInt128)] = NumberBaseType.UInt128,
        [typeof(System.UIntPtr)] = NumberBaseType.UIntPtr,
        [typeof(Numerics.BigRational)] = NumberBaseType.BigRational,
      }.ToFrozenDictionary();

    [System.Flags]
    public enum NumericConstants
    {
      NoConstants = 0,

      IFloatingPointConstants = 0x0000_0000_0000_0001,
    }

    private static readonly FrozenDictionary<Type, NumericConstants> m_constantsMap
      = new Dictionary<Type, NumericConstants>
      {
        [typeof(System.Numerics.IFloatingPointConstants<>)] = NumericConstants.IFloatingPointConstants,
      }.ToFrozenDictionary();

    [System.Flags]
    public enum NumericCore
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

    private static readonly FrozenDictionary<Type, NumericCore> m_coreMap
      = new Dictionary<Type, NumericCore>
      {
        [typeof(System.Numerics.Complex<>)] = NumericCore.ComplexT,
        [typeof(System.Numerics.IBinaryFloatingPointIeee754<>)] = NumericCore.IBinaryFloatingPointIeee754,
        [typeof(System.Numerics.IBinaryInteger<>)] = NumericCore.IBinaryInteger,
        [typeof(System.Numerics.IBinaryNumber<>)] = NumericCore.IBinaryNumber,
        [typeof(System.Numerics.IDecimalFloatingPointIeee754<>)] = NumericCore.IDecimalFloatingPointIeee754,
        [typeof(System.Numerics.IFloatingPoint<>)] = NumericCore.IFloatingPoint,
        [typeof(System.Numerics.IFloatingPointIeee754<>)] = NumericCore.IFloatingPointIeee754,
        [typeof(System.Numerics.IMinMaxValue<>)] = NumericCore.IMinMaxValue,
        [typeof(System.Numerics.INumber<>)] = NumericCore.INumber,
        [typeof(System.Numerics.INumberBase<>)] = NumericCore.INumberBase,
        [typeof(System.Numerics.ISignedNumber<>)] = NumericCore.ISignedNumber,
        [typeof(System.Numerics.IUnsignedNumber<>)] = NumericCore.IUnsignedNumber,
      }.ToFrozenDictionary();

    [System.Flags]
    public enum NumericFormattable
    {
      NotFormattable = 0,

      IFormattable = 0x0000_0000_0000_0001,
      ISpanFormattable = 0x0000_0000_0000_0002,
      IUtf8SpanFormattable = 0x0000_0000_0000_0004,
    }

    private static readonly FrozenDictionary<Type, NumericFormattable> m_formattableMap
      = new Dictionary<Type, NumericFormattable>
      {
        [typeof(System.IFormattable)] = NumericFormattable.IFormattable,
        [typeof(System.ISpanFormattable)] = NumericFormattable.ISpanFormattable,
        [typeof(System.IUtf8SpanFormattable)] = NumericFormattable.IUtf8SpanFormattable,
      }.ToFrozenDictionary();

    [System.Flags]
    public enum NumericFunctions
    {
      NoFunctions = 0,

      IExponentialFunctions = 0x0000_0000_0000_0001,
      IHyperbolicFunctions = 0x0000_0000_0000_0002,
      ILogarithmicFunctions = 0x0000_0000_0000_0004,
      IPowerFunctions = 0x0000_0000_0000_0008,
      IRootFunctions = 0x0000_0000_0000_0010,
      ITrigonometricFunctions = 0x0000_0000_0000_0020,
    }

    private static readonly FrozenDictionary<Type, NumericFunctions> m_functionsMap
      = new Dictionary<Type, NumericFunctions>
      {
        [typeof(System.Numerics.IExponentialFunctions<>)] = NumericFunctions.IExponentialFunctions,
        [typeof(System.Numerics.IHyperbolicFunctions<>)] = NumericFunctions.IHyperbolicFunctions,
        [typeof(System.Numerics.ILogarithmicFunctions<>)] = NumericFunctions.ILogarithmicFunctions,
        [typeof(System.Numerics.IPowerFunctions<>)] = NumericFunctions.IPowerFunctions,
        [typeof(System.Numerics.IRootFunctions<>)] = NumericFunctions.IRootFunctions,
        [typeof(System.Numerics.ITrigonometricFunctions<>)] = NumericFunctions.ITrigonometricFunctions,
      }.ToFrozenDictionary();

    [System.Flags]
    public enum NumericIdentity
    {
      NoIdentities = 0,

      IAdditiveIdentity = 0x0000_0000_0000_0001,
      IMultiplicativeIdentity = 0x0000_0000_0000_0002,
    }

    private static readonly FrozenDictionary<Type, NumericIdentity> m_identityMap
      = new Dictionary<Type, NumericIdentity>
      {
        [typeof(System.Numerics.IAdditiveIdentity<,>)] = NumericIdentity.IAdditiveIdentity,
        [typeof(System.Numerics.IMultiplicativeIdentity<,>)] = NumericIdentity.IMultiplicativeIdentity,
      }.ToFrozenDictionary();

    [System.Flags]
    public enum NumericOperators
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

    private static readonly FrozenDictionary<Type, NumericOperators> m_operatorsMap
      = new Dictionary<Type, NumericOperators>
      {
        [typeof(System.Numerics.IAdditionOperators<,,>)] = NumericOperators.IAdditionOperators,
        [typeof(System.Numerics.IBitwiseOperators<,,>)] = NumericOperators.IBitwiseOperators,
        [typeof(System.Numerics.IComparisonOperators<,,>)] = NumericOperators.IComparisonOperators,
        [typeof(System.Numerics.IDecrementOperators<>)] = NumericOperators.IDecrementOperators,
        [typeof(System.Numerics.IDivisionOperators<,,>)] = NumericOperators.IDivisionOperators,
        [typeof(System.Numerics.IEqualityOperators<,,>)] = NumericOperators.IEqualityOperators,
        [typeof(System.Numerics.IIncrementOperators<>)] = NumericOperators.IIncrementOperators,
        [typeof(System.Numerics.IModulusOperators<,,>)] = NumericOperators.IModulusOperators,
        [typeof(System.Numerics.IMultiplyOperators<,,>)] = NumericOperators.IMultiplyOperators,
        [typeof(System.Numerics.IShiftOperators<,,>)] = NumericOperators.IShiftOperators,
        [typeof(System.Numerics.ISubtractionOperators<,,>)] = NumericOperators.ISubtractionOperators,
        [typeof(System.Numerics.IUnaryNegationOperators<,>)] = NumericOperators.IUnaryNegationOperators,
        [typeof(System.Numerics.IUnaryPlusOperators<,>)] = NumericOperators.IUnaryPlusOperators,
      }.ToFrozenDictionary();

    [System.Flags]
    public enum NumericParsable
    {
      NotParsable = 0,

      IParsable = 0x0000_0000_0000_0001,
      ISpanParsable = 0x0000_0000_0000_0002,
      IUtf8SpanParsable = 0x0000_0000_0000_0004,
    }

    private static readonly System.Collections.Frozen.FrozenDictionary<Type, NumericParsable> m_parsableMap
      = new Dictionary<Type, NumericParsable>
      {
        [typeof(System.IParsable<>)] = NumericParsable.IParsable,
        [typeof(System.ISpanParsable<>)] = NumericParsable.ISpanParsable,
        [typeof(System.IUtf8SpanParsable<>)] = NumericParsable.IUtf8SpanParsable,
      }.ToFrozenDictionary();

    #endregion

    static NumericTypeInfo()
    {
      foreach (var interfaceType in typeof(TNumberBase).GetCachedInterfaces()) // GetCachedInterfaces expands generic type descriptions.
      {
        if (m_constantsMap.TryGetValue(interfaceType, out var constants))
          Constants |= constants;

        if (m_coreMap.TryGetValue(interfaceType, out var core))
          Core |= core;

        if (m_formattableMap.TryGetValue(interfaceType, out var formattable))
          Formattable |= formattable;

        if (m_functionsMap.TryGetValue(interfaceType, out var functions))
          Functions |= functions;

        if (m_identityMap.TryGetValue(interfaceType, out var identity))
          Identity |= identity;

        if (m_operatorsMap.TryGetValue(interfaceType, out var operators))
          Operators |= operators;

        if (m_parsableMap.TryGetValue(interfaceType, out var parsable))
          Parsable |= parsable;
      }

      if (m_numberBaseType.TryGetValue(m_type.IsGenericType ? m_type.GetGenericTypeDefinition() : m_type, out var baseType)) // Has to evaluate generic-type and get type-definition is so, for sake of "System.Numerics.Complex<>".
        BaseType = baseType;

      // Sanity checks.

      System.Diagnostics.Debug.Assert(IntPtr.Size == UIntPtr.Size);
      System.Diagnostics.Debug.Assert(IntPtr.Size == System.Runtime.InteropServices.NFloat.Size);

      System.Diagnostics.Debug.WriteLine(ToStringEx());
    }

    public static Type Type => m_type;

    #region Stored Flags

    #region BaseType

    public static readonly NumberBaseType BaseType;

    #endregion

    #region NumericConstants

    public static readonly NumericConstants Constants;

    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IFloatingPointConstants{TSelf}"/>.</summary>
    public static bool ImplementsIFloatingPointConstants => (Constants & NumericConstants.IFloatingPointConstants) != 0;

    #endregion

    #region NumericCore

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

    public static readonly NumericFormattable Formattable;

    /// <summary>Indicates whether the type implements <see cref="System.IFormattable"/>.</summary>
    public static bool ImplementsIFormattable => (Formattable & NumericFormattable.IFormattable) != 0;

    /// <summary>Indicates whether the type implements <see cref="System.ISpanFormattable"/>.</summary>
    public static bool ImplementsISpanFormattable => (Formattable & NumericFormattable.ISpanFormattable) != 0;

    /// <summary>Indicates whether the type implements <see cref="System.IUtf8SpanFormattable"/>.</summary>
    public static bool ImplementsIUtf8SpanFormattable => (Formattable & NumericFormattable.IUtf8SpanFormattable) != 0;

    #endregion

    #region NumericFunctions

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

    public static readonly NumericIdentity Identity;

    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IAdditiveIdentity{TSelf, TResult}"/>.</summary>
    public static bool ImplementsIAdditiveIdentity => (Identity & NumericIdentity.IAdditiveIdentity) != 0;

    /// <summary>Indicates whether the type implements <see cref="System.Numerics.IMultiplicativeIdentity{TSelf, TResult}"/>.</summary>
    public static bool ImplementsIMultiplicativeIdentity => (Identity & NumericIdentity.IMultiplicativeIdentity) != 0;

    #endregion

    #region NumericOperators

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

    public static readonly NumericParsable Parsable;

    /// <summary>Indicates whether the type implements <see cref="System.IParsable{TSelf}"/>.</summary>
    public static bool ImplementsIParsable => (Parsable & NumericParsable.IParsable) != 0;

    /// <summary>Indicates whether the type implements <see cref="System.ISpanParsable{TSelf}"/>.</summary>
    public static bool ImplementsISpanParsable => (Parsable & NumericParsable.ISpanParsable) != 0;

    /// <summary>Indicates whether the type implements <see cref="System.IUtf8SpanParsable{TSelf}"/>.</summary>
    public static bool ImplementsIUtf8SpanParsable => (Parsable & NumericParsable.IUtf8SpanParsable) != 0;

    #endregion

    #endregion

    #region Is.. Concrete Type Checks

    /// <summary>Indicates whether the type represents <see cref="System.Numerics.BFloat16"/>.</summary>
    public static bool IsBFloat16 => BaseType == NumberBaseType.BFloat16;

    /// <summary>Indicates whether the type represents <see cref="System.Numerics.BigInteger"/>.</summary>
    public static bool IsBigInteger => BaseType == NumberBaseType.BigInteger;

    /// <summary>Indicates whether the type represents <see cref="Flux.Numerics.BigRational"/>.</summary>
    public static bool IsBigRational => BaseType == NumberBaseType.BigRational;

    /// <summary>Indicates whether the type represents <see cref="byte"/>.</summary>
    public static bool IsByte => BaseType == NumberBaseType.Byte;

    /// <summary>Indicates whether the type represents <see cref="System.Numerics.Complex"/>.</summary>
    public static bool IsComplex => BaseType == NumberBaseType.Complex;

    /// <summary>Indicates whether the type represents <see cref="System.Numerics.Complex{TSelf}"/>.</summary>
    public static bool IsComplexT => BaseType == NumberBaseType.ComplexT;

    /// <summary>Indicates whether the type represents <see cref="decimal"/>.</summary>
    public static bool IsDecimal => BaseType == NumberBaseType.Decimal;

    /// <summary>Indicates whether the type represents <see cref="System.Numerics.Decimal32"/>.</summary>
    public static bool IsDecimal32 => BaseType == NumberBaseType.Decimal32;

    /// <summary>Indicates whether the type represents <see cref="System.Numerics.Decimal64"/>.</summary>
    public static bool IsDecimal64 => BaseType == NumberBaseType.Decimal64;

    /// <summary>Indicates whether the type represents <see cref="System.Numerics.Decimal128"/>.</summary>
    public static bool IsDecimal128 => BaseType == NumberBaseType.Decimal128;

    /// <summary>Indicates whether the type represents <see cref="double"/>.</summary>
    public static bool IsDouble => BaseType == NumberBaseType.Double;

    /// <summary>Indicates whether the type represents <see cref="System.Half"/>.</summary>
    public static bool IsHalf => BaseType == NumberBaseType.Half;

    /// <summary>Indicates whether the type represents <see cref="short"/>.</summary>
    public static bool IsInt16 => BaseType == NumberBaseType.Int16;

    /// <summary>Indicates whether the type represents <see cref="int"/>.</summary>
    public static bool IsInt32 => BaseType == NumberBaseType.Int32;

    /// <summary>Indicates whether the type represents <see cref="long"/>.</summary>
    public static bool IsInt64 => BaseType == NumberBaseType.Int64;

    /// <summary>Indicates whether the type represents <see cref="System.Int128"/>.</summary>
    public static bool IsInt128 => BaseType == NumberBaseType.Int128;

    /// <summary>Indicates whether the type represents <see cref="System.UIntPtr"/>.</summary>
    public static bool IsIntPtr => BaseType == NumberBaseType.IntPtr;

    /// <summary>Indicates whether the type represents <see cref="System.IntPtr"/> with <see cref="System.IntPtr.Size"/> equal to 4.</summary>
    public static bool IsIntPtr32 => IsIntPtr && System.IntPtr.Size == 4;

    /// <summary>Indicates whether the type represents <see cref="System.IntPtr"/> with <see cref="System.IntPtr.Size"/> equal to 8.</summary>
    public static bool IsIntPtr64 => IsIntPtr && System.IntPtr.Size == 8;

    /// <summary>Indicates whether the type represents <see cref="System.Runtime.InteropServices.NFloat"/>.</summary>
    public static bool IsNFloat => BaseType == NumberBaseType.NFloat;

    /// <summary>Indicates whether the type represents <see cref="System.Runtime.InteropServices.NFloat"/> with <see cref="System.Runtime.InteropServices.NFloat.Size"/> equal to 4.</summary>
    public static bool IsNFloat32 => IsNFloat && System.Runtime.InteropServices.NFloat.Size == 4;

    /// <summary>Indicates whether the type represents <see cref="System.Runtime.InteropServices.NFloat"/> with <see cref="System.Runtime.InteropServices.NFloat.Size"/> equal to 8.</summary>
    public static bool IsNFloat64 => IsNFloat && System.Runtime.InteropServices.NFloat.Size == 8;

    /// <summary>Indicates whether the type represents <see cref="System.SByte"/>.</summary>
    public static bool IsSByte => BaseType == NumberBaseType.SByte;

    /// <summary>Indicates whether the type represents <see cref="float"/>.</summary>
    public static bool IsSingle => BaseType == NumberBaseType.Single;

    /// <summary>Indicates whether the type represents <see cref="ushort"/>.</summary>
    public static bool IsUInt16 => BaseType == NumberBaseType.UInt16;

    /// <summary>Indicates whether the type represents <see cref="uint"/>.</summary>
    public static bool IsUInt32 => BaseType == NumberBaseType.UInt32;

    /// <summary>Indicates whether the type represents <see cref="ulong"/>.</summary>
    public static bool IsUInt64 => BaseType == NumberBaseType.UInt64;

    /// <summary>Indicates whether the type represents <see cref="System.UInt128"/>.</summary>
    public static bool IsUInt128 => BaseType == NumberBaseType.UInt128;

    /// <summary>Indicates whether the type represents <see cref="System.UIntPtr"/>.</summary>
    public static bool IsUIntPtr => BaseType == NumberBaseType.UIntPtr;

    /// <summary>Indicates whether the type represents <see cref="System.UIntPtr"/> with <see cref="System.UIntPtr.Size"/> equal to 4.</summary>
    public static bool IsUIntPtr32 => IsUIntPtr && System.UIntPtr.Size == 4;

    /// <summary>Indicates whether the type represents <see cref="System.UIntPtr"/> with <see cref="System.UIntPtr.Size"/> equal to 8.</summary>
    public static bool IsUIntPtr64 => IsUIntPtr && System.UIntPtr.Size == 8;

    #endregion

    #region Is.. for Native Compares

    /// <summary>Indicates whether the type represents an IEEE binary32 floating-point, such as <see cref="System.Single"/> or <see cref="System.Runtime.InteropServices.NFloat"/> when <see cref="System.Runtime.InteropServices.NFloat.Size"/> is 4.</summary>
    public static bool IsBinary32 => IsSingle || IsNFloat32;

    /// <summary>Indicates whether the type represents an IEEE binary64 floating-point, such as <see cref="System.Double"/> or <see cref="System.Runtime.InteropServices.NFloat"/> when <see cref="System.Runtime.InteropServices.NFloat.Size"/> is 8.</summary>
    public static bool IsBinary64 => IsDouble || IsNFloat64;

    //public static bool IsNativeSized => IsIntPtr || IsUIntPtr || IsNFloat;

    //public static bool IsNative32 => IsNativeSized && NativeBitWidth == 32;

    //public static bool IsNative64 => IsNativeSized && NativeBitWidth == 64;

    #endregion

    #region Additional Information

    /// <summary>Indicates whether the type has an unbounded bit-width, such as <see cref="System.Numerics.BigInteger"/>.</summary>
    public static bool IsArbitraryPrecision => IsBigInteger || IsBigRational;

    /// <summary>Indicates whether the type has a fixed bit-width, such as <see cref="System.Decimal"/>, <see cref="System.Decimal128"/>, <see cref="System.Int32"/>, or <see cref="System.Single"/>.</summary>
    public static bool IsFixedWidth => !IsArbitraryPrecision;

    /// <summary>Indicates whether the type represents a signed integer, such as <see cref="System.Int128"/>, <see cref="System.Int16"/>, <see cref="System.Int32"/>, <see cref="System.Int64"/>, <see cref="System.SByte"/>, or <see cref="System.Numerics.BigInteger"/>.</summary>
    public static bool IsSignedInteger => ImplementsIBinaryInteger && ImplementsISignedNumber;

    /// <summary>Indicates whether the type represents an unsigned integer , such as <see cref="System.Byte"/>, <see cref="System.UInt128"/>, <see cref="System.UInt16"/>, <see cref="System.UInt32"/>, or <see cref="System.UInt64"/>.</summary>
    public static bool IsUnsignedInteger => ImplementsIBinaryInteger && ImplementsIUnsignedNumber;

    /// <summary>
    /// <para>The width, in bits, of a native-sized value in the current process.</para>
    /// </summary>
    public static int NativeBitWidth => System.IntPtr.Size * 8;

    /// <summary>
    /// <para>The size, in bytes, of a native-sized value in the current process.</para>
    /// </summary>
    public static int NativeSizeInBytes => System.IntPtr.Size;

    /// <summary>
    /// <para>The width of the type in bits.</para>
    /// </summary>
    /// <remarks>For fixed-width types, this equals <see cref="NominalSizeInBytes"/> × 8. If <see cref="IsArbitraryPrecision"/> is <see langword="true"/>, this value is null, indicating an unbounded/not applicable bit-width.</remarks>
    public static int? NominalBitWidth => NominalSizeInBytes * 8;

    /// <summary>
    /// <para>The number of bytes required to represent a value of the type.</para>
    /// </summary>
    /// <remarks>This does not include implementation-dependent padding, object headers, or additional heap allocations (for example, <see cref="System.Numerics.BigInteger"/>).</remarks>
    public static int? NominalSizeInBytes => IsArbitraryPrecision ? null : System.Runtime.CompilerServices.Unsafe.SizeOf<TNumberBase>();

    #endregion

    public static string ToStringEx()
    {
      var sb = new System.Text.StringBuilder();

      sb.Append(m_type.FullName);

      if (m_type.GetCsharpAlias() is var keyword && !string.IsNullOrEmpty(keyword))
        sb.Append($" ({keyword})");

      if (NumberBase.GetNumericDomain<TNumberBase>() != NumericDomain.Unknown)
        sb.AppendJoined($"{NumberBase.GetNumericDomainSymbol<TNumberBase>()}");

      if (IsArbitraryPrecision) sb.AppendJoined(nameof(IsArbitraryPrecision));
      else if (IsFixedWidth) sb.AppendJoined($"{nameof(IsFixedWidth)} {NominalBitWidth}-bit ({NominalSizeInBytes} byte{(Number.IsConsideredPlural(NominalSizeInBytes.HasValue ? NominalSizeInBytes.Value : 0) ? "s" : string.Empty)})");
      if (IsSignedInteger) sb.AppendJoined(nameof(IsSignedInteger));
      else if (IsUnsignedInteger) sb.AppendJoined(nameof(IsUnsignedInteger));

      // Represented CLR types
      if (IsBFloat16) sb.AppendJoined(nameof(IsBFloat16));
      else if (IsBigInteger) sb.AppendJoined(nameof(IsBigInteger));
      else if (IsByte) sb.AppendJoined(nameof(IsByte));
      else if (IsComplex) sb.AppendJoined(nameof(IsComplex));
      else if (IsDecimal) sb.AppendJoined(nameof(IsDecimal));
      else if (IsDecimal32) sb.AppendJoined(nameof(IsDecimal32));
      else if (IsDecimal64) sb.AppendJoined(nameof(IsDecimal64));
      else if (IsDecimal128) sb.AppendJoined(nameof(IsDecimal128));
      else if (IsDouble) sb.AppendJoined(nameof(IsDouble));
      else if (IsInt16) sb.AppendJoined(nameof(IsInt16));
      else if (IsInt32) sb.AppendJoined(nameof(IsInt32));
      else if (IsInt64) sb.AppendJoined(nameof(IsInt64));
      else if (IsInt128) sb.AppendJoined(nameof(IsInt128));
      else if (IsIntPtr) sb.AppendJoined(nameof(IsIntPtr));
      else if (IsNFloat) sb.AppendJoined(nameof(IsNFloat));
      else if (IsSByte) sb.AppendJoined(nameof(IsSByte));
      else if (IsSingle) sb.AppendJoined(nameof(IsSingle));
      else if (IsUInt16) sb.AppendJoined(nameof(IsUInt16));
      else if (IsUInt32) sb.AppendJoined(nameof(IsUInt32));
      else if (IsUInt64) sb.AppendJoined(nameof(IsUInt64));
      else if (IsUInt128) sb.AppendJoined(nameof(IsUInt128));
      else if (IsUIntPtr) sb.AppendJoined(nameof(IsUIntPtr));

      if (IsIntPtr32) sb.AppendJoined(nameof(IsIntPtr32));
      else if (IsIntPtr64) sb.AppendJoined(nameof(IsIntPtr64));

      if (IsNFloat32) sb.AppendJoined(nameof(IsNFloat32));
      else if (IsNFloat64) sb.AppendJoined(nameof(IsNFloat64));

      if (IsUIntPtr32) sb.AppendJoined(nameof(IsUIntPtr32));
      else if (IsUIntPtr64) sb.AppendJoined(nameof(IsUIntPtr64));

      if (IsBigRational) sb.AppendJoined(nameof(IsBigRational));

      if (IsBinary32) sb.AppendJoined(nameof(IsBinary32));
      else if (IsBinary64) sb.AppendJoined(nameof(IsBinary64));

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
