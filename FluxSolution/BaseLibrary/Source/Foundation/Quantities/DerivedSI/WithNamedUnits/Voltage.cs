namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static string GetUnitString(this VoltageUnit unit, bool useFullName = false, bool preferUnicode = false)
      => useFullName ? unit.ToString() : unit switch
      {
        VoltageUnit.Volt => "V",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  public enum VoltageUnit
  {
    /// <summary>Volt.</summary>
    Volt,
  }

  /// <summary>Voltage unit of volt.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Voltage"/>
  public readonly struct Voltage
    : System.IComparable, System.IComparable<Voltage>, System.IConvertible, System.IEquatable<Voltage>, System.IFormattable, IUnitQuantifiable<double, VoltageUnit>
  {
    public const VoltageUnit DefaultUnit = VoltageUnit.Volt;

    private readonly double m_value;

    public Voltage(double value, VoltageUnit unit = DefaultUnit)
      => m_value = unit switch
      {
        VoltageUnit.Volt => value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    #region Static methods
    /// <summary>Creates a new Voltage instance from the specified current and resistance.</summary>
    /// <param name="current"></param>
    /// <param name="resistance"></param>
    [System.Diagnostics.Contracts.Pure]
    public static Voltage From(ElectricCurrent current, ElectricalResistance resistance)
      => new(current.Value * resistance.Value);
    /// <summary>Creates a new Voltage instance from the specified power and current.</summary>
    /// <param name="power"></param>
    /// <param name="current"></param>
    [System.Diagnostics.Contracts.Pure]
    public static Voltage From(Power power, ElectricCurrent current)
      => new(power.Value / current.Value);
    #endregion Static methods

    #region Overloaded operators
    [System.Diagnostics.Contracts.Pure] public static explicit operator double(Voltage v) => v.m_value;
    [System.Diagnostics.Contracts.Pure] public static explicit operator Voltage(double v) => new(v);

    [System.Diagnostics.Contracts.Pure] public static bool operator <(Voltage a, Voltage b) => a.CompareTo(b) < 0;
    [System.Diagnostics.Contracts.Pure] public static bool operator <=(Voltage a, Voltage b) => a.CompareTo(b) <= 0;
    [System.Diagnostics.Contracts.Pure] public static bool operator >(Voltage a, Voltage b) => a.CompareTo(b) > 0;
    [System.Diagnostics.Contracts.Pure] public static bool operator >=(Voltage a, Voltage b) => a.CompareTo(b) >= 0;

    [System.Diagnostics.Contracts.Pure] public static bool operator ==(Voltage a, Voltage b) => a.Equals(b);
    [System.Diagnostics.Contracts.Pure] public static bool operator !=(Voltage a, Voltage b) => !a.Equals(b);

    [System.Diagnostics.Contracts.Pure] public static Voltage operator -(Voltage v) => new(-v.m_value);
    [System.Diagnostics.Contracts.Pure] public static Voltage operator +(Voltage a, double b) => new(a.m_value + b);
    [System.Diagnostics.Contracts.Pure] public static Voltage operator +(Voltage a, Voltage b) => a + b.m_value;
    [System.Diagnostics.Contracts.Pure] public static Voltage operator /(Voltage a, double b) => new(a.m_value / b);
    [System.Diagnostics.Contracts.Pure] public static Voltage operator /(Voltage a, Voltage b) => a / b.m_value;
    [System.Diagnostics.Contracts.Pure] public static Voltage operator *(Voltage a, double b) => new(a.m_value * b);
    [System.Diagnostics.Contracts.Pure] public static Voltage operator *(Voltage a, Voltage b) => a * b.m_value;
    [System.Diagnostics.Contracts.Pure] public static Voltage operator %(Voltage a, double b) => new(a.m_value % b);
    [System.Diagnostics.Contracts.Pure] public static Voltage operator %(Voltage a, Voltage b) => a % b.m_value;
    [System.Diagnostics.Contracts.Pure] public static Voltage operator -(Voltage a, double b) => new(a.m_value - b);
    [System.Diagnostics.Contracts.Pure] public static Voltage operator -(Voltage a, Voltage b) => a - b.m_value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable<>
    [System.Diagnostics.Contracts.Pure] public int CompareTo(Voltage other) => m_value.CompareTo(other.m_value);
    // IComparable
    [System.Diagnostics.Contracts.Pure] public int CompareTo(object? other) => other is not null && other is Voltage o ? CompareTo(o) : -1;

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
    [System.Diagnostics.Contracts.Pure] public bool Equals(Voltage other) => m_value == other.m_value;

    // IFormattable
    [System.Diagnostics.Contracts.Pure] public string ToString(string? format, IFormatProvider? formatProvider) => m_value.ToString(format, formatProvider);

    // IQuantifiable<>
    [System.Diagnostics.Contracts.Pure] public double Value { get => m_value; init => m_value = value; }
    // IUnitQuantifiable<>
    [System.Diagnostics.Contracts.Pure]
    public string ToUnitString(VoltageUnit unit = DefaultUnit, string? format = null, bool useFullName = false, bool preferUnicode = false)
      => $"{string.Format($"{{0{(format is null ? string.Empty : $":{format}")}}}", ToUnitValue(unit))} {unit.GetUnitString()}";
    [System.Diagnostics.Contracts.Pure]
    public double ToUnitValue(VoltageUnit unit = DefaultUnit)
      => unit switch
      {
        VoltageUnit.Volt => m_value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
    #endregion Implemented interfaces

    #region Object overrides
    [System.Diagnostics.Contracts.Pure] public override bool Equals(object? obj) => obj is Voltage o && Equals(o);
    [System.Diagnostics.Contracts.Pure] public override int GetHashCode() => m_value.GetHashCode();
    [System.Diagnostics.Contracts.Pure] public override string ToString() => $"{GetType().Name} {{ {ToUnitString()} }}";
    #endregion Object overrides
  }
}
