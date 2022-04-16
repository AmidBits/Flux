namespace Flux
{
	public static partial class Enumerable
	{
		/// <summary>Returns the specified percent of random elements from the sequence. Uses the specified random number generator.</summary>
		/// <param name="percent">Percent (or probability) as a value in the range [0, 1].</param>
		/// <param name="rng">The random number generator to use.</param>
		public static System.Collections.Generic.IEnumerable<T> RandomElements<T>(this System.Collections.Generic.IEnumerable<T> source, double percent, System.Random rng)
		{
			if (percent <= 0 && percent > 1) throw new System.ArgumentOutOfRangeException(nameof(percent));

			rng ??= Randomization.NumberGenerator.Crypto;

			foreach (var element in source ?? System.Linq.Enumerable.Empty<T>())
				if (rng.NextDouble() < percent)
					yield return element;
		}
		/// <summary>Returns the specified percent of random elements from the sequence. Uses the .NET cryptographic random number generator.</summary>
		/// <param name="percent"></param>
		public static System.Collections.Generic.IEnumerable<T> RandomElements<T>(this System.Collections.Generic.IEnumerable<T> source, double percent)
			=> RandomElements(source, percent, Randomization.NumberGenerator.Crypto);
	}
}
