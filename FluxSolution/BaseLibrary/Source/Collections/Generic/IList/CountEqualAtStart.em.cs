namespace Flux
{
	public static partial class IListEm
	{
		/// <summary>Reports the length (or count) of equality at the start of the sequences. Using the specified comparer.</summary>
		/// <param name="minLength">The smaller length of the two spans.</param>
		public static int CountEqualAtStart<T>(this System.Collections.Generic.IList<T> source, System.Collections.Generic.IList<T> target, System.Collections.Generic.IEqualityComparer<T> equalityComparer, out int minLength)
		{
			if (source is null) throw new System.ArgumentNullException(nameof(source));
			if (target is null) throw new System.ArgumentNullException(nameof(target));
			if (equalityComparer is null) throw new System.ArgumentNullException(nameof(equalityComparer));

			minLength = System.Math.Min(source.Count, target.Count);

			var index = 0;
			while (index < minLength && equalityComparer.Equals(source[index], target[index]))
				index++;
			return index;
		}
		/// <summary>Reports the length (or count) of equality at the start of the sequences. Using the default comparer.</summary>
		/// <param name="minLength">The smaller length of the two spans.</param>
		public static int CountEqualAtStart<T>(this System.Collections.Generic.IList<T> source, System.Collections.Generic.IList<T> target, out int minLength)
			=> CountEqualAtStart(source, target, System.Collections.Generic.EqualityComparer<T>.Default, out minLength);
	}
}
