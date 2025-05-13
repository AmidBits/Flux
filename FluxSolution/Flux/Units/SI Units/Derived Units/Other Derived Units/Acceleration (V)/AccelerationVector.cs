namespace Flux.Units
{
  /// <summary>Acceleration3D, a vector quantity, unit of meters per second square. This is an SI derived quantity.</summary>
  /// <see href="https://en.wikipedia.org/wiki/Acceleration"/>
  public readonly record struct AccelerationVector
    : System.IFormattable, ISiUnitValueQuantifiable<System.Runtime.Intrinsics.Vector256<double>, AccelerationUnit>
  {
    private readonly System.Runtime.Intrinsics.Vector256<double> m_value;

    public AccelerationVector(System.Runtime.Intrinsics.Vector256<double> value, AccelerationUnit unit = AccelerationUnit.MeterPerSecondSquared) => m_value = ConvertFromUnit(unit, value);

    public AccelerationVector(double valueX, double valueY, double valueZ, double valueW, AccelerationUnit unit = AccelerationUnit.MeterPerSecondSquared)
      : this(System.Runtime.Intrinsics.Vector256.Create(valueX, valueY, valueZ, valueW), unit) { }

    public AccelerationVector(MetricPrefix prefix, System.Runtime.Intrinsics.Vector256<double> meterPerSecondSquare) => m_value = prefix.ChangePrefix(meterPerSecondSquare, MetricPrefix.Unprefixed);

    public AccelerationVector(MetricPrefix prefix, double meterPerSecondSquareX, double meterPerSecondSquareY, double meterPerSecondSquareZ, double meterPerSecondSquareW)
      : this(prefix, System.Runtime.Intrinsics.Vector256.Create(meterPerSecondSquareX, meterPerSecondSquareY, meterPerSecondSquareZ, meterPerSecondSquareW)) { }

    public double X => m_value[0];
    public double Y => m_value[1];
    public double Z => m_value[2];
    public double W => m_value[3];

    #region Overloaded operators

    public static AccelerationVector operator -(AccelerationVector v) => new(v.m_value.Negate());
    public static AccelerationVector operator +(AccelerationVector a, double b) => new(a.m_value.Add(b));
    public static AccelerationVector operator +(AccelerationVector a, AccelerationVector b) => new(a.m_value.Add(b.m_value));
    public static AccelerationVector operator /(AccelerationVector a, double b) => new(a.m_value.Divide(b));
    public static AccelerationVector operator /(AccelerationVector a, AccelerationVector b) => new(a.m_value.Divide(b.m_value));
    public static AccelerationVector operator *(AccelerationVector a, double b) => new(a.m_value.Multiply(b));
    public static AccelerationVector operator *(AccelerationVector a, AccelerationVector b) => new(a.m_value.Multiply(b.m_value));
    public static AccelerationVector operator %(AccelerationVector a, double b) => new(a.m_value.Remainder(b));
    public static AccelerationVector operator %(AccelerationVector a, AccelerationVector b) => new(a.m_value.Remainder(b.m_value));
    public static AccelerationVector operator -(AccelerationVector a, double b) => new(a.m_value.Subtract(b));
    public static AccelerationVector operator -(AccelerationVector a, AccelerationVector b) => new(a.m_value.Subtract(b.m_value));

    #endregion Overloaded operators

    #region Implemented interfaces

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToUnitString(AccelerationUnit.MeterPerSecondSquared, format, formatProvider);

    #region ISiUnitValueQuantifiable<>

    public System.Runtime.Intrinsics.Vector256<double> GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ChangePrefix(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, string? format = null, System.IFormatProvider? formatProvider = null)
      => GetSiUnitValue(prefix).ToString() + Unicode.UnicodeSpacing.ThinSpace.ToSpacingString() + prefix.GetMetricPrefixSymbol() + AccelerationUnit.MeterPerSecondSquared.GetUnitSymbol();

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    public static System.Runtime.Intrinsics.Vector256<double> ConvertFromUnit(AccelerationUnit unit, System.Runtime.Intrinsics.Vector256<double> value)
      => unit switch
      {
        AccelerationUnit.MeterPerSecondSquared => value,

        _ => unit.GetUnitFactor() * value,
      };

    public static System.Runtime.Intrinsics.Vector256<double> ConvertToUnit(AccelerationUnit unit, System.Runtime.Intrinsics.Vector256<double> value)
      => unit switch
      {
        AccelerationUnit.MeterPerSecondSquared => value,

        _ => value / unit.GetUnitFactor(),
      };

    public static System.Runtime.Intrinsics.Vector256<double> ConvertUnit(System.Runtime.Intrinsics.Vector256<double> value, AccelerationUnit from, AccelerationUnit to)
      => ConvertToUnit(to, ConvertFromUnit(from, value));

    public System.Runtime.Intrinsics.Vector256<double> GetUnitValue(AccelerationUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(AccelerationUnit unit = AccelerationUnit.MeterPerSecondSquared, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
      => GetUnitValue(unit).ToString() + Unicode.UnicodeSpacing.Space.ToSpacingString() + (fullName ? unit.GetUnitName(true) : unit.GetUnitSymbol(false));

    #endregion // IUnitValueQuantifiable<>

    #region IValueQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="AccelerationVector.Value"/> property is in <see cref="AccelerationUnit.MeterPerSecondSquared"/>.</para>
    /// </summary>
    public System.Runtime.Intrinsics.Vector256<double> Value => m_value;

    #endregion // IValueQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
