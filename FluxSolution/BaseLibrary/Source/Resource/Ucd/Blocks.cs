namespace Flux.Resources.Ucd
{
  /// <summary>
  /// <para>The Unicode block database.</para>
  /// <para><see href="https://www.unicode.org/"/></para>
  /// <para><seealso href="https://unicode.org/Public/"/></para>
  /// <para><seealso href="https://www.unicode.org/Public/UCD/latest/ucd"/></para>
  /// </summary>
  // Download URL: https://www.unicode.org/Public/UCD/latest/ucd/Blocks.txt
  public sealed partial class Blocks
    : ITabularDataAcquirable
  {
    public static readonly System.Uri Local = new(@"file://\Resources\Ucd\Blocks.txt");
    public static readonly System.Uri Origin = new(@"https://www.unicode.org/Public/UCD/latest/ucd/Blocks.txt");

    public System.Uri Uri { get; private set; } = Local;

#if NET7_0_OR_GREATER
    [System.Text.RegularExpressions.GeneratedRegex(@"(\.\.|; )", System.Text.RegularExpressions.RegexOptions.ExplicitCapture)]
    private static partial System.Text.RegularExpressions.Regex SplitRegex();
#else
        private static System.Text.RegularExpressions.Regex SplitRegex() => new(@"(\.\.|; )");
#endif

    /// <summary>Returns the UCD Blocks information.</summary>
    public static System.Collections.Generic.IEnumerable<string[]> GetData(System.Uri uri)
    {
      using var stream = uri.GetStream();
      using var reader = new System.IO.StreamReader(stream, System.Text.Encoding.UTF8);

      var reSplit = SplitRegex();

      foreach (var line in reader.ReadLines(s => s.Length > 0, s => s))
        if (line.Length > 0 && !line.StartsWith('#'))
          yield return reSplit.Split(line);
    }

    #region Implemented interfaces

    public string[] FieldNames => new string[] { "StartCode", "EndCode", "BlockName" };
    public System.Type[] FieldTypes => new System.Type[] { typeof(int), typeof(int), typeof(string) };

    public System.Collections.Generic.IEnumerable<object[]> GetFieldValues() => GetData(Uri);

    #endregion // Implemented interfaces
  }
}
