namespace Flux.Units
{
  /// <summary>
  /// <para>Unit interval is in the range <see cref="UnitInterval.MinValue"/> = 0.0 and <see cref="UnitInterval.MaxValue"/> = 1.0 and is constrained by the <see cref="IntervalNotation"/>.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Unit_interval"/></para>
  /// </summary>
  public readonly record struct UnitInterval
    : System.IComparable, System.IComparable<UnitInterval>, System.IFormattable, IValueQuantifiable<double>
  {
    public const double MaxValue = 1;
    public const double MinValue = 0;

    private readonly IntervalNotation m_notation;

    private readonly double m_value;

    public UnitInterval(double unitInterval, IntervalNotation intervalNotation)
    {
      m_value = Interval<double>.AssertMember(unitInterval, MinValue, MaxValue, intervalNotation, nameof(unitInterval));

      m_notation = intervalNotation;
    }
    public UnitInterval(double unitInterval) : this(unitInterval, IntervalNotation.Closed) { }

    /// <summary>
    /// <para>Determines which <see cref="IntervalNotation"/> this instance of <see cref="UnitInterval"/> is constrained by.</para>
    /// </summary>
    public IntervalNotation Notation => m_notation;

    #region Static methods

    /// <summary>
    /// <para>Asserts that the <paramref name="unitInterval"/> is within <see cref="UnitInterval"/> constrained by the specified <paramref name="intervalNotation"/>. If not, it throws an exception.</para>
    /// </summary>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static TSelf AssertMember<TSelf>(TSelf unitInterval, IntervalNotation intervalNotation, string? paramName = null)
      where TSelf : System.Numerics.INumber<TSelf>
      => Interval<TSelf>.AssertMember(unitInterval, TSelf.CreateChecked(MinValue), TSelf.CreateChecked(MaxValue), intervalNotation, paramName ?? nameof(unitInterval));

    /// <summary>
    /// <para>Returns whether the <paramref name="unitInterval"/> is within <see cref="UnitInterval"/> constrained by the specified <paramref name="intervalNotation"/>.</para>
    /// </summary>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static bool IsMember<TSelf>(TSelf unitInterval, IntervalNotation intervalNotation)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => Interval<TSelf>.IsMember(unitInterval, TSelf.CreateChecked(MinValue), TSelf.CreateChecked(MaxValue), intervalNotation);

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
    public string ToString(string? format, System.IFormatProvider? formatProvider) => m_value.ToString(format, formatProvider);

    #region IValueQuantifiable<>

    /// <summary>
    /// <para>The <see cref="UnitInterval.Value"/> property is a value of the unit interval, between <see cref="MinValue"/> and <see cref="MaxValue"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IValueQuantifiable<>

    #endregion //Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
