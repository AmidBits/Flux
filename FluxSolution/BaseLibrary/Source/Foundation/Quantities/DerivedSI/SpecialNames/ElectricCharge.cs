namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static ElectricCharge Create(this ElectricChargeUnit unit, double value)
      => new(value, unit);
    public static string GetUnitSymbol(this ElectricChargeUnit unit)
      => unit switch
      {
        ElectricChargeUnit.Coulomb => @" C",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  public enum ElectricChargeUnit
  {
    Coulomb,
  }

  /// <summary>Electric charge unit of Coulomb.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Electric_charge"/>
  public struct ElectricCharge
    : System.IComparable<ElectricCharge>, System.IConvertible, System.IEquatable<ElectricCharge>, IValueSiDerivedUnit<double>
  {
    public const ElectricChargeUnit DefaultUnit = ElectricChargeUnit.Coulomb;

    public static ElectricCharge ElementaryCharge
      => new(1.602176634e-19);

    private readonly double m_value;

    public ElectricCharge(double value, ElectricChargeUnit unit = DefaultUnit)
      => m_value = unit switch
      {
        ElectricChargeUnit.Coulomb => value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double Value
      => m_value;

    public string ToUnitString(ElectricChargeUnit unit = DefaultUnit, string? format = null)
      => $"{(format is null ? ToUnitValue(unit) : string.Format($"{{0:{format}}}", ToUnitValue(unit)))}{unit.GetUnitSymbol()}";
    public double ToUnitValue(ElectricChargeUnit unit = DefaultUnit)
      => unit switch
      {
        ElectricChargeUnit.Coulomb => m_value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    #region Overloaded operators
    public static explicit operator double(ElectricCharge v)
      => v.m_value;
    public static explicit operator ElectricCharge(double v)
      => new(v);

    public static bool operator <(ElectricCharge a, ElectricCharge b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(ElectricCharge a, ElectricCharge b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(ElectricCharge a, ElectricCharge b)
      => a.CompareTo(b) > 0;
    public static bool operator >=(ElectricCharge a, ElectricCharge b)
      => a.CompareTo(b) >= 0;

    public static bool operator ==(ElectricCharge a, ElectricCharge b)
      => a.Equals(b);
    public static bool operator !=(ElectricCharge a, ElectricCharge b)
      => !a.Equals(b);

    public static ElectricCharge operator -(ElectricCharge v)
      => new(-v.m_value);
    public static ElectricCharge operator +(ElectricCharge a, double b)
      => new(a.m_value + b);
    public static ElectricCharge operator +(ElectricCharge a, ElectricCharge b)
      => a + b.m_value;
    public static ElectricCharge operator /(ElectricCharge a, double b)
      => new(a.m_value / b);
    public static ElectricCharge operator /(ElectricCharge a, ElectricCharge b)
      => a / b.m_value;
    public static ElectricCharge operator *(ElectricCharge a, double b)
      => new(a.m_value * b);
    public static ElectricCharge operator *(ElectricCharge a, ElectricCharge b)
      => a * b.m_value;
    public static ElectricCharge operator %(ElectricCharge a, double b)
      => new(a.m_value % b);
    public static ElectricCharge operator %(ElectricCharge a, ElectricCharge b)
      => a % b.m_value;
    public static ElectricCharge operator -(ElectricCharge a, double b)
      => new(a.m_value - b);
    public static ElectricCharge operator -(ElectricCharge a, ElectricCharge b)
      => a - b.m_value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(ElectricCharge other)
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
    public bool Equals(ElectricCharge other)
      => m_value == other.m_value;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is ElectricCharge o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
    public override string ToString()
      => $"{GetType().Name} {{ Value = {ToUnitString()} }}";
    #endregion Object overrides
  }
}
