namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Creates and returns a System.IO.Stream from the URI source.</summary>
    /// <param name="uri"></param>
    /// <returns>A stream from a file, http or other URI resource.</returns>
    /// <example>new System.IO.StreamReader(new System.Uri(@"file://\Flux\Resources\Data\Ucd_UnicodeText.txt\").GetStream(), System.Text.Encoding.UTF8)</example>
    public static System.IO.Stream GetStream(this System.Uri uri)
    {
      if (uri is null) throw new System.ArgumentNullException(nameof(uri));

      if (uri.IsFile) // If the URI is a file, create a local FileStream from the URI data.
        return new System.IO.FileStream(uri.LocalPath.StartsWith(@"/") ? uri.LocalPath[1..] : uri.LocalPath, System.IO.FileMode.Open);

      using var hc = new System.Net.Http.HttpClient();
      using var response = hc.SendAsync(new System.Net.Http.HttpRequestMessage(System.Net.Http.HttpMethod.Get, uri), System.Net.Http.HttpCompletionOption.ResponseHeadersRead).Result;
      return response.Content.ReadAsStreamAsync().Result;
    }
  }
}
