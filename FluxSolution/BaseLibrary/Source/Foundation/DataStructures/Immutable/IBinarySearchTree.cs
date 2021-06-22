namespace Flux.DataStructures.Immutable
{
	public interface IBinarySearchTree<TKey, TValue>
		: IBinaryTree<TValue>, IMap<TKey, TValue>
		where TKey : System.IComparable<TKey>
	{
		TKey Key { get; }
		new IBinarySearchTree<TKey, TValue> Left { get; }
		new IBinarySearchTree<TKey, TValue> Right { get; }
		new IBinarySearchTree<TKey, TValue> Add(TKey key, TValue value);
		new IBinarySearchTree<TKey, TValue> Remove(TKey key);
		IBinarySearchTree<TKey, TValue> Search(TKey key);
	}
}
