namespace Flux.Media.Music
{
  public static class Note
  {
    public static readonly double ReferenceTuning = 440.0;

    public const char SignFlat = '\u266D';
    public const char SignSharp = '\u266F';

    public static readonly System.Collections.Generic.IEnumerable<string> Names = System.Linq.Enumerable.Empty<string>().Append(@"C", $"C{SignSharp}/D{SignFlat}", @"D", $"D{SignSharp}/E{SignFlat}", @"E", @"F", $"F{SignSharp}/G{SignFlat}", @"G", $"G{SignSharp}/A{SignFlat}", @"A", $"A{SignSharp}/B{SignFlat}", @"B");
  }
}
