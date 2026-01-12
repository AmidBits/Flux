namespace Flux.Transcode
{
  /// <summary>Custom Base64 encoding, where code 62 and 63 can be different than the typical '+' and '/'.</summary>
  public class CustomBase64
    : IStringTranscoder
  {
    /// <summary>
    /// <para>Base 64 Encoding (<see href="https://datatracker.ietf.org/doc/html/rfc4648#section-4"/>).</para>
    /// </summary>
    public static IStringTranscoder Rfc464s4 { get; } = new CustomBase64() { Char62 = '+', Char63 = '/' };

    /// <summary>
    /// <para>Base 64 Encoding with URL and Filename Safe Alphabet (<see href="https://datatracker.ietf.org/doc/html/rfc4648#section-5"/>).</para>
    /// </summary>
    public static IStringTranscoder Rfc4648s5 { get; } = new CustomBase64() { Char62 = '-', Char63 = '_' };

    /// <summary>
    /// <para>For IMAP mailbox names (<see href="https://datatracker.ietf.org/doc/html/rfc3501#section-5.1.3"/>).</para>
    /// </summary>
    public static IStringTranscoder Rfc3501 { get; } = new CustomBase64() { Char62 = '-', Char63 = '_' };

    public char Char62 { get; init; } = '+';
    public char Char63 { get; init; } = '/';
    public char PaddingCharacter { get; /*init;*/ } = '=';
    //public bool UsePadding { get; init; } = true;

    public byte[] DecodeString(string input)
    {
      var output = new byte[3 * int.EnvelopedDivRem(input.Length, 4).Quotient];

      byte byte0 = 0;
      byte byte1 = 0;
      byte byte2 = 0;

      for (int i = 0, o = 0; i < input.Length; i++, o++)
      {
        byte0 = CharToByte(input[i]);

        byte0 <<= 2;

        if (i++ < input.Length)
        {
          byte1 = CharToByte(input[i]);

          byte0 |= (byte)((byte1 >> 4) & 0b00000011);

          output[o] = byte0;

          byte1 <<= 4;

          if (i++ < input.Length)
          {
            byte2 = CharToByte(input[i]);

            byte1 |= (byte)(byte2 >> 2);

            output[++o] = byte1;

            byte2 <<= 6;

            if (i++ < input.Length)
            {
              byte2 |= CharToByte(input[i]);

              output[++o] = byte2;
            }
          }
        }
      }

      return output;
    }

    private byte CharToByte(char code)
    {
      if (code is >= 'A' and <= 'Z')
        return (byte)(code - 'A');
      if (code is >= 'a' and <= 'z')
        return (byte)(code - 'a' + 26);
      if (code is >= '0' and <= '9')
        return (byte)(code - '0' + 52);
      if (code == Char62)
        return 62;
      if (code == Char63)
        return 63;
      if (code == PaddingCharacter)
        return 0xFF;

      throw new System.ArgumentOutOfRangeException(nameof(code));
    }

    public string EncodeString(byte[] input)
    {
      //var (q, r) = input.Length.EnvelopedDivRem(3);
      var output = new char[4 * int.EnvelopedDivRem(input.Length, 3).Quotient];

      //var bits0 = input.ReadBits<byte>(1, 6);
      //var bits1 = input.ReadBits<byte>(6, 6);
      //var bits2 = input.ReadBits<byte>(12, 6);
      //var bits3 = input.ReadBits<byte>(18, 6);

      byte byte0, byte1, byte2, byte3;

      //using var ms = new System.IO.MemoryStream(input);
      //using var bsr = new Flux.IO.BitStream.BitStreamReader(ms);

      //for (var i = output.Length - 1 + r; i >= 0; i--)
      //{
      //  var b6 = (byte)bsr.ReadBits(6, out var read);
      //}

      for (int i = 0, o = 0; i < input.Length; i++, o++)
      {
        byte1 = input[i];

        byte0 = (byte)((byte1 >> 2) & 0b111111);

        byte1 = (byte)((byte1 << 4) & 0b110000);

        if (++i < input.Length)
        {
          byte2 = input[i];

          byte1 |= (byte)((byte2 >> 4) & 0b001111);
          byte2 = (byte)((byte2 << 2) & 0b111100);
        }
        else byte2 = (byte)0xFF;

        if (++i < input.Length)
        {
          byte3 = input[i];

          byte2 |= (byte)((byte3 >> 6) & 0b000011);
          byte3 = (byte)(byte3 & 0b111111);
        }
        else byte3 = (byte)0xFF;

        output[o] = ByteToChar(byte0);
        output[++o] = ByteToChar(byte1);
        output[++o] = ByteToChar(byte2);
        output[++o] = ByteToChar(byte3);
      }

      return output.AsSpan().ToString();
    }

    private char ByteToChar(byte code)
    {
      if (code is >= 0 and <= 25)
        return (char)(code + 'A');
      if (code is >= 26 and <= 51)
        return (char)((code - 26) + 'a');
      if (code is >= 52 and <= 61)
        return (char)((code - 52) + '0');
      if (code is 62)
        return Char62;
      if (code is 63)
        return Char63;
      if (code is 0xFF)
        return PaddingCharacter;

      throw new System.ArgumentOutOfRangeException(nameof(code));
    }
  }
}
