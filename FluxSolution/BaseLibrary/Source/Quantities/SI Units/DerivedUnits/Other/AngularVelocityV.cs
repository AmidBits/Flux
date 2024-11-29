namespace Flux.Quantities
{
  /// <summary>Angular velocity, a vector quantity, unit of radians per second, of which the magnitude represents the angular frequency, the angular rate at which the object rotates (spins or revolves). This is an SI derived quantity.</summary>
  /// <see href="https://en.wikipedia.org/wiki/Angular_velocity"/>
  public readonly record struct AngularVelocityV
    : System.IFormattable, ISiUnitValueQuantifiable<System.Runtime.Intrinsics.Vector256<double>, AngularFrequencyUnit>
  {
    private readonly System.Runtime.Intrinsics.Vector256<double> m_value;

    public AngularVelocityV(System.Runtime.Intrinsics.Vector256<double> value, AngularFrequencyUnit unit = AngularFrequencyUnit.RadianPerSecond) => m_value = ConvertFromUnit(unit, value);

    public AngularVelocityV(double valueX, double valueY, double valueZ, double valueW, AngularFrequencyUnit unit = AngularFrequencyUnit.RadianPerSecond)
      : this(System.Runtime.Intrinsics.Vector256.Create(valueX, valueY, valueZ, valueW), unit) { }

    public AngularVelocityV(MetricPrefix prefix, System.Runtime.Intrinsics.Vector256<double> RadianPerSecondSquare) => m_value = prefix.ConvertTo(RadianPerSecondSquare, MetricPrefix.Unprefixed);

    public AngularVelocityV(MetricPrefix prefix, double RadianPerSecondSquareX, double RadianPerSecondSquareY, double RadianPerSecondSquareZ, double RadianPerSecondSquareW)
      : this(prefix, System.Runtime.Intrinsics.Vector256.Create(RadianPerSecondSquareX, RadianPerSecondSquareY, RadianPerSecondSquareZ, RadianPerSecondSquareW)) { }

    public double X => m_value[0];
    public double Y => m_value[1];
    public double Z => m_value[2];
    public double W => m_value[3];

    #region Overloaded operators

    public static AngularVelocityV operator -(AngularVelocityV v) => new(v.m_value.Negate());
    public static AngularVelocityV operator +(AngularVelocityV a, double b) => new(a.m_value.Add(b));
    public static AngularVelocityV operator +(AngularVelocityV a, AngularVelocityV b) => new(a.m_value.Add(b.m_value));
    public static AngularVelocityV operator /(AngularVelocityV a, double b) => new(a.m_value.Divide(b));
    public static AngularVelocityV operator /(AngularVelocityV a, AngularVelocityV b) => new(a.m_value.Divide(b.m_value));
    public static AngularVelocityV operator *(AngularVelocityV a, double b) => new(a.m_value.Multiply(b));
    public static AngularVelocityV operator *(AngularVelocityV a, AngularVelocityV b) => new(a.m_value.Multiply(b.m_value));
    public static AngularVelocityV operator %(AngularVelocityV a, double b) => new(a.m_value.Remainder(b));
    public static AngularVelocityV operator %(AngularVelocityV a, AngularVelocityV b) => new(a.m_value.Remainder(b.m_value));
    public static AngularVelocityV operator -(AngularVelocityV a, double b) => new(a.m_value.Subtract(b));
    public static AngularVelocityV operator -(AngularVelocityV a, AngularVelocityV b) => new(a.m_value.Subtract(b.m_value));

    #endregion Overloaded operators

    #region Implemented interfaces

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToUnitString(AngularFrequencyUnit.RadianPerSecond, format, formatProvider);

    #region ISiUnitValueQuantifiable<>

    public static string GetSiUnitName(MetricPrefix prefix, bool preferPlural) => prefix.GetMetricPrefixName() + GetUnitName(AngularFrequencyUnit.RadianPerSecond, preferPlural);

    public static string GetSiUnitSymbol(MetricPrefix prefix, bool preferUnicode) => prefix.GetMetricPrefixSymbol(preferUnicode) + GetUnitSymbol(AngularFrequencyUnit.RadianPerSecond, preferUnicode);

    public System.Runtime.Intrinsics.Vector256<double> GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertTo(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, bool fullName = false)
      => GetSiUnitValue(prefix).ToString() + UnicodeSpacing.ThinSpace.ToSpacingString() + (fullName ? GetSiUnitName(prefix, true) : GetSiUnitSymbol(prefix, false));

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    private static System.Runtime.Intrinsics.Vector256<double> ConvertFromUnit(AngularFrequencyUnit unit, System.Runtime.Intrinsics.Vector256<double> value)
      => unit switch
      {
        AngularFrequencyUnit.RadianPerSecond => value,

        _ => GetUnitFactor(unit) * value,
      };

    private static System.Runtime.Intrinsics.Vector256<double> ConvertToUnit(AngularFrequencyUnit unit, System.Runtime.Intrinsics.Vector256<double> value)
      => unit switch
      {
        AngularFrequencyUnit.RadianPerSecond => value,

        _ => value / GetUnitFactor(unit),
      };

    public static System.Runtime.Intrinsics.Vector256<double> ConvertUnit(System.Runtime.Intrinsics.Vector256<double> value, AngularFrequencyUnit from, AngularFrequencyUnit to)
      => ConvertToUnit(to, ConvertFromUnit(from, value));

    public static System.Runtime.Intrinsics.Vector256<double> GetUnitFactor(AngularFrequencyUnit unit) => System.Runtime.Intrinsics.Vector256.Create(AngularFrequency.GetUnitFactor(unit));

    public static string GetUnitName(AngularFrequencyUnit unit, bool preferPlural) => unit.ToString().ConvertUnitNameToPlural(preferPlural);

    public static string GetUnitSymbol(AngularFrequencyUnit unit, bool preferUnicode) => AngularFrequency.GetUnitSymbol(unit, preferUnicode);

    public System.Runtime.Intrinsics.Vector256<double> GetUnitValue(AngularFrequencyUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(AngularFrequencyUnit unit = AngularFrequencyUnit.RadianPerSecond, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
      => GetUnitValue(unit).ToString() + UnicodeSpacing.Space.ToSpacingString() + (fullName ? GetUnitName(unit, true) : GetUnitSymbol(unit, false));

    #endregion // IUnitValueQuantifiable<>

    #region IValueQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="AngularVelocityV.Value"/> property is in <see cref="AngularFrequencyUnit.RadianPerSecondSquared"/>.</para>
    /// </summary>
    public System.Runtime.Intrinsics.Vector256<double> Value => m_value;

    #endregion // IValueQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
