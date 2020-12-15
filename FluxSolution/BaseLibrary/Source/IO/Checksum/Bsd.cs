namespace Flux.IO.Checksum
{
  /// <summary></summary>
  /// <see cref="https://en.wikipedia.org/wiki/BSD_checksum"/>
  public struct Bsd
    : IChecksum32, System.IEquatable<Bsd>
  {
    public static readonly Bsd Empty;
    public bool IsEmpty => Equals(Empty);

    private uint m_hash;

    public int Code { get => (int)m_hash; set => m_hash = (uint)value; }

    public Bsd(int hash = 0) => m_hash = unchecked((uint)hash);

    public int ComputeChecksum32(byte[] bytes, int startAt, int count)
    {
      if (bytes is null) throw new System.ArgumentNullException(nameof(bytes));

      unchecked
      {
        for (int index = startAt, maxIndex = startAt + count; index < maxIndex; index++)
        {
          m_hash = ((m_hash >> 1) + ((m_hash & 1) << 15) + bytes[index]) & 0xFFFF;
        }
      }

      return Code;
    }

    // Operators
    public static bool operator ==(Bsd a, Bsd b)
      => a.Equals(b);
    public static bool operator !=(Bsd a, Bsd b)
      => !a.Equals(b);

    // IEquatable
    public bool Equals([System.Diagnostics.CodeAnalysis.AllowNull] Bsd other)
      => m_hash == other.m_hash;

    // Object (overrides)
    public override bool Equals(object? obj)
      => obj is Bsd o && Equals(o);
    public override int GetHashCode()
      => m_hash.GetHashCode();
    public override string ToString()
      => $"<{nameof(Bsd)}: {m_hash}>";
  }
}