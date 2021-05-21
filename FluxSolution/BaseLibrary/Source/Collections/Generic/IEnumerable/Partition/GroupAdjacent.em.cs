namespace Flux
{
	public static partial class SystemCollectionsGenericEm
	{
		/// <summary>Creates a new sequence of equal (based on the specified keySelector) adjacent (consecutive) items grouped together as a key and a list. Uses the specified equality comparer.</summary>
		public static System.Collections.Generic.IEnumerable<System.Linq.IGrouping<TKey, TSource>> GroupAdjacent<TSource, TKey>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, TKey> keySelector, System.Collections.Generic.IEqualityComparer<TKey> comparer)
			=> AdjacentGrouping<TKey, TSource>.GroupAdjacent(source, keySelector, comparer);
		/// <summary>Creates a new sequence of equal (based on the specified keySelector) consecutive (adjacent) items grouped together as a key and a list. Uses the default equality comparer.</summary>
		public static System.Collections.Generic.IEnumerable<System.Linq.IGrouping<TKey, TSource>> GroupAdjacent<TSource, TKey>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, TKey> keySelector)
			=> AdjacentGrouping<TKey, TSource>.GroupAdjacent(source, keySelector, System.Collections.Generic.EqualityComparer<TKey>.Default);
	}
}
