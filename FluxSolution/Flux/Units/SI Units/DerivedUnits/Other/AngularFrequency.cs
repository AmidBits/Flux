namespace Flux.Units
{
  /// <summary>Angular frequency (a.k.a. angular speed, angular rate), unit of radians per second, is the magnitude of the pseudovector quantity angular velocity. This is an SI derived quantity.</summary>
  /// <see href="https://en.wikipedia.org/wiki/Angular_frequency"/>
  public readonly record struct AngularFrequency
    : System.IComparable, System.IComparable<AngularFrequency>, System.IFormattable, ISiUnitValueQuantifiable<double, AngularFrequencyUnit>
  {
    private readonly double m_value;

    public AngularFrequency(double value, AngularFrequencyUnit unit = AngularFrequencyUnit.RadianPerSecond) => m_value = ConvertFromUnit(unit, value);

    public AngularFrequency(MetricPrefix prefix, double radianPerSecond) => m_value = prefix.ConvertTo(radianPerSecond, MetricPrefix.Unprefixed);

    /// <summary>Creates a new <see cref="AngularFrequency"/> instance from <see cref="Speed">tangential/linear speed</see> and <see cref="Length">radius</see></summary>
    public AngularFrequency(Speed tangentialSpeed, Length radius) : this(tangentialSpeed.Value / radius.Value) { }

    public Frequency ToFrequency() => new(m_value / double.Tau);

    #region Static methods

    /// <summary>
    /// <para></para>
    /// <see href="https://en.wikipedia.org/wiki/Revolutions_per_minute"/>
    /// </summary>
    /// <param name="radianPerSecond"></param>
    /// <returns></returns>
    public static double ConvertAngularVelocityToRpm(double radianPerSecond) => radianPerSecond / double.Tau;

    /// <summary>
    /// <para></para>
    /// <see href="https://en.wikipedia.org/wiki/Revolutions_per_minute"/>
    /// </summary>
    /// <param name="revolutionPerMinute"></param>
    /// <returns></returns>
    public static double ConvertRpmToAngularVelocity(double revolutionPerMinute) => revolutionPerMinute / 60;

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

    public static string GetSiUnitName(MetricPrefix prefix, bool preferPlural) => prefix.GetMetricPrefixName() + GetUnitName(AngularFrequencyUnit.RadianPerSecond, preferPlural);

    public static string GetSiUnitSymbol(MetricPrefix prefix, bool preferUnicode) => prefix.GetMetricPrefixSymbol(preferUnicode) + GetUnitSymbol(AngularFrequencyUnit.RadianPerSecond, preferUnicode);

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertTo(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, bool fullName = false)
      => GetSiUnitValue(prefix).ToSiFormattedString() + Unicode.UnicodeSpacing.ThinSpace.ToSpacingString() + (fullName ? GetSiUnitName(prefix, GetSiUnitValue(prefix).IsConsideredPlural()) : GetSiUnitSymbol(prefix, false));

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    public static double ConvertFromUnit(AngularFrequencyUnit unit, double value)
      => unit switch
      {
        AngularFrequencyUnit.RadianPerSecond => value,

        _ => GetUnitFactor(unit) * value,
      };

    public static double ConvertToUnit(AngularFrequencyUnit unit, double value)
      => unit switch
      {
        AngularFrequencyUnit.RadianPerSecond => value,

        _ => value / GetUnitFactor(unit),
      };

    public static double ConvertUnit(double value, AngularFrequencyUnit from, AngularFrequencyUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public static double GetUnitFactor(AngularFrequencyUnit unit)
      => unit switch
      {
        AngularFrequencyUnit.RadianPerSecond => 1,

        _ => throw new System.NotImplementedException()
      };

    public static string GetUnitName(AngularFrequencyUnit unit, bool preferPlural) => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(AngularFrequencyUnit unit, bool preferUnicode)
      => unit switch
      {
        AngularFrequencyUnit.RadianPerSecond => preferUnicode ? "\u33AE" : "rad/s",

        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(AngularFrequencyUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(AngularFrequencyUnit unit = AngularFrequencyUnit.RadianPerSecond, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + Unicode.UnicodeSpacing.Space.ToSpacingString() + (fullName ? GetUnitName(unit, GetUnitValue(unit).IsConsideredPlural()) : GetUnitSymbol(unit, false));

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
