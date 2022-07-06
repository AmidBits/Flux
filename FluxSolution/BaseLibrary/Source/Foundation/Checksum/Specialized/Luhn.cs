using System.Linq;

namespace Flux.Checksum.Special
{
  /// <summary>Luhn is a specific purpose checksum algorithm.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Luhn_algorithm"/>
  public struct Luhn
    : System.IEquatable<Luhn>
  {
    public static readonly Luhn Empty;

    private readonly int[] m_sequence;

    private uint m_checkDigit;

    public int CheckDigit { get => (int)m_checkDigit; set => m_checkDigit = (uint)value; }

    public Luhn(System.Collections.Generic.IEnumerable<int> numberSequence)
    {
      m_sequence = numberSequence.Select(digit => (digit >= 0 && digit <= 9) ? digit : throw new System.ArgumentOutOfRangeException(nameof(numberSequence), $"The number {digit} is not a single digit (0-9) number.")).ToArray();

      var sum = 0;

      var numbers = m_sequence.AsEnumerable().Reverse().ToArray();

      for (var index = 0; index < numbers.Length; index++)
      {
        var digit = numbers[index];

        sum += (index & 1) == 0 ? (digit > 4 ? digit * 2 - 9 : digit * 2) : digit;
      }

      m_checkDigit = (uint)(sum * 9 % 10);
    }
    public Luhn(System.Collections.Generic.IEnumerable<char> numberSequence)
      : this(numberSequence.Select(c => c - '0')) { }

    public static bool Verify(System.Collections.Generic.IEnumerable<int> numberSequence)
      => new Luhn(numberSequence.SkipLast(1)).CheckDigit == numberSequence.Last();
    public static bool Verify(System.Collections.Generic.IEnumerable<char> numberSequence)
      => Verify(numberSequence.Select(c => c - '0'));

    // Operators
    public static bool operator ==(Luhn a, Luhn b)
       => a.Equals(b);
    public static bool operator !=(Luhn a, Luhn b)
      => !a.Equals(b);

    // IEquatable
    public bool Equals([System.Diagnostics.CodeAnalysis.AllowNull] Luhn other)
      => m_checkDigit == other.m_checkDigit;

    // Object (overrides)
    public override bool Equals(object? obj)
      => obj is Luhn o && Equals(o);
    public override int GetHashCode()
    {
      var hc = new System.HashCode();
      for (var index = m_sequence.Length - 1; index >= 0; index--)
        hc.Add(m_sequence[index]);
      hc.Add(m_checkDigit);
      return hc.ToHashCode();
    }
    public override string ToString()
      => $"{GetType().Name} {{ {string.Concat(m_sequence.Select(i => (char)(i + '0')))}{m_checkDigit} }}";
  }
}