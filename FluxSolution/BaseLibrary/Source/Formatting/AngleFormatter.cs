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
  public class AngleFormatter
    : AFormatter
  {
    public bool UseUnicodeSymbolWhenAvailable { get; set; }

    private static readonly System.Text.RegularExpressions.Regex m_regexFormat = new System.Text.RegularExpressions.Regex(@"^(?<Unit>[A-Za-z]+)(?<DecimalPlaces>[0-9]+)?$");

    /// <summary>Implementation of System.ICustomFormatter.Format()</summary>
    public override string Format(string? format, object? arg, System.IFormatProvider? formatProvider)
    {
      if (!string.IsNullOrEmpty(format))
      {
        if (arg is Media.Angle angle)
        {
          if (m_regexFormat.Match((format ?? throw new System.ArgumentNullException(nameof(format))).ToUpper(System.Globalization.CultureInfo.CurrentCulture)) is System.Text.RegularExpressions.Match m && m.Success)
          {
            var sb = new System.Text.StringBuilder();

            if (m.Groups[@"Unit"] is var g0 && g0.Success && g0.Value is var unitString)
            {
              if (!(m.Groups[@"DecimalPlaces"] is var g1 && g1.Success && g1.Value is var decimalPlacesString && int.TryParse(decimalPlacesString, out var decimalPlaces) && decimalPlaces >= 0 && decimalPlaces < 15))
              {
                decimalPlaces = -1;
              }

              var formatString = $"{{0:N{(decimalPlaces >= 0 ? decimalPlaces : 4)}}}";

              switch (unitString)
              {
                case var deg when @"Degrees".StartsWith(deg, System.StringComparison.InvariantCultureIgnoreCase):
                  sb.AppendFormat(null, formatString, angle.Degrees);
                  sb.Append(" deg");
                  break;
                case var grad when @"Gradians".StartsWith(grad, System.StringComparison.InvariantCultureIgnoreCase):
                  sb.AppendFormat(null, formatString, angle.Gradians);
                  sb.Append($" gon");
                  break;
                case var rad when @"Radians".StartsWith(rad, System.StringComparison.InvariantCultureIgnoreCase):
                  sb.AppendFormat(null, formatString, angle.Radians);
                  sb.Append($" rad");
                  break;
                default:
                  throw new System.ArgumentOutOfRangeException(nameof(format));
              }
            }

            return sb.ToString();
          }
        }
      }

      return HandleOtherFormats(format, arg);
    }
  }
}
