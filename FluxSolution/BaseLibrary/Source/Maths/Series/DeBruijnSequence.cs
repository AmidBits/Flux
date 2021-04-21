using System.Linq;

namespace Flux
{
	public static partial class Maths
	{
		https://www.rosettacode.org/wiki/De_Bruijn_sequences#C.23
		public static void ValidateDeBruijn(System.Collections.Generic.List<int> sequence, int k, int n)
		{
			var le = sequence.Count;
			var found = new int[le - (n - 1)];
			var errs = new System.Collections.Generic.List<string>();
			// Check all strings of 4 consecutive digits within 'db'
			// to see if all 10,000 combinations occur without duplication.
			for (int i = 0; i < le - 3; i++)
			{
				var s = sequence.GetRange(i, 4).ToArray();
				//if (AllDigits(s))
				//{
				if (int.TryParse(string.Join(string.Empty, s), out int number))
					found[number]++;
				//}
			}
			for (int i = 0; i < 10000; i++)
			{
				if (found[i] == 0)
				{
					errs.Add(string.Format("    PIN number {0,4} missing", i));
				}
				else if (found[i] > 1)
				{
					errs.Add(string.Format("    PIN number {0,4} occurs {1} times", i, found[i]));
				}
			}
			var lerr = errs.Count;
			if (lerr == 0)
			{
				System.Console.WriteLine("  No errors found");
			}
			else
			{
				var pl = lerr == 1 ? "" : "s";
				System.Console.WriteLine("  {0} error{1} found:", lerr, pl);
				errs.ForEach(System.Console.WriteLine);
			}
		}
		public static System.Collections.Generic.List<int> GetDeBruijnSequence(int k, int n)
		{
			var sequence = new System.Collections.Generic.List<int>();

			var a = new int[k * n];

			void DeBruijn(int t, int p)
			{
				if (t > n)
				{
					if (n % p == 0)
					{
						sequence.AddRange(new System.ArraySegment<int>(a, 1, p));
					}
				}
				else
				{
					a[t] = a[t - p];
					DeBruijn(t + 1, p);
					var j = a[t - p] + 1;

					while (j < k)
					{
						a[t] = j;
						DeBruijn(t + 1, t);
						j++;
					}
				}
			}

			DeBruijn(1, 1);

			sequence.AddRange(sequence.GetRange(0, n - 1));

			return sequence;
		}

		/// <summary>A de Bruijn sequence of order n on a size-k alphabet A is a cyclic sequence in which every possible length-n string on A occurs exactly once as a substring (i.e., as a contiguous subsequence). Such a sequence is denoted by B(k, n) and has length kn, which is also the number of distinct strings of length n on A. Each of these distinct strings, when taken as a substring of B(k, n), must start at a different position, because substrings starting at the same position are not distinct. Therefore, B(k, n) must have at least kn symbols. And since B(k, n) has exactly kn symbols, De Bruijn sequences are optimally short with respect to the property of containing every string of length n at least once.</summary>
		/// <param name="n">The order of, or length of each sub-sequence.</param>
		/// <param name="k">Size of the alphabet.</param>
		/// <param name="alphabet">The alphabet.</param>
		// https://en.wikipedia.org/wiki/De_Bruijn_sequence
		// https://www.geeksforgeeks.org/de-bruijn-sequence-set-1/
		//public static System.Collections.Generic.IEnumerable<char> GetDeBruijnSequence(int n, params char[] alphabet)
		//{
		//	if (alphabet is null || alphabet.Length == 0) throw new System.ArgumentOutOfRangeException(nameof(alphabet));

		//	var k = alphabet.Length;

		//	var m_seen = new System.Collections.Generic.HashSet<string>();

		//	var m_edges = new System.Collections.Generic.List<int>();

		//	var startingNode = new string(alphabet[0], n - 1);

		//	ModifiedDfs(startingNode);

		//	var l = (int)System.Math.Pow(k, n); // Number of edges.

		//	for (int i = 0; i < l; i++)
		//		yield return alphabet[m_edges[i]];

		//	foreach (var c in startingNode)
		//		yield return c;

		//	/// <summary>Modified DFS in which no edge is traversed twice.</summary>
		//	void ModifiedDfs(string node)
		//	{
		//		for (var i = 0; i < k; i++)
		//		{
		//			var s = node + alphabet[i];

		//			if (!m_seen.Contains(s))
		//			{
		//				m_seen.Add(s);

		//				ModifiedDfs(s.Substring(1));

		//				m_edges.Add(i);
		//			}
		//		}
		//	}
		//}
	}
}
