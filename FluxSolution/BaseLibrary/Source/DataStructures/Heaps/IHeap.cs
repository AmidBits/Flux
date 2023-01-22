namespace Flux.DataStructures
{
	/// <summary></summary>
	/// <see cref="https://en.wikipedia.org/wiki/Heap_(data_structure)"/>
	public interface IHeap<T>
	{
		/// <summary>Yields the number of items on the heap.</summary>
		int Count { get; }
		/// <summary>Determines whether the heap is empty.</summary>
		bool IsEmpty { get; }
		/// <summary>Removes the first item from the heap and returns it.</summary>
		T Extract();
		/// <summary>Inserts an item on the heap.</summary>
		void Insert(T item);
		/// <summary>Returns the first item on the heap without removing it.</summary>
		T Peek();

		/// <summary>Creates a new sequence with all items from the heap.</summary>
		System.Collections.Generic.IEnumerable<T> ExtractAll()
		{
			while (!IsEmpty)
				yield return Extract();
		}
	}
}
