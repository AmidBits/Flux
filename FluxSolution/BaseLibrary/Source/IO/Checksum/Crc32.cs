using System.Linq;

namespace Flux.Checksum
{
  /// <summary></summary>
  /// <see cref="https://en.wikipedia.org/wiki/Cyclic_redundancy_check"/>
  public record struct Crc32
    : IChecksumGenerator32
  {
    public static readonly Crc32 Empty;

    private readonly uint[] m_lookupTable;

    private uint m_hash;

    public int Checksum32 { get => unchecked((int)(m_hash ^ 0xFFFFFFFF)); set => m_hash = unchecked((uint)value ^ 0xFFFFFFFF); }

    public Crc32(int hash = unchecked((int)0xFFFFFFFF))
    {
      m_hash = unchecked((uint)hash);

      m_lookupTable = NumberSequences.VanEckSequence.GetVanEckSequence(256).Take(256).Select((e, i) => (e == 0 ? i : e) * 256 * i).Select(bi => (uint)bi).ToArray();
    }
    public Crc32(int[] lookupTable)
    {
      m_hash = 0xFFFFFFFF;

      if (lookupTable is null) throw new System.ArgumentNullException(nameof(lookupTable));

      if (lookupTable.Length != 256) throw new System.ArgumentOutOfRangeException(nameof(lookupTable), @"The lookup table must contain 256 values");

      m_lookupTable = lookupTable.Select(i => (uint)i).ToArray();
    }

    public int GenerateChecksum32(byte[] bytes, int offset, int count)
    {
      if (bytes is null) throw new System.ArgumentNullException(nameof(bytes));

      unchecked
      {
        for (int index = offset, maxIndex = offset + count; index < maxIndex; index++)
        {
          var lookupIndex = (m_hash ^ bytes[index]) & 0xFF;

          m_hash = (m_hash >> 8) ^ m_lookupTable[lookupIndex];
        }
      }

      return Checksum32;
    }

    #region Object overrides.
    public override string ToString() => $"{nameof(Crc32)} {{ Checksum = {m_hash} }}";
    #endregion Object overrides.
  }
}