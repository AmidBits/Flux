namespace Flux
{
  public static partial class SystemByteEm
  {
    /// <summary>Decodes chunks of bitsPerByte to bytes.</summary>
    public static System.Collections.Generic.IEnumerable<byte> BitDecode(this System.Collections.Generic.IEnumerable<byte> source, int bitsPerByte = 6)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      if (bitsPerByte > 8) throw new System.ArgumentOutOfRangeException(nameof(bitsPerByte));

      uint bitBuffer = 0;
      var bitIndex = 32;

      foreach (var currentBits in source)
      {
        bitBuffer = (bitBuffer << bitsPerByte) | (byte)((currentBits << (8 - bitsPerByte)) >> (8 - bitsPerByte));
        bitIndex -= bitsPerByte;

        while (bitIndex <= 24)
        {
          var currentByte = (byte)((bitBuffer << bitIndex) >> 24);

          if (!((bitIndex == 24 || bitIndex == 16) && currentByte == 0x40))
          {
            yield return currentByte;
          }

          bitIndex += 8;
        }
      }
    }
    /// <summary>Decodes chunks of bitsPerByte to bytes.</summary>
    public static System.Collections.Generic.IEnumerable<byte> BitDecode(this System.IO.Stream source, int bitsPerByte = 6)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      if (bitsPerByte > 8) throw new System.ArgumentOutOfRangeException(nameof(bitsPerByte));

      uint bitBuffer = 0;
      var bitIndex = 32;

      while (source.ReadByte() is var readByte && readByte > -1)
      {
        bitBuffer = (bitBuffer << bitsPerByte) | (byte)((readByte << (8 - bitsPerByte)) >> (8 - bitsPerByte));
        bitIndex -= bitsPerByte;

        while (bitIndex <= 24)
        {
          var currentByte = (byte)((bitBuffer << bitIndex) >> 24);

          if (!((bitIndex == 24 || bitIndex == 16) && currentByte == 0x40))
          {
            yield return currentByte;
          }

          bitIndex += 8;
        }
      }
    }

    /// <summary>Encodes bytes to chunks of bitsPerByte.</summary>
    public static System.Collections.Generic.IEnumerable<byte> BitEncode(this System.Collections.Generic.IEnumerable<byte> source, int bitsPerByte = 6)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      if (bitsPerByte > 8) throw new System.ArgumentOutOfRangeException(nameof(bitsPerByte));

      uint bitBuffer = 0;
      var bitIndex = 32;
      var byteCount = 0;

      foreach (var currentByte in source)
      {
        bitBuffer = (bitBuffer << 8) | currentByte;
        bitIndex -= 8;
        byteCount++;

        while (bitIndex <= (32 - bitsPerByte))
        {
          yield return (byte)((bitBuffer << bitIndex) >> (32 - bitsPerByte));

          bitIndex += bitsPerByte;
        }
      }

      if (bitIndex < 32)
      {
        yield return (byte)((bitBuffer << bitIndex) >> (32 - bitsPerByte));
      }

      for (var i = byteCount % 3; i < 3 && i > 0; i++)
      {
        yield return 0x40;
      }
    }

    /// <summary>Encodes bytes to chunks of bitsPerByte.</summary>
    public static System.Collections.Generic.IEnumerable<byte> BitEncode(this System.IO.Stream source, int bitsPerByte = 6)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      if (bitsPerByte > 8) throw new System.ArgumentOutOfRangeException(nameof(bitsPerByte));

      uint bitBuffer = 0;
      var bitIndex = 32;
      var byteCount = 0;

      while (source.ReadByte() is var readByte && readByte > -1)
      {
        bitBuffer = (bitBuffer << 8) | (byte)readByte;
        bitIndex -= 8;
        byteCount++;

        while (bitIndex <= (32 - bitsPerByte))
        {
          yield return (byte)((bitBuffer << bitIndex) >> (32 - bitsPerByte));

          bitIndex += bitsPerByte;
        }
      }

      if (bitIndex < 32)
      {
        yield return (byte)((bitBuffer << bitIndex) >> (32 - bitsPerByte));
      }

      for (var i = byteCount % 3; i < 3 && i > 0; i++)
      {
        yield return 0x40;
      }
    }
  }
}
