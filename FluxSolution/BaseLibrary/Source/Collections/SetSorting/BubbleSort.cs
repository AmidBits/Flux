namespace Flux
{
	public static partial class SortingEm
	{
		/// <summary>Sorts the content of the sequence using bubble sort.</summary>
		public static void ApplyBubbleSort<T>(this System.Collections.Generic.IList<T> source, System.Collections.Generic.IComparer<T> comparer)
			=> new Sorting.BubbleSort<T>(comparer).SortInPlace((T[])source);
		/// <summary>Sorts the content of the sequence using bubble sort.</summary>
		public static void ApplyBubbleSort<T>(this System.Collections.Generic.IList<T> source)
			=> ApplyBubbleSort(source, System.Collections.Generic.Comparer<T>.Default);

		/// <summary>Sorts the content of the sequence using bubble sort.</summary>
		public static void ApplyBubbleSort<T>(this System.Span<T> source, System.Collections.Generic.IComparer<T> comparer)
			=> new Sorting.BubbleSort<T>(comparer).SortInPlace(source);
		/// <summary>Sorts the content of the sequence using bubble sort.</summary>
		public static void ApplyBubbleSort<T>(this System.Span<T> source)
			=> ApplyBubbleSort(source, System.Collections.Generic.Comparer<T>.Default);
	}

	namespace Sorting
	{
		/// <summary>Sorts the content of the sequence using an optimized version.</summary>
		/// <see cref="https://en.wikipedia.org/wiki/Bubble_sort"/>
		public class BubbleSort<T>
			: ASortable<T>, ISortableInPlace<T>
		{
			public BubbleSort(System.Collections.Generic.IComparer<T> comparer)
				: base(comparer)
			{
			}
			public BubbleSort()
				: this(System.Collections.Generic.Comparer<T>.Default)
			{
			}

			public void SortInPlace(System.Span<T> source)
			{
				var length = source.Length;

				do
				{
					var newLength = 0;

					for (var i = 1; i < length; i++)
					{
						if (Comparer.Compare(source[i - 1], source[i]) > 0)
						{
							source.Swap(i - 1, i);

							newLength = i;
						}
					}

					length = newLength;
				}
				while (length > 1);
			}
		}
	}
}