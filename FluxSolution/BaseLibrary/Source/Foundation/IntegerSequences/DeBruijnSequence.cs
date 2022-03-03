using System.Linq;

namespace Flux.Numerics
{
  // https://www.rosettacode.org/wiki/De_Bruijn_sequences
  public sealed class DeBruijnSequence
    : ANumberSequenceable<int>
  {
    public int OrderN { get; set; }
    public int SizeK { get; set; }

    public DeBruijnSequence(int sizeK, int orderN)
    {
      OrderN = orderN;
      SizeK = sizeK;
    }

    /// <summary>Creates a new sequence with the code indices expanded.</summary>
    public System.Collections.Generic.IEnumerable<System.Collections.Generic.List<int>> GetExpandedSequence<T>()
      => GetNumberSequence().PartitionNgram(OrderN, (e, i) => e.ToList());

    // INumberSequence
    public override System.Collections.Generic.IEnumerable<int> GetNumberSequence()
      => GetDeBruijnSequence(SizeK, OrderN);

    #region Static methods
    /// <summary>Creates a new sequence with DeBruijn numbers, which are the indices in a k-sized alphabet of order_n size.</summary>
    public static System.Collections.Generic.List<int> GetDeBruijnSequence(int size_k, int order_n)
    {
      var sequence = new System.Collections.Generic.List<int>();

      var a = new int[size_k * order_n];

      DeBruijn(1, 1);

      sequence.AddRange(sequence.GetRange(0, order_n - 1));

      return sequence;

      void DeBruijn(int t, int p)
      {
        if (t > order_n)
        {
          if ((order_n % p) == 0)
            sequence.AddRange(new System.ArraySegment<int>(a, 1, p));
        }
        else
        {
          a[t] = a[t - p];
          DeBruijn(t + 1, p);
          var j = a[t - p] + 1;

          while (j < size_k)
          {
            a[t] = (byte)j;
            DeBruijn(t + 1, t);
            j++;
          }
        }
      }
    }

    /// <summary>Generates a new de Bruijn sequence of order_n on a size_k alphabet.</summary>
    public static System.Collections.Generic.IEnumerable<T> GetDeBruijnSequence<T>(int size_k, int order_n, System.Collections.Generic.IList<T> alphabet)
      => size_k <= alphabet.Count ? GetDeBruijnSequence(size_k, order_n).Select(i => alphabet[i]) : throw new System.ArgumentOutOfRangeException(nameof(size_k));
    /// <summary>Generates a new de Bruijn sequence of order_n on a size_k alphabet.</summary>
    public static System.Collections.Generic.IEnumerable<T> GetDeBruijnSequence<T>(int size_k, int order_n, params T[] alphabet)
      => GetDeBruijnSequence(size_k, order_n, (System.Collections.Generic.IList<T>)alphabet);
    #endregion Static methods
  }

  public sealed class DeBruijnSequence<T>
    : INumberSequenceable<T>
  {
    public int OrderN { get; }
    public int SizeK { get; }

    public System.Collections.Generic.IReadOnlyList<T> Alphabet { get; }

    public DeBruijnSequence(int sizeK, int orderN, System.Collections.Generic.IEnumerable<T> alphabet)
    {
      OrderN = orderN;
      SizeK = sizeK;

      Alphabet = System.Linq.Enumerable.ToList(alphabet);
    }

    /// <summary>Creates a new sequence with the code indices expanded.</summary>
    public System.Collections.Generic.IEnumerable<System.Collections.Generic.List<T>> GetExpandedSequence()
      => GetNumberSequence().PartitionNgram(OrderN, (e, i) => e.ToList());

    // INumberSequence
    public System.Collections.Generic.IEnumerable<T> GetNumberSequence()
      => DeBruijnSequence.GetDeBruijnSequence(SizeK, OrderN, (System.Collections.Generic.IList<T>)Alphabet);

    // IEnumerable
    public System.Collections.Generic.IEnumerator<T> GetEnumerator()
      => GetNumberSequence().GetEnumerator();
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
      => GetEnumerator();
  }

  //public static partial class Maths
  //{
  //  // https://en.wikipedia.org/wiki/De_Bruijn_sequence
  //  // https://www.rosettacode.org/wiki/De_Bruijn_sequences#C.23
  //  //public static System.Collections.Generic.IDictionary<int, int> GetDeBruijnCounts(System.Collections.Generic.List<byte> sequence, int k, int n)
  //  //{
  //  //	var elementCounts = new System.Collections.Generic.Dictionary<int, int>();

  //  //	for (var i = (int)System.Math.Pow(k, n) - 1; i >= 0; i--)
  //  //		elementCounts.Add(i, 0);

  //  //	foreach (var value in DecodeDeBruijnSequence(sequence, k, n, b => string.Join(string.Empty, b)))
  //  //		if (value.Length <= 4)
  //  //			elementCounts[int.Parse(value.Replace(",", string.Empty), System.Globalization.NumberStyles.Integer)]++;

  //  //	return elementCounts;
  //  //}

  //  //public static System.Collections.Generic.List<string> GenerateDeBruijnErrors(System.Collections.Generic.List<byte> sequence, int k, int n)
  //  //{
  //  //	var elementCounts = GetDeBruijnCounts(sequence, k, n);

  //  //	var elementErrors = new System.Collections.Generic.List<string>();

  //  //	foreach (var kvp in elementCounts)
  //  //		if (kvp.Value == 0)
  //  //			elementErrors.Add($"Element {string.Join(',', kvp.Key)} is missing.");
  //  //		else if (kvp.Value > 1)
  //  //			elementErrors.Add($"Element {string.Join(',', kvp.Key)} occurs {kvp.Value} times.");
  //  //	//for (int i = 0; i < elementCounts.Count; i++)
  //  //	//{
  //  //	//	if (elementCounts[i] == 0)
  //  //	//		elementErrors.Add($"Element {i,4} is missing.");
  //  //	//	else if (elementCounts[i] > 1)
  //  //	//		elementErrors.Add($"Element {i,4} occurs {elementCounts[i]} times");
  //  //	//}

  //  //	return elementErrors;
  //  //}

  //  //public static void GetDeBruijnElements(System.Collections.Generic.List<int> sequence, int k, int n)
  //  //{
  //  //	var sc = sequence.Count;
  //  //	var foundElements = new int[sc - (n - 1)];
  //  //	var errs = new System.Collections.Generic.List<string>();
  //  //	// Check all strings of 4 consecutive digits within 'db'
  //  //	// to see if all 10,000 combinations occur without duplication.
  //  //	for (int i = 0; i < sc - 3; i++)
  //  //	{
  //  //		var s = sequence.GetRange(i, 4).ToArray();
  //  //		//if (AllDigits(s))
  //  //		//{
  //  //		if (int.TryParse(string.Join(string.Empty, s), out int number))
  //  //			foundElements[number]++;
  //  //		//}
  //  //	}
  //  //	for (int i = 0; i < 10000; i++)
  //  //	{
  //  //		if (foundElements[i] == 0)
  //  //		{
  //  //			errs.Add(string.Format("    PIN number {0,4} missing", i));
  //  //		}
  //  //		else if (foundElements[i] > 1)
  //  //		{
  //  //			errs.Add(string.Format("    PIN number {0,4} occurs {1} times", i, foundElements[i]));
  //  //		}
  //  //	}
  //  //	var lerr = errs.Count;
  //  //	if (lerr == 0)
  //  //	{
  //  //		System.Console.WriteLine("  No errors found");
  //  //	}
  //  //	else
  //  //	{
  //  //		var pl = lerr == 1 ? "" : "s";
  //  //		System.Console.WriteLine("  {0} error{1} found:", lerr, pl);
  //  //		errs.ForEach(System.Console.WriteLine);
  //  //	}
  //  //}

  //  public static System.Collections.Generic.IEnumerable<TSymbol[]> ExpandDeBruijnSequence<TSymbol>(System.Collections.Generic.List<TSymbol> deBruijnSequence, int order_n)
  //    => deBruijnSequence.PartitionNgram(order_n, (e, i) => e.ToArray());

  //  /// <summary>Generates a new de Bruijn sequence of order_n on a size_k alphabet.</summary>
  //  public static System.Collections.Generic.IEnumerable<TSymbol> GetDeBruijnSequence<TSymbol>(int size_k, int order_n, params TSymbol[] alphabet)
  //  {
  //    var sequence = new System.Collections.Generic.List<byte>();

  //    var a = new byte[size_k * order_n];

  //    void DeBruijn(int t, int p)
  //    {
  //      if (t > order_n)
  //      {
  //        if ((order_n % p) == 0)
  //          sequence.AddRange(new System.ArraySegment<byte>(a, 1, p));
  //      }
  //      else
  //      {
  //        a[t] = a[t - p];
  //        DeBruijn(t + 1, p);
  //        var j = a[t - p] + 1;

  //        while (j < size_k)
  //        {
  //          a[t] = (byte)j;
  //          DeBruijn(t + 1, t);
  //          j++;
  //        }
  //      }
  //    }

  //    DeBruijn(1, 1);

  //    sequence.AddRange(sequence.GetRange(0, order_n - 1));

  //    return sequence.Select(i => alphabet[i]);
  //  }
  //  /// <summary>Generates a new de Bruijn sequence of order_n on a size_k alphabet. The alphabet is integers 0 through 9.</summary>
  //  public static System.Collections.Generic.IEnumerable<int> GetDeBruijnSequence(int size_k, int order_n)
  //    => GetDeBruijnSequence(size_k, order_n, System.Linq.Enumerable.Range(0, size_k).ToArray());

  //  /// <summary>A de Bruijn sequence of order n on a size-k alphabet A is a cyclic sequence in which every possible length-n string on A occurs exactly once as a substring (i.e., as a contiguous subsequence). Such a sequence is denoted by B(k, n) and has length kn, which is also the number of distinct strings of length n on A. Each of these distinct strings, when taken as a substring of B(k, n), must start at a different position, because substrings starting at the same position are not distinct. Therefore, B(k, n) must have at least kn symbols. And since B(k, n) has exactly kn symbols, De Bruijn sequences are optimally short with respect to the property of containing every string of length n at least once.</summary>
  //  /// <param name="n">The order of, or length of each sub-sequence.</param>
  //  /// <param name="k">Size of the alphabet.</param>
  //  /// <param name="alphabet">The alphabet.</param>
  //  // https://en.wikipedia.org/wiki/De_Bruijn_sequence
  //  // https://www.geeksforgeeks.org/de-bruijn-sequence-set-1/
  //  //public static System.Collections.Generic.IEnumerable<char> GetDeBruijnSequence(int n, params char[] alphabet)
  //  //{
  //  //	if (alphabet is null || alphabet.Length == 0) throw new System.ArgumentOutOfRangeException(nameof(alphabet));

  //  //	var k = alphabet.Length;

  //  //	var m_seen = new System.Collections.Generic.HashSet<string>();

  //  //	var m_edges = new System.Collections.Generic.List<int>();

  //  //	var startingNode = new string(alphabet[0], n - 1);

  //  //	ModifiedDfs(startingNode);

  //  //	var l = (int)System.Math.Pow(k, n); // Number of edges.

  //  //	for (int i = 0; i < l; i++)
  //  //		yield return alphabet[m_edges[i]];

  //  //	foreach (var c in startingNode)
  //  //		yield return c;

  //  //	/// <summary>Modified DFS in which no edge is traversed twice.</summary>
  //  //	void ModifiedDfs(string node)
  //  //	{
  //  //		for (var i = 0; i < k; i++)
  //  //		{
  //  //			var s = node + alphabet[i];

  //  //			if (!m_seen.Contains(s))
  //  //			{
  //  //				m_seen.Add(s);

  //  //				ModifiedDfs(s.Substring(1));

  //  //				m_edges.Add(i);
  //  //			}
  //  //		}
  //  //	}
  //  //}
  //}
}
