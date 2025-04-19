namespace Flux
{
  public static partial class Uris
  {
    public static bool TryGetDirectoryInfo(this System.Uri source, out System.IO.DirectoryInfo directoryInfo)
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
  }
}
