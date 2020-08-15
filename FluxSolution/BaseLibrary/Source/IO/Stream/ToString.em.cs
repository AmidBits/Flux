using System.Linq;

namespace Flux
{
  // http://msdn.microsoft.com/query/dev12.query?appId=Dev12IDEF1&l=EN-US&k=k(System.IO.Stream);k(Stream);k(TargetFrameworkMoniker-.NETCore,Version%3Dv4.5.1);k(DevLang-csharp)&rd=true

  public static partial class XtendStream
  {
    /// <summary>Return the stream as a string using the specified encoding.</summary>
    public static string ToString(this System.IO.Stream stream, System.Text.Encoding encoding)
    {
      using System.IO.StreamReader sr = new System.IO.StreamReader(stream, encoding);
      
      return sr.ReadToEnd();
    }
  }
}
