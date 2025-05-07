namespace Flux.Units
{
  /// <summary>
  /// <para>Radioactivity, unit of becquerel.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Specific_activity"/></para>
  /// </summary>
  public readonly record struct Radioactivity
    : System.IComparable, System.IComparable<Radioactivity>, System.IFormattable, ISiUnitValueQuantifiable<double, RadioactivityUnit>
  {
    private readonly double m_value;

    public Radioactivity(double value, RadioactivityUnit unit = RadioactivityUnit.Becquerel) => m_value = ConvertFromUnit(unit, value);

    public Radioactivity(MetricPrefix prefix, double becquerel) => m_value = prefix.ChangePrefix(becquerel, MetricPrefix.Unprefixed);

    #region Overloaded operators

    public static bool operator <(Radioactivity a, Radioactivity b) => a.CompareTo(b) < 0;
    public static bool operator >(Radioactivity a, Radioactivity b) => a.CompareTo(b) > 0;
    public static bool operator <=(Radioactivity a, Radioactivity b) => a.CompareTo(b) <= 0;
    public static bool operator >=(Radioactivity a, Radioactivity b) => a.CompareTo(b) >= 0;

    public static Radioactivity operator -(Radioactivity v) => new(-v.m_value);
    public static Radioactivity operator *(Radioactivity a, Radioactivity b) => new(a.m_value * b.m_value);
    public static Radioactivity operator /(Radioactivity a, Radioactivity b) => new(a.m_value / b.m_value);
    public static Radioactivity operator %(Radioactivity a, Radioactivity b) => new(a.m_value % b.m_value);
    public static Radioactivity operator +(Radioactivity a, Radioactivity b) => new(a.m_value + b.m_value);
    public static Radioactivity operator -(Radioactivity a, Radioactivity b) => new(a.m_value - b.m_value);
    public static Radioactivity operator *(Radioactivity a, double b) => new(a.m_value * b);
    public static Radioactivity operator /(Radioactivity a, double b) => new(a.m_value / b);
    public static Radioactivity operator %(Radioactivity a, double b) => new(a.m_value % b);
    public static Radioactivity operator +(Radioactivity a, double b) => new(a.m_value + b);
    public static Radioactivity operator -(Radioactivity a, double b) => new(a.m_value - b);

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is Radioactivity o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(Radioactivity other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToSiUnitString(MetricPrefix.Unprefixed);

    #region ISiUnitValueQuantifiable<>

    public static string GetSiUnitName(MetricPrefix prefix, bool preferPlural) => prefix.GetMetricPrefixName() + GetUnitName(RadioactivityUnit.Becquerel, preferPlural);

    public static string GetSiUnitSymbol(MetricPrefix prefix, bool preferUnicode) => prefix.GetMetricPrefixSymbol(preferUnicode) + GetUnitSymbol(RadioactivityUnit.Becquerel, preferUnicode);

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ChangePrefix(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
    {
      var value = GetSiUnitValue(prefix);

      return value.ToSiFormattedString(format, formatProvider)
        + Unicode.UnicodeSpacing.ThinSpace.ToSpacingString()
        + (fullName ? GetSiUnitName(prefix, value.IsConsideredPlural()) : GetSiUnitSymbol(prefix, false));
    }

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    public static double ConvertFromUnit(RadioactivityUnit unit, double value)
      => unit switch
      {
        RadioactivityUnit.Becquerel => value,

        _ => GetUnitFactor(unit) * value,
      };

    public static double ConvertToUnit(RadioactivityUnit unit, double value)
      => unit switch
      {
        RadioactivityUnit.Becquerel => value,

        _ => value / GetUnitFactor(unit),
      };

    public static double ConvertUnit(double value, RadioactivityUnit from, RadioactivityUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public static double GetUnitFactor(RadioactivityUnit unit)
      => unit switch
      {
        RadioactivityUnit.Becquerel => 1,

        _ => throw new System.NotImplementedException()
      };

    public static string GetUnitName(RadioactivityUnit unit, bool preferPlural) => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(RadioactivityUnit unit, bool preferUnicode)
      => unit switch
      {
        RadioactivityUnit.Becquerel => preferUnicode ? "\u33C3" : "Bq",

        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(RadioactivityUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(RadioactivityUnit unit = RadioactivityUnit.Becquerel, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
    {
      var value = GetUnitValue(unit);

      return value.ToString(format, formatProvider)
        + Unicode.UnicodeSpacing.Space.ToSpacingString()
        + (fullName ? GetUnitName(unit, value.IsConsideredPlural()) : GetUnitSymbol(unit, false));
    }

    #endregion // IUnitValueQuantifiable<>

    #region IValueQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="Radioactivity.Value"/> property is in <see cref="RadioactivityUnit.Becquerel"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IValueQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
