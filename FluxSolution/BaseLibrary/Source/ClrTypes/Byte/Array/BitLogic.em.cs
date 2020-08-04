namespace Flux
{
  public static partial class XtensionsArray
  {
    /// <summary>Determines the number of bits that are 1.</summary>
    public static int Bit1Count(this byte[] source)
    {
      var count = 0;
      foreach (var b in source ?? throw new System.ArgumentNullException(nameof(source)))
        count += Math.ByteBit1Count[b];
      return count;
    }

    /// <summary>Determines the most significant 1 bit.</summary>
    public static int BitLength(this byte[] source)
    {
      var length = 0;
      foreach (var b in source ?? throw new System.ArgumentNullException(nameof(source)))
        length += Math.BitLength(b);
      return length;
    }

    /// <summary>Creates a new byte[count] of bitwise AND values using source[sourceStartAt..count] and other[otherStartAt..count].</summary>
    public static byte[] BitwiseAnd(this byte[] source, int startAt, byte[] other, int otherStartAt, int count)
    {
      var result = new byte[count];
      for (var index = 0; index < count; count++, startAt++, otherStartAt++)
        result[index] = (byte)(source[startAt] & other[otherStartAt]);
      return result;
    }
    /// <summary>Performs an in-place (source) bitwise AND using source[sourceStartAt..count] and other[otherStartAt..count].</summary>
    public static byte[] BitwiseAndInPlace(this byte[] source, int startAt, byte[] other, int otherStartAt, int count)
    {
      while (count-- > 0)
        source[startAt++] = (byte)(source[startAt] & other[otherStartAt++]);
      return source;
    }

    /// <summary>Creates a new byte[count] of values from source[sourceStartAt..count] negated.</summary>
    public static byte[] BitwiseNot(this byte[] source, int startAt, int count)
    {
      var result = new byte[count];
      for (var index = 0; index < count; index++, startAt++)
        result[index] = (byte)~source[startAt];
      return result;
    }
    /// <summary>Creates a new byte[] of values from source negated.</summary>
    public static byte[] BitwiseNot(this byte[] source)
      => BitwiseNot(source, 0, source.Length);

    /// <summary>Performs an in-place negating of source[sourceStartIndex..count].</summary>
    public static byte[] BitwiseNotInPlace(this byte[] source, int startAt, int count)
    {
      for (int index = startAt, overflowIndex = startAt + count; index < overflowIndex; index++)
        source[index] = (byte)~source[index];
      return source;
    }
    /// <summary>Performs an in-place negating of source.</summary>
    public static byte[] BitwiseNotInPlace(this byte[] source)
      => BitwiseNotInPlace(source, 0, source.Length);

    /// <summary>Creates a new byte[count] of bitwise OR values using source[sourceStartAt..count] and other[otherStartAt..count].</summary>
    public static byte[] BitwiseOr(this byte[] source, int startAt, byte[] other, int otherStartAt, int count)
    {
      var result = new byte[count];
      for (var index = 0; index < count; count++, startAt++, otherStartAt++)
        result[index] = (byte)(source[startAt] | other[otherStartAt]);
      return result;
    }
    /// <summary>Performs an in-place (source) bitwise OR using source[sourceStartAt..count] and other[otherStartAt..count].</summary>
    public static byte[] BitwiseOrInPlace(this byte[] source, int startAt, byte[] other, int otherStartAt, int count)
    {
      while (count-- > 0)
        source[startAt++] = (byte)(source[startAt] | other[otherStartAt++]);
      return source;
    }

    /// <summary>Performs an in-place left rotation of all bits in the array.</summary>
    public static byte[] BitwiseRotateLeft(this byte[] source)
    {
      if (BitwiseShiftLeft(source)) source[source.Length - 1] |= 0x01;

      return source;
    }
    /// <summary>Performs an in-place right rotation of all bits in the array.</summary>
    public static byte[] BitwiseRotateRight(this byte[] source)
    {
      if (BitwiseShiftRight(source)) source[0] |= 0x80;

      return source;
    }

    /// <summary>Performs an in-place one bit shift to the left on all bytes, and returns whether the MSB or overflow bit was set.</summary>
    public static bool BitwiseShiftLeft(this byte[] source)
    {
      var carryFlag = (source[0] & 0x80) > 0;

      for (var index = 0; index < source.Length; index++)
        source[index] = (byte)(index < (source.Length - 1) && (source[index + 1] & 0x80) > 0 ? (source[index] << 1) | 0x01 : source[index] << 1);

      return carryFlag;
    }
    /// <summary>Performs an in-place ont bit shift to the right on all bytes, and returns whether the LSB or overflow bit was set.</summary>
    public static bool BitwiseShiftRight(this byte[] source)
    {
      var index = source.Length - 1;

      var carryFlag = (source[index] & 0x01) > 0;

      for (; index >= 0; index--)
        source[index] = (byte)(index > 0 && (source[index - 1] & 0x01) > 0 ? (source[index] >> 1) | 0x80 : source[index] >> 1);

      return carryFlag;
    }

    /// <summary>Returns a new byte sequence bit-shifted left by count bits, by extending the sequences (on the left) with the necessary number of bytes.</summary>
    public static System.Collections.Generic.IEnumerable<byte> BitwiseShiftLeft(this System.Collections.Generic.IEnumerable<byte> source, int count)
    {
      var effectiveShift = (count % 8);
      var inverseShift = (8 - effectiveShift);

      byte previousByte = 0;

      foreach (var currentByte in source)
      {
        yield return (byte)((previousByte << effectiveShift) | (currentByte >> inverseShift));

        previousByte = currentByte;
      }

      yield return (byte)(previousByte << effectiveShift);

      for (var i = count / 8; i > 0; i--)
      {
        yield return 0;
      }
    }
    /// <summary>Returns a new byte sequence bit-shifted right by count bits, by extending the sequence (on the right) with the necessary number of bytes.</summary>
    public static System.Collections.Generic.IEnumerable<byte> BitwiseShiftRight(this System.Collections.Generic.IEnumerable<byte> source, int count)
    {
      for (var i = count / 8; i > 0; i--)
      {
        yield return 0;
      }

      var effectiveShift = (count % 8);
      var inverseShift = (8 - effectiveShift);

      byte previousByte = 0;

      foreach (var currentByte in source)
      {
        yield return (byte)((previousByte << inverseShift) | (currentByte >> effectiveShift));

        previousByte = currentByte;
      }

      yield return (byte)(previousByte << inverseShift);
    }

    /// <summary>Creates a new byte[count] of bitwise XOR values using source[sourceStartAt..count] and other[otherStartAt..count].</summary>
    public static byte[] BitwiseXor(this byte[] source, int startAt, byte[] other, int otherStartAt, int count)
    {
      var result = new byte[count];
      for (var index = 0; index < count; count++, startAt++, otherStartAt++)
        result[index] = (byte)(source[startAt] ^ other[otherStartAt]);
      return result;
    }
    /// <summary>Performs an in-place (source) bitwise XOR using source[sourceStartAt..count] and other[otherStartAt..count].</summary>
    public static byte[] BitwiseXorInPlace(this byte[] source, int startAt, byte[] other, int otherStartAt, int count)
    {
      while (count-- > 0)
        source[startAt++] = (byte)(source[startAt] ^ other[otherStartAt++]);
      return source;
    }

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
              return (index * 8) + Math.ByteBit1Count[mask];

          return (index * 8);
        }
      }

      return source.Length;
    }
    /// <summary>Determines the number of trailing (right or least significant) consecutive bits, in the array, and whether they are 0 or 1.</summary>
    public static int CountMostSignificantBits(this byte[] source, out int bit)
    {
      if ((source ?? throw new System.ArgumentNullException(nameof(source))).Length == 0) throw new System.ArgumentOutOfRangeException(nameof(source));

      bit = (source[source.Length - 1] & 0x01);
      var bits = bit != 0 ? 0xFF : 0x00;

      for (var index = source.Length - 1; index >= 0; index--)
      {
        if (source[index] is var value && value != bits)
        {
          for (byte mask = 0x7F; mask > 0; mask >>= 1)
            if ((value & mask) == (bits & mask))
              return (source.Length - index - 1) * 8 + Math.ByteBit1Count[mask];

          return (source.Length - index - 1) * 8;
        }
      }

      return source.Length;
    }

    /// <summary>Folds all bits from most significant bit (MSB) of all bytes, to the right throughout the array.</summary>
    public static byte[] FoldLeastSignificantBits(this byte[] source)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

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
    /// <summary>Folds all bits from least significant bit (LSB) of all bytes, to the left throughout the array.</summary>
    public static byte[] FoldMostSignificantBits(this byte[] source)
    {
      var index = (source ?? throw new System.ArgumentNullException(nameof(source))).Length;

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

    ///// <summary>Sets the specified count of most significant bits to 1, in the array.</summary>
    //public static byte[] SetMostSignificantBits(this byte[] source, int count, bool state)
    //{
    //  if (source is null) throw new System.ArgumentNullException(nameof(source));

    //  var bitState = (byte)(state ? 0xFF : 0x00);
    //  if (count > source.Length * 8) throw new System.ArgumentOutOfRangeException(nameof(count));
    //  //if (count / 8 is var q && q > 0) System.Array.Fill<byte>(source, 0xFF, 0, q);
    //  if (count / 8 is var q && q > 0) for (var index = 0; index < q; index++) source[index] = bitState;
    //  if (count % 8 is var r && r > 0 && (byte)(~((1 << (8 - r)) - 1) & 0xFF) is var mask) source[q] = (byte)((source[q] & ~mask) | (bitState & mask));
    //  return source;
    //}
    ///// <summary>Sets the specified count of least significant bits to 1, in the array.</summary>
    //public static byte[] SetLeastSignificantBits(this byte[] source, int count, bool state)
    //{
    //  if (source is null) throw new System.ArgumentNullException(nameof(source));

    //  var bitState = (byte)(state ? 0xFF : 0x00);
    //  if (count > source.Length * 8) throw new System.ArgumentOutOfRangeException(nameof(count));
    //  //if (count / 8 is var q && q > 0) System.Array.Fill<byte>(source, 0xFF, source.Length - q, q);
    //  if (count / 8 is var q && q > 0) for (int index = source.Length - 1, minIndex = source.Length - q; index >= minIndex; index--) source[index] = bitState;
    //  if (count % 8 is var r && r > 0 && (byte)((1 << r) - 1) is var mask) source[source.Length - q - 1] = (byte)((source[source.Length - q - 1] & ~mask) | (bitState & mask));
    //  return source;
    //}

    public static byte[] SetBits(this byte[] source, long startBitIndex, long bitCount, bool state)
    {
      var sourceBitLength = source.Length * 8;

      if (startBitIndex < 0 || startBitIndex > sourceBitLength) throw new System.ArgumentOutOfRangeException(nameof(startBitIndex));
      if (bitCount < 0 || (startBitIndex + bitCount is var endBitIndex && endBitIndex > sourceBitLength)) throw new System.ArgumentOutOfRangeException(nameof(bitCount));

      var bitState = (byte)(state ? 0xFF : 0x00);

      var startQuotient = startBitIndex / 8;
      var startRemainder = startBitIndex % 8;
      var startMask = (byte)((1 << (8 - (int)startRemainder)) - 1);

      var endQuotient = endBitIndex / 8;
      var endRemainder = endBitIndex % 8;
      var endMask = (byte)(~((0x80 >> ((int)endRemainder - 1)) - 1) & 0xFF);

      if (startQuotient == endQuotient && (startMask & endMask) is var mixedMask) // All in the same byte?
      {
        source[startQuotient] = state ? (byte)(source[startQuotient] | mixedMask) : (byte)(source[startQuotient] & ~mixedMask);
      }
      else // Or in 2 or more bytes?
      {
        if (startRemainder > 0) source[startQuotient] = (byte)((source[startQuotient] & (~startMask & 0xFF)) | (bitState & startMask));
        if (endRemainder > 0) source[endQuotient] = (byte)((source[endQuotient] & (~endMask & 0xFF)) | (bitState & endMask));

        for (var index = startRemainder > 0 ? startQuotient + 1 : startQuotient; index < endQuotient; index++) source[index] = bitState; // Set whole bytes if needed.
      }

      return source;
    }
  }
}
