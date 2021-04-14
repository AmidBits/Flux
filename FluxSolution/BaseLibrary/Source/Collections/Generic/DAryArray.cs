namespace Flux.Collections.Generic
{
	public interface IDAryNode<TKey, TValue>
		where TKey : System.IComparable<TKey>
	{
		bool IsEmpty { get; }
		TKey Key { get; }
		TValue Value { get; }
	}

	/// <summary></summary>
	/// <see cref="https://en.wikipedia.org/wiki/Heap_(data_structure)"/>
	public abstract class DAryArray<TKey, TValue>
		where TKey : System.IComparable<TKey>
	{
		private IDAryNode<TKey, TValue>[] m_array = System.Array.Empty<IDAryNode<TKey, TValue>>();

		public DAryArray(int k)
			=> K = k;

		public int K { get; init; }

		public int GetChildIndexOf(int index, int childIndex)
			=> (K * index) + childIndex;
		public int GetParentIndexOf(int index)
			=> index <= 0 ? -1 : (index - 1) / K;
	}
}
