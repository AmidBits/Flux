namespace Flux
{
  public static partial class CultureInfos
  {
    /// <summary>
    /// <para>Try to locate a <paramref name="fileInfo"/> in <paramref name="directory"/> that matches the <paramref name="source"/>.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="fileInfo"></param>
    /// <param name="directory"></param>
    /// <returns></returns>
    public static bool TryLocateCultureFile(this System.Globalization.CultureInfo source, out System.IO.FileInfo? fileInfo, string directory)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      if (GetFileInfos(directory) is var fileInfos && source.TryLocateCulture(out var dt, fileInfos.Select(fi => fi.FullName).ToArray()))
      {
        fileInfo = fileInfos.First(fi => string.Equals(fi.FullName, dt.Rows[0][0]));
        return true;
      }

      fileInfo = default;
      return false;
    }
  }
}
