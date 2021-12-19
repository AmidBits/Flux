namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Searches a text for all indices of a substring. Returns an empty list if not found. Uses the specified equality comparer.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Knuth%E2%80%93Morris%E2%80%93Pratt_algorithm"/>
    public static System.Collections.Generic.List<int> KnuthMorrisPrattSearch<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> find, System.Collections.Generic.IEqualityComparer<T> equalityComparer)
    {
      var table = CreateTable(find);

      var indices = new System.Collections.Generic.List<int>();

      var ti = 0;
      var wi = 0;

      while (ti < source.Length)
      {
        if (equalityComparer.Equals(find[wi], source[ti]))
        {
          ti++;
          wi++;

          if (wi == find.Length)
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
    /// <summary>Searches a text for all indices of a substring. Returns an empty list if not found. Uses the default equality comparer.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Knuth%E2%80%93Morris%E2%80%93Pratt_algorithm"/>
    public static System.Collections.Generic.List<int> KnuthMorrisPrattSearch<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> find)
      => KnuthMorrisPrattSearch(source, find, System.Collections.Generic.EqualityComparer<T>.Default);
  }
}
