namespace Flux
{
  public static partial class Bits
  {
#if !NET7_0_OR_GREATER

    /// <summary>The log base 2 of an integer is the same as the position of the highest bit set (or most significant bit set, MSB).</summary>
    public static int Log2(this System.Numerics.BigInteger value)
    {
      if (value > 255)
      {
        value.ToByteArrayEx(out var byteIndex, out var byteValue);

        return System.Numerics.BitOperations.Log2(byteValue) + byteIndex * 8;
      }
      else if (value > 0)
        return System.Numerics.BitOperations.Log2((uint)value);

      return 0;
    }
#endif
  }
}
