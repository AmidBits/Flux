namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Determines whether the <paramref name="number"/> number is unsigned.</para>
    /// <para><seealso href="https://stackoverflow.com/a/13609383/3178666"/></para>
    /// </summary>
    public static bool IsUnsignedNumber<TNumber>(this TNumber number)
      where TNumber : System.Numerics.INumber<TNumber>
      => number is System.Byte or System.Char or System.UInt16 or System.UInt32 or System.UInt64 or System.UInt128 or System.UIntPtr
      || TNumber.Zero.CompareTo(typeof(TNumber).GetField("MinValue")?.GetValue(null) ?? TNumber.One) == 0 // Or field "MinValue" is zero (and therefor zero-compare-to(MinValue) would equal 0).
      || typeof(TNumber).IsAssignableToGenericType(typeof(System.Numerics.IUnsignedNumber<>)); // Or is-assignable-to System.Numerics.IUnsignedNumber<>.
  }
}
