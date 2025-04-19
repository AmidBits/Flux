namespace Flux
{
  public static partial class CultureInfos
  {
    /// <summary>
    /// <para>Returns the IPA dictionary for the <paramref name="source"/>, if available. Uses the specified <paramref name="comparer"/>, or default if null.</para>
    /// <see href="https://github.com/open-dict-data/ipa-dict/tree/master/"/>
    /// </summary>
    public const string ResourcesDirectoryIpa = @"file://\Resources\Dictionaries\Ipa\";

    private static readonly Dictionary<System.Globalization.CultureInfo, System.Data.DataTable> m_cultureInfoDataTables = [];

    public static System.Data.DataTable GetIpaDictionary(this System.Globalization.CultureInfo source, string directory = ResourcesDirectoryIpa)
    {
      if (!m_cultureInfoDataTables.TryGetValue(source, out var dataTable)) // Use cached data if available.
      {
        if (!source.TryLocateCultureFile(out var fileInfo, directory)) throw new System.InvalidOperationException($"Culture file not found for {source}.");

        using var fileStream = fileInfo?.OpenRead() ?? throw new System.NullReferenceException(nameof(fileInfo));
        using var streamReader = new System.IO.StreamReader(fileStream, System.Text.Encoding.UTF8);

        dataTable = streamReader.ToDataTable(s => s.Length > 0, line => line.Split('\t'), source.Name);

        dataTable.DefaultView.Sort = 1.ToSingleOrdinalColumnName() + " ASC";

        m_cultureInfoDataTables.Add(source, dataTable); // We cache the loaded data.
      }

      return dataTable;
    }

    public static bool TryGetIpaDictionary(this System.Globalization.CultureInfo source, out System.Data.DataTable ipa, string directory = ResourcesDirectoryIpa)
    {
      try
      {
        ipa = source.GetIpaDictionary(directory);
        return true;
      }
      catch { }

      ipa = default!;
      return false;
    }

    public static System.IO.FileInfo[] GetFileInfos(string directory)
      => new System.Uri(directory).TryGetDirectoryInfo(out var directoryInfo)
      ? new System.IO.DirectoryInfo(directoryInfo.FullName).GetFiles()
      : throw new System.InvalidOperationException("Not Found");
  }
}
