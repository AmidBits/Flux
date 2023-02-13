namespace Flux
{
  public static partial class ExtensionMethodsCultureInfo
  {
    /// <summary>Returns the IPA dictionary for the <paramref name="source"/>, if available.</summary>
    /// <see href="https://github.com/open-dict-data/ipa-dict/tree/master/"/>
    public static System.Collections.Generic.SortedDictionary<string, string> GetIpaDictionaryOf(this System.Globalization.CultureInfo source, System.Collections.Generic.IComparer<string>? comparer = null)
    {
      comparer ??= System.Collections.Generic.Comparer<string>.Default;

      using var s = new System.Uri(@$"file://\Resources\IpaDictionaries\{source.Name.Replace('-', '_')}.txt").GetStream();
      using var sr = new System.IO.StreamReader(s, System.Text.Encoding.UTF8);

      var dictionary = new System.Collections.Generic.SortedDictionary<string, string>(comparer);
      foreach (var a in sr.ReadLines(false).Select(line => line.Split('\t')).DistinctBy(a => a[0])) dictionary.Add(a[0].Trim(), a[1].Trim());
      return dictionary;
    }
  }
}
