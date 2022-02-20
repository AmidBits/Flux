namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Raw implementation with all arguments accounted for.</summary>
    public static int DigitCountImpl(System.Numerics.BigInteger value, int radix)
      => System.Convert.ToInt32(System.Math.Floor(System.Numerics.BigInteger.Log(System.Numerics.BigInteger.Abs(value), radix)) + 1);
    /// <summary>Returns the count of all digits in the value using the specified radix.</summary>
    /// <remarks>Uses Ceiling instead of Floor because of Log(value, radix) rounding.</remarks>
    public static int DigitCount(System.Numerics.BigInteger value, int radix)
      => value == 0 ? 1 : radix >= 2 ? DigitCountImpl(value, radix) : throw new System.ArgumentOutOfRangeException(nameof(radix));

    /// <summary>Raw implementation with all arguments accounted for.</summary>
    public static int DigitCountImpl(int value, int radix)
      => System.Convert.ToInt32(System.Math.Floor(System.Math.Log(System.Math.Abs(value), radix)) + 1);
    /// <summary>Returns the count of all digits in the value using the specified radix.</summary>
    /// <remarks>Uses Ceiling instead of Floor because of Log(value, radix) rounding.</remarks>
    public static int DigitCount(int value, int radix)
      => value == 0 ? 1 : radix >= 2 ? DigitCountImpl(value, radix) : throw new System.ArgumentOutOfRangeException(nameof(radix));

    /// <summary>Raw implementation with all arguments accounted for.</summary>
    public static int DigitCountImpl(long value, int radix)
      => System.Convert.ToInt32(System.Math.Floor(System.Math.Log(System.Math.Abs(value), radix)) + 1);
    /// <summary>Returns the count of all digits in the value using the specified radix.</summary>
    /// <remarks>Uses Ceiling instead of Floor because of Log(value, radix) rounding.</remarks>
    public static int DigitCount(long value, int radix)
      => value == 0 ? 1 : radix >= 2 ? DigitCountImpl(value, radix) : throw new System.ArgumentOutOfRangeException(nameof(radix));

    /// <summary>Returns the count of all digits in the value using the specified radix.</summary>
    /// <remarks>Uses Ceiling instead of Floor because of Log(value, radix) rounding.</remarks>
    [System.CLSCompliant(false)]
    public static int DigitCount(uint value, int radix)
      => value == 0 ? 1 : radix >= 2 ? System.Convert.ToInt32(System.Math.Floor(System.Math.Log(value, radix)) + 1) : throw new System.ArgumentOutOfRangeException(nameof(radix));

    /// <summary>Returns the count of all digits in the value using the specified radix.</summary>
    /// <remarks>Uses Ceiling instead of Floor because of Log(value, radix) rounding.</remarks>
    [System.CLSCompliant(false)]
    public static int DigitCount(ulong value, int radix)
      => value == 0 ? 1 : radix >= 2 ? System.Convert.ToInt32(System.Math.Floor(System.Math.Log(value, radix)) + 1) : throw new System.ArgumentOutOfRangeException(nameof(radix));
  }
}
