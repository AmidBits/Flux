using System.Reflection;

namespace Flux
{
  public static partial class BitOps
  {
    /// <summary>Reverses the bits of an integer.</summary>
    public static TSelf ReverseBits<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      var byteCount = value.GetByteCount();

      if (int.IsOddInteger(byteCount))
        throw new System.NotSupportedException();

      var bytes = new byte[byteCount];
      value.WriteLittleEndian(bytes);

      for (int inc = 0, dec = byteCount - 1; dec >= 0; inc += 2, dec -= 2)
      {
        var tmp = bytes[inc].MirrorBits();
        bytes[inc] = bytes[dec].MirrorBits();
        bytes[dec] = tmp;
      }

      return TSelf.ReadLittleEndian(bytes, typeof(System.Numerics.IUnsignedNumber<>).IsSupertypeOf(value.GetType()));
    }
  }
}
