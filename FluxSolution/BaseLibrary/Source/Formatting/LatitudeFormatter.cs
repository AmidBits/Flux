namespace Flux.Formatting
{
  /// <summary>Use of degrees-minutes-seconds is also called DMS notation and a N/S directional suffix.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Degree_(angle)#Subdivisions"/>
  /// <example>
  /// var value = "40 11 15 N";
  /// Flux.IFormatProvider.LatitudeFormatter.TryParse(value, out var result);
  /// System.Console.WriteLine(result);
  /// System.Console.WriteLine(string.Format(new Flux.IFormatProvider.LatitudeFormatter(), "{0:DMS}", result));
  /// </example>
  public sealed class LatitudeFormatter
    : AFormatter
  {
    public bool InsertSpaces { get; set; }
    public bool PreferUnicode { get; set; }

    public static System.Text.Rune SymbolDegrees => new('\u00B0');
    public System.Text.Rune SymbolMinutes => new(PreferUnicode ? '\u2032' : '\'');
    public System.Text.Rune SymbolSeconds => new(PreferUnicode ? '\u2033' : '"');

    private static readonly System.Text.RegularExpressions.Regex m_regexFormat = new(@"(?<Parts>DMS|DM|D)(?<DecimalPlaces>\d+)?");

    /// <summary>Implementation of System.ICustomFormatter.Format()</summary>
    public override string Format(string? format, object? arg, System.IFormatProvider? formatProvider)
    {
      if (!string.IsNullOrEmpty(format) && arg is double latitude)
        if (TryFormat(latitude, format, out var dms))
          return dms;

      return HandleOtherFormats(format, arg);
    }

    /// <summary>Try formatting a latitude from a decimal value according to the format parameter as shown.</summary>
    /// <param name="value">A latitude value.</param>
    /// <param name="format">[D|DM|DMS]{numberOfDecimaPlaces}</param>
    /// <param name="result">{-}[nn.nnnn\u00B0|nn\u00B0nn.nn\u2032|nn\u00B0nn\u2032nn\u2033]</param>
    /// <returns></returns>
    public bool TryFormat(double value, string format, out string result)
    {
      try
      {
        var space = InsertSpaces ? @" " : string.Empty;

        if (m_regexFormat.Match((format ?? throw new System.ArgumentNullException(nameof(format))).ToUpper(System.Globalization.CultureInfo.CurrentCulture)) is System.Text.RegularExpressions.Match m && m.Success)
        {
          var (degrees, decimalMinutes, minutes, decimalSeconds) = Angle.ConvertDecimalDegreeToSexagesimalDegree(value);

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
                sb.AppendFormat(System.Globalization.CultureInfo.CurrentCulture, $"{{0:N{(dp >= 0 ? dp : 4)}}}{SymbolDegrees}", value); // Simply show as decimal degrees.
                break;
              case @"DM":
                sb.AppendFormat(System.Globalization.CultureInfo.CurrentCulture, $"{degrees:N0}{SymbolDegrees}{space}{{0:N{(dp >= 0 ? dp : 2)}}}{SymbolMinutes}", decimalMinutes);
                break;
              case @"DMS":
                sb.AppendFormat(System.Globalization.CultureInfo.CurrentCulture, $"{degrees:N0}{SymbolDegrees}{space}{minutes:N0}{SymbolMinutes}{space}{{0:N{(dp >= 0 ? dp : 0)}}}{SymbolSeconds}", decimalSeconds);
                break;
            }

            sb.Append(space);
            sb.Append(value >= 0 ? 'N' : 'S');
          }

          result = sb.ToString();
          return true;
        }
      }
      catch { }

      result = string.Empty;
      return false;
    }

    private static System.Text.RegularExpressions.Regex m_reParse
      => new System.Text.RegularExpressions.Regex(@"(?<Degrees>\d+(\.\d+)?)[^0-9\.]*(?<Minutes>\d+(\.\d+)?)?[^0-9\.]*(?<Seconds>\d+(\.\d+)?)?[^ENWS]*(?<Direction>[ENWS])?");

    private static System.Text.RegularExpressions.Regex[] m_reParses = new System.Text.RegularExpressions.Regex[]
    {
      new System.Text.RegularExpressions.Regex(@"(?<Degrees>\d+)[^0-9]*(?<Minutes>\d+)[^0-9\.]*(?<Seconds>\d+(\.\d+)?)[^ENWS]*(?<Direction>[ENWS])"),
      new System.Text.RegularExpressions.Regex(@"(?<Degrees>\d+)[^0-9\.]*(?<Minutes>\d+(\.\d+)?)[^ENWS]*(?<Direction>[ENWS])"),
      new System.Text.RegularExpressions.Regex(@"(?<Degrees>\d+(\.\d+)?)[^ENWS]*(?<Direction>[ENWS])?")
    };

    public static bool TryParseDmsToDecimalDegrees(string dms, out double result)
    {
      result = 0.0;

      foreach (var re in m_reParses)
      {
        if (re.Match(dms) is var m && m.Success)
        {
          if (m.Groups["Degrees"] is var g1 && g1.Success && double.TryParse(g1.Value, out var degrees))
            result += degrees;

          if (m.Groups["Minutes"] is var g2 && g2.Success && double.TryParse(g2.Value, out var minutes))
            result += minutes / 60;

          if (m.Groups["Seconds"] is var g3 && g3.Success && double.TryParse(g3.Value, out var seconds))
            result += seconds / 3600;

          if (m.Groups["Direction"] is var g4 && g4.Success && (g4.Value[0] == 'S' || g4.Value[0] == 'W'))
            result = -result;

          return true;
        }
      }

      return false;
    }


    /// <summary>Try parsing a latitude DMS.</summary>
    public static bool TryParse(string dms, out double result)
    {
      try
      {
        var value = 0.0;

        if (m_reParse.Match(dms) is var m && m.Success)
        {
          if (m.Groups["Degrees"] is var g1 && g1.Success && double.TryParse(g1.Value, out var decimalDegrees))
            value += decimalDegrees;

          if (m.Groups["Minutes"] is var g2 && g2.Success && double.TryParse(g2.Value, out var decimalMinutes))
            value += decimalMinutes / 60;

          if (m.Groups["Seconds"] is var g3 && g3.Success && double.TryParse(g3.Value, out var decimalSeconds))
            value += decimalSeconds / 3600;

          if (m.Groups["Direction"] is var g4 && g4.Success && g4.Value[0] == 'S')
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
