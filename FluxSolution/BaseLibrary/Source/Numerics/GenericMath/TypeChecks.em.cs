namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Returns whether the source is of System.Numerics.IBinaryInteger<>.</summary>
    public static bool IsIBinaryInteger<TSelf>(this TSelf source)
      where TSelf : System.Numerics.INumberBase<TSelf>
      => source.GetType().GetInterfaces().Where(i => i.IsGenericType).Any(i => i.GetGenericTypeDefinition() == typeof(System.Numerics.IBinaryInteger<>));

    /// <summary>Returns whether the source is of System.Numerics.IFloatingPoint<>.</summary>
    public static bool IsIFloatingPoint<TSelf>(this TSelf source)
      where TSelf : System.Numerics.INumberBase<TSelf>
      => source.GetType().GetInterfaces().Where(i => i.IsGenericType).Any(i => i.GetGenericTypeDefinition() == typeof(System.Numerics.IFloatingPoint<>));

    /// <summary>Returns whether the source is of System.Numerics.INumber<>.</summary>
    public static bool IsINumber<TSelf>(this TSelf source)
      where TSelf : System.Numerics.INumberBase<TSelf>
      => source.GetType().GetInterfaces().Where(i => i.IsGenericType).Any(i => i.GetGenericTypeDefinition() == typeof(System.Numerics.INumber<>));

    /// <summary>Returns whether the source is of System.Numerics.ISignedNumber<>.</summary>
    public static bool IsISignedNumber<TSelf>(this TSelf source)
      where TSelf : System.Numerics.INumberBase<TSelf>
      => source.GetType().GetInterfaces().Where(i => i.IsGenericType).Any(i => i.GetGenericTypeDefinition() == typeof(System.Numerics.ISignedNumber<>));

    /// <summary>Returns whether the source is of System.Numerics.IUnsignedNumber<>.</summary>
    public static bool IsIUnsignedNumber<TSelf>(this TSelf source)
      where TSelf : System.Numerics.INumberBase<TSelf>
      => source.GetType().GetInterfaces().Where(i => i.IsGenericType).Any(i => i.GetGenericTypeDefinition() == typeof(System.Numerics.IUnsignedNumber<>));
  }
}
