using System.Linq;

namespace Flux.Checksum
{
  /// <summary></summary>
  /// <see cref="https://en.wikipedia.org/wiki/Cyclic_redundancy_check"/>
  public struct Crc32
    : IChecksumGenerator32, System.IEquatable<Crc32>
  {
    public static readonly Crc32 Empty;

    private readonly uint[] m_lookupTable;

    private uint m_hash;

    public int Checksum32 { get => unchecked((int)(m_hash ^ 0xFFFFFFFF)); set => m_hash = unchecked((uint)value ^ 0xFFFFFFFF); }

    public Crc32(int hash = unchecked((int)0xFFFFFFFF))
    {
      m_hash = unchecked((uint)hash);

      m_lookupTable = new Numerics.VanEckSequence(256).Take(256).Select((e, i) => (e == 0 ? i : e) * 256 * i).Select(bi => (uint)bi).ToArray();
    }
    public Crc32(int[] lookupTable)
    {
      m_hash = 0xFFFFFFFF;

      if (lookupTable is null) throw new System.ArgumentNullException(nameof(lookupTable));

      if (lookupTable.Length != 256) throw new System.ArgumentOutOfRangeException(nameof(lookupTable), @"The lookup table must contain 256 values");

      m_lookupTable = lookupTable.Select(i => (uint)i).ToArray();
    }

    public int GenerateChecksum32(byte[] bytes, int startAt, int count)
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

      return Checksum32;
    }

    // Operators
    public static bool operator ==(Crc32 a, Crc32 b)
      => a.Equals(b);
    public static bool operator !=(Crc32 a, Crc32 b)
      => !a.Equals(b);

    // IEquatable
    public bool Equals([System.Diagnostics.CodeAnalysis.AllowNull] Crc32 other)
      => m_hash == other.m_hash;

    // Object (overrides)
    public override bool Equals(object? obj)
      => obj is Crc32 o && Equals(o);
    public override int GetHashCode()
      => m_hash.GetHashCode();
    public override string ToString()
       => $"<{nameof(Crc32)}: {m_hash}>";
  }
}