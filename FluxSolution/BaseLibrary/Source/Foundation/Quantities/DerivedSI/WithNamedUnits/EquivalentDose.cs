namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static string GetUnitString(this EquivalentDoseUnit unit, bool preferUnicode, bool useFullName = false)
      => useFullName ? unit.ToString() : unit switch
      {
        EquivalentDoseUnit.Sievert => "Sv",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  public enum EquivalentDoseUnit
  {
    /// <summary>Sievert.</summary>
    Sievert,
  }

  /// <summary>Dose equivalent, unit of sievert.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Equivalent_dose"/>
  public readonly struct EquivalentDose
    : System.IComparable, System.IComparable<EquivalentDose>, System.IConvertible, System.IEquatable<EquivalentDose>, System.IFormattable, IUnitQuantifiable<double, EquivalentDoseUnit>
  {
    public const EquivalentDoseUnit DefaultUnit = EquivalentDoseUnit.Sievert;

    private readonly double m_value;

    public EquivalentDose(double value, EquivalentDoseUnit unit = DefaultUnit)
      => m_value = unit switch
      {
        EquivalentDoseUnit.Sievert => value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    #region Overloaded operators
    [System.Diagnostics.Contracts.Pure] public static explicit operator double(EquivalentDose v) => v.m_value;
    [System.Diagnostics.Contracts.Pure] public static explicit operator EquivalentDose(double v) => new(v);

    [System.Diagnostics.Contracts.Pure] public static bool operator <(EquivalentDose a, EquivalentDose b) => a.CompareTo(b) < 0;
    [System.Diagnostics.Contracts.Pure] public static bool operator <=(EquivalentDose a, EquivalentDose b) => a.CompareTo(b) <= 0;
    [System.Diagnostics.Contracts.Pure] public static bool operator >(EquivalentDose a, EquivalentDose b) => a.CompareTo(b) > 0;
    [System.Diagnostics.Contracts.Pure] public static bool operator >=(EquivalentDose a, EquivalentDose b) => a.CompareTo(b) >= 0;

    [System.Diagnostics.Contracts.Pure] public static bool operator ==(EquivalentDose a, EquivalentDose b) => a.Equals(b);
    [System.Diagnostics.Contracts.Pure] public static bool operator !=(EquivalentDose a, EquivalentDose b) => !a.Equals(b);

    [System.Diagnostics.Contracts.Pure] public static EquivalentDose operator -(EquivalentDose v) => new(-v.m_value);
    [System.Diagnostics.Contracts.Pure] public static EquivalentDose operator +(EquivalentDose a, double b) => new(a.m_value + b);
    [System.Diagnostics.Contracts.Pure] public static EquivalentDose operator +(EquivalentDose a, EquivalentDose b) => a + b.m_value;
    [System.Diagnostics.Contracts.Pure] public static EquivalentDose operator /(EquivalentDose a, double b) => new(a.m_value / b);
    [System.Diagnostics.Contracts.Pure] public static EquivalentDose operator /(EquivalentDose a, EquivalentDose b) => a / b.m_value;
    [System.Diagnostics.Contracts.Pure] public static EquivalentDose operator *(EquivalentDose a, double b) => new(a.m_value * b);
    [System.Diagnostics.Contracts.Pure] public static EquivalentDose operator *(EquivalentDose a, EquivalentDose b) => a * b.m_value;
    [System.Diagnostics.Contracts.Pure] public static EquivalentDose operator %(EquivalentDose a, double b) => new(a.m_value % b);
    [System.Diagnostics.Contracts.Pure] public static EquivalentDose operator %(EquivalentDose a, EquivalentDose b) => a % b.m_value;
    [System.Diagnostics.Contracts.Pure] public static EquivalentDose operator -(EquivalentDose a, double b) => new(a.m_value - b);
    [System.Diagnostics.Contracts.Pure] public static EquivalentDose operator -(EquivalentDose a, EquivalentDose b) => a - b.m_value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable<>
    [System.Diagnostics.Contracts.Pure] public int CompareTo(EquivalentDose other) => m_value.CompareTo(other.m_value);
    // IComparable
    [System.Diagnostics.Contracts.Pure] public int CompareTo(object? other) => other is not null && other is EquivalentDose o ? CompareTo(o) : -1;

    #region IConvertible
    [System.Diagnostics.Contracts.Pure] public System.TypeCode GetTypeCode() => System.TypeCode.Object;
    [System.Diagnostics.Contracts.Pure] public bool ToBoolean(System.IFormatProvider? provider) => m_value != 0;
    [System.Diagnostics.Contracts.Pure] public byte ToByte(System.IFormatProvider? provider) => System.Convert.ToByte(m_value);
    [System.Diagnostics.Contracts.Pure] public char ToChar(System.IFormatProvider? provider) => System.Convert.ToChar(m_value);
    [System.Diagnostics.Contracts.Pure] public System.DateTime ToDateTime(System.IFormatProvider? provider) => System.Convert.ToDateTime(m_value);
    [System.Diagnostics.Contracts.Pure] public decimal ToDecimal(System.IFormatProvider? provider) => System.Convert.ToDecimal(m_value);
    [System.Diagnostics.Contracts.Pure] public double ToDouble(System.IFormatProvider? provider) => System.Convert.ToDouble(m_value);
    [System.Diagnostics.Contracts.Pure] public short ToInt16(System.IFormatProvider? provider) => System.Convert.ToInt16(m_value);
    [System.Diagnostics.Contracts.Pure] public int ToInt32(System.IFormatProvider? provider) => System.Convert.ToInt32(m_value);
    [System.Diagnostics.Contracts.Pure] public long ToInt64(System.IFormatProvider? provider) => System.Convert.ToInt64(m_value);
    [System.CLSCompliant(false)][System.Diagnostics.Contracts.Pure] public sbyte ToSByte(System.IFormatProvider? provider) => System.Convert.ToSByte(m_value);
    [System.Diagnostics.Contracts.Pure] public float ToSingle(System.IFormatProvider? provider) => System.Convert.ToSingle(m_value);
    [System.Diagnostics.Contracts.Pure] public string ToString(System.IFormatProvider? provider) => string.Format(provider, "{0}", m_value);
    [System.Diagnostics.Contracts.Pure] public object ToType(System.Type conversionType, System.IFormatProvider? provider) => System.Convert.ChangeType(m_value, conversionType, provider);
    [System.CLSCompliant(false)][System.Diagnostics.Contracts.Pure] public ushort ToUInt16(System.IFormatProvider? provider) => System.Convert.ToUInt16(m_value);
    [System.CLSCompliant(false)][System.Diagnostics.Contracts.Pure] public uint ToUInt32(System.IFormatProvider? provider) => System.Convert.ToUInt32(m_value);
    [System.CLSCompliant(false)][System.Diagnostics.Contracts.Pure] public ulong ToUInt64(System.IFormatProvider? provider) => System.Convert.ToUInt64(m_value);
    #endregion IConvertible

    // IEquatable<>
    [System.Diagnostics.Contracts.Pure] public bool Equals(EquivalentDose other) => m_value == other.m_value;

    // IFormattable
    [System.Diagnostics.Contracts.Pure] public string ToString(string? format, IFormatProvider? formatProvider) => m_value.ToString(format, formatProvider);

    // IQuantifiable<>
    [System.Diagnostics.Contracts.Pure] public double Value { get => m_value; init => m_value = value; }
    // IUnitQuantifiable<>
    [System.Diagnostics.Contracts.Pure]
    public string ToUnitString(EquivalentDoseUnit unit = DefaultUnit, string? format = null, bool preferUnicode = false, bool useFullName = false)
      => $"{string.Format($"{{0{(format is null ? string.Empty : $":{format}")}}}", ToUnitValue(unit))} {unit.GetUnitString(preferUnicode, useFullName)}";
    [System.Diagnostics.Contracts.Pure]
    public double ToUnitValue(EquivalentDoseUnit unit = DefaultUnit)
      => unit switch
      {
        EquivalentDoseUnit.Sievert => m_value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
    #endregion Implemented interfaces

    #region Object overrides
    [System.Diagnostics.Contracts.Pure] public override bool Equals(object? obj) => obj is EquivalentDose o && Equals(o);
    [System.Diagnostics.Contracts.Pure] public override int GetHashCode() => m_value.GetHashCode();
    [System.Diagnostics.Contracts.Pure] public override string ToString() => $"{GetType().Name} {{ Value = {ToUnitString()} }}";
    #endregion Object overrides
  }
}
