namespace Flux.Units
{
  /// <summary>
  /// <para>Volumetric mass density, unit of kilograms per cubic meter.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Density"/></para>
  /// </summary>
  /// <remarks>Dimensional relationship: <see cref="LinearDensity"/>, <see cref="AreaDensity"/> and <see cref="Density"/>.</remarks>
  public readonly record struct Density
    : System.IComparable, System.IComparable<Density>, System.IFormattable, ISiUnitValueQuantifiable<double, DensityUnit>
  {
    private readonly double m_value;

    public Density(double value, DensityUnit unit = DensityUnit.KilogramPerCubicMeter) => m_value = ConvertFromUnit(unit, value);

    public Density(MetricPrefix prefix, double gramPerCubicMeter) => m_value = prefix.ConvertPrefix(gramPerCubicMeter, MetricPrefix.Unprefixed);

    public Density(Mass mass, Volume volume) : this(mass.Value / volume.Value) { }

    #region Static methods

    #endregion // Static methods

    #region Overloaded operators

    public static bool operator <(Density a, Density b) => a.CompareTo(b) < 0;
    public static bool operator >(Density a, Density b) => a.CompareTo(b) > 0;
    public static bool operator <=(Density a, Density b) => a.CompareTo(b) <= 0;
    public static bool operator >=(Density a, Density b) => a.CompareTo(b) >= 0;

    public static Density operator -(Density v) => new(-v.m_value);
    public static Density operator *(Density a, Density b) => new(a.m_value * b.m_value);
    public static Density operator /(Density a, Density b) => new(a.m_value / b.m_value);
    public static Density operator %(Density a, Density b) => new(a.m_value % b.m_value);
    public static Density operator +(Density a, Density b) => new(a.m_value + b.m_value);
    public static Density operator -(Density a, Density b) => new(a.m_value - b.m_value);
    public static Density operator *(Density a, double b) => new(a.m_value * b);
    public static Density operator /(Density a, double b) => new(a.m_value / b);
    public static Density operator %(Density a, double b) => new(a.m_value % b);
    public static Density operator +(Density a, double b) => new(a.m_value + b);
    public static Density operator -(Density a, double b) => new(a.m_value - b);

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is Density o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(Density other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToUnitString(DensityUnit.KilogramPerCubicMeter, format, formatProvider);

    #region ISiUnitValueQuantifiable<>

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Kilo.ConvertPrefix(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, string? format = null, System.IFormatProvider? formatProvider = null)
      => GetSiUnitValue(prefix).ToSiFormattedString(format, formatProvider) + UnicodeSpacing.ThinSpace.ToSpacingString() + prefix.GetMetricPrefixSymbol() + DensityUnit.GramPerCubicMeter.GetUnitSymbol();

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    public static double ConvertFromUnit(DensityUnit unit, double value)
      => unit switch
      {
        DensityUnit.KilogramPerCubicMeter => value,

        _ => unit.GetUnitFactor() * value,
      };

    public static double ConvertToUnit(DensityUnit unit, double value)
      => unit switch
      {
        DensityUnit.KilogramPerCubicMeter => value,

        _ => value / unit.GetUnitFactor(),
      };

    public static double ConvertUnit(double value, DensityUnit from, DensityUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public double GetUnitValue(DensityUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(DensityUnit unit = DensityUnit.KilogramPerCubicMeter, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing spacing = UnicodeSpacing.Space, bool fullName = false)
    {
      var value = GetUnitValue(unit);

      return value.ToString(format, formatProvider) + spacing.ToSpacingString() + (fullName ? unit.GetUnitName(value.IsConsideredPlural()) : unit.GetUnitSymbol(false));
    }

    #endregion // IUnitValueQuantifiable<>

    #region IValueQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="Density.Value"/> property is in <see cref="DensityUnit.KilogramPerCubicMeter"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IValueQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
