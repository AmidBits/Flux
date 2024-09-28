namespace Flux.Quantities
{
  public enum AccelerationUnit
  {
    /// <summary>This is the default unit for <see cref="Acceleration"/>.</summary>
    MeterPerSecondSquared,
  }

  /// <summary>Acceleration, a scalar quantity, unit of meters per second square. This is an SI derived quantity.</summary>
  /// <see href="https://en.wikipedia.org/wiki/Acceleration"/>
  public readonly record struct Acceleration
    : System.IComparable, System.IComparable<Acceleration>, System.IFormattable, IUnitValueQuantifiable<double, AccelerationUnit>
  {
    /// <summary>
    /// <para>The approximate acceleration due to gravity on the surface of the Moon.</para>
    /// </summary>
    public static Acceleration MoonGravity => new(1.625);

    /// <summary>
    /// <para>The nominal gravitational acceleration of an object in a vacuum near the surface of the Earth.</para>
    /// </summary>
    public static Acceleration StandardGravity => new(9.80665);

    private readonly double m_value;

    public Acceleration(double value, AccelerationUnit unit = AccelerationUnit.MeterPerSecondSquared) => m_value = ConvertFromUnit(unit, value);

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
    public static bool operator <=(Acceleration a, Acceleration b) => a.CompareTo(b) <= 0;
    public static bool operator >(Acceleration a, Acceleration b) => a.CompareTo(b) > 0;
    public static bool operator >=(Acceleration a, Acceleration b) => a.CompareTo(b) >= 0;

    public static Acceleration operator -(Acceleration v) => new(-v.m_value);
    public static Acceleration operator +(Acceleration a, double b) => new(a.m_value + b);
    public static Acceleration operator +(Acceleration a, Acceleration b) => a + b.m_value;
    public static Acceleration operator /(Acceleration a, double b) => new(a.m_value / b);
    public static Acceleration operator /(Acceleration a, Acceleration b) => a / b.m_value;
    public static Acceleration operator *(Acceleration a, double b) => new(a.m_value * b);
    public static Acceleration operator *(Acceleration a, Acceleration b) => a * b.m_value;
    public static Acceleration operator %(Acceleration a, double b) => new(a.m_value % b);
    public static Acceleration operator %(Acceleration a, Acceleration b) => a % b.m_value;
    public static Acceleration operator -(Acceleration a, double b) => new(a.m_value - b);
    public static Acceleration operator -(Acceleration a, Acceleration b) => a - b.m_value;

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is Acceleration o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(Acceleration other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToUnitString(AccelerationUnit.MeterPerSecondSquared, format, formatProvider);

    #region IQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="Acceleration.Value"/> property is in <see cref="AccelerationUnit.MeterPerSecondSquared"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IQuantifiable<>

    #region IUnitQuantifiable<>

    public static double ConvertFromUnit(AccelerationUnit unit, double value)
      => unit switch
      {
        AccelerationUnit.MeterPerSecondSquared => value,

        _ => GetUnitFactor(unit) * value,
      };

    public static double ConvertToUnit(AccelerationUnit unit, double value)
      => unit switch
      {
        AccelerationUnit.MeterPerSecondSquared => value,

        _ => value / GetUnitFactor(unit),
      };

    public static double GetUnitFactor(AccelerationUnit unit)
      => unit switch
      {
        AccelerationUnit.MeterPerSecondSquared => 1,

        _ => throw new System.NotImplementedException()
      };

    public string GetUnitName(AccelerationUnit unit, bool preferPlural)
      => unit.ToString().ConvertUnitNameToPlural(preferPlural && GetUnitValue(unit).IsConsideredPlural());

    public string GetUnitSymbol(AccelerationUnit unit, bool preferUnicode)
      => unit switch
      {
        Quantities.AccelerationUnit.MeterPerSecondSquared => preferUnicode ? "\u33A8" : "m/s²",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(AccelerationUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(AccelerationUnit unit = AccelerationUnit.MeterPerSecondSquared, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + UnicodeSpacing.Space.ToSpacingString() + (fullName ? GetUnitName(unit, true) : GetUnitSymbol(unit, false));

    //public string ToUnitValueNameString(AccelerationUnit unit = AccelerationUnit.MeterPerSecondSquared, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferPlural = false)
    //  => GetUnitValue(unit).ToString(format, formatProvider) + unitSpacing.ToSpacingString() + GetUnitName(unit, preferPlural);

    //public string ToUnitValueSymbolString(AccelerationUnit unit = AccelerationUnit.MeterPerSecondSquared, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferUnicode = false)
    //  => GetUnitValue(unit).ToString(format, formatProvider) + unitSpacing.ToSpacingString() + GetUnitSymbol(unit, preferUnicode);

    #endregion // IUnitQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
