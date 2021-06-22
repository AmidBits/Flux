namespace Flux.DataStructures
{
	public interface IBinaryTreeArrayNode<TKey, TValue>
		where TKey : System.IComparable<TKey>
	{
		bool IsEmpty { get; }
		TKey Key { get; }
		TValue Value { get; }
	}
}
