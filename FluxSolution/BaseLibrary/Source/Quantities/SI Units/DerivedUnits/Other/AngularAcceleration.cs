namespace Flux.Quantities
{
  public enum AngularAccelerationUnit
  {
    /// <summary>This is the default unit for <see cref="AngularAcceleration"/>.</summary>
    RadianPerSecondSquared,
  }

  /// <summary>Angular, acceleration unit of radians per second square. This is an SI derived quantity.</summary>
  /// <see href="https://en.wikipedia.org/wiki/Angular_acceleration"/>
  public readonly record struct AngularAcceleration
    : System.IComparable, System.IComparable<AngularAcceleration>, System.IFormattable, IUnitValueQuantifiable<double, AngularAccelerationUnit>
  {
    private readonly double m_value;

    public AngularAcceleration(double value, AngularAccelerationUnit unit = AngularAccelerationUnit.RadianPerSecondSquared)
      => m_value = unit switch
      {
        AngularAccelerationUnit.RadianPerSecondSquared => value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

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
    public static bool operator <=(AngularAcceleration a, AngularAcceleration b) => a.CompareTo(b) <= 0;
    public static bool operator >(AngularAcceleration a, AngularAcceleration b) => a.CompareTo(b) > 0;
    public static bool operator >=(AngularAcceleration a, AngularAcceleration b) => a.CompareTo(b) >= 0;

    public static AngularAcceleration operator -(AngularAcceleration v) => new(-v.m_value);
    public static AngularAcceleration operator +(AngularAcceleration a, double b) => new(a.m_value + b);
    public static AngularAcceleration operator +(AngularAcceleration a, AngularAcceleration b) => a + b.m_value;
    public static AngularAcceleration operator /(AngularAcceleration a, double b) => new(a.m_value / b);
    public static AngularAcceleration operator /(AngularAcceleration a, AngularAcceleration b) => a / b.m_value;
    public static AngularAcceleration operator *(AngularAcceleration a, double b) => new(a.m_value * b);
    public static AngularAcceleration operator *(AngularAcceleration a, AngularAcceleration b) => a * b.m_value;
    public static AngularAcceleration operator %(AngularAcceleration a, double b) => new(a.m_value % b);
    public static AngularAcceleration operator %(AngularAcceleration a, AngularAcceleration b) => a % b.m_value;
    public static AngularAcceleration operator -(AngularAcceleration a, double b) => new(a.m_value - b);
    public static AngularAcceleration operator -(AngularAcceleration a, AngularAcceleration b) => a - b.m_value;

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is AngularAcceleration o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(AngularAcceleration other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider)
      => ToUnitValueSymbolString(AngularAccelerationUnit.RadianPerSecondSquared, format, formatProvider);

    // IQuantifiable<>
    /// <summary>
    /// <para>The unit of the <see cref="AngularAcceleration.Value"/> property is in <see cref="AngularAccelerationUnit.RadianPerSecondSquared"/>.</para>
    /// </summary>
    public double Value => m_value;

    // IUnitQuantifiable<>
    public string GetUnitName(AngularAccelerationUnit unit, bool preferPlural)
      => unit.ToString() is var us && preferPlural ? us + GetUnitValue(unit).PluralStringSuffix() : us;

    public string GetUnitSymbol(AngularAccelerationUnit unit, bool preferUnicode)
      => unit switch
      {
        Quantities.AngularAccelerationUnit.RadianPerSecondSquared => preferUnicode ? "\u33AF" : "rad/s²",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(AngularAccelerationUnit unit)
      => unit switch
      {
        AngularAccelerationUnit.RadianPerSecondSquared => m_value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public string ToUnitValueNameString(AngularAccelerationUnit unit = AngularAccelerationUnit.RadianPerSecondSquared, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferPlural = false)
      => GetUnitValue(unit).ToString(format, formatProvider)+ unitSpacing.ToSpacingString()+ GetUnitName(unit, preferPlural);

    public string ToUnitValueSymbolString(AngularAccelerationUnit unit = AngularAccelerationUnit.RadianPerSecondSquared, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferUnicode = false)
      => GetUnitValue(unit).ToString(format, formatProvider)+ unitSpacing.ToSpacingString()+ GetUnitSymbol(unit, preferUnicode);

    #endregion Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
