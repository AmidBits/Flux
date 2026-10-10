
namespace Flux
{
  public static partial class TypeExtensions
  {
    private static readonly System.Collections.Concurrent.ConcurrentDictionary<System.Type, System.Type[]> m_cachedInterfaces = new();

    extension(System.Type)
    {
      public static void ClearCache()
        => m_cachedInterfaces.Clear();
    }

    extension(System.Type type)
    {
      public System.Type[] GetCachedInterfaces()
      {
        if (m_cachedInterfaces.TryGetValue(type, out var interfaces))
          return interfaces;

        interfaces = [.. type.GetInterfaces().Select(ti => ti.IsGenericType ? ti.GetGenericTypeDefinition() : ti)];

        if (interfaces.Length > 0)
          m_cachedInterfaces[type] = interfaces;

        return interfaces;
      }

      public bool ImplementsInterface(System.Type numericInterface)
      {
        var interfaces = type.GetCachedInterfaces();

        return interfaces.Any(gi => gi == numericInterface);
      }
      public bool ImplementsInterfacesAll(params System.Type[] numericInterfaces)
      {
        var interfaces = type.GetCachedInterfaces();

        return numericInterfaces.All(ni => interfaces.Any(gi => gi == ni));
      }
      public bool ImplementsInterfacesAny(params System.Type[] numericInterfaces)
      {
        var interfaces = type.GetCachedInterfaces();

        return numericInterfaces.Any(ni => interfaces.Any(gi => gi == ni));
      }

      public bool ImplementsIBinaryFloatingPointIeee754() => type.ImplementsInterface(typeof(System.Numerics.IBinaryFloatingPointIeee754<>));
      public bool ImplementsIBinaryInteger() => type.ImplementsInterface(typeof(System.Numerics.IBinaryInteger<>));
      public bool ImplementsIBinaryIntegerSigned() => type.ImplementsInterfacesAll(typeof(System.Numerics.IBinaryInteger<>), typeof(System.Numerics.ISignedNumber<>));
      public bool ImplementsIBinaryIntegerUnsigned() => type.ImplementsInterfacesAll(typeof(System.Numerics.IBinaryInteger<>), typeof(System.Numerics.IUnsignedNumber<>));
      public bool ImplementsIBinaryNumber() => type.ImplementsInterface(typeof(System.Numerics.IBinaryNumber<>));
#if NET11_0_OR_GREATER
      public bool ImplementsIDecimalFloatingPointIeee754() => type.ImplementsInterface(typeof(System.Numerics.IDecimalFloatingPointIeee754<>));
#endif
      public bool ImplementsIFloatingPoint() => type.ImplementsInterface(typeof(System.Numerics.IFloatingPoint<>));
      public bool ImplementsIFloatingPointIeee754() => type.ImplementsInterface(typeof(System.Numerics.IFloatingPointIeee754<>));
      public bool ImplementsIMinMaxValue() => type.ImplementsInterface(typeof(System.Numerics.IMinMaxValue<>));
      public bool ImplementsINumber() => type.ImplementsInterface(typeof(System.Numerics.INumber<>));
      public bool ImplementsINumberBase() => type.ImplementsInterface(typeof(System.Numerics.INumberBase<>));
      public bool ImplementsISignedNumber() => type.ImplementsInterface(typeof(System.Numerics.ISignedNumber<>));
      public bool ImplementsIUnsignedNumber() => type.ImplementsInterface(typeof(System.Numerics.IUnsignedNumber<>));
    }
  }
}
