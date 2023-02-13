namespace Flux
{
  public static partial class ExtensionMethodsCultureInfo
  {
    /// <summary>Returns the IPA dictionary for the <paramref name="source"/>, if available.</summary>
    /// <see href="https://github.com/open-dict-data/ipa-dict/tree/master/"/>
    public static System.Collections.Generic.HashSet<string> GetWordlistOf(this System.Globalization.CultureInfo source, System.Collections.Generic.IEqualityComparer<string>? equalityComparer = null)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<string>.Default;

      using var s = new System.Uri(@$"file://\Resources\Wordlists\{source.Name}.txt").GetStream();
      using var sr = new System.IO.StreamReader(s, System.Text.Encoding.UTF8);

      var hashSet = new System.Collections.Generic.HashSet<string>(equalityComparer);
      foreach (var line in sr.ReadLines(false).Distinct()) hashSet.Add(line.Trim());
      return hashSet;
    }
  }
}
