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
  public sealed class LongitudeFormatter
    : AFormatter
  {
    public bool InsertSpaces { get; set; }
    public System.Text.Rune SymbolDegrees { get; set; } = new System.Text.Rune('\u00B0');
    public System.Text.Rune SymbolMinutes { get; set; } = new System.Text.Rune('\u2032');
    public System.Text.Rune SymbolSeconds { get; set; } = new System.Text.Rune('\u2033');

    private static readonly System.Text.RegularExpressions.Regex m_regexFormat = new(@"(?<Parts>DMS|DM|D)(?<DecimalPlaces>\d+)?");

    /// <summary>Implementation of System.ICustomFormatter.Format()</summary>
    public override string Format(string? format, object? arg, System.IFormatProvider? formatProvider)
    {
      if (!string.IsNullOrEmpty(format) && arg is double longitude)
        if (TryFormat(longitude, format, out var dms))
          return dms;

      return HandleOtherFormats(format, arg);
    }

    /// <summary>Try formatting either a latitude or a longitude from a decimal value according to the format parameter as shown.</summary>
    /// <param name="value">Either latitude or longitude.</param>
    /// <param name="format">[D|DM|DMS]{numberOfDecimaPlaces}{NS|EW}</param>
    /// <param name="result">{-}[nn.nnnn\u00B0|nn\u00B0nn.nn\u2032|nn\u00B0nn\u2032nn\u2033]</param>
    /// <returns></returns>
    public bool TryFormat(double value, string format, out string result)
    {
      try
      {
        var space = InsertSpaces ? @" " : string.Empty;

        if (m_regexFormat.Match((format ?? throw new System.ArgumentNullException(nameof(format))).ToUpper(System.Globalization.CultureInfo.CurrentCulture)) is System.Text.RegularExpressions.Match m && m.Success)
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
                sb.AppendFormat(System.Globalization.CultureInfo.CurrentCulture, $"{{0:N{(dp >= 0 ? dp : 4)}}}{SymbolDegrees}", decimalDegrees);
                break;
              case @"DM":
                sb.AppendFormat(System.Globalization.CultureInfo.CurrentCulture, $"{degrees:N0}{SymbolDegrees}{space}{{0:N{(dp >= 0 ? dp : 2)}}}{SymbolMinutes}", decimalMinutes);
                break;
              case @"DMS":
                sb.AppendFormat(System.Globalization.CultureInfo.CurrentCulture, $"{degrees:N0}{SymbolDegrees}{space}{minutes:N0}{SymbolMinutes}{space}{{0:N{(dp >= 0 ? dp : 0)}}}{SymbolSeconds}", decimalSeconds);
                break;
            }

            sb.Append(space);
            sb.Append(value >= 0 ? 'E' : 'W');
          }

          result = sb.ToString();
          return true;
        }
      }
      catch { }

      result = string.Empty;
      return false;
    }

    /// <summary>Try parsing a single DMS part (i.e. either latitude OR longitude).</summary>
    public static bool TryParse(string dms, out double result)
    {
      try
      {
        var regex = new System.Text.RegularExpressions.Regex(@"(?<D>\d+(\.\d+)?)[^0-9\.]*(?<M>\d+(\.\d+)?)[^0-9\.]*(?<S>\d+(\.\d+)?)[^EW]*(?<C>[EW])?");

        var value = 0.0;

        if (regex.Match(dms) is var m && m.Success)
        {
          if (m.Groups["D"] is var g1 && g1.Success && double.TryParse(g1.Value, out var decimalDegrees))
            value += decimalDegrees;

          if (m.Groups["M"] is var g2 && g2.Success && double.TryParse(g2.Value, out var decimalMinutes))
            value += decimalMinutes / 60;

          if (m.Groups["S"] is var g3 && g3.Success && double.TryParse(g3.Value, out var decimalSeconds))
            value += decimalSeconds / 3600;

          if (m.Groups["C"] is var g4 && g4.Success && g4.Value[0] == 'E')
            value = -value;

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
