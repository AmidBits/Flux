namespace Flux
{
	public static partial class SystemCollectionsGenericIListEm
	{
		/// <summary>Swap two elements by the specified indices.</summary>
		public static void Swap<T>(this System.Collections.Generic.IList<T> source, int indexA, int indexB)
		{
			if (source.Count == 0)
				throw new System.ArgumentException(@"The sequence is empty.");
			else if (indexA < 0 || indexA >= source.Count)
				throw new System.ArgumentOutOfRangeException(nameof(indexA));
			else if (indexB < 0 || indexB >= source.Count)
				throw new System.ArgumentOutOfRangeException(nameof(indexB));
			else if (indexA != indexB)
			{
				var tmp = source[indexA];
				source[indexA] = source[indexB];
				source[indexB] = tmp;
			}
		}
		public static void SwapFirstWith<T>(this System.Collections.Generic.IList<T> source, int index)
			=> Swap(source, 0, index);

		public static void SwapLastWith<T>(this System.Collections.Generic.IList<T> source, int index)
			=> Swap(source, index, (source ?? throw new System.ArgumentNullException(nameof(source))).Count - 1);
	}
}
