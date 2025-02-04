namespace Flux.DataStructures.BinaryTrees
{
  /// <summary>
	/// <para>A self-balancing binary search tree (BST) is any node-based binary search tree that automatically keeps its height (maximal number of levels below the root) small in the face of arbitrary item insertions and deletions.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Self-balancing_binary_search_tree"/></para>
  /// </summary>
  /// <typeparam name="TKey">The type of key for the BST node. This is used to access the associated <typeparamref name="TValue"/>.</typeparam>
  /// <typeparam name="TValue">The type of value for the BST node.</typeparam>
  /// <remarks>The original implementation is courtesy Eric Lippert.</remarks>
	public interface IBalancedBinarySearchTree<TKey, TValue>
    : IBinarySearchTree<TKey, TValue>
    where TKey : System.IComparable<TKey>
  {
  }
}
