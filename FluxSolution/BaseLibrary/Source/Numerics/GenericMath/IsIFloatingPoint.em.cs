namespace Flux
{
  public static partial class Reflection
  {
    /// <summary>Returns whether the source is of System.Numerics.INumber<>.</summary>
    public static bool IsIFloatingPoint<TSelf>(this TSelf source)
      where TSelf : System.Numerics.INumberBase<TSelf>
    {
      switch (source)
      {
        case System.Decimal:
        case System.Double:
        case System.Half:
        case System.Single:
          return true;
        default:
          return source.GetType().GetInterfaces().Any(i => i.GetGenericTypeDefinition() == typeof(System.Numerics.IFloatingPoint<>));
      }
    }
  }
}
