namespace Flux
{
  // PopCount for int/long, uint/ulong is kept for backwards compatibility, use System.Numerics.BitOperations.PopCount when available.

  public static partial class BitOps
  {
    // http://aggregate.org/MAGIC/#Population%20Count%20(Ones%20Count)
    // http://graphics.stanford.edu/~seander/bithacks.html#CountBitsSetNaive

    /// <summary>Also known as "population count" of a binary integer value x is the number of one bits in the value.</summary>
    public static int PopCount(System.Numerics.BigInteger value)
    {
      if (value <= uint.MaxValue)
        return System.Numerics.BitOperations.PopCount((uint)value);
      else if (value <= ulong.MaxValue)
        return System.Numerics.BitOperations.PopCount((ulong)value);

      var byteArray = value.ToByteArray();
      var byteArrayLength = byteArray.Length;

      var count = 0;

      var index = 0;

      while (byteArrayLength - index >= 4)
      {
        count += System.Numerics.BitOperations.PopCount(System.BitConverter.ToUInt32(byteArray, index));

        index += 4;
      }

      while (index < byteArrayLength)
        count += System.Numerics.BitOperations.PopCount((uint)byteArray[index++]);

      return count;
    }

    /// <summary>Also known as "population count" of a binary integer value x is the number of one bits in the value.</summary>
    public static int PopCount(int value)
      => System.Numerics.BitOperations.PopCount(unchecked((uint)value));

    /// <summary>Also known as "population count" of a binary integer value x is the number of one bits in the value.</summary>
    public static int PopCount(long value)
      => System.Numerics.BitOperations.PopCount(unchecked((ulong)value));

    ///// <summary>Also known as "population count" of a binary integer value x is the number of one bits in the value.</summary>
    //[System.CLSCompliant(false)]
    //public static int PopCount(uint value)
    //{
    //  value -= (value >> 1) & 0x55555555U;
    //  value = (value & 0x33333333U) + ((value >> 2) & 0x33333333U);
    //  value = (((value + (value >> 4)) & 0x0F0F0F0FU) * 0x01010101U) >> 24;
    //  return (int)value;
    //}
    ///// <summary>Also known as "population count" of a binary integer value x is the number of one bits in the value.</summary>
    //[System.CLSCompliant(false)]
    //public static int PopCount(ulong value)
    //{
    //  value -= (value >> 1) & 0x5555555555555555UL;
    //  value = (value & 0x3333333333333333UL) + ((value >> 2) & 0x3333333333333333UL);
    //  value = (((value + (value >> 4)) & 0x0F0F0F0F0F0F0F0FUL) * 0x0101010101010101UL) >> 56;
    //  return (int)value;
    //}
  }
}
