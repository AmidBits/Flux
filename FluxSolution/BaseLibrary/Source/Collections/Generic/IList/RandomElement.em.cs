namespace Flux
{
	public static partial class IListEm
	{
		/// <summary>Returns a random element from the list in the output variable. Uses the specified random number generator.</summary>
		public static bool RandomElement<T>(this System.Collections.Generic.IList<T> source, out T result, System.Random random)
		{
			if (source is null) throw new System.ArgumentNullException(nameof(source));
			if (random is null) throw new System.ArgumentNullException(nameof(random));

			var sourceCount = source.Count;

			if (sourceCount > 0)
			{
				result = source[random.Next(sourceCount)];
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
