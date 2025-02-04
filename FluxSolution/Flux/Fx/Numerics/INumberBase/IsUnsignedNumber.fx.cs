namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Determines whether the <paramref name="numberBase"/> number is unsigned.</para>
    /// <para><seealso href="https://stackoverflow.com/a/13609383/3178666"/></para>
    /// </summary>
    public static bool IsUnsignedNumber<TNumberBase>(this TNumberBase numberBase)
      where TNumberBase : System.Numerics.INumber<TNumberBase>
      => numberBase is System.Byte
      || numberBase is System.Char
      || numberBase is System.UInt16
      || numberBase is System.UInt32
      || numberBase is System.UInt64
      || numberBase is System.UInt128
      || numberBase is System.UIntPtr
      || TNumberBase.Zero.CompareTo(typeof(TNumberBase).GetField("MinValue")?.GetValue(null) ?? TNumberBase.One) == 0 // By field "MinValue" being zero.
      || typeof(TNumberBase).IsAssignableToGenericType(typeof(System.Numerics.IUnsignedNumber<>)); // By being assignable to System.Numerics.IUnsignedNumber<>.
  }
}
