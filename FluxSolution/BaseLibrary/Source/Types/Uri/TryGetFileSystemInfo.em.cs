namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static bool TryGetFileSystemInfo(this System.Uri source, out System.IO.FileSystemInfo fileSystemInfo, params System.Uri[] alternateFiles)
    {
      var list = alternateFiles.ToList();

      list.Insert(0, source);

      for (var index = 0; index < list.Count; index++)
      {
        var uri = list[index];

        try
        {
          fileSystemInfo = new System.IO.FileInfo(uri.LocalPath.StartsWith(@"/") ? uri.LocalPath[1..] : uri.LocalPath);

          if (fileSystemInfo.Exists)
            return true;
        }
        catch { }
      }

      fileSystemInfo = default!;
      return false;
    }
  }
}
