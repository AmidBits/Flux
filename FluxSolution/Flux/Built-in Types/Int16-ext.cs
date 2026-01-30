namespace Flux
{
  public static partial class Int16Extensions
  {
    extension(System.Int16)
    {
      /// <summary>
      /// <para>The largest prime number that fits in the type.</para>
      /// </summary>
      public static short MaxPrimeNumber => 32749;

      /// <summary>
      /// <para>Bit-reversal of a ushort, i.e. trade place of bit 15 with bit 0 and bit 14 with bit 1 and so on.</para>
      /// </summary>
      [System.CLSCompliant(false)]
      public static ushort ReverseBits(ushort value)
      {
        ReverseBitsInPlace(ref value);

        return value;
      }

      /// <summary>
      /// <para>In-place (by ref) mirror the bits (bit-reversal of a ushort, i.e. trade place of bit 15 with bit 0 and bit 14 with bit 1 and so on.</para>
      /// </summary>
      [System.CLSCompliant(false)]
      public static void ReverseBitsInPlace(ref ushort value)
        => value = (ushort)(((((((byte)(value & 0xFF)) * 0x0202020202UL) & 0x010884422010UL) % 1023) << 8) | ((((((byte)((value >> 8) & 0xFF)) * 0x0202020202UL) & 0x010884422010UL) % 1023)));
      // Alternatively:
      // {
      //  value = (ushort)(((value & 0xAAAAu) >> 0x01) | ((value & 0x5555u) << 0x01));
      //  value = (ushort)(((value & 0xCCCCu) >> 0x02) | ((value & 0x3333u) << 0x02));
      //  value = (ushort)(((value & 0xF0F0u) >> 0x04) | ((value & 0x0F0Fu) << 0x04));
      //  value = (ushort)(((value & 0xFF00u) >> 0x08) | ((value & 0x00FFu) << 0x08));
      // }
    }
  }
}
