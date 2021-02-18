namespace Flux
{
	/// <summary>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension.</summary>
	public static partial class ArrayRank1
	{
		/// <summary>Create a new array with the specified number of elements removed at the index.</summary>
		public static T[] Remove<T>(this T[] source, int index, int count)
		{
			if (source is null) throw new System.ArgumentNullException(nameof(source));

			var sourceLength = source.Length;

			if (index < 0 || index >= sourceLength) throw new System.ArgumentOutOfRangeException(nameof(source));
			if (count < 0 || index + count > sourceLength) throw new System.ArgumentOutOfRangeException(nameof(count));

			var targetLength = sourceLength - count;

			var target = new T[targetLength];

			if (index > 0) // Copy left-most elements, if needed.
				System.Array.Copy(source, 0, target, 0, index);

			var copyIndex = index + count;

			if (copyIndex < targetLength) // Copy right-most elements, if needed.
				System.Array.Copy(source, copyIndex, target, index, sourceLength - copyIndex);

			return target;
		}
	}
}
