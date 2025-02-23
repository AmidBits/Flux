namespace Flux
{
  public static partial class Resources
  {
    #region UCD Blocks

    [System.Text.RegularExpressions.GeneratedRegex(@"(\.\.|; )", System.Text.RegularExpressions.RegexOptions.ExplicitCapture)]
    private static partial System.Text.RegularExpressions.Regex RegexUcdBlocks();

    /// <summary>
    /// <para>The Unicode block database.</para>
    /// <para><see href="https://www.unicode.org/"/></para>
    /// <para><seealso href="https://unicode.org/Public/"/></para>
    /// <para><seealso href="https://www.unicode.org/Public/UCD/latest/ucd"/></para>
    /// <para>Local: <see href="file://\Resources\Ucd\Blocks.txt"/></para>
    /// <para>Remote: <see href="https://www.unicode.org/Public/UCD/latest/ucd/Blocks.txt"/></para>
    /// </summary>
    // Download URL: https://www.unicode.org/Public/UCD/latest/ucd/Blocks.txt
    /// <param name="file"></param>
    /// <returns></returns>
    public static System.Collections.Generic.IEnumerable<string[]> GetUcdBlocks(string file = @"file://\Resources\Ucd\Blocks.txt")
    {
      yield return new string[] { "StartCode", "EndCode", "BlockName" };

      using var stream = new System.Uri(file).GetStream();
      using var reader = new System.IO.StreamReader(stream, System.Text.Encoding.UTF8);

      foreach (var fields in reader.ReadLines(s => s.Length > 0 && !s.StartsWith('#'), RegexUcdBlocks().Split))
        yield return fields;
    }

    #endregion // UCD Blocks
  }
}
