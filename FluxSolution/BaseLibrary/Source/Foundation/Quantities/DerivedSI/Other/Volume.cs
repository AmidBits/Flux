namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static string GetUnitString(this VolumeUnit unit, bool useFullName = false, bool preferUnicode = false)
      => useFullName ? unit.ToString() : unit switch
      {
        VolumeUnit.Millilitre => "ml",
        VolumeUnit.Centilitre => "cl",
        VolumeUnit.Decilitre => "dl",
        VolumeUnit.Litre => "l",
        VolumeUnit.ImperialGallon => "gal (imp)",
        VolumeUnit.ImperialQuart => "qt (imp)",
        VolumeUnit.USGallon => "gal (US)",
        VolumeUnit.USQuart => "qt (US)",
        VolumeUnit.CubicFeet => "ft�",
        VolumeUnit.CubicYard => "yd�",
        VolumeUnit.CubicMeter => "m�",
        VolumeUnit.CubicMile => "mi�",
        VolumeUnit.CubicKilometer => "km�",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  public enum VolumeUnit
  {
    CubicMeter, // DefaultUnit first for actual instatiation defaults.
    Millilitre,
    Centilitre,
    Decilitre,
    Litre,
    /// <summary>British unit.</summary>
    ImperialGallon,
    /// <summary>British unit.</summary>
    ImperialQuart,
    /// <summary>US unit.</summary>
    USGallon,
    /// <summary>US unit.</summary>
    USQuart,
    CubicFeet,
    CubicYard,
    CubicMile,
    CubicKilometer,
  }

  /// <summary>Volume, unit of cubic meter. This is an SI derived quantity.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Volume"/>
  public struct Volume
    : System.IComparable, System.IComparable<Volume>, System.IConvertible, System.IEquatable<Volume>, System.IFormattable, ISiDerivedUnitQuantifiable<double, VolumeUnit>
  {
    public const VolumeUnit DefaultUnit = VolumeUnit.CubicMeter;

    private readonly double m_value;

    public Volume(double value, VolumeUnit unit = DefaultUnit)
      => m_value = unit switch
      {
        VolumeUnit.Millilitre => value / 1000000,
        VolumeUnit.Centilitre => value / 100000,
        VolumeUnit.Decilitre => value / 10000,
        VolumeUnit.Litre => value / 1000,
        VolumeUnit.ImperialGallon => value * 0.004546,
        VolumeUnit.ImperialQuart => value / 879.87699319635,
        VolumeUnit.USGallon => value * 0.003785,
        VolumeUnit.USQuart => value / 1056.68821,// Approximate.
        VolumeUnit.CubicFeet => value / (1953125000.0 / 55306341.0),
        VolumeUnit.CubicYard => value / (1953125000.0 / 1493271207.0),
        VolumeUnit.CubicMeter => value,
        VolumeUnit.CubicMile => value * (8140980127813632.0 / 1953125.0),// 
        VolumeUnit.CubicKilometer => value * 1e9,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    #region Static methods
    /// <summary>Creates a new Volumne instance from the specified rectangular length, width and height.</summary>
    /// <param name="length"></param>
    /// <param name="width"></param>
    /// <param name="height"></param>
    [System.Diagnostics.Contracts.Pure]
    public static Volume From(Length length, Length width, Length height)
      => new(length.Value * width.Value * height.Value);
    #endregion Static methods

    #region Overloaded operators
    [System.Diagnostics.Contracts.Pure] public static explicit operator double(Volume v) => v.m_value;
    [System.Diagnostics.Contracts.Pure] public static explicit operator Volume(double v) => new(v);

    [System.Diagnostics.Contracts.Pure] public static bool operator <(Volume a, Volume b) => a.CompareTo(b) < 0;
    [System.Diagnostics.Contracts.Pure] public static bool operator <=(Volume a, Volume b) => a.CompareTo(b) <= 0;
    [System.Diagnostics.Contracts.Pure] public static bool operator >(Volume a, Volume b) => a.CompareTo(b) > 0;
    [System.Diagnostics.Contracts.Pure] public static bool operator >=(Volume a, Volume b) => a.CompareTo(b) >= 0;

    [System.Diagnostics.Contracts.Pure] public static bool operator ==(Volume a, Volume b) => a.Equals(b);
    [System.Diagnostics.Contracts.Pure] public static bool operator !=(Volume a, Volume b) => !a.Equals(b);

    [System.Diagnostics.Contracts.Pure] public static Volume operator -(Volume v) => new(-v.m_value);
    [System.Diagnostics.Contracts.Pure] public static Volume operator +(Volume a, double b) => new(a.m_value + b);
    [System.Diagnostics.Contracts.Pure] public static Volume operator +(Volume a, Volume b) => a + b.m_value;
    [System.Diagnostics.Contracts.Pure] public static Volume operator /(Volume a, double b) => new(a.m_value / b);
    [System.Diagnostics.Contracts.Pure] public static Volume operator /(Volume a, Volume b) => a / b.m_value;
    [System.Diagnostics.Contracts.Pure] public static Volume operator *(Volume a, double b) => new(a.m_value * b);
    [System.Diagnostics.Contracts.Pure] public static Volume operator *(Volume a, Volume b) => a * b.m_value;
    [System.Diagnostics.Contracts.Pure] public static Volume operator %(Volume a, double b) => new(a.m_value % b);
    [System.Diagnostics.Contracts.Pure] public static Volume operator %(Volume a, Volume b) => a % b.m_value;
    [System.Diagnostics.Contracts.Pure] public static Volume operator -(Volume a, double b) => new(a.m_value - b);
    [System.Diagnostics.Contracts.Pure] public static Volume operator -(Volume a, Volume b) => a - b.m_value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable<>
    [System.Diagnostics.Contracts.Pure] public int CompareTo(Volume other) => m_value.CompareTo(other.m_value);
    // IComparable
    [System.Diagnostics.Contracts.Pure] public int CompareTo(object? other) => other is not null && other is Volume o ? CompareTo(o) : -1;

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
    [System.Diagnostics.Contracts.Pure] public bool Equals(Volume other) => m_value == other.m_value;

    // IFormattable
    [System.Diagnostics.Contracts.Pure] public string ToString(string? format, IFormatProvider? formatProvider) => m_value.ToString(format, formatProvider);

    // IMetricOneQuantifiable
    [System.Diagnostics.Contracts.Pure]
    public string ToMetricOneString(MetricMultiplicativePrefix prefix, string? format = null, bool useFullName = false, bool preferUnicode = false)
       => $"{new MetricMultiplicative(m_value, MetricMultiplicativePrefix.One).ToUnitString(prefix, format, useFullName, preferUnicode)}{DefaultUnit.GetUnitString(useFullName, preferUnicode)}";

    // ISiDerivedUnitQuantifiable<>
    [System.Diagnostics.Contracts.Pure]
    public double Value
      => m_value;
    [System.Diagnostics.Contracts.Pure]
    public string ToUnitString(VolumeUnit unit = DefaultUnit, string? format = null, bool useFullName = false, bool preferUnicode = false)
      => $"{string.Format($"{{0{(format is null ? string.Empty : $":{format}")}}}", ToUnitValue(unit))} {unit.GetUnitString()}";
    [System.Diagnostics.Contracts.Pure]
    public double ToUnitValue(VolumeUnit unit = DefaultUnit)
      => unit switch
      {
        VolumeUnit.Millilitre => m_value * 1000000,
        VolumeUnit.Centilitre => m_value * 100000,
        VolumeUnit.Decilitre => m_value * 10000,
        VolumeUnit.Litre => m_value * 1000,
        VolumeUnit.ImperialGallon => m_value / 0.004546,
        VolumeUnit.ImperialQuart => m_value * 879.87699319635,
        VolumeUnit.USGallon => m_value / 0.003785,
        VolumeUnit.USQuart => m_value * 1056.68821,// Approximate.
        VolumeUnit.CubicFeet => m_value * (1953125000.0 / 55306341.0),
        VolumeUnit.CubicYard => m_value * (1953125000.0 / 1493271207.0),
        VolumeUnit.CubicMeter => m_value,
        VolumeUnit.CubicMile => m_value / (8140980127813632.0 / 1953125.0),
        VolumeUnit.CubicKilometer => m_value / 1e9,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
    #endregion Implemented interfaces

    #region Object overrides
    [System.Diagnostics.Contracts.Pure] public override bool Equals(object? obj) => obj is Volume o && Equals(o);
    [System.Diagnostics.Contracts.Pure] public override int GetHashCode() => m_value.GetHashCode();
    [System.Diagnostics.Contracts.Pure] public override string ToString() => $"{GetType().Name} {{ Value = {ToUnitString()} }}";
    #endregion Object overrides
  }
}
