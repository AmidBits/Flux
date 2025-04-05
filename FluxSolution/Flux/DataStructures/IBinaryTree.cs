namespace Flux.DataStructures
{
  /// <summary>
	/// <para>A binary tree is a tree data structure in which each node has at most two children, which are referred to as the left child and the right child.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Binary_tree"/></para>
  /// <para><seealso href="https://ericlippert.com/2007/12/18/immutability-in-c-part-six-a-simple-binary-tree/"/></para>
  /// </summary>
  /// <typeparam name="TValue">The type of value for the binary tree node.</typeparam>
  /// <remarks>The original implementation is courtesy Eric Lippert.</remarks>
  public interface IBinaryTree<TValue>
  {
    /// <summary>
    /// <para>Indicates whether this <see cref="IBinaryTree{TValue}"/> is empty.</para>
    /// </summary>
    bool IsEmpty { get; }

    /// <summary>
    /// <para>The left child of the <see cref="IBinaryTree{TValue}"/>.</para>
    /// </summary>
    IBinaryTree<TValue> Left { get; }

    /// <summary>
    /// <para>The right child of the <see cref="IBinaryTree{TValue}"/>.</para>
    /// </summary>
    IBinaryTree<TValue> Right { get; }

    /// <summary>
    /// <para>The value of the <see cref="IBinaryTree{TValue}"/>.</para>
    /// </summary>
    TValue Value { get; }
  }
}
