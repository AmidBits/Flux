using System;
using System.Linq;

namespace Flux.IFormatProvider
{
  // Additional input wanted for completeness.

  /// <summary>Pima County Street Address formatter</summary>
  /// <see cref="http://webcms.pima.gov/cms/One.aspx?pageId=61696"/>
  /// <example>
  /// if (Flux.IFormatProvider.PcsaFormatter.TryParse(@"33 South CL Stone Avenue Floor 14", out var results))
  ///   for (var index = 0; index<results.Length; index++)
  ///     System.Console.WriteLine($"{results[index]}");
  /// if (Flux.IFormatProvider.PcsaFormatter.TryFormat(@"33 South CL Stone Avenue Floor 14", @"Number Direction Name Type", out var result))
  ///   System.Console.WriteLine(result);
  /// if (Flux.IFormatProvider.PcsaFormatter.TryFormat(@"33 South CL Stone Avenue Floor 14", @"Number Direction$ Name Type$", out var result))
  ///   System.Console.WriteLine(result);
  /// </example>
  /// <remarks>The formatting string can contain any of the (enum) Parts and in addition Direction and Type can each have a trailing '$' to indicate leaving the value as found, otherwise the direction will be N, E, S or W and type will be the two character uppercase abbreviation for the entered type.</remarks>
  public class PcsaFormatter : FormatProvider
  {
    public const string FormatIdentifier = @"PCSA";

    public enum Parts
    {
      Number,
      Direction,
      Intersection,
      Name,
      Type,
      Unit
    }

    public static System.Collections.Generic.Dictionary<string, string> StreetDirections = new System.Collections.Generic.Dictionary<string, string>()
    {
      { @"E", @"East" },
      { @"N", @"North" },
      { @"S", @"South" },
      { @"W", @"West" }
    };

    public static System.Collections.Generic.Dictionary<string, string[]> StreetTypes = new System.Collections.Generic.Dictionary<string, string[]>()
    {
      { @"AV", new string[] { @"Avenue" } },
      { @"BL", new string[] { @"Boulevard", @"Blvd" } },
      { @"CI", new string[] { @"Circle" } },
      { @"CT", new string[] { @"Court", @"Crt" } },
      { @"DR", new string[] { @"Drive" } },
      { @"HY", new string[] { @"Highway", @"Hwy" } },
      { @"LN", new string[] { @"Lane" } },
      { @"LP", new string[] { @"Loop" } },
      { @"PL", new string[] { @"Place" } },
      { @"PW", new string[] { @"Parkway", @"Pkwy" } },
      { @"RD", new string[] { @"Road" } },
      { @"SQ", new string[] { @"Square" } },
      { @"ST", new string[] { @"Street" } },
      { @"SV", new string[] { @"Stravenue" } },
      { @"TE", new string[] { @"Terrace" } },
      { @"TR", new string[] { @"Trail", @"Trl" } },
      { @"WY", new string[] { @"Way" } },
    };

    /// <summary>Implementation of System.ICustomFormatter.Format()</summary>
    public override string Format(string? format, object? arg, System.IFormatProvider? formatProvider)
    {
      if (!string.IsNullOrEmpty(format) && format.StartsWith(FormatIdentifier, System.StringComparison.OrdinalIgnoreCase))
      {
        if (TryFormat(arg?.ToString() ?? string.Empty, format.Substring(FormatIdentifier.Length), out var result))
        {
          return result;
        }
      }

      return HandleOtherFormats(format, arg);
    }

    /// <summary>Try formatting either a latitude or a longitude from a decimal value according to the format parameter as shown.</summary>
    /// <param name="value">Either latitude or longitude.</param>
    /// <param name="format">[D|DM|DMS]{numberOfDecimaPlaces}{NS|EW}</param>
    /// <param name="result">{-}[nn.nnnn\u00B0|nn\u00B0nn.nn\u2032|nn\u00B0nn\u2032nn\u2033]</param>
    /// <returns></returns>
    public static bool TryFormat(string value, string format, out string result)
    {
      if (TryParse(value, out var parts))
      {
        foreach (int index in System.Enum.GetValues(typeof(Parts)).Cast<int>())
        {
          var e = (Parts)index;
          var s = e.ToString();
          var si = (format ?? throw new System.ArgumentNullException(nameof(format))).IndexOf(s, System.StringComparison.Ordinal);
          var sli = si + s.Length;

          if (e == Parts.Direction && si > -1)
          {
            if (sli < format.Length && format[sli] == '$') format = format.Remove(sli, 1);
            else parts[index] = StreetDirections.First(kvp => kvp.Key == parts[index] || kvp.Value.Contains(parts[index], System.StringComparison.OrdinalIgnoreCase)).Key;
          }
          else if (e == Parts.Type && si > -1)
          {
            if (sli < format.Length && format[sli] == '$') format = format.Remove(sli, 1);
            else parts[index] = StreetTypes.First(kvp => kvp.Key == parts[index] || kvp.Value.Contains(parts[index])).Key;
          }

          format = format.Replace(s, $"{{{index}}}", StringComparison.Ordinal);
        }

        result = string.Format(System.Globalization.CultureInfo.CurrentCulture, format, parts).NormalizeAll(' ', c => c == ' ');
        return true;
      }

      result = string.Empty;
      return false;
    }

    private static readonly System.Text.RegularExpressions.Regex _regexParse = new System.Text.RegularExpressions.Regex($@"^(?<Number>\d+)?(?:\s*(?<Direction>{StreetDirections.SelectMany(kvp => new string[] { kvp.Value, kvp.Key }).ToDelimitedString(@"|")})\.?)?(?:\s*(?<Intersection>CL|EPI|PI|SPI)\s+)?(?:\s*(?<Name>.*?))?(?:(?:\s+(?<Type>{StreetTypes.SelectMany(kvp => kvp.Value.Append(kvp.Key)).ToDelimitedString(@"|")})\.?)(?:\s+(?<Unit>.*))?)?$");
    /// <summary>Try parsing dms format.</summary>
    public static bool TryParse(string text, out string[] result)
    {
      try
      {
        var parts = new string[6];

        if (_regexParse.Match(text) is var m && m.Success)
        {
          foreach (int index in System.Enum.GetValues(typeof(Parts)).Cast<int>())
          {
            if (m.Groups[((Parts)index).ToString()] is var g && g.Success && !string.IsNullOrEmpty(g.Value))
            {
              parts[index] = g.Value;
            }
          }

          result = parts;
          return true;
        }
      }
#pragma warning disable CA1031 // Do not catch general exception types.
      catch
#pragma warning restore CA1031 // Do not catch general exception types.
      { }

      result = new string[0];
      return false;
    }
  }
}
