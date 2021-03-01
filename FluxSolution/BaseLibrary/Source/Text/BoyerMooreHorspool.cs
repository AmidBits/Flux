namespace Flux
{
	public class BoyerMooreHorspool
	{
		public static System.Collections.Generic.Dictionary<char, int> PreProcess(System.ReadOnlySpan<char> pattern)
		{
			var table = new System.Collections.Generic.Dictionary<char, int>();

			for (var i = 0; i < pattern.Length; i++)
				if (!table.ContainsKey(pattern[i]))
					table.Add(pattern[i], pattern.Length);

			for (var i = 0; i < pattern.Length; i++)
				table[pattern[i]] = pattern.Length - 1 - i;

			return table;
		}
	}
}
