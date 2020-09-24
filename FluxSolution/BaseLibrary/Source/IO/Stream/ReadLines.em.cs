namespace Flux
{
  // http://msdn.microsoft.com/query/dev12.query?appId=Dev12IDEF1&l=EN-US&k=k(System.IO.Stream);k(Stream);k(TargetFrameworkMoniker-.NETCore,Version%3Dv4.5.1);k(DevLang-csharp)&rd=true

  public static partial class Xtensions
  {
    /// <summary>Returns an IEnumerable<string> from a stream.</summary>
    public static System.Collections.Generic.IEnumerable<string> ReadLines(this System.IO.Stream source, System.Text.Encoding encoding)
    {
      using System.IO.StreamReader sr = new System.IO.StreamReader(source, encoding);

      while (!sr.EndOfStream)
      {
        yield return sr.ReadLine() ?? string.Empty;
      }
    }
  }
}
