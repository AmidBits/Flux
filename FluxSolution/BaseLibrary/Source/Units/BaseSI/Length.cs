namespace Flux
{
  public static partial class Em
  {
    public static string GetUnitString(this Units.LengthUnit unit, bool preferUnicode = false, bool useFullName = false)
      => useFullName ? unit.ToString() : unit switch
      {
        Units.LengthUnit.Metre => "m",
        Units.LengthUnit.Femtometre => preferUnicode ? "\u3399" : "fm",
        Units.LengthUnit.Nanometre => preferUnicode ? "\u339A" : "nm",
        Units.LengthUnit.Micrometre => preferUnicode ? "\u339B" : "µm",
        Units.LengthUnit.Millimetre => preferUnicode ? "\u339C" : "mm",
        Units.LengthUnit.Centimetre => preferUnicode ? "\u339D" : "cm",
        Units.LengthUnit.Inch => preferUnicode ? "\u33CC" : "in",
        Units.LengthUnit.Decimetre => preferUnicode ? "\u3377" : "dm",
        Units.LengthUnit.Foot => "ft",
        Units.LengthUnit.Yard => "yd",
        Units.LengthUnit.Kilometre => preferUnicode ? "\u339E" : "km",
        Units.LengthUnit.Mile => "mi",
        Units.LengthUnit.NauticalMile => "NM", // There is no single internationally agreed symbol. Others used are "N", "NM", "nmi" and "nm".
        Units.LengthUnit.AstronomicalUnit => preferUnicode ? "\u3373" : "au",
        Units.LengthUnit.Parsec => preferUnicode ? "\u3376" : "pc",
        Units.LengthUnit.Ångström => preferUnicode ? "\u212B" : "Å",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Units
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

    /// <summary>Length. SI unit of meter. This is a base quantity.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Length"/>
    public readonly record struct Length
      : System.IComparable, System.IComparable<Length>, System.IFormattable, IMetricMultiplicable<double>, IUnitValueQuantifiable<double, LengthUnit>
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

          LengthUnit.Femtometre => Flux.Units.MetricPrefix.Femto.Convert(value, Flux.Units.MetricPrefix.Count), // value * 1E-15,
          LengthUnit.Nanometre => Flux.Units.MetricPrefix.Nano.Convert(value, Flux.Units.MetricPrefix.Count), // value * 1E-9,
          LengthUnit.Micrometre => Flux.Units.MetricPrefix.Micro.Convert(value, Flux.Units.MetricPrefix.Count), // value * 1E-6,
          LengthUnit.Millimetre => Flux.Units.MetricPrefix.Milli.Convert(value, Flux.Units.MetricPrefix.Count), // value * 1E-3,
          LengthUnit.Centimetre => Flux.Units.MetricPrefix.Centi.Convert(value, Flux.Units.MetricPrefix.Count), // value * 1E-2,
          LengthUnit.Decimetre => Flux.Units.MetricPrefix.Deci.Convert(value, Flux.Units.MetricPrefix.Count), // value * 1E-1,
          LengthUnit.Kilometre => Flux.Units.MetricPrefix.Kilo.Convert(value, Flux.Units.MetricPrefix.Count), // value * 1E+3,

          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public Length(MetricPrefix prefix, double meter) => m_value = prefix.Convert(meter, MetricPrefix.Count);

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
      public static Length From(Speed speed, AngularFrequency angularVelocity)
        => new(speed.Value / angularVelocity.Value);

      /// <summary>Computes the wavelength from the specified phase velocity and frequency. A wavelength is the spatial period of a periodic wave, i.e. the distance over which the wave's shape repeats. The default reference value for the speed of sound is 343.21 m/s. This determines the unit of measurement (i.e. meters per second) for the wavelength distance.</summary>
      /// <param name="phaseVelocity">The constant speed of the traveling wave. If these are sound waves then typically this is the speed of sound. If electromagnetic radiation (e.g. light) in free space then speed of light.</param>
      /// <param name="frequency"></param>
      /// <returns>The wavelength of the frequency cycle at the phase velocity.</returns>
      /// <see href="https://en.wikipedia.org/wiki/Wavelength"/>
      public static Length Wavelength(Speed phaseVelocity, Frequency frequency)
        => new(phaseVelocity.Value / frequency.Value);

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
        => ToUnitValueString(LengthUnit.Metre, format, formatProvider);

      //IMetricMultiplicable<>
      public double GetMetricValue(MetricPrefix prefix) => MetricPrefix.Count.Convert(m_value, prefix);

      public string ToMetricValueString(MetricPrefix prefix, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing spacing = UnicodeSpacing.None)
      {
        var sb = new System.Text.StringBuilder();
        sb.Append(GetMetricValue(prefix).ToString(format, formatProvider));
        sb.Append(spacing.ToSpacingString());
        sb.Append(prefix.GetUnitString(true, false));
        sb.Append(LengthUnit.Metre.GetUnitString(false, false));
        return sb.ToString();
      }

      // IQuantifiable<>
      /// <summary>
      /// <para>The unit of the <see cref="Length.Value"/> property is in <see cref="LengthUnit.Metre"/>.</para>
      /// </summary>
      public double Value => m_value;

      // IUnitQuantifiable<>
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

      public string ToUnitValueString(LengthUnit unit = LengthUnit.Metre, string? format = null, System.IFormatProvider? formatProvider = null, bool preferUnicode = false, UnicodeSpacing unicodeSpacing = UnicodeSpacing.None, bool useFullName = false)
      {
        var sb = new System.Text.StringBuilder();
        sb.Append(GetUnitValue(unit).ToString(format, formatProvider));
        sb.Append(unicodeSpacing.ToSpacingString());
        sb.Append(unit.GetUnitString(preferUnicode, useFullName));
        return sb.ToString();
      }

      #endregion Implemented interfaces

      public override string ToString() => ToString(null, null);
    }
  }
}
