namespace Flux
{
	public static partial class SetSortEm
	{
		/// <summary>Sorts the content of the sequence using insertion sort.</summary>
		public static void ApplyInsertionSort<T>(this System.Collections.Generic.IList<T> source, System.Collections.Generic.IComparer<T> comparer)
			=> new Sequence.Sort.InsertionSort<T>(comparer).SortInPlace((T[])source);
		/// <summary>Sorts the content of the sequence using insertion sort.</summary>
		public static void ApplyInsertionSort<T>(this System.Collections.Generic.IList<T> source)
			=> ApplyInsertionSort(source, System.Collections.Generic.Comparer<T>.Default);

		/// <summary>Sorts the content of the sequence using insertion sort.</summary>
		public static void ApplyInsertionSort<T>(this System.Span<T> source, System.Collections.Generic.IComparer<T> comparer)
			=> new Sequence.Sort.InsertionSort<T>(comparer).SortInPlace(source);
		/// <summary>Sorts the content of the sequence using insertion sort.</summary>
		public static void ApplyInsertionSort<T>(this System.Span<T> source)
			=> ApplyInsertionSort(source, System.Collections.Generic.Comparer<T>.Default);
	}

	namespace Sequence.Sort
	{
		/// <summary>Sorts the content of the sequence using insertion sort.</summary>
		/// <see cref="https://en.wikipedia.org/wiki/Insertion_sort"/>
		public class InsertionSort<T>
			: ASortable<T>, ISortableInPlace<T>
		{
			public InsertionSort(System.Collections.Generic.IComparer<T> comparer)
				: base(comparer)
			{
			}
			public InsertionSort()
				: this(System.Collections.Generic.Comparer<T>.Default)
			{
			}

			public void SortInPlaceOld(System.Span<T> source)
			{
				var sourceLength = source.Length;

				for (var i = 1; i < sourceLength; i++)
					for (var j = i; j > 0 && Comparer.Compare(source[j - 1], source[j]) > 0; j--)
						source.Swap(j, j - 1);
			}

			public void SortInPlace(System.Span<T> source)
			{
				var sourceLength = source.Length;

				for (var i = 1; i < sourceLength; i++)
				{
					var tmp = source[i];

					var j = i - 1;

					while (j >= 0 && Comparer.Compare(source[j], tmp) > 0)
					{
						source[j + 1] = source[j];
						j--;
					}

					source[j + 1] = tmp;
				}
			}
		}
	}
}