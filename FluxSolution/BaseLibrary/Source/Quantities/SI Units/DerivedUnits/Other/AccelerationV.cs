namespace Flux.Quantities
{
  /// <summary>Acceleration3D, a vector quantity, unit of meters per second square. This is an SI derived quantity.</summary>
  /// <see href="https://en.wikipedia.org/wiki/Acceleration"/>
  public readonly record struct AccelerationV
    : System.IFormattable, ISiUnitValueQuantifiable<System.Runtime.Intrinsics.Vector256<double>, AccelerationUnit>
  {
    private readonly System.Runtime.Intrinsics.Vector256<double> m_value;

    public AccelerationV(System.Runtime.Intrinsics.Vector256<double> value, AccelerationUnit unit = AccelerationUnit.MeterPerSecondSquared) => m_value = ConvertFromUnit(unit, value);

    public AccelerationV(double valueX, double valueY, double valueZ, double valueW, AccelerationUnit unit = AccelerationUnit.MeterPerSecondSquared)
      : this(System.Runtime.Intrinsics.Vector256.Create(valueX, valueY, valueZ, valueW), unit) { }

    public AccelerationV(MetricPrefix prefix, System.Runtime.Intrinsics.Vector256<double> meterPerSecondSquare) => m_value = prefix.ConvertTo(meterPerSecondSquare, MetricPrefix.Unprefixed);

    public AccelerationV(MetricPrefix prefix, double meterPerSecondSquareX, double meterPerSecondSquareY, double meterPerSecondSquareZ, double meterPerSecondSquareW)
      : this(prefix, System.Runtime.Intrinsics.Vector256.Create(meterPerSecondSquareX, meterPerSecondSquareY, meterPerSecondSquareZ, meterPerSecondSquareW)) { }

    public double X => m_value[0];
    public double Y => m_value[1];
    public double Z => m_value[2];
    public double W => m_value[3];

    #region Overloaded operators

    public static AccelerationV operator -(AccelerationV v) => new(v.m_value.Negate());
    public static AccelerationV operator +(AccelerationV a, double b) => new(a.m_value.Add(b));
    public static AccelerationV operator +(AccelerationV a, AccelerationV b) => new(a.m_value.Add(b.m_value));
    public static AccelerationV operator /(AccelerationV a, double b) => new(a.m_value.Divide(b));
    public static AccelerationV operator /(AccelerationV a, AccelerationV b) => new(a.m_value.Divide(b.m_value));
    public static AccelerationV operator *(AccelerationV a, double b) => new(a.m_value.Multiply(b));
    public static AccelerationV operator *(AccelerationV a, AccelerationV b) => new(a.m_value.Multiply(b.m_value));
    public static AccelerationV operator %(AccelerationV a, double b) => new(a.m_value.Remainder(b));
    public static AccelerationV operator %(AccelerationV a, AccelerationV b) => new(a.m_value.Remainder(b.m_value));
    public static AccelerationV operator -(AccelerationV a, double b) => new(a.m_value.Subtract(b));
    public static AccelerationV operator -(AccelerationV a, AccelerationV b) => new(a.m_value.Subtract(b.m_value));

    #endregion Overloaded operators

    #region Implemented interfaces

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToUnitString(AccelerationUnit.MeterPerSecondSquared, format, formatProvider);

    #region ISiUnitValueQuantifiable<>

    public static string GetSiUnitName(MetricPrefix prefix, bool preferPlural) => prefix.GetMetricPrefixName() + GetUnitName(AccelerationUnit.MeterPerSecondSquared, preferPlural);

    public static string GetSiUnitSymbol(MetricPrefix prefix, bool preferUnicode) => prefix.GetMetricPrefixSymbol(preferUnicode) + GetUnitSymbol(AccelerationUnit.MeterPerSecondSquared, preferUnicode);

    public System.Runtime.Intrinsics.Vector256<double> GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertTo(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, bool fullName = false)
      => GetSiUnitValue(prefix).ToString() + UnicodeSpacing.ThinSpace.ToSpacingString() + (fullName ? GetSiUnitName(prefix, true) : GetSiUnitSymbol(prefix, false));

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    private static System.Runtime.Intrinsics.Vector256<double> ConvertFromUnit(AccelerationUnit unit, System.Runtime.Intrinsics.Vector256<double> value)
      => unit switch
      {
        AccelerationUnit.MeterPerSecondSquared => value,

        _ => GetUnitFactor(unit) * value,
      };

    private static System.Runtime.Intrinsics.Vector256<double> ConvertToUnit(AccelerationUnit unit, System.Runtime.Intrinsics.Vector256<double> value)
      => unit switch
      {
        AccelerationUnit.MeterPerSecondSquared => value,

        _ => value / GetUnitFactor(unit),
      };

    public static System.Runtime.Intrinsics.Vector256<double> ConvertUnit(System.Runtime.Intrinsics.Vector256<double> value, AccelerationUnit from, AccelerationUnit to)
      => ConvertToUnit(to, ConvertFromUnit(from, value));

    public static System.Runtime.Intrinsics.Vector256<double> GetUnitFactor(AccelerationUnit unit)
      => unit switch
      {
        AccelerationUnit.MeterPerSecondSquared => System.Runtime.Intrinsics.Vector256<double>.One,

        _ => throw new System.NotImplementedException()
      };

    public static string GetUnitName(AccelerationUnit unit, bool preferPlural) => unit.ToString().ConvertUnitNameToPlural(preferPlural);

    public static string GetUnitSymbol(AccelerationUnit unit, bool preferUnicode) => Acceleration.GetUnitSymbol(unit, preferUnicode);

    public System.Runtime.Intrinsics.Vector256<double> GetUnitValue(AccelerationUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(AccelerationUnit unit = AccelerationUnit.MeterPerSecondSquared, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
      => GetUnitValue(unit).ToString() + UnicodeSpacing.Space.ToSpacingString() + (fullName ? GetUnitName(unit, true) : GetUnitSymbol(unit, false));

    #endregion // IUnitValueQuantifiable<>

    #region IValueQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="AccelerationV.Value"/> property is in <see cref="AccelerationUnit.MeterPerSecondSquared"/>.</para>
    /// </summary>
    public System.Runtime.Intrinsics.Vector256<double> Value => m_value;

    #endregion // IValueQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
