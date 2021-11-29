namespace Flux.Hashing
{
  /// <summary></summary>
  /// <see cref="https://en.wikipedia.org/wiki/Jenkins_hash_function"/>
#if NET5_0
  public struct Oat
    : ISimpleHashGenerator32, System.IEquatable<Oat>
#else
  public struct Oat
    : ISimpleHashGenerator32
#endif
  {
    public static readonly Oat Empty;

    private uint m_hash;

    public int SimpleHash32 { get => unchecked((int)m_hash); set => m_hash = unchecked((uint)value); }

    public Oat(int hash = 0) => m_hash = unchecked((uint)hash);

    public int GenerateSimpleHash32(byte[] bytes, int startAt, int count)
    {
      if (bytes is null) throw new System.ArgumentNullException(nameof(bytes));

      unchecked
      {
        for (int index = startAt, maxIndex = startAt + count; index < maxIndex; index++)
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

    // Operators
#if NET5_0
    public static bool operator ==(Oat a, Oat b)
      => a.Equals(b);
    public static bool operator !=(Oat a, Oat b)
      => !a.Equals(b);
#endif

#if NET5_0
    // IEquatable
    public bool Equals([System.Diagnostics.CodeAnalysis.AllowNull] Oat other)
      => m_hash == other.m_hash;
#endif

    // Object (overrides)
#if NET5_0
    public override bool Equals(object? obj)
      => obj is Oat o && Equals(o);
    public override int GetHashCode()
      => m_hash.GetHashCode();
#endif
    public override string ToString()
      => $"{nameof(Oat)} {{ HashCode = {m_hash} }}";
  }
}
