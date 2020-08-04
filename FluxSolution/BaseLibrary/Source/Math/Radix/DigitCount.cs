namespace Flux
{
  public static partial class Math
  {
    /// <summary>Returns the count of all digits in the value using the specified radix.</summary>
    /// <remarks>Uses Ceiling instead of Floor because of Log(value, radix) rounding.</remarks>
    public static int DigitCount(System.Numerics.BigInteger value, int radix)
      => value == 0 ? 1 : unchecked((int)System.Math.Ceiling(System.Numerics.BigInteger.Log(value > 0 ? value + 1 : -value + 1, radix > 0 ? radix : throw new System.ArgumentOutOfRangeException(nameof(radix)))));

    /// <summary>Returns the count of all digits in the value using the specified radix.</summary>
    /// <remarks>Uses Ceiling instead of Floor because of Log(value, radix) rounding.</remarks>
    public static int DigitCount(int value, int radix)
      => value == 0 ? 1 : unchecked((int)System.Math.Ceiling(System.Math.Log(value > 0 ? value + 1 : -value + 1, radix > 0 ? radix : throw new System.ArgumentOutOfRangeException(nameof(radix)))));
    /// <summary>Returns the count of all digits in the value using the specified radix.</summary>
    /// <remarks>Uses Ceiling instead of Floor because of Log(value, radix) rounding.</remarks>
    public static int DigitCount(long value, int radix)
      => value == 0 ? 1 : unchecked((int)System.Math.Ceiling(System.Math.Log(value > 0 ? value + 1 : -value + 1, radix > 0 ? radix : throw new System.ArgumentOutOfRangeException(nameof(radix)))));

    /// <summary>Returns the count of all digits in the value using the specified radix.</summary>
    /// <remarks>Uses Ceiling instead of Floor because of Log(value, radix) rounding.</remarks>
    [System.CLSCompliant(false)]
    public static int DigitCount(uint value, int radix)
      => value == 0 ? 1 : unchecked((int)System.Math.Ceiling(System.Math.Log(value + 1, radix > 0 ? radix : throw new System.ArgumentOutOfRangeException(nameof(radix)))));
    /// <summary>Returns the count of all digits in the value using the specified radix.</summary>
    /// <remarks>Uses Ceiling instead of Floor because of Log(value, radix) rounding.</remarks>
    [System.CLSCompliant(false)]
    public static int DigitCount(ulong value, int radix)
      => value == 0 ? 1 : unchecked((int)System.Math.Ceiling(System.Math.Log(value + 1, radix > 0 ? radix : throw new System.ArgumentOutOfRangeException(nameof(radix)))));
  }
}
