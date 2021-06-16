namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Returns a new string with ordinal extensions (e.g. 3rd, 12th, etc.) for all numeric substrings surrounded by spaces (or the beginning and end of the string).</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Ordinal_indicator"/>
    public static string InsertOrdinalIndicator(this string source)
      => source.InsertOrdinalIndicator(match => true);
    /// <summary>Returns a new string with ordinal extensions (e.g. 3rd, 12th, etc.) for all numeric substrings surrounded by spaces (or the beginning and end of the string), if the predicate(leftOfMatch, matchedDigits, rightOfMatch, matchIndex) is satisfied.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Ordinal_indicator"/>
    public static string InsertOrdinalIndicator(this string source, System.Func<(string leftOfMatch, string matchedDigits, string rightOfMatch, int indexOfMatch), bool> predicate)
    {
      var index = 0;

      return System.Text.RegularExpressions.Regex.Replace(source, @"(?<=^|[^\d])\d+(?=[^\d]|$)", (match) =>
      {
        if (predicate((source.Substring(0, match.Index), match.Value, source.Substring(match.Index + match.Length), index++)))
        {
          var isTenth1 = match.Length > 1 && match.Value[match.Length - 2] == '1';

          return (match.Value[match.Length - 1]) switch
          {
            '1' when !isTenth1 => match.Value + @"st",
            '2' when !isTenth1 => match.Value + @"nd",
            '3' when !isTenth1 => match.Value + @"rd",
            _ => match.Value + @"th",
          };
        }

        return match.Value;
      });
    }
  }
}
