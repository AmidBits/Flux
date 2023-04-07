namespace Flux
{
  public static partial class QuantitiesExtensionMethods
  {
    public static string GetUnitString(this Quantities.LengthUnit unit, bool preferUnicode, bool useFullName = false)
      => useFullName ? unit.ToString() : unit switch
      {
        Quantities.LengthUnit.Femtometer => preferUnicode ? "\u3399" : "fm",
        Quantities.LengthUnit.Nanometer => preferUnicode ? "\u339A" : "nm",
        Quantities.LengthUnit.Micrometer => preferUnicode ? "\u339B" : "µm",
        Quantities.LengthUnit.Millimeter => preferUnicode ? "\u339C" : "mm",
        Quantities.LengthUnit.Centimeter => preferUnicode ? "\u339D" : "cm",
        Quantities.LengthUnit.Inch => preferUnicode ? "\u33CC" : "in",
        Quantities.LengthUnit.Decimeter => preferUnicode ? "\u3377" : "dm",
        Quantities.LengthUnit.Foot => "ft",
        Quantities.LengthUnit.Yard => "yd",
        Quantities.LengthUnit.Meter => "m",
        Quantities.LengthUnit.Kilometer => preferUnicode ? "\u339E" : "km",
        Quantities.LengthUnit.Mile => "mi",
        Quantities.LengthUnit.NauticalMile => "NM", // There is no single internationally agreed symbol. Others used are "N", "NM", "nmi" and "nm".
        Quantities.LengthUnit.AstronomicalUnit => preferUnicode ? "\u3373" : "au",
        Quantities.LengthUnit.Parsec => preferUnicode ? "\u3376" : "pc",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Quantities
  {
    public enum LengthUnit
    {
      /// <summary>This is the default unit for length.</summary>
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
    /// <see cref="https://en.wikipedia.org/wiki/Length"/>
    public readonly record struct Length
      : System.IComparable, System.IComparable<Length>, System.IFormattable, IUnitQuantifiable<double, LengthUnit>
    {
      public const double PiParsecsInMeters = 96939420213600000;
      public const double OneParsecInMeters = PiParsecsInMeters / System.Math.PI;

      public const LengthUnit DefaultUnit = LengthUnit.Meter;

      private readonly double m_value;

      public Length(double value, LengthUnit unit = DefaultUnit)
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
      /// <summary>Computes the wavelength from the specified phase velocity and frequency. A wavelength is the spatial period of a periodic wave, i.e. the distance over which the wave's shape repeats. The default reference value for the speed of sound is 343.21 m/s. This determines the unit of measurement (i.e. meters per second) for the wavelength distance.</summary>
      /// <param name="phaseVelocity">The constant speed of the traveling wave. If these are sound waves then typically this is the speed of sound. If electromagnetic radiation (e.g. light) in free space then speed of light.</param>
      /// <param name="frequency"></param>
      /// <returns>The wavelength of the frequency cycle at the phase velocity.</returns>
      /// <see cref="https://en.wikipedia.org/wiki/Wavelength"/>

      public static Length ComputeWavelength(LinearVelocity phaseVelocity, Frequency frequency)
        => new(phaseVelocity.Value / frequency.Value);

      /// <summary>Creates a new <see cref="Length"/> instance from <see cref="LinearVelocity"/> and <see cref="AngularVelocity"/></summary>
      /// <param name="speed"></param>
      /// <param name="angularVelocity"></param>

      public static Length From(LinearVelocity speed, AngularVelocity angularVelocity)
        => new(speed.Value / angularVelocity.Value);
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
      public string ToString(string? format, IFormatProvider? formatProvider) => m_value.ToString(format, formatProvider);

      // IQuantifiable<>
      public string ToQuantityString(string? format = null, bool preferUnicode = false, bool useFullName = false) => ToUnitString(DefaultUnit, format, preferUnicode, useFullName);
      public double Value { get => m_value; init => m_value = value; }

      // IUnitQuantifiable<>
      public string ToUnitString(LengthUnit unit, string? format = null, bool preferUnicode = false, bool useFullName = false)
        => $"{string.Format($"{{0{(format is null ? string.Empty : $":{format}")}}}", ToUnitValue(unit))} {unit.GetUnitString(preferUnicode, useFullName)}";
      public double ToUnitValue(LengthUnit unit = DefaultUnit)
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

      #endregion Implemented interfaces

      public override string ToString() => ToQuantityString();
    }
  }
}