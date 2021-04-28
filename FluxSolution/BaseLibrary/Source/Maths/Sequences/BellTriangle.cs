namespace Flux
{
	public class BellTriangle
		: ISequenceInfinite<System.Numerics.BigInteger[]>
	{
		public System.Collections.Generic.IEnumerator<System.Numerics.BigInteger[]> GetEnumerator()
				=> GetSequence().GetEnumerator();
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
			=> GetEnumerator();

		public System.Collections.Generic.IEnumerable<System.Numerics.BigInteger[]> GetSequence()
		{
			var current = new System.Numerics.BigInteger[1] { 1 };

			while (true)
			{
				yield return current;

				var previous = current;
				current = new System.Numerics.BigInteger[previous.Length + 1];
				current[0] = previous[previous.Length - 1];
				for (var i = 1; i <= previous.Length; i++)
					current[i] = previous[i - 1] + current[i - 1];
			}
		}
	}
}
