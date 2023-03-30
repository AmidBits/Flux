namespace Flux
{
  public static partial class ExtensionMethodsIO
  {
    /// <summary>Returns a new sequence with all lines from the <see cref="System.IO.TextReader"/> and whether to keep empty lines.</summary>
    public static System.Collections.Generic.IEnumerable<string> ReadLines(this System.IO.TextReader source, bool keepEmptyLines)
    {
      for (var line = source.ReadLine(); line is not null; line = source.ReadLine())
        if (line.Length > 0 || keepEmptyLines)
          yield return line;
    }
  }
}
