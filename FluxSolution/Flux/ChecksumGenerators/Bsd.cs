namespace Flux.ChecksumGenerators
{
  /// <summary></summary>
  /// <see href="https://en.wikipedia.org/wiki/BSD_checksum"/>
  public record struct Bsd
    : IChecksum32Generatable
  {
    private uint m_hash;

    public int Checksum32 { readonly get => unchecked((int)m_hash); set => m_hash = unchecked((uint)value); }

    public Bsd(int hash = 0) => m_hash = unchecked((uint)hash);

    public int GenerateChecksum32(byte[] bytes, int offset, int count)
    {
      System.ArgumentNullException.ThrowIfNull(bytes);

      unchecked
      {
        for (int index = offset, maxIndex = offset + count; index < maxIndex; index++)
        {
          m_hash = ((m_hash >> 1) + ((m_hash & 1) << 15) + bytes[index]) & 0xFFFF;
        }
      }

      return Checksum32;
    }

    #region Object overrides.
    public readonly override string ToString() => $"{nameof(Bsd)} {{ CheckSum = {m_hash} }}";
    #endregion Object overrides.
  }
}