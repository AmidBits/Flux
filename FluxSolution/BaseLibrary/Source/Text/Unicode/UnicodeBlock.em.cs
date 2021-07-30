namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Returns the first UTF32 code point in the specified Unicode block.</summary>
    public static int GetCodePointFirst(this Text.UnicodeBlock source)
      => (int)((long)source >> 32 & 0x7FFFFFFF);
    /// <summary>Returns the last UTF32 code point in the specified Unicode block.</summary>
    public static int GetCodePointLast(this Text.UnicodeBlock source)
      => (int)((long)source & 0x7FFFFFFF);

    /// <summary>One or more (even many) code points may be unassigned.</summary>
    public static System.Collections.Generic.IEnumerable<System.Text.Rune> GetCodePoints(this Text.UnicodeBlock source)
    {
      for (int codePoint = GetCodePointFirst(source), last = GetCodePointLast(source); codePoint <= last; codePoint++)
        yield return (System.Text.Rune)codePoint;
    }

    public static string ToConsoleTable(this Text.UnicodeBlock source, int skipFirst, int skipLast)
    {
      var sb = new System.Text.StringBuilder();

      var start = GetCodePointFirst(source) + skipFirst;
      var end = GetCodePointLast(source) - skipLast;

      var last = RoundUpToMultipleOf(0x10, end);
      var first = RoundDownToMultipleOf(0x10, start);

      var digitCount = System.Convert.ToInt32(System.Math.Log10(last));

      sb.Append("  " + new string(' ', digitCount));
      for (var columnHeading = 0; columnHeading < 0x10; columnHeading++)
        sb.Append($" {columnHeading.ToString("X1")}");
      sb.AppendLine();

      var format = $"U+{{0:x{digitCount}}}";

      for (int row = 0, maxRows = (last - first) / 0x10; row < maxRows; ++row)
      {
        //sb.Append($"U+{(first + 0x10 * r):x5}".Substring(0, 6) + 'x');
        sb.Append(string.Format(format, first + 0x10 * row).Substring(0, 2 + digitCount - 1) + 'x');
        for (var column = 0; column < 0x10; ++column)
        {
          var current = (first + 0x10 * row + column);

          if (current < start || current > end) sb.Append($"  ");
          else
          {
            var chars = char.ConvertFromUtf32(current);

            if (System.Globalization.CharUnicodeInfo.GetUnicodeCategory(chars[0]) is var uc && (uc == System.Globalization.UnicodeCategory.OtherNotAssigned || uc == System.Globalization.UnicodeCategory.PrivateUse))
              sb.Append(@"  ");
            else
              sb.Append($" {chars}");
          }
        }

        sb.AppendLine();

        if (0 < row && row % 0x10 == 0)
          sb.AppendLine();
      }

      return sb.ToString();

      static int RoundUpToMultipleOf(int b, int u)
        => RoundDownToMultipleOf(b, u) + b;
      static int RoundDownToMultipleOf(int b, int u)
        => u - (u % b);
    }
    public static string ToConsoleTable(this Text.UnicodeBlock source)
      => ToConsoleTable(source, 0, 0);
  }
}
