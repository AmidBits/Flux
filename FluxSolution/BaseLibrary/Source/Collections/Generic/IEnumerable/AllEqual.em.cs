namespace Flux
{
	public static partial class IEnumerableEm
	{
		public static bool AllEqual<TSource, TValue>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, TValue> valueSelector, out TValue value, System.Collections.Generic.IEqualityComparer<TValue> comparer)
		{
			if (source is null) throw new System.ArgumentNullException(nameof(source));
			if (valueSelector is null) throw new System.ArgumentNullException(nameof(valueSelector));
			if (comparer is null) throw new System.ArgumentNullException(nameof(comparer));

			using var enumerator = source.GetEnumerator();

			if (enumerator.MoveNext())
			{
				var previousValue = value = valueSelector(enumerator.Current); // Ensure first round has the same values.

				while (enumerator.MoveNext())
				{
					value = valueSelector(enumerator.Current);

					if (!comparer.Equals(value, previousValue))
						return false;

					previousValue = value;
				}

				return true;
			}
			else throw new System.ArgumentException(@"The sequence is empty.", nameof(source));
		}
		public static bool AllEqual<TSource, TValue>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, TValue> valueSelector, out TValue value)
			=> AllEqual(source, valueSelector, out value, System.Collections.Generic.EqualityComparer<TValue>.Default);
	}
}
