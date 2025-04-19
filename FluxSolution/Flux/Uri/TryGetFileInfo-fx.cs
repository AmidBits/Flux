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
  }
}
