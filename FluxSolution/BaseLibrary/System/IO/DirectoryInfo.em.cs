namespace Flux
{
  public static partial class Fx
  {
    public static void DeleteContent(this System.IO.DirectoryInfo directoryInfo)
    {
      directoryInfo.DeleteDirectories(@"*", System.IO.SearchOption.TopDirectoryOnly, true);
      directoryInfo.DeleteFiles(@"*", System.IO.SearchOption.TopDirectoryOnly);
    }

    public static void DeleteDirectories(this System.IO.DirectoryInfo directoryInfo, string searchPattern, System.IO.SearchOption searchOption, bool recursiveDelete)
    {
      System.ArgumentNullException.ThrowIfNull(directoryInfo);

      foreach (var directory in directoryInfo.EnumerateDirectories(searchPattern, searchOption))
        directory.Delete(recursiveDelete);
    }
    public static void DeleteFiles(this System.IO.DirectoryInfo directoryInfo, string searchPattern, System.IO.SearchOption searchOption)
    {
      System.ArgumentNullException.ThrowIfNull(directoryInfo);

      foreach (var file in directoryInfo.EnumerateFiles(searchPattern, searchOption))
        file.Delete();
    }

    public static System.Collections.Generic.IEnumerable<System.IO.FileInfo> DirectorySearch(this System.IO.DirectoryInfo directoryInfo, System.Func<System.IO.FileInfo, bool> predicateFile, System.Func<System.IO.DirectoryInfo, bool> predicateDirectory)
    {
      System.ArgumentNullException.ThrowIfNull(directoryInfo);

      predicateFile ??= fi => true;
      predicateDirectory ??= di => true;

      if (directoryInfo.Exists)
      {
        var fileInfos = System.Array.Empty<System.IO.FileInfo>();

        try { fileInfos = directoryInfo.GetFiles(); }
        catch { }

        foreach (var fi in fileInfos)
          if (predicateFile(fi))
            yield return fi;

        var directoryInfos = System.Array.Empty<System.IO.DirectoryInfo>();

        try { directoryInfos = directoryInfo.GetDirectories(); }
        catch { }

        foreach (var di in directoryInfos)
          if (predicateDirectory(di))
            foreach (var fi in DirectorySearch(di, predicateFile, predicateDirectory!))
              yield return fi;
      }
    }
  }
}
