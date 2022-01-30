namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static string GetUnitString(this AngularVelocityUnit unit, bool useNameInsteadOfSymbol = false, bool useUnicodeIfAvailable = false)
      => useNameInsteadOfSymbol ? unit.ToString() : unit switch
      {
        AngularVelocityUnit.RadianPerSecond => "rad/s",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }


  public enum AngularVelocityUnit
  {
    RadianPerSecond,
  }

  /// <summary>Angular velocity, unit of radians per second. This is an SI derived quantity.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Angular_velocity"/>
  public struct AngularVelocity
    : System.IComparable<AngularVelocity>, System.IConvertible, System.IEquatable<AngularVelocity>, IValueSiDerivedUnit<double>
  {
    public const AngularVelocityUnit DefaultUnit = AngularVelocityUnit.RadianPerSecond;

    private readonly double m_value;

    public AngularVelocity(double value, AngularVelocityUnit unit = DefaultUnit)
      => m_value = unit switch
      {
        AngularVelocityUnit.RadianPerSecond => value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double Value
      => m_value;

    public string ToUnitString(AngularVelocityUnit unit = DefaultUnit, string? format = null)
      => $"{string.Format($"{{0:{(format is null ? string.Empty : $":{format}")}}}", ToUnitValue(unit))} {unit.GetUnitString()}";
    public double ToUnitValue(AngularVelocityUnit unit = DefaultUnit)
      => unit switch
      {
        AngularVelocityUnit.RadianPerSecond => m_value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    #region Static methods
    public static AngularVelocity From(Angle angle, Time time)
      => new(angle.Value / time.Value);
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator double(AngularVelocity v)
      => v.m_value;
    public static explicit operator AngularVelocity(double v)
      => new(v);

    public static bool operator <(AngularVelocity a, AngularVelocity b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(AngularVelocity a, AngularVelocity b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(AngularVelocity a, AngularVelocity b)
      => a.CompareTo(b) > 0;
    public static bool operator >=(AngularVelocity a, AngularVelocity b)
      => a.CompareTo(b) >= 0;

    public static bool operator ==(AngularVelocity a, AngularVelocity b)
      => a.Equals(b);
    public static bool operator !=(AngularVelocity a, AngularVelocity b)
      => !a.Equals(b);

    public static AngularVelocity operator -(AngularVelocity v)
      => new(-v.m_value);
    public static AngularVelocity operator +(AngularVelocity a, AngularVelocity b)
      => new(a.m_value + b.m_value);
    public static AngularVelocity operator /(AngularVelocity a, AngularVelocity b)
      => new(a.m_value / b.m_value);
    public static AngularVelocity operator *(AngularVelocity a, AngularVelocity b)
      => new(a.m_value * b.m_value);
    public static AngularVelocity operator %(AngularVelocity a, AngularVelocity b)
      => new(a.m_value % b.m_value);
    public static AngularVelocity operator -(AngularVelocity a, AngularVelocity b)
      => new(a.m_value - b.m_value);

    public static AngularVelocity operator +(AngularVelocity a, double b)
      => new(a.m_value + b);
    public static AngularVelocity operator /(AngularVelocity a, double b)
      => new(a.m_value / b);
    public static AngularVelocity operator *(AngularVelocity a, double b)
      => new(a.m_value * b);
    public static AngularVelocity operator %(AngularVelocity a, double b)
      => new(a.m_value % b);
    public static AngularVelocity operator -(AngularVelocity a, double b)
      => new(a.m_value - b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(AngularVelocity other)
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
    public bool Equals(AngularVelocity other)
      => m_value == other.m_value;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is AngularVelocity o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
    public override string ToString()
      => $"{GetType().Name} {{ Value = {ToUnitString()} }}";
    #endregion Object overrides
  }
}
