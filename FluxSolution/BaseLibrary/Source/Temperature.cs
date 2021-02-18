namespace Flux
{
  public struct Temperature
    : System.IEquatable<Temperature>
  {
    public const double CelsiusAbsoluteZero = -273.15;
    public const double CelsiusBoilingPoint = 99.9839;
    public const double CelsiusIcePoint = 0d;

    public const double FahrenheitAbsoluteZero = -459.67;
    public const double FahrenheitBoilingPoint = 211.97102;
    public const double FahrenheitIcePoint = 32d;

    public const double KelvinAbsoluteZero = 0d;
    public const double KelvinBoilingPoint = 373.1339;
    public const double KelvinIcePoint = 273.15;

    public const double RankineAbsoluteZero = 0d;
    public const double RankineBoilingPoint = 671.64102;
    public const double RankineIcePoint = 491.67;

    private readonly double m_kelvin;

    public double Celsius
      => ConvertKelvinToCelsius(m_kelvin);
    public double Fahrenheit
      => ConvertKelvinToFahrenheit(m_kelvin);
    public double Kelvin
      => m_kelvin;
    public double Rankine
      => ConvertKelvinToRankine(m_kelvin);

    public Temperature(double kelvin)
      => m_kelvin = kelvin;

    #region // Statics
    public static Temperature Add(Temperature left, Temperature right)
      => new Temperature(left.m_kelvin + right.m_kelvin);
    public static Temperature Divide(Temperature left, Temperature right)
      => new Temperature(left.m_kelvin / right.m_kelvin);
    public static Temperature FromCelsius(double celsius)
      => new Temperature(ConvertCelsiusToKelvin(celsius));
    public static Temperature FromFahrenheit(double fahrenheit)
      => new Temperature(ConvertFahrenheitToKelvin(fahrenheit));
    public static Temperature FromKelvin(double kelvin)
      => new Temperature(kelvin);
    public static Temperature FromRankine(double rankine)
      => new Temperature(ConvertRankineToKelvin(rankine));
    public static Temperature Multiply(Temperature left, Temperature right)
      => new Temperature(left.m_kelvin * right.m_kelvin);
    public static Temperature Negate(Temperature value)
      => new Temperature(-value.m_kelvin);
    public static Temperature Remainder(Temperature dividend, Temperature divisor)
      => new Temperature(dividend.m_kelvin % divisor.m_kelvin);
    public static Temperature Subtract(Temperature left, Temperature right)
      => new Temperature(left.m_kelvin - right.m_kelvin);

    /// <summary>Convert the temperature specified in Celsius to Fahrenheit.</summary>
    public static double ConvertCelsiusToFahrenheit(double celsius)
      => celsius * (9d / 5d) + FahrenheitIcePoint;
    /// <summary>Convert the temperature specified in Celsius to Kelvin.</summary>
    public static double ConvertCelsiusToKelvin(double celsius)
      => celsius - CelsiusAbsoluteZero;
    /// <summary>Convert the temperature specified in Celsius to Rankine.</summary>
    public static double ConvertCelsiusToRankine(double celsius)
      => (celsius - CelsiusAbsoluteZero) * (9d / 5d);
    /// <summary>Convert the temperature specified in Fahrenheit to Celsius.</summary>
    public static double ConvertFahrenheitToCelsius(double fahrenheit)
      => (fahrenheit - FahrenheitIcePoint) * (5d / 9d);
    /// <summary>Convert the temperature specified in Fahrenheit to Kelvin.</summary>
    public static double ConvertFahrenheitToKelvin(double fahrenheit)
      => (fahrenheit - FahrenheitAbsoluteZero) * (5d / 9d);
    /// <summary>Convert the temperature specified in Fahrenheit to Rankine.</summary>
    public static double ConvertFahrenheitToRankine(double fahrenheit)
      => fahrenheit - FahrenheitAbsoluteZero;
    /// <summary>Convert the temperature specified in Kelvin to Celsius.</summary>
    public static double ConvertKelvinToCelsius(double kelvin)
      => kelvin - KelvinIcePoint;
    /// <summary>Convert the temperature specified in Kelvin to Fahrenheit.</summary>
    public static double ConvertKelvinToFahrenheit(double kelvin)
      => kelvin * (9d / 5d) + FahrenheitAbsoluteZero;
    /// <summary>Convert the temperature specified in Kelvin to Rankine.</summary>
    public static double ConvertKelvinToRankine(double kelvin)
      => kelvin * (9d / 5d);
    /// <summary>Convert the temperature specified in Rankine to Celsius.</summary>
    public static double ConvertRankineToCelsius(double rankine)
      => (rankine - RankineIcePoint) * (5d / 9d);
    /// <summary>Convert the temperature specified in Rankine to Fahrenheit.</summary>
    public static double ConvertRankineToFahrenheit(double rankine)
      => rankine - RankineIcePoint;
    /// <summary>Convert the temperature specified in Rankine to Kelvin.</summary>
    public static double ConvertRankineToKelvin(double rankine)
      => rankine * (5d / 9d);
    #endregion // Statics

    // Operators
    public static bool operator ==(Temperature a, Temperature b)
      => a.Equals(b);
    public static bool operator !=(Temperature a, Temperature b)
      => !a.Equals(b);
    public static Temperature operator +(Temperature a, Temperature b)
      => Add(a, b);
    public static Temperature operator /(Temperature a, Temperature b)
      => Divide(a, b);
    public static Temperature operator *(Temperature a, Temperature b)
      => Multiply(a, b);
    public static Temperature operator -(Temperature v)
      => Negate(v);
    public static Temperature operator %(Temperature a, Temperature b)
      => Remainder(a, b);
    public static Temperature operator -(Temperature a, Temperature b)
      => Subtract(a, b);

    // IEquatable<Angle>
    public bool Equals(Temperature other)
      => m_kelvin == other.m_kelvin;
    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider)
      => string.Format(formatProvider ?? new FormatProviders.TemperatureFormatProvider(), format ?? $"<{nameof(Temperature)}: {{0:F3}}>", this);
    // Overrides
    public override bool Equals(object? obj)
      => obj is Temperature o && Equals(o);
    public override int GetHashCode()
      => m_kelvin.GetHashCode();
    public override string ToString()
      => ToString(null, null);
  }
}
