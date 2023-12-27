namespace Flux
{
  public static partial class Reflection
  {
    public static bool TryToDirectoryInfo(this System.Uri source, out System.IO.DirectoryInfo? directoryInfo)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      if (source.IsFile)
      {
        directoryInfo = new System.IO.DirectoryInfo(source.LocalPath.StartsWith('/') ? source.LocalPath[1..] : source.LocalPath);

        if (directoryInfo.Exists) // Check and fall through on non-existent.
          return true;
      }

      directoryInfo = default!;
      return false;
    }
  }
}
