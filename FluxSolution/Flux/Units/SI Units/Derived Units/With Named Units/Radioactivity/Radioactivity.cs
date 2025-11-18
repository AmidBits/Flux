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

    public Radioactivity(MetricPrefix prefix, double becquerel) => m_value = prefix.ConvertPrefix(becquerel, MetricPrefix.Unprefixed);

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

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertPrefix(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, string? format = null, System.IFormatProvider? formatProvider = null)
      => GetSiUnitValue(prefix).ToSiFormattedString(format, formatProvider) + UnicodeSpacing.ThinSpace.ToSpacingString() + prefix.GetMetricPrefixSymbol() + RadioactivityUnit.Becquerel.GetUnitSymbol();

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    public static double ConvertFromUnit(RadioactivityUnit unit, double value)
      => unit switch
      {
        RadioactivityUnit.Becquerel => value,

        _ => unit.GetUnitFactor() * value,
      };

    public static double ConvertToUnit(RadioactivityUnit unit, double value)
      => unit switch
      {
        RadioactivityUnit.Becquerel => value,

        _ => value / unit.GetUnitFactor(),
      };

    public static double ConvertUnit(double value, RadioactivityUnit from, RadioactivityUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public double GetUnitValue(RadioactivityUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(RadioactivityUnit unit = RadioactivityUnit.Becquerel, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing spacing = UnicodeSpacing.Space, bool fullName = false)
    {
      var value = GetUnitValue(unit);

      return value.ToString(format, formatProvider)
        + spacing.ToSpacingString()
        + (fullName ? unit.GetUnitName(value.IsConsideredPlural()) : unit.GetUnitSymbol(false));
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
