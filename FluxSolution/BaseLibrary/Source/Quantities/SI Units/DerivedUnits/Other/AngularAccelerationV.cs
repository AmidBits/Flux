namespace Flux.Quantities
{
  /// <summary>Angular acceleration, a vector quantity, unit of radians per second square. This is an SI derived quantity.</summary>
  /// <see href="https://en.wikipedia.org/wiki/Acceleration"/>
  public readonly record struct AngularAccelerationV
    : System.IFormattable, ISiUnitValueQuantifiable<System.Runtime.Intrinsics.Vector256<double>, AngularAccelerationUnit>
  {
    private readonly System.Runtime.Intrinsics.Vector256<double> m_value;

    public AngularAccelerationV(System.Runtime.Intrinsics.Vector256<double> value, AngularAccelerationUnit unit = AngularAccelerationUnit.RadianPerSecondSquared) => m_value = ConvertFromUnit(unit, value);

    public AngularAccelerationV(double valueX, double valueY, double valueZ, double valueW, AngularAccelerationUnit unit = AngularAccelerationUnit.RadianPerSecondSquared)
      : this(System.Runtime.Intrinsics.Vector256.Create(valueX, valueY, valueZ, valueW), unit) { }

    public AngularAccelerationV(MetricPrefix prefix, System.Runtime.Intrinsics.Vector256<double> meterPerSecondSquare) => m_value = prefix.ConvertTo(meterPerSecondSquare, MetricPrefix.Unprefixed);

    public AngularAccelerationV(MetricPrefix prefix, double meterPerSecondSquareX, double meterPerSecondSquareY, double meterPerSecondSquareZ, double meterPerSecondSquareW)
      : this(prefix, System.Runtime.Intrinsics.Vector256.Create(meterPerSecondSquareX, meterPerSecondSquareY, meterPerSecondSquareZ, meterPerSecondSquareW)) { }

    public double X => m_value[0];
    public double Y => m_value[1];
    public double Z => m_value[2];
    public double W => m_value[3];

    #region Overloaded operators

    public static AngularAccelerationV operator -(AngularAccelerationV v) => new(v.m_value.Negate());
    public static AngularAccelerationV operator +(AngularAccelerationV a, double b) => new(a.m_value.Add(b));
    public static AngularAccelerationV operator +(AngularAccelerationV a, AngularAccelerationV b) => new(a.m_value.Add(b.m_value));
    public static AngularAccelerationV operator /(AngularAccelerationV a, double b) => new(a.m_value.Divide(b));
    public static AngularAccelerationV operator /(AngularAccelerationV a, AngularAccelerationV b) => new(a.m_value.Divide(b.m_value));
    public static AngularAccelerationV operator *(AngularAccelerationV a, double b) => new(a.m_value.Multiply(b));
    public static AngularAccelerationV operator *(AngularAccelerationV a, AngularAccelerationV b) => new(a.m_value.Multiply(b.m_value));
    public static AngularAccelerationV operator %(AngularAccelerationV a, double b) => new(a.m_value.Remainder(b));
    public static AngularAccelerationV operator %(AngularAccelerationV a, AngularAccelerationV b) => new(a.m_value.Remainder(b.m_value));
    public static AngularAccelerationV operator -(AngularAccelerationV a, double b) => new(a.m_value.Subtract(b));
    public static AngularAccelerationV operator -(AngularAccelerationV a, AngularAccelerationV b) => new(a.m_value.Subtract(b.m_value));

    #endregion Overloaded operators

    #region Implemented interfaces

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToUnitString(AngularAccelerationUnit.RadianPerSecondSquared, format, formatProvider);

    #region ISiUnitValueQuantifiable<>

    public static string GetSiUnitName(MetricPrefix prefix, bool preferPlural) => prefix.GetMetricPrefixName() + GetUnitName(AngularAccelerationUnit.RadianPerSecondSquared, preferPlural);

    public static string GetSiUnitSymbol(MetricPrefix prefix, bool preferUnicode) => prefix.GetMetricPrefixSymbol(preferUnicode) + GetUnitSymbol(AngularAccelerationUnit.RadianPerSecondSquared, preferUnicode);

    public System.Runtime.Intrinsics.Vector256<double> GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertTo(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, bool fullName = false)
      => GetSiUnitValue(prefix).ToString() + UnicodeSpacing.ThinSpace.ToSpacingString() + (fullName ? GetSiUnitName(prefix, true) : GetSiUnitSymbol(prefix, false));

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    private static System.Runtime.Intrinsics.Vector256<double> ConvertFromUnit(AngularAccelerationUnit unit, System.Runtime.Intrinsics.Vector256<double> value)
      => unit switch
      {
        AngularAccelerationUnit.RadianPerSecondSquared => value,

        _ => GetUnitFactor(unit) * value,
      };

    private static System.Runtime.Intrinsics.Vector256<double> ConvertToUnit(AngularAccelerationUnit unit, System.Runtime.Intrinsics.Vector256<double> value)
      => unit switch
      {
        AngularAccelerationUnit.RadianPerSecondSquared => value,

        _ => value / GetUnitFactor(unit),
      };

    public static System.Runtime.Intrinsics.Vector256<double> ConvertUnit(System.Runtime.Intrinsics.Vector256<double> value, AngularAccelerationUnit from, AngularAccelerationUnit to)
      => ConvertToUnit(to, ConvertFromUnit(from, value));

    public static System.Runtime.Intrinsics.Vector256<double> GetUnitFactor(AngularAccelerationUnit unit)
      => unit switch
      {
        AngularAccelerationUnit.RadianPerSecondSquared => System.Runtime.Intrinsics.Vector256<double>.One,

        _ => throw new System.NotImplementedException()
      };

    public static string GetUnitName(AngularAccelerationUnit unit, bool preferPlural) => unit.ToString().ConvertUnitNameToPlural(preferPlural);

    public static string GetUnitSymbol(AngularAccelerationUnit unit, bool preferUnicode)
      => unit switch
      {
        AngularAccelerationUnit.RadianPerSecondSquared => preferUnicode ? "\u33A8" : "m/s²",

        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public System.Runtime.Intrinsics.Vector256<double> GetUnitValue(AngularAccelerationUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(AngularAccelerationUnit unit = AngularAccelerationUnit.RadianPerSecondSquared, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
      => GetUnitValue(unit).ToString() + UnicodeSpacing.Space.ToSpacingString() + (fullName ? GetUnitName(unit, true) : GetUnitSymbol(unit, false));

    #endregion // IUnitValueQuantifiable<>

    #region IValueQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="AngularAccelerationV.Value"/> property is in <see cref="AngularAccelerationUnit.RadianPerSecondSquared"/>.</para>
    /// </summary>
    public System.Runtime.Intrinsics.Vector256<double> Value => m_value;

    #endregion // IValueQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
