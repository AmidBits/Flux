namespace Flux.Music
{
  public static class Note
  {
    public const char SignFlat = '\u266D';
    public const char SignSharp = '\u266F';

    public static readonly System.Collections.Generic.IList<string> Names = new string[] { @"C", $"C{SignSharp}/D{SignFlat}", @"D", $"D{SignSharp}/E{SignFlat}", @"E", @"F", $"F{SignSharp}/G{SignFlat}", @"G", $"G{SignSharp}/A{SignFlat}", @"A", $"A{SignSharp}/B{SignFlat}", @"B" };
  }
}
