namespace Flux
{
  public static partial class UriExtensions
  {
    extension(System.Uri uri)
    {
      #region GetStream

      /// <summary>Creates and returns a System.IO.Stream from the URI source.</summary>
      /// <param name="source"></param>
      /// <returns>A stream from a file, http or other URI resource.</returns>
      /// <example>new System.IO.StreamReader(new System.Uri(@"file://\Flux\Resources\Data\Ucd_UnicodeText.txt\").GetStream(), System.Text.Encoding.UTF8)</example>
      public System.IO.Stream GetStream()
      {
        System.ArgumentNullException.ThrowIfNull(uri);

        if (uri.TryGetFileInfo(out var fileInfo))
          return fileInfo.OpenRead();

        //if (source.IsFile) // If the URI is a file, create a local FileStream from the URI data.
        //  return new System.IO.FileStream(source.LocalPath.StartsWith(@"/") ? source.LocalPath[1..] : source.LocalPath, System.IO.FileMode.Open);

        using var hc = new System.Net.Http.HttpClient();
        using var response = hc.SendAsync(new System.Net.Http.HttpRequestMessage(System.Net.Http.HttpMethod.Get, uri), System.Net.Http.HttpCompletionOption.ResponseHeadersRead).Result;
        return response.Content.ReadAsStreamAsync().Result;
      }

      public bool TryGetStream(out System.IO.Stream stream)
      {
        try
        {
          stream = uri.GetStream();
          return true;
        }
        catch { }

        stream = System.IO.Stream.Null;
        return false;
      }

      #endregion

      #region TryConnectTo

      public bool TryConnectTo()
      {
        try
        {
          System.ArgumentNullException.ThrowIfNull(uri);

          using var wc = new System.Net.Http.HttpClient();
          using var s = wc.GetStreamAsync(uri);

          return true;
        }
        catch { }
        return false;
      }

      #endregion

      #region Try/GetDirectoryInfo

      public System.IO.DirectoryInfo GetDirectoryInfo()
      {
        System.ArgumentNullException.ThrowIfNull(uri);

        if (uri.IsFile) // Is file:// URI.
          return new System.IO.DirectoryInfo(uri.LocalPath.TrimCommonPrefix(['/']).ToString());

        throw new System.InvalidOperationException("Expected URI file://.");
      }

      public bool TryGetDirectoryInfo(out System.IO.DirectoryInfo directoryInfo)
      {
        try
        {
          directoryInfo = GetDirectoryInfo(uri);

          return directoryInfo.Exists;
        }
        catch { }

        directoryInfo = default!;
        return false;
      }

      #endregion

      #region Try/GetFileInfo

      public System.IO.FileInfo GetFileInfo()
      {
        System.ArgumentNullException.ThrowIfNull(uri);

        if (uri.IsFile) // Is file:// URI.
          return new System.IO.FileInfo(uri.LocalPath.AsSpan().TrimCommonPrefix(['/']).TrimCommonSuffix(['/']).ToString());

        throw new System.UriFormatException("Expected URI file://.");
      }

      public bool TryGetFileInfo(out System.IO.FileInfo fileInfo)
      {
        try
        {
          fileInfo = GetFileInfo(uri);

          return fileInfo.Exists;
        }
        catch { }

        fileInfo = default!;
        return false;
      }

      #endregion
    }
  }
}
