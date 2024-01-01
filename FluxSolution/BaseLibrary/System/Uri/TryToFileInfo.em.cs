namespace Flux
{
  public static partial class Fx
  {
    public static bool TryToFileInfo(this System.Uri source, out System.IO.FileInfo fileInfo)
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

      fileInfo = default!;
      return false;
    }
  }
}
