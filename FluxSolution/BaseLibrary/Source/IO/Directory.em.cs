namespace Flux
{
  public static partial class Xtensions
  {
    public static void DeleteContent(this System.IO.DirectoryInfo directoryInfo)
    {
      directoryInfo.DeleteDirectories(@"*", System.IO.SearchOption.TopDirectoryOnly, true);

      directoryInfo.DeleteFiles(@"*", System.IO.SearchOption.TopDirectoryOnly);
    }

    public static void DeleteDirectories(this System.IO.DirectoryInfo directoryInfo, string searchPattern, System.IO.SearchOption searchOption, bool recursiveDelete)
    {
      if (directoryInfo is null) throw new System.ArgumentNullException(nameof(directoryInfo));

      foreach (var directory in directoryInfo.EnumerateDirectories(searchPattern, searchOption))
      {
        directory.Delete(recursiveDelete);
      }
    }
    public static void DeleteFiles(this System.IO.DirectoryInfo directoryInfo, string searchPattern, System.IO.SearchOption searchOption)
    {
      if (directoryInfo is null) throw new System.ArgumentNullException(nameof(directoryInfo));

      foreach (var file in directoryInfo.EnumerateFiles(searchPattern, searchOption))
      {
        file.Delete();
      }
    }

    public static System.Collections.Generic.IEnumerable<System.IO.FileInfo> DirectorySearch(this System.IO.DirectoryInfo directoryInfo, System.Func<System.IO.FileInfo, bool> predicateFile, System.Func<System.IO.DirectoryInfo, bool> predicateDirectory)
    {
      if (directoryInfo is null) throw new System.ArgumentNullException(nameof(directoryInfo));

#pragma warning disable CA1031 // Do not catch general exception types
      System.IO.FileInfo[] GetFileInfos() { try { return directoryInfo.GetFiles(); } catch { return System.Array.Empty<System.IO.FileInfo>(); } }
      System.IO.DirectoryInfo[] GetDirectoryInfos() { try { return directoryInfo.GetDirectories(); } catch { return System.Array.Empty<System.IO.DirectoryInfo>(); } }
#pragma warning restore CA1031 // Do not catch general exception types

      if (directoryInfo.Exists)
      {
        foreach (var fi in GetFileInfos())
        {
          if (predicateFile?.Invoke(fi) ?? true)
          {
            yield return fi;
          }
        }

        foreach (var di in GetDirectoryInfos())
        {
          if (predicateDirectory?.Invoke(di) ?? true)
          {
            foreach (var fi in DirectorySearch(di, predicateFile, predicateDirectory!))
            {
              yield return fi;
            }
          }
        }
      }
    }
  }
}
