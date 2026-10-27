
namespace Flux
{
  #region NumericCapabilities

  [System.Flags]
  public enum NumericCapabilities : long
  {
    None = 0,

    ComplexT = 0x0000_0000_0000_0001,
    IAdditionOperators = 0x0000_0000_0000_0002,
    IAdditiveIdentity = 0x0000_0000_0000_0004,
    IBinaryFloatingPointIeee754 = 0x0000_0000_0000_0008,
    IBinaryInteger = 0x0000_0000_0000_0010,
    IBinaryNumber = 0x0000_0000_0000_0020,
    IBitwiseOperators = 0x0000_0000_0000_0040,
    IComparisonOperators = 0x0000_0000_0000_0080,
#if NET11_0_OR_GREATER
      IDecimalFloatingPointIeee754 = 0x0000_0000_0000_0100,
#endif
    IDecrementOperators = 0x0000_0000_0000_0200,
    IDivisionOperators = 0x0000_0000_0000_0400,
    IEqualityOperators = 0x0000_0000_0000_0800,
    IExponentialFunctions = 0x0000_0000_0000_1000,
    IFloatingPoint = 0x0000_0000_0000_2000,
    IFloatingPointIeee754 = 0x0000_0000_0000_4000,
    IFloatingPointConstants = 0x0000_0000_0000_8000,
    IHyperbolicFunctions = 0x0000_0000_0001_0000,
    IIncrementOperators = 0x0000_0000_0002_0000,
    ILogarithmicFunctions = 0x0000_0000_0004_0000,
    IMinMaxValue = 0x0000_0000_0008_0000,
    IModulusOperators = 0x0000_0000_0010_0000,
    IMultiplyOperators = 0x0000_0000_0020_0000,
    IMultiplicativeIdentity = 0x0000_0000_0040_0000,
    INumber = 0x0000_0000_0080_0000,
    INumberBase = 0x0000_0000_0100_0000,
    IPowerFunctions = 0x0000_0000_0200_0000,
    IRootFunctions = 0x0000_0000_0400_0000,
    IShiftOperators = 0x0000_0000_0800_0000,
    ISignedNumber = 0x0000_0000_1000_0000,
    ISubtractionOperators = 0x0000_0000_2000_0000,
    ITrigonometricFunctions = 0x0000_0000_4000_0000,
    IUnaryNegationOperators = 0x0000_0000_8000_0000,
    IUnaryPlusOperators = 0x0000_0001_0000_0000,
    IUnsignedNumber = 0x0000_0002_0000_0000,

    ArbitraryPrecision = 0x1000_0000_0000_0000,
  }

  #endregion

  public static partial class TypeExtensions
  {
    private static readonly System.Collections.Concurrent.ConcurrentDictionary<System.Type, System.Type[]> m_cachedInterfaces = new();

    private static readonly System.Collections.Concurrent.ConcurrentDictionary<System.Type, NumericCapabilities> m_cachedNumericCapabilities = new();

    extension(System.Type)
    {
      public static void ClearCachedInterfaces() => m_cachedInterfaces.Clear();

      public static void ClearCachedNumericCapabilities() => m_cachedNumericCapabilities.Clear();
    }

    extension(System.Type type)
    {
      public System.Type[] GetInterfacesWithCache(bool skipCaching = false)
      {
        if (m_cachedInterfaces.TryGetValue(type, out var interfaces))
          return interfaces;

        interfaces = [.. type.GetInterfaces().Select(ti => ti.IsGenericType ? ti.GetGenericTypeDefinition() : ti)];

        if (!skipCaching && interfaces.Length > 0)
          m_cachedInterfaces[type] = interfaces;

        return interfaces;
      }

      public NumericCapabilities GetNumericCapabilitiesWithCache()
      {
        if (m_cachedNumericCapabilities.TryGetValue(type, out var numericCapabilities))
          return numericCapabilities;

        numericCapabilities = NumericCapabilities.None;

        foreach (var itype in type.GetInterfacesWithCache(true))
        {
#if NET11_0_OR_GREATER
          if (itype == typeof(System.Numerics.Complex<>))
            numericCapabilities |= NumericCapabilities.ComplexT;
          else 
#endif
          if (itype == typeof(System.Numerics.IAdditionOperators<,,>))
            numericCapabilities |= NumericCapabilities.IAdditionOperators;
          else if (itype == typeof(System.Numerics.IAdditiveIdentity<,>))
            numericCapabilities |= NumericCapabilities.IAdditiveIdentity;
          else if (itype == typeof(System.Numerics.IBinaryFloatingPointIeee754<>))
            numericCapabilities |= NumericCapabilities.IBinaryFloatingPointIeee754;
          else if (itype == typeof(System.Numerics.IBinaryInteger<>))
            numericCapabilities |= NumericCapabilities.IBinaryInteger;
          else if (itype == typeof(System.Numerics.IBinaryNumber<>))
            numericCapabilities |= NumericCapabilities.IBinaryNumber;
          else if (itype == typeof(System.Numerics.IBitwiseOperators<,,>))
            numericCapabilities |= NumericCapabilities.IBitwiseOperators;
          else if (itype == typeof(System.Numerics.IComparisonOperators<,,>))
            numericCapabilities |= NumericCapabilities.IComparisonOperators;
#if NET11_0_OR_GREATER
          else if (itype == typeof(System.Numerics.IDecimalFloatingPointIeee754<>))
            numericCapabilities |= NumericCapabilities.IDecimalFloatingPointIeee754;
#endif
          else if (itype == typeof(System.Numerics.IDecrementOperators<>))
            numericCapabilities |= NumericCapabilities.IDecrementOperators;
          else if (itype == typeof(System.Numerics.IDivisionOperators<,,>))
            numericCapabilities |= NumericCapabilities.IDivisionOperators;
          else if (itype == typeof(System.Numerics.IExponentialFunctions<>))
            numericCapabilities |= NumericCapabilities.IExponentialFunctions;
          else if (itype == typeof(System.Numerics.IEqualityOperators<,,>))
            numericCapabilities |= NumericCapabilities.IEqualityOperators;
          else if (itype == typeof(System.Numerics.IFloatingPoint<>))
            numericCapabilities |= NumericCapabilities.IFloatingPoint;
          else if (itype == typeof(System.Numerics.IFloatingPointConstants<>))
            numericCapabilities |= NumericCapabilities.IFloatingPointConstants;
          else if (itype == typeof(System.Numerics.IFloatingPointIeee754<>))
            numericCapabilities |= NumericCapabilities.IFloatingPointIeee754;
          else if (itype == typeof(System.Numerics.IHyperbolicFunctions<>))
            numericCapabilities |= NumericCapabilities.IHyperbolicFunctions;
          else if (itype == typeof(System.Numerics.IIncrementOperators<>))
            numericCapabilities |= NumericCapabilities.IIncrementOperators;
          else if (itype == typeof(System.Numerics.ILogarithmicFunctions<>))
            numericCapabilities |= NumericCapabilities.ILogarithmicFunctions;
          else if (itype == typeof(System.Numerics.IMinMaxValue<>))
            numericCapabilities |= NumericCapabilities.IMinMaxValue;
          else if (itype == typeof(System.Numerics.IModulusOperators<,,>))
            numericCapabilities |= NumericCapabilities.IModulusOperators;
          else if (itype == typeof(System.Numerics.IMultiplicativeIdentity<,>))
            numericCapabilities |= NumericCapabilities.IMultiplicativeIdentity;
          else if (itype == typeof(System.Numerics.IMultiplyOperators<,,>))
            numericCapabilities |= NumericCapabilities.IMultiplyOperators;
          else if (itype == typeof(System.Numerics.INumber<>))
            numericCapabilities |= NumericCapabilities.INumber;
          else if (itype == typeof(System.Numerics.INumberBase<>))
            numericCapabilities |= NumericCapabilities.INumberBase;
          else if (itype == typeof(System.Numerics.IPowerFunctions<>))
            numericCapabilities |= NumericCapabilities.IPowerFunctions;
          else if (itype == typeof(System.Numerics.IRootFunctions<>))
            numericCapabilities |= NumericCapabilities.IRootFunctions;
          else if (itype == typeof(System.Numerics.IShiftOperators<,,>))
            numericCapabilities |= NumericCapabilities.IShiftOperators;
          else if (itype == typeof(System.Numerics.ISignedNumber<>))
            numericCapabilities |= NumericCapabilities.ISignedNumber;
          else if (itype == typeof(System.Numerics.ISubtractionOperators<,,>))
            numericCapabilities |= NumericCapabilities.ISubtractionOperators;
          else if (itype == typeof(System.Numerics.ITrigonometricFunctions<>))
            numericCapabilities |= NumericCapabilities.ITrigonometricFunctions;
          else if (itype == typeof(System.Numerics.IUnaryNegationOperators<,>))
            numericCapabilities |= NumericCapabilities.IUnaryNegationOperators;
          else if (itype == typeof(System.Numerics.IUnaryPlusOperators<,>))
            numericCapabilities |= NumericCapabilities.IUnaryPlusOperators;
          else if (itype == typeof(System.Numerics.IUnsignedNumber<>))
            numericCapabilities |= NumericCapabilities.IUnsignedNumber;
        }

        if (type == typeof(System.Numerics.BigInteger)) // Add additional conditions if .NET acquires new arbitrary precision types.
          numericCapabilities |= NumericCapabilities.ArbitraryPrecision;

        if (numericCapabilities != NumericCapabilities.None)
          m_cachedNumericCapabilities[type] = numericCapabilities; // Cache number capabilities if there are any.

        return numericCapabilities;
      }

      public bool ImplementsInterface(System.Type numericInterface)
      {
        var interfaces = type.GetInterfacesWithCache();

        return interfaces.Any(gi => gi == numericInterface);
      }
      public bool ImplementsInterfacesAll(params System.Type[] numericInterfaces)
      {
        var interfaces = type.GetInterfacesWithCache();

        return numericInterfaces.All(ni => interfaces.Any(gi => gi == ni));
      }
      public bool ImplementsInterfacesAny(params System.Type[] numericInterfaces)
      {
        var interfaces = type.GetInterfacesWithCache();

        return numericInterfaces.Any(ni => interfaces.Any(gi => gi == ni));
      }

      public bool IsIBinaryFloatingPointIeee754() => System.Enum.HasAnyFlags(type.GetNumericCapabilitiesWithCache(), NumericCapabilities.IBinaryFloatingPointIeee754);
      public bool IsIBinaryInteger() => System.Enum.HasAnyFlags(type.GetNumericCapabilitiesWithCache(), NumericCapabilities.IBinaryInteger);
      public bool IsIBinaryNumber() => System.Enum.HasAnyFlags(type.GetNumericCapabilitiesWithCache(), NumericCapabilities.IBinaryNumber);
#if NET11_0_OR_GREATER
      public bool IsIDecimalFloatingPointIeee754() => System.Enum.HasAnyFlags(type.GetNumericCapabilitiesWithCache(), NumericCapabilities.IDecimalFloatingPointIeee754);
#endif
      public bool IsIFloatingPoint() => System.Enum.HasAnyFlags(type.GetNumericCapabilitiesWithCache(), NumericCapabilities.IFloatingPoint);
      public bool IsIFloatingPointIeee754() => System.Enum.HasAnyFlags(type.GetNumericCapabilitiesWithCache(), NumericCapabilities.IFloatingPointIeee754);
      public bool IsIMinMaxValue() => System.Enum.HasAnyFlags(type.GetNumericCapabilitiesWithCache(), NumericCapabilities.IMinMaxValue);
      public bool IsINumber() => System.Enum.HasAnyFlags(type.GetNumericCapabilitiesWithCache(), NumericCapabilities.INumber);
      public bool IsINumberBase() => System.Enum.HasAnyFlags(type.GetNumericCapabilitiesWithCache(), NumericCapabilities.INumberBase);
      public bool IsISignedNumber() => System.Enum.HasAnyFlags(type.GetNumericCapabilitiesWithCache(), NumericCapabilities.ISignedNumber);
      public bool IsIUnsignedNumber() => System.Enum.HasAnyFlags(type.GetNumericCapabilitiesWithCache(), NumericCapabilities.IUnsignedNumber);

      public bool IsNumericDouble() => type == typeof(System.Double) || (type == typeof(System.Runtime.InteropServices.NFloat) && System.Runtime.InteropServices.NFloat.Size == 8);
      public bool IsNumericHalf() => type == typeof(System.Half);
      public bool IsNumericSingle() => type == typeof(System.Single) || (type == typeof(System.Runtime.InteropServices.NFloat) && System.Runtime.InteropServices.NFloat.Size == 4);
      public bool IsNumericsBigInteger() => type == typeof(System.Numerics.BigInteger);
      public bool IsNumericsComplex() => type == typeof(System.Numerics.Complex);

      public bool IsSignedInteger() => System.Enum.HasAllFlags(type.GetNumericCapabilitiesWithCache(), NumericCapabilities.IBinaryInteger | NumericCapabilities.ISignedNumber);
      public bool IsSignedIntegerFixedWidth() => System.Enum.HasAllFlags(type.GetNumericCapabilitiesWithCache(), NumericCapabilities.IBinaryInteger | NumericCapabilities.IMinMaxValue | NumericCapabilities.ISignedNumber);
      public bool IsUnsignedInteger() => System.Enum.HasAllFlags(type.GetNumericCapabilitiesWithCache(), NumericCapabilities.IBinaryInteger | NumericCapabilities.IUnsignedNumber);
    }
  }
}
