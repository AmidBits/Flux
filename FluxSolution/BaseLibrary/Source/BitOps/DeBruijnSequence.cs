namespace Flux
{
  // https://en.wikipedia.org/wiki/De_Bruijn_sequence
  // https://www.geeksforgeeks.org/de-bruijn-sequence-set-1/
  public class DeBrujinSequence
  {
    private static System.Collections.Generic.HashSet<string> m_seen = new System.Collections.Generic.HashSet<string>();
    private static System.Collections.Generic.List<int> m_edges = new System.Collections.Generic.List<int>();

    /// <summary>Function to find a de Bruijn sequence of order n on k characters.</summary>
    /// <param name="n"></param>
    /// <param name="k"></param>
    /// <param name="A"></param>
    public System.Collections.Generic.IEnumerable<char> Get(int k, int n, params char[] A)
    {
      m_seen.Clear();
      m_edges.Clear();

      //var startingNode = Strings(n - 1, A[0]);
      var startingNode = new string(A[0], n - 1);

      ModifiedDfs(startingNode, k, A);

      string s = string.Empty;

      var l = (int)System.Math.Pow(k, n); // Number of edges.

      for (int i = 0; i < l; i++)
        yield return A[m_edges[i]];

      foreach (var c in startingNode)
        yield return c;
    }

    /// <summary>Modified DFS in which no edge is traversed twice.</summary>
    private void ModifiedDfs(string node, int k, char[] A)
    {
      for (var i = 0; i < k; i++)
      {
        var s = node + A[i];

        if (!m_seen.Contains(s))
        {
          m_seen.Add(s);

          ModifiedDfs(s.Substring(1), k, A);

          m_edges.Add(i);
        }
      }
    }

    private static string Strings(int n, char charAt)
    {
      var s = string.Empty;

      for (int i = 0; i < n; i++)
        s += charAt;

      return s;
    }
  }
}
