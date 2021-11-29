namespace Flux.Checksum
{
  /// <summary></summary>
  /// <see cref="https://en.wikipedia.org/wiki/BSD_checksum"/>
#if NET5_0
  public struct Bsd
    : IChecksumGenerator32, System.IEquatable<Bsd>
#else
  public struct Bsd
    : IChecksumGenerator32
#endif
  {
    public static readonly Bsd Empty;

    private uint m_hash;

    public int Checksum32 { get => unchecked((int)m_hash); set => m_hash = unchecked((uint)value); }

    public Bsd(int hash = 0) => m_hash = unchecked((uint)hash);

    public int GenerateChecksum32(byte[] bytes, int startAt, int count)
    {
      if (bytes is null) throw new System.ArgumentNullException(nameof(bytes));

      unchecked
      {
        for (int index = startAt, maxIndex = startAt + count; index < maxIndex; index++)
        {
          m_hash = ((m_hash >> 1) + ((m_hash & 1) << 15) + bytes[index]) & 0xFFFF;
        }
      }

      return Checksum32;
    }

    // Operators
#if NET5_0
    public static bool operator ==(Bsd a, Bsd b)
      => a.Equals(b);
    public static bool operator !=(Bsd a, Bsd b)
      => !a.Equals(b);
#endif

#if NET5_0
    // IEquatable
    public bool Equals([System.Diagnostics.CodeAnalysis.AllowNull] Bsd other)
      => m_hash == other.m_hash;
#endif

    // Object (overrides)
#if NET5_0
    public override bool Equals(object? obj)
      => obj is Bsd o && Equals(o);
    public override int GetHashCode()
      => m_hash.GetHashCode();
#endif
    public override string ToString()
      => $"{nameof(Bsd)} {{ CheckSum = {m_hash} }}";
  }
}