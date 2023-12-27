namespace Flux
{
  public static partial class Reflection
  {
    /// <summary>Creates and returns a System.IO.Stream from the URI source.</summary>
    /// <param name="source"></param>
    /// <returns>A stream from a file, http or other URI resource.</returns>
    /// <example>new System.IO.StreamReader(new System.Uri(@"file://\Flux\Resources\Data\Ucd_UnicodeText.txt\").GetStream(), System.Text.Encoding.UTF8)</example>
    public static System.IO.Stream GetStream(this System.Uri source)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      if (source.TryToFileInfo(out var fi))
        return fi.OpenRead();
      //if (source.IsFile) // If the URI is a file, create a local FileStream from the URI data.
      //  return new System.IO.FileStream(source.LocalPath.StartsWith(@"/") ? source.LocalPath[1..] : source.LocalPath, System.IO.FileMode.Open);

      using var hc = new System.Net.Http.HttpClient();
      using var response = hc.SendAsync(new System.Net.Http.HttpRequestMessage(System.Net.Http.HttpMethod.Get, source), System.Net.Http.HttpCompletionOption.ResponseHeadersRead).Result;
      return response.Content.ReadAsStreamAsync().Result;
    }
  }
}
