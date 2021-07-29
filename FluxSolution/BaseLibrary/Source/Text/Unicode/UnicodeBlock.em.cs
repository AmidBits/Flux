namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Returns the first UTF32 code point in the specified Unicode block.</summary>
    public static int GetCodePointFirst(this Text.UnicodeBlock block)
      => (int)((long)block >> 32 & 0x7FFFFFFF);
    /// <summary>Returns the last UTF32 code point in the specified Unicode block.</summary>
    public static int GetCodePointLast(this Text.UnicodeBlock block)
      => (int)((long)block & 0x7FFFFFFF);

    /// <summary>One or more (even many) code points may be unassigned.</summary>
    public static System.Collections.Generic.IEnumerable<System.Text.Rune> GetCodePoints(this Text.UnicodeBlock block)
    {
      for (int codePoint = GetCodePointFirst(block), last = GetCodePointLast(block); codePoint <= last; codePoint++)
        yield return (System.Text.Rune)codePoint;
    }

    public static string ToConsoleTable(this Text.UnicodeBlock block, int skipStart, int skipEnd)
    {
      var sb = new System.Text.StringBuilder();

      var start = GetCodePointFirst(block) + skipStart;
      var end = GetCodePointLast(block) - skipEnd;

      var last = RoundUpToMultipleOf(0x10, end);
      var first = RoundDownToMultipleOf(0x10, start);

      var rows = (last - first) / 0x10;

      sb.Append("       ");
      for (var c = 0; c < 0x10; c++)
        sb.Append($" {c.ToString("X1")}");
      sb.AppendLine();

      for (var r = 0; r < rows; ++r)
      {
        sb.Append($"U+{(first + 0x10 * r):x5}".Substring(0, 6) + 'x');

        for (var c = 0; c < 0x10; ++c)
        {
          var cur = (first + 0x10 * r + c);

          if (cur < start)
            sb.Append($" {(char)(0x20)} ");
          else if (end < cur)
            sb.Append($" {(char)(0x20)} ");
          else
          {
            var chars = char.ConvertFromUtf32((int)cur);

            if (System.Globalization.CharUnicodeInfo.GetUnicodeCategory(chars[0]) is var uc && (uc == System.Globalization.UnicodeCategory.OtherNotAssigned || uc == System.Globalization.UnicodeCategory.PrivateUse))
              sb.Append($"  ");
            else
              sb.Append($" {chars}");
          }
        }

        sb.AppendLine();

        if (0 < r && r % 0x10 == 0)
          sb.AppendLine();
      }

      return sb.ToString();

      static int RoundUpToMultipleOf(int b, int u)
        => RoundDownToMultipleOf(b, u) + b;
      static int RoundDownToMultipleOf(int b, int u)
        => u - (u % b);
    }

  }
}
