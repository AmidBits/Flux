namespace Flux
{
  public static class NumericTypeInfo
  {
    public static int Count = 0;

    /// <summary>
    /// <para>The width, in bits, of a native-sized value in the current process.</para>
    /// </summary>
    public static int NativeBitCount => System.IntPtr.Size * 8;

    /// <summary>
    /// <para>The size, in bytes, of a native-sized value in the current process.</para>
    /// </summary>
    public static int NativeByteCount => System.IntPtr.Size;

    public static void Preload()
    {
      var t1 = Flux.NumericTypeInfo<byte>.ToStringEx();
      var t2 = Flux.NumericTypeInfo<char>.ToStringEx();
      var t3 = Flux.NumericTypeInfo<decimal>.ToStringEx();
      var t4 = Flux.NumericTypeInfo<double>.ToStringEx();
      var t5 = Flux.NumericTypeInfo<System.Half>.ToStringEx();
      var t6 = Flux.NumericTypeInfo<System.Int128>.ToStringEx();
      var t7 = Flux.NumericTypeInfo<short>.ToStringEx();
      var t8 = Flux.NumericTypeInfo<int>.ToStringEx();
      var t9 = Flux.NumericTypeInfo<long>.ToStringEx();
      var t10 = Flux.NumericTypeInfo<System.IntPtr>.ToStringEx();
      var t11 = Flux.NumericTypeInfo<System.Numerics.BFloat16>.ToStringEx();
      var t12 = Flux.NumericTypeInfo<System.Numerics.Decimal128>.ToStringEx();
      var t13 = Flux.NumericTypeInfo<System.Numerics.Decimal32>.ToStringEx();
      var t14 = Flux.NumericTypeInfo<System.Numerics.Decimal64>.ToStringEx();
      var t15 = Flux.NumericTypeInfo<System.Runtime.InteropServices.NFloat>.ToStringEx();
      var t16 = Flux.NumericTypeInfo<System.SByte>.ToStringEx();
      var t17 = Flux.NumericTypeInfo<float>.ToStringEx();
      var t18 = Flux.NumericTypeInfo<System.UInt128>.ToStringEx();
      var t19 = Flux.NumericTypeInfo<ushort>.ToStringEx();
      var t20 = Flux.NumericTypeInfo<uint>.ToStringEx();
      var t21 = Flux.NumericTypeInfo<ulong>.ToStringEx();
      var t22 = Flux.NumericTypeInfo<System.UIntPtr>.ToStringEx();

      var t23 = Flux.NumericTypeInfo<System.Numerics.BigInteger>.ToStringEx();
      var t24 = Flux.NumericTypeInfo<System.Numerics.Complex>.ToStringEx();
      var t25 = Flux.NumericTypeInfo<System.Numerics.Complex<System.Numerics.Decimal128>>.ToStringEx();

      var t26 = Flux.NumericTypeInfo<Flux.Numerics.BigDecimal>.ToStringEx();
      var t27 = Flux.NumericTypeInfo<Flux.Numerics.BigRational>.ToStringEx();
    }
  }

  public static class NumericTypeInfo<TNumberBase>
    where TNumberBase : System.Numerics.INumberBase<TNumberBase>
  {
    /// <summary>
    /// <para>The generic type definition of TNumberBase, if there is one, otherwise <see langword="typeof"/>(TNumberBase).</para>
    /// </summary>
    private static readonly System.Type m_genericTypeDefinition = typeof(TNumberBase) is var t && t.IsGenericType ? t.GetGenericTypeDefinition() : t;

    /// <summary>
    /// <para>The storage size of TNumberBase.</para>
    /// </summary>
    private static readonly int m_sizeOf = System.Runtime.CompilerServices.Unsafe.SizeOf<TNumberBase>();

    /// <summary>
    /// <para>The TNumberBase type.</para>
    /// </summary>
    private static readonly System.Type m_type = typeof(TNumberBase);

    /// <summary>
    /// <para>Depends on pre-initialization of <see cref="m_type"/>. Has to be before here.</para>
    /// </summary>
    private static readonly bool m_isArbitraryPrecision = IsSystemNumericsBigInteger || IsFluxNumericsBigDecimal || IsFluxNumericsBigRational; // Dependent on m_type being initialized!!!

    static NumericTypeInfo()
    {
      NumericTypeInfo.Count++;

      foreach (var type in typeof(TNumberBase).GetCachedInterfaces()) // GetCachedInterfaces expands generic type descriptions.
      {
        var name = type.GetNameWithoutExtras();

        if (System.Enum.TryParse<NumericConstants>(name, out var constant))
          Constants |= constant;

        if (System.Enum.TryParse<NumericCore>(name, false, out var core))
          Core |= core;

        if (System.Enum.TryParse<NumericFormattable>(name, false, out var formattable))
          Formattable |= formattable;

        if (System.Enum.TryParse<NumericFunctions>(name, false, out var functions))
          Functions |= functions;

        if (System.Enum.TryParse<NumericIdentity>(name, false, out var identity))
          Identity |= identity;

        if (System.Enum.TryParse<NumericOperators>(name, false, out var operators))
          Operators |= operators;

        if (System.Enum.TryParse<NumericParsable>(name, false, out var parsable))
          Parsable |= parsable;
      }

      // Ensure all native sizes are the same.
      System.Diagnostics.Debug.Assert(System.IntPtr.Size == System.UIntPtr.Size);
      System.Diagnostics.Debug.Assert(System.UIntPtr.Size == System.Runtime.InteropServices.NFloat.Size);
      System.Diagnostics.Debug.Assert(System.Runtime.InteropServices.NFloat.Size == System.IntPtr.Size);

      System.Diagnostics.Debug.WriteLine(ToStringEx());
    }

    public static System.Type Type => m_type;

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

    #region Is[Namespace][Type]

    /// <summary>Indicates whether the type represents <see cref="Flux.Numerics.BigDecimal"/>.</summary>
    public static bool IsFluxNumericsBigDecimal => m_type == typeof(Flux.Numerics.BigDecimal);

    /// <summary>Indicates whether the type represents <see cref="Flux.Numerics.BigRational"/>.</summary>
    public static bool IsFluxNumericsBigRational => m_type == typeof(Flux.Numerics.BigRational);

    /// <summary>Indicates whether the type represents <see cref="System.Numerics.BFloat16"/>.</summary>
    public static bool IsSystemNumericsBFloat16 => m_type == typeof(System.Numerics.BFloat16);

    /// <summary>Indicates whether the type represents <see cref="System.Numerics.BigInteger"/>.</summary>
    public static bool IsSystemNumericsBigInteger => m_type == typeof(System.Numerics.BigInteger);

    /// <summary>Indicates whether the type represents <see cref="byte"/>.</summary>
    public static bool IsSystemByte => m_type == typeof(System.Byte);

    /// <summary>Indicates whether the type represents <see cref="System.Numerics.Complex"/>.</summary>
    public static bool IsSystemNumericsComplex => m_type == typeof(System.Numerics.Complex);

    /// <summary>Indicates whether the type represents <see cref="System.Numerics.Complex{TSelf}"/>.</summary>
    public static bool IsSystemNumericsComplexT => m_genericTypeDefinition == typeof(System.Numerics.Complex<>);

    /// <summary>Indicates whether the type represents <see cref="decimal"/>.</summary>
    public static bool IsSystemDecimal => m_type == typeof(System.Decimal);

    /// <summary>Indicates whether the type represents <see cref="System.Numerics.Decimal32"/>.</summary>
    public static bool IsSystemNumericsDecimal32 => m_type == typeof(System.Numerics.Decimal32);

    /// <summary>Indicates whether the type represents <see cref="System.Numerics.Decimal64"/>.</summary>
    public static bool IsSystemNumericsDecimal64 => m_type == typeof(System.Numerics.Decimal64);

    /// <summary>Indicates whether the type represents <see cref="System.Numerics.Decimal128"/>.</summary>
    public static bool IsSystemNumericsDecimal128 => m_type == typeof(System.Numerics.Decimal128);

    /// <summary>Indicates whether the type represents <see cref="double"/>.</summary>
    public static bool IsSystemDouble => m_type == typeof(System.Double);

    /// <summary>Indicates whether the type represents <see cref="System.Half"/>.</summary>
    public static bool IsSystemHalf => m_type == typeof(System.Half);

    /// <summary>Indicates whether the type represents <see cref="short"/>.</summary>
    public static bool IsSystemInt16 => m_type == typeof(System.Int16);

    /// <summary>Indicates whether the type represents <see cref="int"/>.</summary>
    public static bool IsSystemInt32 => m_type == typeof(System.Int32);

    /// <summary>Indicates whether the type represents <see cref="long"/>.</summary>
    public static bool IsSystemInt64 => m_type == typeof(System.Int64);

    /// <summary>Indicates whether the type represents <see cref="System.Int128"/>.</summary>
    public static bool IsSystemInt128 => m_type == typeof(System.Int128);

    /// <summary>Indicates whether the type represents <see cref="System.IntPtr"/>.</summary>
    public static bool IsSystemIntPtr => m_type == typeof(System.IntPtr);

    /// <summary>Indicates whether the type represents <see cref="System.Runtime.InteropServices.NFloat"/>.</summary>
    public static bool IsSystemRuntimeInteropServicesNFloat => m_type == typeof(System.Runtime.InteropServices.NFloat);

    /// <summary>Indicates whether the type represents <see cref="System.SByte"/>.</summary>
    public static bool IsSystemSByte => m_type == typeof(System.SByte);

    /// <summary>Indicates whether the type represents <see cref="float"/>.</summary>
    public static bool IsSystemSingle => m_type == typeof(System.Single);

    /// <summary>Indicates whether the type represents <see cref="ushort"/>.</summary>
    public static bool IsSystemUInt16 => m_type == typeof(System.UInt16);

    /// <summary>Indicates whether the type represents <see cref="uint"/>.</summary>
    public static bool IsSystemUInt32 => m_type == typeof(System.UInt32);

    /// <summary>Indicates whether the type represents <see cref="ulong"/>.</summary>
    public static bool IsSystemUInt64 => m_type == typeof(System.UInt64);

    /// <summary>Indicates whether the type represents <see cref="System.UInt128"/>.</summary>
    public static bool IsSystemUInt128 => m_type == typeof(System.UInt128);

    /// <summary>Indicates whether the type represents <see cref="System.UIntPtr"/>.</summary>
    public static bool IsSystemUIntPtr => m_type == typeof(System.UIntPtr);

    #endregion

    #region Additional Information

    /// <summary>
    /// <para>Indicates whether the type has an unbounded bit-width, such as <see cref="System.Numerics.BigInteger"/>.</para>
    /// </summary>
    public static bool IsArbitraryPrecision => m_isArbitraryPrecision;

    ///// <summary>
    ///// <para>Indicates wheter the type represents the numeric domain of complex numbers.</para>
    ///// </summary>
    //public static bool IsNumericDomainComplex => IsSystemNumericsComplex || IsSystemNumericsComplexT;

    ///// <summary>
    ///// <para>Indicates wheter the type represents the numeric domain of rational numbers.</para>
    ///// </summary>
    //public static bool IsNumericDomainRational => IsFluxNumericsBigDecimal || IsFluxNumericsBigRational;

    ///// <summary>
    ///// <para>Indicates wheter the type represents the numeric domain of real numbers.</para>
    ///// </summary>
    //public static bool IsNumericDomainReal => IsSystemHalf || IsIeeeBinary32 || IsIeeeBinary64;

    /// <summary>
    /// <para>Indicates whether the type has a fixed bit-width, such as <see cref="System.Decimal"/>, <see cref="System.Decimal128"/>, <see cref="System.Int32"/>, or <see cref="System.Single"/>.</para>
    /// </summary>
    public static bool IsFixedWidth => !m_isArbitraryPrecision;

    /// <summary>Indicates whether the type represents the family of IEEE binary32 floating-point types, such as <see cref="System.Single"/> or <see cref="System.Runtime.InteropServices.NFloat"/> when <see cref="System.Runtime.InteropServices.NFloat.Size"/> is 4.</summary>
    /// <remarks>Needed to identify IEEE binary32 floating point types in .NET, where NFloat is dependent on system hardware.</remarks>
    public static bool IsIeeeBinary32 => IsSystemSingle || (IsSystemRuntimeInteropServicesNFloat && NumericTypeInfo.NativeByteCount == 4);

    /// <summary>Indicates whether the type represents the family of IEEE binary64 floating-point types, such as <see cref="System.Double"/> or <see cref="System.Runtime.InteropServices.NFloat"/> when <see cref="System.Runtime.InteropServices.NFloat.Size"/> is 8.</summary>
    /// <remarks>Needed to identify IEEE binary64 floating point types in .NET, where NFloat is dependent on system hardware.</remarks>
    public static bool IsIeeeBinary64 => IsSystemDouble || (IsSystemRuntimeInteropServicesNFloat && NumericTypeInfo.NativeByteCount == 8);

    //public static bool IsRadix10 => TNumberBase.Radix == 10;

    //public static bool IsRadix2 => TNumberBase.Radix == 2;

    /// <summary>
    /// <para>Indicates whether the type represents a signed integer, such as <see cref="System.Int128"/>, <see cref="System.Int16"/>, <see cref="System.Int32"/>, <see cref="System.Int64"/>, <see cref="System.SByte"/>, or <see cref="System.Numerics.BigInteger"/>.</para>
    /// </summary>
    public static bool IsSignedInteger => ImplementsIBinaryInteger && ImplementsISignedNumber;

    /// <summary>
    /// <para>Indicates whether the type represents an unsigned integer , such as <see cref="System.Byte"/>, <see cref="System.UInt128"/>, <see cref="System.UInt16"/>, <see cref="System.UInt32"/>, or <see cref="System.UInt64"/>.</para>
    /// </summary>
    public static bool IsUnsignedInteger => ImplementsIBinaryInteger && ImplementsIUnsignedNumber;

    //public static bool Is8Bit => !m_isArbitraryPrecision && m_sizeOf == 1;
    //public static bool Is16Bit => !m_isArbitraryPrecision && m_sizeOf == 2;
    //public static bool Is32Bit => !m_isArbitraryPrecision && m_sizeOf == 4;
    //public static bool Is64Bit => !m_isArbitraryPrecision && m_sizeOf == 8;
    //public static bool Is128Bit => !m_isArbitraryPrecision && m_sizeOf == 16;

    /// <summary>
    /// <para>The width of the type in bits.</para>
    /// </summary>
    /// <remarks>For fixed-width types, this equals <see cref="ByteCount"/> × 8. If <see cref="IsArbitraryPrecision"/> is <see langword="true"/>, this value is null, indicating an unbounded/not applicable bit-width.</remarks>
    public static int? BitCount => m_isArbitraryPrecision ? null : m_sizeOf * 8;

    /// <summary>
    /// <para>The number of bytes required to represent a value of the type.</para>
    /// </summary>
    /// <remarks>This does not include implementation-dependent padding, object headers, or additional heap allocations (for example, <see cref="System.Numerics.BigInteger"/>).</remarks>
    public static int? ByteCount => m_isArbitraryPrecision ? null : m_sizeOf;

    public static NumericDomain NumericDomain
      => IsUnsignedInteger ? NumericDomain.NaturalNumbers
      : IsSignedInteger ? NumericDomain.Integers
      : IsFluxNumericsBigDecimal || IsFluxNumericsBigRational ? NumericDomain.RationalNumbers
      : ImplementsIFloatingPoint ? NumericDomain.RealNumbers
      : IsSystemNumericsComplex || IsSystemNumericsComplexT ? NumericDomain.ComplexNumbers
      : NumericDomain.Unknown;

    public static int Radix => TNumberBase.Radix;

    #endregion

    public static string ToStringEx()
    {
      var sb = new System.Text.StringBuilder();

      sb.Append($"{NumericTypeInfo.Count} : ");

      sb.Append(m_type.FullName);

      if (m_type.GetCsharpAlias() is string keyword)
        sb.AppendSpaced($"({keyword})");

      if (NumericDomain != NumericDomain.Unknown)
        sb.AppendJoined($"{NumberBase.GetNumericDomainSymbol<TNumberBase>()}");

      sb.Append(BinaryInteger.ToSubscriptString(Radix, 10));

      //sb.AppendJoined($"{nameof(Radix)} {Radix}");

      if (IsArbitraryPrecision) sb.AppendJoined(nameof(IsArbitraryPrecision));
      if (IsFixedWidth) sb.AppendJoined($"{ByteCount}-byte ({BitCount}-bit)");

      if (IsFluxNumericsBigDecimal) sb.AppendJoined(nameof(IsFluxNumericsBigDecimal));
      else if (IsFluxNumericsBigRational) sb.AppendJoined(nameof(IsFluxNumericsBigRational));
      else if (IsSystemNumericsBFloat16) sb.AppendJoined(nameof(IsSystemNumericsBFloat16));
      else if (IsSystemNumericsBigInteger) sb.AppendJoined(nameof(IsSystemNumericsBigInteger));
      else if (IsSystemByte) sb.AppendJoined(nameof(IsSystemByte));
      else if (IsSystemNumericsComplex) sb.AppendJoined(nameof(IsSystemNumericsComplex));
      else if (IsSystemNumericsComplexT) sb.AppendJoined(nameof(IsSystemNumericsComplexT));
      else if (IsSystemDecimal) sb.AppendJoined(nameof(IsSystemDecimal));
      else if (IsSystemNumericsDecimal32) sb.AppendJoined(nameof(IsSystemNumericsDecimal32));
      else if (IsSystemNumericsDecimal64) sb.AppendJoined(nameof(IsSystemNumericsDecimal64));
      else if (IsSystemNumericsDecimal128) sb.AppendJoined(nameof(IsSystemNumericsDecimal128));
      else if (IsSystemDouble) sb.AppendJoined(nameof(IsSystemDouble));
      else if (IsSystemInt16) sb.AppendJoined(nameof(IsSystemInt16));
      else if (IsSystemInt32) sb.AppendJoined(nameof(IsSystemInt32));
      else if (IsSystemInt64) sb.AppendJoined(nameof(IsSystemInt64));
      else if (IsSystemInt128) sb.AppendJoined(nameof(IsSystemInt128));
      else if (IsSystemIntPtr) sb.AppendJoined(nameof(IsSystemIntPtr));
      else if (IsSystemRuntimeInteropServicesNFloat) sb.AppendJoined(nameof(IsSystemRuntimeInteropServicesNFloat));
      else if (IsSystemSByte) sb.AppendJoined(nameof(IsSystemSByte));
      else if (IsSystemSingle) sb.AppendJoined(nameof(IsSystemSingle));
      else if (IsSystemUInt16) sb.AppendJoined(nameof(IsSystemUInt16));
      else if (IsSystemUInt32) sb.AppendJoined(nameof(IsSystemUInt32));
      else if (IsSystemUInt64) sb.AppendJoined(nameof(IsSystemUInt64));
      else if (IsSystemUInt128) sb.AppendJoined(nameof(IsSystemUInt128));
      else if (IsSystemUIntPtr) sb.AppendJoined(nameof(IsSystemUIntPtr));

      if (IsSignedInteger) sb.AppendJoined(nameof(IsSignedInteger));
      if (IsUnsignedInteger) sb.AppendJoined(nameof(IsUnsignedInteger));

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

    #region Lookup Tables

    [System.Flags]
    public enum NumericConstants
    {
      NoConstants = 0,

      IFloatingPointConstants = 0x0000_0000_0000_0001,
    }

    [System.Flags]
    public enum NumericCore
    {
      NoCore = 0,

      IBinaryFloatingPointIeee754 = 0x0000_0000_0000_0001,
      IBinaryInteger = 0x0000_0000_0000_0002,
      IBinaryNumber = 0x0000_0000_0000_0004,
      IDecimalFloatingPointIeee754 = 0x0000_0000_0000_0008,
      IFloatingPoint = 0x0000_0000_0000_0010,
      IFloatingPointIeee754 = 0x0000_0000_0000_0020,
      IMinMaxValue = 0x0000_0000_0000_0040,
      INumber = 0x0000_0000_0000_0080,
      INumberBase = 0x0000_0000_0000_0100,
      ISignedNumber = 0x0000_0000_0000_0200,
      IUnsignedNumber = 0x0000_0000_0000_0400,
    }

    [System.Flags]
    public enum NumericFormattable
    {
      NotFormattable = 0,

      IFormattable = 0x0000_0000_0000_0001,
      ISpanFormattable = 0x0000_0000_0000_0002,
      IUtf8SpanFormattable = 0x0000_0000_0000_0004,
    }

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

    [System.Flags]
    public enum NumericIdentity
    {
      NoIdentities = 0,

      IAdditiveIdentity = 0x0000_0000_0000_0001,
      IMultiplicativeIdentity = 0x0000_0000_0000_0002,
    }

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

    [System.Flags]
    public enum NumericParsable
    {
      NotParsable = 0,

      IParsable = 0x0000_0000_0000_0001,
      ISpanParsable = 0x0000_0000_0000_0002,
      IUtf8SpanParsable = 0x0000_0000_0000_0004,
    }

    #endregion
  }
}
