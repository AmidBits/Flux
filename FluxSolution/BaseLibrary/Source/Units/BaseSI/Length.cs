namespace Flux
{
  public static partial class Em
  {
    public static string GetUnitString(this Units.LengthUnit unit, QuantifiableValueStringOptions options)
      => options.UseFullName ? unit.ToString() : unit switch
      {
        Units.LengthUnit.Femtometer => options.PreferUnicode ? "\u3399" : "fm",
        Units.LengthUnit.Nanometer => options.PreferUnicode ? "\u339A" : "nm",
        Units.LengthUnit.Micrometer => options.PreferUnicode ? "\u339B" : "µm",
        Units.LengthUnit.Millimeter => options.PreferUnicode ? "\u339C" : "mm",
        Units.LengthUnit.Centimeter => options.PreferUnicode ? "\u339D" : "cm",
        Units.LengthUnit.Inch => options.PreferUnicode ? "\u33CC" : "in",
        Units.LengthUnit.Decimeter => options.PreferUnicode ? "\u3377" : "dm",
        Units.LengthUnit.Foot => "ft",
        Units.LengthUnit.Yard => "yd",
        Units.LengthUnit.Meter => "m",
        Units.LengthUnit.Kilometer => options.PreferUnicode ? "\u339E" : "km",
        Units.LengthUnit.Mile => "mi",
        Units.LengthUnit.NauticalMile => "NM", // There is no single internationally agreed symbol. Others used are "N", "NM", "nmi" and "nm".
        Units.LengthUnit.AstronomicalUnit => options.PreferUnicode ? "\u3373" : "au",
        Units.LengthUnit.Parsec => options.PreferUnicode ? "\u3376" : "pc",
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
      : System.IComparable, System.IComparable<Length>, System.IFormattable, IUnitValueQuantifiable<double, LengthUnit>
    {
      public const double OneParsecInMeters = 30856775814913672;

      private readonly double m_value;

      public Length(double value, LengthUnit unit = LengthUnit.Meter)
        => m_value = unit switch
        {
          LengthUnit.Femtometer => value * 1E-15,
          LengthUnit.Nanometer => value * 1E-9,
          LengthUnit.Micrometer => value * 1E-6,
          LengthUnit.Millimeter => value * 1E-3,
          LengthUnit.Centimeter => value * 1E-2,
          LengthUnit.Inch => value * 0.0254,
          LengthUnit.Decimeter => value * 1E-1,
          LengthUnit.Foot => value * 0.3048,
          LengthUnit.Yard => value * 0.9144,
          LengthUnit.Meter => value,
          LengthUnit.Kilometer => value * 1E+3,
          LengthUnit.Mile => value * 1609.344,
          LengthUnit.NauticalMile => value * 1852,
          LengthUnit.AstronomicalUnit => value * 149597870700,
          LengthUnit.Parsec => value * OneParsecInMeters,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #region Static methods

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
      public string ToString(string? format, System.IFormatProvider? formatProvider) => ToValueString(QuantifiableValueStringOptions.Default with { Format = format, FormatProvider = formatProvider });

      // IQuantifiable<>
      public string ToValueString(QuantifiableValueStringOptions options) => ToUnitValueString(LengthUnit.Meter, options);

      /// <summary>
      /// <para>The unit of the <see cref="Length.Value"/> property is in <see cref="LengthUnit.Meter"/>.</para>
      /// </summary>
      public double Value => m_value;

      // IUnitQuantifiable<>
      public double GetUnitValue(LengthUnit unit)
        => unit switch
        {
          LengthUnit.Femtometer => m_value * 1E+15,
          LengthUnit.Nanometer => m_value * 1E+9,
          LengthUnit.Micrometer => m_value * 1E+6,
          LengthUnit.Millimeter => m_value * 1E+3,
          LengthUnit.Centimeter => m_value * 1E+2,
          LengthUnit.Inch => m_value / 0.0254,
          LengthUnit.Decimeter => m_value * 1E+1,
          LengthUnit.Foot => m_value / 0.3048,
          LengthUnit.Yard => m_value / 0.9144,
          LengthUnit.Meter => m_value,
          LengthUnit.Kilometer => m_value * 1E-3,
          LengthUnit.Mile => m_value / 1609.344,
          LengthUnit.NauticalMile => m_value / 1852,
          LengthUnit.AstronomicalUnit => m_value / 149597870700,
          LengthUnit.Parsec => m_value / OneParsecInMeters,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(LengthUnit unit, QuantifiableValueStringOptions options)
        => $"{string.Format(options.CultureInfo, $"{{0{(options.Format is null ? string.Empty : $":{options.Format}")}}}", GetUnitValue(unit))} {unit.GetUnitString(options)}";

      #endregion Implemented interfaces

      public override string ToString() => ToValueString(QuantifiableValueStringOptions.Default);
    }
  }
}
