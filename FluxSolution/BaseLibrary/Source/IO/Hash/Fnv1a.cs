namespace Flux.IO.Hash
{
  /// <summary></summary>
  /// <see cref="https://en.wikipedia.org/wiki/Fowler–Noll–Vo_hash_function"/>
  public class Fnv1a
    : ISimpleHash32, System.IEquatable<Fnv1a>, System.IFormattable
  {
    private uint m_hash; // = 2166136261U;
    public int Code { get => (int)m_hash; set => m_hash = (uint)value; }

    private uint m_primeMultiplier; // = 16777619U;
    public int Prime { get => (int)m_primeMultiplier; set => m_primeMultiplier = (uint)value; }

    public Fnv1a(int hash = unchecked((int)2166136261U), int primeMultiplier = unchecked((int)16777619U))
    {
      m_hash = unchecked((uint)hash);

      m_primeMultiplier = unchecked((uint)primeMultiplier);
    }

    public int ComputeSimpleHash32(byte[] bytes, int startAt, int count)
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

      return Code;
    }

    // Operators
    public static bool operator ==(Fnv1a a, Fnv1a b)
      => a.Equals(b);
    public static bool operator !=(Fnv1a a, Fnv1a b)
      => !a.Equals(b);
    // IEquatable
    public bool Equals([System.Diagnostics.CodeAnalysis.AllowNull] Fnv1a other)
      => !(other is null) && m_hash == other.m_hash && m_primeMultiplier == other.m_primeMultiplier;
    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider)
      => $"<{m_hash}>";
    // Object (overrides)
    public override bool Equals(object? obj)
      => obj is Fnv1a fnv1a && Equals(fnv1a);
    public override int GetHashCode()
      => m_hash.GetHashCode();
    public override string ToString()
      => ToString(null, null);
  }
}
