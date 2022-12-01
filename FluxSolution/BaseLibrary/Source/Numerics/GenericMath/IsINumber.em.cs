namespace Flux
{
  public static partial class Reflection
  {
    /// <summary>Returns whether the source is of System.Numerics.INumber<>.</summary>
    public static bool IsINumber<TSelf>(this TSelf source)
      where TSelf : System.Numerics.INumberBase<TSelf>
    {
      switch (source)
      {
        case System.Byte:
        case System.Char:
        case System.Decimal:
        case System.Double:
        case System.Half:
        case System.Int128:
        case System.Int16:
        case System.Int32:
        case System.Int64:
        case System.IntPtr:
        case System.Numerics.BigInteger:
        case System.SByte:
        case System.Single:
        case System.UInt128:
        case System.UInt16:
        case System.UInt32:
        case System.UInt64:
        case System.UIntPtr:
          return true;
        default:
          return source.GetType().GetInterfaces().Any(i => i.GetGenericTypeDefinition() == typeof(System.Numerics.INumber<>));
      }
    }
  }
}
