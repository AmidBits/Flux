namespace Flux
{
  public static partial class Reflection
  {
    /// <summary>Returns whether the source is of System.Numerics.INumberBase<>.</summary>
    public static bool IsIUnsignedNumber<TSelf>(this TSelf source)
      where TSelf : System.Numerics.INumberBase<TSelf>
    {
      switch (source)
      {
        case System.Byte:
        case System.Char:
        case System.UInt128:
        case System.UInt16:
        case System.UInt32:
        case System.UInt64:
        case System.UIntPtr:
          return true;
        default:
          return source.GetType().GetInterfaces().Any(i => i.GetGenericTypeDefinition() == typeof(System.Numerics.IUnsignedNumber<>));
      }
    }
  }
}
