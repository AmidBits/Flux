namespace Flux.Numerics
{
	public class BellTriangleAugmented
		: ASequencedNumbers<System.Numerics.BigInteger[]>
	{
		// INumberSequence
		public override System.Collections.Generic.IEnumerable<System.Numerics.BigInteger[]> GetNumberSequence()
		{
			var current = new System.Numerics.BigInteger[] { 1 };

			while (true)
			{
				yield return current;

				var previous = current;
				current = new System.Numerics.BigInteger[previous.Length + 1];
				current[0] = (current[1] = previous[^1]) - previous[0];
				for (var i = 2; i <= previous.Length; i++)
					current[i] = previous[i - 1] + current[i - 1];
			}
		}
	}
}
