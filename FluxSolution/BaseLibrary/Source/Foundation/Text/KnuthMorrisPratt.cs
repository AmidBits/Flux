namespace Flux.Text
{
  public class KnuthMorrisPratt
  {
    /// <summary>Creates the amount of safely skippable</summary>
    /// <see href="https://en.wikipedia.org/wiki/Knuth%E2%80%93Morris%E2%80%93Pratt_algorithm"/>
    public static System.Collections.Generic.Dictionary<int, int> Table(System.ReadOnlySpan<char> word)
    {
      var table = new System.Collections.Generic.Dictionary<int, int>();

      table[0] = -1;

      var pi = 1; // Position index.
      var ci = 0; // Current candidate index.

      while (pi < word.Length)
      {
        if (word[pi] == word[ci])
        {
          table[pi] = table[ci];
        }
        else
        {
          table[pi] = ci;

          while (ci >= 0 && word[pi] != word[ci])
            ci = table[ci];
        }

        pi++;
        ci++;
      }

      table[pi] = ci;

      return table;
    }

    public static System.Collections.Generic.List<int> Search(System.ReadOnlySpan<char> word, System.ReadOnlySpan<char> text)
    {
      var table = Table(word);

      var indices = new System.Collections.Generic.List<int>();

      var ti = 0;
      var wi = 0;

      while (ti < text.Length)
      {
        if (word[wi] == text[ti])
        {
          ti++;
          wi++;

          if (wi == word.Length)
          {
            indices.Add(ti - wi);

            wi = table[wi];
          }
        }
        else
        {
          wi = table[wi];

          if (wi < 0)
          {
            ti++;
            wi++;
          }
        }
      }

      return indices;
    }
  }
}
