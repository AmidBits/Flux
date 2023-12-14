namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Try to locate a file that</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="fileInfo"></param>
    /// <param name="directory"></param>
    /// <returns></returns>
    public static bool TryLocateCultureFile(this System.Globalization.CultureInfo source, out System.IO.FileInfo? fileInfo, string directory)
    {
      source ??= System.Globalization.CultureInfo.CurrentCulture;

      var list = new List<(int offset, int index, FileInfo fileInfo)>();

      var fileInfos = GetFileInfos(directory);

      var matched = source.TryLocateCulture(fileInfos.Select(fi => fi.FullName), out var matches, out var dt);

      System.Console.WriteLine(dt.DefaultView.ToConsoleString(new ConsoleStringOptions() { CenterContent = true }));

      if (matched)
      {
        fileInfo = fileInfos.First(fi => string.Equals(fi.FullName, matches.First().text));
        return true;
      }

      fileInfo = default;
      return false;
    }
    /// <summary>
    /// <para>Returns the lexicon (word list) for the <paramref name="source"/>, if available.</para>
    /// <see href="https://github.com/open-dict-data/ipa-dict/tree/master/"/>
    /// </summary>
    public static bool TryLoadCultureFile(this System.Globalization.CultureInfo source, System.Func<string, string[]> itemArraySelector, out System.Data.DataTable dataTable, out System.IO.FileInfo? fileInfo, string directory, System.Text.Encoding? encoding = null)
    {
      if (source.TryLocateCultureFile(out fileInfo, directory))
      {
        using var fileStream = fileInfo?.OpenRead() ?? throw new System.ArgumentNullException(nameof(fileInfo));
        using var streamReader = new System.IO.StreamReader(fileStream, encoding ?? System.Text.Encoding.UTF8);

        dataTable = streamReader.ToDataTable(s => s.Length > 0, itemArraySelector, source.Name);

        return true;
      }

      dataTable = default!;
      fileInfo = default;
      return false;
    }
  }
}
