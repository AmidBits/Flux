namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Returns a <see cref="System.IO.DirectoryInfo"/> object for the specified <see cref="System.Environment.SpecialFolder"/>.</para>
    /// </summary>
    public static System.IO.DirectoryInfo? GetDirectoryInfo(this System.Environment.SpecialFolder specialFolder)
      => System.Environment.GetFolderPath(specialFolder) is var fp && string.IsNullOrEmpty(fp)
      ? default
      : new System.IO.DirectoryInfo(fp);
  }
}
