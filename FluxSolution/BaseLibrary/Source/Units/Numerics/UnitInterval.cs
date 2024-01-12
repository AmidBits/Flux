namespace Flux
{
  namespace Units
  {
    /// <summary>Unit interval, unit of rational number between 0 and 1 (closed interval, i.e. includes both endpoints).</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Unit_interval"/>
    public readonly record struct UnitInterval
    : System.IComparable, System.IComparable<UnitInterval>, System.IFormattable, IValueQuantifiable<double>
    {
      private readonly IntervalNotation m_intervalNotation;
      private readonly double m_value;

      public UnitInterval(double unitInterval, IntervalNotation constraint)
      {
        m_value = AssertMember(unitInterval, constraint);
        m_intervalNotation = constraint;
      }
      public UnitInterval(double unitInterval) : this(unitInterval, IntervalNotation.Closed) { }

      #region Static methods

      /// <summary>Asserts that the value is a member of the unit interval (throws an exception if not).</summary>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public static double AssertMember(double value, IntervalNotation constraint, string? paramName = null) => constraint.AssertMember(value, 0, 1, paramName);

      /// <summary>Returns whether the value is a member of the unit interval.</summary>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public static bool IsMember(double value, IntervalNotation constraint) => constraint.IsMember(value, 0, 1);

      #endregion Static methods

      #region Overloaded operators
      public static explicit operator double(UnitInterval v) => v.m_value;
      public static explicit operator UnitInterval(double v) => new(v);

      public static bool operator <(UnitInterval a, UnitInterval b) => a.CompareTo(b) < 0;
      public static bool operator <=(UnitInterval a, UnitInterval b) => a.CompareTo(b) <= 0;
      public static bool operator >(UnitInterval a, UnitInterval b) => a.CompareTo(b) > 0;
      public static bool operator >=(UnitInterval a, UnitInterval b) => a.CompareTo(b) >= 0;

      public static UnitInterval operator -(UnitInterval v) => new(-v.m_value);
      public static UnitInterval operator +(UnitInterval a, double b) => new(a.m_value + b);
      public static UnitInterval operator +(UnitInterval a, UnitInterval b) => a + b.m_value;
      public static UnitInterval operator /(UnitInterval a, double b) => new(a.m_value / b);
      public static UnitInterval operator /(UnitInterval a, UnitInterval b) => a / b.m_value;
      public static UnitInterval operator *(UnitInterval a, double b) => new(a.m_value * b);
      public static UnitInterval operator *(UnitInterval a, UnitInterval b) => a * b.m_value;
      public static UnitInterval operator %(UnitInterval a, double b) => new(a.m_value % b);
      public static UnitInterval operator %(UnitInterval a, UnitInterval b) => a % b.m_value;
      public static UnitInterval operator -(UnitInterval a, double b) => new(a.m_value - b);
      public static UnitInterval operator -(UnitInterval a, UnitInterval b) => a - b.m_value;
      #endregion Overloaded operators

      #region Implemented interfaces

      // IComparable
      public int CompareTo(object? other) => other is not null && other is UnitInterval o ? CompareTo(o) : -1;

      // IComparable<>
      public int CompareTo(UnitInterval other) => m_value.CompareTo(other.m_value);

      // IFormattable
      public string ToString(string? format, IFormatProvider? formatProvider) => m_value.ToString(format, formatProvider);

      // IQuantifiable<>
      public string ToValueString(string? format = null, bool preferUnicode = false, bool useFullName = false, System.Globalization.CultureInfo? culture = null)
        => string.Format(culture, $"{{0{(format is null ? string.Empty : $":{format}")}}}", m_value);

      public double Value => m_value;

      #endregion Implemented interfaces

      public override string ToString() => ToValueString();
    }
  }
}
