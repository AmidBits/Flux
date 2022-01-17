namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static AngularAcceleration Create(this AngularAccelerationUnit unit, double value)
      => new(value, unit);
    public static string GetUnitSymbol(this AngularAccelerationUnit unit)
      => unit switch
      {
        AngularAccelerationUnit.RadianPerSecondSquare => "rad/s²",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  public enum AngularAccelerationUnit
  {
    RadianPerSecondSquare,
  }

  /// <summary>Angular, acceleration unit of radians per second square. This is an SI derived quantity.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Angular_acceleration"/>
  public struct AngularAcceleration
    : System.IComparable<AngularAcceleration>, System.IConvertible, System.IEquatable<AngularAcceleration>, IValueSiDerivedUnit<double>
  {
    public const AngularAccelerationUnit DefaultUnit = AngularAccelerationUnit.RadianPerSecondSquare;

    private readonly double m_value;

    public AngularAcceleration(double value, AngularAccelerationUnit unit = DefaultUnit)
      => m_value = unit switch
      {
        AngularAccelerationUnit.RadianPerSecondSquare => value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double Value
      => m_value;

    public string ToUnitString(AngularAccelerationUnit unit = DefaultUnit, string? format = null)
      => $"{string.Format($"{{0:{(format is null ? string.Empty : $":{format}")}}}", ToUnitValue(unit))} {unit.GetUnitSymbol()}";
    public double ToUnitValue(AngularAccelerationUnit unit = DefaultUnit)
      => unit switch
      {
        AngularAccelerationUnit.RadianPerSecondSquare => m_value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    #region Overloaded operators
    public static explicit operator double(AngularAcceleration v)
      => v.m_value;
    public static explicit operator AngularAcceleration(double v)
      => new(v);

    public static bool operator <(AngularAcceleration a, AngularAcceleration b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(AngularAcceleration a, AngularAcceleration b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(AngularAcceleration a, AngularAcceleration b)
      => a.CompareTo(b) > 0;
    public static bool operator >=(AngularAcceleration a, AngularAcceleration b)
      => a.CompareTo(b) >= 0;

    public static bool operator ==(AngularAcceleration a, AngularAcceleration b)
      => a.Equals(b);
    public static bool operator !=(AngularAcceleration a, AngularAcceleration b)
      => !a.Equals(b);

    public static AngularAcceleration operator -(AngularAcceleration v)
      => new(-v.m_value);
    public static AngularAcceleration operator +(AngularAcceleration a, double b)
      => new(a.m_value + b);
    public static AngularAcceleration operator +(AngularAcceleration a, AngularAcceleration b)
      => a + b.m_value;
    public static AngularAcceleration operator /(AngularAcceleration a, double b)
      => new(a.m_value / b);
    public static AngularAcceleration operator /(AngularAcceleration a, AngularAcceleration b)
      => a / b.m_value;
    public static AngularAcceleration operator *(AngularAcceleration a, double b)
      => new(a.m_value * b);
    public static AngularAcceleration operator *(AngularAcceleration a, AngularAcceleration b)
      => a * b.m_value;
    public static AngularAcceleration operator %(AngularAcceleration a, double b)
      => new(a.m_value % b);
    public static AngularAcceleration operator %(AngularAcceleration a, AngularAcceleration b)
      => a % b.m_value;
    public static AngularAcceleration operator -(AngularAcceleration a, double b)
      => new(a.m_value - b);
    public static AngularAcceleration operator -(AngularAcceleration a, AngularAcceleration b)
      => a - b.m_value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(AngularAcceleration other)
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
    public bool Equals(AngularAcceleration other)
      => m_value == other.m_value;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is AngularAcceleration o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
    public override string ToString()
      => $"{GetType().Name} {{ Value = {ToUnitString()} }}";
    #endregion Object overrides
  }
}
