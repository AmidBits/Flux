namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Determines whether the <paramref name="source"/> number is signed.</para>
    /// <para><seealso href="https://stackoverflow.com/a/13609383/3178666"/></para>
    /// </summary>
    public static bool IsSignedNumber<TSelf>(this TSelf source)
      where TSelf : System.Numerics.INumber<TSelf>
      => source is System.Decimal
      || source is System.Double
      || source is System.Half
      || source is System.Int16
      || source is System.Int32
      || source is System.Int64
      || source is System.Int128
      || source is System.IntPtr
      || source is System.Numerics.BigInteger
      || source is System.Runtime.InteropServices.NFloat
      || source is System.SByte
      || source is System.Single
      || TSelf.Zero.CompareTo(typeof(TSelf).GetField("MinValue")?.GetValue(null) ?? TSelf.Zero) > 0 // By field "MinValue" being less than zero.
      || typeof(TSelf).IsAssignableToGenericType(typeof(System.Numerics.ISignedNumber<>)); // By being assignable to System.Numerics.ISignedNumber<>.

    //public static bool IsSignedNumberByIsAssignableTo<TSelf>(TSelf source)
    //  where TSelf : System.Numerics.INumber<TSelf>
    //  => typeof(TSelf).IsAssignableToGenericType(typeof(System.Numerics.ISignedNumber<>));

    //public static bool IsSignedNumberByMinValue<TSelf>(TSelf source)
    //  where TSelf : System.Numerics.INumber<TSelf>
    //  => TSelf.Zero.CompareTo(typeof(TSelf).GetField("MinValue")?.GetValue(null) ?? TSelf.Zero) > 0;

    //public static bool IsSignedNumberByNegativeOne<TSelf>(this TSelf source)
    //  where TSelf : System.Numerics.INumber<TSelf>
    //  => TSelf.Zero.CompareTo(typeof(TSelf).GetProperty("NegativeOne", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public)?.GetValue(null) ?? TSelf.Zero) > 0
    //  || TSelf.Zero.CompareTo(typeof(TSelf).GetProperty($"System.Numerics.ISignedNumber<{typeof(TSelf).FullName}>.NegativeOne", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public)?.GetValue(null) ?? TSelf.Zero) > 0;

    //public static bool IsSignedNumberByTypeSpecificOverloads<TSelf>(TSelf source)
    //  where TSelf : System.Numerics.INumber<TSelf>
    //  => source is decimal || source is double || source is short || source is int || source is long || source is sbyte || source is float // Primitive signed types.
    //  || source is System.Half || source is System.Int128 || source is System.IntPtr || source is System.Runtime.InteropServices.NFloat || source is System.Numerics.BigInteger; // Extended signed types.
  }
}
