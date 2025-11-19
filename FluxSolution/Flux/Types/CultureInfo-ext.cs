namespace Flux
{
  public static partial class CultureInfoExtensions
  {
    #region IPA helpers

    /// <summary>
    /// <para>Returns the IPA dictionary for the <paramref name="source"/>, if available. Uses the specified <paramref name="comparer"/>, or default if null.</para>
    /// <see href="https://github.com/open-dict-data/ipa-dict/tree/master/"/>
    /// </summary>
    public const string ResourcesDirectoryIpa = @"file://\Resources\Dictionaries\Ipa\";

    private static readonly Dictionary<System.Globalization.CultureInfo, System.Data.DataTable> m_cultureInfoDataTables = [];

    #endregion

    private static System.IO.FileInfo[] GetFileInfos(string directory)
      => new System.Uri(directory).TryGetDirectoryInfo(out var directoryInfo)
      ? new System.IO.DirectoryInfo(directoryInfo.FullName).GetFiles()
      : throw new System.InvalidOperationException("Not Found");

    extension(System.Globalization.CultureInfo)
    {
      public static System.Data.DataTable GetIpaDictionary(System.Globalization.CultureInfo source, string directory = ResourcesDirectoryIpa)
      {
        if (!m_cultureInfoDataTables.TryGetValue(source, out var dataTable)) // Use cached data if available.
        {
          if (!System.Globalization.CultureInfo.TryLocateMatchingFiles(source, out var fileInfo, directory)) throw new System.InvalidOperationException($"Culture file not found for {source}.");

          using var fileStream = fileInfo?.OpenRead() ?? throw new System.NullReferenceException(nameof(fileInfo));
          using var streamReader = new System.IO.StreamReader(fileStream, System.Text.Encoding.UTF8);

          dataTable = streamReader.ToDataTable(s => s.Length > 0, line => line.Split('\t'), source.Name);

          dataTable.DefaultView.Sort = 0.ToSingleOrdinalColumnName() + " ASC";

          m_cultureInfoDataTables.Add(source, dataTable); // We cache the loaded data.
        }

        return dataTable;
      }

      public static bool TryGetIpaDictionary(System.Globalization.CultureInfo source, out System.Data.DataTable ipa, string directory = ResourcesDirectoryIpa)
      {
        try
        {
          ipa = GetIpaDictionary(source, directory);
          return true;
        }
        catch { }

        ipa = default!;
        return false;
      }

      /// <summary>
      /// <para>Creates a <paramref name="dataTable"/> with text, hierarchy-level and index (within the array) for any successful matches of <paramref name="source"/> in an array of <paramref name="texts"/>.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <param name="dataTable"></param>
      /// <param name="texts"></param>
      /// <returns></returns>
      public static bool TryLocateMatchingCultures(System.Globalization.CultureInfo source, out System.Data.DataTable dataTable, params string[] texts)
      {
        System.ArgumentNullException.ThrowIfNull(source);

        dataTable = new System.Data.DataTable("CultureLocator");

        dataTable.Columns.Add("Text", typeof(string));
        dataTable.Columns.Add("Level", typeof(int));
        dataTable.Columns.Add("Index", typeof(int));

        dataTable.DefaultView.Sort = "Level ASC, Index ASC";

        var index = 0;

        foreach (var text in texts)
        {
          if (System.Globalization.CultureInfo.TryMatchInCultureHierarchy(source, text, out var hierarchy))
            dataTable.Rows.Add(text, hierarchy, index);

          index++;
        }

        return dataTable.Rows.Count > 0;
      }

      /// <summary>
      /// <para>Try to locate a <paramref name="fileInfo"/> in <paramref name="directory"/> that matches the <paramref name="source"/>.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <param name="fileInfo"></param>
      /// <param name="directory"></param>
      /// <returns></returns>
      public static bool TryLocateMatchingFiles(System.Globalization.CultureInfo source, out System.IO.FileInfo? fileInfo, string directory)
      {
        System.ArgumentNullException.ThrowIfNull(source);

        if (GetFileInfos(directory) is var fileInfos && TryLocateMatchingCultures(source, out var dt, fileInfos.Select(fi => fi.FullName).ToArray()))
        {
          fileInfo = fileInfos.First(fi => string.Equals(fi.FullName, dt.Rows[0][0]));
          return true;
        }

        fileInfo = default;
        return false;
      }

      /// <summary>
      /// <para>Tries to match a <paramref name="text"/> to a culture in the <paramref name="source"/> hierarchy. Also returns the <paramref name="hierarchyLevel"/> where a match was found, -1 if not found.</para>
      /// </summary>
      /// <param name="source">The culture hierarchy to compare against.</param>
      /// <param name="text">The name to match.</param>
      /// <param name="hierarchyLevel">How far up the chain the match was found.</param>
      /// <returns>Whether a match was found and also the 'distance' (<paramref name="hierarchyLevel"/>) from the <paramref name="source"/> the match was found (-1 if not found).</returns>
      public static bool TryMatchInCultureHierarchy(System.Globalization.CultureInfo source, string text, out int hierarchyLevel)
      {
        System.ArgumentNullException.ThrowIfNull(source);

        for (hierarchyLevel = 0; source != System.Globalization.CultureInfo.InvariantCulture; hierarchyLevel++)
        {
          var pattern = @"(?<=(^|[^\p{L}]))" + source.Name.Replace('-', '.') + @"(?=([^\p{L}]|$))";

          if (System.Text.RegularExpressions.Regex.IsMatch(text, pattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase))
            return true;

          source = source.Parent;
        }

        hierarchyLevel = -1;
        return false;
      }
    }

    extension(System.Globalization.CultureInfo source)
    {
      /// <summary>
      /// 
      /// 
      /// </summary>
      /// <param name="source"></param>
      /// <returns></returns>
      /// <exception cref="System.NotImplementedException"></exception>
      public System.ReadOnlySpan<char> GetConsonants()
      {
        source ??= System.Globalization.CultureInfo.CurrentCulture;

        return source.TwoLetterISOLanguageName switch
        {
          "en" => "bcdfghjklmnpqrstvwxyzBCDFGHJKLMNPQRSTVWXYZ",
          "se" => "bcdfghjklmnpqrstvwxzBCDFGHJKLMNPQRSTVWXZ",
          _ => throw new System.NotImplementedException(nameof(source))
        };
      }

      /// <summary>
      /// <para></para>
      /// </summary>
      /// <param name="source"></param>
      /// <param name="culture">If null, then <see cref="System.Globalization.CultureInfo.CurrentCulture"/></param>
      /// <returns></returns>
      public System.ReadOnlySpan<char> GetVowels()
      {
        source ??= System.Globalization.CultureInfo.CurrentCulture;

        return source.TwoLetterISOLanguageName switch
        {
          "en" => "aeiouyAEIOUY",
          "se" => "aeiouy'\u00E5\u00E4\u00F6AEIOUY\u00C5\u00C4\u00D6", // The additional characters are å, ä, ö and Å, Ä, Ö.
          _ => throw new System.NotImplementedException(nameof(source))
        };
      }

      /// <summary>
      /// <para>Load an IPA file based on the specified <see cref="System.Globalization.CultureInfo"/>.</para>
      /// <para></para>
      /// </summary>
      /// <param name="directory"></param>
      /// <returns></returns>
      /// <exception cref="System.InvalidOperationException"></exception>
      /// <exception cref="System.NullReferenceException"></exception>
      public string ReadIpaFile(string directory = ResourcesDirectoryIpa)
      {
        if (!System.Globalization.CultureInfo.TryLocateMatchingFiles(source, out var fileInfo, directory)) throw new System.InvalidOperationException($"No culture file found for {source}.");

        using var fileStream = fileInfo?.OpenRead() ?? throw new System.NullReferenceException(nameof(fileInfo));
        using var streamReader = new System.IO.StreamReader(fileStream, System.Text.Encoding.UTF8);

        return streamReader.ReadToEnd();
      }
    }
  }
}
