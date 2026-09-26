namespace Flux.Units
{
  /// <summary>Angular frequency (a.k.a. angular speed, angular rate), unit of radians per second, is the magnitude of the pseudovector quantity angular velocity. This is an SI derived quantity.</summary>
  /// <see href="https://en.wikipedia.org/wiki/Angular_frequency"/>
  public readonly record struct AngularFrequency
    : System.IComparable, System.IComparable<AngularFrequency>, System.IFormattable, ISiUnitValueQuantifiable<double, AngularFrequencyUnit>
  {
    private readonly double m_value;

    public AngularFrequency(double value, AngularFrequencyUnit unit = AngularFrequencyUnit.RadianPerSecond) => m_value = ConvertFromUnit(unit, value);

    public AngularFrequency(MetricPrefix prefix, double radianPerSecond) => m_value = prefix.ConvertPrefix(radianPerSecond, MetricPrefix.Unprefixed);

    /// <summary>
    /// <para>Constructs an angular frequency from tangential speed and radius.</para>
    /// </summary>
    /// <param name="tangentialSpeed"></param>
    /// <param name="radius"></param>
    public AngularFrequency(Speed tangentialSpeed, Length radius) : this(tangentialSpeed.Value / radius.Value) { }

    /// <summary>
    /// <para>Returns the frequency corresponding to the angular frequency.</para>
    /// </summary>
    public Frequency Frequency => new(m_value / double.Tau);

    /// <summary>
    /// <para>Returns the number of revolutions per minute (RPM) corresponding to the angular frequency.</para>
    /// </summary>
    public double RevolutionsPerMinute
      => m_value / double.Tau * 60;

    #region Static methods

    /// <summary>
    /// <para>Constructs an angular frequency from revolutions per minute (RPM).</para>
    /// </summary>
    /// <param name="rpm"></param>
    /// <returns></returns>
    public static AngularFrequency FromRevolutionsPerMinute(double rpm)
      => new(rpm * double.Tau / 60);

    #endregion Static methods

    #region Overloaded operators

    public static bool operator <(AngularFrequency a, AngularFrequency b) => a.CompareTo(b) < 0;
    public static bool operator >(AngularFrequency a, AngularFrequency b) => a.CompareTo(b) > 0;
    public static bool operator <=(AngularFrequency a, AngularFrequency b) => a.CompareTo(b) <= 0;
    public static bool operator >=(AngularFrequency a, AngularFrequency b) => a.CompareTo(b) >= 0;

    public static AngularFrequency operator -(AngularFrequency v) => new(-v.m_value);
    public static AngularFrequency operator *(AngularFrequency a, AngularFrequency b) => new(a.m_value * b.m_value);
    public static AngularFrequency operator /(AngularFrequency a, AngularFrequency b) => new(a.m_value / b.m_value);
    public static AngularFrequency operator %(AngularFrequency a, AngularFrequency b) => new(a.m_value % b.m_value);
    public static AngularFrequency operator +(AngularFrequency a, AngularFrequency b) => new(a.m_value + b.m_value);
    public static AngularFrequency operator -(AngularFrequency a, AngularFrequency b) => new(a.m_value - b.m_value);
    public static AngularFrequency operator *(AngularFrequency a, double b) => new(a.m_value * b);
    public static AngularFrequency operator /(AngularFrequency a, double b) => new(a.m_value / b);
    public static AngularFrequency operator %(AngularFrequency a, double b) => new(a.m_value % b);
    public static AngularFrequency operator +(AngularFrequency a, double b) => new(a.m_value + b);
    public static AngularFrequency operator -(AngularFrequency a, double b) => new(a.m_value - b);

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is AngularFrequency o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(AngularFrequency other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToUnitString(AngularFrequencyUnit.RadianPerSecond, format, formatProvider);

    #region ISiUnitValueQuantifiable<>

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertPrefix(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, string? format = null, System.IFormatProvider? formatProvider = null)
      => GetSiUnitValue(prefix).ToSiFormattedString(format, formatProvider) + UnicodeSpacing.ThinSpace.ToSpacingString() + prefix.GetMetricPrefixSymbol() + AngularFrequencyUnit.RadianPerSecond.GetUnitSymbol();

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    public static double ConvertFromUnit(AngularFrequencyUnit unit, double value)
      => unit switch
      {
        AngularFrequencyUnit.RadianPerSecond => value,

        _ => unit.GetUnitFactor() * value,
      };

    public static double ConvertToUnit(AngularFrequencyUnit unit, double value)
      => unit switch
      {
        AngularFrequencyUnit.RadianPerSecond => value,

        _ => value / unit.GetUnitFactor(),
      };

    public static double ConvertUnit(double value, AngularFrequencyUnit from, AngularFrequencyUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public double GetUnitValue(AngularFrequencyUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(AngularFrequencyUnit unit = AngularFrequencyUnit.RadianPerSecond, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing spacing = UnicodeSpacing.Space, bool fullName = false)
    {
      var value = GetUnitValue(unit);

      return value.ToString(format, formatProvider) + spacing.ToSpacingString() + (fullName ? unit.GetUnitName(Number.IsConsideredPlural(value)) : unit.GetUnitSymbol(false));
    }

    #endregion // IUnitValueQuantifiable<>

    #region IValueQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="AngularFrequency.Value"/> property is in <see cref="AngularFrequencyUnit.RadianPerSecond"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IValueQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
