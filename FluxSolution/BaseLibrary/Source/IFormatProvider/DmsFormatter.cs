namespace Flux.IFormatProvider
{
  /// <summary>Use of degrees-minutes-seconds is also called DMS notation.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Degree_(angle)#Subdivisions"/>
  /// <example>
  /// var value = "40 11 15 ";
  /// Flux.IFormatProvider.DmsFormatter.TryParse(value, out var result);
  /// System.Console.WriteLine(result);
  /// System.Console.WriteLine(string.Format(new Flux.IFormatProvider.DmsFormatter(), "{0:DMS}", result));
  /// </example>
  public class DmsFormatter : FormatProvider
  {
    public const string FormatIdentifier = @"DMS";

    public static string SymbolDegrees = "\u00B0";
    public static string SymbolMinutes = "\u2032";
    public static string SymbolSeconds = "\u2033";

    /// <summary>Implementation of System.ICustomFormatter.Format()</summary>
    public override string Format(string? format, object? arg, System.IFormatProvider? formatProvider)
    {
      if (!string.IsNullOrEmpty(format))
      {
        if (Flux.Convert.TryTypeConverter<double>(arg ?? string.Empty, out var signedDecimalDegrees, null))
        {
          if (TryFormat(signedDecimalDegrees, format, out var dms))
          {
            return dms;
          }
        }
      }

      return HandleOtherFormats(format, arg);
    }

    private static readonly System.Text.RegularExpressions.Regex _regexFormat = new System.Text.RegularExpressions.Regex(@"(?<Parts>DMS|DM|D)(?<DecimalPlaces>\d+)?(?<AxisCardinalDirections>(NS|EW))?");
    /// <summary>Try formatting either a latitude or a longitude from a decimal value according to the format parameter as shown.</summary>
    /// <param name="value">Either latitude or longitude.</param>
    /// <param name="format">[D|DM|DMS]{numberOfDecimaPlaces}{NS|EW}</param>
    /// <param name="result">{-}[nn.nnnn\u00B0|nn\u00B0nn.nn\u2032|nn\u00B0nn\u2032nn\u2033]</param>
    /// <returns></returns>
    public static bool TryFormat(double value, string format, out string result)
    {
      try
      {
        if (_regexFormat.Match(format.ToUpper()) is System.Text.RegularExpressions.Match m && m.Success)
        {
          var decimalDegrees = System.Math.Abs(value);
          var degrees = System.Math.Floor(decimalDegrees);
          var decimalMinutes = 60 * (decimalDegrees - degrees);
          var minutes = System.Math.Floor(decimalMinutes);
          var decimalSeconds = 60 * decimalMinutes - 60 * minutes;

          var sb = new System.Text.StringBuilder();

          if (m.Groups[@"Parts"] is System.Text.RegularExpressions.Group gp && gp.Success && gp.Value is string sp)
          {
            if (!(m.Groups[@"DecimalPlaces"] is System.Text.RegularExpressions.Group gdp && gdp.Success && gdp.Value is string sdp && int.TryParse(sdp, out var dp) && dp >= 0 && dp < 15))
            {
              dp = -1;
            }

            switch (sp)
            {
              case @"D":
                sb.AppendFormat(@"{0:N" + (dp >= 0 ? dp : 4) + @"}{1}", decimalDegrees, SymbolDegrees);
                break;
              case @"DM":
                sb.AppendFormat(@"{0:N0}{1}{2:N" + (dp >= 0 ? dp : 2) + @"}{3}", degrees, SymbolDegrees, decimalMinutes, SymbolMinutes);
                break;
              case @"DMS":
                sb.AppendFormat(@"{0:N0}{1}{2:N0}{3}{4:N" + (dp >= 0 ? dp : 0) + @"}{5}", degrees, SymbolDegrees, minutes, SymbolMinutes, decimalSeconds, SymbolSeconds);
                break;
            }

            if (m.Groups["AxisCardinalDirections"] is System.Text.RegularExpressions.Group group && group.Success && group.Value is string axisCardinalDirections && axisCardinalDirections.Length == 2)
            {
              sb.Append(value >= 0 ? axisCardinalDirections[0] : axisCardinalDirections[1]);
            }
          }

          result = sb.ToString();
          return true;
        }
      }
      catch { }

      result = string.Empty;
      return false;
    }

    private static readonly System.Text.RegularExpressions.Regex _regexParse = new System.Text.RegularExpressions.Regex(string.Format(@"(?<Degrees>\d+(\.\d+)?)[^0-9\.]*(?<Minutes>\d+(\.\d+)?)[^0-9\.]*(?<Seconds>\d+(\.\d+)?)[^NSEW]*(?<CompassDirection>[NSEW])?", SymbolDegrees, SymbolMinutes, SymbolSeconds));
    /// <summary>Try parsing dms format.</summary>
    public static bool TryParse(string text, out double result)
    {
      try
      {
        result = 0;

        if (_regexParse.Match(text) is var m && m.Success)
        {
          if (m.Groups["Degrees"] is var g1 && g1.Success && double.TryParse(g1.Value, out var decimalDegrees))
          {
            result += decimalDegrees;
          }

          if (m.Groups["Minutes"] is var g2 && g2.Success && double.TryParse(g2.Value, out var decimalMinutes))
          {
            result += decimalMinutes / 60;
          }

          if (m.Groups["Seconds"] is var g3 && g3.Success && double.TryParse(g3.Value, out var decimalSeconds))
          {
            result += decimalSeconds / 3600;
          }

          if (m.Groups["CompassDirection"] is var g4 && g4.Success && (g4.Value[0] == 'S' || g4.Value[0] == 'W'))
          {
            result = -result;
          }

          return true;
        }
      }
      catch { }

      result = default;
      return false;
    }
  }
}
