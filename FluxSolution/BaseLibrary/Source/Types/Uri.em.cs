namespace Flux
{
  public static partial class Xtensions
  {
    /// <summary>Creates and returns a System.IO.Stream from the URI source.</summary>
    /// <param name="uri"></param>
    /// <returns>A stream from a file, http or other URI resource.</returns>
    /// <example>new System.IO.StreamReader(new System.Uri(@"file://\Flux\Resources\Data\Ucd_UnicodeText.txt\").GetStream(), System.Text.Encoding.UTF8)</example>
    public static System.IO.Stream GetStream(this System.Uri uri)
      => (uri ?? throw new System.ArgumentNullException(nameof(uri))).IsFile
      ? (new System.IO.FileStream(uri.LocalPath.StartsWith(@"/", System.StringComparison.Ordinal) ? uri.LocalPath.Substring(1) : uri.LocalPath, System.IO.FileMode.Open))
      : System.Net.WebRequest.Create(uri).GetResponse().GetResponseStream();

    /// <summary>Returns all lines from the URI source.</summary>
    /// <param name="uri"></param>
    /// <param name="encoding"></param>
    /// <returns></returns>
    /// <example>new System.Uri(@"file://\Flux\Resources\Ucd\UnicodeText.txt\").ReadLines(System.Text.Encoding.UTF8)</example>
    public static System.Collections.Generic.IEnumerable<string> ReadLines(this System.Uri uri, System.Text.Encoding encoding)
    {
      using var reader = new System.IO.StreamReader(uri.GetStream(), encoding);

      while (!reader.EndOfStream)
        if (reader.ReadLine() is var line && line is not null && line.Length >= 0)
          yield return line;
    }

    public static byte[] ReadAllBytes(this System.Uri uri)
    {
      using var reader = new System.IO.BinaryReader(uri.GetStream());

      return reader.ReadBytes(int.MaxValue);
    }
    public static string ReadAllText(this System.Uri uri, System.Text.Encoding encoding)
    {
      using var reader = new System.IO.StreamReader(uri.GetStream(), encoding);

      return reader.ReadToEnd();
    }

    public static bool TryConnectTo(this System.Uri uri)
    {
      try
      {
        using var wc = new System.Net.WebClient();
        using var s = wc.OpenRead(uri);

        return true;
      }
#pragma warning disable CA1031 // Do not catch general exception types.
      catch
#pragma warning restore CA1031 // Do not catch general exception types.
      { }
      return false;
    }
  }
}
