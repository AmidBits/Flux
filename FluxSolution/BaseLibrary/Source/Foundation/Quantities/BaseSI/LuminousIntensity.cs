namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static LuminousIntensity Create(this LuminousIntensityUnit unit, double value)
      => new(value, unit);
    public static string GetUnitSymbol(this LuminousIntensityUnit unit)
      => unit switch
      {
        LuminousIntensityUnit.Candela => "cd",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  public enum LuminousIntensityUnit
  {
    Candela,
  }

  /// <summary>Luminous intensity. SI unit of candela. This is a base quantity.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Amount_of_substance"/>
  public struct LuminousIntensity
    : System.IComparable<LuminousIntensity>, System.IConvertible, System.IEquatable<LuminousIntensity>, IValueSiBaseUnit<double>, IValueGeneralizedUnit<double>
  {
    public const LuminousIntensityUnit DefaultUnit = LuminousIntensityUnit.Candela;

    private readonly double m_value;

    public LuminousIntensity(double value, LuminousIntensityUnit unit = DefaultUnit)
      => m_value = unit switch
      {
        LuminousIntensityUnit.Candela => value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double Value
      => m_value;

    public string ToUnitString(LuminousIntensityUnit unit = DefaultUnit, string? format = null)
      => $"{string.Format($"{{0:{(format is null ? string.Empty : $":{format}")}}}", ToUnitValue(unit))} {unit.GetUnitSymbol()}";
    public double ToUnitValue(LuminousIntensityUnit unit = DefaultUnit)
      => unit switch
      {
        LuminousIntensityUnit.Candela => m_value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    #region Static methods
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator double(LuminousIntensity v)
      => v.m_value;
    public static explicit operator LuminousIntensity(double v)
      => new(v);

    public static bool operator <(LuminousIntensity a, LuminousIntensity b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(LuminousIntensity a, LuminousIntensity b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(LuminousIntensity a, LuminousIntensity b)
      => a.CompareTo(b) > 0;
    public static bool operator >=(LuminousIntensity a, LuminousIntensity b)
      => a.CompareTo(b) >= 0;

    public static bool operator ==(LuminousIntensity a, LuminousIntensity b)
      => a.Equals(b);
    public static bool operator !=(LuminousIntensity a, LuminousIntensity b)
      => !a.Equals(b);

    public static LuminousIntensity operator -(LuminousIntensity v)
      => new(-v.m_value);
    public static LuminousIntensity operator +(LuminousIntensity a, double b)
      => new(a.m_value + b);
    public static LuminousIntensity operator +(LuminousIntensity a, LuminousIntensity b)
      => a + b.m_value;
    public static LuminousIntensity operator /(LuminousIntensity a, double b)
      => new(a.m_value / b);
    public static LuminousIntensity operator /(LuminousIntensity a, LuminousIntensity b)
      => a / b.m_value;
    public static LuminousIntensity operator *(LuminousIntensity a, double b)
      => new(a.m_value * b);
    public static LuminousIntensity operator *(LuminousIntensity a, LuminousIntensity b)
      => a * b.m_value;
    public static LuminousIntensity operator %(LuminousIntensity a, double b)
      => new(a.m_value % b);
    public static LuminousIntensity operator %(LuminousIntensity a, LuminousIntensity b)
      => a % b.m_value;
    public static LuminousIntensity operator -(LuminousIntensity a, double b)
      => new(a.m_value - b);
    public static LuminousIntensity operator -(LuminousIntensity a, LuminousIntensity b)
      => a - b.m_value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(LuminousIntensity other)
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
    public bool Equals(LuminousIntensity other)
      => m_value == other.m_value;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is LuminousIntensity o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
    public override string ToString()
      => $"{GetType().Name} {{ Value = {ToUnitString()} }}";
    #endregion Object overrides
  }
}
