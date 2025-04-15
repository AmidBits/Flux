namespace Flux
{
  public static partial class Uris
  {
    public static bool TryGetFileInfo(this System.Uri source, out System.IO.FileInfo fileInfo)
    {
      try
      {
        System.ArgumentNullException.ThrowIfNull(source);

        if (source.IsFile)
        {
          var localPath = source.LocalPath;

          if (localPath.StartsWith('/'))
            localPath = localPath[1..];

          if (localPath.EndsWith('/'))
            localPath = localPath[..^1];

          fileInfo = new System.IO.FileInfo(localPath);

          if (fileInfo.Exists) // Check and fall through on non-existent.
            return true;
        }
      }
      catch { }

      fileInfo = default!;
      return false;
    }
  }
}
