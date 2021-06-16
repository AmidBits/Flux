namespace Flux.Formatting
{
  /// <summary>Use of degrees-minutes-seconds is also called DMS notation.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Degree_(angle)#Subdivisions"/>
  /// <example>
  /// var value = "40 11 15 ";
  /// Flux.IFormatProvider.DmsFormatter.TryParse(value, out var result);
  /// System.Console.WriteLine(result);
  /// System.Console.WriteLine(string.Format(new Flux.IFormatProvider.DmsFormatter(), "{0:DMSNS}", result)); // For a north-south suffix.
  /// System.Console.WriteLine(string.Format(new Flux.IFormatProvider.DmsFormatter(), "{0:DMSEW}", result)); // For a east-west suffix.
  /// </example>
  public class TemperatureFormatter
    : AFormatter
  {
    public bool UseUnicodeSymbolWhenAvailable { get; set; }

    private static readonly System.Text.RegularExpressions.Regex m_regexFormat = new System.Text.RegularExpressions.Regex(@"^(?<Unit>[A-Za-z]+)(?<DecimalPlaces>[0-9]+)?$");

    /// <summary>Implementation of System.ICustomFormatter.Format()</summary>
    public override string Format(string? format, object? arg, System.IFormatProvider? formatProvider)
    {
      if (!string.IsNullOrEmpty(format) && arg is Units.Temperature temperature)
      {
        if (m_regexFormat.Match(format.ToUpper(System.Globalization.CultureInfo.CurrentCulture)) is System.Text.RegularExpressions.Match m && m.Success)
        {
          if (m.Groups[@"Unit"] is var g0 && g0.Success && g0.Value is var unitString)
          {
            if (!(m.Groups[@"DecimalPlaces"] is var g1 && g1.Success && g1.Value is var decimalPlacesString && int.TryParse(decimalPlacesString, out var decimalPlaces) && decimalPlaces >= 0 && decimalPlaces < 15))
              decimalPlaces = -1;

            var formatString = $"{{0:N{(decimalPlaces >= 0 ? decimalPlaces : 4)}}}";

            switch (unitString)
            {
              case var celsius when @"Celsius".StartsWith(celsius, System.StringComparison.InvariantCultureIgnoreCase):
                return string.Format(null, formatString, temperature.Celsius) + (UseUnicodeSymbolWhenAvailable ? " \u2103" : " \u00B0C");
              case var fahrenheit when @"Fahrenheit".StartsWith(fahrenheit, System.StringComparison.InvariantCultureIgnoreCase):
                return string.Format(null, formatString, temperature.Fahrenheit) + (UseUnicodeSymbolWhenAvailable ? " \u2109" : " \u00B0F");
              case var kelvin when @"Kelvin".StartsWith(kelvin, System.StringComparison.InvariantCultureIgnoreCase):
                return string.Format(null, formatString, temperature.Kelvin) + (UseUnicodeSymbolWhenAvailable ? " \u212A" : " \u00B0K");
              case var rankine when @"Rankine".StartsWith(rankine, System.StringComparison.InvariantCultureIgnoreCase):
                return string.Format(null, formatString, temperature.Rankine) + " \u00B0R";
              default:
                break;
            }
          }
        }
      }

      return HandleOtherFormats(format, arg);
    }
  }
}
