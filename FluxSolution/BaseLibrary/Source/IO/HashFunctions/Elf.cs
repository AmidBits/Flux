namespace Flux.Hashing
{
  /// <summary></summary>
  /// <see cref="https://en.wikipedia.org/wiki/PJW_hash_function"/>
  public record struct Elf
    : ISimpleHashGenerator32
  {
    public static readonly Elf Empty;

    private uint m_hash;

    public int SimpleHash32 { get => unchecked((int)m_hash); set => m_hash = unchecked((uint)value); }

    public Elf(int hash = 0) => m_hash = unchecked((uint)hash);

    public int GenerateSimpleHash32(byte[] bytes, int offset, int count)
    {
      if (bytes is null) throw new System.ArgumentNullException(nameof(bytes));

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
    public override string ToString() => $"{nameof(Elf)} {{ HashCode = {m_hash} }}";
    #endregion Object overrides.
  }
}