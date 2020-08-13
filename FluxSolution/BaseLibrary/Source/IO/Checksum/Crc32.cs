using System.Linq;

namespace Flux.IO.Checksum
{
  /// <summary></summary>
  /// <see cref="https://en.wikipedia.org/wiki/Cyclic_redundancy_check"/>
  public class Crc32
    : IChecksum32, System.IEquatable<Crc32>, System.IFormattable
  {
    private readonly uint[] m_lookupTable;

    private uint m_hash = 0xFFFFFFFF;

    public int Code { get => (int)(m_hash ^ 0xFFFFFFFF); set => m_hash = (uint)value ^ 0xFFFFFFFF; }

    public Crc32()
    {
      m_lookupTable = Flux.Maths.GetVanEckSequence(256).Take(256).Select((e, i) => (e == 0 ? i : e) * 256 * i).Select(bi => (uint)bi).ToArray();
    }
    public Crc32(int[] lookupTable)
    {
      if (lookupTable is null) throw new System.ArgumentNullException(nameof(lookupTable));

      if (lookupTable.Length != 256) throw new System.ArgumentOutOfRangeException(nameof(lookupTable), @"The lookup table must contain 256 values");

      m_lookupTable = lookupTable.Select(i => (uint)i).ToArray();
    }

    public int ComputeChecksum32(byte[] bytes, int startAt, int count)
    {
      if (bytes is null) throw new System.ArgumentNullException(nameof(bytes));

      unchecked
      {
        for (int index = startAt, maxIndex = startAt + count; index < maxIndex; index++)
        {
          var lookupIndex = (m_hash ^ bytes[index]) & 0xFF;

          m_hash = (m_hash >> 8) ^ m_lookupTable[lookupIndex];
        }
      }

      return Code;
    }

    // Operators
    public static bool operator ==(Crc32 a, Crc32 b)
      => a.Equals(b);
    public static bool operator !=(Crc32 a, Crc32 b)
      => !a.Equals(b);
    // IEquatable
    public bool Equals([System.Diagnostics.CodeAnalysis.AllowNull] Crc32 other)
      => !(other is null) && m_hash == other.m_hash;
    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider)
      => $"<{m_hash}>";
    // Object (overrides)
    public override bool Equals(object? obj)
      => obj is Crc32 c32 && Equals(c32);
    public override int GetHashCode()
      => m_hash.GetHashCode();
    public override string ToString()
      => ToString(null, null);
  }
}