namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Returns the lexicon (word list) for the <paramref name="source"/>, if available. Uses the specified <paramref name="equalityComparer"/>, or default if null.</summary>
    /// <see href="https://github.com/open-dict-data/ipa-dict/tree/master/"/>
    public static System.Collections.Generic.HashSet<string> GetLexiconOf(this System.Globalization.CultureInfo source, out System.IO.FileSystemInfo fsi, System.Collections.Generic.IEqualityComparer<string>? equalityComparer = null)
    {
      const string filePath = @"file://\Resources\Lexicons\{0}.txt";

      source ??= System.Globalization.CultureInfo.CurrentCulture;
      equalityComparer ??= System.Collections.Generic.EqualityComparer<string>.Default;

      var hashSet = new System.Collections.Generic.HashSet<string>(equalityComparer);

      if (new System.Uri(string.Format(filePath, source.Name)).TryGetFileSystemInfo(out fsi, source.Name.IndexOf('-') is var index && index > -1 ? new System.Uri[] { new(string.Format(filePath, source.Name[..source.Name.IndexOf('-')])) } : System.Array.Empty<System.Uri>()))
      {
        using var s = new System.Uri(fsi.FullName).GetStream();
        using var sr = new System.IO.StreamReader(s, System.Text.Encoding.UTF8);

        foreach (var line in sr.ReadLines(false))
          hashSet.Add(line.Trim());
      }

      return hashSet;
    }
  }
}
