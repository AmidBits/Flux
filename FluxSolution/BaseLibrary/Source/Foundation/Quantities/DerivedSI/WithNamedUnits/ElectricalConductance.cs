namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static ElectricalConductance Create(this ElectricalConductanceUnit unit, double value)
      => new(value, unit);
    public static string GetUnitSymbol(this ElectricalConductanceUnit unit)
      => unit switch
      {
        ElectricalConductanceUnit.Siemens => "S",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  public enum ElectricalConductanceUnit
  {
    Siemens,
  }

  /// <summary>Electrical conductance, unit of Siemens.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Electrical_resistance_and_conductance"/>
  public struct ElectricalConductance
    : System.IComparable<ElectricalConductance>, System.IConvertible, System.IEquatable<ElectricalConductance>, IValueSiDerivedUnit<double>
  {
    public const ElectricalConductanceUnit DefaultUnit = ElectricalConductanceUnit.Siemens;

    private readonly double m_value;

    public ElectricalConductance(double value, ElectricalConductanceUnit unit = DefaultUnit)
      => m_value = unit switch
      {
        ElectricalConductanceUnit.Siemens => value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double Value
      => m_value;

    public string ToUnitString(ElectricalConductanceUnit unit = DefaultUnit, string? format = null)
      => $"{string.Format($"{{0:{(format is null ? string.Empty : $":{format}")}}}", ToUnitValue(unit))} {unit.GetUnitSymbol()}";
    public double ToUnitValue(ElectricalConductanceUnit unit = DefaultUnit)
      => unit switch
      {
        ElectricalConductanceUnit.Siemens => m_value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
    public ElectricalResistance ToElectricResistance()
      => new(1 / m_value);

    #region Static methods
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator double(ElectricalConductance v)
      => v.m_value;
    public static explicit operator ElectricalConductance(double v)
      => new(v);

    public static bool operator <(ElectricalConductance a, ElectricalConductance b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(ElectricalConductance a, ElectricalConductance b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(ElectricalConductance a, ElectricalConductance b)
      => a.CompareTo(b) > 0;
    public static bool operator >=(ElectricalConductance a, ElectricalConductance b)
      => a.CompareTo(b) >= 0;

    public static bool operator ==(ElectricalConductance a, ElectricalConductance b)
      => a.Equals(b);
    public static bool operator !=(ElectricalConductance a, ElectricalConductance b)
      => !a.Equals(b);

    public static ElectricalConductance operator -(ElectricalConductance v)
      => new(-v.m_value);
    public static ElectricalConductance operator +(ElectricalConductance a, double b)
      => new(a.m_value + b);
    public static ElectricalConductance operator +(ElectricalConductance a, ElectricalConductance b)
      => a + b.m_value;
    public static ElectricalConductance operator /(ElectricalConductance a, double b)
      => new(a.m_value / b);
    public static ElectricalConductance operator /(ElectricalConductance a, ElectricalConductance b)
      => a / b.m_value;
    public static ElectricalConductance operator *(ElectricalConductance a, double b)
      => new(a.m_value * b);
    public static ElectricalConductance operator *(ElectricalConductance a, ElectricalConductance b)
      => a * b.m_value;
    public static ElectricalConductance operator %(ElectricalConductance a, double b)
      => new(a.m_value % b);
    public static ElectricalConductance operator %(ElectricalConductance a, ElectricalConductance b)
      => a % b.m_value;
    public static ElectricalConductance operator -(ElectricalConductance a, double b)
      => new(a.m_value - b);
    public static ElectricalConductance operator -(ElectricalConductance a, ElectricalConductance b)
      => a - b.m_value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(ElectricalConductance other)
      => m_value.CompareTo(other.m_value);

    #region IConvertible
    public System.TypeCode GetTypeCode() => System.TypeCode.Object;
    public bool ToBoolean(System.IFormatProvider? provider) => Value != 0;
    public byte ToByte(System.IFormatProvider? provider) => System.Convert.ToByte(Value);
    public char ToChar(System.IFormatProvider? provider) => System.Convert.ToChar(Value);
    public System.DateTime ToDateTime(System.IFormatProvider? provider) => System.Convert.ToDateTime(Value);
    public decimal ToDecimal(System.IFormatProvider? provider) => System.Convert.ToDecimal(Value);
    public double ToDouble(System.IFormatProvider? provider) => System.Convert.ToDouble(Value);
    public short ToInt16(System.IFormatProvider? provider) => System.Convert.ToInt16(Value);
    public int ToInt32(System.IFormatProvider? provider) => System.Convert.ToInt32(Value);
    public long ToInt64(System.IFormatProvider? provider) => System.Convert.ToInt64(Value);
    [System.CLSCompliant(false)] public sbyte ToSByte(System.IFormatProvider? provider) => System.Convert.ToSByte(Value);
    public float ToSingle(System.IFormatProvider? provider) => System.Convert.ToSingle(Value);
    public string ToString(System.IFormatProvider? provider) => string.Format(provider, "{0}", Value);
    public object ToType(System.Type conversionType, System.IFormatProvider? provider) => System.Convert.ChangeType(Value, conversionType, provider);
    [System.CLSCompliant(false)] public ushort ToUInt16(System.IFormatProvider? provider) => System.Convert.ToUInt16(Value);
    [System.CLSCompliant(false)] public uint ToUInt32(System.IFormatProvider? provider) => System.Convert.ToUInt32(Value);
    [System.CLSCompliant(false)] public ulong ToUInt64(System.IFormatProvider? provider) => System.Convert.ToUInt64(Value);
    #endregion IConvertible

    // IEquatable
    public bool Equals(ElectricalConductance other)
      => m_value == other.m_value;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is ElectricalConductance o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
    public override string ToString()
      => $"{GetType().Name} {{ Value = {ToUnitString()} }}";
    #endregion Object overrides
  }
}
