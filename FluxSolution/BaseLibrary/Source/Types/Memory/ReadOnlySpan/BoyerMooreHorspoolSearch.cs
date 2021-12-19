namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Searches a text for the index of a substring. Returns -1 if not found. Uses the specified equality comparer.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Boyer%E2%80%93Moore%E2%80%93Horspool_algorithm"/>
    public static int BoyerMooreHorspoolSearch<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> find, System.Collections.Generic.IEqualityComparer<T> EqualityComparer)
      where T : notnull
    {
      var skippable = CreateTable(find, source);

      var skip = 0;

      var wordLength = find.Length;
      var textLength = source.Length;

      while (textLength - skip >= wordLength)
      {
        if (Same(source[skip..], find, wordLength))
          return skip;

        skip += skippable[source[skip + wordLength - 1]];
      }

      return -1;

      /// <summary>Creates the amount of safely skippable items.</summary>
      System.Collections.Generic.Dictionary<T, int> CreateTable(System.ReadOnlySpan<T> word, System.ReadOnlySpan<T> text)
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

      bool Same(System.ReadOnlySpan<T> word1, System.ReadOnlySpan<T> word2, int length)
      {
        for (var i = length - 1; i >= 0; i--)
          if (!EqualityComparer.Equals(word1[i], word2[i]))
            return false;

        return true;
      }

    }
    /// <summary>Searches a text for the index of a substring. Returns -1 if not found. Uses the default equality comparer.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Boyer%E2%80%93Moore%E2%80%93Horspool_algorithm"/>
    public static int BoyerMooreHorspoolSearch<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> find)
      where T : notnull
      => BoyerMooreHorspoolSearch(source, find, System.Collections.Generic.EqualityComparer<T>.Default);
  }
}
