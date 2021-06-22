using System.Linq;

namespace Flux.Checksum
{
  /// <summary>Luhn is a specific purpose checksum algorithm.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Luhn_algorithm"/>
  public struct Nmea
    : System.IEquatable<Nmea>
  {
    public static readonly Nmea Empty;
    public bool IsEmpty => Equals(Empty);

    private readonly int[] m_sequence;

    private uint m_checkDigit;

    public int CheckDigit { get => (int)m_checkDigit; set => m_checkDigit = (uint)value; }

    public Nmea(System.Collections.Generic.IEnumerable<int> numberSequence)
    {
      m_sequence = numberSequence.ToArray();

      m_checkDigit = 0;

      foreach (var n in numberSequence)
        m_checkDigit ^= unchecked((uint)n);

      var s = m_checkDigit.ToString("x2");
    }
    public Nmea(System.Collections.Generic.IEnumerable<char> characterSequence)
      : this(characterSequence.Select(c => (int)c))
    { }

    public static bool Verify(System.Collections.Generic.IEnumerable<int> numberSequence)
      => new Nmea(numberSequence.SkipLast(1)).CheckDigit == numberSequence.Last();
    public static bool Verify(System.Collections.Generic.IEnumerable<char> numberSequence)
      => Verify(numberSequence.Select(c => c - '0'));

    // Operators
    public static bool operator ==(Nmea a, Nmea b)
      => a.Equals(b);
    public static bool operator !=(Nmea a, Nmea b)
      => !a.Equals(b);

    // IEquatable
    public bool Equals([System.Diagnostics.CodeAnalysis.AllowNull] Nmea other)
      => m_checkDigit == other.m_checkDigit;

    // Object (overrides)
    public override bool Equals(object? obj)
      => obj is Nmea o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(m_sequence.CombineHashCore(), m_checkDigit);
    public override string ToString()
      => $"<{nameof(Nmea)}: {string.Concat(m_sequence.Select(i => (char)(i + '0')))}{m_checkDigit}>";
  }
}