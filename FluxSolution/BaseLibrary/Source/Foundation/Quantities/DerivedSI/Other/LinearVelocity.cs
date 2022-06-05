namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static string GetUnitString(this LinearVelocityUnit unit, bool useFullName = false, bool preferUnicode = false)
      => useFullName ? unit.ToString() : unit switch
      {
        LinearVelocityUnit.FootPerSecond => "ft/s",
        LinearVelocityUnit.KilometerPerHour => "km/h",
        LinearVelocityUnit.Knot => preferUnicode ? "\u33CF" : "knot",
        LinearVelocityUnit.MeterPerSecond => preferUnicode ? "\u33A7" : "m/s",
        LinearVelocityUnit.MilePerHour => "mph",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  public enum LinearVelocityUnit
  {
    MeterPerSecond, // DefaultUnit first for actual instatiation defaults.
    FootPerSecond,
    KilometerPerHour,
    Knot,
    MilePerHour,
  }

  /// <summary>Speed (a.k.a. velocity) unit of meters per second.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Speed"/>
  public readonly struct LinearVelocity
    : System.IComparable, System.IComparable<LinearVelocity>, System.IConvertible, System.IEquatable<LinearVelocity>, System.IFormattable, IUnitQuantifiable<double, LinearVelocityUnit>
  {
    public const LinearVelocityUnit DefaultUnit = LinearVelocityUnit.MeterPerSecond;

    [System.Diagnostics.Contracts.Pure] public static LinearVelocity SpeedOfLightInVacuum => new(299792458);
    [System.Diagnostics.Contracts.Pure] public static LinearVelocity ApproximateSpeedOfSoundInAir => new(343);
    [System.Diagnostics.Contracts.Pure] public static LinearVelocity ApproximateSpeedOfSoundInDiamond => new(12000);
    [System.Diagnostics.Contracts.Pure] public static LinearVelocity ApproximateSpeedOfSoundInIron => new(5120);
    [System.Diagnostics.Contracts.Pure] public static LinearVelocity ApproximateSpeedOfSoundInWater => new(1481);

    private readonly double m_value;

    public LinearVelocity(double value, LinearVelocityUnit unit = DefaultUnit)
      => m_value = unit switch
      {
        LinearVelocityUnit.FootPerSecond => value * (381.0 / 1250.0),
        LinearVelocityUnit.KilometerPerHour => value * (5.0 / 18.0),
        LinearVelocityUnit.Knot => value * (1852.0 / 3600.0),
        LinearVelocityUnit.MeterPerSecond => value,
        LinearVelocityUnit.MilePerHour => value * (1397.0 / 3125.0),
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    #region Static methods
    /// <summary>Create a new Speed instance representing phase velocity from the specified frequency and wavelength.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Phase_velocity"/>
    /// <param name="frequency"></param>
    /// <param name="wavelength"></param>
    [System.Diagnostics.Contracts.Pure]
    public static LinearVelocity ComputePhaseVelocity(Frequency frequency, Length wavelength)
      => new(frequency.Value * wavelength.Value);

    /// <summary>Creates a new Speed instance from the specified length and time.</summary>
    /// <param name="length"></param>
    /// <param name="time"></param>
    [System.Diagnostics.Contracts.Pure]
    public static LinearVelocity From(Length length, Time time)
      => new(length.Value / time.Value);

    /// <summary>Creates a new <see cref="LinearVelocity">tangential/linear speed</see> instance from the specified <see cref="AngularVelocity"/> and <see cref="Length">Radius</see>.</summary>
    /// <param name="angularVelocity"></param>
    /// <param name="radius"></param>
    [System.Diagnostics.Contracts.Pure]
    public static LinearVelocity From(AngularVelocity angularVelocity, Length radius)
      => new(angularVelocity.Value * radius.Value);
    #endregion Static methods

    #region Overloaded operators
    [System.Diagnostics.Contracts.Pure] public static explicit operator double(LinearVelocity v) => v.m_value;
    [System.Diagnostics.Contracts.Pure] public static explicit operator LinearVelocity(double v) => new(v);

    [System.Diagnostics.Contracts.Pure] public static bool operator <(LinearVelocity a, LinearVelocity b) => a.CompareTo(b) < 0;
    [System.Diagnostics.Contracts.Pure] public static bool operator <=(LinearVelocity a, LinearVelocity b) => a.CompareTo(b) <= 0;
    [System.Diagnostics.Contracts.Pure] public static bool operator >(LinearVelocity a, LinearVelocity b) => a.CompareTo(b) > 0;
    [System.Diagnostics.Contracts.Pure] public static bool operator >=(LinearVelocity a, LinearVelocity b) => a.CompareTo(b) >= 0;

    [System.Diagnostics.Contracts.Pure] public static bool operator ==(LinearVelocity a, LinearVelocity b) => a.Equals(b);
    [System.Diagnostics.Contracts.Pure] public static bool operator !=(LinearVelocity a, LinearVelocity b) => !a.Equals(b);

    [System.Diagnostics.Contracts.Pure] public static LinearVelocity operator -(LinearVelocity v) => new(-v.m_value);
    [System.Diagnostics.Contracts.Pure] public static LinearVelocity operator +(LinearVelocity a, double b) => new(a.m_value + b);
    [System.Diagnostics.Contracts.Pure] public static LinearVelocity operator +(LinearVelocity a, LinearVelocity b) => a + b.m_value;
    [System.Diagnostics.Contracts.Pure] public static LinearVelocity operator /(LinearVelocity a, double b) => new(a.m_value / b);
    [System.Diagnostics.Contracts.Pure] public static LinearVelocity operator /(LinearVelocity a, LinearVelocity b) => a / b.m_value;
    [System.Diagnostics.Contracts.Pure] public static LinearVelocity operator *(LinearVelocity a, double b) => new(a.m_value * b);
    [System.Diagnostics.Contracts.Pure] public static LinearVelocity operator *(LinearVelocity a, LinearVelocity b) => a * b.m_value;
    [System.Diagnostics.Contracts.Pure] public static LinearVelocity operator %(LinearVelocity a, double b) => new(a.m_value % b);
    [System.Diagnostics.Contracts.Pure] public static LinearVelocity operator %(LinearVelocity a, LinearVelocity b) => a % b.m_value;
    [System.Diagnostics.Contracts.Pure] public static LinearVelocity operator -(LinearVelocity a, double b) => new(a.m_value - b);
    [System.Diagnostics.Contracts.Pure] public static LinearVelocity operator -(LinearVelocity a, LinearVelocity b) => a - b.m_value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable<>
    [System.Diagnostics.Contracts.Pure] public int CompareTo(LinearVelocity other) => m_value.CompareTo(other.m_value);
    // IComparable
    [System.Diagnostics.Contracts.Pure] public int CompareTo(object? other) => other is not null && other is LinearVelocity o ? CompareTo(o) : -1;

    #region IConvertible
    [System.Diagnostics.Contracts.Pure] public System.TypeCode GetTypeCode() => System.TypeCode.Object;
    [System.Diagnostics.Contracts.Pure] public bool ToBoolean(System.IFormatProvider? provider) => m_value != 0;
    [System.Diagnostics.Contracts.Pure] public byte ToByte(System.IFormatProvider? provider) => System.Convert.ToByte(m_value);
    [System.Diagnostics.Contracts.Pure] public char ToChar(System.IFormatProvider? provider) => System.Convert.ToChar(m_value);
    [System.Diagnostics.Contracts.Pure] public System.DateTime ToDateTime(System.IFormatProvider? provider) => System.Convert.ToDateTime(m_value);
    [System.Diagnostics.Contracts.Pure] public decimal ToDecimal(System.IFormatProvider? provider) => System.Convert.ToDecimal(m_value);
    [System.Diagnostics.Contracts.Pure] public double ToDouble(System.IFormatProvider? provider) => System.Convert.ToDouble(m_value);
    [System.Diagnostics.Contracts.Pure] public short ToInt16(System.IFormatProvider? provider) => System.Convert.ToInt16(m_value);
    [System.Diagnostics.Contracts.Pure] public int ToInt32(System.IFormatProvider? provider) => System.Convert.ToInt32(m_value);
    [System.Diagnostics.Contracts.Pure] public long ToInt64(System.IFormatProvider? provider) => System.Convert.ToInt64(m_value);
    [System.CLSCompliant(false)][System.Diagnostics.Contracts.Pure] public sbyte ToSByte(System.IFormatProvider? provider) => System.Convert.ToSByte(m_value);
    [System.Diagnostics.Contracts.Pure] public float ToSingle(System.IFormatProvider? provider) => System.Convert.ToSingle(m_value);
    [System.Diagnostics.Contracts.Pure] public string ToString(System.IFormatProvider? provider) => string.Format(provider, "{0}", m_value);
    [System.Diagnostics.Contracts.Pure] public object ToType(System.Type conversionType, System.IFormatProvider? provider) => System.Convert.ChangeType(m_value, conversionType, provider);
    [System.CLSCompliant(false)][System.Diagnostics.Contracts.Pure] public ushort ToUInt16(System.IFormatProvider? provider) => System.Convert.ToUInt16(m_value);
    [System.CLSCompliant(false)][System.Diagnostics.Contracts.Pure] public uint ToUInt32(System.IFormatProvider? provider) => System.Convert.ToUInt32(m_value);
    [System.CLSCompliant(false)][System.Diagnostics.Contracts.Pure] public ulong ToUInt64(System.IFormatProvider? provider) => System.Convert.ToUInt64(m_value);
    #endregion IConvertible

    // IEquatable<>
    [System.Diagnostics.Contracts.Pure] public bool Equals(LinearVelocity other) => m_value == other.m_value;

    // IFormattable
    [System.Diagnostics.Contracts.Pure] public string ToString(string? format, IFormatProvider? formatProvider) => m_value.ToString(format, formatProvider);

    // IQuantifiable<>
    [System.Diagnostics.Contracts.Pure] public double Value { get => m_value; init => m_value = value; }
    // IUnitQuantifiable<>
    [System.Diagnostics.Contracts.Pure]
    public string ToUnitString(LinearVelocityUnit unit = DefaultUnit, string? format = null, bool useFullName = false, bool preferUnicode = false)
      => $"{string.Format($"{{0{(format is null ? string.Empty : $":{format}")}}}", ToUnitValue(unit))} {unit.GetUnitString()}";
    [System.Diagnostics.Contracts.Pure]
    public double ToUnitValue(LinearVelocityUnit unit = DefaultUnit)
      => unit switch
      {
        LinearVelocityUnit.FootPerSecond => m_value * (1250.0 / 381.0),
        LinearVelocityUnit.KilometerPerHour => m_value * (18.0 / 5.0),
        LinearVelocityUnit.Knot => m_value * (3600.0 / 1852.0),
        LinearVelocityUnit.MeterPerSecond => m_value,
        LinearVelocityUnit.MilePerHour => m_value * (3125.0 / 1397.0),
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
    #endregion Implemented interfaces

    #region Object overrides
    [System.Diagnostics.Contracts.Pure] public override bool Equals(object? obj) => obj is LinearVelocity o && Equals(o);
    [System.Diagnostics.Contracts.Pure] public override int GetHashCode() => m_value.GetHashCode();
    [System.Diagnostics.Contracts.Pure] public override string ToString() => $"{GetType().Name} {{ Value = {ToUnitString()} }}";
    #endregion Object overrides
  }
}
