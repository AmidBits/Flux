namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Determines whether the <paramref name="source"/> number is a binary integer.</para>
    /// <para><seealso href="https://stackoverflow.com/a/13609383/3178666"/></para>
    /// </summary>
    /// <remarks>For types not covered by type-specific overloads, this function uses reflection to find whether the <paramref name="source"/> type implements <see cref="System.Numerics.IBinaryInteger{TSelf}"/>.</remarks>
    public static bool IsBinaryInteger<TSelf>(this TSelf source)
      where TSelf : System.Numerics.INumberBase<TSelf>
      => source is System.Byte
      || source is System.Char
      || source is System.Int16
      || source is System.Int32
      || source is System.Int64
      || source is System.Int128
      || source is System.IntPtr
      || source is System.Numerics.BigInteger
      || source is System.SByte
      || source is System.UInt16
      || source is System.UInt32
      || source is System.UInt64
      || source is System.UInt128
      || source is System.UIntPtr
      || typeof(TSelf).IsAssignableToGenericType(typeof(System.Numerics.IBinaryInteger<>)); // By being assignable to System.Numerics.IBinaryInteger<>.
  }
}
