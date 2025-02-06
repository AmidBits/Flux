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
      => number is System.Decimal or System.Double or System.Half or System.Int16 or System.Int32 or System.Int64 or System.Int128 or System.IntPtr or System.Numerics.BigInteger or System.Runtime.InteropServices.NFloat or System.SByte or System.Single
      || TNumber.Zero.CompareTo(typeof(TNumber).GetField("MinValue")?.GetValue(null) ?? TNumber.Zero) > 0 // Or field "MinValue" is negative (and therefor zero-compare-to(MinValue) would be greater-than 0).
      || typeof(TNumber).IsAssignableToGenericType(typeof(System.Numerics.ISignedNumber<>)); // Or is-assignable-to System.Numerics.ISignedNumber<>.

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
  }
}
