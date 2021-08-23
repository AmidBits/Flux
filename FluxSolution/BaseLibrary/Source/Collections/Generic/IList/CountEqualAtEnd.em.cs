namespace Flux
{
	public static partial class ExtensionMethods
	{
		/// <summary>Reports the count of elements equal at the end of the sequences. Using the specified comparer.</summary>
		/// <param name="minLength">The smaller length of the two spans.</param>
		public static int CountEqualAtEnd<T>(this System.Collections.Generic.IList<T> source, System.Collections.Generic.IList<T> target, System.Collections.Generic.IEqualityComparer<T> equalityComparer, out int minLength)
		{
			if (source is null) throw new System.ArgumentNullException(nameof(source));
			if (target is null) throw new System.ArgumentNullException(nameof(target));
			if (equalityComparer is null) throw new System.ArgumentNullException(nameof(equalityComparer));

			var sourceIndex = source.Count;
			var targetIndex = target.Count;

			minLength = System.Math.Min(sourceIndex, targetIndex);

			for (var atEnd = 0; --sourceIndex >= 0 && --targetIndex >= 0; atEnd++)
				if (!equalityComparer.Equals(source[sourceIndex], target[targetIndex]))
					return atEnd;

			return minLength;
		}
		/// <summary>Reports the count of elements equal at the end of the sequences. Using the default comparer.</summary>
		/// <param name="minLength">The smaller length of the two spans.</param>
		public static int CountEqualAtEnd<T>(this System.Collections.Generic.IList<T> source, System.Collections.Generic.IList<T> target, out int minLength)
			=> CountEqualAtEnd(source, target, System.Collections.Generic.EqualityComparer<T>.Default, out minLength);
	}
}
