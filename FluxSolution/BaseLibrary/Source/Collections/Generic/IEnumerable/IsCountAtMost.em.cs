namespace Flux
{
	public static partial class ExtensionMethods
	{
		/// <summary>Returns whether the sequence has at most the specified count (number of elements) matching the predicate.</summary>
		public static bool IsCountAtMost<T>(this System.Collections.Generic.IEnumerable<T> source, int maxCount, System.Func<T, int, bool> predicate)
		{
			if (source is null) throw new System.ArgumentNullException(nameof(source));
			if (maxCount < 0) throw new System.ArgumentOutOfRangeException(nameof(maxCount));
			if (predicate is null) throw new System.ArgumentNullException(nameof(predicate));

			using var e = source.GetEnumerator();
			int counter = 0, index = 0;
			while (counter <= maxCount && e.MoveNext())
				if (predicate(e.Current, index++))
					counter++;
			return counter <= maxCount;
		}
	}
}