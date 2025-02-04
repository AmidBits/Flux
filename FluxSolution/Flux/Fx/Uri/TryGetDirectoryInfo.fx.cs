namespace Flux
{
  public static partial class Fx
  {
    public static bool TryGetDirectoryInfo(this System.Uri source, out System.IO.DirectoryInfo directoryInfo)
    {
      try
      {
        System.ArgumentNullException.ThrowIfNull(source);

        if (source.IsFile)
        {
          var localPath = source.LocalPath;

          if (localPath.StartsWith('/'))
            localPath = localPath[1..];

          directoryInfo = new System.IO.DirectoryInfo(localPath);

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
