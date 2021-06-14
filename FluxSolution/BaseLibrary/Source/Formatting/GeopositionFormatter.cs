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
  public class GeopositionFormatter
    : AFormatter
  {
    public bool InsertSpaces { get; set; }

    private static readonly System.Text.RegularExpressions.Regex m_regexFormat = new System.Text.RegularExpressions.Regex(@"^(?<Parts>DMS|DM|D)(?<DecimalPlaces>\d+)?$");

    /// <summary>Implementation of System.ICustomFormatter.Format()</summary>
    public override string Format(string? format, object? arg, System.IFormatProvider? formatProvider)
    {
      if (!string.IsNullOrEmpty(format) && arg is Media.Geoposition geo)
      {
        if (m_regexFormat.Match((format ?? throw new System.ArgumentNullException(nameof(format))).ToUpper(System.Globalization.CultureInfo.CurrentCulture)) is System.Text.RegularExpressions.Match m && m.Success)
        {
          if (m.Groups[@"Parts"] is System.Text.RegularExpressions.Group g0 && g0.Success && g0.Value is string parts)
          {
            if (!(m.Groups[@"DecimalPlaces"] is System.Text.RegularExpressions.Group g1 && g1.Success && g1.Value is string g1s && int.TryParse(g1s, out var decimalPlaces) && decimalPlaces >= 0 && decimalPlaces < 15))
            {
              decimalPlaces = -1;
            }

            var spacing = InsertSpaces ? @" " : string.Empty;

            var sb = new System.Text.StringBuilder();

            sb.Append($"{FormatParts(geo.Latitude, parts, decimalPlaces)}{spacing}");
            sb.Append($"{(geo.Latitude >= 0 ? 'N' : 'S')}");
            sb.Append($",{spacing}");
            sb.Append($"{FormatParts(geo.Longitude, parts, decimalPlaces)}{spacing}");
            sb.Append($"{(geo.Longitude >= 0 ? 'E' : 'W')}");
            sb.Append($",{spacing}");
            sb.Append($"{geo.Altitude}{spacing}m");

            return sb.ToString();

            /// <param name="format">[D|DM|DMS]{numberOfDecimaPlaces}{NS|EW}</param>
            /// <param name="arg">Either latitude or longitude.</param>
            /// <returns>{-}[nn.nnnn\u00B0|nn\u00B0nn.nn\u2032|nn\u00B0nn\u2032nn\u2033]</returns>
            string FormatParts(double value, string parts, int decimalPlaces)
            {
              var decimalDegrees = System.Math.Abs(value);
              var degrees = System.Math.Floor(decimalDegrees);
              var decimalMinutes = 60 * (decimalDegrees - degrees);
              var minutes = System.Math.Floor(decimalMinutes);
              var decimalSeconds = 60 * decimalMinutes - 60 * minutes;
              // var seconds = System.Math.Floor(decimalSeconds);

              switch (parts)
              {
                case @"D":
                  return string.Format(System.Globalization.CultureInfo.CurrentCulture, $"{{0:N{(decimalPlaces >= 0 ? decimalPlaces : 4)}}}\u00B0", decimalDegrees);
                case @"DM":
                  return string.Format(System.Globalization.CultureInfo.CurrentCulture, $"{degrees:N0}\u00B0{spacing}{{0:N{(decimalPlaces >= 0 ? decimalPlaces : 2)}}}\u2032", decimalMinutes);
                case @"DMS":
                  return string.Format(System.Globalization.CultureInfo.CurrentCulture, $"{degrees:N0}\u00B0{spacing}{minutes:N0}\u2032{spacing}{{0:N{(decimalPlaces >= 0 ? decimalPlaces : 0)}}}\u2033", decimalSeconds);
                default:
                  throw new System.ArgumentOutOfRangeException(nameof(parts));
              }
            }
          }
        }
      }

      return HandleOtherFormats(format, arg);
    }

    /// <summary>Try formatting either a latitude or a longitude from a decimal value according to the format parameter as shown.</summary>
    /// <returns></returns>
    //    public bool TryFormat(double value, string format, out string result)
    //    {
    //      try
    //      {
    //        var space = InsertSpaces ? @" " : string.Empty;

    //        if (m_regexFormat.Match((format ?? throw new System.ArgumentNullException(nameof(format))).ToUpper(System.Globalization.CultureInfo.CurrentCulture)) is System.Text.RegularExpressions.Match m && m.Success)
    //        {
    //          var decimalDegrees = System.Math.Abs(value);
    //          var degrees = System.Math.Floor(decimalDegrees);
    //          var decimalMinutes = 60 * (decimalDegrees - degrees);
    //          var minutes = System.Math.Floor(decimalMinutes);
    //          var decimalSeconds = 60 * decimalMinutes - 60 * minutes;
    //          var seconds = System.Math.Floor(decimalSeconds);

    //          var sb = new System.Text.StringBuilder();

    //          if (m.Groups[@"Parts"] is System.Text.RegularExpressions.Group gp && gp.Success && gp.Value is string sp)
    //          {
    //            if (!(m.Groups[@"DecimalPlaces"] is System.Text.RegularExpressions.Group gdp && gdp.Success && gdp.Value is string sdp && int.TryParse(sdp, out var dp) && dp >= 0 && dp < 15))
    //            {
    //              dp = -1;
    //            }

    //            switch (sp)
    //            {
    //              case @"D":
    //                sb.AppendFormat(System.Globalization.CultureInfo.CurrentCulture, $"{{0:N{(dp >= 0 ? dp : 4)}}}{SymbolDegrees}", decimalDegrees);
    //                break;
    //              case @"DM":
    //                sb.AppendFormat(System.Globalization.CultureInfo.CurrentCulture, $"{degrees:N0}{SymbolDegrees}{space}{{0:N{(dp >= 0 ? dp : 2)}}}{SymbolMinutes}", decimalMinutes);
    //                break;
    //              case @"DMS":
    //                sb.AppendFormat(System.Globalization.CultureInfo.CurrentCulture, $"{degrees:N0}{SymbolDegrees}{space}{minutes:N0}{SymbolMinutes}{space}{{0:N{(dp >= 0 ? dp : 0)}}}{SymbolSeconds}", decimalSeconds);
    //                break;
    //            }

    //            if (m.Groups["AxisCardinalDirections"] is System.Text.RegularExpressions.Group group && group.Success && group.Value is string axisCardinalDirections && axisCardinalDirections.Length == 2)
    //            {
    //              sb.Append(space);
    //              sb.Append(value >= 0 ? axisCardinalDirections[0] : axisCardinalDirections[1]);
    //            }
    //          }

    //          result = sb.ToString();
    //          return true;
    //        }
    //      }
    //#pragma warning disable CA1031 // Do not catch general exception types
    //      catch { }
    //#pragma warning restore CA1031 // Do not catch general exception types

    //      result = string.Empty;
    //      return false;
    //    }

    /// <summary>Try parsing a single DMS part (i.e. either latitude OR longitude).</summary>
    public static bool TryParse(string dms, out double result)
    {
      try
      {
        var regex = new System.Text.RegularExpressions.Regex(@"(?<Degrees>\d+(\.\d+)?)[^0-9\.]*(?<Minutes>\d+(\.\d+)?)[^0-9\.]*(?<Seconds>\d+(\.\d+)?)[^NSEW]*(?<CompassDirection>[NSEW])?");

        var value = 0.0;

        if (regex.Match(dms) is var m && m.Success)
        {
          if (m.Groups["Degrees"] is var g1 && g1.Success && double.TryParse(g1.Value, out var decimalDegrees))
          {
            value += decimalDegrees;
          }

          if (m.Groups["Minutes"] is var g2 && g2.Success && double.TryParse(g2.Value, out var decimalMinutes))
          {
            value += decimalMinutes / 60;
          }

          if (m.Groups["Seconds"] is var g3 && g3.Success && double.TryParse(g3.Value, out var decimalSeconds))
          {
            value += decimalSeconds / 3600;
          }

          if (m.Groups["CompassDirection"] is var g4 && g4.Success && (g4.Value[0] == 'S' || g4.Value[0] == 'W'))
          {
            value = -value;
          }

          result = value;
          return true;
        }
      }
      catch { }

      result = default;
      return false;
    }
  }
}
