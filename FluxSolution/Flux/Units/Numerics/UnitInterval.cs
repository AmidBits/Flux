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

    private readonly double m_unitInterval;

    public UnitInterval(double unitInterval)
    {
      m_unitInterval = unitInterval is >= MinValue or <= MaxValue ? unitInterval : throw new System.ArgumentOutOfRangeException(nameof(unitInterval));
    }

    #region Static methods

    /// <summary>
    /// <para>Asserts that the <paramref name="unitInterval"/> is within <see cref="UnitInterval"/> constrained by the specified <paramref name="intervalNotation"/>. If not, it throws an exception.</para>
    /// </summary>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static TFloat AssertMember<TFloat>(TFloat unitInterval, IntervalNotation intervalNotation, string? paramName = null)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>
    {
      if (IsMember(unitInterval))
        return unitInterval;

      throw new System.ArgumentOutOfRangeException(nameof(unitInterval));
    }

    /// <summary>
    /// <para>Returns whether the <paramref name="unitInterval"/> is within <see cref="UnitInterval"/> constrained by the specified <paramref name="intervalNotation"/>.</para>
    /// </summary>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static bool IsMember<TFloat>(TFloat unitInterval)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>
      => unitInterval is >= MinValue or <= MaxValue;

    #endregion Static methods

    #region Overloaded operators

    public static explicit operator UnitInterval(double value) => new(value);
    public static implicit operator double(UnitInterval value) => value.m_unitInterval;

    public static bool operator <(UnitInterval a, UnitInterval b) => a.CompareTo(b) < 0;
    public static bool operator <=(UnitInterval a, UnitInterval b) => a.CompareTo(b) <= 0;
    public static bool operator >(UnitInterval a, UnitInterval b) => a.CompareTo(b) > 0;
    public static bool operator >=(UnitInterval a, UnitInterval b) => a.CompareTo(b) >= 0;

    public static UnitInterval operator -(UnitInterval v) => new(-v.m_unitInterval);
    public static UnitInterval operator +(UnitInterval a, double b) => new(a.m_unitInterval + b);
    public static UnitInterval operator +(UnitInterval a, UnitInterval b) => a + b.m_unitInterval;
    public static UnitInterval operator /(UnitInterval a, double b) => new(a.m_unitInterval / b);
    public static UnitInterval operator /(UnitInterval a, UnitInterval b) => a / b.m_unitInterval;
    public static UnitInterval operator *(UnitInterval a, double b) => new(a.m_unitInterval * b);
    public static UnitInterval operator *(UnitInterval a, UnitInterval b) => a * b.m_unitInterval;
    public static UnitInterval operator %(UnitInterval a, double b) => new(a.m_unitInterval % b);
    public static UnitInterval operator %(UnitInterval a, UnitInterval b) => a % b.m_unitInterval;
    public static UnitInterval operator -(UnitInterval a, double b) => new(a.m_unitInterval - b);
    public static UnitInterval operator -(UnitInterval a, UnitInterval b) => a - b.m_unitInterval;

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is UnitInterval o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(UnitInterval other) => m_unitInterval.CompareTo(other.m_unitInterval);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => m_unitInterval.ToString(format, formatProvider);

    #region IValueQuantifiable<>

    /// <summary>
    /// <para>The <see cref="UnitInterval.Value"/> property is a value of the unit interval, between <see cref="MinValue"/> and <see cref="MaxValue"/>.</para>
    /// </summary>
    public double Value => m_unitInterval;

    #endregion // IValueQuantifiable<>

    #endregion //Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
