namespace Flux
{
  namespace Units
  {
    /// <summary>Unit interval, unit of rational number between 0 and 1, constrained by the <see cref="IntervalNotation"/>.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Unit_interval"/>
    public readonly record struct UnitInterval
    : System.IComparable, System.IComparable<UnitInterval>, System.IFormattable, IValueQuantifiable<double>
    {
      public const double MaxValue = 1;
      public const double MinValue = 0;

      private readonly IntervalNotation m_notation;

      private readonly double m_value;

      public UnitInterval(double unitInterval, IntervalNotation notation)
      {
        m_value = notation.AssertMember(unitInterval, MinValue, MaxValue, nameof(unitInterval));

        m_notation = notation;
      }
      public UnitInterval(double unitInterval) : this(unitInterval, IntervalNotation.Closed) { }

      /// <summary>
      /// <para>Determines which <see cref="IntervalNotation"/> this instance of <see cref="UnitInterval"/> is constrained by.</para>
      /// </summary>
      public IntervalNotation Notation => m_notation;

      #region Static methods

      /// <summary>
      /// <para>Asserts that the <paramref name="value"/> is a member of the unit interval constrained by <paramref name="notation"/>. If not, it throws an exception with the <paramref name="paramName"/>.</para>
      /// </summary>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public static TSelf AssertMember<TSelf>(TSelf value, IntervalNotation notation, string? paramName = null)
        where TSelf : System.Numerics.IFloatingPoint<TSelf>
        => notation.AssertMember(value, TSelf.CreateChecked(MinValue), TSelf.CreateChecked(MaxValue), paramName);

      /// <summary>
      /// <para>Returns whether the <paramref name="value"/> is a member of the unit interval constrained by <paramref name="notation"/>.</para>
      /// </summary>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public static bool VerifyMember<TSelf>(TSelf value, IntervalNotation notation)
        where TSelf : System.Numerics.IFloatingPoint<TSelf>
        => notation.VerifyMember(value, TSelf.CreateChecked(MinValue), TSelf.CreateChecked(MaxValue));

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
