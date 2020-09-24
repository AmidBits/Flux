using System.Linq;

namespace Flux
{
  // http://msdn.microsoft.com/query/dev12.query?appId=Dev12IDEF1&l=EN-US&k=k(System.IO.Stream);k(Stream);k(TargetFrameworkMoniker-.NETCore,Version%3Dv4.5.1);k(DevLang-csharp)&rd=true

  public static partial class Xtensions
  {
    /// <summary>Writes an string[] or a params of strings to a stream.</summary>
    public static void WriteLines(this System.IO.Stream target, params string[] lines)
    {
      using System.IO.StreamWriter sw = new System.IO.StreamWriter(target);
      
      foreach (string line in lines)
      {
        sw.WriteLine(line);
      }
    }
    /// <summary>Writes an string[] or a params of strings to a stream.</summary>
    public static void WriteLines(this System.IO.Stream target, System.Collections.Generic.IEnumerable<string> lines)
    {
      if (lines is null) throw new System.ArgumentNullException(nameof(lines));

      using System.IO.StreamWriter sw = new System.IO.StreamWriter(target);
      
      foreach (string line in lines)
      {
        sw.WriteLine(line);
      }
    }
  }
}
