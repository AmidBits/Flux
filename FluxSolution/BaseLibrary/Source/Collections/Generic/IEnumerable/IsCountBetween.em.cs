namespace Flux
{
	public static partial class ExtensionMethods
	{
		/// <summary>Returns whether the sequence has at least a minimum count (inclusive) and at most a maximum count (inclusive), matching the predicate.</summary>
		public static bool IsCountBetween<T>(this System.Collections.Generic.IEnumerable<T> source, int minCount, int maxCount, System.Func<T, int, bool> predicate)
		{
			if (source is null) throw new System.ArgumentNullException(nameof(source));
			if (minCount < 0) throw new System.ArgumentOutOfRangeException(nameof(minCount));
			if (maxCount < minCount) throw new System.ArgumentOutOfRangeException(nameof(maxCount));
			if (predicate is null) throw new System.ArgumentNullException(nameof(predicate));

			using var e = source.GetEnumerator();
			int counter = 0, index = 0;
			while (counter < minCount && counter <= maxCount && e.MoveNext())
				if (predicate(e.Current, index++))
					counter++;
			return counter >= minCount && counter <= maxCount;
		}
	}
}
