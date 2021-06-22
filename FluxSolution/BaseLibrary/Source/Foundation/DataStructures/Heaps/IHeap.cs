namespace Flux.Collections.Generic
{
	/// <summary></summary>
	/// <see cref="https://en.wikipedia.org/wiki/Heap_(data_structure)"/>
	public interface IHeap<T>
		: System.Collections.Generic.IEnumerable<T>
	{
		int Count { get; }
		T Extract();
		void Insert(T item);
		bool IsEmpty { get; }
		T Peek();

		/// <summary>Creates a new sequence with all elements from the heap.</summary>
		System.Collections.Generic.IEnumerable<T> ExtractAll()
		{
			while (!IsEmpty)
			{
				yield return Extract();
			}
		}
	}
}
