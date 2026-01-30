namespace Flux.Units
{
  /// <summary>Angular acceleration, unit of radians per second square. This is an SI derived quantity.</summary>
  /// <see href="https://en.wikipedia.org/wiki/Angular_acceleration"/>
  public readonly record struct AngularAcceleration
    : System.IComparable, System.IComparable<AngularAcceleration>, System.IFormattable, ISiUnitValueQuantifiable<double, AngularAccelerationUnit>
  {
    private readonly double m_value;

    public AngularAcceleration(double value, AngularAccelerationUnit unit = AngularAccelerationUnit.RadianPerSecondSquared) => m_value = ConvertFromUnit(unit, value);

    public AngularAcceleration(MetricPrefix prefix, double radianPerSecondSquared) => m_value = prefix.ConvertPrefix(radianPerSecondSquared, MetricPrefix.Unprefixed);

    /// <summary>
    /// <para>Creates a new angular acceleration from the length (magnitude) of <paramref name="vector"/> and <paramref name="unit"/>.</para>
    /// </summary>
    /// <param name="vector"></param>
    /// <param name="unit"></param>
    public AngularAcceleration(System.Numerics.Vector2 vector, AngularAccelerationUnit unit = AngularAccelerationUnit.RadianPerSecondSquared) : this(vector.Length(), unit) { }

    /// <summary>
    /// <para>Creates a new angular acceleration from the length (magnitude) of <paramref name="vector"/> and <paramref name="unit"/>.</para>
    /// </summary>
    /// <param name="vector"></param>
    /// <param name="unit"></param>
    public AngularAcceleration(System.Numerics.Vector3 vector, AngularAccelerationUnit unit = AngularAccelerationUnit.RadianPerSecondSquared) : this(vector.Length(), unit) { }

    #region Overloaded operators

    public static bool operator <(AngularAcceleration a, AngularAcceleration b) => a.CompareTo(b) < 0;
    public static bool operator >(AngularAcceleration a, AngularAcceleration b) => a.CompareTo(b) > 0;
    public static bool operator <=(AngularAcceleration a, AngularAcceleration b) => a.CompareTo(b) <= 0;
    public static bool operator >=(AngularAcceleration a, AngularAcceleration b) => a.CompareTo(b) >= 0;

    public static AngularAcceleration operator -(AngularAcceleration v) => new(-v.m_value);
    public static AngularAcceleration operator *(AngularAcceleration a, AngularAcceleration b) => new(a.m_value * b.m_value);
    public static AngularAcceleration operator /(AngularAcceleration a, AngularAcceleration b) => new(a.m_value / b.m_value);
    public static AngularAcceleration operator %(AngularAcceleration a, AngularAcceleration b) => new(a.m_value % b.m_value);
    public static AngularAcceleration operator +(AngularAcceleration a, AngularAcceleration b) => new(a.m_value + b.m_value);
    public static AngularAcceleration operator -(AngularAcceleration a, AngularAcceleration b) => new(a.m_value - b.m_value);
    public static AngularAcceleration operator *(AngularAcceleration a, double b) => new(a.m_value * b);
    public static AngularAcceleration operator /(AngularAcceleration a, double b) => new(a.m_value / b);
    public static AngularAcceleration operator %(AngularAcceleration a, double b) => new(a.m_value % b);
    public static AngularAcceleration operator +(AngularAcceleration a, double b) => new(a.m_value + b);
    public static AngularAcceleration operator -(AngularAcceleration a, double b) => new(a.m_value - b);

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is AngularAcceleration o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(AngularAcceleration other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToUnitString(AngularAccelerationUnit.RadianPerSecondSquared, format, formatProvider);

    #region ISiUnitValueQuantifiable<>

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertPrefix(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, string? format = null, System.IFormatProvider? formatProvider = null)
      => GetSiUnitValue(prefix).ToSiFormattedString(format, formatProvider) + UnicodeSpacing.ThinSpace.ToSpacingString() + prefix.GetMetricPrefixSymbol() + AngularAccelerationUnit.RadianPerSecondSquared.GetUnitSymbol();

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    public static double ConvertFromUnit(AngularAccelerationUnit unit, double value)
      => unit switch
      {
        AngularAccelerationUnit.RadianPerSecondSquared => value,

        _ => unit.GetUnitFactor() * value,
      };

    public static double ConvertToUnit(AngularAccelerationUnit unit, double value)
      => unit switch
      {
        AngularAccelerationUnit.RadianPerSecondSquared => value,

        _ => value / unit.GetUnitFactor(),
      };

    public static double ConvertUnit(double value, AngularAccelerationUnit from, AngularAccelerationUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public double GetUnitValue(AngularAccelerationUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(AngularAccelerationUnit unit = AngularAccelerationUnit.RadianPerSecondSquared, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing spacing = UnicodeSpacing.Space, bool fullName = false)
    {
      var value = GetUnitValue(unit);

      return value.ToString(format, formatProvider) + spacing.ToSpacingString() + (fullName ? unit.GetUnitName(INumber.IsConsideredPlural(value)) : unit.GetUnitSymbol(false));
    }

    #endregion // IUnitValueQuantifiable<>

    #region IValueQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="AngularAcceleration.Value"/> property is in <see cref="AngularAccelerationUnit.RadianPerSecondSquared"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IValueQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
