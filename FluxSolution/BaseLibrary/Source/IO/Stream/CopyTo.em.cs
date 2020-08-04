using System.Linq;

namespace Flux
{
  // http://msdn.microsoft.com/query/dev12.query?appId=Dev12IDEF1&l=EN-US&k=k(System.IO.Stream);k(Stream);k(TargetFrameworkMoniker-.NETCore,Version%3Dv4.5.1);k(DevLang-csharp)&rd=true

  public static partial class XtensionsStream
  {
    /// <summary>Copies a chunk of bytes from one stream to another.</summary>
    static public int CopyTo(this System.IO.Stream source, System.IO.Stream destination, int bufferSize = 8192)
    {
      var buffer = new byte[bufferSize];

      var bytesCopied = 0;

      for (var bytesRead = -1; bytesRead != 0; bytesCopied += bytesRead)
      {
        bytesRead = source.Read(buffer, 0, buffer.Length);

        if (bytesRead > 0)
        {
          destination.Write(buffer, 0, bytesRead);
        }
      }

      return bytesCopied;
    }
  }
}
