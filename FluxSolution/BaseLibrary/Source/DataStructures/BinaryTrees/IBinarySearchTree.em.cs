namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Gets the maximum (with the greatest key) node.</summary>
    public static DataStructures.IBinarySearchTree<TKey, TValue> GetMaximumNode<TKey, TValue>(this DataStructures.IBinarySearchTree<TKey, TValue> source)
      where TKey : System.IComparable<TKey>
    {
      System.ArgumentNullException.ThrowIfNull(source);

      if (source.IsEmpty) return source;

      var node = source;
      while (!node.Right.IsEmpty)
        node = node.Right;
      return node;
    }

    /// <summary>Gets the minimum (with the least key) node.</summary>
    public static DataStructures.IBinarySearchTree<TKey, TValue> GetMinimumNode<TKey, TValue>(this DataStructures.IBinarySearchTree<TKey, TValue> source)
      where TKey : System.IComparable<TKey>
    {
      System.ArgumentNullException.ThrowIfNull(source);

      if (source.IsEmpty) return source;

      var node = source;
      while (!node.Left.IsEmpty)
        node = node.Left;
      return node;
    }

    /// <summary>Gets the predecessor (or "previous") node by key.</summary>
    public static DataStructures.IBinarySearchTree<TKey, TValue> GetPredecessorNode<TKey, TValue>(this DataStructures.IBinarySearchTree<TKey, TValue> source)
      where TKey : System.IComparable<TKey>
    {
      System.ArgumentNullException.ThrowIfNull(source);

      return source.IsEmpty ? source : source.Left.GetMaximumNode();
    }

    /// <summary>Gets the successor (or "next") node by key.</summary>
    public static DataStructures.IBinarySearchTree<TKey, TValue> GetSuccessorNode<TKey, TValue>(this DataStructures.IBinarySearchTree<TKey, TValue> source)
      where TKey : System.IComparable<TKey>
    {
      System.ArgumentNullException.ThrowIfNull(source);

      return source.IsEmpty ? source : source.Right.GetMinimumNode();
    }

    /// <summary>Returns whether <paramref name="source"/> is a binary-search-tree considering only its <typeparamref name="TKey"/> property. Returns false if <paramref name="source"/> or any sub-nodes violates the BST property (considering only <typeparamref name="TKey"/>.</summary>
    /// <param name="minKey">The minimum key for <typeparamref name="TKey"/>.</param>
    /// <param name="maxKey">The maximum key for <typeparamref name="TKey"/>.</param>
    /// <param name="keyDecrementor">Return the key decremented.</param>
    /// <param name="keyIncrementor">Return the key incremented.</param>
    public static bool IsBstByKey<TKey, TValue>(this DataStructures.IBinarySearchTree<TKey, TValue> source, TKey minKey, TKey maxKey, System.Func<TKey, TKey> keyDecrementor, System.Func<TKey, TKey> keyIncrementor)
      where TKey : System.IComparable<TKey>
    {
      System.ArgumentNullException.ThrowIfNull(source);
      System.ArgumentNullException.ThrowIfNull(keyDecrementor);
      System.ArgumentNullException.ThrowIfNull(keyIncrementor);

      if (source.IsEmpty) return true;
      if (source.Key.CompareTo(minKey) < 0 || source.Key.CompareTo(maxKey) > 0) return false;

      return IsBstByKey(source.Left, minKey, keyDecrementor(source.Key), keyDecrementor, keyIncrementor) && IsBstByKey(source.Right, keyIncrementor(source.Key), maxKey, keyDecrementor, keyIncrementor);
    }
  }
}
