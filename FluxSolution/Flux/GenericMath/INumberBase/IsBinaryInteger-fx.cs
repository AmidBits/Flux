namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>
    /// <para>Determines whether the <paramref name="numberBase"/> number is a binary integer.</para>
    /// <para><seealso href="https://stackoverflow.com/a/13609383/3178666"/></para>
    /// </summary>
    /// <remarks>For types not covered by type-specific overloads, this function uses reflection to find whether the <paramref name="numberBase"/> type implements <see cref="System.Numerics.IBinaryInteger{TSelf}"/>.</remarks>
    /// <typeparam name="TNumberBase"></typeparam>
    /// <param name="numberBase"></param>
    /// <returns></returns>
    public static bool IsBinaryInteger<TNumberBase>(this TNumberBase numberBase)
      where TNumberBase : System.Numerics.INumberBase<TNumberBase>
      => numberBase
      is System.Byte
      or System.Char
      or System.Int128
      or System.Int16
      or System.Int32
      or System.Int64
      or System.IntPtr
      or System.Numerics.BigInteger
      or System.SByte
      or System.UInt128
      or System.UInt16
      or System.UInt32
      or System.UInt64
      or System.UIntPtr
      // Or type is-assignable-to System.Numerics.IBinaryInteger<>.
      || typeof(TNumberBase).IsAssignableToGenericType(typeof(System.Numerics.IBinaryInteger<>));
  }
}
