namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static Flow Create(this FlowUnit unit, double value)
      => new(value, unit);
    public static string GetUnitSymbol(this FlowUnit unit)
      => unit switch
      {
        FlowUnit.CubicMetersPerSecond => @" m³/s",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  public enum FlowUnit
  {
    CubicMetersPerSecond,
  }

  /// <summary>Volumetric flow, unit of cubic meters per second.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Flow"/>
  public struct Flow
    : System.IComparable<Flow>, System.IConvertible, System.IEquatable<Flow>, IValueSiDerivedUnit<double>
  {
    public const FlowUnit DefaultUnit = FlowUnit.CubicMetersPerSecond;

    private readonly double m_value;

    public Flow(double value, FlowUnit unit = DefaultUnit)
      => m_value = unit switch
      {
        FlowUnit.CubicMetersPerSecond => value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double Value
      => m_value;

    public string ToUnitString(FlowUnit unit = DefaultUnit, string? format = null)
      => $"{(format is null ? ToUnitValue(unit) : string.Format($"{{0:{format}}}", ToUnitValue(unit)))}{unit.GetUnitSymbol()}";
    public double ToUnitValue(FlowUnit unit = DefaultUnit)
      => unit switch
      {
        FlowUnit.CubicMetersPerSecond => m_value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    #region Static methods
    public static Flow From(Volume volume, Time time)
      => new(volume.Value / time.Value);
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator double(Flow v)
      => v.m_value;
    public static explicit operator Flow(double v)
      => new(v);

    public static bool operator <(Flow a, Flow b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(Flow a, Flow b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(Flow a, Flow b)
      => a.CompareTo(b) > 0;
    public static bool operator >=(Flow a, Flow b)
      => a.CompareTo(b) >= 0;

    public static bool operator ==(Flow a, Flow b)
      => a.Equals(b);
    public static bool operator !=(Flow a, Flow b)
      => !a.Equals(b);

    public static Flow operator -(Flow v)
      => new(-v.m_value);
    public static Flow operator +(Flow a, double b)
      => new(a.m_value + b);
    public static Flow operator +(Flow a, Flow b)
      => a + b.m_value;
    public static Flow operator /(Flow a, double b)
      => new(a.m_value / b);
    public static Flow operator /(Flow a, Flow b)
      => a / b.m_value;
    public static Flow operator *(Flow a, double b)
      => new(a.m_value * b);
    public static Flow operator *(Flow a, Flow b)
      => a * b.m_value;
    public static Flow operator %(Flow a, double b)
      => new(a.m_value % b);
    public static Flow operator %(Flow a, Flow b)
      => a % b.m_value;
    public static Flow operator -(Flow a, double b)
      => new(a.m_value - b);
    public static Flow operator -(Flow a, Flow b)
      => a - b.m_value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(Flow other)
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
    public bool Equals(Flow other)
      => m_value == other.m_value;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Flow o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
    public override string ToString()
      => $"{GetType().Name} {{ {ToUnitString()} }}";
    #endregion Object overrides
  }
}
