namespace Flux
{
	public static partial class SystemCollectionsGenericIListEm
	{
		/// <summary>Returns a shuffled (randomized) sequence. Uses the specified Random.</summary>
		public static void Shuffle<T>(this System.Collections.Generic.IList<T> source, System.Random rng)
		{
			if (rng is null) throw new System.ArgumentNullException(nameof(rng));

			for (var index = source.Count - 1; index > 0; index--)
				source.Swap(index, rng.Next(index));
		}
		/// <summary>Returns a shuffled (randomized) sequence. Uses the cryptographic Random.</summary>
		public static void Shuffle<T>(this System.Collections.Generic.IList<T> source)
			=> Shuffle(source, Flux.Random.NumberGenerator.Crypto);
	}
}
