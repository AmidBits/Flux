#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class Maths
  {
    private static bool ImplementsGenericInterface<TSelf>(this TSelf source, System.Type interfaceGenericTypeDefinition)
      => typeof(TSelf).GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition().Equals(interfaceGenericTypeDefinition));

    /// <summary>
    /// <para>Determines whether <paramref name="source"/> implements System.Numerics.IBinaryInteger<>.</para>
    /// </summary>
    public static bool IsIBinaryInteger<TSelf>(this TSelf source)
      => source.ImplementsGenericInterface(typeof(System.Numerics.IBinaryInteger<>));

    /// <summary>
    /// <para>Determines whether <paramref name="source"/> implements System.Numerics.IFloatingPoint<>.</para>
    /// </summary>
    public static bool IsIFloatingPoint<TSelf>(this TSelf source)
      => source.ImplementsGenericInterface(typeof(System.Numerics.IFloatingPoint<>));

    /// <summary>
    /// <para>Determines whether <paramref name="source"/> implements System.Numerics.IFloatingPointIeee754<>.</para>
    /// </summary>
    public static bool IsIFloatingPointIeee754<TSelf>(this TSelf source)
      => source.ImplementsGenericInterface(typeof(System.Numerics.IFloatingPointIeee754<>));

    /// <summary>
    /// <para>Determines whether <paramref name="source"/> implements System.Numerics.INumber<>.</para>
    /// </summary>
    public static bool IsINumber<TSelf>(this TSelf source)
      => source.ImplementsGenericInterface(typeof(System.Numerics.INumber<>));

    /// <summary>
    /// <para>Determines whether <paramref name="source"/> implements System.Numerics.INumberBase<>.</para>
    /// </summary>
    public static bool IsINumberBase<TSelf>(this TSelf source)
      => source.ImplementsGenericInterface(typeof(System.Numerics.INumberBase<>));

    /// <summary>
    /// <para>Determines whether <paramref name="source"/> implements System.Numerics.ISignedNumber<>.</para>
    /// </summary>
    public static bool IsISignedNumber<TSelf>(this TSelf source)
      => source.ImplementsGenericInterface(typeof(System.Numerics.ISignedNumber<>));

    /// <summary>
    /// <para>Determines whether <paramref name="source"/> implements System.Numerics.IUnsignedNumber<>.</para>
    /// </summary>
    public static bool IsIUnsignedNumber<TSelf>(this TSelf source)
      => source.ImplementsGenericInterface(typeof(System.Numerics.IUnsignedNumber<>));
  }
}
#endif
