namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static string GetUnitString(this DynamicViscosityUnit unit, bool useFullName = false, bool preferUnicode = false)
      => useFullName ? unit.ToString() : unit switch
      {
        DynamicViscosityUnit.PascalSecond => "Pa�s",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }


  public enum DynamicViscosityUnit
  {
    PascalSecond,
  }

  /// <summary>Dynamic viscosity, unit of Pascal second.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Dynamic_viscosity"/>
  public struct DynamicViscosity
    : System.IComparable<DynamicViscosity>, System.IConvertible, System.IEquatable<DynamicViscosity>, ISiDerivedUnitQuantifiable<double, DynamicViscosityUnit>
  {
    public const DynamicViscosityUnit DefaultUnit = DynamicViscosityUnit.PascalSecond;

    private readonly double m_value;

    public DynamicViscosity(double value, DynamicViscosityUnit unit = DefaultUnit)
      => m_value = unit switch
      {
        DynamicViscosityUnit.PascalSecond => value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double Value
      => m_value;

    public string ToUnitString(DynamicViscosityUnit unit = DefaultUnit, string? format = null, bool useFullName = false, bool preferUnicode = false)
      => $"{string.Format($"{{0{(format is null ? string.Empty : $":{format}")}}}", ToUnitValue(unit))} {unit.GetUnitString()}";
    public double ToUnitValue(DynamicViscosityUnit unit = DefaultUnit)
      => unit switch
      {
        DynamicViscosityUnit.PascalSecond => m_value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    #region Static methods
    public static DynamicViscosity From(Pressure pressure, Time time)
      => new(pressure.Value * time.Value);
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator double(DynamicViscosity v)
      => v.m_value;
    public static explicit operator DynamicViscosity(double v)
      => new(v);

    public static bool operator <(DynamicViscosity a, DynamicViscosity b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(DynamicViscosity a, DynamicViscosity b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(DynamicViscosity a, DynamicViscosity b)
      => a.CompareTo(b) > 0;
    public static bool operator >=(DynamicViscosity a, DynamicViscosity b)
      => a.CompareTo(b) >= 0;

    public static bool operator ==(DynamicViscosity a, DynamicViscosity b)
      => a.Equals(b);
    public static bool operator !=(DynamicViscosity a, DynamicViscosity b)
      => !a.Equals(b);

    public static DynamicViscosity operator -(DynamicViscosity v)
      => new(-v.m_value);
    public static DynamicViscosity operator +(DynamicViscosity a, double b)
      => new(a.m_value + b);
    public static DynamicViscosity operator +(DynamicViscosity a, DynamicViscosity b)
      => a + b.m_value;
    public static DynamicViscosity operator /(DynamicViscosity a, double b)
      => new(a.m_value / b);
    public static DynamicViscosity operator /(DynamicViscosity a, DynamicViscosity b)
      => a / b.m_value;
    public static DynamicViscosity operator *(DynamicViscosity a, double b)
      => new(a.m_value * b);
    public static DynamicViscosity operator *(DynamicViscosity a, DynamicViscosity b)
      => a * b.m_value;
    public static DynamicViscosity operator %(DynamicViscosity a, double b)
      => new(a.m_value % b);
    public static DynamicViscosity operator %(DynamicViscosity a, DynamicViscosity b)
      => a % b.m_value;
    public static DynamicViscosity operator -(DynamicViscosity a, double b)
      => new(a.m_value - b);
    public static DynamicViscosity operator -(DynamicViscosity a, DynamicViscosity b)
      => a - b.m_value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(DynamicViscosity other)
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
    public bool Equals(DynamicViscosity other)
      => m_value == other.m_value;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is DynamicViscosity o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
    public override string ToString()
      => $"{GetType().Name} {{ Value = {ToUnitString()} }}";
    #endregion Object overrides
  }
}