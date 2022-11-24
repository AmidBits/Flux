using System.Reflection;

namespace Flux
{
  public static partial class BitOps
  {
    /// <summary>
    /// <para>Reverses the bits of an integer. The LSBs (least significant bits) becomes the MSBs (most significant bits) and vice versa, i.e. the bits are mirrored across the integer storage space. It's a reversal of all bits.</para>
    /// See <see cref="ReverseBytes{TSelf}(TSelf)"/> for byte reversal.
    /// </summary>
    public static TSelf ReverseBits<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      var bytes = new byte[value.GetByteCountEx(out var byteCountEx, out var bitCount)];
      value.WriteLittleEndian(bytes);
      System.Array.Reverse(bytes, 0, byteCountEx);

      //for (int inc = 0, dec = byteCountEx - 1; inc <= dec; inc += 1, dec -= 1)
      //{
      //  var tmp = bytes[inc].MirrorBits();
      //  bytes[inc] = bytes[dec].MirrorBits();
      //  bytes[dec] = tmp;
      //}

      // NOTE: All this needs to be done in one loop somehow.

      for (var index = byteCountEx; index >= 0; index--)
        bytes[index] = bytes[index].MirrorBits();

      var shiftRight = byteCountEx * 8 - bitCount;
      var shiftLeft = 8 - shiftRight;

      if (shiftRight > 0)
        for (var i = 0; i < byteCountEx; i++)
          bytes[i] = (byte)((bytes[i] >> shiftRight) | ((i + 1) < byteCountEx ? (bytes[i + 1] << shiftLeft) : 0));

      return TSelf.ReadLittleEndian(bytes, typeof(System.Numerics.IUnsignedNumber<>).IsSupertypeOf(value.GetType()));
    }
  }
}
