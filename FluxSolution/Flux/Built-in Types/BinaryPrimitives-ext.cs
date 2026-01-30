namespace Flux
{
  public static class BinaryPrimitivesExtensions
  {
    extension(System.Buffers.Binary.BinaryPrimitives)
    {
      #region Read..

      public static System.Numerics.BigInteger ReadBigIntegerBigEndian(System.ReadOnlySpan<byte> source)
        => new(source, false, true);

      public static System.Numerics.BigInteger ReadBigIntegerLittleEndian(System.ReadOnlySpan<byte> source)
        => new(source, false, false);

      /// <summary>
      /// <para>Reads a 16-byte <paramref name="source"/> to a <see cref="System.Decimal"/> using the specified <paramref name="endianess"/>.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <param name="endianess"></param>
      /// <returns></returns>
      public static decimal ReadDecimalBigEndian(System.ReadOnlySpan<byte> source)
      {
        var parts = decimal.GetBits(decimal.Zero);

        for (var i = parts.Length - 1; i >= 0; i--)
          parts[i] = System.Buffers.Binary.BinaryPrimitives.ReadInt32BigEndian(source[(i * 4)..]);

        return new(parts);
      }

      /// <summary>
      /// <para>Reads a 16-byte <paramref name="source"/> to a <see cref="System.Decimal"/> using the specified <paramref name="endianess"/>.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <param name="endianess"></param>
      /// <returns></returns>
      public static decimal ReadDecimalLittleEndian(System.ReadOnlySpan<byte> source)
      {
        var parts = decimal.GetBits(decimal.Zero);

        for (var i = 0; i < parts.Length; i++)
          parts[i] = System.Buffers.Binary.BinaryPrimitives.ReadInt32LittleEndian(source[(i * 4)..]);

        return new(parts);
      }

      /// <summary>
      /// <para>Reads a 16-byte <paramref name="source"/> to a <see cref="System.Int128"/> using the specified <paramref name="endianess"/>.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <param name="endianess"></param>
      /// <returns></returns>
      public static System.Int128 ReadInt128BigEndian(System.ReadOnlySpan<byte> source)
        => unchecked((System.Int128)ReadUInt128BigEndian(source));

      /// <summary>
      /// <para>Reads a 16-byte <paramref name="source"/> to a <see cref="System.Int128"/> using the specified <paramref name="endianess"/>.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <param name="endianess"></param>
      /// <returns></returns>
      public static System.Int128 ReadInt128LittleEndian(System.ReadOnlySpan<byte> source)
        => unchecked((System.Int128)ReadUInt128LittleEndian(source));

      /// <summary>
      /// <para>Reads a 16-byte <paramref name="source"/> to a <see cref="System.UInt128"/> using the specified <paramref name="endianess"/>.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <param name="endianess"></param>
      /// <returns></returns>
      [System.CLSCompliant(false)]
      public static System.UInt128 ReadUInt128BigEndian(System.ReadOnlySpan<byte> source)
      {
        if (source.Length < 16) throw new System.ArgumentOutOfRangeException(nameof(source));

        return (System.UInt128)source[0] << 120
             | (System.UInt128)source[1] << 112
             | (System.UInt128)source[2] << 104
             | (System.UInt128)source[3] << 96
             | (System.UInt128)source[4] << 88
             | (System.UInt128)source[5] << 80
             | (System.UInt128)source[6] << 72
             | (System.UInt128)source[7] << 64
             | (System.UInt128)source[8] << 56
             | (System.UInt128)source[9] << 48
             | (System.UInt128)source[10] << 40
             | (System.UInt128)source[11] << 32
             | (System.UInt128)source[12] << 24
             | (System.UInt128)source[13] << 16
             | (System.UInt128)source[14] << 8
             | (System.UInt128)source[15];
      }

      /// <summary>
      /// <para>Reads a 16-byte <paramref name="source"/> to a <see cref="System.UInt128"/> using the specified <paramref name="endianess"/>.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <param name="endianess"></param>
      /// <returns></returns>
      [System.CLSCompliant(false)]
      public static System.UInt128 ReadUInt128LittleEndian(System.ReadOnlySpan<byte> source)
      {
        if (source.Length < 16) throw new System.ArgumentOutOfRangeException(nameof(source));

        return (System.UInt128)source[15] << 120
             | (System.UInt128)source[14] << 112
             | (System.UInt128)source[13] << 104
             | (System.UInt128)source[12] << 96
             | (System.UInt128)source[11] << 88
             | (System.UInt128)source[10] << 80
             | (System.UInt128)source[9] << 72
             | (System.UInt128)source[8] << 64
             | (System.UInt128)source[7] << 56
             | (System.UInt128)source[6] << 48
             | (System.UInt128)source[5] << 40
             | (System.UInt128)source[4] << 32
             | (System.UInt128)source[3] << 24
             | (System.UInt128)source[2] << 16
             | (System.UInt128)source[1] << 8
             | (System.UInt128)source[0];
      }

      #endregion

      #region Write..

      public static int WriteBigIntegerBigEndian(System.Span<byte> source, System.Numerics.BigInteger value)
      {
        var count = value.GetByteCount();

        if (!value.TryWriteBytes(source[..count], out var bytesWritten, false, true))
          throw new System.InvalidOperationException();

        return count;
      }

      public static int WriteBigIntegeLittleEndian(System.Span<byte> source, System.Numerics.BigInteger value)
      {
        var count = value.GetByteCount();

        if (!value.TryWriteBytes(source[..count], out var bytesWritten, false, false))
          throw new System.InvalidOperationException();

        return count;
      }

      /// <summary>
      /// <para>Writes a <see cref="System.Decimal"/> to a 16-byte <paramref name="source"/> using the specified <paramref name="endianess"/>.</para>
      /// </summary>
      /// <param name="value"></param>
      /// <param name="source"></param>
      /// <param name="endianess"></param>
      public static void WriteDecimalBigEndian(System.Span<byte> source, decimal value)
      {
        var parts = decimal.GetBits(value);

        for (var i = parts.Length - 1; i >= 0; i--)
          System.Buffers.Binary.BinaryPrimitives.WriteInt32BigEndian(source[(i * 4)..], parts[i]);
      }

      /// <summary>
      /// <para>Writes a <see cref="System.Decimal"/> to a 16-byte <paramref name="source"/> using the specified <paramref name="endianess"/>.</para>
      /// </summary>
      /// <param name="value"></param>
      /// <param name="source"></param>
      /// <param name="endianess"></param>
      public static void WriteDecimalLittleEndian(System.Span<byte> source, decimal value)
      {
        var parts = decimal.GetBits(value);

        for (var i = 0; i < parts.Length; i++)
          System.Buffers.Binary.BinaryPrimitives.WriteInt32LittleEndian(source[(i * 4)..], parts[i]);
      }

      /// <summary>
      /// <para>Writes a <see cref="System.Int128"/> to a 16-byte <paramref name="source"/> using the specified <paramref name="endianess"/>.</para>
      /// </summary>
      /// <param name="value"></param>
      /// <param name="source"></param>
      /// <param name="endianess"></param>
      public static void WriteInt128BigEndian(System.Span<byte> source, System.Int128 value)
        => WriteUInt128BigEndian(source, unchecked((System.UInt128)value));

      /// <summary>
      /// <para>Writes a <see cref="System.Int128"/> to a 16-byte <paramref name="source"/> using the specified <paramref name="endianess"/>.</para>
      /// </summary>
      /// <param name="value"></param>
      /// <param name="source"></param>
      /// <param name="endianess"></param>
      public static void WriteInt128LittleEndian(System.Span<byte> source, System.Int128 value)
        => WriteUInt128LittleEndian(source, unchecked((System.UInt128)value));

      /// <summary>
      /// <para>Writes a <see cref="System.UInt128"/> to a 16-byte <paramref name="source"/> using the specified <paramref name="endianess"/>.</para>
      /// </summary>
      /// <param name="value"></param>
      /// <param name="source"></param>
      /// <param name="endianess"></param>
      [System.CLSCompliant(false)]
      public static void WriteUInt128BigEndian(System.Span<byte> source, System.UInt128 value)
      {
        source[15] = (byte)value;
        source[14] = (byte)(value >> 8);
        source[13] = (byte)(value >> 16);
        source[12] = (byte)(value >> 24);
        source[11] = (byte)(value >> 32);
        source[10] = (byte)(value >> 40);
        source[9] = (byte)(value >> 48);
        source[8] = (byte)(value >> 56);
        source[7] = (byte)(value >> 64);
        source[6] = (byte)(value >> 72);
        source[5] = (byte)(value >> 80);
        source[4] = (byte)(value >> 88);
        source[3] = (byte)(value >> 96);
        source[2] = (byte)(value >> 104);
        source[1] = (byte)(value >> 112);
        source[0] = (byte)(value >> 120);
      }

      /// <summary>
      /// <para>Writes a <see cref="System.UInt128"/> to a 16-byte <paramref name="source"/> using the specified <paramref name="endianess"/>.</para>
      /// </summary>
      /// <param name="value"></param>
      /// <param name="source"></param>
      /// <param name="endianess"></param>
      [System.CLSCompliant(false)]
      public static void WriteUInt128LittleEndian(System.Span<byte> source, System.UInt128 value)
      {
        source[0] = (byte)value;
        source[1] = (byte)(value >> 8);
        source[2] = (byte)(value >> 16);
        source[3] = (byte)(value >> 24);
        source[4] = (byte)(value >> 32);
        source[5] = (byte)(value >> 40);
        source[6] = (byte)(value >> 48);
        source[7] = (byte)(value >> 56);
        source[8] = (byte)(value >> 64);
        source[9] = (byte)(value >> 72);
        source[10] = (byte)(value >> 80);
        source[11] = (byte)(value >> 88);
        source[12] = (byte)(value >> 96);
        source[13] = (byte)(value >> 104);
        source[14] = (byte)(value >> 112);
        source[15] = (byte)(value >> 120);
      }

      #endregion
    }
  }
}
