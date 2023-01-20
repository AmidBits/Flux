namespace Flux.DataStructures
{
	public interface IBinarySearchTree<TKey, TValue>
		: IBinaryTree<TValue>, IMap<TKey, TValue>
		where TKey : System.IComparable<TKey>
	{
		/// <summary>The key of the BST node.</summary>
		TKey Key { get; }

		/// <summary>The left child of the BST node.</summary>
		new IBinarySearchTree<TKey, TValue> Left { get; }
		/// <summary>The right child of the BST node.</summary>
		new IBinarySearchTree<TKey, TValue> Right { get; }

		/// <summary>Add a key with a value to the BST.</summary>
		new IBinarySearchTree<TKey, TValue> Add(TKey key, TValue value);
		/// <summary>Remove a key from the BST.</summary>
		new IBinarySearchTree<TKey, TValue> Remove(TKey key);
		/// <summary>Search for a key in the BST.</summary>
		IBinarySearchTree<TKey, TValue> Search(TKey key);
	}
}
