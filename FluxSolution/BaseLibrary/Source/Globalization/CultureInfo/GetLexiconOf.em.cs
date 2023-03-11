using System.Linq;

namespace Flux
{
  public static partial class ExtensionMethodsCultureInfo
  {
    /// <summary>Returns the IPA dictionary for the <paramref name="source"/>, if available.</summary>
    /// <see href="https://github.com/open-dict-data/ipa-dict/tree/master/"/>
    public static System.Collections.Generic.HashSet<string> GetLexiconOf(this System.Globalization.CultureInfo source, System.Collections.Generic.IEqualityComparer<string>? equalityComparer = null)
    {
      const string filePath = @"file://\Resources\Lexicons\{0}.txt";

      equalityComparer ??= System.Collections.Generic.EqualityComparer<string>.Default;

      if (!new System.Uri(string.Format(filePath, source.Name)).TryGetFileSystemInfo(out var fsi, new System.Uri(string.Format(filePath, source.Name[..source.Name.IndexOf('-')]))))
        throw new System.NotImplementedException(nameof(source));

      using var s = new System.Uri(fsi.FullName).GetStream();
      using var sr = new System.IO.StreamReader(s, System.Text.Encoding.UTF8);

      var hashSet = new System.Collections.Generic.HashSet<string>(equalityComparer);
      foreach (var line in sr.ReadLines(false).Distinct())
        hashSet.Add(line.Trim());
      return hashSet;
    }
  }
}
