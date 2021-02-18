namespace Flux
{
	/// <summary>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension.</summary>
	public static partial class ArrayRank1
	{
		/// <summary>Create a new array with the specified elements inserted at the index.</summary>
		public static T[] Insert<T>(this T[] source, int index, params T[] insert)
		{
			if (source is null) throw new System.ArgumentNullException(nameof(source));
			if (insert is null) throw new System.ArgumentNullException(nameof(insert));

			var sourceLength = source.Length;
			var insertLength = insert.Length;

			if (index < 0 || index > sourceLength) throw new System.ArgumentOutOfRangeException(nameof(source));

			var targetLength = sourceLength + insertLength;

			var target = new T[targetLength];

			if (index > 0) // Copy left-most elements, if needed.
				System.Array.Copy(source, 0, target, 0, index);

			System.Array.Copy(insert, 0, target, index, insertLength); // Copy insert elements.

			if (index < sourceLength) // Copy right-most elements, if needed.
				System.Array.Copy(source, index, target, index + insertLength, sourceLength - index);

			return target;
		}
	}
}
