using System.Linq;

namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Creates a new BigInteger from the sequence of bytes and adds a padding zero byte if needed.</summary>
    public static System.Numerics.BigInteger ToBigInteger(this System.Collections.Generic.IEnumerable<byte> source)
      => new System.Numerics.BigInteger(source.ToArray() is var ba && (ba[ba.Length - 1] & 0x80) != 0 ? ba.Append((byte)0).ToArray() : ba);

    /// <summary>Creates a new BigInteger from the sequence of bytes and adds a padding zero byte if needed.</summary>
    public static System.Numerics.BigInteger ToBigInteger(this System.Collections.Generic.IEnumerable<byte> source, int offset, int count)
      => new System.Numerics.BigInteger(source.Skip(offset).Take(count).ToArray() is var ba && (ba[ba.Length - 1] & 0x80) != 0 ? ba.Append((byte)0).ToArray() : ba);
  }
}
