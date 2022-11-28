namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static string GetUnitString(this LuminousIntensityUnit unit, bool preferUnicode, bool useFullName = false)
      => useFullName ? unit.ToString() : unit switch
      {
        LuminousIntensityUnit.Candela => preferUnicode ? "\u33C5" : "cd",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  public enum LuminousIntensityUnit
  {
    Candela,
  }

  /// <summary>Luminous intensity. SI unit of candela. This is a base quantity.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Luminous_intensity"/>
  public readonly struct LuminousIntensity
    : System.IComparable, System.IComparable<LuminousIntensity>, System.IConvertible, System.IEquatable<LuminousIntensity>, System.IFormattable, IUnitQuantifiable<double, LuminousIntensityUnit>
  {
    public const LuminousIntensityUnit DefaultUnit = LuminousIntensityUnit.Candela;

    private readonly double m_value;

    public LuminousIntensity(double value, LuminousIntensityUnit unit = DefaultUnit)
      => m_value = unit switch
      {
        LuminousIntensityUnit.Candela => value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    #region Static methods
    #endregion Static methods

    #region Overloaded operators
    [System.Diagnostics.Contracts.Pure] public static explicit operator double(LuminousIntensity v) => v.m_value;
    [System.Diagnostics.Contracts.Pure] public static explicit operator LuminousIntensity(double v) => new(v);

    [System.Diagnostics.Contracts.Pure] public static bool operator <(LuminousIntensity a, LuminousIntensity b) => a.CompareTo(b) < 0;
    [System.Diagnostics.Contracts.Pure] public static bool operator <=(LuminousIntensity a, LuminousIntensity b) => a.CompareTo(b) <= 0;
    [System.Diagnostics.Contracts.Pure] public static bool operator >(LuminousIntensity a, LuminousIntensity b) => a.CompareTo(b) > 0;
    [System.Diagnostics.Contracts.Pure] public static bool operator >=(LuminousIntensity a, LuminousIntensity b) => a.CompareTo(b) >= 0;

    [System.Diagnostics.Contracts.Pure] public static bool operator ==(LuminousIntensity a, LuminousIntensity b) => a.Equals(b);
    [System.Diagnostics.Contracts.Pure] public static bool operator !=(LuminousIntensity a, LuminousIntensity b) => !a.Equals(b);

    [System.Diagnostics.Contracts.Pure] public static LuminousIntensity operator -(LuminousIntensity v) => new(-v.m_value);
    [System.Diagnostics.Contracts.Pure] public static LuminousIntensity operator +(LuminousIntensity a, double b) => new(a.m_value + b);
    [System.Diagnostics.Contracts.Pure] public static LuminousIntensity operator +(LuminousIntensity a, LuminousIntensity b) => a + b.m_value;
    [System.Diagnostics.Contracts.Pure] public static LuminousIntensity operator /(LuminousIntensity a, double b) => new(a.m_value / b);
    [System.Diagnostics.Contracts.Pure] public static LuminousIntensity operator /(LuminousIntensity a, LuminousIntensity b) => a / b.m_value;
    [System.Diagnostics.Contracts.Pure] public static LuminousIntensity operator *(LuminousIntensity a, double b) => new(a.m_value * b);
    [System.Diagnostics.Contracts.Pure] public static LuminousIntensity operator *(LuminousIntensity a, LuminousIntensity b) => a * b.m_value;
    [System.Diagnostics.Contracts.Pure] public static LuminousIntensity operator %(LuminousIntensity a, double b) => new(a.m_value % b);
    [System.Diagnostics.Contracts.Pure] public static LuminousIntensity operator %(LuminousIntensity a, LuminousIntensity b) => a % b.m_value;
    [System.Diagnostics.Contracts.Pure] public static LuminousIntensity operator -(LuminousIntensity a, double b) => new(a.m_value - b);
    [System.Diagnostics.Contracts.Pure] public static LuminousIntensity operator -(LuminousIntensity a, LuminousIntensity b) => a - b.m_value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable<>
    [System.Diagnostics.Contracts.Pure] public int CompareTo(LuminousIntensity other) => m_value.CompareTo(other.m_value);
    // IComparable
    [System.Diagnostics.Contracts.Pure] public int CompareTo(object? other) => other is not null && other is LuminousIntensity o ? CompareTo(o) : -1;

    #region IConvertible
    [System.Diagnostics.Contracts.Pure] public System.TypeCode GetTypeCode() => System.TypeCode.Object;
    [System.Diagnostics.Contracts.Pure] public bool ToBoolean(System.IFormatProvider? provider) => Value != 0;
    [System.Diagnostics.Contracts.Pure] public byte ToByte(System.IFormatProvider? provider) => System.Convert.ToByte(Value);
    [System.Diagnostics.Contracts.Pure] public char ToChar(System.IFormatProvider? provider) => System.Convert.ToChar(Value);
    [System.Diagnostics.Contracts.Pure] public System.DateTime ToDateTime(System.IFormatProvider? provider) => System.Convert.ToDateTime(Value);
    [System.Diagnostics.Contracts.Pure] public decimal ToDecimal(System.IFormatProvider? provider) => System.Convert.ToDecimal(Value);
    [System.Diagnostics.Contracts.Pure] public double ToDouble(System.IFormatProvider? provider) => System.Convert.ToDouble(Value);
    [System.Diagnostics.Contracts.Pure] public short ToInt16(System.IFormatProvider? provider) => System.Convert.ToInt16(Value);
    [System.Diagnostics.Contracts.Pure] public int ToInt32(System.IFormatProvider? provider) => System.Convert.ToInt32(Value);
    [System.Diagnostics.Contracts.Pure] public long ToInt64(System.IFormatProvider? provider) => System.Convert.ToInt64(Value);
    [System.CLSCompliant(false)][System.Diagnostics.Contracts.Pure] public sbyte ToSByte(System.IFormatProvider? provider) => System.Convert.ToSByte(Value);
    [System.Diagnostics.Contracts.Pure] public float ToSingle(System.IFormatProvider? provider) => System.Convert.ToSingle(Value);
    [System.Diagnostics.Contracts.Pure] public string ToString(System.IFormatProvider? provider) => string.Format(provider, "{0}", Value);
    [System.Diagnostics.Contracts.Pure] public object ToType(System.Type conversionType, System.IFormatProvider? provider) => System.Convert.ChangeType(Value, conversionType, provider);
    [System.CLSCompliant(false)][System.Diagnostics.Contracts.Pure] public ushort ToUInt16(System.IFormatProvider? provider) => System.Convert.ToUInt16(Value);
    [System.CLSCompliant(false)][System.Diagnostics.Contracts.Pure] public uint ToUInt32(System.IFormatProvider? provider) => System.Convert.ToUInt32(Value);
    [System.CLSCompliant(false)][System.Diagnostics.Contracts.Pure] public ulong ToUInt64(System.IFormatProvider? provider) => System.Convert.ToUInt64(Value);
    #endregion IConvertible

    // IEquatable<>
    [System.Diagnostics.Contracts.Pure] public bool Equals(LuminousIntensity other) => m_value == other.m_value;

    // IFormattable
    [System.Diagnostics.Contracts.Pure] public string ToString(string? format, IFormatProvider? formatProvider) => m_value.ToString(format, formatProvider);

    // IQuantifiable<>
    [System.Diagnostics.Contracts.Pure] public double Value { get => m_value; init => m_value = value; }
    // IUnitQuantifiable<>
    [System.Diagnostics.Contracts.Pure]
    public string ToUnitString(LuminousIntensityUnit unit = DefaultUnit, string? format = null, bool preferUnicode = false, bool useFullName = false)
      => $"{string.Format($"{{0{(format is null ? string.Empty : $":{format}")}}}", ToUnitValue(unit))} {unit.GetUnitString(preferUnicode, useFullName)}";
    [System.Diagnostics.Contracts.Pure]
    public double ToUnitValue(LuminousIntensityUnit unit = DefaultUnit)
      => unit switch
      {
        LuminousIntensityUnit.Candela => m_value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
    #endregion Implemented interfaces

    #region Object overrides
    [System.Diagnostics.Contracts.Pure] public override bool Equals(object? obj) => obj is LuminousIntensity o && Equals(o);
    [System.Diagnostics.Contracts.Pure] public override int GetHashCode() => m_value.GetHashCode();
    [System.Diagnostics.Contracts.Pure] public override string ToString() => $"{GetType().Name} {{ Value = {ToUnitString()} }}";
    #endregion Object overrides
  }
}
