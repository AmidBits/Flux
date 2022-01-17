namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static Energy Create(this EnergyUnit unit, double value)
      => new(value, unit);
    public static string GetUnitSymbol(this EnergyUnit unit)
      => unit switch
      {
        EnergyUnit.Joule => "J",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  public enum EnergyUnit
  {
    Joule,
    ElectronVolt,
  }

  /// <summary>Energy unit of Joule.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Energy"/>
  public struct Energy
    : System.IComparable<Energy>, System.IConvertible, System.IEquatable<Energy>, IValueSiDerivedUnit<double>
  {
    public const EnergyUnit DefaultUnit = EnergyUnit.Joule;

    private readonly double m_value;

    public Energy(double value, EnergyUnit unit = DefaultUnit)
      => m_value = unit switch
      {
        EnergyUnit.Joule => value,
        EnergyUnit.ElectronVolt => value / 1.602176634e-19,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double Value
      => m_value;

    public string ToUnitString(EnergyUnit unit = DefaultUnit, string? format = null)
      => $"{string.Format($"{{0:{(format is null ? string.Empty : $":{format}")}}}", ToUnitValue(unit))} {unit.GetUnitSymbol()}";
    public double ToUnitValue(EnergyUnit unit = DefaultUnit)
      => unit switch
      {
        EnergyUnit.Joule => m_value,
        EnergyUnit.ElectronVolt => m_value * 1.602176634e-19,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    #region Static methods
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator double(Energy v)
      => v.m_value;
    public static explicit operator Energy(double v)
      => new(v);

    public static bool operator <(Energy a, Energy b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(Energy a, Energy b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(Energy a, Energy b)
      => a.CompareTo(b) > 0;
    public static bool operator >=(Energy a, Energy b)
      => a.CompareTo(b) >= 0;

    public static bool operator ==(Energy a, Energy b)
      => a.Equals(b);
    public static bool operator !=(Energy a, Energy b)
      => !a.Equals(b);

    public static Energy operator -(Energy v)
      => new(-v.m_value);
    public static Energy operator +(Energy a, double b)
      => new(a.m_value + b);
    public static Energy operator +(Energy a, Energy b)
      => a + b.m_value;
    public static Energy operator /(Energy a, double b)
      => new(a.m_value / b);
    public static Energy operator /(Energy a, Energy b)
      => a / b.m_value;
    public static Energy operator *(Energy a, double b)
      => new(a.m_value * b);
    public static Energy operator *(Energy a, Energy b)
      => a * b.m_value;
    public static Energy operator %(Energy a, double b)
      => new(a.m_value % b);
    public static Energy operator %(Energy a, Energy b)
      => a % b.m_value;
    public static Energy operator -(Energy a, double b)
      => new(a.m_value - b);
    public static Energy operator -(Energy a, Energy b)
      => a - b.m_value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(Energy other)
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
    public bool Equals(Energy other)
      => m_value == other.m_value;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Energy o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
    public override string ToString()
      => $"{GetType().Name} {{ Value = {ToUnitString()} J }}";
    #endregion Object overrides
  }
}
