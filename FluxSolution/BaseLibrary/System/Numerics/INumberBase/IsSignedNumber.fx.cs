namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Determines whether the <paramref name="numberBase"/> number is signed.</para>
    /// <para><seealso href="https://stackoverflow.com/a/13609383/3178666"/></para>
    /// </summary>
    public static bool IsSignedNumber<TNumberBase>(this TNumberBase numberBase)
      where TNumberBase : System.Numerics.INumber<TNumberBase>
      => numberBase is System.Decimal
      || numberBase is System.Double
      || numberBase is System.Half
      || numberBase is System.Int16
      || numberBase is System.Int32
      || numberBase is System.Int64
      || numberBase is System.Int128
      || numberBase is System.IntPtr
      || numberBase is System.Numerics.BigInteger
      || numberBase is System.Runtime.InteropServices.NFloat
      || numberBase is System.SByte
      || numberBase is System.Single
      || TNumberBase.Zero.CompareTo(typeof(TNumberBase).GetField("MinValue")?.GetValue(null) ?? TNumberBase.Zero) > 0 // By field "MinValue" being less than zero.
      || typeof(TNumberBase).IsAssignableToGenericType(typeof(System.Numerics.ISignedNumber<>)); // By being assignable to System.Numerics.ISignedNumber<>.

    //public static bool IsSignedNumberByIsAssignableTo<TNumberBase>(TNumberBase numberBase)
    //  where TNumberBase : System.Numerics.INumber<TNumberBase>
    //  => typeof(TNumberBase).IsAssignableToGenericType(typeof(System.Numerics.ISignedNumber<>));

    //public static bool IsSignedNumberByMinValue<TNumberBase>(TNumberBase numberBase)
    //  where TNumberBase : System.Numerics.INumber<TNumberBase>
    //  => TNumberBase.Zero.CompareTo(typeof(TNumberBase).GetField("MinValue")?.GetValue(null) ?? TNumberBase.Zero) > 0;

    //public static bool IsSignedNumberByNegativeOne<TNumberBase>(this TNumberBase numberBase)
    //  where TNumberBase : System.Numerics.INumber<TNumberBase>
    //  => TNumberBase.Zero.CompareTo(typeof(TNumberBase).GetProperty("NegativeOne", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public)?.GetValue(null) ?? TNumberBase.Zero) > 0
    //  || TNumberBase.Zero.CompareTo(typeof(TNumberBase).GetProperty($"System.Numerics.ISignedNumber<{typeof(TNumberBase).FullName}>.NegativeOne", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public)?.GetValue(null) ?? TNumberBase.Zero) > 0;

    //public static bool IsSignedNumberByTypeSpecificOverloads<TNumberBase>(TNumberBase numberBase)
    //  where TNumberBase : System.Numerics.INumber<TNumberBase>
    //  => numberBase is decimal || numberBase is double || numberBase is short || numberBase is int || numberBase is long || numberBase is sbyte || numberBase is float // Primitive signed types.
    //  || numberBase is System.Half || numberBase is System.Int128 || numberBase is System.IntPtr || numberBase is System.Runtime.InteropServices.NFloat || numberBase is System.Numerics.BigInteger; // Extended signed types.
  }
}
