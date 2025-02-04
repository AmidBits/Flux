namespace Flux.IO.HashGenerators
{
  /// <summary></summary>
  /// <see href="https://en.wikipedia.org/wiki/PJW_hash_function"/>
  public record struct Elf
    : ISimpleHash32Generatable
  {
    private uint m_hash;

    public int SimpleHash32 { readonly get => unchecked((int)m_hash); set => m_hash = unchecked((uint)value); }

    public Elf(int hash = 0) => m_hash = unchecked((uint)hash);

    public int GenerateSimpleHash32(byte[] bytes, int offset, int count)
    {
      System.ArgumentNullException.ThrowIfNull(bytes);

      unchecked
      {
        for (int index = offset, maxIndex = offset + count; index < maxIndex; index++)
        {
          m_hash = (m_hash << 4) + bytes[index];

          var highCode = m_hash & 0xF0000000;

          if (highCode != 0) m_hash ^= highCode >> 24;

          m_hash &= ~highCode;
        }
      }

      return SimpleHash32;
    }

    #region Object overrides.
    public readonly override string ToString() => $"{nameof(Elf)} {{ HashCode = {m_hash} }}";
    #endregion Object overrides.
  }
}
