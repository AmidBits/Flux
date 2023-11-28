namespace Flux
{
  public static partial class ExtensionMethodsCultureInfo
  {
    /// <summary>Returns the IPA dictionary for the <paramref name="source"/>, if available. Uses the specified <paramref name="comparer"/>, or default if null.</summary>
    /// <see href="https://github.com/open-dict-data/ipa-dict/tree/master/"/>
    public static System.Collections.Generic.SortedDictionary<string, string> GetIpaDictionaryOf(this System.Globalization.CultureInfo source, out System.IO.FileSystemInfo fsi, System.Collections.Generic.IComparer<string>? comparer = null)
    {
      const string filePath = @"file://\Resources\Dictionaries\Ipa\{0}.txt";

      comparer ??= System.Collections.Generic.Comparer<string>.Default;

      var dictionary = new System.Collections.Generic.SortedDictionary<string, string>(comparer);

      if (new System.Uri(string.Format(filePath, source.Name)).TryGetFileSystemInfo(out fsi, source.Name.IndexOf('-') is var index && index > -1 ? new System.Uri[] { new(string.Format(filePath, source.Name[..source.Name.IndexOf('-')])) } : System.Array.Empty<System.Uri>()))
      {
        using var s = new System.Uri(fsi.FullName).GetStream();
        using var sr = new System.IO.StreamReader(s, System.Text.Encoding.UTF8);

        foreach (var a in sr.ReadLines(false).Select(line => line.Split('\t')))
          dictionary.TryAdd(a[0].Trim(), a[1].Trim());
      }

      return dictionary;
    }
  }
}
