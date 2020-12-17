namespace Flux
{
	public static partial class IEnumerableEm
	{
		/// <summary>Returns whether the sequence has at least the specified count (number of elements) matching the predicate.</summary>
		public static bool IsCountAtLeast<T>(this System.Collections.Generic.IEnumerable<T> source, int minCount, System.Func<T, int, bool> predicate)
		{
			if (source is null) throw new System.ArgumentNullException(nameof(source));
			if (minCount < 0) throw new System.ArgumentOutOfRangeException(nameof(minCount));
			if (predicate is null) throw new System.ArgumentNullException(nameof(predicate));

			using var e = source.GetEnumerator();
			int counter = 0, index = 0;
			while (counter < minCount && e.MoveNext())
				if (predicate(e.Current, index++))
					counter++;
			return counter >= minCount;
		}

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

		/// <summary>Returns whether the sequence has at least the specified minimum count (number of elements) and at most the maximum count, matching the predicate.</summary>
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
