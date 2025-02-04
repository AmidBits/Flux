namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Determines whether the <paramref name="numberBase"/> number is a binary integer.</para>
    /// <para><seealso href="https://stackoverflow.com/a/13609383/3178666"/></para>
    /// </summary>
    /// <remarks>For types not covered by type-specific overloads, this function uses reflection to find whether the <paramref name="numberBase"/> type implements <see cref="System.Numerics.IBinaryInteger{TSelf}"/>.</remarks>
    public static bool IsBinaryInteger<TNumberBase>(this TNumberBase numberBase)
      where TNumberBase : System.Numerics.INumberBase<TNumberBase>
      => numberBase is System.Byte
      || numberBase is System.Char
      || numberBase is System.Int16
      || numberBase is System.Int32
      || numberBase is System.Int64
      || numberBase is System.Int128
      || numberBase is System.IntPtr
      || numberBase is System.Numerics.BigInteger
      || numberBase is System.SByte
      || numberBase is System.UInt16
      || numberBase is System.UInt32
      || numberBase is System.UInt64
      || numberBase is System.UInt128
      || numberBase is System.UIntPtr
      || typeof(TNumberBase).IsAssignableToGenericType(typeof(System.Numerics.IBinaryInteger<>)); // By being assignable to System.Numerics.IBinaryInteger<>.
  }
}
