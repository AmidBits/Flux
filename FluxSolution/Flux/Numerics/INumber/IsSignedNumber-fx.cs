namespace Flux
{
  public static partial class Number
  {
    /// <summary>
    /// <para>Determines whether the <paramref name="value"/> is signed.</para>
    /// <para><seealso href="https://stackoverflow.com/a/13609383/3178666"/></para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    public static bool IsSignedNumber<TNumber>(this TNumber value)
      where TNumber : System.Numerics.INumber<TNumber>
      => typeof(TNumber) is var type
      && (
        type.IsSignedNumericType()
        // Or if 0.CompareTo(FIELD:MinValue) > 0, i.e. zero is greater than the field value. https://stackoverflow.com/a/13609383
        || TNumber.Zero.CompareTo(type.GetField("MinValue")?.GetValue(null) ?? TNumber.Zero) > 0
        // Or if 0.CompareTo(PROPERTY:NegativeOne) > 0, i.e. zero is greater than the property value.
        || TNumber.Zero.CompareTo(type.GetProperty("NegativeOne", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public)?.GetValue(null) ?? TNumber.Zero) > 0
        // Or if 0.CompareTo(PROPERTY:"System.Numerics.ISignedNumber<[FULLY_QUALIFIED_TYPE_NAME]>.NegativeOne") > 0, i.e. zero is greater than the property value.
        || TNumber.Zero.CompareTo(type.GetProperty($"System.Numerics.ISignedNumber<{type.FullName}>.NegativeOne", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public)?.GetValue(null) ?? TNumber.Zero) > 0
        // Or if type is-assignable-to System.Numerics.ISignedNumber<>.
        || type.IsAssignableToGenericType(typeof(System.Numerics.ISignedNumber<>))
      );
  }
}
