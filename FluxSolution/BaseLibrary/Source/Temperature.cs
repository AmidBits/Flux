namespace Flux
{
  public static partial class Temperature
  {
    public const double CelsiusAbsoluteZero = -273.15;
    public const double CelsiusBoilingPoint = 99.9839;
    public const double CelsiusIcePoint = 0d;

    /// <summary>Convert the temperature specified in Celsius to Fahrenheit.</summary>
    public static double CelsiusToFahrenheit(double celsius)
      => celsius * (9d / 5d) + FahrenheitIcePoint;
    /// <summary>Convert the temperature specified in Celsius to Kelvin.</summary>
    public static double CelsiusToKelvin(double celsius)
      => celsius - CelsiusAbsoluteZero;
    /// <summary>Convert the temperature specified in Celsius to Rankine.</summary>
    public static double CelsiusToRankine(double celsius)
      => (celsius - CelsiusAbsoluteZero) * (9d / 5d);

    public const double FahrenheitAbsoluteZero = -459.67;
    public const double FahrenheitBoilingPoint = 211.97102;
    public const double FahrenheitIcePoint = 32d;

    /// <summary>Convert the temperature specified in Fahrenheit to Celsius.</summary>
    public static double FahrenheitToCelsius(double fahrenheit)
      => (fahrenheit - FahrenheitIcePoint) * (5d / 9d);
    /// <summary>Convert the temperature specified in Fahrenheit to Kelvin.</summary>
    public static double FahrenheitToKelvin(double fahrenheit)
      => (fahrenheit - FahrenheitAbsoluteZero) * (5d / 9d);
    /// <summary>Convert the temperature specified in Fahrenheit to Rankine.</summary>
    public static double FahrenheitToRankine(double fahrenheit)
      => fahrenheit - FahrenheitAbsoluteZero;

    public const double KelvinAbsoluteZero = 0d;
    public const double KelvinBoilingPoint = 373.1339;
    public const double KelvinIcePoint = 273.15;

    /// <summary>Convert the temperature specified in Kelvin to Celsius.</summary>
    public static double KelvinToCelsius(double kelvin)
      => kelvin - KelvinIcePoint;
    /// <summary>Convert the temperature specified in Kelvin to Fahrenheit.</summary>
    public static double KelvinToFahrenheit(double kelvin)
      => kelvin * (9d / 5d) + FahrenheitAbsoluteZero;
    /// <summary>Convert the temperature specified in Kelvin to Rankine.</summary>
    public static double KelvinToRankine(double kelvin)
      => kelvin * (9d / 5d);

    public const double RankineAbsoluteZero = 0d;
    public const double RankineBoilingPoint = 671.64102;
    public const double RankineIcePoint = 491.67;

    /// <summary>Convert the temperature specified in Rankine to Celsius.</summary>
    public static double RankineToCelsius(double rankine)
      => (rankine - RankineIcePoint) * (5d / 9d);
    /// <summary>Convert the temperature specified in Rankine to Fahrenheit.</summary>
    public static double RankineToFahrenheit(double rankine)
      => rankine - RankineIcePoint;
    /// <summary>Convert the temperature specified in Rankine to Kelvin.</summary>
    public static double RankineToKelvin(double rankine)
      => rankine * (5d / 9d);
  }
}