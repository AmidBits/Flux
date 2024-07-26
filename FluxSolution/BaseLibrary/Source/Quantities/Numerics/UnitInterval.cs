namespace Flux
{
  namespace Quantities
  {
    /// <summary>
    /// <para>Unit interval, unit of rational number, with the interval 0.0 (<see cref="UnitInterval.MinValue"/>) and 1.0 (<see cref="UnitInterval.MaxValue"/>), constrained by the <see cref="IntervalNotation"/>.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Unit_interval"/></para>
    /// </summary>
    public readonly record struct UnitInterval
      : System.IComparable, System.IComparable<UnitInterval>, System.IFormattable, System.Numerics.IMinMaxValue<double>, IValueQuantifiable<double>
    {
      //public const double MaxValue = 1;
      //public const double MinValue = 0;

      private readonly IntervalNotation m_notation;

      private readonly double m_value;

      public UnitInterval(double unitInterval, IntervalNotation notation)
      {
        m_value = notation.AssertValidMember(unitInterval, MinValue, MaxValue, nameof(unitInterval));

        m_notation = notation;
      }
      public UnitInterval(double unitInterval) : this(unitInterval, IntervalNotation.Closed) { }

      /// <summary>
      /// <para>Determines which <see cref="IntervalNotation"/> this instance of <see cref="UnitInterval"/> is constrained by.</para>
      /// </summary>
      public IntervalNotation Notation => m_notation;

      #region Static methods

      /// <summary>
      /// <para>Asserts that the <paramref name="unitInterval"/> is a member of the unit interval constrained by <paramref name="notation"/>. If not, it throws an exception.</para>
      /// </summary>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public static TSelf AssertMember<TSelf>(TSelf unitInterval, IntervalNotation notation, string? paramName = null)
        where TSelf : System.Numerics.IFloatingPoint<TSelf>
        => notation.AssertValidMember(unitInterval, TSelf.CreateChecked(MinValue), TSelf.CreateChecked(MaxValue), paramName ?? nameof(unitInterval));

      /// <summary>
      /// <para>Returns whether the <paramref name="unitInterval"/> is a member of the unit interval constrained by <paramref name="notation"/>.</para>
      /// </summary>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public static bool VerifyMember<TSelf>(TSelf unitInterval, IntervalNotation notation)
        where TSelf : System.Numerics.IFloatingPoint<TSelf>
        => notation.IsValidMember(unitInterval, TSelf.CreateChecked(MinValue), TSelf.CreateChecked(MaxValue));

      #endregion Static methods

      #region Overloaded operators

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
      public string ToString(string? format, System.IFormatProvider? formatProvider)
        => string.Format(formatProvider, $"{{0{(format is null ? string.Empty : $":{format}")}}}", m_value);

      public readonly double MaxValue => 1;
      public readonly double MinValue => 0;

      // IQuantifiable<>
      /// <summary>
      /// <para>The <see cref="UnitInterval.Value"/> property is a value of the unit interval, between <see cref="MinValue"/> and <see cref="MaxValue"/>.</para>
      /// </summary>
      public double Value => m_value;

      #endregion Implemented interfaces

      public override string ToString() => ToString(null, null);
    }
  }
}
