namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Determines whether the <paramref name="number"/> number is signed.</para>
    /// <para><seealso href="https://stackoverflow.com/a/13609383/3178666"/></para>
    /// </summary>
    public static bool IsSignedNumber<TNumber>(this TNumber number)
      where TNumber : System.Numerics.INumber<TNumber>
      => number
      is System.Decimal
      or System.Double
      or System.Half
      or System.Int128
      or System.Int16
      or System.Int32
      or System.Int64
      or System.IntPtr
      or System.Numerics.BigInteger
      or System.Numerics.Complex
      or System.Runtime.InteropServices.NFloat
      or System.SByte
      or System.Single
      // Or if 0.CompareTo(FIELD:MinValue) > 0, i.e. zero is greater than the field value. https://stackoverflow.com/a/13609383
      || TNumber.Zero.CompareTo(typeof(TNumber).GetField("MinValue")?.GetValue(null) ?? TNumber.Zero) > 0
      // Or if 0.CompareTo(PROPERTY:NegativeOne) > 0, i.e. zero is greater than the property value.
      || TNumber.Zero.CompareTo(typeof(TNumber).GetProperty("NegativeOne", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public)?.GetValue(null) ?? TNumber.Zero) > 0
      // Or if 0.CompareTo(PROPERTY:"System.Numerics.ISignedNumber<[FULLY_QUALIFIED_TYPE_NAME]>.NegativeOne") > 0, i.e. zero is greater than the property value.
      || TNumber.Zero.CompareTo(typeof(TNumber).GetProperty($"System.Numerics.ISignedNumber<{typeof(TNumber).FullName}>.NegativeOne", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public)?.GetValue(null) ?? TNumber.Zero) > 0
      // Or if type is-assignable-to System.Numerics.ISignedNumber<>.
      || typeof(TNumber).IsAssignableToGenericType(typeof(System.Numerics.ISignedNumber<>));
  }
}
