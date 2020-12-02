namespace Flux
{
	/// <summary>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension.</summary>
	public static partial class ArrayEm
	{
		/// <summary>Remove the specified number of elements at the index. This is an in-place function.</summary>
		public static void RemoveInPlace<T>(ref T[] source, int index, int count)
		{
			if (source is null) throw new System.ArgumentNullException(nameof(source));

			var sourceLength = source.Length;

			if (index < 0 || index >= sourceLength) throw new System.ArgumentOutOfRangeException(nameof(source));
			if (count < 0 || index + count > sourceLength) throw new System.ArgumentOutOfRangeException(nameof(count));

			var moveCount = sourceLength - (index + count);

			System.Array.Copy(source, index + count, source, index, moveCount);
			System.Array.Resize(ref source, index + moveCount);
		}
	}
}
