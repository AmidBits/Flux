namespace Flux.Units
{
  /// <summary>Acceleration, a scalar quantity, unit of meters per second square. This is an SI derived quantity.</summary>
  /// <see href="https://en.wikipedia.org/wiki/Acceleration"/>
  public readonly record struct Acceleration
    : System.IComparable, System.IComparable<Acceleration>, System.IFormattable, ISiUnitValueQuantifiable<double, AccelerationUnit>
  {
    /// <summary>
    /// <para>The approximate acceleration due to gravity on the surface of the Moon.</para>
    /// </summary>
    public const double MoonGravity = 1.625;

    /// <summary>
    /// <para>The standard acceleration for gravity for Earth as defined per CODATA 2018 (exact value). I.e. a nominal gravitational acceleration of an object in a vacuum near the surface of the Earth.</para>
    /// </summary>
    public const double StandardGravity = 9.80665;

    ///// <summary>
    ///// <para>The approximate acceleration due to gravity on the surface of the Moon.</para>
    ///// </summary>
    //public static Acceleration MoonGravity { get; } = new(1.625);

    ///// <summary>
    ///// <para>The nominal gravitational acceleration of an object in a vacuum near the surface of the Earth.</para>
    ///// </summary>
    //public static Acceleration StandardGravity { get; } = new(9.80665);

    private readonly double m_value;

    public Acceleration(double value, AccelerationUnit unit = AccelerationUnit.MeterPerSecondSquared) => m_value = ConvertFromUnit(unit, value);

    public Acceleration(MetricPrefix prefix, double meterPerSecondSquare) => m_value = prefix.ConvertPrefix(meterPerSecondSquare, MetricPrefix.Unprefixed);

    public Acceleration(Force force, Mass mass) => m_value = force.Value / mass.Value;

    /// <summary>
    /// <para>Creates a new acceleration from the length (magnitude) of <paramref name="vector"/> and <paramref name="unit"/>.</para>
    /// </summary>
    /// <param name="vector"></param>
    /// <param name="unit"></param>
    public Acceleration(System.Numerics.Vector2 vector, AccelerationUnit unit = AccelerationUnit.MeterPerSecondSquared) : this(vector.Length(), unit) { }

    /// <summary>
    /// <para>Creates a new acceleration from the length (magnitude) of <paramref name="vector"/> and <paramref name="unit"/>.</para>
    /// </summary>
    /// <param name="vector"></param>
    /// <param name="unit"></param>
    public Acceleration(System.Numerics.Vector3 vector, AccelerationUnit unit = AccelerationUnit.MeterPerSecondSquared) : this(vector.Length(), unit) { }

    #region Overloaded operators

    public static bool operator <(Acceleration a, Acceleration b) => a.CompareTo(b) < 0;
    public static bool operator >(Acceleration a, Acceleration b) => a.CompareTo(b) > 0;
    public static bool operator <=(Acceleration a, Acceleration b) => a.CompareTo(b) <= 0;
    public static bool operator >=(Acceleration a, Acceleration b) => a.CompareTo(b) >= 0;

    public static Acceleration operator -(Acceleration v) => new(-v.m_value);
    public static Acceleration operator *(Acceleration a, Acceleration b) => new(a.m_value * b.m_value);
    public static Acceleration operator /(Acceleration a, Acceleration b) => new(a.m_value / b.m_value);
    public static Acceleration operator %(Acceleration a, Acceleration b) => new(a.m_value % b.m_value);
    public static Acceleration operator +(Acceleration a, Acceleration b) => new(a.m_value + b.m_value);
    public static Acceleration operator -(Acceleration a, Acceleration b) => new(a.m_value - b.m_value);
    public static Acceleration operator *(Acceleration a, double b) => new(a.m_value * b);
    public static Acceleration operator /(Acceleration a, double b) => new(a.m_value / b);
    public static Acceleration operator %(Acceleration a, double b) => new(a.m_value % b);
    public static Acceleration operator +(Acceleration a, double b) => new(a.m_value + b);
    public static Acceleration operator -(Acceleration a, double b) => new(a.m_value - b);

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is Acceleration o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(Acceleration other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToUnitString(AccelerationUnit.MeterPerSecondSquared, format, formatProvider);

    #region ISiUnitValueQuantifiable<>

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertPrefix(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, string? format = null, System.IFormatProvider? formatProvider = null)
      => GetSiUnitValue(prefix).ToSiFormattedString(format, formatProvider) + UnicodeSpacing.ThinSpace.ToSpacingString() + prefix.GetMetricPrefixSymbol() + AccelerationUnit.MeterPerSecondSquared.GetUnitSymbol();

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    public static double ConvertFromUnit(AccelerationUnit unit, double value)
      => unit switch
      {
        AccelerationUnit.MeterPerSecondSquared => value,

        _ => unit.GetUnitFactor() * value,
      };

    public static double ConvertToUnit(AccelerationUnit unit, double value)
      => unit switch
      {
        AccelerationUnit.MeterPerSecondSquared => value,

        _ => value / unit.GetUnitFactor(),
      };

    public static double ConvertUnit(double value, AccelerationUnit from, AccelerationUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public double GetUnitValue(AccelerationUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(AccelerationUnit unit = AccelerationUnit.MeterPerSecondSquared, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing spacing = UnicodeSpacing.Space, bool fullName = false)
    {
      var value = GetUnitValue(unit);

      return value.ToString(format, formatProvider) + spacing.ToSpacingString() + (fullName ? unit.GetUnitName(value.IsConsideredPlural()) : unit.GetUnitSymbol(false));
    }

    #endregion // IUnitValueQuantifiable<>

    #region IValueQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="Acceleration.Value"/> property is in <see cref="AccelerationUnit.MeterPerSecondSquared"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IValueQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
