namespace Flux.Units
{
  /// <summary>
  /// <para>Volumetric mass density, unit of kilograms per cubic meter.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Density"/></para>
  /// </summary>
  /// <remarks>Dimensional relationship: <see cref="LinearDensity"/>, <see cref="AreaDensity"/> and <see cref="Molarity"/>.</remarks>
  public readonly record struct Molarity
    : System.IComparable, System.IComparable<Molarity>, System.IFormattable, ISiUnitValueQuantifiable<double, MolarityUnit>
  {
    private readonly double m_value;

    public Molarity(double value, MolarityUnit unit = MolarityUnit.MolesPerCubicMeter) => m_value = ConvertFromUnit(unit, value);

    public Molarity(MetricPrefix prefix, double molesPerCubicMeter) => m_value = prefix.ConvertPrefix(molesPerCubicMeter, MetricPrefix.Unprefixed);

    #region Static methods

    #endregion // Static methods

    #region Overloaded operators

    public static bool operator <(Molarity a, Molarity b) => a.CompareTo(b) < 0;
    public static bool operator >(Molarity a, Molarity b) => a.CompareTo(b) > 0;
    public static bool operator <=(Molarity a, Molarity b) => a.CompareTo(b) <= 0;
    public static bool operator >=(Molarity a, Molarity b) => a.CompareTo(b) >= 0;

    public static Molarity operator -(Molarity v) => new(-v.m_value);
    public static Molarity operator *(Molarity a, Molarity b) => new(a.m_value * b.m_value);
    public static Molarity operator /(Molarity a, Molarity b) => new(a.m_value / b.m_value);
    public static Molarity operator %(Molarity a, Molarity b) => new(a.m_value % b.m_value);
    public static Molarity operator +(Molarity a, Molarity b) => new(a.m_value + b.m_value);
    public static Molarity operator -(Molarity a, Molarity b) => new(a.m_value - b.m_value);
    public static Molarity operator *(Molarity a, double b) => new(a.m_value * b);
    public static Molarity operator /(Molarity a, double b) => new(a.m_value / b);
    public static Molarity operator %(Molarity a, double b) => new(a.m_value % b);
    public static Molarity operator +(Molarity a, double b) => new(a.m_value + b);
    public static Molarity operator -(Molarity a, double b) => new(a.m_value - b);

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is Molarity o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(Molarity other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToUnitString(MolarityUnit.MolesPerCubicMeter, format, formatProvider);

    #region ISiUnitValueQuantifiable<>

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Kilo.ConvertPrefix(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, string? format = null, System.IFormatProvider? formatProvider = null)
      => GetSiUnitValue(prefix).ToSiFormattedString(format, formatProvider) + UnicodeSpacing.ThinSpace.ToSpacingString() + prefix.GetMetricPrefixSymbol() + MolarityUnit.MolesPerCubicMeter.GetUnitSymbol();

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    public static double ConvertFromUnit(MolarityUnit unit, double value)
      => unit switch
      {
        MolarityUnit.MolesPerCubicMeter => value,

        _ => unit.GetUnitFactor() * value,
      };

    public static double ConvertToUnit(MolarityUnit unit, double value)
      => unit switch
      {
        MolarityUnit.MolesPerCubicMeter => value,

        _ => value / unit.GetUnitFactor(),
      };

    public static double ConvertUnit(double value, MolarityUnit from, MolarityUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public double GetUnitValue(MolarityUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(MolarityUnit unit = MolarityUnit.MolesPerCubicMeter, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing spacing = UnicodeSpacing.Space, bool fullName = false)
    {
      var value = GetUnitValue(unit);

      return value.ToString(format, formatProvider) + spacing.ToSpacingString() + (fullName ? unit.GetUnitName(INumber.IsConsideredPlural(value)) : unit.GetUnitSymbol(false));
    }

    #endregion // IUnitValueQuantifiable<>

    #region IValueQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="Molarity.Value"/> property is in <see cref="MolarityUnit.MolesPerCubicMeter"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IValueQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
