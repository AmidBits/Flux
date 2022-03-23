namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static string GetUnitString(this ElectricCurrentUnit unit, bool useNameInstead = false, bool useUnicodeIfAvailable = false)
      => useNameInstead ? unit.ToString() : unit switch
      {
        ElectricCurrentUnit.Ampere => "A",
        ElectricCurrentUnit.Milliampere => "mA",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  public enum ElectricCurrentUnit
  {
    Ampere,
    Milliampere,
  }

  /// <summary>Electric current. SI unit of ampere. This is a base quantity.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Electric_current"/>
  public struct ElectricCurrent
    : System.IComparable, System.IComparable<ElectricCurrent>, System.IConvertible, System.IEquatable<ElectricCurrent>, IMetricOneQuantifiable, ISiBaseUnitQuantifiable<double, ElectricCurrentUnit>
  {
    public const ElectricCurrentUnit DefaultUnit = ElectricCurrentUnit.Ampere;

    private readonly double m_value;

    public ElectricCurrent(double value, ElectricCurrentUnit unit = DefaultUnit)
      => m_value = unit switch
      {
        ElectricCurrentUnit.Ampere => value,
        ElectricCurrentUnit.Milliampere => value / 1000,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    [System.Diagnostics.Contracts.Pure] public double Value => m_value;

    [System.Diagnostics.Contracts.Pure]
    public string ToMetricOneString(MetricMultiplicativePrefix prefix, string? format = null, bool useFullName = false, bool preferUnicode = false)
       => $"{new MetricMultiplicative(m_value, MetricMultiplicativePrefix.One).ToUnitString(prefix, format, useFullName, preferUnicode)}{DefaultUnit.GetUnitString(useFullName, preferUnicode)}";

    [System.Diagnostics.Contracts.Pure]
    public string ToUnitString(ElectricCurrentUnit unit = DefaultUnit, string? format = null, bool useFullName = false, bool preferUnicode = false)
      => $"{string.Format($"{{0{(format is null ? string.Empty : $":{format}")}}}", ToUnitValue(unit))} {unit.GetUnitString()}";
    [System.Diagnostics.Contracts.Pure]
    public double ToUnitValue(ElectricCurrentUnit unit = DefaultUnit)
      => unit switch
      {
        ElectricCurrentUnit.Milliampere => m_value * 1000,
        ElectricCurrentUnit.Ampere => m_value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    #region Static methods
    /// <summary>Creates a new ElectricCurrent instance from power and voltage.</summary>
    /// <param name="power"></param>
    /// <param name="voltage"></param>
    [System.Diagnostics.Contracts.Pure] public static ElectricCurrent From(Power power, Voltage voltage) => new(power.Value / voltage.Value);
    /// <summary>Creates a new ElectricCurrent instance from voltage and resistance.</summary>
    /// <param name="voltage"></param>
    /// <param name="resistance"></param>
    [System.Diagnostics.Contracts.Pure] public static ElectricCurrent From(Voltage voltage, ElectricalResistance resistance) => new(voltage.Value / resistance.Value);
    #endregion Static methods

    #region Overloaded operators
    [System.Diagnostics.Contracts.Pure] public static explicit operator double(ElectricCurrent v) => v.m_value;
    [System.Diagnostics.Contracts.Pure] public static explicit operator ElectricCurrent(double v) => new(v);

    [System.Diagnostics.Contracts.Pure] public static bool operator <(ElectricCurrent a, ElectricCurrent b) => a.CompareTo(b) < 0;
    [System.Diagnostics.Contracts.Pure] public static bool operator <=(ElectricCurrent a, ElectricCurrent b) => a.CompareTo(b) <= 0;
    [System.Diagnostics.Contracts.Pure] public static bool operator >(ElectricCurrent a, ElectricCurrent b) => a.CompareTo(b) > 0;
    [System.Diagnostics.Contracts.Pure] public static bool operator >=(ElectricCurrent a, ElectricCurrent b) => a.CompareTo(b) >= 0;

    [System.Diagnostics.Contracts.Pure] public static bool operator ==(ElectricCurrent a, ElectricCurrent b) => a.Equals(b);
    [System.Diagnostics.Contracts.Pure] public static bool operator !=(ElectricCurrent a, ElectricCurrent b) => !a.Equals(b);

    [System.Diagnostics.Contracts.Pure] public static ElectricCurrent operator -(ElectricCurrent v) => new(-v.m_value);
    [System.Diagnostics.Contracts.Pure] public static ElectricCurrent operator +(ElectricCurrent a, double b) => new(a.m_value + b);
    [System.Diagnostics.Contracts.Pure] public static ElectricCurrent operator +(ElectricCurrent a, ElectricCurrent b) => a + b.m_value;
    [System.Diagnostics.Contracts.Pure] public static ElectricCurrent operator /(ElectricCurrent a, double b) => new(a.m_value / b);
    [System.Diagnostics.Contracts.Pure] public static ElectricCurrent operator /(ElectricCurrent a, ElectricCurrent b) => a / b.m_value;
    [System.Diagnostics.Contracts.Pure] public static ElectricCurrent operator *(ElectricCurrent a, double b) => new(a.m_value * b);
    [System.Diagnostics.Contracts.Pure] public static ElectricCurrent operator *(ElectricCurrent a, ElectricCurrent b) => a * b.m_value;
    [System.Diagnostics.Contracts.Pure] public static ElectricCurrent operator %(ElectricCurrent a, double b) => new(a.m_value % b);
    [System.Diagnostics.Contracts.Pure] public static ElectricCurrent operator %(ElectricCurrent a, ElectricCurrent b) => a % b.m_value;
    [System.Diagnostics.Contracts.Pure] public static ElectricCurrent operator -(ElectricCurrent a, double b) => new(a.m_value - b);
    [System.Diagnostics.Contracts.Pure] public static ElectricCurrent operator -(ElectricCurrent a, ElectricCurrent b) => a - b.m_value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable<T>
    [System.Diagnostics.Contracts.Pure] public int CompareTo(ElectricCurrent other) => m_value.CompareTo(other.m_value);
    // IComparable
    [System.Diagnostics.Contracts.Pure] public int CompareTo(object? other) => other is not null && other is ElectricCurrent o ? CompareTo(o) : -1;

    #region IConvertible
    [System.Diagnostics.Contracts.Pure] public System.TypeCode GetTypeCode() => System.TypeCode.Object;
    [System.Diagnostics.Contracts.Pure] public bool ToBoolean(System.IFormatProvider? provider) => Value != 0;
    [System.Diagnostics.Contracts.Pure] public byte ToByte(System.IFormatProvider? provider) => System.Convert.ToByte(Value);
    [System.Diagnostics.Contracts.Pure] public char ToChar(System.IFormatProvider? provider) => System.Convert.ToChar(Value);
    [System.Diagnostics.Contracts.Pure] public System.DateTime ToDateTime(System.IFormatProvider? provider) => System.Convert.ToDateTime(Value);
    [System.Diagnostics.Contracts.Pure] public decimal ToDecimal(System.IFormatProvider? provider) => System.Convert.ToDecimal(Value);
    [System.Diagnostics.Contracts.Pure] public double ToDouble(System.IFormatProvider? provider) => System.Convert.ToDouble(Value);
    [System.Diagnostics.Contracts.Pure] public short ToInt16(System.IFormatProvider? provider) => System.Convert.ToInt16(Value);
    [System.Diagnostics.Contracts.Pure] public int ToInt32(System.IFormatProvider? provider) => System.Convert.ToInt32(Value);
    [System.Diagnostics.Contracts.Pure] public long ToInt64(System.IFormatProvider? provider) => System.Convert.ToInt64(Value);
    [System.CLSCompliant(false)][System.Diagnostics.Contracts.Pure] public sbyte ToSByte(System.IFormatProvider? provider) => System.Convert.ToSByte(Value);
    [System.Diagnostics.Contracts.Pure] public float ToSingle(System.IFormatProvider? provider) => System.Convert.ToSingle(Value);
    [System.Diagnostics.Contracts.Pure] public string ToString(System.IFormatProvider? provider) => string.Format(provider, "{0}", Value);
    [System.Diagnostics.Contracts.Pure] public object ToType(System.Type conversionType, System.IFormatProvider? provider) => System.Convert.ChangeType(Value, conversionType, provider);
    [System.CLSCompliant(false)][System.Diagnostics.Contracts.Pure] public ushort ToUInt16(System.IFormatProvider? provider) => System.Convert.ToUInt16(Value);
    [System.CLSCompliant(false)][System.Diagnostics.Contracts.Pure] public uint ToUInt32(System.IFormatProvider? provider) => System.Convert.ToUInt32(Value);
    [System.CLSCompliant(false)][System.Diagnostics.Contracts.Pure] public ulong ToUInt64(System.IFormatProvider? provider) => System.Convert.ToUInt64(Value);
    #endregion IConvertible

    // IEquatable<T>
    [System.Diagnostics.Contracts.Pure] public bool Equals(ElectricCurrent other) => m_value == other.m_value;
    #endregion Implemented interfaces

    #region Object overrides
    [System.Diagnostics.Contracts.Pure] public override bool Equals(object? obj) => obj is ElectricCurrent o && Equals(o);
    [System.Diagnostics.Contracts.Pure] public override int GetHashCode() => m_value.GetHashCode();
    [System.Diagnostics.Contracts.Pure] public override string ToString() => $"{GetType().Name} {{ Value = {ToUnitString()} }}";
    #endregion Object overrides
  }
}
