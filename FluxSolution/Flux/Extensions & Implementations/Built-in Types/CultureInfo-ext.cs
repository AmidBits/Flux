using System.Collections.Frozen;

namespace Flux
{
  public static partial class CultureInfoExtensions
  {
    #region Consonants & Vowels

    /// <summary>
    /// <para>Basic latin consonants with 21 letters <c>[bcdfghjklmnpqrstvwxyzBCDFGHJKLMNPQRSTVWXYZ]</c>, which includes <c>[yY]</c>.</para>
    /// </summary>
    private static readonly System.Collections.Frozen.FrozenSet<System.Text.Rune> m_basicLatinConsonantsWithY = "bcdfghjklmnpqrstvwxyzBCDFGHJKLMNPQRSTVWXYZ".EnumerateRunes().ToFrozenSet();

    /// <summary>
    /// <para>Basic latin consonants with 20 letters <c>[bcdfghjklmnpqrstvwxzBCDFGHJKLMNPQRSTVWXZ]</c>, which excludes <c>[yY]</c>.</para>
    /// </summary>
    private static readonly System.Collections.Frozen.FrozenSet<System.Text.Rune> m_basicLatinConsonantsWithoutY = "bcdfghjklmnpqrstvwxzBCDFGHJKLMNPQRSTVWXZ".EnumerateRunes().ToFrozenSet();

    /// <summary>
    /// <para>Basic latin vowels with 6 letters <c>[aeiouyAEIOUY]</c>.</para>
    /// </summary>
    private static readonly System.Collections.Frozen.FrozenSet<System.Text.Rune> m_basicLatinVowelsWithY = "aeiouyAEIOUY".EnumerateRunes().ToFrozenSet();

    /// <summary>
    /// <para>Basic latin vowels with 5 letters <c>[aeiouAEIOU]</c>.</para>
    /// </summary>
    private static readonly System.Collections.Frozen.FrozenSet<System.Text.Rune> m_basicLatinVowelsWithoutY = "aeiouAEIOU".EnumerateRunes().ToFrozenSet();

    /// <summary>
    /// <para>The German consonants has 20 basic latin letters <c>[bcdfghjklmnpqrstvwxzBCDFGHJKLMNPQRSTVWXZ]</c> and 1 additional letter <c>[ßẞ]</c>.</para>
    /// </summary>
    private static readonly System.Collections.Frozen.FrozenSet<System.Text.Rune> m_deConsonants = "bcdfghjklmnpqrstvwxzßBCDFGHJKLMNPQRSTVWXZẞ".EnumerateRunes().ToFrozenSet();

    /// <summary>
    /// <para>The German vowels has 6 basic latin letters <c>[aeiouyAEIOUY]</c> and 3 additional letters <c>[äöüÄÖÜ]</c>.</para>
    /// </summary>
    private static readonly System.Collections.Frozen.FrozenSet<System.Text.Rune> m_deVowels = "aeiouyäöüAEIOUYÄÖÜ".EnumerateRunes().ToFrozenSet();

    /// <summary>
    /// <para>The Italian consonants contains only 16 basic latin letters <c>[bcdfghlmnpqrstvzBCDFGHLMNPQRSTVZ]</c>, because it lacks 5 of the the basic latin letters <c>[jkwxyJKWXY]</c>.</para>
    /// </summary>
    private static readonly System.Collections.Frozen.FrozenSet<System.Text.Rune> m_itConsonants = "bcdfghlmnpqrstvzBCDFGHLMNPQRSTVZ".EnumerateRunes().ToFrozenSet();

    /// <summary>
    /// <para>The Swedish vowels has 6 basic latin letters <c>[aeiouyAEIOUY]</c> and 3 additional letters <c>[åäöÅÄÖ]</c>.</para>
    /// </summary>
    private static readonly System.Collections.Frozen.FrozenSet<System.Text.Rune> m_seVowels = "aeiouyåäöAEIOUYÅÄÖ".EnumerateRunes().ToFrozenSet();

    #endregion

    #region IPA helpers

    /// <summary>
    /// <para>Returns the IPA dictionary for the <paramref name="source"/>, if available. Uses the specified <paramref name="comparer"/>, or default if null.</para>
    /// <see href="https://github.com/open-dict-data/ipa-dict/tree/master/"/>
    /// </summary>
    public const string ResourcesDirectoryIpa = @"file://\Resources\Dictionaries\Ipa\";

    #endregion

    extension(System.Globalization.CultureInfo)
    {
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
      public static System.IO.FileInfo? LocateMatchingFiles(System.Globalization.CultureInfo source, string directory)
      {
        System.ArgumentNullException.ThrowIfNull(source);

        if (new System.Uri(directory).GetDirectoryInfo().GetFiles() is var fileInfos && TryLocateMatchingCultures(source, out var dt, [.. fileInfos.Select(fi => fi.FullName)]))
          return fileInfos.First(fi => string.Equals(fi.FullName, dt.Rows[0][0]));

        return default;
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

    extension(System.Globalization.CultureInfo? source)
    {
      #region Consonants & Vowels

      /// <summary>
      /// <para>Gets the consonants of the <see cref="System.Globalization.CultureInfo"/>.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <returns></returns>
      /// <exception cref="System.NotImplementedException"></exception>
      public System.Collections.Generic.IReadOnlySet<System.Text.Rune> GetConsonants()
      {
        source ??= System.Globalization.CultureInfo.CurrentCulture;

        return source.TwoLetterISOLanguageName switch
        {
          "iv" => m_basicLatinConsonantsWithY,
          "en" => m_basicLatinConsonantsWithY,
          "es" => m_basicLatinConsonantsWithY,
          "fr" => m_basicLatinConsonantsWithoutY,
          "de" => m_deConsonants,
          "it" => m_itConsonants,
          "se" => m_basicLatinConsonantsWithoutY,
          _ => throw new System.NotImplementedException(nameof(source))
        };
      }

      /// <summary>
      /// <para>Gets the vowels of the <see cref="System.Globalization.CultureInfo"/>.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <param name="culture">If null, then <see cref="System.Globalization.CultureInfo.CurrentCulture"/></param>
      /// <returns></returns>
      public System.Collections.Generic.IReadOnlySet<System.Text.Rune> GetVowels()
      {
        source ??= System.Globalization.CultureInfo.CurrentCulture;

        return source.TwoLetterISOLanguageName switch
        {
          "iv" => m_basicLatinVowelsWithY,
          "en" => m_basicLatinVowelsWithY,
          "es" => m_basicLatinVowelsWithoutY,
          "fr" => m_basicLatinVowelsWithY,
          "de" => m_deVowels,
          "it" => m_basicLatinVowelsWithoutY,
          "se" => m_seVowels,
          _ => throw new System.NotImplementedException(nameof(source))
        };
      }

      #endregion

      #region GetGrammaticalNumber

      /// <summary>
      /// <para>Gets the grammatical number of the numeric value.</para>
      /// </summary>
      /// <param name="value"></param>
      /// <returns></returns>
      public GrammaticalNumber GetGrammaticalNumber<TNumber>(TNumber value)
        where TNumber : System.Numerics.INumber<TNumber>
      {
        source ??= System.Globalization.CultureInfo.CurrentCulture;

        return source.TwoLetterISOLanguageName switch
        {
          // In English, only an integer equal to 1 (not 1.0) is singular, otherwise a number is considered plural.
          "en" or "iv" => (NumericTypeInfo<TNumber>.ImplementsIBinaryInteger && value == TNumber.One) ? GrammaticalNumber.One : GrammaticalNumber.Other,
          _ => throw new System.NotImplementedException(nameof(source))
        };
      }

      #endregion

      #region IPA file functions

      public System.IO.FileInfo? AssertIpaFile(string directory = ResourcesDirectoryIpa)
      {
        if (!IsIpaFile(source, out var fileInfo, directory)) throw new System.InvalidOperationException($"IPA file not found for culture {source}.");

        return fileInfo;
      }

      public bool IsIpaFile(out System.IO.FileInfo? fileInfo, string directory = ResourcesDirectoryIpa)
         => (fileInfo = System.Globalization.CultureInfo.LocateMatchingFiles(source, directory)) is not null && fileInfo.Exists;

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
        var fileInfo = AssertIpaFile(source, directory);

        using var fileStream = fileInfo?.OpenRead() ?? throw new System.NullReferenceException(nameof(fileInfo));
        using var streamReader = new System.IO.StreamReader(fileStream, System.Text.Encoding.UTF8);

        return streamReader.ReadToEnd();
      }

      public System.Data.DataTable GetIpaDataTable(string directory = ResourcesDirectoryIpa)
      {
        var fileInfo = AssertIpaFile(source, directory);

        using var fileStream = fileInfo?.OpenRead() ?? throw new System.NullReferenceException(nameof(fileInfo));
        using var streamReader = new System.IO.StreamReader(fileStream, System.Text.Encoding.UTF8);

        var dataTable = streamReader.ToDataTable(s => s.Length > 0, line => line.Split('\t'), source.Name, "Word", "Phonetic");
        dataTable.DefaultView.Sort = dataTable.Columns[0].ColumnName + " ASC";
        return dataTable;
      }

      public System.Collections.Generic.Dictionary<string, string> GetIpaDictionary(string directory = ResourcesDirectoryIpa)
      {
        var fileInfo = AssertIpaFile(source, directory);

        using var fileStream = fileInfo?.OpenRead() ?? throw new System.NullReferenceException(nameof(fileInfo));
        using var streamReader = new System.IO.StreamReader(fileStream, System.Text.Encoding.UTF8);

        return streamReader.ReadLines(s => s.Length > 0, s => s.Split('\t')).ToDictionary(a => a[0], a => a[1]);
      }

      #endregion
    }
  }
}
