namespace Flux.Quantities
{
  public enum MolarityUnit
  {
    /// <summary>This is the default unit for <see cref="Molarity"/>.</summary>
    MolesPerCubicMeter,
  }

  /// <summary>
  /// <para>Volumetric mass density, unit of kilograms per cubic meter.</para>
  /// <see href="https://en.wikipedia.org/wiki/Density"/>
  /// </summary>
  /// <remarks>Dimensional relationship: <see cref="LinearDensity"/>, <see cref="AreaDensity"/> and <see cref="Molarity"/>.</remarks>
  public readonly record struct Molarity
    : System.IComparable, System.IComparable<Molarity>, System.IFormattable, ISiUnitValueQuantifiable<double, MolarityUnit>
  {
    private readonly double m_value;

    public Molarity(double value, MolarityUnit unit = MolarityUnit.MolesPerCubicMeter) => m_value = ConvertFromUnit(unit, value);

    #region Static methods

    #endregion // Static methods

    #region Overloaded operators

    public static bool operator <(Molarity a, Molarity b) => a.CompareTo(b) < 0;
    public static bool operator <=(Molarity a, Molarity b) => a.CompareTo(b) <= 0;
    public static bool operator >(Molarity a, Molarity b) => a.CompareTo(b) > 0;
    public static bool operator >=(Molarity a, Molarity b) => a.CompareTo(b) >= 0;

    public static Molarity operator -(Molarity v) => new(-v.m_value);
    public static Molarity operator +(Molarity a, double b) => new(a.m_value + b);
    public static Molarity operator +(Molarity a, Molarity b) => a + b.m_value;
    public static Molarity operator /(Molarity a, double b) => new(a.m_value / b);
    public static Molarity operator /(Molarity a, Molarity b) => a / b.m_value;
    public static Molarity operator *(Molarity a, double b) => new(a.m_value * b);
    public static Molarity operator *(Molarity a, Molarity b) => a * b.m_value;
    public static Molarity operator %(Molarity a, double b) => new(a.m_value % b);
    public static Molarity operator %(Molarity a, Molarity b) => a % b.m_value;
    public static Molarity operator -(Molarity a, double b) => new(a.m_value - b);
    public static Molarity operator -(Molarity a, Molarity b) => a - b.m_value;

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is Molarity o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(Molarity other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToUnitString(MolarityUnit.MolesPerCubicMeter, format, formatProvider);

    #region ISiUnitValueQuantifiable<>

    public string GetSiUnitName(MetricPrefix prefix, bool preferPlural) => prefix.GetPrefixName() + GetUnitName(MolarityUnit.MolesPerCubicMeter, preferPlural);

    public string GetSiUnitSymbol(MetricPrefix prefix, bool preferUnicode) => prefix.GetPrefixSymbol(preferUnicode) + GetUnitSymbol(MolarityUnit.MolesPerCubicMeter, preferUnicode);

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Kilo.ConvertTo(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, bool fullName = false)
      => GetSiUnitValue(prefix).ToSiFormattedString() + UnicodeSpacing.ThinSpace.ToSpacingString() + (fullName ? GetSiUnitName(prefix, true) : GetSiUnitSymbol(prefix, false));

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    private static double ConvertFromUnit(MolarityUnit unit, double value)
      => unit switch
      {
        MolarityUnit.MolesPerCubicMeter => value,

        _ => GetUnitFactor(unit) * value,
      };

    private static double ConvertToUnit(MolarityUnit unit, double value)
      => unit switch
      {
        MolarityUnit.MolesPerCubicMeter => value,

        _ => value / GetUnitFactor(unit),
      };

    public static double ConvertUnit(double value, MolarityUnit from, MolarityUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public static double GetUnitFactor(MolarityUnit unit)
      => unit switch
      {
        MolarityUnit.MolesPerCubicMeter => 1,

        _ => throw new System.NotImplementedException()
      };

    public string GetUnitName(MolarityUnit unit, bool preferPlural)
      => unit.ToString().ConvertUnitNameToPlural(preferPlural && GetUnitValue(unit).IsConsideredPlural());

    public string GetUnitSymbol(MolarityUnit unit, bool preferUnicode)
      => unit switch
      {
        MolarityUnit.MolesPerCubicMeter => "J/m³",

        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(MolarityUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(MolarityUnit unit = MolarityUnit.MolesPerCubicMeter, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + UnicodeSpacing.Space.ToSpacingString() + (fullName ? GetUnitName(unit, true) : GetUnitSymbol(unit, false));

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
