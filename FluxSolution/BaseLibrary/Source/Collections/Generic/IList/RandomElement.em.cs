namespace Flux
{
	public static partial class ExtensionMethods
	{
		/// <summary>Returns a random element from the list in the output variable. Uses the specified random number generator (the .NET cryptographic if null).</summary>
		public static bool RandomElement<T>(this System.Collections.Generic.IList<T> source, out T result, System.Random rng)
		{
			if (source is null) throw new System.ArgumentNullException(nameof(source));
			if (rng is null) throw new System.ArgumentNullException(nameof(rng));

			var sourceCount = source.Count;

			if (sourceCount > 0)
			{
				result = source[rng.Next(sourceCount)];
				return true;
			}

			result = default!;
			return false;
		}
		/// <summary>Returns a random element from the list in the output variable. Uses the .NET cryptographic random number generator.</summary>
		public static bool RandomElement<T>(this System.Collections.Generic.IList<T> source, out T result)
			=> RandomElement(source, out result, Randomization.NumberGenerator.Crypto);
	}
}
