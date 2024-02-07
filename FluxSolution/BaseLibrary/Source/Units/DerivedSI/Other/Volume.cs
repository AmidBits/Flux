namespace Flux
{
  public static partial class Em
  {
    public static string GetUnitString(this Units.VolumeUnit unit, QuantifiableValueStringOptions options)
      => options.UseFullName ? unit.ToString() : unit switch
      {
        Units.VolumeUnit.Microlitre => options.PreferUnicode ? "\u3395" : "µl",
        Units.VolumeUnit.Millilitre => options.PreferUnicode ? "\u3396" : "ml",
        Units.VolumeUnit.Centilitre => "cl",
        Units.VolumeUnit.Decilitre => options.PreferUnicode ? "\u3397" : "dl",
        Units.VolumeUnit.Litre => "l",
        Units.VolumeUnit.ImperialGallon => options.PreferUnicode ? "\u33FF" : "gal (imp)",
        Units.VolumeUnit.ImperialQuart => "qt (imp)",
        Units.VolumeUnit.USGallon => options.PreferUnicode ? "\u33FF" : "gal (US)",
        Units.VolumeUnit.USQuart => "qt (US)",
        Units.VolumeUnit.CubicFeet => "ft³",
        Units.VolumeUnit.CubicYard => "yd³",
        Units.VolumeUnit.CubicMeter => options.PreferUnicode ? "\u33A5" : "m³",
        Units.VolumeUnit.CubicMile => "mi³",
        Units.VolumeUnit.CubicKilometer => options.PreferUnicode ? "\u33A6" : "km³",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Units
  {
    public enum VolumeUnit
    {
      /// <summary>This is the default unit for <see cref="Volume"/>.</summary>
      CubicMeter,
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
    /// <see href="https://en.wikipedia.org/wiki/Volume"/>
    public readonly record struct Volume
      : System.IComparable, System.IComparable<Volume>, System.IFormattable, IUnitValueQuantifiable<double, VolumeUnit>
    {
      private readonly double m_value;

      public Volume(double value, VolumeUnit unit = VolumeUnit.CubicMeter)
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

      /// <summary>Creates a new <see cref="Volume"/> instance from the specified cuboid.</summary>
      /// <param name="length">The length of a cuboid.</param>
      /// <param name="width">The width of a cuboid.</param>
      /// <param name="height">The height of a cuboid.</param>
      public static Volume OfCuboid(Length length, Length width, Length height)
        => new(length.Value * width.Value * height.Value);

      /// <summary>Creates a new <see cref="Volume"/> instance from the specified cylinder.</summary>
      /// <param name="radius">The radius of a cylinder.</param>
      /// <param name="height">The height of a cylinder.</param>
      public static Volume OfCylinder(Length radius, Length height)
        => new(System.Math.PI * System.Math.Pow(radius.Value, 2) * height.Value);

      /// <summary>Creates a new <see cref="Volume"/> instance from the specified sphere.</summary>
      /// <param name="radius">The radius of a sphere.</param>
      public static Volume OfSphere(Length radius)
        => new(4d / 3d * System.Math.PI * System.Math.Pow(radius.Value, 3));

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
      public string ToString(string? format, System.IFormatProvider? formatProvider) => ToValueString(QuantifiableValueStringOptions.Default with { Format = format, FormatProvider = formatProvider });

      // IQuantifiable<>
      public string ToValueString(QuantifiableValueStringOptions options) => ToUnitValueString(VolumeUnit.CubicMeter, options);

      /// <summary>
      ///  <para>The unit of the <see cref="Volume.Value"/> property is in <see cref="VolumeUnit.CubicMeter"/>.</para>
      /// </summary>
      public double Value => m_value;

      // IUnitQuantifiable<>
      public double GetUnitValue(VolumeUnit unit)
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

      public string ToUnitValueString(VolumeUnit unit, QuantifiableValueStringOptions options)
        => $"{string.Format(options.CultureInfo, $"{{0{(options.Format is null ? string.Empty : $":{options.Format}")}}}", GetUnitValue(unit))} {unit.GetUnitString(options)}";

      #endregion Implemented interfaces

      public override string ToString() => ToValueString(QuantifiableValueStringOptions.Default);
    }
  }
}
