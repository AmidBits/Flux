namespace Flux
{
  public static partial class ByteEm
  {
    /// <summary>Performs an in-place one bit shift to the left on all bytes, and returns whether the MSB or overflow bit was set.</summary>
    public static bool BitShiftLeft(this byte[] source)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var maxIndex = source.Length - 1;

      var carryFlag = (source[0] & 0x80) > 0;

      for (var index = 1; index < maxIndex; index++)
        source[index] = (byte)(source[index] << 1 | ((source[index + 1] & 0x80) >> 7));

      source[maxIndex] <<= 1;

      return carryFlag;
    }
    /// <summary>Performs an in-place ont bit shift to the right on all bytes, and returns whether the LSB or overflow bit was set.</summary>
    public static bool BitShiftRight(this byte[] source)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var index = source.Length - 1;

      var carryFlag = (source[index] & 0x01) > 0;

      for (; index > 0; index--)
        source[index] = (byte)(((source[index - 1] & 0x01) << 7) | source[index] >> 1);

      source[0] >>= 1;

      return carryFlag;
    }
  }
}
