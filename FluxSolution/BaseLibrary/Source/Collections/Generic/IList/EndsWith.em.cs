namespace Flux
{
	public static partial class IListEm
	{
		/// <summary>Indicates whether the sequence ends with the other sequence. Uses the specified comparer.</summary>
		public static bool EndsWith<T>(this System.Collections.Generic.IList<T> source, System.Collections.Generic.IList<T> target, System.Collections.Generic.IEqualityComparer<T> equalityComparer)
		{
			if (source is null) throw new System.ArgumentNullException(nameof(source));
			if (target is null) throw new System.ArgumentNullException(nameof(target));
			if (equalityComparer is null) throw new System.ArgumentNullException(nameof(equalityComparer));

			var sourceCount = source.Count;
			var targetCount = target.Count;

			if (sourceCount < targetCount) return false;

			for (int sourceIndex = sourceCount - 1, valueIndex = targetCount - 1; sourceIndex >= 0 && valueIndex >= 0; sourceIndex--, valueIndex--)
				if (!equalityComparer.Equals(source[sourceIndex], target[valueIndex]))
					return false;

			return true;
		}
		/// <summary>Indicates whether the sequence ends with the other sequence. Uses the default comparer.</summary>
		public static bool EndsWith<T>(this System.Collections.Generic.IList<T> source, System.Collections.Generic.IList<T> target)
			=> EndsWith(source, target, System.Collections.Generic.EqualityComparer<T>.Default);
	}
}
