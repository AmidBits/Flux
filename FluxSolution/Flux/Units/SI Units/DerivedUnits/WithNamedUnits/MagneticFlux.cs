namespace Flux.Units
{
  /// <summary>
  /// <para>Magnetic flux, unit of weber.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Magnetic_flux"/></para>
  /// </summary>
  public readonly record struct MagneticFlux
    : System.IComparable, System.IComparable<MagneticFlux>, System.IFormattable, ISiUnitValueQuantifiable<double, MagneticFluxUnit>
  {
    private readonly double m_value;

    public MagneticFlux(double value, MagneticFluxUnit unit = MagneticFluxUnit.Weber) => m_value = ConvertFromUnit(unit, value);

    public MagneticFlux(MetricPrefix prefix, double weber) => m_value = prefix.ConvertTo(weber, MetricPrefix.Unprefixed);

    #region Overloaded operators

    public static bool operator <(MagneticFlux a, MagneticFlux b) => a.CompareTo(b) < 0;
    public static bool operator >(MagneticFlux a, MagneticFlux b) => a.CompareTo(b) > 0;
    public static bool operator <=(MagneticFlux a, MagneticFlux b) => a.CompareTo(b) <= 0;
    public static bool operator >=(MagneticFlux a, MagneticFlux b) => a.CompareTo(b) >= 0;

    public static MagneticFlux operator -(MagneticFlux v) => new(-v.m_value);
    public static MagneticFlux operator *(MagneticFlux a, MagneticFlux b) => new(a.m_value * b.m_value);
    public static MagneticFlux operator /(MagneticFlux a, MagneticFlux b) => new(a.m_value / b.m_value);
    public static MagneticFlux operator %(MagneticFlux a, MagneticFlux b) => new(a.m_value % b.m_value);
    public static MagneticFlux operator +(MagneticFlux a, MagneticFlux b) => new(a.m_value + b.m_value);
    public static MagneticFlux operator -(MagneticFlux a, MagneticFlux b) => new(a.m_value - b.m_value);
    public static MagneticFlux operator *(MagneticFlux a, double b) => new(a.m_value * b);
    public static MagneticFlux operator /(MagneticFlux a, double b) => new(a.m_value / b);
    public static MagneticFlux operator %(MagneticFlux a, double b) => new(a.m_value % b);
    public static MagneticFlux operator +(MagneticFlux a, double b) => new(a.m_value + b);
    public static MagneticFlux operator -(MagneticFlux a, double b) => new(a.m_value - b);

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is MagneticFlux o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(MagneticFlux other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToSiUnitString(MetricPrefix.Unprefixed);

    #region ISiUnitValueQuantifiable<>

    public static string GetSiUnitName(MetricPrefix prefix, bool preferPlural) => prefix.GetMetricPrefixName() + GetUnitName(MagneticFluxUnit.Weber, preferPlural);

    public static string GetSiUnitSymbol(MetricPrefix prefix, bool preferUnicode) => prefix.GetMetricPrefixSymbol(preferUnicode) + GetUnitSymbol(MagneticFluxUnit.Weber, preferUnicode);

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertTo(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, bool fullName = false)
      => GetSiUnitValue(prefix).ToSiFormattedString() + Unicode.UnicodeSpacing.ThinSpace.ToSpacingString() + (fullName ? GetSiUnitName(prefix, GetSiUnitValue(prefix).IsConsideredPlural()) : GetSiUnitSymbol(prefix, false));

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    public static double ConvertFromUnit(MagneticFluxUnit unit, double value)
      => unit switch
      {
        MagneticFluxUnit.Weber => value,

        _ => GetUnitFactor(unit) * value,
      };

    public static double ConvertToUnit(MagneticFluxUnit unit, double value)
      => unit switch
      {
        MagneticFluxUnit.Weber => value,

        _ => value / GetUnitFactor(unit),
      };

    public static double ConvertUnit(double value, MagneticFluxUnit from, MagneticFluxUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public static double GetUnitFactor(MagneticFluxUnit unit)
      => unit switch
      {
        MagneticFluxUnit.Weber => 1,

        MagneticFluxUnit.Maxwell => 1e8,

        _ => throw new System.NotImplementedException()
      };

    public static string GetUnitName(MagneticFluxUnit unit, bool preferPlural) => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(MagneticFluxUnit unit, bool preferUnicode)
      => unit switch
      {
        MagneticFluxUnit.Weber => preferUnicode ? "\u33DD" : "Wb",

        MagneticFluxUnit.Maxwell => "Mx",

        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(MagneticFluxUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(MagneticFluxUnit unit = MagneticFluxUnit.Weber, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + Unicode.UnicodeSpacing.Space.ToSpacingString() + (fullName ? GetUnitName(unit, GetUnitValue(unit).IsConsideredPlural()) : GetUnitSymbol(unit, false));

    #endregion // IUnitValueQuantifiable<>

    #region IValueQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="MagneticFlux.Value"/> property is in <see cref="MagneticFluxUnit.Weber"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IValueQuantifiable<>

    #endregion Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
