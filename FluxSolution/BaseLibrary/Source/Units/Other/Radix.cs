namespace Flux
{
  namespace Units
  {
    /// <summary>Radix, unit of natural number.</summary>
    /// <seealso cref="https://en.wikipedia.org/wiki/Radix"/>
    public readonly partial record struct Radix
    : System.IComparable<Radix>, IQuantifiable<int>
    {
      private readonly int m_value;

      public Radix(int radix) => m_value = AssertRadix(radix);

      #region Static methods

      /// <summary>Asserts the number is a valid <paramref name="radix"/> (throws an exception with an optional <paramref name="paramName"/>, if not).</summary>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public static int AssertRadix(int radix, string? paramName = null)
        => IsRadix(radix) ? radix : throw new System.ArgumentOutOfRangeException(paramName ?? nameof(radix), $"Must be an integer, greater than or equal to 2.");

      /// <summary>Asserts the number is a valid <paramref name="radix"/>, with an <paramref name="upperLimit"/> (throws an exception with an optional <paramref name="paramName"/>, if not).</summary>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public static int AssertRadix(int radix, int upperLimit, string? paramName = null)
        => IsRadix(radix, upperLimit) ? radix : throw new System.ArgumentOutOfRangeException(paramName ?? nameof(radix), $"Must be an integer, greater than or equal to 2 and less than or equal to {upperLimit}.");

      /// <summary>Returns whether the number is a valid <paramref name="radix"/>.</summary>
      public static bool IsRadix(int radix) => radix > 1;

      /// <summary>Returns whether the number is a valid <paramref name="radix"/>, with an <paramref name="upperLimit"/>.</summary>
      public static bool IsRadix(int radix, int upperLimit) => upperLimit > 2 && radix > 1 && radix <= upperLimit;

      #endregion Static methods

      #region Overloaded operators
      public static explicit operator int(Radix v) => v.m_value;
      public static explicit operator Radix(int v) => new(v);

      public static bool operator <(Radix a, Radix b) => a.CompareTo(b) < 0;
      public static bool operator <=(Radix a, Radix b) => a.CompareTo(b) <= 0;
      public static bool operator >(Radix a, Radix b) => a.CompareTo(b) > 0;
      public static bool operator >=(Radix a, Radix b) => a.CompareTo(b) >= 0;

      public static Radix operator -(Radix v) => new(-v.m_value);
      public static Radix operator +(Radix a, int b) => new(a.m_value + b);
      public static Radix operator +(Radix a, Radix b) => a + b.m_value;
      public static Radix operator /(Radix a, int b) => new(a.m_value / b);
      public static Radix operator /(Radix a, Radix b) => a / b.m_value;
      public static Radix operator *(Radix a, int b) => new(a.m_value * b);
      public static Radix operator *(Radix a, Radix b) => a * b.m_value;
      public static Radix operator %(Radix a, int b) => new(a.m_value % b);
      public static Radix operator %(Radix a, Radix b) => a % b.m_value;
      public static Radix operator -(Radix a, int b) => new(a.m_value - b);
      public static Radix operator -(Radix a, Radix b) => a - b.m_value;
      #endregion Overloaded operators

      #region Implemented interfaces
      // IComparable<>
      public int CompareTo(Radix other) => m_value.CompareTo(other.m_value);
      // IComparable
      public int CompareTo(object? other) => other is not null && other is Radix o ? CompareTo(o) : -1;

      // IQuantifiable<>
      public string ToQuantityString(string? format, bool preferUnicode = false, bool useFullName = false) => $"{m_value}";
      public int Value => m_value;
      #endregion Implemented interfaces

      /// <summary>Creates a string containing the scientific pitch notation of the specified MIDI note.</summary>
      /// <see cref="https://en.wikipedia.org/wiki/Scientific_pitch_notation#Table_of_note_frequencies"/>
      public override string ToString() => ToQuantityString(null, false, false);
    }
  }
}
