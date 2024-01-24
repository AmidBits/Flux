namespace Flux
{
  public static partial class Em
  {
    public static string GetUnitString(this Units.LengthUnit unit, bool preferUnicode, bool useFullName = false)
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
    /// <see href="https://en.wikipedia.org/wiki/Length"/>
    public readonly record struct Length
      : System.IComparable, System.IComparable<Length>, System.IFormattable, IUnitValueQuantifiable<double, LengthUnit>
    {
      public const double OneParsecInMeters = 30856775814913672;

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

      /// <summary>Creates a new <see cref="Length"/> instance from <see cref="LinearVelocity"/> and <see cref="AngularVelocity"/></summary>
      /// <param name="speed"></param>
      /// <param name="angularVelocity"></param>
      public static Length From(LinearVelocity speed, AngularVelocity angularVelocity)
        => new(speed.Value / angularVelocity.Value);

      public static Length OfCirclePerimeter(Length radius)
        => new(2 * System.Math.PI * radius.Value);

      /// <summary>Creates a new <see cref="Length"/> instance representing the perimeter of the specified ellipse.</summary>
      /// <param name="semiMajorAxis">The longer radius.</param>
      /// <param name="semiMinorAxis">The shorter radius.</param>
      public static Length OfEllipsePerimeter(Length semiMajorAxis, Length semiMinorAxis)
      {
        var circle = System.Math.PI * (semiMajorAxis.Value + semiMinorAxis.Value);

        if (semiMajorAxis.Value == semiMinorAxis.Value) // For a circle, use (PI * diameter);
          return new(circle);

        var h3 = 3 * System.Math.Pow(semiMajorAxis.Value - semiMinorAxis.Value, 2) / System.Math.Pow(semiMajorAxis.Value + semiMinorAxis.Value, 2);

        var ellipse = circle * (1 + h3 / (10 + System.Math.Sqrt(4 - h3)));

        return new(ellipse);
      }

      /// <summary>Creates a new <see cref="Length"/> instance from the specified rectangle.</summary>
      /// <param name="radius">The radius or length of one side of a hexagon are both the same.</param>
      public static Length OfHexagonPerimeter(Length radius)
        => new(6 * radius.Value);

      /// <summary>Creates a new <see cref="Length"/> instance from the specified rectangle.</summary>
      /// <param name="length">The length of a rectangle.</param>
      /// <param name="width">The width of a rectangle.</param>
      public static Area OfRectanglePerimiter(Length length, Length width)
        => new(2 * length.Value + 2 * width.Value);

      /// <summary>Computes the wavelength from the specified phase velocity and frequency. A wavelength is the spatial period of a periodic wave, i.e. the distance over which the wave's shape repeats. The default reference value for the speed of sound is 343.21 m/s. This determines the unit of measurement (i.e. meters per second) for the wavelength distance.</summary>
      /// <param name="phaseVelocity">The constant speed of the traveling wave. If these are sound waves then typically this is the speed of sound. If electromagnetic radiation (e.g. light) in free space then speed of light.</param>
      /// <param name="frequency"></param>
      /// <returns>The wavelength of the frequency cycle at the phase velocity.</returns>
      /// <see href="https://en.wikipedia.org/wiki/Wavelength"/>
      public static Length Wavelength(LinearVelocity phaseVelocity, Frequency frequency)
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
      public string ToString(string? format, IFormatProvider? formatProvider) => m_value.ToString(format, formatProvider);

      // IQuantifiable<>
      public string ToValueString(string? format = null, bool preferUnicode = false, bool useFullName = false, System.Globalization.CultureInfo? culture = null)
        => ToUnitValueString(DefaultUnit, format, preferUnicode, useFullName, culture);

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

      public string ToUnitValueString(LengthUnit unit, string? format = null, bool preferUnicode = false, bool useFullName = false, System.Globalization.CultureInfo? culture = null)
        => $"{string.Format(culture, $"{{0{(format is null ? string.Empty : $":{format}")}}}", GetUnitValue(unit))} {unit.GetUnitString(preferUnicode, useFullName)}";

      #endregion Implemented interfaces

      public override string ToString() => ToValueString();
    }
  }
}
