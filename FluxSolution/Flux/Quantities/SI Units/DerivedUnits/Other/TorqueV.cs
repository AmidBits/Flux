namespace Flux.Quantities
{
  /// <summary>Acceleration3D, a vector quantity, unit of meters per second square. This is an SI derived quantity.</summary>
  /// <see href="https://en.wikipedia.org/wiki/Velocity"/>
  public readonly record struct TorqueV
    : System.IFormattable, ISiUnitValueQuantifiable<System.Runtime.Intrinsics.Vector256<double>, TorqueUnit>
  {
    private readonly System.Runtime.Intrinsics.Vector256<double> m_value;

    public TorqueV(System.Runtime.Intrinsics.Vector256<double> value, TorqueUnit unit = TorqueUnit.NewtonMeter) => m_value = ConvertFromUnit(unit, value);

    public TorqueV(double valueX, double valueY, double valueZ, double valueW, TorqueUnit unit = TorqueUnit.NewtonMeter)
      : this(System.Runtime.Intrinsics.Vector256.Create(valueX, valueY, valueZ, valueW), unit) { }

    public TorqueV(MetricPrefix prefix, System.Runtime.Intrinsics.Vector256<double> NewtonMeterSquare) => m_value = prefix.ConvertTo(NewtonMeterSquare, MetricPrefix.Unprefixed);

    public TorqueV(MetricPrefix prefix, double NewtonMeterSquareX, double NewtonMeterSquareY, double NewtonMeterSquareZ, double NewtonMeterSquareW)
      : this(prefix, System.Runtime.Intrinsics.Vector256.Create(NewtonMeterSquareX, NewtonMeterSquareY, NewtonMeterSquareZ, NewtonMeterSquareW)) { }

    public double X => m_value[0];
    public double Y => m_value[1];
    public double Z => m_value[2];
    public double W => m_value[3];

    #region Overloaded operators

    public static TorqueV operator -(TorqueV v) => new(v.m_value.Negate());
    public static TorqueV operator +(TorqueV a, double b) => new(a.m_value.Add(b));
    public static TorqueV operator +(TorqueV a, TorqueV b) => new(a.m_value.Add(b.m_value));
    public static TorqueV operator /(TorqueV a, double b) => new(a.m_value.Divide(b));
    public static TorqueV operator /(TorqueV a, TorqueV b) => new(a.m_value.Divide(b.m_value));
    public static TorqueV operator *(TorqueV a, double b) => new(a.m_value.Multiply(b));
    public static TorqueV operator *(TorqueV a, TorqueV b) => new(a.m_value.Multiply(b.m_value));
    public static TorqueV operator %(TorqueV a, double b) => new(a.m_value.Remainder(b));
    public static TorqueV operator %(TorqueV a, TorqueV b) => new(a.m_value.Remainder(b.m_value));
    public static TorqueV operator -(TorqueV a, double b) => new(a.m_value.Subtract(b));
    public static TorqueV operator -(TorqueV a, TorqueV b) => new(a.m_value.Subtract(b.m_value));

    #endregion Overloaded operators

    #region Implemented interfaces

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToUnitString(TorqueUnit.NewtonMeter, format, formatProvider);

    #region ISiUnitValueQuantifiable<>

    public static string GetSiUnitName(MetricPrefix prefix, bool preferPlural) => prefix.GetMetricPrefixName() + GetUnitName(TorqueUnit.NewtonMeter, preferPlural);

    public static string GetSiUnitSymbol(MetricPrefix prefix, bool preferUnicode) => prefix.GetMetricPrefixSymbol(preferUnicode) + GetUnitSymbol(TorqueUnit.NewtonMeter, preferUnicode);

    public System.Runtime.Intrinsics.Vector256<double> GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertTo(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, bool fullName = false)
      => GetSiUnitValue(prefix).ToString() + Unicode.UnicodeSpacing.ThinSpace.ToSpacingString() + (fullName ? GetSiUnitName(prefix, true) : GetSiUnitSymbol(prefix, false));

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    public static System.Runtime.Intrinsics.Vector256<double> ConvertFromUnit(TorqueUnit unit, System.Runtime.Intrinsics.Vector256<double> value)
      => unit switch
      {
        TorqueUnit.NewtonMeter => value,

        _ => GetUnitFactor(unit) * value,
      };

    public static System.Runtime.Intrinsics.Vector256<double> ConvertToUnit(TorqueUnit unit, System.Runtime.Intrinsics.Vector256<double> value)
      => unit switch
      {
        TorqueUnit.NewtonMeter => value,

        _ => value / GetUnitFactor(unit),
      };

    public static System.Runtime.Intrinsics.Vector256<double> ConvertUnit(System.Runtime.Intrinsics.Vector256<double> value, TorqueUnit from, TorqueUnit to)
      => ConvertToUnit(to, ConvertFromUnit(from, value));

    public static System.Runtime.Intrinsics.Vector256<double> GetUnitFactor(TorqueUnit unit) => System.Runtime.Intrinsics.Vector256.Create(Torque.GetUnitFactor(unit));

    public static string GetUnitName(TorqueUnit unit, bool preferPlural) => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(TorqueUnit unit, bool preferUnicode) => Torque.GetUnitSymbol(unit, preferUnicode);

    public System.Runtime.Intrinsics.Vector256<double> GetUnitValue(TorqueUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(TorqueUnit unit = TorqueUnit.NewtonMeter, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
      => GetUnitValue(unit).ToString() + Unicode.UnicodeSpacing.Space.ToSpacingString() + (fullName ? GetUnitName(unit, true) : GetUnitSymbol(unit, false));

    #endregion // IUnitValueQuantifiable<>

    #region IValueQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="TorqueV.Value"/> property is in <see cref="TorqueUnit.NewtonMeterSquared"/>.</para>
    /// </summary>
    public System.Runtime.Intrinsics.Vector256<double> Value => m_value;

    #endregion // IValueQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
