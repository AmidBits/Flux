//namespace Flux
//{
//  public static partial class Fx
//  {
//    public static System.Collections.Generic.IEnumerable<string> LoadCultureFile(this System.Globalization.CultureInfo source, string directory, System.Text.Encoding? encoding = null)
//    {
//      if (!source.TryLocateCultureFile(out var fileInfo, directory)) throw new System.InvalidOperationException($"Culture file not found for {source}.");

//      using var fileStream = fileInfo?.OpenRead() ?? throw new System.ArgumentNullException(nameof(fileInfo));
//      using var streamReader = new System.IO.StreamReader(fileStream, encoding ?? System.Text.Encoding.UTF8);

//      foreach (var line in streamReader.ReadLines(line => line.Length > 0, line => line))
//        yield return line;
//    }

//    /// <summary>
//    /// <para>Returns the lexicon (word list) for the <paramref name="source"/>, if available.</para>
//    /// <see href="https://github.com/open-dict-data/ipa-dict/tree/master/"/>
//    /// </summary>
//    public static bool TryLoadCultureFile(this System.Globalization.CultureInfo source, System.Func<string, string[]> itemArraySelector, out System.Data.DataTable dataTable, out System.IO.FileInfo? fileInfo, string directory, System.Text.Encoding? encoding = null)
//    {
//      if (source.TryLocateCultureFile(out fileInfo, directory))
//      {
//        using var fileStream = fileInfo?.OpenRead() ?? throw new System.ArgumentNullException(nameof(fileInfo));
//        using var streamReader = new System.IO.StreamReader(fileStream, encoding ?? System.Text.Encoding.UTF8);

//        dataTable = streamReader.ToDataTable(s => s.Length > 0, itemArraySelector, source.Name);

//        return true;
//      }

//      dataTable = default!;
//      fileInfo = default;
//      return false;
//    }
//  }
//}
