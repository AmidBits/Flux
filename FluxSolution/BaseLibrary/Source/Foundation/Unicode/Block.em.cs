namespace Flux
{
  public static partial class Unicode
  {
    /// <summary>Creates a new sequence of all runes in the specified Unicode block.</summary>
    public static System.Collections.Generic.IEnumerable<System.Text.Rune> GetAllRunes(this UnicodeBlock source)
    {
      for (int first = GetMinUtf32(source), last = GetMaxUtf32(source); first <= last; first++)
        yield return (System.Text.Rune)first;
    }

    /// <summary>Returns the last UTF-32 value in the specified Unicode block.</summary>
    public static int GetMaxUtf32(this UnicodeBlock source)
      => (int)source & 0x7FFFFFFF;
    /// <summary>Returns the first UTF-32 value in the specified Unicode block.</summary>
    public static int GetMinUtf32(this UnicodeBlock source)
      => (int)((long)source >> 32 & 0x7FFFFFFF);

    public static bool IsSurrogate(this UnicodeBlock source)
      => source == UnicodeBlock.HighSurrogates || source == UnicodeBlock.LowSurrogates || source == UnicodeBlock.HighPrivateUseSurrogates;

    public static string ToConsoleTable(this UnicodeBlock source, int skipFirst, int skipLast, bool includeTitle = true)
    {
      var sb = new System.Text.StringBuilder();

      var actualFirst = GetMinUtf32(source) + skipFirst;
      var actualLast = GetMaxUtf32(source) - skipLast;

      var roundedFirst = Maths.RoundToMultiple(actualFirst, 0x10, FullRounding.Floor);
      var roundedLast = Maths.RoundToMultiple(actualLast, 0x10, FullRounding.Ceiling);

      var digitCount = System.Math.Max(System.Convert.ToInt32(System.Math.Log10(roundedLast)), 4); // Show 4 or 5 digits.

      if (includeTitle)
        sb.AppendLine($"{source} (Unicode block)");

      sb.Append(' ', 2 + digitCount);
      for (var columnHeading = 0; columnHeading < 0x10; columnHeading++)
      {
        sb.Append(' ');
        sb.Append(columnHeading.ToString("X1"));
      }

      sb.AppendLine();

      var format = $"U+{{0:x{digitCount}}}";

      for (int row = 0, maxRows = (roundedLast - roundedFirst) / 0x10; row < maxRows; ++row)
      {
        sb.Append(string.Format(format, roundedFirst + 0x10 * row)[..(2 + digitCount - 1)] + 'x');

        for (var column = 0; column < 0x10; ++column)
        {
          var current = roundedFirst + 0x10 * row + column;

          var chars = char.ConvertFromUtf32(current);

          sb.Append(' ');

          switch (System.Globalization.CharUnicodeInfo.GetUnicodeCategory(chars[0]))
          {
            case System.Globalization.UnicodeCategory.OtherNotAssigned:
            case System.Globalization.UnicodeCategory.PrivateUse:
              sb.Append(' ');
              break;
            default:
              sb.Append(chars);
              break;
          }
        }

        sb.AppendLine();

        if (row > 0 && (row % 0x10) == 0)
          sb.AppendLine();
      }

      return sb.ToString();
    }
    public static string ToConsoleTable(this UnicodeBlock source)
      => ToConsoleTable(source, 0, 0);
  }
}
