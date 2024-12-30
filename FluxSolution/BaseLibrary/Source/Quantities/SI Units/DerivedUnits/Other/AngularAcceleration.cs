namespace Flux.Quantities
{
  public enum AngularAccelerationUnit
  {
    /// <summary>This is the default unit for <see cref="AngularAcceleration"/>.</summary>
    RadianPerSecondSquared,
  }

  /// <summary>Angular acceleration, unit of radians per second square. This is an SI derived quantity.</summary>
  /// <see href="https://en.wikipedia.org/wiki/Angular_acceleration"/>
  public readonly record struct AngularAcceleration
    : System.IComparable, System.IComparable<AngularAcceleration>, System.IFormattable, ISiUnitValueQuantifiable<double, AngularAccelerationUnit>
  {
    private readonly double m_value;

    public AngularAcceleration(double value, AngularAccelerationUnit unit = AngularAccelerationUnit.RadianPerSecondSquared) => m_value = ConvertFromUnit(unit, value);

    public AngularAcceleration(MetricPrefix prefix, double radianPerSecondSquared) => m_value = prefix.ConvertTo(radianPerSecondSquared, MetricPrefix.Unprefixed);

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

    public static string GetSiUnitName(MetricPrefix prefix, bool preferPlural) => prefix.GetMetricPrefixName() + GetUnitName(AngularAccelerationUnit.RadianPerSecondSquared, preferPlural);

    public static string GetSiUnitSymbol(MetricPrefix prefix, bool preferUnicode) => prefix.GetMetricPrefixSymbol(preferUnicode) + GetUnitSymbol(AngularAccelerationUnit.RadianPerSecondSquared, preferUnicode);

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertTo(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, bool fullName = false)
      => GetSiUnitValue(prefix).ToSiFormattedString() + UnicodeSpacing.ThinSpace.ToSpacingString() + (fullName ? GetSiUnitName(prefix, GetSiUnitValue(prefix).IsConsideredPlural()) : GetSiUnitSymbol(prefix, false));

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    private static double ConvertFromUnit(AngularAccelerationUnit unit, double value)
      => unit switch
      {
        AngularAccelerationUnit.RadianPerSecondSquared => value,

        _ => GetUnitFactor(unit) * value,
      };

    private static double ConvertToUnit(AngularAccelerationUnit unit, double value)
      => unit switch
      {
        AngularAccelerationUnit.RadianPerSecondSquared => value,

        _ => value / GetUnitFactor(unit),
      };

    public static double ConvertUnit(double value, AngularAccelerationUnit from, AngularAccelerationUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public static double GetUnitFactor(AngularAccelerationUnit unit)
      => unit switch
      {
        AngularAccelerationUnit.RadianPerSecondSquared => 1,

        _ => throw new System.NotImplementedException()
      };

    public static string GetUnitName(AngularAccelerationUnit unit, bool preferPlural) => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(AngularAccelerationUnit unit, bool preferUnicode)
      => unit switch
      {
        AngularAccelerationUnit.RadianPerSecondSquared => preferUnicode ? "\u33AF" : "rad/s�",

        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(AngularAccelerationUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(AngularAccelerationUnit unit = AngularAccelerationUnit.RadianPerSecondSquared, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + UnicodeSpacing.Space.ToSpacingString() + (fullName ? GetUnitName(unit, GetUnitValue(unit).IsConsideredPlural()) : GetUnitSymbol(unit, false));

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
