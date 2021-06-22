namespace Flux.DataStructures.Immutable
{
	public interface IBinaryTree<TValue>
	{
		bool IsEmpty { get; }
		IBinaryTree<TValue> Left { get; }
		IBinaryTree<TValue> Right { get; }
		TValue Value { get; }
	}
}
