namespace Flux.Units
{
  /// <summary>Angular acceleration, a vector quantity, unit of radians per second square. This is an SI derived quantity.</summary>
  /// <see href="https://en.wikipedia.org/wiki/Acceleration"/>
  public readonly record struct AngularAccelerationVector
    : System.IFormattable, ISiUnitValueQuantifiable<System.Runtime.Intrinsics.Vector256<double>, AngularAccelerationUnit>
  {
    private readonly System.Runtime.Intrinsics.Vector256<double> m_value;

    public AngularAccelerationVector(System.Runtime.Intrinsics.Vector256<double> value, AngularAccelerationUnit unit = AngularAccelerationUnit.RadianPerSecondSquared) => m_value = ConvertFromUnit(unit, value);

    public AngularAccelerationVector(double valueX, double valueY, double valueZ, double valueW, AngularAccelerationUnit unit = AngularAccelerationUnit.RadianPerSecondSquared)
      : this(System.Runtime.Intrinsics.Vector256.Create(valueX, valueY, valueZ, valueW), unit) { }

    public AngularAccelerationVector(MetricPrefix prefix, System.Runtime.Intrinsics.Vector256<double> meterPerSecondSquare) => m_value = prefix.ChangePrefix(meterPerSecondSquare, MetricPrefix.Unprefixed);

    public AngularAccelerationVector(MetricPrefix prefix, double meterPerSecondSquareX, double meterPerSecondSquareY, double meterPerSecondSquareZ, double meterPerSecondSquareW)
      : this(prefix, System.Runtime.Intrinsics.Vector256.Create(meterPerSecondSquareX, meterPerSecondSquareY, meterPerSecondSquareZ, meterPerSecondSquareW)) { }

    public double X => m_value[0];
    public double Y => m_value[1];
    public double Z => m_value[2];
    public double W => m_value[3];

    #region Overloaded operators

    public static AngularAccelerationVector operator -(AngularAccelerationVector v) => new(v.m_value.Negate());
    public static AngularAccelerationVector operator +(AngularAccelerationVector a, double b) => new(a.m_value.Add(b));
    public static AngularAccelerationVector operator +(AngularAccelerationVector a, AngularAccelerationVector b) => new(a.m_value.Add(b.m_value));
    public static AngularAccelerationVector operator /(AngularAccelerationVector a, double b) => new(a.m_value.Divide(b));
    public static AngularAccelerationVector operator /(AngularAccelerationVector a, AngularAccelerationVector b) => new(a.m_value.Divide(b.m_value));
    public static AngularAccelerationVector operator *(AngularAccelerationVector a, double b) => new(a.m_value.Multiply(b));
    public static AngularAccelerationVector operator *(AngularAccelerationVector a, AngularAccelerationVector b) => new(a.m_value.Multiply(b.m_value));
    public static AngularAccelerationVector operator %(AngularAccelerationVector a, double b) => new(a.m_value.Remainder(b));
    public static AngularAccelerationVector operator %(AngularAccelerationVector a, AngularAccelerationVector b) => new(a.m_value.Remainder(b.m_value));
    public static AngularAccelerationVector operator -(AngularAccelerationVector a, double b) => new(a.m_value.Subtract(b));
    public static AngularAccelerationVector operator -(AngularAccelerationVector a, AngularAccelerationVector b) => new(a.m_value.Subtract(b.m_value));

    #endregion Overloaded operators

    #region Implemented interfaces

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToUnitString(AngularAccelerationUnit.RadianPerSecondSquared, format, formatProvider);

    #region ISiUnitValueQuantifiable<>

    public System.Runtime.Intrinsics.Vector256<double> GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ChangePrefix(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, string? format = null, System.IFormatProvider? formatProvider = null)
      => GetSiUnitValue(prefix).ToString() + UnicodeSpacing.ThinSpace.ToSpacingString() + prefix.GetMetricPrefixSymbol() + AngularAccelerationUnit.RadianPerSecondSquared.GetUnitSymbol();

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    public static System.Runtime.Intrinsics.Vector256<double> ConvertFromUnit(AngularAccelerationUnit unit, System.Runtime.Intrinsics.Vector256<double> value)
      => unit switch
      {
        AngularAccelerationUnit.RadianPerSecondSquared => value,

        _ => unit.GetUnitFactor() * value,
      };

    public static System.Runtime.Intrinsics.Vector256<double> ConvertToUnit(AngularAccelerationUnit unit, System.Runtime.Intrinsics.Vector256<double> value)
      => unit switch
      {
        AngularAccelerationUnit.RadianPerSecondSquared => value,

        _ => value / unit.GetUnitFactor(),
      };

    public static System.Runtime.Intrinsics.Vector256<double> ConvertUnit(System.Runtime.Intrinsics.Vector256<double> value, AngularAccelerationUnit from, AngularAccelerationUnit to)
      => ConvertToUnit(to, ConvertFromUnit(from, value));

    public System.Runtime.Intrinsics.Vector256<double> GetUnitValue(AngularAccelerationUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(AngularAccelerationUnit unit = AngularAccelerationUnit.RadianPerSecondSquared, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing spacing = UnicodeSpacing.Space, bool fullName = false)
      => GetUnitValue(unit).ToString() + spacing.ToSpacingString() + (fullName ? unit.GetUnitName(true) : unit.GetUnitSymbol(false));

    #endregion // IUnitValueQuantifiable<>

    #region IValueQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="AngularAccelerationVector.Value"/> property is in <see cref="AngularAccelerationUnit.RadianPerSecondSquared"/>.</para>
    /// </summary>
    public System.Runtime.Intrinsics.Vector256<double> Value => m_value;

    #endregion // IValueQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
