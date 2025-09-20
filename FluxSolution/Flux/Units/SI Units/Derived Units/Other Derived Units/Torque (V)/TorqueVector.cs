namespace Flux.Units
{
  /// <summary>Torque, a vector quantity, unit of newton-meter. This is an SI derived quantity.</summary>
  /// <see href="https://en.wikipedia.org/wiki/Torque"/>
  public readonly record struct TorqueVector
    : System.IFormattable, ISiUnitValueQuantifiable<System.Runtime.Intrinsics.Vector256<double>, TorqueUnit>
  {
    private readonly System.Runtime.Intrinsics.Vector256<double> m_value;

    public TorqueVector(System.Runtime.Intrinsics.Vector256<double> value, TorqueUnit unit = TorqueUnit.NewtonMeter) => m_value = ConvertFromUnit(unit, value);

    public TorqueVector(double valueX, double valueY, double valueZ, double valueW, TorqueUnit unit = TorqueUnit.NewtonMeter)
      : this(System.Runtime.Intrinsics.Vector256.Create(valueX, valueY, valueZ, valueW), unit) { }

    public TorqueVector(MetricPrefix prefix, System.Runtime.Intrinsics.Vector256<double> NewtonMeterSquare) => m_value = prefix.ChangePrefix(NewtonMeterSquare, MetricPrefix.Unprefixed);

    public TorqueVector(MetricPrefix prefix, double NewtonMeterSquareX, double NewtonMeterSquareY, double NewtonMeterSquareZ, double NewtonMeterSquareW)
      : this(prefix, System.Runtime.Intrinsics.Vector256.Create(NewtonMeterSquareX, NewtonMeterSquareY, NewtonMeterSquareZ, NewtonMeterSquareW)) { }

    public double X => m_value[0];
    public double Y => m_value[1];
    public double Z => m_value[2];
    public double W => m_value[3];

    #region Overloaded operators

    public static TorqueVector operator -(TorqueVector v) => new(v.m_value.Negate());
    public static TorqueVector operator +(TorqueVector a, double b) => new(a.m_value.Add(b));
    public static TorqueVector operator +(TorqueVector a, TorqueVector b) => new(a.m_value.Add(b.m_value));
    public static TorqueVector operator /(TorqueVector a, double b) => new(a.m_value.Divide(b));
    public static TorqueVector operator /(TorqueVector a, TorqueVector b) => new(a.m_value.Divide(b.m_value));
    public static TorqueVector operator *(TorqueVector a, double b) => new(a.m_value.Multiply(b));
    public static TorqueVector operator *(TorqueVector a, TorqueVector b) => new(a.m_value.Multiply(b.m_value));
    public static TorqueVector operator %(TorqueVector a, double b) => new(a.m_value.Remainder(b));
    public static TorqueVector operator %(TorqueVector a, TorqueVector b) => new(a.m_value.Remainder(b.m_value));
    public static TorqueVector operator -(TorqueVector a, double b) => new(a.m_value.Subtract(b));
    public static TorqueVector operator -(TorqueVector a, TorqueVector b) => new(a.m_value.Subtract(b.m_value));

    #endregion Overloaded operators

    #region Implemented interfaces

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToUnitString(TorqueUnit.NewtonMeter, format, formatProvider);

    #region ISiUnitValueQuantifiable<>

    public System.Runtime.Intrinsics.Vector256<double> GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ChangePrefix(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, string? format = null, System.IFormatProvider? formatProvider = null)
      => GetSiUnitValue(prefix).ToString() + UnicodeSpacing.ThinSpace.ToSpacingString() + prefix.GetMetricPrefixSymbol() + TorqueUnit.NewtonMeter.GetUnitSymbol();

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    public static System.Runtime.Intrinsics.Vector256<double> ConvertFromUnit(TorqueUnit unit, System.Runtime.Intrinsics.Vector256<double> value)
      => unit switch
      {
        TorqueUnit.NewtonMeter => value,

        _ => unit.GetUnitFactor() * value,
      };

    public static System.Runtime.Intrinsics.Vector256<double> ConvertToUnit(TorqueUnit unit, System.Runtime.Intrinsics.Vector256<double> value)
      => unit switch
      {
        TorqueUnit.NewtonMeter => value,

        _ => value / unit.GetUnitFactor(),
      };

    public static System.Runtime.Intrinsics.Vector256<double> ConvertUnit(System.Runtime.Intrinsics.Vector256<double> value, TorqueUnit from, TorqueUnit to)
      => ConvertToUnit(to, ConvertFromUnit(from, value));

    public System.Runtime.Intrinsics.Vector256<double> GetUnitValue(TorqueUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(TorqueUnit unit = TorqueUnit.NewtonMeter, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing spacing = UnicodeSpacing.Space, bool fullName = false)
      => GetUnitValue(unit).ToString() + spacing.ToSpacingString() + (fullName ? unit.GetUnitName(true) : unit.GetUnitSymbol(false));

    #endregion // IUnitValueQuantifiable<>

    #region IValueQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="TorqueVector.Value"/> property is in <see cref="TorqueUnit.NewtonMeterSquared"/>.</para>
    /// </summary>
    public System.Runtime.Intrinsics.Vector256<double> Value => m_value;

    #endregion // IValueQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
