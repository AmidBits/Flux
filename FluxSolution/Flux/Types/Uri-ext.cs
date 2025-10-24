namespace Flux
{
  public static partial class XtensionUri
  {
    extension(System.Uri source)
    {
      #region GetStream

      /// <summary>Creates and returns a System.IO.Stream from the URI source.</summary>
      /// <param name="source"></param>
      /// <returns>A stream from a file, http or other URI resource.</returns>
      /// <example>new System.IO.StreamReader(new System.Uri(@"file://\Flux\Resources\Data\Ucd_UnicodeText.txt\").GetStream(), System.Text.Encoding.UTF8)</example>
      public System.IO.Stream GetStream()
      {
        System.ArgumentNullException.ThrowIfNull(source);

        if (source.TryGetFileInfo(out var fileInfo))
          return fileInfo.OpenRead();

        //if (source.IsFile) // If the URI is a file, create a local FileStream from the URI data.
        //  return new System.IO.FileStream(source.LocalPath.StartsWith(@"/") ? source.LocalPath[1..] : source.LocalPath, System.IO.FileMode.Open);

        using var hc = new System.Net.Http.HttpClient();
        using var response = hc.SendAsync(new System.Net.Http.HttpRequestMessage(System.Net.Http.HttpMethod.Get, source), System.Net.Http.HttpCompletionOption.ResponseHeadersRead).Result;
        return response.Content.ReadAsStreamAsync().Result;
      }

      public bool TryGetStream(out System.IO.Stream? stream)
      {
        try
        {
          //System.IO.Stream.Null
          stream = source.GetStream();
          return true;
        }
        catch { }

        stream = null;
        return false;
      }

      #endregion

      #region TryConnectTo

      public bool TryConnectTo()
      {
        try
        {
          System.ArgumentNullException.ThrowIfNull(source);

          using var wc = new System.Net.Http.HttpClient();
          using var s = wc.GetStreamAsync(source);

          return true;
        }
        catch { }
        return false;
      }

      #endregion

      #region TryGetDirectoryInfo

      public bool TryGetDirectoryInfo(out System.IO.DirectoryInfo directoryInfo)
      {
        try
        {
          System.ArgumentNullException.ThrowIfNull(source);

          if (source.IsFile)
          {
            var directoryPath = source.LocalPath.AsSpan().TrimCommonPrefix('/').ToString();

            directoryInfo = new System.IO.DirectoryInfo(directoryPath);

            if (directoryInfo.Exists) // Check and fall through on non-existent.
              return true;
          }
        }
        catch { }

        directoryInfo = default!;
        return false;
      }

      #endregion

      #region TryGetFileInfo

      public bool TryGetFileInfo(out System.IO.FileInfo fileInfo)
      {
        try
        {
          System.ArgumentNullException.ThrowIfNull(source);

          if (source.IsFile)
          {
            var filePath = source.LocalPath.AsSpan().TrimCommonPrefix('/').TrimCommonSuffix('/').ToString();

            fileInfo = new System.IO.FileInfo(filePath);

            if (fileInfo.Exists) // Check and fall through on non-existent.
              return true;
          }
        }
        catch { }

        fileInfo = default!;
        return false;
      }

      #endregion
    }
  }
}
