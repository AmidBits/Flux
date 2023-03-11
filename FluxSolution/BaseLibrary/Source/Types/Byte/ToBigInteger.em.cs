using System.Linq;

namespace Flux
{
  public static partial class ByteEm
  {
    /// <summary>Creates a new BigInteger from the sequence of bytes and adds a padding zero byte if needed.</summary>
    public static System.Numerics.BigInteger ToBigInteger(this System.Collections.Generic.IEnumerable<byte> source)
      => new(source.ToArray() is var ba && (ba[^1] & 0x80) != 0 ? ba.Concat(new byte[] { 0 }).ToArray() : ba);

    /// <summary>Creates a new BigInteger from the sequence of bytes and adds a padding zero byte if needed.</summary>
    public static System.Numerics.BigInteger ToBigInteger(this System.Collections.Generic.IEnumerable<byte> source, int offset, int count)
      => new(source.Skip(offset).Take(count).ToArray() is var ba && (ba[^1] & 0x80) != 0 ? ba.Concat(new byte[] { 0 }).ToArray() : ba);
  }
}
