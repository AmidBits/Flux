namespace Flux.Units
{
  /// <summary>Angular velocity, a vector quantity, unit of radians per second, of which the magnitude represents the angular frequency, the angular rate at which the object rotates (spins or revolves). This is an SI derived quantity.</summary>
  /// <see href="https://en.wikipedia.org/wiki/Angular_velocity"/>
  public readonly record struct AngularVelocityVector
    : System.IFormattable, ISiUnitValueQuantifiable<System.Runtime.Intrinsics.Vector256<double>, AngularFrequencyUnit>
  {
    private readonly System.Runtime.Intrinsics.Vector256<double> m_value;

    public AngularVelocityVector(System.Runtime.Intrinsics.Vector256<double> value, AngularFrequencyUnit unit = AngularFrequencyUnit.RadianPerSecond) => m_value = ConvertFromUnit(unit, value);

    public AngularVelocityVector(double valueX, double valueY, double valueZ, double valueW, AngularFrequencyUnit unit = AngularFrequencyUnit.RadianPerSecond)
      : this(System.Runtime.Intrinsics.Vector256.Create(valueX, valueY, valueZ, valueW), unit) { }

    public AngularVelocityVector(MetricPrefix prefix, System.Runtime.Intrinsics.Vector256<double> RadianPerSecondSquare) => m_value = prefix.ConvertPrefix(RadianPerSecondSquare, MetricPrefix.Unprefixed);

    public AngularVelocityVector(MetricPrefix prefix, double RadianPerSecondSquareX, double RadianPerSecondSquareY, double RadianPerSecondSquareZ, double RadianPerSecondSquareW)
      : this(prefix, System.Runtime.Intrinsics.Vector256.Create(RadianPerSecondSquareX, RadianPerSecondSquareY, RadianPerSecondSquareZ, RadianPerSecondSquareW)) { }

    public double X => m_value[0];
    public double Y => m_value[1];
    public double Z => m_value[2];
    public double W => m_value[3];

    #region Overloaded operators

    public static AngularVelocityVector operator -(AngularVelocityVector v) => new(v.m_value.Negate());
    public static AngularVelocityVector operator +(AngularVelocityVector a, double b) => new(a.m_value.Add(b));
    public static AngularVelocityVector operator +(AngularVelocityVector a, AngularVelocityVector b) => new(a.m_value.Add(b.m_value));
    public static AngularVelocityVector operator /(AngularVelocityVector a, double b) => new(a.m_value.Divide(b));
    public static AngularVelocityVector operator /(AngularVelocityVector a, AngularVelocityVector b) => new(a.m_value.Divide(b.m_value));
    public static AngularVelocityVector operator *(AngularVelocityVector a, double b) => new(a.m_value.Multiply(b));
    public static AngularVelocityVector operator *(AngularVelocityVector a, AngularVelocityVector b) => new(a.m_value.Multiply(b.m_value));
    public static AngularVelocityVector operator %(AngularVelocityVector a, double b) => new(a.m_value.Remainder(b));
    public static AngularVelocityVector operator %(AngularVelocityVector a, AngularVelocityVector b) => new(a.m_value.Remainder(b.m_value));
    public static AngularVelocityVector operator -(AngularVelocityVector a, double b) => new(a.m_value.Subtract(b));
    public static AngularVelocityVector operator -(AngularVelocityVector a, AngularVelocityVector b) => new(a.m_value.Subtract(b.m_value));

    #endregion Overloaded operators

    #region Implemented interfaces

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToUnitString(AngularFrequencyUnit.RadianPerSecond, format, formatProvider);

    #region ISiUnitValueQuantifiable<>

    public System.Runtime.Intrinsics.Vector256<double> GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertPrefix(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, string? format = null, System.IFormatProvider? formatProvider = null)
      => GetSiUnitValue(prefix).ToString() + UnicodeSpacing.ThinSpace.ToSpacingString() + prefix.GetMetricPrefixSymbol() + AngularFrequencyUnit.RadianPerSecond.GetUnitSymbol();

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    public static System.Runtime.Intrinsics.Vector256<double> ConvertFromUnit(AngularFrequencyUnit unit, System.Runtime.Intrinsics.Vector256<double> value)
      => unit switch
      {
        AngularFrequencyUnit.RadianPerSecond => value,

        _ => unit.GetUnitFactor() * value,
      };

    public static System.Runtime.Intrinsics.Vector256<double> ConvertToUnit(AngularFrequencyUnit unit, System.Runtime.Intrinsics.Vector256<double> value)
      => unit switch
      {
        AngularFrequencyUnit.RadianPerSecond => value,

        _ => value / unit.GetUnitFactor(),
      };

    public static System.Runtime.Intrinsics.Vector256<double> ConvertUnit(System.Runtime.Intrinsics.Vector256<double> value, AngularFrequencyUnit from, AngularFrequencyUnit to)
      => ConvertToUnit(to, ConvertFromUnit(from, value));

    public System.Runtime.Intrinsics.Vector256<double> GetUnitValue(AngularFrequencyUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(AngularFrequencyUnit unit = AngularFrequencyUnit.RadianPerSecond, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing spacing = UnicodeSpacing.Space, bool fullName = false)
    {
      var value = GetUnitValue(unit);

      return value.ToString() + spacing.ToSpacingString() + (fullName ? unit.GetUnitName(true) : unit.GetUnitSymbol(false));
    }

    #endregion // IUnitValueQuantifiable<>

    #region IValueQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="AngularVelocityVector.Value"/> property is in <see cref="AngularFrequencyUnit.RadianPerSecondSquared"/>.</para>
    /// </summary>
    public System.Runtime.Intrinsics.Vector256<double> Value => m_value;

    #endregion // IValueQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
