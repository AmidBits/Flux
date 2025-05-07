namespace Flux.Units
{
  /// <summary>
  /// <para>Velocity (a.k.a. SpeedVector), a vector quantity, unit of meters per second, is the speed in combination with the direction of motion of an object. This is an SI derived quantity.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Velocity"/></para>
  /// </summary>
  public readonly record struct Velocity
    : System.IFormattable, ISiUnitValueQuantifiable<System.Runtime.Intrinsics.Vector256<double>, SpeedUnit>
  {
    private readonly System.Runtime.Intrinsics.Vector256<double> m_value;

    public Velocity(System.Runtime.Intrinsics.Vector256<double> value, SpeedUnit unit = SpeedUnit.MeterPerSecond) => m_value = ConvertFromUnit(unit, value);

    public Velocity(double valueX, double valueY, double valueZ, double valueW, SpeedUnit unit = SpeedUnit.MeterPerSecond)
      : this(System.Runtime.Intrinsics.Vector256.Create(valueX, valueY, valueZ, valueW), unit) { }

    public Velocity(MetricPrefix prefix, System.Runtime.Intrinsics.Vector256<double> meterPerSecondSquare) => m_value = prefix.ChangePrefix(meterPerSecondSquare, MetricPrefix.Unprefixed);

    public Velocity(MetricPrefix prefix, double meterPerSecondSquareX, double meterPerSecondSquareY, double meterPerSecondSquareZ, double meterPerSecondSquareW)
      : this(prefix, System.Runtime.Intrinsics.Vector256.Create(meterPerSecondSquareX, meterPerSecondSquareY, meterPerSecondSquareZ, meterPerSecondSquareW)) { }

    public double X => m_value[0];
    public double Y => m_value[1];
    public double Z => m_value[2];
    public double W => m_value[3];

    #region Overloaded operators

    public static Velocity operator -(Velocity v) => new(v.m_value.Negate());
    public static Velocity operator +(Velocity a, double b) => new(a.m_value.Add(b));
    public static Velocity operator +(Velocity a, Velocity b) => new(a.m_value.Add(b.m_value));
    public static Velocity operator /(Velocity a, double b) => new(a.m_value.Divide(b));
    public static Velocity operator /(Velocity a, Velocity b) => new(a.m_value.Divide(b.m_value));
    public static Velocity operator *(Velocity a, double b) => new(a.m_value.Multiply(b));
    public static Velocity operator *(Velocity a, Velocity b) => new(a.m_value.Multiply(b.m_value));
    public static Velocity operator %(Velocity a, double b) => new(a.m_value.Remainder(b));
    public static Velocity operator %(Velocity a, Velocity b) => new(a.m_value.Remainder(b.m_value));
    public static Velocity operator -(Velocity a, double b) => new(a.m_value.Subtract(b));
    public static Velocity operator -(Velocity a, Velocity b) => new(a.m_value.Subtract(b.m_value));

    #endregion Overloaded operators

    #region Implemented interfaces

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToUnitString(SpeedUnit.MeterPerSecond, format, formatProvider);

    #region ISiUnitValueQuantifiable<>

    public static string GetSiUnitName(MetricPrefix prefix, bool preferPlural) => prefix.GetMetricPrefixName() + GetUnitName(SpeedUnit.MeterPerSecond, preferPlural);

    public static string GetSiUnitSymbol(MetricPrefix prefix, bool preferUnicode) => prefix.GetMetricPrefixSymbol(preferUnicode) + GetUnitSymbol(SpeedUnit.MeterPerSecond, preferUnicode);

    public System.Runtime.Intrinsics.Vector256<double> GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ChangePrefix(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
      => GetSiUnitValue(prefix).ToString() + Unicode.UnicodeSpacing.ThinSpace.ToSpacingString() + (fullName ? GetSiUnitName(prefix, true) : GetSiUnitSymbol(prefix, false));

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    public static System.Runtime.Intrinsics.Vector256<double> ConvertFromUnit(SpeedUnit unit, System.Runtime.Intrinsics.Vector256<double> value)
      => unit switch
      {
        SpeedUnit.MeterPerSecond => value,

        _ => GetUnitFactor(unit) * value,
      };

    public static System.Runtime.Intrinsics.Vector256<double> ConvertToUnit(SpeedUnit unit, System.Runtime.Intrinsics.Vector256<double> value)
      => unit switch
      {
        SpeedUnit.MeterPerSecond => value,

        _ => value / GetUnitFactor(unit),
      };

    public static System.Runtime.Intrinsics.Vector256<double> ConvertUnit(System.Runtime.Intrinsics.Vector256<double> value, SpeedUnit from, SpeedUnit to)
      => ConvertToUnit(to, ConvertFromUnit(from, value));

    public static System.Runtime.Intrinsics.Vector256<double> GetUnitFactor(SpeedUnit unit) => System.Runtime.Intrinsics.Vector256.Create(Speed.GetUnitFactor(unit));

    public static string GetUnitName(SpeedUnit unit, bool preferPlural) => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(SpeedUnit unit, bool preferUnicode) => Speed.GetUnitSymbol(unit, preferUnicode);

    public System.Runtime.Intrinsics.Vector256<double> GetUnitValue(SpeedUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(SpeedUnit unit = SpeedUnit.MeterPerSecond, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
      => GetUnitValue(unit).ToString() + Unicode.UnicodeSpacing.Space.ToSpacingString() + (fullName ? GetUnitName(unit, true) : GetUnitSymbol(unit, false));

    #endregion // IUnitValueQuantifiable<>

    #region IValueQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="Velocity.Value"/> property is in <see cref="SpeedUnit.MeterPerSecondSquared"/>.</para>
    /// </summary>
    public System.Runtime.Intrinsics.Vector256<double> Value => m_value;

    #endregion // IValueQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
