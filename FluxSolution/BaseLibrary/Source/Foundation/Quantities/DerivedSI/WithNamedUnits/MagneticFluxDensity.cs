namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static string GetUnitString(this MagneticFluxDensityUnit unit, bool useFullName = false, bool preferUnicode = false)
      => useFullName ? unit.ToString() : unit switch
      {
        MagneticFluxDensityUnit.Tesla => "T",
        MagneticFluxDensityUnit.KilogramPerSquareSecond => "kg/s²",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  public enum MagneticFluxDensityUnit
  {
    /// <summary>Tesla.</summary>
    Tesla,
    KilogramPerSquareSecond
  }

  /// <summary>Magnetic flux density unit of tesla.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Magnetic_flux_density"/>
  public struct MagneticFluxDensity
    : System.IComparable<MagneticFluxDensity>, System.IConvertible, System.IEquatable<MagneticFluxDensity>, ISiDerivedUnitQuantifiable<double, MagneticFluxDensityUnit>
  {
    public const MagneticFluxDensityUnit DefaultUnit = MagneticFluxDensityUnit.Tesla;

    private readonly double m_value;

    public MagneticFluxDensity(double value, MagneticFluxDensityUnit unit = DefaultUnit)
      => m_value = unit switch
      {
        MagneticFluxDensityUnit.Tesla => value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    [System.Diagnostics.Contracts.Pure]
    public double Value
      => m_value;

    [System.Diagnostics.Contracts.Pure]
    public string ToMetricOneString(MetricMultiplicativePrefix prefix, string? format = null, bool useFullName = false, bool preferUnicode = false)
      => $"{ToMetricMultiplicative().ToUnitString(prefix, format, useFullName, preferUnicode)},{DefaultUnit.GetUnitString(useFullName, preferUnicode)}";
    [System.Diagnostics.Contracts.Pure]
    public MetricMultiplicative ToMetricMultiplicative()
      => new(ToUnitValue(DefaultUnit), MetricMultiplicativePrefix.One);
    [System.Diagnostics.Contracts.Pure]
    public string ToUnitString(MagneticFluxDensityUnit unit = DefaultUnit, string? format = null, bool useFullName = false, bool preferUnicode = false)
      => $"{string.Format($"{{0{(format is null ? string.Empty : $":{format}")}}}", ToUnitValue(unit))} {unit.GetUnitString()}";
    [System.Diagnostics.Contracts.Pure]
    public double ToUnitValue(MagneticFluxDensityUnit unit = DefaultUnit)
      => unit switch
      {
        MagneticFluxDensityUnit.Tesla => m_value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    #region Overloaded operators
    [System.Diagnostics.Contracts.Pure] public static explicit operator double(MagneticFluxDensity v) => v.m_value;
    [System.Diagnostics.Contracts.Pure] public static explicit operator MagneticFluxDensity(double v) => new(v);

    [System.Diagnostics.Contracts.Pure] public static bool operator <(MagneticFluxDensity a, MagneticFluxDensity b) => a.CompareTo(b) < 0;
    [System.Diagnostics.Contracts.Pure] public static bool operator <=(MagneticFluxDensity a, MagneticFluxDensity b) => a.CompareTo(b) <= 0;
    [System.Diagnostics.Contracts.Pure] public static bool operator >(MagneticFluxDensity a, MagneticFluxDensity b) => a.CompareTo(b) > 0;
    [System.Diagnostics.Contracts.Pure] public static bool operator >=(MagneticFluxDensity a, MagneticFluxDensity b) => a.CompareTo(b) >= 0;

    [System.Diagnostics.Contracts.Pure] public static bool operator ==(MagneticFluxDensity a, MagneticFluxDensity b) => a.Equals(b);
    [System.Diagnostics.Contracts.Pure] public static bool operator !=(MagneticFluxDensity a, MagneticFluxDensity b) => !a.Equals(b);

    [System.Diagnostics.Contracts.Pure] public static MagneticFluxDensity operator -(MagneticFluxDensity v) => new(-v.m_value);
    [System.Diagnostics.Contracts.Pure] public static MagneticFluxDensity operator +(MagneticFluxDensity a, double b) => new(a.m_value + b);
    [System.Diagnostics.Contracts.Pure] public static MagneticFluxDensity operator +(MagneticFluxDensity a, MagneticFluxDensity b) => a + b.m_value;
    [System.Diagnostics.Contracts.Pure] public static MagneticFluxDensity operator /(MagneticFluxDensity a, double b) => new(a.m_value / b);
    [System.Diagnostics.Contracts.Pure] public static MagneticFluxDensity operator /(MagneticFluxDensity a, MagneticFluxDensity b) => a / b.m_value;
    [System.Diagnostics.Contracts.Pure] public static MagneticFluxDensity operator *(MagneticFluxDensity a, double b) => new(a.m_value * b);
    [System.Diagnostics.Contracts.Pure] public static MagneticFluxDensity operator *(MagneticFluxDensity a, MagneticFluxDensity b) => a * b.m_value;
    [System.Diagnostics.Contracts.Pure] public static MagneticFluxDensity operator %(MagneticFluxDensity a, double b) => new(a.m_value % b);
    [System.Diagnostics.Contracts.Pure] public static MagneticFluxDensity operator %(MagneticFluxDensity a, MagneticFluxDensity b) => a % b.m_value;
    [System.Diagnostics.Contracts.Pure] public static MagneticFluxDensity operator -(MagneticFluxDensity a, double b) => new(a.m_value - b);
    [System.Diagnostics.Contracts.Pure] public static MagneticFluxDensity operator -(MagneticFluxDensity a, MagneticFluxDensity b) => a - b.m_value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    [System.Diagnostics.Contracts.Pure] public int CompareTo(MagneticFluxDensity other) => m_value.CompareTo(other.m_value);

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

    // IEquatable
    [System.Diagnostics.Contracts.Pure] public bool Equals(MagneticFluxDensity other) => m_value == other.m_value;
    #endregion Implemented interfaces

    #region Object overrides
    [System.Diagnostics.Contracts.Pure] public override bool Equals(object? obj) => obj is MagneticFluxDensity o && Equals(o);
    [System.Diagnostics.Contracts.Pure] public override int GetHashCode() => m_value.GetHashCode();
    [System.Diagnostics.Contracts.Pure] public override string ToString() => $"{GetType().Name} {{ Value = {ToUnitString()} }}";
    #endregion Object overrides
  }
}
