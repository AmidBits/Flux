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

    public MagneticFlux(MetricPrefix prefix, double weber) => m_value = prefix.ChangePrefix(weber, MetricPrefix.Unprefixed);

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

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ChangePrefix(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, string? format = null, System.IFormatProvider? formatProvider = null)
      => GetSiUnitValue(prefix).ToSiFormattedString(format, formatProvider) + UnicodeSpacing.ThinSpace.ToSpacingString() + prefix.GetMetricPrefixSymbol() + MagneticFluxUnit.Weber.GetUnitSymbol();

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    public static double ConvertFromUnit(MagneticFluxUnit unit, double value)
      => unit switch
      {
        MagneticFluxUnit.Weber => value,

        _ => unit.GetUnitFactor() * value,
      };

    public static double ConvertToUnit(MagneticFluxUnit unit, double value)
      => unit switch
      {
        MagneticFluxUnit.Weber => value,

        _ => value / unit.GetUnitFactor(),
      };

    public static double ConvertUnit(double value, MagneticFluxUnit from, MagneticFluxUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public double GetUnitValue(MagneticFluxUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(MagneticFluxUnit unit = MagneticFluxUnit.Weber, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing spacing = UnicodeSpacing.Space, bool fullName = false)
    {
      var value = GetUnitValue(unit);

      return value.ToString(format, formatProvider)
        + spacing.ToSpacingString()
        + (fullName ? unit.GetUnitName(false) : unit.GetUnitSymbol(false));
    }

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
