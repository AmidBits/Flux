namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static string GetUnitString(this VolumeUnit unit, bool useNameInsteadOfSymbol = false, bool useUnicodeIfAvailable = false)
      => useNameInsteadOfSymbol ? unit.ToString() : unit switch
      {
        VolumeUnit.Millilitre => "ml",
        VolumeUnit.Centilitre => "cl",
        VolumeUnit.Decilitre => "dl",
        VolumeUnit.Litre => "l",
        VolumeUnit.ImperialGallon => "gal (imp)",
        VolumeUnit.ImperialQuart => "qt (imp)",
        VolumeUnit.USGallon => "gal (US)",
        VolumeUnit.USQuart => "qt (US)",
        VolumeUnit.CubicFeet => "ft³",
        VolumeUnit.CubicYard => "yd³",
        VolumeUnit.CubicMeter => "m³",
        VolumeUnit.CubicMile => "mi³",
        VolumeUnit.CubicKilometer => "km³",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  public enum VolumeUnit
  {
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
    CubicMeter,
    CubicMile,
    CubicKilometer,
  }

  /// <summary>Volume, unit of cubic meter. This is an SI derived quantity.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Volume"/>
  public struct Volume
    : System.IComparable<Volume>, System.IConvertible, System.IEquatable<Volume>, ISiDerivedUnitQuantifiable<double, VolumeUnit>
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

    public double Value
      => m_value;

    public string ToUnitString(VolumeUnit unit = DefaultUnit, string? format = null, bool useFullName = false, bool preferUnicode = false)
      => $"{string.Format($"{{0:{(format is null ? string.Empty : $":{format}")}}}", ToUnitValue(unit))} {unit.GetUnitString()}";
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

    #region Static methods
    /// <summary>Creates a new Volumne instance from the specified rectangular length, width and height.</summary>
    /// <param name="length"></param>
    /// <param name="width"></param>
    /// <param name="height"></param>
    public static Volume From(Length length, Length width, Length height)
      => new(length.Value * width.Value * height.Value);
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator double(Volume v)
      => v.m_value;
    public static explicit operator Volume(double v)
      => new(v);

    public static bool operator <(Volume a, Volume b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(Volume a, Volume b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(Volume a, Volume b)
      => a.CompareTo(b) > 0;
    public static bool operator >=(Volume a, Volume b)
      => a.CompareTo(b) >= 0;

    public static bool operator ==(Volume a, Volume b)
      => a.Equals(b);
    public static bool operator !=(Volume a, Volume b)
      => !a.Equals(b);

    public static Volume operator -(Volume v)
      => new(-v.m_value);
    public static Volume operator +(Volume a, double b)
      => new(a.m_value + b);
    public static Volume operator +(Volume a, Volume b)
      => a + b.m_value;
    public static Volume operator /(Volume a, double b)
      => new(a.m_value / b);
    public static Volume operator /(Volume a, Volume b)
      => a / b.m_value;
    public static Volume operator *(Volume a, double b)
      => new(a.m_value * b);
    public static Volume operator *(Volume a, Volume b)
      => a * b.m_value;
    public static Volume operator %(Volume a, double b)
      => new(a.m_value % b);
    public static Volume operator %(Volume a, Volume b)
      => a % b.m_value;
    public static Volume operator -(Volume a, double b)
      => new(a.m_value - b);
    public static Volume operator -(Volume a, Volume b)
      => a - b.m_value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(Volume other)
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
    public bool Equals(Volume other)
      => m_value == other.m_value;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Volume o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
    public override string ToString()
      => $"{GetType().Name} {{ Value = {ToUnitString()} }}";
    #endregion Object overrides
  }
}
