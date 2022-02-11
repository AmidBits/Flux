namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static string GetUnitString(this VoltageUnit unit, bool useNameInsteadOfSymbol = false, bool useUnicodeIfAvailable = false)
      => useNameInsteadOfSymbol ? unit.ToString() : unit switch
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
  public struct Voltage
    : System.IComparable<Voltage>, System.IConvertible, System.IEquatable<Voltage>, ISiDerivedUnitQuantifiable<double, VoltageUnit>
  {
    public const VoltageUnit DefaultUnit = VoltageUnit.Volt;

    private readonly double m_value;

    public Voltage(double value, VoltageUnit unit = DefaultUnit)
      => m_value = unit switch
      {
        VoltageUnit.Volt => value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double Value
      => m_value;

    public string ToUnitString(VoltageUnit unit = DefaultUnit, string? format = null, bool useFullName = false, bool preferUnicode = false)
      => $"{string.Format($"{{0:{(format is null ? string.Empty : $":{format}")}}}", ToUnitValue(unit))} {unit.GetUnitString()}";
    public double ToUnitValue(VoltageUnit unit = DefaultUnit)
      => unit switch
      {
        VoltageUnit.Volt => m_value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    #region Static methods
    /// <summary>Creates a new Voltage instance from the specified current and resistance.</summary>
    /// <param name="current"></param>
    /// <param name="resistance"></param>
    public static Voltage From(ElectricCurrent current, ElectricalResistance resistance)
      => new(current.Value * resistance.Value);
    /// <summary>Creates a new Voltage instance from the specified power and current.</summary>
    /// <param name="power"></param>
    /// <param name="current"></param>
    public static Voltage From(Power power, ElectricCurrent current)
      => new(power.Value / current.Value);
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator double(Voltage v)
      => v.m_value;
    public static explicit operator Voltage(double v)
      => new(v);

    public static bool operator <(Voltage a, Voltage b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(Voltage a, Voltage b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(Voltage a, Voltage b)
      => a.CompareTo(b) > 0;
    public static bool operator >=(Voltage a, Voltage b)
      => a.CompareTo(b) >= 0;

    public static bool operator ==(Voltage a, Voltage b)
      => a.Equals(b);
    public static bool operator !=(Voltage a, Voltage b)
      => !a.Equals(b);

    public static Voltage operator -(Voltage v)
      => new(-v.m_value);
    public static Voltage operator +(Voltage a, double b)
      => new(a.m_value + b);
    public static Voltage operator +(Voltage a, Voltage b)
      => a + b.m_value;
    public static Voltage operator /(Voltage a, double b)
      => new(a.m_value / b);
    public static Voltage operator /(Voltage a, Voltage b)
      => a / b.m_value;
    public static Voltage operator *(Voltage a, double b)
      => new(a.m_value * b);
    public static Voltage operator *(Voltage a, Voltage b)
      => a * b.m_value;
    public static Voltage operator %(Voltage a, double b)
      => new(a.m_value % b);
    public static Voltage operator %(Voltage a, Voltage b)
      => a % b.m_value;
    public static Voltage operator -(Voltage a, double b)
      => new(a.m_value - b);
    public static Voltage operator -(Voltage a, Voltage b)
      => a - b.m_value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(Voltage other)
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
    public bool Equals(Voltage other)
      => m_value == other.m_value;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Voltage o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
    public override string ToString()
      => $"{GetType().Name} {{ {ToUnitString()} }}";
    #endregion Object overrides
  }
}
