namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static string GetUnitString(this AreaUnit unit, bool useNameInsteadOfSymbol = false, bool useUnicodeIfAvailable = false)
      => useNameInsteadOfSymbol ? unit.ToString() : unit switch
      {
        AreaUnit.SquareMeter => "m²",
        AreaUnit.Hectare => "ha",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  public enum AreaUnit
  {
    SquareMeter,
    Hectare,
  }

  /// <summary>Area, unit of square meter. This is an SI derived quantity.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Area"/>
  public struct Area
    : System.IComparable<Area>, System.IConvertible, System.IEquatable<Area>, IValueSiDerivedUnit<double>
  {
    public const AreaUnit DefaultUnit = AreaUnit.SquareMeter;

    private readonly double m_value;

    public Area(double value, AreaUnit unit = DefaultUnit)
      => m_value = unit switch
      {
        AreaUnit.SquareMeter => value,
        AreaUnit.Hectare => value * 10000,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double Value
      => m_value;

    public string ToUnitString(AreaUnit unit = DefaultUnit, string? format = null)
      => $"{string.Format($"{{0:{(format is null ? string.Empty : $":{format}")}}}", ToUnitValue(unit))} {unit.GetUnitString()}";
    public double ToUnitValue(AreaUnit unit = DefaultUnit)
      => unit switch
      {
        AreaUnit.SquareMeter => m_value,
        AreaUnit.Hectare => m_value / 10000,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    #region Static methods
    /// <summary>Creates a new Area instance from the specified rectangular length and width.</summary>
    /// <param name="length"></param>
    /// <param name="width"></param>
    public static Area From(Length length, Length width)
      => new(length.Value * width.Value);
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator double(Area v)
      => v.m_value;
    public static explicit operator Area(double v)
      => new(v);

    public static bool operator <(Area a, Area b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(Area a, Area b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(Area a, Area b)
      => a.CompareTo(b) > 0;
    public static bool operator >=(Area a, Area b)
      => a.CompareTo(b) >= 0;

    public static bool operator ==(Area a, Area b)
      => a.Equals(b);
    public static bool operator !=(Area a, Area b)
      => !a.Equals(b);

    public static Area operator -(Area v)
      => new(-v.m_value);
    public static Area operator +(Area a, double b)
      => new(a.m_value + b);
    public static Area operator +(Area a, Area b)
      => a + b.m_value;
    public static Area operator /(Area a, double b)
      => new(a.m_value / b);
    public static Area operator /(Area a, Area b)
      => a / b.m_value;
    public static Area operator *(Area a, double b)
      => new(a.m_value * b);
    public static Area operator *(Area a, Area b)
      => a * b.m_value;
    public static Area operator %(Area a, double b)
      => new(a.m_value % b);
    public static Area operator %(Area a, Area b)
      => a % b.m_value;
    public static Area operator -(Area a, double b)
      => new(a.m_value - b);
    public static Area operator -(Area a, Area b)
      => a - b.m_value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(Area other)
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
    public bool Equals(Area other)
      => m_value == other.m_value;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Area o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
    public override string ToString()
      => $"{GetType().Name} {{ Value = {ToUnitString()} }}";
    #endregion Object overrides
  }
}
