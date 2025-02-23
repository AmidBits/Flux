namespace Flux
{
  public static partial class Resources
  {
    #region SCOWL TwoOfTwelveFull

    [System.Text.RegularExpressions.GeneratedRegexAttribute(@"(?<=[\-0-9]+[:#&=]?)\s+", System.Text.RegularExpressions.RegexOptions.Compiled)]
    private static partial System.Text.RegularExpressions.Regex RegexScowlTwoOfTwelveFull();

    /// <summary>
    /// <para>The records from 2Of12Full word list.</para>
    /// <para><see href="https://github.com/en-wl/wordlist"/></para>
    /// <para><seealso cref="http://wordlist.aspell.net/"/></para>
    /// <para><seealso cref="https://github.com/en-wl/wordlist/blob/master/"/></para>
    /// <para>Local: <see href="file://\Resources\Scowl\2of12full.txt"/></para>
    /// <para>Remote: <see href="https://github.com/en-wl/wordlist/raw/master/alt12dicts/2of12full.txt"/></para>
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    public static System.Collections.Generic.IEnumerable<string[]> GetScowlTwoOfTwelveFull(string file = @"file://\Resources\Scowl\2of12full.txt")
    {
      yield return new string[] { "DictionaryCount", "NonVariant", "Variant", "NonAmerican", "SecondClass", "Word" };

      using var stream = new System.Uri(file).GetStream();
      using var reader = new System.IO.StreamReader(stream, System.Text.Encoding.UTF8);

      foreach (var fields in reader.ReadLines(s => s.Length > 0, s => RegexScowlTwoOfTwelveFull().Split(s.Trim())))
        yield return fields;
    }

    #endregion // SCOWL TwoOfTwelveFull
  }
}
