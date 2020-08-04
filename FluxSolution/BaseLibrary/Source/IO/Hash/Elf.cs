namespace Flux.IO.Hash
{
  /// <summary></summary>
  /// <see cref="https://en.wikipedia.org/wiki/PJW_hash_function"/>
  public struct Elf
    : ISimpleHash32, System.IEquatable<Elf>, System.IFormattable
  {
    private uint m_hash;

    public int Code { get => (int)m_hash; set => m_hash = (uint)value; }

    public Elf(int hash = 0) => m_hash = unchecked((uint)hash);

    public int ComputeSimpleHash32(byte[] bytes, int startAt, int count)
    {
      if (bytes is null) throw new System.ArgumentNullException(nameof(bytes));

      unchecked
      {
        for (int index = startAt, maxIndex = startAt + count; index < maxIndex; index++)
        {
          m_hash = (m_hash << 4) + bytes[index];

          var highCode = m_hash & 0xF0000000;

          if (highCode != 0) m_hash ^= highCode >> 24;

          m_hash &= ~highCode;
        }
      }

      return Code;
    }

    // Operators
    public static bool operator ==(Elf a, Elf b)
      => a.Equals(b);
    public static bool operator !=(Elf a, Elf b)
      => !a.Equals(b);
    // IEquatable
    public bool Equals([System.Diagnostics.CodeAnalysis.AllowNull] Elf other)
      => m_hash == other.m_hash;
    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider)
      => $"<{m_hash}>";
    // Object (overrides)
    public override bool Equals(object? obj)
      => obj is Elf elf && Equals(elf);
    public override int GetHashCode()
      => m_hash.GetHashCode();
    public override string ToString()
      => ToString(null, null);
  }
}
