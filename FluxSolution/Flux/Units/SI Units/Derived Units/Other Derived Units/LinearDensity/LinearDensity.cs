namespace Flux.Units
{
  /// <summary>
  /// <para>Linear mass density, unit of kilograms per meter.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Linear_density"/></para>
  /// </summary>
  /// <remarks>Dimensional relationship: <see cref="LinearDensity"/>, <see cref="AreaDensity"/> and <see cref="Density"/>.</remarks>
  public readonly record struct LinearDensity
    : System.IComparable, System.IComparable<LinearDensity>, System.IFormattable, ISiUnitValueQuantifiable<double, LinearDensityUnit>
  {
    private readonly double m_value;

    public LinearDensity(double value, LinearDensityUnit unit = LinearDensityUnit.KilogramPerMeter) => m_value = ConvertFromUnit(unit, value);

    public LinearDensity(MetricPrefix prefix, double gramPerMeter) => m_value = prefix.ConvertPrefix(gramPerMeter, MetricPrefix.Unprefixed);

    public LinearDensity(Mass mass, Volume volume) : this(mass.Value / volume.Value) { }

    #region Static methods

    #endregion // Static methods

    #region Overloaded operators

    public static bool operator <(LinearDensity a, LinearDensity b) => a.CompareTo(b) < 0;
    public static bool operator >(LinearDensity a, LinearDensity b) => a.CompareTo(b) > 0;
    public static bool operator <=(LinearDensity a, LinearDensity b) => a.CompareTo(b) <= 0;
    public static bool operator >=(LinearDensity a, LinearDensity b) => a.CompareTo(b) >= 0;

    public static LinearDensity operator -(LinearDensity v) => new(-v.m_value);
    public static LinearDensity operator *(LinearDensity a, LinearDensity b) => new(a.m_value * b.m_value);
    public static LinearDensity operator /(LinearDensity a, LinearDensity b) => new(a.m_value / b.m_value);
    public static LinearDensity operator %(LinearDensity a, LinearDensity b) => new(a.m_value % b.m_value);
    public static LinearDensity operator +(LinearDensity a, LinearDensity b) => new(a.m_value + b.m_value);
    public static LinearDensity operator -(LinearDensity a, LinearDensity b) => new(a.m_value - b.m_value);
    public static LinearDensity operator *(LinearDensity a, double b) => new(a.m_value * b);
    public static LinearDensity operator /(LinearDensity a, double b) => new(a.m_value / b);
    public static LinearDensity operator %(LinearDensity a, double b) => new(a.m_value % b);
    public static LinearDensity operator +(LinearDensity a, double b) => new(a.m_value + b);
    public static LinearDensity operator -(LinearDensity a, double b) => new(a.m_value - b);

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is LinearDensity o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(LinearDensity other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToUnitString(LinearDensityUnit.KilogramPerMeter, format, formatProvider);

    #region ISiUnitValueQuantifiable<>

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Kilo.ConvertPrefix(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, string? format = null, System.IFormatProvider? formatProvider = null)
      => GetSiUnitValue(prefix).ToSiFormattedString(format, formatProvider) + UnicodeSpacing.ThinSpace.ToSpacingString() + prefix.GetMetricPrefixSymbol() + LinearDensityUnit.GramPerMeter.GetUnitSymbol();

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    public static double ConvertFromUnit(LinearDensityUnit unit, double value)
      => unit switch
      {
        LinearDensityUnit.KilogramPerMeter => value,

        _ => unit.GetUnitFactor() * value,
      };

    public static double ConvertToUnit(LinearDensityUnit unit, double value)
      => unit switch
      {
        LinearDensityUnit.KilogramPerMeter => value,

        _ => value / unit.GetUnitFactor(),
      };

    public static double ConvertUnit(double value, LinearDensityUnit from, LinearDensityUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public double GetUnitValue(LinearDensityUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(LinearDensityUnit unit = LinearDensityUnit.KilogramPerMeter, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing spacing = UnicodeSpacing.Space, bool fullName = false)
    {
      var value = GetUnitValue(unit);

      return value.ToString(format, formatProvider) + spacing.ToSpacingString() + (fullName ? unit.GetUnitName(Number.IsConsideredPlural(value)) : unit.GetUnitSymbol(false));
    }

    #endregion // IUnitValueQuantifiable<>

    #region IValueQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="LinearDensity.Value"/> property is in <see cref="LinearDensityUnit.KilogramPerCubicMeter"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IValueQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
