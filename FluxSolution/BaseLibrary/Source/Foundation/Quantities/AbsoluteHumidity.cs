namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static AbsoluteHumidity Create(this AbsoluteHumidityUnit unit, double value)
      => new(value, unit);
    public static string GetUnitSymbol(this AbsoluteHumidityUnit unit)
      => unit switch
      {
        AbsoluteHumidityUnit.GramsPerCubicMeter => @" g/m³",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  public enum AbsoluteHumidityUnit
  {
    GramsPerCubicMeter,
  }

  /// <summary>Absolute humidity unit of grams per cubic meter.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Humidity#Absolute_humidity"/>
  public struct AbsoluteHumidity
    : System.IComparable<AbsoluteHumidity>, System.IConvertible, System.IEquatable<AbsoluteHumidity>, IValueGeneralizedUnit<double>
  {
    public const AbsoluteHumidityUnit DefaultUnit = AbsoluteHumidityUnit.GramsPerCubicMeter;

    private readonly double m_value;

    public AbsoluteHumidity(double value, AbsoluteHumidityUnit unit = DefaultUnit)
      => m_value = unit switch
      {
        AbsoluteHumidityUnit.GramsPerCubicMeter => value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double Value
      => m_value;

    public string ToUnitString(AbsoluteHumidityUnit unit = DefaultUnit, string? format = null)
      => $"{(format is null ? ToUnitValue(unit) : string.Format($"{{0:{format}}}", ToUnitValue(unit)))}{unit.GetUnitSymbol()}";
    public double ToUnitValue(AbsoluteHumidityUnit unit = DefaultUnit)
      => unit switch
      {
        AbsoluteHumidityUnit.GramsPerCubicMeter => m_value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    #region Static methods
    public static AbsoluteHumidity From(double grams, Volume volume)
      => new(grams / volume.Value);
    public static AbsoluteHumidity From(Mass mass, Volume volume)
      => From(mass.Value * 1000, volume);
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator double(AbsoluteHumidity v)
      => v.m_value;
    public static explicit operator AbsoluteHumidity(double v)
      => new(v);

    public static bool operator <(AbsoluteHumidity a, AbsoluteHumidity b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(AbsoluteHumidity a, AbsoluteHumidity b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(AbsoluteHumidity a, AbsoluteHumidity b)
      => a.CompareTo(b) > 0;
    public static bool operator >=(AbsoluteHumidity a, AbsoluteHumidity b)
      => a.CompareTo(b) >= 0;

    public static bool operator ==(AbsoluteHumidity a, AbsoluteHumidity b)
      => a.Equals(b);
    public static bool operator !=(AbsoluteHumidity a, AbsoluteHumidity b)
      => !a.Equals(b);

    public static AbsoluteHumidity operator -(AbsoluteHumidity v)
      => new(-v.m_value);
    public static AbsoluteHumidity operator +(AbsoluteHumidity a, double b)
      => new(a.m_value + b);
    public static AbsoluteHumidity operator +(AbsoluteHumidity a, AbsoluteHumidity b)
      => a + b.m_value;
    public static AbsoluteHumidity operator /(AbsoluteHumidity a, double b)
      => new(a.m_value / b);
    public static AbsoluteHumidity operator /(AbsoluteHumidity a, AbsoluteHumidity b)
      => a / b.m_value;
    public static AbsoluteHumidity operator *(AbsoluteHumidity a, double b)
      => new(a.m_value * b);
    public static AbsoluteHumidity operator *(AbsoluteHumidity a, AbsoluteHumidity b)
      => a * b.m_value;
    public static AbsoluteHumidity operator %(AbsoluteHumidity a, double b)
      => new(a.m_value % b);
    public static AbsoluteHumidity operator %(AbsoluteHumidity a, AbsoluteHumidity b)
      => a % b.m_value;
    public static AbsoluteHumidity operator -(AbsoluteHumidity a, double b)
      => new(a.m_value - b);
    public static AbsoluteHumidity operator -(AbsoluteHumidity a, AbsoluteHumidity b)
      => a - b.m_value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(AbsoluteHumidity other)
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
    public bool Equals(AbsoluteHumidity other)
      => m_value == other.m_value;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is AbsoluteHumidity o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
    public override string ToString()
      => $"{GetType().Name} {{ Value = {ToUnitString()} }}";
    #endregion Object overrides
  }
}
