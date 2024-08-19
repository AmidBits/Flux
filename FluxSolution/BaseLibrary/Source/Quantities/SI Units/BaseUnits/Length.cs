namespace Flux.Quantities
{
  public enum LengthUnit
  {
    /// <summary>This is the default unit for <see cref="Length"/>.</summary>
    Metre,
    Femtometre,
    Nanometre,
    Micrometre,
    Millimetre,
    Centimetre,
    Inch,
    Decimetre,
    Foot,
    Yard,
    Kilometre,
    Mile,
    NauticalMile,
    AstronomicalUnit,
    Parsec,
    Ångström,
  }

  /// <summary>
  /// <para>Length. SI unit of meter. This is a base quantity.</para>
  /// <see href="https://en.wikipedia.org/wiki/Length"/>
  /// </summary>
  /// <remarks>Dimensional relationship: <see cref="Length"/>, <see cref="Area"/> and <see cref="Volume"/>.</remarks>
  public readonly record struct Length
    : System.IComparable, System.IComparable<Length>, System.IFormattable, ISiPrefixValueQuantifiable<double, LengthUnit>
  {
    public const double OneParsecInMeters = 30856775814913672;

    private readonly double m_value;

    public Length(double value, LengthUnit unit = LengthUnit.Metre)
      => m_value = unit switch
      {
        LengthUnit.Metre => value,

        LengthUnit.Inch => ConvertInchToMetre(value),
        LengthUnit.Foot => ConvertFootToMetre(value),
        LengthUnit.Yard => ConvertYardToMetre(value),
        LengthUnit.Mile => ConvertMileToMetre(value),
        LengthUnit.NauticalMile => ConvertNauticalMileToMetre(value),
        LengthUnit.AstronomicalUnit => ConvertAstronomicalUnitToMetre(value),
        LengthUnit.Parsec => ConvertParsecToMetre(value),
        LengthUnit.Ångström => ConvertÅngströmToMetre(value),

        LengthUnit.Femtometre => Flux.Quantities.MetricPrefix.Femto.Convert(value, Flux.Quantities.MetricPrefix.NoPrefix), // value * 1E-15,
        LengthUnit.Nanometre => Flux.Quantities.MetricPrefix.Nano.Convert(value, Flux.Quantities.MetricPrefix.NoPrefix), // value * 1E-9,
        LengthUnit.Micrometre => Flux.Quantities.MetricPrefix.Micro.Convert(value, Flux.Quantities.MetricPrefix.NoPrefix), // value * 1E-6,
        LengthUnit.Millimetre => Flux.Quantities.MetricPrefix.Milli.Convert(value, Flux.Quantities.MetricPrefix.NoPrefix), // value * 1E-3,
        LengthUnit.Centimetre => Flux.Quantities.MetricPrefix.Centi.Convert(value, Flux.Quantities.MetricPrefix.NoPrefix), // value * 1E-2,
        LengthUnit.Decimetre => Flux.Quantities.MetricPrefix.Deci.Convert(value, Flux.Quantities.MetricPrefix.NoPrefix), // value * 1E-1,
        LengthUnit.Kilometre => Flux.Quantities.MetricPrefix.Kilo.Convert(value, Flux.Quantities.MetricPrefix.NoPrefix), // value * 1E+3,

        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    /// <summary>
    /// <para>Creates a new instance from the specified <see cref="MetricPrefix"/> (metric multiple) of <see cref="TimeUnit.Meter"/>, e.g. <see cref="MetricPrefix.Centi"/> for centimeter.</para>
    /// </summary>
    /// <param name="metres"></param>
    /// <param name="prefix"></param>
    public Length(double metres, MetricPrefix prefix) => m_value = prefix.Convert(metres, MetricPrefix.NoPrefix);

    /// <summary>
    /// <para>Computes the wavelength from the specified phase velocity and frequency. A wavelength is the spatial period of a periodic wave, i.e. the distance over which the wave's shape repeats. The default reference value for the speed of sound is 343.21 m/s. This determines the unit of measurement (i.e. meters per second) for the wavelength distance.</para>
    /// </summary>
    /// <param name="phaseVelocity">The constant speed of the traveling wave. If these are sound waves then typically this is the speed of sound. If electromagnetic radiation (e.g. light) in free space then speed of light.</param>
    /// <param name="frequency"></param>
    /// <returns>The wavelength of the frequency cycle at the phase velocity.</returns>
    /// <see href="https://en.wikipedia.org/wiki/Wavelength"/>
    public Length(Speed phaseVelocity, Frequency frequency) : this(phaseVelocity.Value / frequency.Value) { }

    #region Static methods

    #region Conversion methods

    public static double ConvertAstronomicalUnitToMetre(double astronomicalUnit) => astronomicalUnit * 149597870700;
    public static double ConvertFootToMetre(double foot) => foot * 0.3048;
    public static double ConvertInchToMetre(double inch) => inch * 0.0254;
    public static double ConvertMetreToAstronomicalUnit(double meter) => meter / 149597870700;
    public static double ConvertMetreToFoot(double meter) => meter / 0.3048;
    public static double ConvertMetreToInch(double meter) => meter / 0.0254;
    public static double ConvertMetreToMile(double meter) => meter / 1609.344;
    public static double ConvertMetreToNauticalMile(double meter) => meter / 1852;
    public static double ConvertMetreToParsec(double meter) => meter / 30856775814913672;
    public static double ConvertMetreToYard(double meter) => meter / 0.9144;
    public static double ConvertMetreToÅngström(double meter) => meter * 10000000000;
    public static double ConvertMileToMetre(double mile) => mile * 1609.344;
    public static double ConvertNauticalMileToMetre(double nauticalMile) => nauticalMile * 1852;
    public static double ConvertParsecToMetre(double parsec) => parsec * 30856775814913672;
    public static double ConvertYardToMetre(double yard) => yard * 0.9144;
    public static double ConvertÅngströmToMetre(double ångström) => ångström * 10000000000;

    #endregion // Conversion methods

    /// <summary>Creates a new <see cref="Length"/> instance from <see cref="Speed"/> and <see cref="AngularFrequency"/></summary>
    /// <param name="speed"></param>
    /// <param name="angularVelocity"></param>
    public static Length From(Speed speed, AngularFrequency angularVelocity) => new(speed.Value / angularVelocity.Value);

    #endregion Static methods

    #region Overloaded operators

    public static bool operator <(Length a, Length b) => a.CompareTo(b) < 0;
    public static bool operator <=(Length a, Length b) => a.CompareTo(b) <= 0;
    public static bool operator >(Length a, Length b) => a.CompareTo(b) > 0;
    public static bool operator >=(Length a, Length b) => a.CompareTo(b) >= 0;

    public static Length operator -(Length v) => new(-v.m_value);
    public static Length operator +(Length a, double b) => new(a.m_value + b);
    public static Length operator +(Length a, Length b) => a + b.m_value;
    public static Length operator /(Length a, double b) => new(a.m_value / b);
    public static Length operator /(Length a, Length b) => a / b.m_value;
    public static Length operator *(Length a, double b) => new(a.m_value * b);
    public static Length operator *(Length a, Length b) => a * b.m_value;
    public static Length operator %(Length a, double b) => new(a.m_value % b);
    public static Length operator %(Length a, Length b) => a % b.m_value;
    public static Length operator -(Length a, double b) => new(a.m_value - b);
    public static Length operator -(Length a, Length b) => a - b.m_value;

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is Length o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(Length other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider)
      => ToUnitValueSymbolString(LengthUnit.Metre, format, formatProvider);

    // ISiUnitValueQuantifiable<>
    public (MetricPrefix Prefix, LengthUnit Unit) GetSiPrefixUnit(MetricPrefix prefix) => (prefix, LengthUnit.Metre);

    public string GetSiPrefixSymbol(MetricPrefix prefix, bool preferUnicode) => prefix.GetUnitSymbol(preferUnicode) + GetUnitSymbol(GetSiPrefixUnit(prefix).Unit, preferUnicode);

    public double GetSiPrefixValue(MetricPrefix prefix) => MetricPrefix.NoPrefix.Convert(m_value, prefix);

    public string ToSiPrefixValueSymbolString(MetricPrefix prefix, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferUnicode = false)
    {
      var sb = new System.Text.StringBuilder();
      sb.Append(GetSiPrefixValue(prefix).ToString(format, formatProvider));
      sb.Append(unitSpacing.ToSpacingString());
      sb.Append(GetSiPrefixSymbol(prefix, preferUnicode));
      return sb.ToString();
    }

    // IQuantifiable<>
    /// <summary>
    /// <para>The unit of the <see cref="Length.Value"/> property is in <see cref="LengthUnit.Metre"/>.</para>
    /// </summary>
    public double Value => m_value;

    // IUnitQuantifiable<>
    public string GetUnitName(LengthUnit unit, bool preferPlural)
      => unit.ToString() is var us && preferPlural ? us + GetUnitValue(unit).PluralStringSuffix() : us;

    public string GetUnitSymbol(LengthUnit unit, bool preferUnicode)
     => unit switch
     {
       Quantities.LengthUnit.Metre => "m",
       Quantities.LengthUnit.Femtometre => preferUnicode ? "\u3399" : "fm",
       Quantities.LengthUnit.Nanometre => preferUnicode ? "\u339A" : "nm",
       Quantities.LengthUnit.Micrometre => preferUnicode ? "\u339B" : "µm",
       Quantities.LengthUnit.Millimetre => preferUnicode ? "\u339C" : "mm",
       Quantities.LengthUnit.Centimetre => preferUnicode ? "\u339D" : "cm",
       Quantities.LengthUnit.Inch => preferUnicode ? "\u33CC" : "in",
       Quantities.LengthUnit.Decimetre => preferUnicode ? "\u3377" : "dm",
       Quantities.LengthUnit.Foot => "ft",
       Quantities.LengthUnit.Yard => "yd",
       Quantities.LengthUnit.Kilometre => preferUnicode ? "\u339E" : "km",
       Quantities.LengthUnit.Mile => "mi",
       Quantities.LengthUnit.NauticalMile => "NM", // There is no single internationally agreed symbol. Others used are "N", "NM", "nmi" and "nm".
       Quantities.LengthUnit.AstronomicalUnit => preferUnicode ? "\u3373" : "au",
       Quantities.LengthUnit.Parsec => preferUnicode ? "\u3376" : "pc",
       Quantities.LengthUnit.Ångström => preferUnicode ? "\u212B" : "Å",
       _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
     };

    public double GetUnitValue(LengthUnit unit)
        => unit switch
        {
          LengthUnit.Metre => m_value,

          LengthUnit.Inch => ConvertMetreToInch(m_value),
          LengthUnit.Foot => ConvertMetreToFoot(m_value),
          LengthUnit.Yard => ConvertMetreToYard(m_value),
          LengthUnit.Mile => ConvertMetreToMile(m_value),
          LengthUnit.NauticalMile => ConvertMetreToNauticalMile(m_value),
          LengthUnit.AstronomicalUnit => ConvertMetreToAstronomicalUnit(m_value),
          LengthUnit.Parsec => ConvertMetreToParsec(m_value),
          LengthUnit.Ångström => m_value * 10000000000,

          LengthUnit.Femtometre => m_value * 1E+15,
          LengthUnit.Nanometre => m_value * 1E+9,
          LengthUnit.Micrometre => m_value * 1E+6,
          LengthUnit.Millimetre => m_value * 1E+3,
          LengthUnit.Centimetre => m_value * 1E+2,
          LengthUnit.Decimetre => m_value * 1E+1,
          LengthUnit.Kilometre => m_value * 1E-3,

          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

    public string ToUnitValueNameString(LengthUnit unit = LengthUnit.Metre, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferPlural = false)
      => string.Concat(GetUnitValue(unit).ToString(format, formatProvider), unitSpacing.ToSpacingString(), GetUnitName(unit, preferPlural));

    public string ToUnitValueSymbolString(LengthUnit unit = LengthUnit.Metre, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferUnicode = false)
      => string.Concat(GetUnitValue(unit).ToString(format, formatProvider), unitSpacing.ToSpacingString(), GetUnitSymbol(unit, preferUnicode));

    #endregion Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
