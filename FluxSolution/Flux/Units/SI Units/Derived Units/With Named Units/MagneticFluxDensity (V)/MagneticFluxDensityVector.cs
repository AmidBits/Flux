namespace Flux.Units
{
  /// <summary>
  /// <para>Magnetic flux density (B), a vector quantity, unit of tesla. This is an SI derived quantity.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Magnetic_field"/></para>
  /// </summary>
  public readonly record struct MagneticFluxDensityVector
    : System.IFormattable, ISiUnitValueQuantifiable<System.Runtime.Intrinsics.Vector256<double>, MagneticFluxDensityUnit>
  {
    private readonly System.Runtime.Intrinsics.Vector256<double> m_value;

    public MagneticFluxDensityVector(System.Runtime.Intrinsics.Vector256<double> value, MagneticFluxDensityUnit unit = MagneticFluxDensityUnit.Tesla) => m_value = ConvertFromUnit(unit, value);

    public MagneticFluxDensityVector(double valueX, double valueY, double valueZ, double valueW, MagneticFluxDensityUnit unit = MagneticFluxDensityUnit.Tesla)
      : this(System.Runtime.Intrinsics.Vector256.Create(valueX, valueY, valueZ, valueW), unit) { }

    public MagneticFluxDensityVector(MetricPrefix prefix, System.Runtime.Intrinsics.Vector256<double> TeslaSquare) => m_value = prefix.ChangePrefix(TeslaSquare, MetricPrefix.Unprefixed);

    public MagneticFluxDensityVector(MetricPrefix prefix, double TeslaSquareX, double TeslaSquareY, double TeslaSquareZ, double TeslaSquareW)
      : this(prefix, System.Runtime.Intrinsics.Vector256.Create(TeslaSquareX, TeslaSquareY, TeslaSquareZ, TeslaSquareW)) { }

    public double X => m_value[0];
    public double Y => m_value[1];
    public double Z => m_value[2];
    public double W => m_value[3];

    #region Overloaded operators

    public static MagneticFluxDensityVector operator -(MagneticFluxDensityVector v) => new(v.m_value.Negate());
    public static MagneticFluxDensityVector operator +(MagneticFluxDensityVector a, double b) => new(a.m_value.Add(b));
    public static MagneticFluxDensityVector operator +(MagneticFluxDensityVector a, MagneticFluxDensityVector b) => new(a.m_value.Add(b.m_value));
    public static MagneticFluxDensityVector operator /(MagneticFluxDensityVector a, double b) => new(a.m_value.Divide(b));
    public static MagneticFluxDensityVector operator /(MagneticFluxDensityVector a, MagneticFluxDensityVector b) => new(a.m_value.Divide(b.m_value));
    public static MagneticFluxDensityVector operator *(MagneticFluxDensityVector a, double b) => new(a.m_value.Multiply(b));
    public static MagneticFluxDensityVector operator *(MagneticFluxDensityVector a, MagneticFluxDensityVector b) => new(a.m_value.Multiply(b.m_value));
    public static MagneticFluxDensityVector operator %(MagneticFluxDensityVector a, double b) => new(a.m_value.Remainder(b));
    public static MagneticFluxDensityVector operator %(MagneticFluxDensityVector a, MagneticFluxDensityVector b) => new(a.m_value.Remainder(b.m_value));
    public static MagneticFluxDensityVector operator -(MagneticFluxDensityVector a, double b) => new(a.m_value.Subtract(b));
    public static MagneticFluxDensityVector operator -(MagneticFluxDensityVector a, MagneticFluxDensityVector b) => new(a.m_value.Subtract(b.m_value));

    #endregion Overloaded operators

    #region Implemented interfaces

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToUnitString(MagneticFluxDensityUnit.Tesla, format, formatProvider);

    #region ISiUnitValueQuantifiable<>

    public System.Runtime.Intrinsics.Vector256<double> GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ChangePrefix(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, string? format = null, System.IFormatProvider? formatProvider = null)
      => GetSiUnitValue(prefix).ToString() + Unicode.UnicodeSpacing.ThinSpace.ToSpacingString() + prefix.GetMetricPrefixSymbol() + MagneticFluxDensityUnit.Tesla.GetUnitSymbol();

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    public static System.Runtime.Intrinsics.Vector256<double> ConvertFromUnit(MagneticFluxDensityUnit unit, System.Runtime.Intrinsics.Vector256<double> value)
      => unit switch
      {
        MagneticFluxDensityUnit.Tesla => value,

        _ => unit.GetUnitFactor() * value,
      };

    public static System.Runtime.Intrinsics.Vector256<double> ConvertToUnit(MagneticFluxDensityUnit unit, System.Runtime.Intrinsics.Vector256<double> value)
      => unit switch
      {
        MagneticFluxDensityUnit.Tesla => value,

        _ => value / unit.GetUnitFactor(),
      };

    public static System.Runtime.Intrinsics.Vector256<double> ConvertUnit(System.Runtime.Intrinsics.Vector256<double> value, MagneticFluxDensityUnit from, MagneticFluxDensityUnit to)
      => ConvertToUnit(to, ConvertFromUnit(from, value));

    public System.Runtime.Intrinsics.Vector256<double> GetUnitValue(MagneticFluxDensityUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(MagneticFluxDensityUnit unit = MagneticFluxDensityUnit.Tesla, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
      => GetUnitValue(unit).ToString() + Unicode.UnicodeSpacing.Space.ToSpacingString() + (fullName ? unit.GetUnitName(false) : unit.GetUnitSymbol(false));

    #endregion // IUnitValueQuantifiable<>

    #region IValueQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="MagneticFluxDensityVector.Value"/> property is in <see cref="MagneticFluxDensityUnit.TeslaSquared"/>.</para>
    /// </summary>
    public System.Runtime.Intrinsics.Vector256<double> Value => m_value;

    #endregion // IValueQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
