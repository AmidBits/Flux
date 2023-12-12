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

      for (var index = 0; index < fileInfos.Count; index++)
      {
        fileInfo = fileInfos[index];

        var fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(fileInfo.Name);

        if (source.TryMatchCulture(fileNameWithoutExtension, out var hierarchy))
          list.Add((hierarchy, index, fileInfo));
      }

      if (list.Count > 0)
      {
        list.Sort();

        fileInfo = list.First().fileInfo;
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

        dataTable = streamReader.ToDataTable(s => s.Length > 0, itemArraySelector, System.IO.Path.GetFileNameWithoutExtension(source.Name));

        return true;
      }

      dataTable = default!;
      fileInfo = default;
      return false;
    }
  }
}
