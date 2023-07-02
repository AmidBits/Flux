namespace Flux.DataStructures
{
  /// <summary>
	/// <para>A binary search tree (BST), also called an ordered or sorted binary tree, is a rooted binary tree data structure with the key of each internal node being greater than all the keys in the respective node's left subtree and less than the ones in its right subtree.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Binary_search_tree"/></para>
  /// <para><seealso href="https://ericlippert.com/2008/01/18/immutability-in-c-part-eight-even-more-on-binary-trees/"/></para>
  /// </summary>
  /// <typeparam name="TKey">The type of key for the BST node. This is used to access the associated <typeparamref name="TValue"/>.</typeparam>
  /// <typeparam name="TValue">The type of value for the BST node.</typeparam>
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
