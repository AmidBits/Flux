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

      public Radix(int radix) => m_value = Assert(radix);

      #region Static methods

      /// <summary>Asserts the number is a valid <paramref name="radix"/> (throws an exception with an optional <paramref name="paramName"/>, if not).</summary>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public static int Assert(int radix, string? paramName = null)
        => Is(radix) ? radix : throw new System.ArgumentOutOfRangeException(paramName ?? nameof(radix), $"Must be an integer, greater than or equal to 2.");

      /// <summary>Asserts the number is a valid <paramref name="radix"/>, with an <paramref name="upperLimit"/> (throws an exception with an optional <paramref name="paramName"/>, if not).</summary>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public static int Assert(int radix, int upperLimit, string? paramName = null)
        => Is(radix, upperLimit) ? radix : throw new System.ArgumentOutOfRangeException(paramName ?? nameof(radix), $"Must be an integer, greater than or equal to 2 and less than or equal to {upperLimit}.");

      /// <summary>Returns whether the number is a valid <paramref name="radix"/>.</summary>
      public static bool Is(int radix) => radix > 1;

      /// <summary>Returns whether the number is a valid <paramref name="radix"/>, with an <paramref name="upperLimit"/>.</summary>
      public static bool Is(int radix, int upperLimit) => upperLimit > 2 && radix > 1 && radix <= upperLimit;

      #endregion Static methods

      #region Overloaded operators
      public static implicit operator Radix(System.Byte v) => new(v);
      public static implicit operator Radix(System.Int16 v) => new(v);
      public static implicit operator Radix(System.Int32 v) => new(v);
      public static implicit operator Radix(System.Int64 v) => new((int)v);
      public static implicit operator Radix(System.Int128 v) => new((int)v);
      public static implicit operator Radix(System.Numerics.BigInteger v) => new((int)v);

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
