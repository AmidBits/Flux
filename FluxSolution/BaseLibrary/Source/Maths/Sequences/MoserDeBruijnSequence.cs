namespace Flux
{
	public static partial class Maths
	{
		// https://en.wikipedia.org/wiki/Moser%E2%80%93De_Bruijn_sequence
		// https://www.geeksforgeeks.org/moser-de-bruijn-sequence/
		public static System.Collections.Generic.IList<int> GenerateMoserDeBruijnSequence(int number)
		{
			if (number < 0) throw new System.ArgumentOutOfRangeException(nameof(number));

			var sequence = new System.Collections.Generic.List<int>();

			sequence.Add(0);

			if (number > 0)
				sequence.Add(1);

			for (var i = 2; i <= number; i++)
			{
				if (i % 2 == 0) // S(2 * n) = 4 * S(n)
					sequence.Add(4 * sequence[i / 2]);
				else // S(2 * n + 1) = 4 * S(n) + 1
					sequence.Add(4 * sequence[i / 2] + 1);
			}

			return sequence;
		}
	}
}
