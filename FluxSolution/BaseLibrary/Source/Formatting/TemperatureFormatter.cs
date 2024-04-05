namespace Flux.Formatting
{
  /// <summary>Use of degrees-minutes-seconds is also called DMS notation.</summary>
  /// <see href="https://en.wikipedia.org/wiki/Degree_(angle)#Subdivisions"/>
  /// <example>
  /// var value = "40 11 15 ";
  /// Flux.IFormatProvider.DmsFormatter.TryParse(value, out var result);
  /// System.Console.WriteLine(result);
  /// System.Console.WriteLine(string.Format(new Flux.IFormatProvider.DmsFormatter(), "{0:DMSNS}", result)); // For a north-south suffix.
  /// System.Console.WriteLine(string.Format(new Flux.IFormatProvider.DmsFormatter(), "{0:DMSEW}", result)); // For a east-west suffix.
  /// </example>
  public sealed class TemperatureFormatter
    : AFormatter
  {
    private static readonly System.Text.RegularExpressions.Regex m_regexFormat = new(@"^(?<Unit>[A-Za-z]+)(?<DecimalPlaces>[0-9]+)?$", System.Text.RegularExpressions.RegexOptions.Compiled);

    /// <summary>Implementation of System.ICustomFormatter.Format()</summary>
    public override string Format(string? format, object? arg, System.IFormatProvider? formatProvider)
    {
      if (!string.IsNullOrWhiteSpace(format) && arg is Quantities.ThermaldynamicTemperature temperature)
      {
        if (m_regexFormat.Match(format) is System.Text.RegularExpressions.Match m && m.Success)
        {
          if (m.Groups[@"Unit"] is var g0 && g0.Success && g0.Value is var unitString)
          {
            if (!(m.Groups[@"DecimalPlaces"] is var g1 && g1.Success && g1.Value is var decimalPlacesString && int.TryParse(decimalPlacesString, out var decimalPlaces) && decimalPlaces >= 0 && decimalPlaces < 15))
              decimalPlaces = 2;

            foreach (var unit in (Quantities.ThermaldynamicTemperatureUnit[])System.Enum.GetValues(typeof(Quantities.ThermaldynamicTemperatureUnit)))
              if (unit.ToString().StartsWith(unitString, System.StringComparison.InvariantCultureIgnoreCase))
                return string.Format(null, $"{{0:N{decimalPlaces}}}", temperature.GetUnitValue(unit)) + unit.GetUnitString();
          }
        }
      }

      return HandleOtherFormats(format, arg);
    }
  }
}
