namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Returns the first UTF32 code point in the specified Unicode block.</summary>
    public static int GetUtf32First(this Text.UnicodeBlock source)
      => (int)((long)source >> 32 & 0x7FFFFFFF);
    /// <summary>Returns the last UTF32 code point in the specified Unicode block.</summary>
    public static int GetUtf32Last(this Text.UnicodeBlock source)
      => (int)((long)source & 0x7FFFFFFF);

    /// <summary>One or more (even many) code points may be unassigned.</summary>
    public static System.Collections.Generic.IEnumerable<System.Text.Rune> GetCodePoints(this Text.UnicodeBlock source)
    {
      for (int utf32Current = GetUtf32First(source), utf32Last = GetUtf32Last(source); utf32Current <= utf32Last; utf32Current++)
        yield return (System.Text.Rune)utf32Current;
    }

    public static string ToConsoleTable(this Text.UnicodeBlock source, int skipFirst, int skipLast, bool includeTitle = true)
    {
      var sb = new System.Text.StringBuilder();

      var actualFirst = GetUtf32First(source) + skipFirst;
      var actualLast = GetUtf32Last(source) - skipLast;

      var roundedFirst = Maths.RoundToMultipleOf(actualFirst, 0x10, FullRoundingBehavior.Floor);
      var roundedLast = Maths.RoundToMultipleOf(actualLast, 0x10, FullRoundingBehavior.Ceiling);

      var digitCount = System.Math.Max(System.Convert.ToInt32(System.Math.Log10(roundedLast)), 4); // Show 4 or 5 digits.

      if (includeTitle)
        sb.AppendLine($"{source.ToString()} (Unicode block)");

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
        sb.Append(string.Format(format, roundedFirst + 0x10 * row).Substring(0, 2 + digitCount - 1) + 'x');

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
    public static string ToConsoleTable(this Text.UnicodeBlock source)
      => ToConsoleTable(source, 0, 0);
  }
}
