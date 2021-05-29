namespace Flux.Numerics
{
  public static partial class BitOps
  {
    // http://graphics.stanford.edu/~seander/bithacks.html#CountBitsSetNaive
    // http://aggregate.org/MAGIC/#Population%20Count%20(Ones%20Count)
    // http://aggregate.org/MAGIC/
    // http://graphics.stanford.edu/~seander/bithacks.html#CountBitsSetKernighan

    /// <summary>Also known as "population count" of a binary integer value x is the number of one bits in the value.</summary>
    public static int PopCount(System.Numerics.BigInteger value)
    {
      if (value <= uint.MaxValue)
        return PopCount((uint)value);
      else if (value <= ulong.MaxValue)
        return PopCount((ulong)value);

      var byteArray = value.ToByteArray();
      var byteArrayLength = byteArray.Length;

      var count = 0;

      var index = 0;

      while (byteArrayLength - index >= 4)
      {
        count += PopCount(System.BitConverter.ToUInt32(byteArray, 0));

        index += 4;
      }

      while (index < byteArrayLength)
        count += PopCount((uint)byteArray[index++]);

      return count;
    }

    /// <summary>Also known as "population count" of a binary integer value x is the number of one bits in the value.</summary>
    public static int PopCount(int value)
      => PopCount(unchecked((uint)value));

    /// <summary>Also known as "population count" of a binary integer value x is the number of one bits in the value.</summary>
    public static int PopCount(long value)
      => PopCount(unchecked((ulong)value));

    /// <summary>Also known as "population count" of a binary integer value x is the number of one bits in the value.</summary>
    [System.CLSCompliant(false)]
    public static int PopCount(uint value)
    {
#if NETCOREAPP
      return System.Numerics.BitOperations.PopCount(value);
#else
			value -= (value >> 1) & 0x55555555U;
			value = (value & 0x33333333U) + ((value >> 2) & 0x33333333U);
			value = (((value + (value >> 4)) & 0x0F0F0F0FU) * 0x01010101U) >> 24;
			return (int)value;
#endif
    }
    /// <summary>Also known as "population count" of a binary integer value x is the number of one bits in the value.</summary>
    [System.CLSCompliant(false)]
    public static int PopCount(ulong value)
    {
#if NETCOREAPP
      return System.Numerics.BitOperations.PopCount(value);
#else
			value -= (value >> 1) & 0x5555555555555555UL;
			value = (value & 0x3333333333333333UL) + ((value >> 2) & 0x3333333333333333UL);
			value = (((value + (value >> 4)) & 0x0F0F0F0F0F0F0F0FUL) * 0x0101010101010101UL) >> 56;
			return (int)value;
#endif
    }
  }
}
