namespace Flux.Checksum
{
  /// <summary></summary>
  /// <see cref="https://en.wikipedia.org/wiki/Fletcher%27s_checksum"/>
  public struct Fletcher32
    : IChecksumGenerator32, System.IEquatable<Fletcher32>
  {
    public static readonly Fletcher32 Empty;

    private uint m_hash;

    public int Checksum32 { get => unchecked((int)m_hash); set => m_hash = unchecked((uint)value); }

    public Fletcher32(int hash = 0) => m_hash = unchecked((uint)hash);

    public int GenerateChecksum32(byte[] bytes, int startAt, int count)
    {
      if (bytes is null) throw new System.ArgumentNullException(nameof(bytes));

      uint sum1 = m_hash & 0xFFFF, sum2 = m_hash >> 16;

      unchecked
      {
        for (int index = startAt, maxIndex = startAt + count; index < maxIndex; index++)
        {
          sum1 = (sum1 + bytes[index]) % 65535;
          sum2 = (sum2 + sum1) % 65535;
        }

        m_hash = (sum2 << 16) | sum1;

        return Checksum32;
      }
    }

    // Operators
    public static bool operator ==(Fletcher32 a, Fletcher32 b)
      => a.Equals(b);
    public static bool operator !=(Fletcher32 a, Fletcher32 b)
      => !a.Equals(b);

    // IEquatable
    public bool Equals([System.Diagnostics.CodeAnalysis.AllowNull] Fletcher32 other)
      => m_hash == other.m_hash;

    // Object (overrides)
    public override bool Equals(object? obj)
      => obj is Fletcher32 o && Equals(o);
    public override int GetHashCode()
      => m_hash.GetHashCode();
    public override string ToString()
       => $"<{nameof(Fletcher32)}: {m_hash}>";
  }
}