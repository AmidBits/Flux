namespace Flux.Numerics
{
	public class BellTriangleAugmented
		: INumberSequence<System.Numerics.BigInteger[]>
	{
		// INumberSequence
		public System.Collections.Generic.IEnumerable<System.Numerics.BigInteger[]> GetSequence()
		{
			var current = new System.Numerics.BigInteger[] { 1 };

			while (true)
			{
				yield return current;

				var previous = current;
				current = new System.Numerics.BigInteger[previous.Length + 1];
				current[0] = (current[1] = previous[previous.Length - 1]) - previous[0];
				for (var i = 2; i <= previous.Length; i++)
					current[i] = previous[i - 1] + current[i - 1];
			}
		}

		// IEnumerable
		public System.Collections.Generic.IEnumerator<System.Numerics.BigInteger[]> GetEnumerator()
				=> GetSequence().GetEnumerator();
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
			=> GetEnumerator();
	}
}