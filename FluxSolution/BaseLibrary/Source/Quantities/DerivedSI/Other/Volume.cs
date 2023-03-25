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
        Quantities.VolumeUnit.ImperialGallon => preferUnicode ? "\u33FF" : "gal (imp)",
        Quantities.VolumeUnit.ImperialQuart => "qt (imp)",
        Quantities.VolumeUnit.USGallon => preferUnicode ? "\u33FF" : "gal (US)",
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
    public readonly record struct Volume
      : System.IComparable, System.IComparable<Volume>, System.IFormattable, IUnitQuantifiable<double, VolumeUnit>
    {
      public static readonly Volume Zero;

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

      // IComparable
      public int CompareTo(object? other) => other is not null && other is Volume o ? CompareTo(o) : -1;

      // IComparable<>
      public int CompareTo(Volume other) => m_value.CompareTo(other.m_value);

      // IFormattable
      public string ToString(string? format, IFormatProvider? formatProvider) => m_value.ToString(format, formatProvider);

      // IQuantifiable<>
      public string ToQuantityString(string? format = null, bool preferUnicode = false, bool useFullName = false) => ToUnitString(DefaultUnit, format, preferUnicode, useFullName);
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

      public override string ToString() => ToQuantityString();
    }
  }
}
