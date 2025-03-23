namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>
    /// <para>Determines whether the <paramref name="number"/> is unsigned.</para>
    /// <para><seealso href="https://stackoverflow.com/a/13609383/3178666"/></para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="number"></param>
    /// <returns></returns>
    public static bool IsUnsignedNumber<TNumber>(this TNumber number)
      where TNumber : System.Numerics.INumber<TNumber>
      => number
      is System.Byte
      or System.Char
      or System.UInt128
      or System.UInt16
      or System.UInt32
      or System.UInt64
      or System.UIntPtr
      // Or if 0.CompareTo(FIELD:MinValue) <= 0, i.e. zero is equal-or-less-than the number.
      || TNumber.Zero.CompareTo(typeof(TNumber).GetField("MinValue")?.GetValue(null) ?? TNumber.One) <= 0
      // Or if type is-assignable-to System.Numerics.IUnsignedNumber<>.
      || typeof(TNumber).IsAssignableToGenericType(typeof(System.Numerics.IUnsignedNumber<>));
  }
}
