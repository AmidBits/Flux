namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Returns the IPA dictionary for the <paramref name="source"/>, if available. Uses the specified <paramref name="comparer"/>, or default if null.</para>
    /// <see href="https://github.com/open-dict-data/ipa-dict/tree/master/"/>
    /// </summary>
    public const string ResourcesDirectoryIpa = @"file://\Resources\Dictionaries\Ipa\";

    /// <summary>
    /// <para>Returns the lexicon (word list) for the <paramref name="source"/>, if available.</para>
    /// <see href="https://github.com/open-dict-data/ipa-dict/tree/master/"/>
    /// </summary>
    public const string ResourcesDirectoryLexicon = @"file://\Resources\Lexicons\";

    public static System.Collections.Generic.List<System.IO.FileInfo> GetFileInfos(string directory)
      => new System.Uri(directory).TryGetDirectoryInfo(out var directoryInfo)
      ? new System.IO.DirectoryInfo(directoryInfo.FullName).EnumerateFiles().ToList()
      : throw new System.InvalidOperationException("Not Found");
  }
}
