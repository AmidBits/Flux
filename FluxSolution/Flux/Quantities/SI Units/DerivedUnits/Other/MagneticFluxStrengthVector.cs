namespace Flux.Quantities
{
  /// <summary>Magnetic flux strength (H), a vector quantity, unit of ampere-per-meter. This is an SI derived quantity.</summary>
  /// <see href="https://en.wikipedia.org/wiki/Magnetic_field"/>
  public readonly record struct MagneticFluxStrengthVector
    : System.IFormattable, ISiUnitValueQuantifiable<System.Runtime.Intrinsics.Vector256<double>, MagneticFluxStrengthUnit>
  {
    private readonly System.Runtime.Intrinsics.Vector256<double> m_value;

    public MagneticFluxStrengthVector(System.Runtime.Intrinsics.Vector256<double> value, MagneticFluxStrengthUnit unit = MagneticFluxStrengthUnit.AmperePerMeter) => m_value = ConvertFromUnit(unit, value);

    public MagneticFluxStrengthVector(double valueX, double valueY, double valueZ, double valueW, MagneticFluxStrengthUnit unit = MagneticFluxStrengthUnit.AmperePerMeter)
      : this(System.Runtime.Intrinsics.Vector256.Create(valueX, valueY, valueZ, valueW), unit) { }

    public MagneticFluxStrengthVector(MetricPrefix prefix, System.Runtime.Intrinsics.Vector256<double> AmperePerMeterSquare) => m_value = prefix.ConvertTo(AmperePerMeterSquare, MetricPrefix.Unprefixed);

    public MagneticFluxStrengthVector(MetricPrefix prefix, double AmperePerMeterSquareX, double AmperePerMeterSquareY, double AmperePerMeterSquareZ, double AmperePerMeterSquareW)
      : this(prefix, System.Runtime.Intrinsics.Vector256.Create(AmperePerMeterSquareX, AmperePerMeterSquareY, AmperePerMeterSquareZ, AmperePerMeterSquareW)) { }

    public double X => m_value[0];
    public double Y => m_value[1];
    public double Z => m_value[2];
    public double W => m_value[3];

    #region Overloaded operators

    public static MagneticFluxStrengthVector operator -(MagneticFluxStrengthVector v) => new(v.m_value.Negate());
    public static MagneticFluxStrengthVector operator +(MagneticFluxStrengthVector a, double b) => new(a.m_value.Add(b));
    public static MagneticFluxStrengthVector operator +(MagneticFluxStrengthVector a, MagneticFluxStrengthVector b) => new(a.m_value.Add(b.m_value));
    public static MagneticFluxStrengthVector operator /(MagneticFluxStrengthVector a, double b) => new(a.m_value.Divide(b));
    public static MagneticFluxStrengthVector operator /(MagneticFluxStrengthVector a, MagneticFluxStrengthVector b) => new(a.m_value.Divide(b.m_value));
    public static MagneticFluxStrengthVector operator *(MagneticFluxStrengthVector a, double b) => new(a.m_value.Multiply(b));
    public static MagneticFluxStrengthVector operator *(MagneticFluxStrengthVector a, MagneticFluxStrengthVector b) => new(a.m_value.Multiply(b.m_value));
    public static MagneticFluxStrengthVector operator %(MagneticFluxStrengthVector a, double b) => new(a.m_value.Remainder(b));
    public static MagneticFluxStrengthVector operator %(MagneticFluxStrengthVector a, MagneticFluxStrengthVector b) => new(a.m_value.Remainder(b.m_value));
    public static MagneticFluxStrengthVector operator -(MagneticFluxStrengthVector a, double b) => new(a.m_value.Subtract(b));
    public static MagneticFluxStrengthVector operator -(MagneticFluxStrengthVector a, MagneticFluxStrengthVector b) => new(a.m_value.Subtract(b.m_value));

    #endregion Overloaded operators

    #region Implemented interfaces

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToUnitString(MagneticFluxStrengthUnit.AmperePerMeter, format, formatProvider);

    #region ISiUnitValueQuantifiable<>

    public static string GetSiUnitName(MetricPrefix prefix, bool preferPlural) => prefix.GetMetricPrefixName() + GetUnitName(MagneticFluxStrengthUnit.AmperePerMeter, preferPlural);

    public static string GetSiUnitSymbol(MetricPrefix prefix, bool preferUnicode) => prefix.GetMetricPrefixSymbol(preferUnicode) + GetUnitSymbol(MagneticFluxStrengthUnit.AmperePerMeter, preferUnicode);

    public System.Runtime.Intrinsics.Vector256<double> GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertTo(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, bool fullName = false)
      => GetSiUnitValue(prefix).ToString() + Unicode.UnicodeSpacing.ThinSpace.ToSpacingString() + (fullName ? GetSiUnitName(prefix, true) : GetSiUnitSymbol(prefix, false));

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    public static System.Runtime.Intrinsics.Vector256<double> ConvertFromUnit(MagneticFluxStrengthUnit unit, System.Runtime.Intrinsics.Vector256<double> value)
      => unit switch
      {
        MagneticFluxStrengthUnit.AmperePerMeter => value,

        _ => GetUnitFactor(unit) * value,
      };

    public static System.Runtime.Intrinsics.Vector256<double> ConvertToUnit(MagneticFluxStrengthUnit unit, System.Runtime.Intrinsics.Vector256<double> value)
      => unit switch
      {
        MagneticFluxStrengthUnit.AmperePerMeter => value,

        _ => value / GetUnitFactor(unit),
      };

    public static System.Runtime.Intrinsics.Vector256<double> ConvertUnit(System.Runtime.Intrinsics.Vector256<double> value, MagneticFluxStrengthUnit from, MagneticFluxStrengthUnit to)
      => ConvertToUnit(to, ConvertFromUnit(from, value));

    public static System.Runtime.Intrinsics.Vector256<double> GetUnitFactor(MagneticFluxStrengthUnit unit) => System.Runtime.Intrinsics.Vector256.Create(MagneticFluxStrength.GetUnitFactor(unit));

    public static string GetUnitName(MagneticFluxStrengthUnit unit, bool preferPlural) => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(MagneticFluxStrengthUnit unit, bool preferUnicode) => MagneticFluxStrength.GetUnitSymbol(unit, preferUnicode);

    public System.Runtime.Intrinsics.Vector256<double> GetUnitValue(MagneticFluxStrengthUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(MagneticFluxStrengthUnit unit = MagneticFluxStrengthUnit.AmperePerMeter, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
      => GetUnitValue(unit).ToString() + Unicode.UnicodeSpacing.Space.ToSpacingString() + (fullName ? GetUnitName(unit, true) : GetUnitSymbol(unit, false));

    #endregion // IUnitValueQuantifiable<>

    #region IValueQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="MagneticFluxStrengthVector.Value"/> property is in <see cref="MagneticFluxStrengthUnit.AmperePerMeterSquared"/>.</para>
    /// </summary>
    public System.Runtime.Intrinsics.Vector256<double> Value => m_value;

    #endregion // IValueQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
