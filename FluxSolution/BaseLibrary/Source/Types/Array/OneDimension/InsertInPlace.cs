namespace Flux
{
	/// <summary>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension.</summary>
	public static partial class ArrayRank1
	{
		/// <summary>Insert the specified elements at the index.</summary>
		public static void InsertInPlace<T>(ref T[] source, int index, params T[] insert)
		{
			if (source is null) throw new System.ArgumentNullException(nameof(source));
			if (insert is null) throw new System.ArgumentNullException(nameof(insert));

			var sourceLength = source.Length;

			if (index < 0 || index >= sourceLength) throw new System.ArgumentOutOfRangeException(nameof(source));

			var insertLength = insert.Length;

			if (insertLength > 0)
			{
				System.Array.Resize(ref source, sourceLength + insertLength);

				if (index < sourceLength) // Copy the right-most elements, if needed.
					System.Array.Copy(source, index, source, index + insertLength, sourceLength - index);

				System.Array.Copy(insert, 0, source, index, insertLength); // Copy insert elements.
			}
		}
	}
}
