namespace Flux
{
	public static partial class Enumerable
	{
		/// <summary>Copies the specified number of elements from the sequence into the IList starting at the target index. This implementation copies as much as it can, i.e. the minimum of count, source items or target space.</summary>
		/// <exception cref="System.ArgumentNullException"/>
		public static int CopyInto<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IList<T> target, int targetIndex, int count)
		{
			if (source is null) throw new System.ArgumentNullException(nameof(source));
			if (target is null) throw new System.ArgumentNullException(nameof(target));

			if (targetIndex < 0 || targetIndex >= target.Count) throw new System.ArgumentOutOfRangeException(nameof(targetIndex));
			if (targetIndex + count > target.Count) throw new System.ArgumentOutOfRangeException(nameof(count));

			var offset = 0;

			foreach (var item in source)
			{
				if (count-- == 0)
					break;

				if (targetIndex + offset++ is var index && index >= target.Count)
					break;

				target[index] = item;
			}

			return offset;
		}
		/// <summary>Copies the specified number of elements from the sequence into the IList starting at the target index. This implementation copies as much as it can. If source runs out, or target runs short, the algorithm exists with the number of items copied.</summary>
		public static int CopyInto<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IList<T> target)
			=> CopyInto(source, target, 0, target.ThrowIfNull().Count());
	}
}
