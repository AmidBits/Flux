namespace Flux.Hashing
{
  /// <summary></summary>
  /// <see cref="https://en.wikipedia.org/wiki/Fowler�Noll�Vo_hash_function"/>
  public struct Fnv1a
    : ISimpleHashGenerator32, System.IEquatable<Fnv1a>
  {
    public static readonly Fnv1a Empty;

    private uint m_hash; // = 2166136261U;

    public int SimpleHash32 { get => unchecked((int)m_hash); set => m_hash = unchecked((uint)value); }

    private uint m_primeMultiplier; // = 16777619U;
    public int Prime { get => (int)m_primeMultiplier; set => m_primeMultiplier = (uint)value; }

    public Fnv1a(int hash = unchecked((int)2166136261U), int primeMultiplier = unchecked((int)16777619U))
    {
      m_hash = unchecked((uint)hash);

      m_primeMultiplier = unchecked((uint)primeMultiplier);
    }

    public int GenerateSimpleHash32(byte[] bytes, int startAt, int count)
    {
      if (bytes is null) throw new System.ArgumentNullException(nameof(bytes));

      unchecked
      {
        for (int index = startAt, maxIndex = startAt + count; index < maxIndex; index++)
        {
          m_hash ^= bytes[index];
          m_hash *= m_primeMultiplier;
        }
      }

      return SimpleHash32;
    }

    // Operators
    
    public static bool operator ==(Fnv1a a, Fnv1a b)
      => a.Equals(b);
    
    public static bool operator !=(Fnv1a a, Fnv1a b)
      => !a.Equals(b);

    // IEquatable
    
    public bool Equals([System.Diagnostics.CodeAnalysis.AllowNull] Fnv1a other)
      => m_hash == other.m_hash && m_primeMultiplier == other.m_primeMultiplier;

    // Object (overrides)
    
    public override bool Equals(object? obj)
      => obj is Fnv1a o && Equals(o);
    
    public override int GetHashCode()
      => m_hash.GetHashCode();
    
    public override string ToString()
      => $"{nameof(Fnv1a)} {{ HashCode = {m_hash} }}";
  }
}