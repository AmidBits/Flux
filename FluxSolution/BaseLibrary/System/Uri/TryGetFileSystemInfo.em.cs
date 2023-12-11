namespace Flux
{
  public static partial class Fx
  {
    public static bool TryGetFileInfo(this System.Uri source, out System.IO.FileInfo? fileInfo)
    {
      try
      {
        fileInfo = new System.IO.FileInfo(source.LocalPath.StartsWith('/') ? source.LocalPath[1..] : source.LocalPath);

        if (fileInfo.Exists)
          return true;
      }
      catch { }

      fileInfo = default;
      return false;
    }

    public static bool TryGetFileInfo(this System.Collections.Generic.IEnumerable<System.Uri> source, out System.IO.FileInfo? fileInfo)
    {
      foreach (var uri in source)
      {
        if (uri.TryGetFileInfo(out fileInfo))
          return true;
      }

      fileInfo = default;
      return false;
    }
  }
}
