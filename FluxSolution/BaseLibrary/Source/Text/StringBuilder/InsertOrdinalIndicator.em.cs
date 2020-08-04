namespace Flux
{
  public static partial class XtensionsStringBuilder
  {
    /// <summary>Returns a new string with ordinal extensions (e.g. 3rd, 12th, etc.) for all numeric substrings surrounded by spaces (or the beginning and end of the string).</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Ordinal_indicator"/>
    public static System.Text.StringBuilder InsertOrdinalIndicator(this System.Text.StringBuilder source)
    {
      var wasDigit = false;

      for (var index = source.Length - 1; index >= 0; index--)
      {
        var c = source[index];

        var isDigit = char.IsDigit(c);

        if (isDigit && !wasDigit)
        {
          var isTenth = index > 0 && source[index - 1] == '1';

          switch (c)
          {
            case '1' when !isTenth:
              source.Insert(index + 1, @"st");
              break;
            case '2' when !isTenth:
              source.Insert(index + 1, @"nd");
              break;
            case '3' when !isTenth:
              source.Insert(index + 1, @"rd");
              break;
            default:
              source.Insert(index + 1, @"th");
              break;
          }
        }

        wasDigit = isDigit;
      }

      return source;
    }
  }
}
