namespace Flux.Quantities
{
  public enum MagneticFluxUnit
  {
    /// <summary>This is the default unit for <see cref="MagneticFlux"/>.</summary>
    Weber,
  }

  /// <summary>Magnetic flux unit of weber.</summary>
  /// <see href="https://en.wikipedia.org/wiki/Magnetic_flux"/>
  public readonly record struct MagneticFlux
    : System.IComparable, System.IComparable<MagneticFlux>, System.IFormattable, ISiPrefixValueQuantifiable<double, MagneticFluxUnit>
  {
    private readonly double m_value;

    public MagneticFlux(double value, MagneticFluxUnit unit = MagneticFluxUnit.Weber) => m_value = ConvertFromUnit(unit, value);

    #region Overloaded operators

    public static bool operator <(MagneticFlux a, MagneticFlux b) => a.CompareTo(b) < 0;
    public static bool operator <=(MagneticFlux a, MagneticFlux b) => a.CompareTo(b) <= 0;
    public static bool operator >(MagneticFlux a, MagneticFlux b) => a.CompareTo(b) > 0;
    public static bool operator >=(MagneticFlux a, MagneticFlux b) => a.CompareTo(b) >= 0;

    public static MagneticFlux operator -(MagneticFlux v) => new(-v.m_value);
    public static MagneticFlux operator +(MagneticFlux a, double b) => new(a.m_value + b);
    public static MagneticFlux operator +(MagneticFlux a, MagneticFlux b) => a + b.m_value;
    public static MagneticFlux operator /(MagneticFlux a, double b) => new(a.m_value / b);
    public static MagneticFlux operator /(MagneticFlux a, MagneticFlux b) => a / b.m_value;
    public static MagneticFlux operator *(MagneticFlux a, double b) => new(a.m_value * b);
    public static MagneticFlux operator *(MagneticFlux a, MagneticFlux b) => a * b.m_value;
    public static MagneticFlux operator %(MagneticFlux a, double b) => new(a.m_value % b);
    public static MagneticFlux operator %(MagneticFlux a, MagneticFlux b) => a % b.m_value;
    public static MagneticFlux operator -(MagneticFlux a, double b) => new(a.m_value - b);
    public static MagneticFlux operator -(MagneticFlux a, MagneticFlux b) => a - b.m_value;

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is MagneticFlux o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(MagneticFlux other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToSiPrefixString(MetricPrefix.Unprefixed);

    #region IQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="MagneticFlux.Value"/> property is in <see cref="MagneticFluxUnit.Weber"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IQuantifiable<>

    #region ISiUnitValueQuantifiable<>

    public string GetSiPrefixName(MetricPrefix prefix, bool preferPlural) => prefix.GetPrefixName() + GetUnitName(MagneticFluxUnit.Weber, preferPlural);

    public string GetSiPrefixSymbol(MetricPrefix prefix, bool preferUnicode) => prefix.GetPrefixSymbol(preferUnicode) + GetUnitSymbol(MagneticFluxUnit.Weber, preferUnicode);

    public double GetSiPrefixValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertTo(m_value, prefix);

    public string ToSiPrefixString(MetricPrefix prefix, bool fullName = false)
      => GetSiPrefixValue(prefix).ToSiFormattedString() + UnicodeSpacing.ThinSpace.ToSpacingString() + (fullName ? GetSiPrefixName(prefix, true) : GetSiPrefixSymbol(prefix, false));

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitQuantifiable<>

    private static double ConvertFromUnit(MagneticFluxUnit unit, double value)
      => unit switch
      {
        MagneticFluxUnit.Weber => value,

        _ => GetUnitFactor(unit) * value,
      };

    private static double ConvertToUnit(MagneticFluxUnit unit, double value)
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

        _ => throw new System.NotImplementedException()
      };

    public string GetUnitName(MagneticFluxUnit unit, bool preferPlural)
      => unit.ToString().ConvertUnitNameToPlural(preferPlural && GetUnitValue(unit).IsConsideredPlural());

    public string GetUnitSymbol(MagneticFluxUnit unit, bool preferUnicode)
      => unit switch
      {
        Quantities.MagneticFluxUnit.Weber => preferUnicode ? "\u33DD" : "Wb",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(MagneticFluxUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(MagneticFluxUnit unit = MagneticFluxUnit.Weber, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + UnicodeSpacing.Space.ToSpacingString() + (fullName ? GetUnitName(unit, true) : GetUnitSymbol(unit, false));

    #endregion // IUnitQuantifiable<>

    #endregion Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
