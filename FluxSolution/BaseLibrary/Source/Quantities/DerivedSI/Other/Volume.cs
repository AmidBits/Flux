namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static string GetUnitString(this Quantities.VolumeUnit unit, bool preferUnicode, bool useFullName = false)
      => useFullName ? unit.ToString() : unit switch
      {
        Quantities.VolumeUnit.Microlitre => preferUnicode ? "\u3395" : "µl",
        Quantities.VolumeUnit.Millilitre => preferUnicode ? "\u3396" : "ml",
        Quantities.VolumeUnit.Centilitre => "cl",
        Quantities.VolumeUnit.Decilitre => preferUnicode ? "\u3397" : "dl",
        Quantities.VolumeUnit.Litre => "l",
        Quantities.VolumeUnit.ImperialGallon => "gal (imp)",
        Quantities.VolumeUnit.ImperialQuart => "qt (imp)",
        Quantities.VolumeUnit.USGallon => "gal (US)",
        Quantities.VolumeUnit.USQuart => "qt (US)",
        Quantities.VolumeUnit.CubicFeet => "ft³",
        Quantities.VolumeUnit.CubicYard => "yd³",
        Quantities.VolumeUnit.CubicMeter => preferUnicode ? "\u33A5" : "m³",
        Quantities.VolumeUnit.CubicMile => "mi³",
        Quantities.VolumeUnit.CubicKilometer => preferUnicode ? "\u33A6" : "km³",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Quantities
  {
    public enum VolumeUnit
    {
      CubicMeter, // DefaultUnit first for actual instatiation defaults.
      Microlitre,
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
    public readonly struct Volume
      : System.IComparable, System.IComparable<Volume>, System.IConvertible, System.IEquatable<Volume>, System.IFormattable, IUnitQuantifiable<double, VolumeUnit>
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

      public static Volume From(Length length, Length width, Length height)
        => new(length.Value * width.Value * height.Value);
      #endregion Static methods

      #region Overloaded operators
      public static explicit operator double(Volume v) => v.m_value;
      public static explicit operator Volume(double v) => new(v);

      public static bool operator <(Volume a, Volume b) => a.CompareTo(b) < 0;
      public static bool operator <=(Volume a, Volume b) => a.CompareTo(b) <= 0;
      public static bool operator >(Volume a, Volume b) => a.CompareTo(b) > 0;
      public static bool operator >=(Volume a, Volume b) => a.CompareTo(b) >= 0;

      public static bool operator ==(Volume a, Volume b) => a.Equals(b);
      public static bool operator !=(Volume a, Volume b) => !a.Equals(b);

      public static Volume operator -(Volume v) => new(-v.m_value);
      public static Volume operator +(Volume a, double b) => new(a.m_value + b);
      public static Volume operator +(Volume a, Volume b) => a + b.m_value;
      public static Volume operator /(Volume a, double b) => new(a.m_value / b);
      public static Volume operator /(Volume a, Volume b) => a / b.m_value;
      public static Volume operator *(Volume a, double b) => new(a.m_value * b);
      public static Volume operator *(Volume a, Volume b) => a * b.m_value;
      public static Volume operator %(Volume a, double b) => new(a.m_value % b);
      public static Volume operator %(Volume a, Volume b) => a % b.m_value;
      public static Volume operator -(Volume a, double b) => new(a.m_value - b);
      public static Volume operator -(Volume a, Volume b) => a - b.m_value;
      #endregion Overloaded operators

      #region Implemented interfaces
      // IComparable<>
      public int CompareTo(Volume other) => m_value.CompareTo(other.m_value);
      // IComparable
      public int CompareTo(object? other) => other is not null && other is Volume o ? CompareTo(o) : -1;

      #region IConvertible
      public System.TypeCode GetTypeCode() => System.TypeCode.Object;
      public bool ToBoolean(System.IFormatProvider? provider) => m_value != 0;
      public byte ToByte(System.IFormatProvider? provider) => System.Convert.ToByte(m_value);
      public char ToChar(System.IFormatProvider? provider) => System.Convert.ToChar(m_value);
      public System.DateTime ToDateTime(System.IFormatProvider? provider) => System.Convert.ToDateTime(m_value);
      public decimal ToDecimal(System.IFormatProvider? provider) => System.Convert.ToDecimal(m_value);
      public double ToDouble(System.IFormatProvider? provider) => System.Convert.ToDouble(m_value);
      public short ToInt16(System.IFormatProvider? provider) => System.Convert.ToInt16(m_value);
      public int ToInt32(System.IFormatProvider? provider) => System.Convert.ToInt32(m_value);
      public long ToInt64(System.IFormatProvider? provider) => System.Convert.ToInt64(m_value);
      [System.CLSCompliant(false)] public sbyte ToSByte(System.IFormatProvider? provider) => System.Convert.ToSByte(m_value);
      public float ToSingle(System.IFormatProvider? provider) => System.Convert.ToSingle(m_value);
      public string ToString(System.IFormatProvider? provider) => string.Format(provider, "{0}", m_value);
      public object ToType(System.Type conversionType, System.IFormatProvider? provider) => System.Convert.ChangeType(m_value, conversionType, provider);
      [System.CLSCompliant(false)] public ushort ToUInt16(System.IFormatProvider? provider) => System.Convert.ToUInt16(m_value);
      [System.CLSCompliant(false)] public uint ToUInt32(System.IFormatProvider? provider) => System.Convert.ToUInt32(m_value);
      [System.CLSCompliant(false)] public ulong ToUInt64(System.IFormatProvider? provider) => System.Convert.ToUInt64(m_value);
      #endregion IConvertible

      // IEquatable<>
      public bool Equals(Volume other) => m_value == other.m_value;

      // IFormattable
      public string ToString(string? format, IFormatProvider? formatProvider) => m_value.ToString(format, formatProvider);

      // IQuantifiable<>
      public string ToQuantityString(string? format = null, bool preferUnicode = false, bool useFullName = false)
        => ToUnitString(DefaultUnit, format, preferUnicode, useFullName);

      public double Value { get => m_value; init => m_value = value; }

      // IUnitQuantifiable<>

      public string ToUnitString(VolumeUnit unit, string? format = null, bool preferUnicode = false, bool useFullName = false)
        => $"{string.Format($"{{0{(format is null ? string.Empty : $":{format}")}}}", ToUnitValue(unit))} {unit.GetUnitString(preferUnicode, useFullName)}";

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
      public override bool Equals(object? obj) => obj is Volume o && Equals(o);
      public override int GetHashCode() => m_value.GetHashCode();
      public override string ToString() => $"{GetType().Name} {{ {ToQuantityString()} }}";
      #endregion Object overrides
    }
  }
}
