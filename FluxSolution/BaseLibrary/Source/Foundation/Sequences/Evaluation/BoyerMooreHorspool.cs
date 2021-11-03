namespace Flux.Metrical
{
  /// <summary>Searches a text for the index of a substring. Returns -1 if not found.</summary>
  /// <see href="https://en.wikipedia.org/wiki/Boyer%E2%80%93Moore%E2%80%93Horspool_algorithm"/>
  public class BoyerMooreHorspool<T>
    : AMetrical<T>
    where T : notnull
  {
    public BoyerMooreHorspool(System.Collections.Generic.IEqualityComparer<T> equalityComparer)
      : base(equalityComparer)
    { }
    public BoyerMooreHorspool()
      : base()
    { }

    /// <summary>Creates the amount of safely skippable items.</summary>
    public System.Collections.Generic.Dictionary<T, int> CreateTable(System.ReadOnlySpan<T> word, System.ReadOnlySpan<T> text)
    {
      var table = new System.Collections.Generic.Dictionary<T, int>(); // The alphabet.

      var wordLength = word.Length;
      var textLength = text.Length;

      for (var index = System.Math.Max(wordLength, textLength) - 1; index >= 0; index--)
      {
        if (index < wordLength && word[index] is var wc && !table.ContainsKey(wc)) // Add to alphabet from word (needle) characters, if it is not already in the table.
          table.Add(wc, wordLength);
        if (index < textLength && text[index] is var tc && !table.ContainsKey(tc)) // Add to alphabet from text (haystack) characters, if it is not already in the table.
          table.Add(tc, wordLength);
      }

      for (var i = 0; i < wordLength; i++)
        table[word[i]] = wordLength - 1 - i;

      return table;
    }

    public bool Same(System.ReadOnlySpan<T> word1, System.ReadOnlySpan<T> word2, int length)
    {
      for (var i = length - 1; i >= 0; i--)
        if (!EqualityComparer.Equals(word1[i], word2[i]))
          return false;

      return true;
    }

    public int Search(System.ReadOnlySpan<T> word, System.ReadOnlySpan<T> text)
    {
      var skippable = CreateTable(word, text);

      var skip = 0;

      var wordLength = word.Length;
      var textLength = text.Length;

      while (textLength - skip >= wordLength)
      {
        if (Same(text.Slice(skip), word, wordLength))
          return skip;

        skip += skippable[text[skip + wordLength - 1]];
      }

      return -1;
    }
  }
}
