namespace Flux
{
  public static partial class Fx
  {
    public static bool TryFindCultureInfoFile(this System.Globalization.CultureInfo source, out System.IO.FileInfo? fileInfo, string directory)
    {
      source ??= System.Globalization.CultureInfo.CurrentCulture;

      var fileInfos = GetFileInfos(directory);

      for (var index = 0; index < fileInfos.Count; index++)
      {
        fileInfo = fileInfos[index];

        var fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(fileInfo.Name);

        if (source.TryMatchName(fileNameWithoutExtension))
          return true;
      }

      fileInfo = default;
      return false;
    }
    /// <summary>
    /// <para>Returns the lexicon (word list) for the <paramref name="source"/>, if available.</para>
    /// <see href="https://github.com/open-dict-data/ipa-dict/tree/master/"/>
    /// </summary>
    public static bool TryLoadCultureInfoFile(this System.Globalization.CultureInfo source, System.Func<string, string[]> itemArraySelector, out System.Data.DataTable dataTable, out System.IO.FileInfo? fileInfo, string directory)
    {
      if (source.TryFindCultureInfoFile(out fileInfo, directory))
      {
        dataTable = fileInfo!.ReadIntoDataTable(itemArraySelector, System.Text.Encoding.UTF8);

        return true;

      }

      if (source.Parent != System.Globalization.CultureInfo.InvariantCulture && source.Parent.TryFindCultureInfoFile(out fileInfo, directory))
      {
        dataTable = fileInfo!.ReadIntoDataTable(itemArraySelector, System.Text.Encoding.UTF8);

        return true;
      }

      dataTable = default!;
      fileInfo = default;
      return false;
    }
  }
}
