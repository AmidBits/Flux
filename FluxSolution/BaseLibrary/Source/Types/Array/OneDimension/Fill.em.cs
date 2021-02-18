namespace Flux
{
	/// <summary>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension.</summary>
	public static partial class ArrayRank1
	{
		/// <summary>Fill the array with the specified value pattern, at the offset and count.</summary>
		public static T[] Fill<T>(this T[] source, int offset, int count, params T[] fillPattern)
		{
			if (source is null) throw new System.ArgumentNullException(nameof(source));
			if (fillPattern is null) throw new System.ArgumentNullException(nameof(fillPattern));

			var copyLength = fillPattern.Length < count ? fillPattern.Length : count;

			System.Array.Copy(fillPattern, 0, source, offset, copyLength);

			while ((copyLength << 1) < count)
			{
				System.Array.Copy(source, offset, source, offset + copyLength, copyLength);

				copyLength <<= 1;
			}

			System.Array.Copy(source, offset, source, offset + copyLength, count - copyLength);

			return source;
		}
		/// <summary>Fill the array with the specified value pattern.</summary>
		public static T[] Fill<T>(this T[] source, params T[] fillPattern)
			=> Fill(source, 0, source?.Length ?? throw new System.ArgumentNullException(nameof(source)), fillPattern);
	}
}
