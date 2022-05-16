namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static string GetUnitString(this RadioactivityUnit unit, bool useFullName = false, bool preferUnicode = false)
      => useFullName ? unit.ToString() : unit switch
      {
        RadioactivityUnit.Becquerel => "Bq",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  public enum RadioactivityUnit
  {
    /// <summary>Becquerel.</summary>
    Becquerel,
  }

  /// <summary>Radioactivity unit of becquerel.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Radioactivity"/>
  public struct Radioactivity
    : System.IComparable, System.IComparable<Radioactivity>, System.IConvertible, System.IEquatable<Radioactivity>, System.IFormattable, ISiDerivedUnitQuantifiable<double, RadioactivityUnit>
  {
    public const RadioactivityUnit DefaultUnit = RadioactivityUnit.Becquerel;

    private readonly double m_value;

    public Radioactivity(double value, RadioactivityUnit unit = DefaultUnit)
      => m_value = unit switch
      {
        RadioactivityUnit.Becquerel => value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    #region Overloaded operators
    [System.Diagnostics.Contracts.Pure] public static explicit operator double(Radioactivity v) => v.m_value;
    [System.Diagnostics.Contracts.Pure] public static explicit operator Radioactivity(double v) => new(v);

    [System.Diagnostics.Contracts.Pure] public static bool operator <(Radioactivity a, Radioactivity b) => a.CompareTo(b) < 0;
    [System.Diagnostics.Contracts.Pure] public static bool operator <=(Radioactivity a, Radioactivity b) => a.CompareTo(b) <= 0;
    [System.Diagnostics.Contracts.Pure] public static bool operator >(Radioactivity a, Radioactivity b) => a.CompareTo(b) > 0;
    [System.Diagnostics.Contracts.Pure] public static bool operator >=(Radioactivity a, Radioactivity b) => a.CompareTo(b) >= 0;

    [System.Diagnostics.Contracts.Pure] public static bool operator ==(Radioactivity a, Radioactivity b) => a.Equals(b);
    [System.Diagnostics.Contracts.Pure] public static bool operator !=(Radioactivity a, Radioactivity b) => !a.Equals(b);

    [System.Diagnostics.Contracts.Pure] public static Radioactivity operator -(Radioactivity v) => new(-v.m_value);
    [System.Diagnostics.Contracts.Pure] public static Radioactivity operator +(Radioactivity a, double b) => new(a.m_value + b);
    [System.Diagnostics.Contracts.Pure] public static Radioactivity operator +(Radioactivity a, Radioactivity b) => a + b.m_value;
    [System.Diagnostics.Contracts.Pure] public static Radioactivity operator /(Radioactivity a, double b) => new(a.m_value / b);
    [System.Diagnostics.Contracts.Pure] public static Radioactivity operator /(Radioactivity a, Radioactivity b) => a / b.m_value;
    [System.Diagnostics.Contracts.Pure] public static Radioactivity operator *(Radioactivity a, double b) => new(a.m_value * b);
    [System.Diagnostics.Contracts.Pure] public static Radioactivity operator *(Radioactivity a, Radioactivity b) => a * b.m_value;
    [System.Diagnostics.Contracts.Pure] public static Radioactivity operator %(Radioactivity a, double b) => new(a.m_value % b);
    [System.Diagnostics.Contracts.Pure] public static Radioactivity operator %(Radioactivity a, Radioactivity b) => a % b.m_value;
    [System.Diagnostics.Contracts.Pure] public static Radioactivity operator -(Radioactivity a, double b) => new(a.m_value - b);
    [System.Diagnostics.Contracts.Pure] public static Radioactivity operator -(Radioactivity a, Radioactivity b) => a - b.m_value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable<>
    [System.Diagnostics.Contracts.Pure] public int CompareTo(Radioactivity other) => m_value.CompareTo(other.m_value);
    // IComparable
    [System.Diagnostics.Contracts.Pure] public int CompareTo(object? other) => other is not null && other is Radioactivity o ? CompareTo(o) : -1;

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
    [System.Diagnostics.Contracts.Pure] public bool Equals(Radioactivity other) => m_value == other.m_value;

    // IFormattable
    [System.Diagnostics.Contracts.Pure] public string ToString(string? format, IFormatProvider? formatProvider) => m_value.ToString(format, formatProvider);

    //// IMetricOneQuantifiable
    //[System.Diagnostics.Contracts.Pure]
    //public string ToMetricOneString(MetricMultiplicativePrefix prefix, string? format = null, bool useFullName = false, bool preferUnicode = false)
    //  => $"{new MetricMultiplicative(m_value, MetricMultiplicativePrefix.One).ToUnitString(prefix, format, useFullName, preferUnicode)}{DefaultUnit.GetUnitString(useFullName, preferUnicode)}";

    // ISiDerivedUnitQuantifiable<>
    [System.Diagnostics.Contracts.Pure]
    public double Value
      => m_value;
    [System.Diagnostics.Contracts.Pure]
    public string ToUnitString(RadioactivityUnit unit = DefaultUnit, string? format = null, bool useFullName = false, bool preferUnicode = false)
      => $"{string.Format($"{{0{(format is null ? string.Empty : $":{format}")}}}", ToUnitValue(unit))} {unit.GetUnitString()}";
    [System.Diagnostics.Contracts.Pure]
    public double ToUnitValue(RadioactivityUnit unit = DefaultUnit)
      => unit switch
      {
        RadioactivityUnit.Becquerel => m_value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
    #endregion Implemented interfaces

    #region Object overrides
    [System.Diagnostics.Contracts.Pure] public override bool Equals(object? obj) => obj is Radioactivity o && Equals(o);
    [System.Diagnostics.Contracts.Pure] public override int GetHashCode() => m_value.GetHashCode();
    [System.Diagnostics.Contracts.Pure] public override string ToString() => $"{GetType().Name} {{ Value = {ToUnitString()} }}";
    #endregion Object overrides
  }
}
