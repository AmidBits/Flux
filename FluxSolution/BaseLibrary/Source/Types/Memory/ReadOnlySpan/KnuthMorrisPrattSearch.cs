namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Searches a text for all indices of a substring. Returns an empty list if not found.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Knuth%E2%80%93Morris%E2%80%93Pratt_algorithm"/>
    public static System.Collections.Generic.List<int> KnuthMorrisPrattSearch<T>(this System.ReadOnlySpan<T> text, System.ReadOnlySpan<T> word, System.Collections.Generic.IEqualityComparer<T> equalityComparer)
    {
      var table = CreateTable(word);

      var indices = new System.Collections.Generic.List<int>();

      var ti = 0;
      var wi = 0;

      while (ti < text.Length)
      {
        if (equalityComparer.Equals(word[wi], text[ti]))
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

      /// <summary>Creates the amount of safely skippable</summary>
      System.Collections.Generic.Dictionary<int, int> CreateTable(System.ReadOnlySpan<T> word)
      {
        var table = new System.Collections.Generic.Dictionary<int, int>
        {
          [0] = -1
        };

        var pi = 1; // Position index.
        var ci = 0; // Current candidate index.

        while (pi < word.Length)
        {
          if (equalityComparer.Equals(word[pi], word[ci]))
          {
            table[pi] = table[ci];
          }
          else
          {
            table[pi] = ci;

            while (ci >= 0 && !equalityComparer.Equals(word[pi], word[ci]))
              ci = table[ci];
          }

          pi++;
          ci++;
        }

        table[pi] = ci;

        return table;
      }
    }
  }
}
