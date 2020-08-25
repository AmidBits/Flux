namespace Flux.Media.Music
{
  public static class Note
  {
    public static readonly double ReferenceTuning = 440.0;

    public static char SignFlat = 'b'; // True Unicode character '\u266D';
    public static char SignSharp = '#'; // True Unicode character '\u266F';

    public static System.Collections.Generic.IEnumerable<string> GetNames() => System.Linq.Enumerable.Empty<string>().Append(@"C", $"C{SignSharp}/D{SignFlat}", @"D", $"D{SignSharp}/E{SignFlat}", @"E", @"F", $"F{SignSharp}/G{SignFlat}", @"G", $"G{SignSharp}/A{SignFlat}", @"A", $"A{SignSharp}/B{SignFlat}", @"B");
  }
}
