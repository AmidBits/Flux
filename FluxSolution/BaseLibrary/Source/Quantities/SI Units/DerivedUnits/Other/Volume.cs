namespace Flux.Quantities
{
  public enum VolumeUnit
  {
    /// <summary>This is the default unit for <see cref="Volume"/>.</summary>
    CubicMeter,
    //Microliter,
    Milliliter,
    Centiliter,
    Deciliter,
    Liter,
    /// <summary>British unit.</summary>
    UKGallon,
    /// <summary>British unit.</summary>
    UKQuart,
    /// <summary>US unit.</summary>
    USLiquidGallon,
    /// <summary>US unit.</summary>
    USDryGallon,
    /// <summary>US unit.</summary>
    USDryQuart,
    /// <summary>US unit.</summary>
    USLiquidQuart,
    CubicFoot,
    CubicYard,
    CubicMile,
    CubicKilometer,
  }

  /// <summary>
  /// <para>Volume, unit of cubic meter. This is an SI derived quantity.</para>
  /// <see href="https://en.wikipedia.org/wiki/Volume"/>
  /// </summary>
  /// <remarks>Dimensional relationship: <see cref="Length"/>, <see cref="Area"/> and <see cref="Volume"/>.</remarks>
  public readonly record struct Volume
    : System.IComparable, System.IComparable<Volume>, System.IFormattable, IUnitValueQuantifiable<double, VolumeUnit>
  {
    private readonly double m_value;

    public Volume(double value, VolumeUnit unit = VolumeUnit.CubicMeter) => m_value = ConvertFromUnit(unit, value);
    //=> m_value = unit switch
    //{
    //  VolumeUnit.Milliliter => value / 1000000,
    //  VolumeUnit.Centiliter => value / 100000,
    //  VolumeUnit.Deciliter => value / 10000,
    //  VolumeUnit.Liter => value / 1000,
    //  VolumeUnit.UKGallon => value * 0.004546,
    //  VolumeUnit.UKQuart => value / 879.87699319635,
    //  VolumeUnit.USDryGallon => value * 0.0044,
    //  VolumeUnit.USLiquidGallon => value * 0.003785,
    //  VolumeUnit.USDryQuart => value / 0.00110122095, // Approximate.
    //  VolumeUnit.USLiquidQuart => value / 0.00094635295, // Approximate.
    //  VolumeUnit.CubicFoot => value / (1953125000.0 / 55306341.0),
    //  VolumeUnit.CubicYard => value / (1953125000.0 / 1493271207.0),
    //  VolumeUnit.CubicMeter => value,
    //  VolumeUnit.CubicMile => value * (8140980127813632.0 / 1953125.0),// 
    //  VolumeUnit.CubicKilometer => value * 1e9,
    //  _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
    //};

    #region Static methods

    /// <summary>Creates a new <see cref="Volume"/> instance from the specified cuboid.</summary>
    /// <param name="length">The length of a cuboid.</param>
    /// <param name="width">The width of a cuboid.</param>
    /// <param name="height">The height of a cuboid.</param>
    public static Volume OfCuboid(Length length, Length width, Length height) => new(length.Value * width.Value * height.Value);

    /// <summary>Creates a new <see cref="Volume"/> instance from the specified cylinder.</summary>
    /// <param name="radius">The radius of a cylinder.</param>
    /// <param name="height">The height of a cylinder.</param>
    public static Volume OfCylinder(Length radius, Length height) => new(System.Math.PI * System.Math.Pow(radius.Value, 2) * height.Value);

    /// <summary>Creates a new <see cref="Volume"/> instance from the specified sphere.</summary>
    /// <param name="radius">The radius of a sphere.</param>
    public static Volume OfSphere(Length radius) => new(4d / 3d * System.Math.PI * System.Math.Pow(radius.Value, 3));

    #endregion Static methods

    #region Overloaded operators

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
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToUnitString(VolumeUnit.CubicMeter, format, formatProvider);

    #region IQuantifiable<>

    /// <summary>
    ///  <para>The unit of the <see cref="Volume.Value"/> property is in <see cref="VolumeUnit.CubicMeter"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IQuantifiable<>

    #region ISiUnitValueQuantifiable<>

    public string GetSiPrefixName(MetricPrefix prefix, bool preferPlural) => GetUnitName(VolumeUnit.CubicMeter, preferPlural).Insert(5, prefix.GetPrefixName());

    public string GetSiPrefixSymbol(MetricPrefix prefix, bool preferUnicode) => prefix.GetPrefixSymbol(preferUnicode) + GetUnitSymbol(VolumeUnit.CubicMeter, preferUnicode);

    public double GetSiPrefixValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertTo(m_value, prefix, 3);

    public string ToSiPrefixValueNameString(MetricPrefix prefix, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferPlural = true)
      => GetSiPrefixValue(prefix).ToSiFormattedString() + unitSpacing.ToSpacingString() + GetSiPrefixName(prefix, preferPlural);

    public string ToSiPrefixValueSymbolString(MetricPrefix prefix, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferUnicode = false)
      => GetSiPrefixValue(prefix).ToSiFormattedString() + unitSpacing.ToSpacingString() + GetSiPrefixSymbol(prefix, preferUnicode);

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitQuantifiable<>

    private static double ConvertFromUnit(VolumeUnit unit, double value)
      => unit switch
      {
        VolumeUnit.CubicMeter => value,

        _ => GetUnitFactor(unit) * value,
      };

    private static double ConvertToUnit(VolumeUnit unit, double value)
      => unit switch
      {
        VolumeUnit.CubicMeter => value,

        _ => value / GetUnitFactor(unit),
      };

    public static double ConvertUnit(double value, VolumeUnit from, VolumeUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public static double GetUnitFactor(VolumeUnit unit)
      => unit switch
      {
        VolumeUnit.CubicMeter => 1,

        VolumeUnit.Milliliter => throw new NotImplementedException(),
        VolumeUnit.Centiliter => throw new NotImplementedException(),
        VolumeUnit.Deciliter => throw new NotImplementedException(),
        VolumeUnit.Liter => 1000,
        VolumeUnit.UKGallon => 219.96924829909,
        VolumeUnit.UKQuart => 879.87699319635,
        VolumeUnit.USDryGallon => 227.02074456538,
        VolumeUnit.USLiquidGallon => 264.17205124156,
        VolumeUnit.USDryQuart => 1 / 0.00110122095,
        VolumeUnit.USLiquidQuart => 1 / 0.00094635295,
        VolumeUnit.CubicFoot => (1953125000.0 / 55306341.0),
        VolumeUnit.CubicYard => (1953125000.0 / 1493271207.0),
        VolumeUnit.CubicMile => 1 / (8140980127813632.0 / 1953125.0),
        VolumeUnit.CubicKilometer => 1e9,
        _ => throw new System.NotImplementedException()
      };

    public string GetUnitName(VolumeUnit unit, bool preferPlural)
      => unit.ToString().ConvertUnitNameToPlural(preferPlural && GetUnitValue(unit).IsConsideredPlural());

    public string GetUnitSymbol(VolumeUnit unit, bool preferUnicode)
      => unit switch
      {
        //Units.VolumeUnit.Microliter => preferUnicode ? "\u3395" : "µl",
        Quantities.VolumeUnit.Milliliter => preferUnicode ? "\u3396" : "ml",
        Quantities.VolumeUnit.Centiliter => "cl",
        Quantities.VolumeUnit.Deciliter => preferUnicode ? "\u3397" : "dl",
        Quantities.VolumeUnit.Liter => "l",
        Quantities.VolumeUnit.UKGallon => preferUnicode ? "\u33FF" : "gal (UK)",
        Quantities.VolumeUnit.UKQuart => "qt (UK)",
        Quantities.VolumeUnit.USDryGallon => preferUnicode ? "\u33FF" : "gal (US-dry)",
        Quantities.VolumeUnit.USLiquidGallon => preferUnicode ? "\u33FF" : "gal (US-liquid)",
        Quantities.VolumeUnit.USDryQuart => "qt (US-dry)",
        Quantities.VolumeUnit.USLiquidQuart => "qt (US-liquid)",
        Quantities.VolumeUnit.CubicFoot => "ft³",
        Quantities.VolumeUnit.CubicYard => "yd³",
        Quantities.VolumeUnit.CubicMeter => preferUnicode ? "\u33A5" : "m³",
        Quantities.VolumeUnit.CubicMile => "mi³",
        Quantities.VolumeUnit.CubicKilometer => preferUnicode ? "\u33A6" : "km³",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(VolumeUnit unit) => ConvertFromUnit(unit, m_value);

    public string ToUnitString(VolumeUnit unit = VolumeUnit.CubicMeter, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + UnicodeSpacing.Space.ToSpacingString() + (fullName ? GetUnitName(unit, true) : GetUnitSymbol(unit, false));

    #endregion // IUnitQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
