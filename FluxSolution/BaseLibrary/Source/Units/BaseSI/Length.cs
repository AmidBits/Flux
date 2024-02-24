namespace Flux
{
  public static partial class Em
  {
    public static string GetUnitString(this Units.LengthUnit unit, bool preferUnicode = false, bool useFullName = false)
      => useFullName ? unit.ToString() : unit switch
      {
        Units.LengthUnit.Femtometer => preferUnicode ? "\u3399" : "fm",
        Units.LengthUnit.Nanometer => preferUnicode ? "\u339A" : "nm",
        Units.LengthUnit.Micrometer => preferUnicode ? "\u339B" : "µm",
        Units.LengthUnit.Millimeter => preferUnicode ? "\u339C" : "mm",
        Units.LengthUnit.Centimeter => preferUnicode ? "\u339D" : "cm",
        Units.LengthUnit.Inch => preferUnicode ? "\u33CC" : "in",
        Units.LengthUnit.Decimeter => preferUnicode ? "\u3377" : "dm",
        Units.LengthUnit.Foot => "ft",
        Units.LengthUnit.Yard => "yd",
        Units.LengthUnit.Meter => "m",
        Units.LengthUnit.Kilometer => preferUnicode ? "\u339E" : "km",
        Units.LengthUnit.Mile => "mi",
        Units.LengthUnit.NauticalMile => "NM", // There is no single internationally agreed symbol. Others used are "N", "NM", "nmi" and "nm".
        Units.LengthUnit.AstronomicalUnit => preferUnicode ? "\u3373" : "au",
        Units.LengthUnit.Parsec => preferUnicode ? "\u3376" : "pc",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Units
  {
    public enum LengthUnit
    {
      /// <summary>This is the default unit for <see cref="Length"/>.</summary>
      Meter,
      Femtometer,
      Nanometer,
      Micrometer,
      Millimeter,
      Centimeter,
      Inch,
      Decimeter,
      Foot,
      Yard,
      Kilometer,
      Mile,
      NauticalMile,
      AstronomicalUnit,
      Parsec,
    }

    /// <summary>Length. SI unit of meter. This is a base quantity.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Length"/>
    public readonly record struct Length
      : System.IComparable, System.IComparable<Length>, System.IFormattable, IMetricMultiplicable<double>, IUnitValueQuantifiable<double, LengthUnit>
    {
      public const double OneParsecInMeters = 30856775814913672;

      private readonly double m_value;

      public Length(double value, LengthUnit unit = LengthUnit.Meter)
        => m_value = unit switch
        {
          LengthUnit.Meter => value,

          LengthUnit.Inch => ConvertInchToMeter(value),
          LengthUnit.Foot => ConvertFootToMeter(value),
          LengthUnit.Yard => ConvertYardToMeter(value),
          LengthUnit.Mile => ConvertMileToMeter(value),
          LengthUnit.NauticalMile => ConvertNauticalMileToMeter(value),
          LengthUnit.AstronomicalUnit => ConvertAstronomicalUnitToMeter(value),
          LengthUnit.Parsec => ConvertParsecToMeter(value),

          LengthUnit.Femtometer => Flux.Units.MetricPrefix.Femto.Convert(value, Flux.Units.MetricPrefix.Count), // value * 1E-15,
          LengthUnit.Nanometer => Flux.Units.MetricPrefix.Nano.Convert(value, Flux.Units.MetricPrefix.Count), // value * 1E-9,
          LengthUnit.Micrometer => Flux.Units.MetricPrefix.Micro.Convert(value, Flux.Units.MetricPrefix.Count), // value * 1E-6,
          LengthUnit.Millimeter => Flux.Units.MetricPrefix.Milli.Convert(value, Flux.Units.MetricPrefix.Count), // value * 1E-3,
          LengthUnit.Centimeter => Flux.Units.MetricPrefix.Centi.Convert(value, Flux.Units.MetricPrefix.Count), // value * 1E-2,
          LengthUnit.Decimeter => Flux.Units.MetricPrefix.Deci.Convert(value, Flux.Units.MetricPrefix.Count), // value * 1E-1,
          LengthUnit.Kilometer => Flux.Units.MetricPrefix.Kilo.Convert(value, Flux.Units.MetricPrefix.Count), // value * 1E+3,

          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public Length(MetricPrefix prefix, double meter) => m_value = prefix.Convert(meter, MetricPrefix.Count);

      #region Static methods

      #region Conversion methods

      public static double ConvertAstronomicalUnitToMeter(double astronomicalUnit) => astronomicalUnit * 149597870700;
      public static double ConvertFootToMeter(double foot) => foot * 0.3048;
      public static double ConvertInchToMeter(double inch) => inch * 0.0254;
      public static double ConvertMeterToAstronomicalUnit(double meter) => meter / 149597870700;
      public static double ConvertMeterToFoot(double meter) => meter / 0.3048;
      public static double ConvertMeterToInch(double meter) => meter / 0.0254;
      public static double ConvertMeterToMile(double meter) => meter / 1609.344;
      public static double ConvertMeterToNauticalMile(double meter) => meter / 1852;
      public static double ConvertMeterToParsec(double meter) => meter / 30856775814913672;
      public static double ConvertMeterToYard(double meter) => meter / 0.9144;
      public static double ConvertMileToMeter(double mile) => mile * 1609.344;
      public static double ConvertNauticalMileToMeter(double nauticalMile) => nauticalMile * 1852;
      public static double ConvertParsecToMeter(double parsec) => parsec * 30856775814913672;
      public static double ConvertYardToMeter(double yard) => yard * 0.9144;

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
      public static explicit operator double(Length v) => v.m_value;
      public static explicit operator Length(double v) => new(v);

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
        => ToUnitValueString(LengthUnit.Meter, UnitValueStringOptions.Default with { Format = format, FormatProvider = formatProvider });

      //IMetricMultiplicable<>
      public double ToMetricValue(MetricPrefix prefix) => MetricPrefix.Count.Convert(m_value, prefix);

      public string ToMetricValueString(MetricPrefix prefix, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing spacing = UnicodeSpacing.NarrowNoBreakSpace)
      {
        var sb = new System.Text.StringBuilder();
        sb.Append(ToMetricValue(prefix).ToString(format, formatProvider));
        sb.Append(spacing.ToSpacingString());
        sb.Append(prefix.GetUnitString(true, false));
        sb.Append(LengthUnit.Meter.GetUnitString(false, false));
        return sb.ToString();
      }

      // IQuantifiable<>
      /// <summary>
      /// <para>The unit of the <see cref="Length.Value"/> property is in <see cref="LengthUnit.Meter"/>.</para>
      /// </summary>
      public double Value => m_value;

      // IUnitQuantifiable<>
      public double GetUnitValue(LengthUnit unit)
        => unit switch
        {
          LengthUnit.Meter => m_value,

          LengthUnit.Femtometer => m_value * 1E+15,
          LengthUnit.Nanometer => m_value * 1E+9,
          LengthUnit.Micrometer => m_value * 1E+6,
          LengthUnit.Millimeter => m_value * 1E+3,
          LengthUnit.Centimeter => m_value * 1E+2,
          LengthUnit.Decimeter => m_value * 1E+1,
          LengthUnit.Kilometer => m_value * 1E-3,

          LengthUnit.Inch => m_value / 0.0254,
          LengthUnit.Foot => m_value / 0.3048,
          LengthUnit.Yard => m_value / 0.9144,
          LengthUnit.Mile => m_value / 1609.344,
          LengthUnit.NauticalMile => m_value / 1852,
          LengthUnit.AstronomicalUnit => m_value / 149597870700,
          LengthUnit.Parsec => m_value / OneParsecInMeters,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(LengthUnit unit, UnitValueStringOptions options = default)
      {
        var sb = new System.Text.StringBuilder();
        sb.Append(GetUnitValue(unit).ToString(options.Format, options.FormatProvider));
        sb.Append(options.UnitSpacing.ToSpacingString());
        sb.Append(unit.GetUnitString(options.PreferUnicode, options.UseFullName));
        return sb.ToString();
      }

      #endregion Implemented interfaces
    }
  }
}
