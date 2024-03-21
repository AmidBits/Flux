namespace Flux.Hashing
{
  /// <summary></summary>
  /// <see href="https://en.wikipedia.org/wiki/Fowler-Noll-Vo_hash_function"/>
  public record struct Fnv1a
    : ISimpleHash32Generatable
  {
    [System.CLSCompliant(false)] public const uint DefaultHash = 2166136261U;
    [System.CLSCompliant(false)] public const uint DefaultPrimeMultiplier = 16777619U;

    private uint m_hash;

    public int SimpleHash32 { readonly get => unchecked((int)m_hash); set => m_hash = unchecked((uint)value); }

    private uint m_primeMultiplier;
    public int Prime { readonly get => (int)m_primeMultiplier; set => m_primeMultiplier = (uint)value; }

    [System.CLSCompliant(false)]
    public Fnv1a(uint hash, uint primeMultiplier)
    {
      m_hash = hash;
      m_primeMultiplier = primeMultiplier;
    }
    public Fnv1a(int hash, int primeMultiplier) : this(unchecked((uint)hash), unchecked((uint)primeMultiplier)) { }
    public Fnv1a() : this(DefaultHash, DefaultPrimeMultiplier) { }

    public int GenerateSimpleHash32(byte[] bytes, int offset, int count)
    {
      System.ArgumentNullException.ThrowIfNull(bytes);

      unchecked
      {
        for (int index = offset, maxIndex = offset + count; index < maxIndex; index++)
        {
          m_hash ^= bytes[index];
          m_hash *= m_primeMultiplier;
        }
      }

      return SimpleHash32;
    }

    #region Object overrides.
    public override readonly string ToString() => $"{nameof(Fnv1a)} {{ HashCode = {m_hash} }}";
    #endregion Object overrides.
  }
}
