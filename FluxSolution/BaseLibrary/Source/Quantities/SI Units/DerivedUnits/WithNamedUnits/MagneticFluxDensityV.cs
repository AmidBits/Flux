namespace Flux.Quantities
{
  /// <summary>Magnetic flux density (B), a vector quantity, unit of tesla. This is an SI derived quantity.</summary>
  /// <see href="https://en.wikipedia.org/wiki/Magnetic_field"/>
  public readonly record struct MagneticFluxDensityV
    : System.IFormattable, ISiUnitValueQuantifiable<System.Runtime.Intrinsics.Vector256<double>, MagneticFluxDensityUnit>
  {
    private readonly System.Runtime.Intrinsics.Vector256<double> m_value;

    public MagneticFluxDensityV(System.Runtime.Intrinsics.Vector256<double> value, MagneticFluxDensityUnit unit = MagneticFluxDensityUnit.Tesla) => m_value = ConvertFromUnit(unit, value);

    public MagneticFluxDensityV(double valueX, double valueY, double valueZ, double valueW, MagneticFluxDensityUnit unit = MagneticFluxDensityUnit.Tesla)
      : this(System.Runtime.Intrinsics.Vector256.Create(valueX, valueY, valueZ, valueW), unit) { }

    public MagneticFluxDensityV(MetricPrefix prefix, System.Runtime.Intrinsics.Vector256<double> TeslaSquare) => m_value = prefix.ConvertTo(TeslaSquare, MetricPrefix.Unprefixed);

    public MagneticFluxDensityV(MetricPrefix prefix, double TeslaSquareX, double TeslaSquareY, double TeslaSquareZ, double TeslaSquareW)
      : this(prefix, System.Runtime.Intrinsics.Vector256.Create(TeslaSquareX, TeslaSquareY, TeslaSquareZ, TeslaSquareW)) { }

    public double X => m_value[0];
    public double Y => m_value[1];
    public double Z => m_value[2];
    public double W => m_value[3];

    #region Overloaded operators

    public static MagneticFluxDensityV operator -(MagneticFluxDensityV v) => new(v.m_value.Negate());
    public static MagneticFluxDensityV operator +(MagneticFluxDensityV a, double b) => new(a.m_value.Add(b));
    public static MagneticFluxDensityV operator +(MagneticFluxDensityV a, MagneticFluxDensityV b) => new(a.m_value.Add(b.m_value));
    public static MagneticFluxDensityV operator /(MagneticFluxDensityV a, double b) => new(a.m_value.Divide(b));
    public static MagneticFluxDensityV operator /(MagneticFluxDensityV a, MagneticFluxDensityV b) => new(a.m_value.Divide(b.m_value));
    public static MagneticFluxDensityV operator *(MagneticFluxDensityV a, double b) => new(a.m_value.Multiply(b));
    public static MagneticFluxDensityV operator *(MagneticFluxDensityV a, MagneticFluxDensityV b) => new(a.m_value.Multiply(b.m_value));
    public static MagneticFluxDensityV operator %(MagneticFluxDensityV a, double b) => new(a.m_value.Remainder(b));
    public static MagneticFluxDensityV operator %(MagneticFluxDensityV a, MagneticFluxDensityV b) => new(a.m_value.Remainder(b.m_value));
    public static MagneticFluxDensityV operator -(MagneticFluxDensityV a, double b) => new(a.m_value.Subtract(b));
    public static MagneticFluxDensityV operator -(MagneticFluxDensityV a, MagneticFluxDensityV b) => new(a.m_value.Subtract(b.m_value));

    #endregion Overloaded operators

    #region Implemented interfaces

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToUnitString(MagneticFluxDensityUnit.Tesla, format, formatProvider);

    #region ISiUnitValueQuantifiable<>

    public static string GetSiUnitName(MetricPrefix prefix, bool preferPlural) => prefix.GetMetricPrefixName() + GetUnitName(MagneticFluxDensityUnit.Tesla, preferPlural);

    public static string GetSiUnitSymbol(MetricPrefix prefix, bool preferUnicode) => prefix.GetMetricPrefixSymbol(preferUnicode) + GetUnitSymbol(MagneticFluxDensityUnit.Tesla, preferUnicode);

    public System.Runtime.Intrinsics.Vector256<double> GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertTo(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, bool fullName = false)
      => GetSiUnitValue(prefix).ToString() + UnicodeSpacing.ThinSpace.ToSpacingString() + (fullName ? GetSiUnitName(prefix, true) : GetSiUnitSymbol(prefix, false));

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    private static System.Runtime.Intrinsics.Vector256<double> ConvertFromUnit(MagneticFluxDensityUnit unit, System.Runtime.Intrinsics.Vector256<double> value)
      => unit switch
      {
        MagneticFluxDensityUnit.Tesla => value,

        _ => GetUnitFactor(unit) * value,
      };

    private static System.Runtime.Intrinsics.Vector256<double> ConvertToUnit(MagneticFluxDensityUnit unit, System.Runtime.Intrinsics.Vector256<double> value)
      => unit switch
      {
        MagneticFluxDensityUnit.Tesla => value,

        _ => value / GetUnitFactor(unit),
      };

    public static System.Runtime.Intrinsics.Vector256<double> ConvertUnit(System.Runtime.Intrinsics.Vector256<double> value, MagneticFluxDensityUnit from, MagneticFluxDensityUnit to)
      => ConvertToUnit(to, ConvertFromUnit(from, value));

    public static System.Runtime.Intrinsics.Vector256<double> GetUnitFactor(MagneticFluxDensityUnit unit) => System.Runtime.Intrinsics.Vector256.Create(MagneticFluxDensity.GetUnitFactor(unit));

    public static string GetUnitName(MagneticFluxDensityUnit unit, bool preferPlural) => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(MagneticFluxDensityUnit unit, bool preferUnicode) => MagneticFluxDensity.GetUnitSymbol(unit, preferUnicode);

    public System.Runtime.Intrinsics.Vector256<double> GetUnitValue(MagneticFluxDensityUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(MagneticFluxDensityUnit unit = MagneticFluxDensityUnit.Tesla, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
      => GetUnitValue(unit).ToString() + UnicodeSpacing.Space.ToSpacingString() + (fullName ? GetUnitName(unit, true) : GetUnitSymbol(unit, false));

    #endregion // IUnitValueQuantifiable<>

    #region IValueQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="MagneticFluxDensityV.Value"/> property is in <see cref="MagneticFluxDensityUnit.TeslaSquared"/>.</para>
    /// </summary>
    public System.Runtime.Intrinsics.Vector256<double> Value => m_value;

    #endregion // IValueQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
