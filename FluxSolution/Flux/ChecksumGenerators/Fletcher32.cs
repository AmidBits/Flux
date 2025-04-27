namespace Flux.ChecksumGenerators
{
  /// <summary></summary>
  /// <see href="https://en.wikipedia.org/wiki/Fletcher%27s_checksum"/>
  public record struct Fletcher32
    : IChecksum32Generatable
  {
    private uint m_hash;

    public int Checksum32 { readonly get => unchecked((int)m_hash); set => m_hash = unchecked((uint)value); }

    public Fletcher32(int hash = 0) => m_hash = unchecked((uint)hash);

    public int GenerateChecksum32(byte[] bytes, int offset, int count)
    {
      System.ArgumentNullException.ThrowIfNull(bytes);

      uint sum1 = m_hash & 0xFFFF, sum2 = m_hash >> 16;

      unchecked
      {
        for (int index = offset, maxIndex = offset + count; index < maxIndex; index++)
        {
          sum1 = (sum1 + bytes[index]) % 65535;
          sum2 = (sum2 + sum1) % 65535;
        }

        m_hash = (sum2 << 16) | sum1;

        return Checksum32;
      }
    }

    #region Object overrides.
    public readonly override string ToString() => $"{nameof(Fletcher32)} {{ CheckSum = {m_hash} }}";
    #endregion Object overrides.
  }
}