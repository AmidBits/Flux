namespace Flux.Units
{
  /// <summary>
  /// <para>Current density, unit of ampere per square meter.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Current_density"/></para>
  /// </summary>
  public readonly record struct CurrentDensity
    : System.IComparable, System.IComparable<CurrentDensity>, System.IFormattable, ISiUnitValueQuantifiable<double, CurrentDensityUnit>
  {
    private readonly double m_value;

    public CurrentDensity(double value, CurrentDensityUnit unit = CurrentDensityUnit.AmperePerSquareMeter) => m_value = ConvertFromUnit(unit, value);

    public CurrentDensity(MetricPrefix prefix, double amperePerSquareMeter) => m_value = prefix.ConvertPrefix(amperePerSquareMeter, MetricPrefix.Unprefixed);

    #region Overloaded operators

    public static bool operator <(CurrentDensity a, CurrentDensity b) => a.CompareTo(b) < 0;
    public static bool operator >(CurrentDensity a, CurrentDensity b) => a.CompareTo(b) > 0;
    public static bool operator <=(CurrentDensity a, CurrentDensity b) => a.CompareTo(b) <= 0;
    public static bool operator >=(CurrentDensity a, CurrentDensity b) => a.CompareTo(b) >= 0;

    public static CurrentDensity operator -(CurrentDensity v) => new(-v.m_value);
    public static CurrentDensity operator *(CurrentDensity a, CurrentDensity b) => new(a.m_value * b.m_value);
    public static CurrentDensity operator /(CurrentDensity a, CurrentDensity b) => new(a.m_value / b.m_value);
    public static CurrentDensity operator %(CurrentDensity a, CurrentDensity b) => new(a.m_value % b.m_value);
    public static CurrentDensity operator +(CurrentDensity a, CurrentDensity b) => new(a.m_value + b.m_value);
    public static CurrentDensity operator -(CurrentDensity a, CurrentDensity b) => new(a.m_value - b.m_value);
    public static CurrentDensity operator *(CurrentDensity a, double b) => new(a.m_value * b);
    public static CurrentDensity operator /(CurrentDensity a, double b) => new(a.m_value / b);
    public static CurrentDensity operator %(CurrentDensity a, double b) => new(a.m_value % b);
    public static CurrentDensity operator +(CurrentDensity a, double b) => new(a.m_value + b);
    public static CurrentDensity operator -(CurrentDensity a, double b) => new(a.m_value - b);

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is CurrentDensity o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(CurrentDensity other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToUnitString(CurrentDensityUnit.AmperePerSquareMeter, format, formatProvider);

    #region ISiUnitValueQuantifiable<>

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertPrefix(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, string? format = null, System.IFormatProvider? formatProvider = null)
      => GetSiUnitValue(prefix).ToSiFormattedString(format, formatProvider) + UnicodeSpacing.ThinSpace.ToSpacingString() + prefix.GetMetricPrefixSymbol() + CurrentDensityUnit.AmperePerSquareMeter.GetUnitSymbol();

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    public static double ConvertFromUnit(CurrentDensityUnit unit, double value)
      => unit switch
      {
        CurrentDensityUnit.AmperePerSquareMeter => value,

        _ => unit.GetUnitFactor() * value,
      };

    public static double ConvertToUnit(CurrentDensityUnit unit, double value)
      => unit switch
      {
        CurrentDensityUnit.AmperePerSquareMeter => value,

        _ => value / unit.GetUnitFactor(),
      };

    public static double ConvertUnit(double value, CurrentDensityUnit from, CurrentDensityUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public double GetUnitValue(CurrentDensityUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(CurrentDensityUnit unit = CurrentDensityUnit.AmperePerSquareMeter, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing spacing = UnicodeSpacing.Space, bool fullName = false)
    {
      var value = GetUnitValue(unit);

      return value.ToString(format, formatProvider) + spacing.ToSpacingString() + (fullName ? unit.GetUnitName(INumber.IsConsideredPlural(value)) : unit.GetUnitSymbol(false));
    }

    #endregion // IUnitValueQuantifiable<>

    #region IValueQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="CurrentDensity.Value"/> property is in <see cref="CurrentDensityUnit.AmperePerSquareMeter"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IValueQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
