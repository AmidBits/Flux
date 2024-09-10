namespace Flux.DataStructures
{
  /// <summary>
	/// <para>A binary search tree (BST), also called an ordered or sorted binary tree, is a rooted binary tree data structure with the key of each internal node being greater than all the keys in the respective node's left subtree and less than the ones in its right subtree.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Binary_search_tree"/></para>
  /// <para><seealso href="https://ericlippert.com/2008/01/18/immutability-in-c-part-eight-even-more-on-binary-trees/"/></para>
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
