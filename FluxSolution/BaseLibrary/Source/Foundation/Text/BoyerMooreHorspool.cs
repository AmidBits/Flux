using System.Linq;

namespace Flux.Text
{
  public class BoyerMooreHorspool
  {
    /// <summary>Creates the amount of safely skippable</summary>
    /// <see href="https://en.wikipedia.org/wiki/Boyer%E2%80%93Moore%E2%80%93Horspool_algorithm"/>
    public static System.Collections.Generic.Dictionary<char, int> PreProcess(System.ReadOnlySpan<char> word, System.ReadOnlySpan<char> text)
    {
      var table = new System.Collections.Generic.Dictionary<char, int>();

      word.CreateMinMax(out var min, out var max);
      text.UpdateMinMax(ref min, ref max);

      for (var i = min; i <= max; i++)
        table.Add((char)i, word.Length);

      for (var i = 0; i < word.Length; i++)
        table[word[i]] = word.Length - 1 - i;

      return table;
    }

    public static bool Same(System.ReadOnlySpan<char> word1, System.ReadOnlySpan<char> word2, int length)
    {
      for (var i = length - 1; i >= 0; i--)
        if (word1[i] != word2[i])
          return false;

      return true;
    }

    public static int Search(System.ReadOnlySpan<char> word, System.ReadOnlySpan<char> text)
    {
      var skippable = PreProcess(word, text);

      var skip = 0;

      while (text.Length - skip >= word.Length)
      {
        if (Same(text.Slice(skip), word, word.Length))
          return skip;

        skip =skip+ skippable[text[skip + word.Length - 1]];
      }

      return -1;
    }
  }
}
