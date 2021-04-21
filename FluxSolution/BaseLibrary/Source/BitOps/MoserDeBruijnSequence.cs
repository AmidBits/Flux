namespace Flux
{
  // https://en.wikipedia.org/wiki/Moser%E2%80%93De_Bruijn_sequence
  // https://www.geeksforgeeks.org/moser-de-bruijn-sequence/
  public class MoserDeBruijnSequence
    : System.Collections.Generic.IEnumerable<int>
  {
    /// <summary>Using dynamic programmin.</summary>
    private static int Generate(int n)
    {
      var sequence = new int[n + 1];

      sequence[0] = 0;

      if (n != 0)
        sequence[1] = 1;

      for (var i = 2; i <= n; i++)
      {
        if (i % 2 == 0) // S(2 * n) = 4 * S(n)
          sequence[i] = 4 * sequence[i / 2];
        else // S(2 * n + 1) = 4 * S(n) + 1
          sequence[i] = 4 * sequence[i / 2] + 1;
      }

      return sequence[n];
    }

    public static System.Collections.Generic.IEnumerable<int> Get(int n)
    {
      for (var i = 0; i < n; i++)
        yield return Generate(i);
    }

    public System.Collections.Generic.IEnumerator<int> GetEnumerator()
      => Get(int.MaxValue).GetEnumerator();
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
      => GetEnumerator();
  }
}
