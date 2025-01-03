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
    [System.Text.RegularExpressions.GeneratedRegex(@"(\.\.|; )", System.Text.RegularExpressions.RegexOptions.ExplicitCapture)]
    private static partial System.Text.RegularExpressions.Regex SplitRegex();

    public static readonly System.Uri Local = new(@"file://\Resources\Ucd\Blocks.txt");
    public static readonly System.Uri Origin = new(@"https://www.unicode.org/Public/UCD/latest/ucd/Blocks.txt");

    public System.Uri Uri { get; private set; } = Local;

    /// <summary>Returns the UCD Blocks information.</summary>
    public static System.Collections.Generic.IEnumerable<string[]> GetData(System.Uri uri)
    {
      using var stream = uri.GetStream();
      using var reader = new System.IO.StreamReader(stream, System.Text.Encoding.UTF8);

      foreach (var fields in reader.ReadLines(s => s.Length > 0 && !s.StartsWith('#'), SplitRegex().Split))
        yield return fields;
    }

    #region Implemented interfaces

    public string[] FieldNames => new string[] { "StartCode", "EndCode", "BlockName" };
    public System.Type[] FieldTypes => new System.Type[] { typeof(int), typeof(int), typeof(string) };

    public System.Collections.Generic.IEnumerable<object[]> GetFieldValues() => GetData(Uri);

    #endregion // Implemented interfaces
  }
}
