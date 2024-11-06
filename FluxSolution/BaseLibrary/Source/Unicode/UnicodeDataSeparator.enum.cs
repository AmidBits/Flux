namespace Flux
{
  public static partial class Unicode
  {
    public static char GetVisualSymbol(this UnicodeDataSeparator separator)
      => separator switch
      {
        UnicodeDataSeparator.UnitSeparator => '\u241F',
        UnicodeDataSeparator.RecordSeparator => '\u241E',
        UnicodeDataSeparator.GroupSeparator => '\u241D',
        UnicodeDataSeparator.FileSeparator => '\u241C',
        _ => throw new System.NotImplementedException(),
      };
    public static string ToSpacingString(this UnicodeDataSeparator separator)
      => $"{(char)(int)separator}";

    public static bool TryGetUnicodeSpacingChar(this UnicodeDataSeparator separator, out char separatorChar)
      => (separatorChar = (char)(int)separator) != '\0';
  }

  /// <summary>
  /// <para></para>
  /// </summary>
  public enum UnicodeDataSeparator
  {
    UnitSeparator = '\u001F',
    RecordSeparator = '\u001E',
    GroupSeparator = '\u001D',
    FileSeparator = '\u001C',
  }
}
