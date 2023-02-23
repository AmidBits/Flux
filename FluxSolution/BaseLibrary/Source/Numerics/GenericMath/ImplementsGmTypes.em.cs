namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Returns whether the source is of System.Numerics.IBinaryInteger<>.</summary>
    public static bool ImplementsBinaryInteger<TSelf>(this TSelf source)
      where TSelf : System.Numerics.INumberBase<TSelf>
      => source.GetType().GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(System.Numerics.IBinaryInteger<>));

    /// <summary>Returns whether the source is of System.Numerics.IFloatingPoint<>.</summary>
    public static bool ImplementsFloatingPoint<TSelf>(this TSelf source)
      where TSelf : System.Numerics.INumberBase<TSelf>
      => source.GetType().GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(System.Numerics.IFloatingPoint<>));

    /// <summary>Returns whether the source is of System.Numerics.IFloatingPointIeee754<>.</summary>
    public static bool ImplementsFloatingPointIeee754<TSelf>(this TSelf source)
      where TSelf : System.Numerics.INumberBase<TSelf>
      => source.GetType().GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(System.Numerics.IFloatingPointIeee754<>));

    /// <summary>Returns whether the source is of System.Numerics.INumber<>.</summary>
    public static bool ImplementsNumber<TSelf>(this TSelf source)
      where TSelf : System.Numerics.INumberBase<TSelf>
      => source.GetType().GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(System.Numerics.INumber<>));

    /// <summary>Returns whether the source is of System.Numerics.ISignedNumber<>.</summary>
    public static bool ImplementsSignedNumber<TSelf>(this TSelf source)
      where TSelf : System.Numerics.INumberBase<TSelf>
      => source.GetType().GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(System.Numerics.ISignedNumber<>));

    /// <summary>Returns whether the source is of System.Numerics.IUnsignedNumber<>.</summary>
    public static bool ImplementsUnsignedNumber<TSelf>(this TSelf source)
      where TSelf : System.Numerics.INumberBase<TSelf>
      => source.GetType().GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(System.Numerics.IUnsignedNumber<>));
  }
}
