namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Determines the number of leading (left or most significant) consecutive bits, in the array, and whether they are 0 or 1.</summary>
    public static int CountLeastSignificantBits(this byte[] source, out int bit)
    {
      if ((source ?? throw new System.ArgumentNullException(nameof(source))).Length == 0) throw new System.ArgumentOutOfRangeException(nameof(source));

      bit = (source[0] & 0x80) >> 7;
      var bits = bit != 0 ? 0xFF : 0x00;

      for (var index = 0; index < source.Length; index++)
      {
        if (source[index] is var value && value != bits)
        {
          for (byte mask = 0xFE; mask > 0; mask <<= 1)
            if ((value & mask) == (bits & mask))
              return (index * 8) + System.Numerics.BitOperations.PopCount(mask);

          return (index * 8);
        }
      }

      return source.Length;
    }
    /// <summary>Determines the number of trailing (right or least significant) consecutive bits, in the array, and whether they are 0 or 1.</summary>
    public static int CountMostSignificantBits(this byte[] source, out int bit)
    {
      if ((source ?? throw new System.ArgumentNullException(nameof(source))).Length == 0) throw new System.ArgumentOutOfRangeException(nameof(source));

      bit = (source[^1] & 0x01);
      var bits = bit != 0 ? 0xFF : 0x00;

      for (var index = source.Length - 1; index >= 0; index--)
      {
        if (source[index] is var value && value != bits)
        {
          for (byte mask = 0x7F; mask > 0; mask >>= 1)
            if ((value & mask) == (bits & mask))
              return (source.Length - index - 1) * 8 + System.Numerics.BitOperations.PopCount(mask);

          return (source.Length - index - 1) * 8;
        }
      }

      return source.Length;
    }

    /// <summary>Folds all bits from least significant bit (LSB) of all bytes, throughout the array.</summary>
    public static byte[] FoldLeastSignificantBits(this byte[] source)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      var index = source.Length;

      while (--index >= 0)
      {
        if ((int)source[index] is var value && value > 0)
        {
          value |= (value << 1);
          value |= (value << 2);
          value |= (value << 4);
          source[index] = (byte)value;
          break;
        }
      }

      while (--index >= 0)
        source[index] = 0xFF;

      return source;
    }
    /// <summary>Folds all bits from most significant bit (MSB) of all bytes, throughout the array.</summary>
    public static byte[] FoldMostSignificantBits(this byte[] source)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      var index = -1;

      while (++index < source.Length)
      {
        if ((int)source[index] is var value && value > 0)
        {
          value |= (value >> 1);
          value |= (value >> 2);
          value |= (value >> 4);
          source[index] = (byte)value;
          break;
        }
      }

      while (++index < source.Length)
        source[index] = 0xFF;

      return source;
    }
  }
}
