namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Determines whether the <paramref name="source"/> number is unsigned.</para>
    /// <para><seealso href="https://stackoverflow.com/a/13609383/3178666"/></para>
    /// </summary>
    public static bool IsUnsignedNumber<TSelf>(this TSelf source)
      where TSelf : System.Numerics.INumber<TSelf>
      => source is System.Byte
      || source is System.Char
      || source is System.UInt16
      || source is System.UInt32
      || source is System.UInt64
      || source is System.UInt128
      || source is System.UIntPtr
      || TSelf.Zero.CompareTo(typeof(TSelf).GetField("MinValue")?.GetValue(null) ?? TSelf.One) == 0 // By field "MinValue" being zero.
      || typeof(TSelf).IsAssignableToGenericType(typeof(System.Numerics.IUnsignedNumber<>)); // By being assignable to System.Numerics.IUnsignedNumber<>.
  }
}
