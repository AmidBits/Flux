using System.Linq;

namespace Flux.Resources
{
  //public static class Formatted
  //{
  //  public static System.Collections.Generic.IEnumerable<string[]> GetCsv(System.Uri uri, Text.CsvOptions options, params string[] fieldNames)
  //  {
  //    yield return fieldNames;

  //    foreach (var csv in uri.GetStream().ReadCsv(options ?? new Text.CsvOptions()))
  //      yield return csv;
  //  }
  //  public static System.Collections.Generic.IEnumerable<string[]> GetCsv(System.Uri uri, Text.CsvOptions options)
  //    => uri.GetStream().ReadCsv(options ?? new Text.CsvOptions());
  //  public static System.Collections.Generic.IEnumerable<string[]> GetCsv(System.Uri uri)
  //    => uri.GetStream().ReadCsv(new Text.CsvOptions());
  //}

  public static class Presets
  {
    //#region Census CountiesAllData
    //public static System.Uri UriCensusCountiesAllDataLocal
    //  => new System.Uri(@"file://\Resources\Census\cc-est2019-alldata-04.csv");
    //public static System.Uri UriCensusCountiesAllDataSource
    //  => new System.Uri(@"https://www2.census.gov/programs-surveys/popest/datasets/2010-2019/counties/asrh/cc-est2019-alldata-04.csv");
    ///// <summary>Census all data.</summary>
    ///// <see cref="https://www.census.gov/content/census/en/data/tables/time-series/demo/popest/2010s-counties-detail.html"/>
    //// Download URL: https://www2.census.gov/programs-surveys/popest/datasets/2010-2019/counties/asrh/cc-est2019-alldata-04.csv
    //public static System.Collections.Generic.IEnumerable<string[]> GetCensusCountiesAllData(System.Uri uri)
    //  => uri.GetStream().ReadCsv(new Text.CsvOptions());
    //#endregion Census CountiesAllData

    //#region ProjectGutenberg SynonymsAndAntonymsSamuelFallows
    //public static System.Uri UriProjectGutenbergSynonymsAndAntonymsSamuelFallowsLocal
    //  => new System.Uri(@"file://\Resources\ProjectGutenberg\51155-0.txt");
    //public static System.Uri UriProjectGutenbergSynonymsAndAntonymsSamuelFallowsSource
    //  => new System.Uri(@"http://www.gutenberg.org/files/51155/51155-0.txt");
    ///// <summary>A Complete Dictionary of Synonyms and Antonyms by Samuel Fallows (Acdsasf).</summary>
    ///// <remarks>Returns keywords, synonyms and antonyms.</summary>
    ///// <see cref="http://www.gutenberg.org/ebooks/51155"/>
    //public static System.Collections.Generic.IEnumerable<string[]> GetProjectGutenbergSynonymsAndAntonymsSamuelFallows(System.Uri uri)
    //{
    //  yield return new string[] { "Keywords", "Synonyms", "Antonyms" };

    //  if (uri is null) throw new System.ArgumentNullException(nameof(uri));

    //  var reSection = new System.Text.RegularExpressions.Regex(@"(?<=(KEY:|SYN:|ANT:))\s", System.Text.RegularExpressions.RegexOptions.Compiled | System.Text.RegularExpressions.RegexOptions.IgnoreCase);

    //  foreach (var item in EnumerateArrays().Select(a => new string[] { string.Join(@",", a[0]), string.Join(@",", a[1]), string.Join(@",", a[2]) }))
    //    yield return item;

    //  System.Collections.Generic.IEnumerable<string[][]> EnumerateArrays()
    //  {
    //    var lines = new System.Text.StringBuilder();

    //    foreach (var line in uri.GetStream().ReadLines(System.Text.Encoding.UTF8))
    //    {
    //      if (line == @"=" || line.Length == 0)
    //      {
    //        if (lines.StartsWith(@"KEY:"))
    //        {
    //          var list = reSection.Replace(lines.ToString(), @",").ToLower(System.Globalization.CultureInfo.CurrentCulture).Split(new char[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim(' ', '.')).Where(s => s.Length > 0).ToList();

    //          var iKey = list.IndexOf(@"key:");
    //          var iSyn = list.IndexOf(@"syn:");
    //          var iAnt = list.IndexOf(@"ant:");

    //          var aKey = iKey > -1 ? list.GetRange(iKey + 1, (iSyn > -1 ? iSyn : iAnt > -1 ? iAnt : list.Count) - 1).ToArray() : System.Array.Empty<string>();
    //          var aSyn = iSyn > -1 ? list.GetRange(iSyn + 1, (iAnt > -1 ? iAnt - iSyn : list.Count - iSyn) - 1).ToArray() : System.Array.Empty<string>();
    //          var aAnt = iAnt > -1 ? list.GetRange(iAnt + 1, (list.Count - iAnt) - 1).ToArray() : System.Array.Empty<string>();

    //          if (aKey.Length > 0)
    //            yield return new string[][] { aKey, aSyn, aAnt };
    //        }

    //        lines.Clear();
    //      }
    //      else
    //      {
    //        if (lines.Length > 0)
    //          lines.Append(',');

    //        lines.Append(line);
    //      }
    //    }
    //  }
    //}
    //#endregion ProjectGutenberg SynonymsAndAntonymsSamuelFallows

    //#region ProjectGutenberg TableOfContents
    //public static System.Uri UriProjectGutenbergTableOfContentsLocal
    //  => new System.Uri(@"file://\Resources\ProjectGutenberg\GUTINDEX.ALL");
    //public static System.Uri UriProjectGutenbergTableOfContentsSource
    //  => new System.Uri(@"http://www.gutenberg.org/dirs/GUTINDEX.ALL");
    ///// <summary>Get all the book title/author and number available in the Gutenberg project (from gutenberg.com</summary>
    //public static System.Collections.Generic.IEnumerable<string[]> GetProjectGutenbergTableOfContents(System.Uri uri)
    //{
    //  yield return new string[] { @"Ebook", @"Number" };

    //  var reMatch = new System.Text.RegularExpressions.Regex(@"^[\p{L}\p{N}\p{Zs}\p{P}]+\s{2,}\d+$", System.Text.RegularExpressions.RegexOptions.Compiled);
    //  var reSplit = new System.Text.RegularExpressions.Regex(@"(?<=^.+)\s{2,}(?=\d+$)", System.Text.RegularExpressions.RegexOptions.Compiled);

    //  foreach (var item in uri.GetStream().ReadLines(System.Text.Encoding.UTF8).Where(s => s.Length == 78 && reMatch.IsMatch(s)).Select(s => reSplit.Split(s)))
    //    yield return item;
    //}
    //#endregion ProjectGutenberg TableOfContents

    //#region ProjectGutenberg TenThousandWonderfulThings
    //public static System.Uri UriProjectGutenbergTenThousandWonderfulThingsLocal
    //  => new System.Uri(@"file://\Resources\ProjectGutenberg\pg45849.txt");
    //public static System.Uri UriProjectGutenbergTenThousandWonderfulThingsSource
    //  => new System.Uri(@"http://www.gutenberg.org/ebooks/45849.txt.utf-8");
    //public static System.Collections.Generic.IEnumerable<string[]> GetProjectGutenbergTenThousandWonderfulThings(System.Uri uri)
    //{
    //  yield return new string[] { @"Title", @"Text" };

    //  var reTitle = new System.Text.RegularExpressions.Regex(@"^[\!\-\:\;\'\""\,\.\? A-Z0-9]+$", System.Text.RegularExpressions.RegexOptions.Compiled);

    //  using var e = uri.GetStream().ReadLines(System.Text.Encoding.UTF8, true).GetEnumerator();

    //  var entry = new System.Text.StringBuilder();

    //  while (e.MoveNext())
    //    if (e.Current.Equals(@"TEN THOUSAND WONDERFUL THINGS.", System.StringComparison.Ordinal)) // This is where we start enumerating lines from the text.
    //      break;

    //  while (e.MoveNext())
    //  {
    //    if (e.Current.Equals(@"  PRINTED BY WILLIAM CLOWES AND SONS, LIMITED, LONDON AND DECCLES.", System.StringComparison.Ordinal)) // This is where we stop enumerating lines from the text.
    //      break;

    //    if (entry.EndsWith("\r\n\r\n\r\n") && reTitle.IsMatch(e.Current))
    //    {
    //      var text = entry.ToString().Trim();
    //      var index = text.IndexOf("\r\n", System.StringComparison.Ordinal);

    //      yield return new string[] { text.Substring(0, index), text.Substring(index).Trim() };

    //      entry.Clear();
    //    }

    //    entry.AppendLine(e.Current);
    //  }
    //}
    //#endregion ProjectGutenberg TenThousandWonderfulThings

    //#region ProjectGutenberg WebstersUnabridgedDictionary
    //public static System.Uri UriProjectGutenbergWebstersUnabridgedDictionaryLocal
    //   => new System.Uri(@"file://\Resources\ProjectGutenberg\pg29765.txt");
    //public static System.Uri UriProjectGutenbergWebstersUnabridgedDictionarySource
    //  => new System.Uri(@"http://www.gutenberg.org/ebooks/29765.txt.utf-8");
    ///// <summary>Get the lines of Webster's Unabridged Dictionary (from gutenberg.com</summary>
    ///// <example>var words = Flux.Resources.GetGutenbergWebstersUnabridgedDictionary().Select(idr => idr.GetString(0)).Where(s => System.Text.RegularExpressions.Regex.IsMatch(s, @"^[A-Z]+$")).Select(s => s.ToLower()).ToArray();</example>
    ///// <see cref="http://www.gutenberg.org/ebooks/29765"/>
    //// Download URL: http://www.gutenberg.org/ebooks/29765.txt.utf-8
    //public static System.Collections.Generic.IEnumerable<string[]> GetProjectGutenbergWebstersUnabridgedDictionary(System.Uri uri)
    //{
    //  yield return new string[] { @"Word", @"Definition" };

    //  var m_reSplitWords = new System.Text.RegularExpressions.Regex(@"\s*;\s*", System.Text.RegularExpressions.RegexOptions.Compiled);
    //  var m_reWord = new System.Text.RegularExpressions.Regex(@"^[A-Z \-';]+$", System.Text.RegularExpressions.RegexOptions.Compiled);

    //  var word = new System.Text.StringBuilder();
    //  var definition = new System.Text.StringBuilder();

    //  foreach (var line in uri.GetStream().ReadLines(System.Text.Encoding.UTF8))
    //  {
    //    if (m_reWord.Match(line) is var match && match.Success)
    //    {
    //      if (word.Length > 0 && definition.Length == 0)
    //      {
    //        word.Append(' ');
    //        word.Append(match.Value.Trim());

    //        continue;
    //      }
    //      else if (word.Length > 0 && definition.Length > 0)
    //      {
    //        yield return new string[] { string.Join(System.Environment.NewLine, m_reSplitWords.Split(word.ToString())), definition.ToString() };

    //        word.Clear();
    //        definition.Clear();
    //      }

    //      word.Append(match.Value.Trim());
    //      if (line.Length > match.Value.Length) definition.Append(line.Substring(match.Value.Length));
    //    }
    //    else if (word.Length > 0)
    //    {
    //      if (line.Length > 0)
    //      {
    //        if (definition.Length > 0 && !definition.ToString().EndsWith(System.Environment.NewLine, System.StringComparison.Ordinal)) definition.Append(' ');
    //        definition.Append(line.Trim());
    //      }
    //      else definition.Append(System.Environment.NewLine);
    //    }
    //  }
    //}
    //#endregion ProjectGutenberg WebstersUnabridgedDictionary

    //#region Scowl TwoOfTwelveFull
    //public static System.Uri UriScowlTwoOfTwelveFullLocal
    //  => new System.Uri(@"file://\Resources\Scowl\2of12full.txt");
    //public static System.Uri UriScowlTwoOfTwelveFullSource
    //  => new System.Uri(@"https://raw.githubusercontent.com/en-wl/wordlist/master/alt12dicts/2of12full.txt");
    ///// <summary>The records from 2Of12Full word list.</summary>
    ///// <see cref="https://github.com/en-wl/wordlist"/>
    //// Download URL: https://github.com/en-wl/wordlist/raw/master/alt12dicts/2of12full.txt
    ///// <seealso cref="http://wordlist.aspell.net/"/>
    ///// <seealso cref="https://github.com/en-wl/wordlist/blob/master/"/>
    //public static System.Collections.Generic.IEnumerable<string[]> GetScowlTwoOfTwelveFull(System.Uri uri)
    //{
    //  yield return new string[] { "DictionaryCount", "NonVariant", "Variant", "NonAmerican", "SecondClass", "Word" };

    //  if (uri is null) throw new System.ArgumentNullException(nameof(uri));

    //  var reSplit = new System.Text.RegularExpressions.Regex(@"(?<=[\-0-9]+[:#&=]?)\s+", System.Text.RegularExpressions.RegexOptions.Compiled);

    //  foreach (var item in uri.GetStream().ReadLines(System.Text.Encoding.UTF8).Select(s => reSplit.Split(s.Trim())))
    //    yield return item;
    //}
    //#endregion Scowl TwoOfTwelveFull

    //#region Scrape ZipCodes
    //public static System.Uri UriScrapeZipCodesLocal
    //  => new System.Uri(@"file://\Resources\Scrape\free-zipcode-database.csv");
    //public static System.Uri UriScrapeZipCodesSource
    //  => new System.Uri(@"http://federalgovernmentzipcodes.us/free-zipcode-database.csv");
    ///// <summary>A free zip code file.</summary>
    ///// <see cref="http://federalgovernmentzipcodes.us/"/>
    //// Download URL: http://federalgovernmentzipcodes.us/free-zipcode-database.csv
    //public static System.Collections.Generic.IEnumerable<string[]> GetScrapeZipCodes(System.Uri uri)
    //  => uri.GetStream().ReadCsv(new Text.CsvOptions());
    //#endregion Scrape ZipCodes

    //#region UCD Blocks
    //public static System.Uri UriUcdBlocksLocal
    //  => new System.Uri(@"file://\Resources\Ucd\Blocks.txt");
    //public static System.Uri UriUcdBlocksSource
    //  => new System.Uri(@"https://www.unicode.org/Public/UCD/latest/ucd/Blocks.txt");
    ///// <summary>The Unicode block database.</summary>
    ///// <see cref="https://www.unicode.org/"/>
    ///// <seealso cref="https://unicode.org/Public/"/>
    ///// <seealso cref="https://www.unicode.org/Public/UCD/latest/ucd"/>
    //// Download URL: https://www.unicode.org/Public/UCD/latest/ucd/Blocks.txt
    //public static System.Collections.Generic.IEnumerable<string[]> GetUcdBlocks(System.Uri uri)
    //{
    //  yield return new string[] { "StartCode", "EndCode", "BlockName" };

    //  if (uri is null) throw new System.ArgumentNullException(nameof(uri));

    //  var m_reSplit = new System.Text.RegularExpressions.Regex(@"(\.\.|; )", System.Text.RegularExpressions.RegexOptions.ExplicitCapture);

    //  foreach (var item in uri.GetStream().ReadLines(System.Text.Encoding.UTF8).Where(line => line.Length > 0 && !line.StartsWith('#')).Select(line => m_reSplit.Split(line)))
    //    yield return item;
    //}
    //#endregion UCD Blocks

    //#region UCD UnicodeData
    //public static System.Uri UriUcdUnicodeDataLocal
    //  => new System.Uri(@"file://\Resources\Ucd\UnicodeData.txt");
    //public static System.Uri UriUcdUnicodeDataSource
    //  => new System.Uri(@"https://www.unicode.org/Public/UCD/latest/ucd/UnicodeData.txt");
    ///// <summary>The Unicode character database.</summary>
    ///// <see cref="https://www.unicode.org/"/>
    ///// <seealso cref="https://www.unicode.org/Public/UCD/latest/ucd"/>
    ///// <seealso cref="https://unicode.org/Public/"/>
    //// Download URL: https://www.unicode.org/Public/UCD/latest/ucd/UnicodeData.txt
    //public static System.Collections.Generic.IEnumerable<string[]> GetUcdUnicodeData(System.Uri uri)
    //  => uri.GetStream().ReadCsv(new Text.CsvOptions() { FieldSeparator = ';' });
    ////{
    ////  yield return new string[] { "CodePoint", "Name", "GeneralCategory", "CanonicalCombiningClass", "BidiClass", "DecompositionTypeMapping", "NumericType6", "NumericType7", "NumericType8", "BidiMirrored", "Unicode1Name", "IsoComment", "SimpleUppercaseMapping", "SimpleLowercaseMapping", "SimpleTitlecaseMapping" };

    ////  if (uri is null) throw new System.ArgumentNullException(nameof(uri));

    ////  foreach (var item in uri.GetStream().ReadLines(System.Text.Encoding.UTF8).Select(s => s.Split(';')))
    ////    yield return item;
    ////}
    //#endregion UCD UnicodeData

    //#region W3C NamedCharacterReferences
    //public static System.Uri UriW3cNamedCharacterReferencesLocal
    //  => new System.Uri(@"file://\Resources\W3c\NamedCharacterReferences.json");
    //public static System.Uri UriW3cNamedCharacterReferencesSource
    //  => new System.Uri(@"https://html.spec.whatwg.org/entities.json");
    ///// <summary>The character reference names that are supported by HTML, and the code points to which they refer.</summary>
    //public static System.Collections.Generic.IEnumerable<string[]> GetW3cNamedCharacterReferences(System.Uri uri)
    //{
    //  yield return new string[] { "Name", "CodePoints", "Characters", "CharactersAsString" };

    //  if (uri is null) throw new System.ArgumentNullException(nameof(uri));

    //  var m_reMatchUnicodeNotation = new System.Text.RegularExpressions.Regex(@"\\u([0-9a-fA-F]{4})|\\U([0-9a-fA-F]{8})", System.Text.RegularExpressions.RegexOptions.Compiled);

    //  using var jd = System.Text.Json.JsonDocument.Parse(uri.GetStream().ReadAllText(System.Text.Encoding.UTF8));

    //  foreach (var jp in jd.RootElement.EnumerateObject())
    //  {
    //    var codepoints = string.Join(@",", jp.Value.GetProperty(@"codepoints").EnumerateArray().Select(e => e.GetInt32()));
    //    var characters = jp.Value.GetProperty(@"characters").GetRawText().Trim().Replace(@"""", string.Empty, System.StringComparison.Ordinal);
    //    var charactersAsString = m_reMatchUnicodeNotation.Replace(characters, match => ((char)int.Parse(match.Value.Substring(2), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.CurrentCulture)).ToString());

    //    yield return new string[] { jp.Name, codepoints, characters, charactersAsString };
    //  }
    //}
    //#endregion W3C NamedCharacterReferences
  }

  /// <summary></summary>
  //public abstract class ResourceFactory
  //{
  //  public abstract System.Collections.Generic.IList<string> FieldNames { get; }
  //  public abstract System.Collections.Generic.IList<System.Type> FieldTypes { get; }

  //  public static System.Collections.Generic.IEnumerable<object[]> TransformTypes<T>(System.Collections.Generic.IEnumerable<T[]> source, System.Func<T, int, object> resultSelector)
  //  {
  //    if (source is null) throw new System.ArgumentNullException(nameof(source));
  //    if (resultSelector is null) throw new System.ArgumentNullException(nameof(resultSelector));

  //    foreach (var sa in source)
  //    {
  //      var ta = new object[sa.Length];
  //      for (var index = 0; index < sa.Length; index++)
  //        ta[index] = resultSelector(sa[index], index);
  //      yield return ta;
  //    }
  //  }

  //  public string GetName(int index)
  //    => index >= 0 && index < FieldNames.Count ? FieldNames[index] : $"Column_{index + 1}";
  //  public System.Type GetType(int index)
  //    => index >= 0 && index < FieldTypes.Count ? FieldTypes[index] : typeof(object);

  //  /// <summary>This is an abstract method for basic string data enumeration. This is considered the 'first' layer of data enumeration.</summary>
  //  public abstract System.Collections.Generic.IEnumerable<string[]> GetStrings(System.Uri uri);

  //  /// <summary>This is a virtual method for basic object data enumeration. By default the data from the method GetStrings() is converted using the method ConvertStringToObject(). This can be considered the 'second' layer of data enumeration. Override in derived class for complete control, e.g. make this the 'first' layer (skipping the GetStrings() altogether.</summary>
  //  public virtual System.Collections.Generic.IEnumerable<object[]> GetObjects(System.Uri uri)
  //  => TransformTypes(GetStrings(uri).Skip(1), (value, index) =>
  //  {
  //    if (GetType(index) is var type && type != typeof(string)) // Are we converting?
  //      return string.IsNullOrEmpty(value) ? Reflection.Helper.GetDefaultValue(type) ?? System.DBNull.Value : Convert.ChangeType(value, null, type); // Either convert the value to the specified field type, or if the value is empty (or externally null) then return the default for the specified type.

  //    return value; // Either we have no field type, or the field type is a string, so we simply return the value, as is.
  //  });

  //  /// <summary>Returns data as an IDataReader, by using GetObjects (for better type matching). An IDataReader can be passed to a SqlBulkCopy for import to SQL Server.</summary>
  //  public System.Data.IDataReader GetDataReader(System.Uri uri)
  //    => new Data.EnumerableTabularDataReader(GetObjects(uri), FieldNames, FieldTypes);
  //}
}

/*
 * Example (using in-assembly functionality):
 
#region Local URIs
System.Console.WriteLine(nameof(Flux.Resources.Census.CountiesAllData));
foreach (var strings in new Flux.Resources.Census.CountiesAllData().GetStrings(Flux.Resources.Census.CountiesAllData.LocalUri).Take(1))
  System.Console.WriteLine(string.Join('|', strings));

System.Console.WriteLine(nameof(Flux.Resources.ProjectGutenberg.SynonymsAndAntonymsSamuelFallows));
foreach (var strings in new Flux.Resources.ProjectGutenberg.SynonymsAndAntonymsSamuelFallows().GetStrings(Flux.Resources.ProjectGutenberg.SynonymsAndAntonymsSamuelFallows.LocalUri).Take(1))
  System.Console.WriteLine(string.Join('|', strings));
System.Console.WriteLine(nameof(Flux.Resources.ProjectGutenberg.TableOfContents));
foreach (var strings in new Flux.Resources.ProjectGutenberg.TableOfContents().GetStrings(Flux.Resources.ProjectGutenberg.TableOfContents.LocalUri).Take(1))
  System.Console.WriteLine(string.Join('|', strings));
System.Console.WriteLine(nameof(Flux.Resources.ProjectGutenberg.TenThousandWonderfulThings));
foreach (var strings in new Flux.Resources.ProjectGutenberg.TenThousandWonderfulThings().GetStrings(Flux.Resources.ProjectGutenberg.TenThousandWonderfulThings.LocalUri).Take(1))
  System.Console.WriteLine(string.Join('|', strings));
System.Console.WriteLine(nameof(Flux.Resources.ProjectGutenberg.WebstersUnabridgedDictionary));
foreach (var strings in new Flux.Resources.ProjectGutenberg.WebstersUnabridgedDictionary().GetStrings(Flux.Resources.ProjectGutenberg.WebstersUnabridgedDictionary.LocalUri).Take(1))
  System.Console.WriteLine(string.Join('|', strings));

System.Console.WriteLine(nameof(Flux.Resources.Scowl.TwoOfTwelveFull));
foreach (var strings in new Flux.Resources.Scowl.TwoOfTwelveFull().GetStrings(Flux.Resources.Scowl.TwoOfTwelveFull.LocalUri).Take(1))
  System.Console.WriteLine(string.Join('|', strings));

System.Console.WriteLine(nameof(Flux.Resources.Scrape.ZipCodes));
foreach (var strings in new Flux.Resources.Scrape.ZipCodes().GetStrings(Flux.Resources.Scrape.ZipCodes.LocalUri).Take(1))
  System.Console.WriteLine(string.Join('|', strings));

System.Console.WriteLine(nameof(Flux.Resources.Ucd.Blocks));
foreach (var strings in new Flux.Resources.Ucd.Blocks().GetStrings(Flux.Resources.Ucd.Blocks.LocalUri).Take(1))
  System.Console.WriteLine(string.Join('|', strings));
System.Console.WriteLine(nameof(Flux.Resources.Ucd.UnicodeData));
foreach (var strings in new Flux.Resources.Ucd.UnicodeData().GetStrings(Flux.Resources.Ucd.UnicodeData.LocalUri).Take(1))
  System.Console.WriteLine(string.Join('|', strings));

System.Console.WriteLine(nameof(Flux.Resources.W3c.NamedCharacterReferences));
foreach (var strings in new Flux.Resources.W3c.NamedCharacterReferences().GetStrings(Flux.Resources.W3c.NamedCharacterReferences.LocalUri).Take(1))
  System.Console.WriteLine(string.Join('|', strings));
#endregion

#region Source URIs
System.Console.WriteLine(nameof(Flux.Resources.Census.CountiesAllData));
foreach (var strings in new Flux.Resources.Census.CountiesAllData().GetStrings(Flux.Resources.Census.CountiesAllData.SourceUri).Take(1))
  System.Console.WriteLine(string.Join('|', strings));

System.Console.WriteLine(nameof(Flux.Resources.ProjectGutenberg.SynonymsAndAntonymsSamuelFallows));
foreach (var strings in new Flux.Resources.ProjectGutenberg.SynonymsAndAntonymsSamuelFallows().GetStrings(Flux.Resources.ProjectGutenberg.SynonymsAndAntonymsSamuelFallows.SourceUri).Take(1))
  System.Console.WriteLine(string.Join('|', strings));
System.Console.WriteLine(nameof(Flux.Resources.ProjectGutenberg.TableOfContents));
foreach (var strings in new Flux.Resources.ProjectGutenberg.TableOfContents().GetStrings(Flux.Resources.ProjectGutenberg.TableOfContents.SourceUri).Take(1))
  System.Console.WriteLine(string.Join('|', strings));
System.Console.WriteLine(nameof(Flux.Resources.ProjectGutenberg.TenThousandWonderfulThings));
foreach (var strings in new Flux.Resources.ProjectGutenberg.TenThousandWonderfulThings().GetStrings(Flux.Resources.ProjectGutenberg.TenThousandWonderfulThings.SourceUri).Take(1))
  System.Console.WriteLine(string.Join('|', strings));
System.Console.WriteLine(nameof(Flux.Resources.ProjectGutenberg.WebstersUnabridgedDictionary));
foreach (var strings in new Flux.Resources.ProjectGutenberg.WebstersUnabridgedDictionary().GetStrings(Flux.Resources.ProjectGutenberg.WebstersUnabridgedDictionary.SourceUri).Take(1))
  System.Console.WriteLine(string.Join('|', strings));

System.Console.WriteLine(nameof(Flux.Resources.Scowl.TwoOfTwelveFull));
foreach (var strings in new Flux.Resources.Scowl.TwoOfTwelveFull().GetStrings(Flux.Resources.Scowl.TwoOfTwelveFull.SourceUri).Take(1))
  System.Console.WriteLine(string.Join('|', strings));

System.Console.WriteLine(nameof(Flux.Resources.Scrape.ZipCodes));
foreach (var strings in new Flux.Resources.Scrape.ZipCodes().GetStrings(Flux.Resources.Scrape.ZipCodes.SourceUri).Take(1))
  System.Console.WriteLine(string.Join('|', strings));

System.Console.WriteLine(nameof(Flux.Resources.Ucd.Blocks));
foreach (var strings in new Flux.Resources.Ucd.Blocks().GetStrings(Flux.Resources.Ucd.Blocks.SourceUri).Take(1))
  System.Console.WriteLine(string.Join('|', strings));
System.Console.WriteLine(nameof(Flux.Resources.Ucd.UnicodeData));
foreach (var strings in new Flux.Resources.Ucd.UnicodeData().GetStrings(Flux.Resources.Ucd.UnicodeData.SourceUri).Take(1))
  System.Console.WriteLine(string.Join('|', strings));

System.Console.WriteLine(nameof(Flux.Resources.W3c.NamedCharacterReferences));
foreach (var strings in new Flux.Resources.W3c.NamedCharacterReferences().GetStrings(Flux.Resources.W3c.NamedCharacterReferences.SourceUri).Take(1))
  System.Console.WriteLine(string.Join('|', strings));
#endregion
*/
