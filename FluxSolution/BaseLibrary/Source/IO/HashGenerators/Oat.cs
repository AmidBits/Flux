namespace Flux.IO.Hashing
{
  /// <summary></summary>
  /// <see href="https://en.wikipedia.org/wiki/Jenkins_hash_function"/>
  public record struct Oat
    : ISimpleHash32Generatable
  {
    private uint m_hash;

    public int SimpleHash32 { readonly get => unchecked((int)m_hash); set => m_hash = unchecked((uint)value); }

    public Oat(int hash = 0) => m_hash = unchecked((uint)hash);

    public int GenerateSimpleHash32(byte[] bytes, int offset, int count)
    {
      System.ArgumentNullException.ThrowIfNull(bytes);

      unchecked
      {
        for (int index = offset, maxIndex = offset + count; index < maxIndex; index++)
        {
          m_hash += bytes[index];
          m_hash += m_hash << 10;
          m_hash ^= m_hash >> 6;
        }

        m_hash += m_hash << 3;
        m_hash ^= m_hash >> 11;
        m_hash += m_hash << 15;
      }

      return SimpleHash32;
    }

    #region Object overrides.
    public override readonly string ToString() => $"{nameof(Oat)} {{ HashCode = {m_hash} }}";
    #endregion Object overrides.
  }
}
