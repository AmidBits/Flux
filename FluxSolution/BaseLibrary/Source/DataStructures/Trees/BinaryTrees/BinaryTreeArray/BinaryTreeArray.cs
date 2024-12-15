namespace Flux.DataStructure
{
  public sealed class BinaryTreeArray<TKey, TValue>
    where TKey : System.IComparable<TKey>
  {
    private static readonly IBinaryTreeArrayNode<TKey, TValue> Empty = new BinaryTreeArrayEmpty();

    private IBinaryTreeArrayNode<TKey, TValue>[] m_data;

    public IBinaryTreeArrayNode<TKey, TValue> this[int index]
    {
      get { return (index >= 0 && index < m_data.Length) ? m_data[index] : Empty; }
      set { m_data[index] = (index >= 0 && index < m_data.Length) ? value : throw new System.ArgumentOutOfRangeException(nameof(value)); }
    }
    public int Count { get; private set; }

    /// <summary>Yields the count of descendants plus this node.</summary>
    public int GetTreeCount(int index)
      => index > m_data.Length - 1 || m_data[index] is var indexItem && indexItem.IsEmpty
      ? 0
      : GetTreeCount(ChildIndexLeft(index)) + 1 + GetTreeCount(ChildIndexRight(index));

    public BinaryTreeArray(int size)
    {
      m_data = new IBinaryTreeArrayNode<TKey, TValue>[size];

      for (var index = 0; index < m_data.Length; index++)
      {
        m_data[index] = Empty;
      }
    }
    public BinaryTreeArray()
      : this(1)
    { }

    ///// <summary>
    ///// <para>Creates a new <see cref="Flux.DataStructures.IBinaryTree{TValue}"/> based on the <paramref name="inOrder"/> and <paramref name="levelOrder"/> arrays.</para>
    ///// </summary>
    ///// <param name="inOrder">The values from a binary-tree using in-order traversal.</param>
    ///// <param name="levelOrder">The values from a binary-tree using level-order traversal.</param>
    ///// <returns></returns>
    //public static Flux.DataStructures.IBinaryTree<TValue> Create(TValue[] inOrder, TValue[] levelOrder)
    //{
    //  return ConstructTree(Flux.DataStructures.ImmutableBinaryTree<TValue>.Empty, levelOrder, inOrder, 0, inOrder.Length - 1);

    //  static Flux.DataStructures.IBinaryTree<TValue> ConstructTree(Flux.DataStructures.IBinaryTree<TValue> startNode, TValue[] levelOrder, TValue[] inOrder, int inStart, int inEnd)
    //  {
    //    if (inStart > inEnd)
    //      return Flux.DataStructures.ImmutableBinaryTree<TValue>.Empty;

    //    if (inStart == inEnd)
    //      return new Flux.DataStructures.ImmutableBinaryTree<TValue>(inOrder[inStart], Flux.DataStructures.ImmutableBinaryTree<TValue>.Empty, Flux.DataStructures.ImmutableBinaryTree<TValue>.Empty);

    //    var found = false;
    //    var index = 0;

    //    for (var i = 0; i < levelOrder.Length - 1; i++) // It represents the index in inOrder array of element that appear first in levelOrder array.
    //    {
    //      var data = levelOrder[i];

    //      for (var j = inStart; j < inEnd; j++)
    //      {
    //        if (data.Equals(inOrder[j]))
    //        {
    //          startNode = new Flux.DataStructures.ImmutableBinaryTree<TValue>(data, Flux.DataStructures.ImmutableBinaryTree<TValue>.Empty, Flux.DataStructures.ImmutableBinaryTree<TValue>.Empty);
    //          index = j;
    //          found = true;
    //          break;
    //        }
    //      }

    //      if (found == true)
    //        break;
    //    }

    //    startNode = new Flux.DataStructures.ImmutableBinaryTree<TValue>(
    //      startNode.Value,
    //      ConstructTree(startNode, levelOrder, inOrder, inStart, index - 1), // Elements before index are part of left child of startNode.
    //      ConstructTree(startNode, levelOrder, inOrder, index + 1, inEnd) // Elements after index are part of right child of startNode.
    //    );

    //    return startNode;
    //  }
    //}

    /// <summary>Determines whether this is a valid binary-search-tree.</summary>
    public bool IsBst(int index, TKey minimumKey, TKey maximumKey)
    {
      if (index >= m_data.Length - 1 || (m_data[index] is var item && item.IsEmpty)) return true;
      if (item.Key.CompareTo(minimumKey) < 0 || item.Key.CompareTo(maximumKey) > 0) return false;

      return IsBst(ChildIndexLeft(index), minimumKey, item.Key) && IsBst(ChildIndexRight(index), item.Key, maximumKey);
    }

    /// <summary>Determines whether this is a valid binary-tree.</summary>
    public bool IsBt(int index)
    {
      var item = m_data[index];

      var indexL = ChildIndexLeft(index);
      var indexR = ChildIndexRight(index);

      var isL = indexL > m_data.Length - 1 || (m_data[indexL] is var itemL && (itemL.IsEmpty || (itemL.Key.CompareTo(item.Key) < 0 && IsBt(indexL))));
      var isR = indexR > m_data.Length - 1 || (m_data[indexR] is var itemR && (itemR.IsEmpty || (itemR.Key.CompareTo(item.Key) > 0 && IsBt(indexR))));

      return isL && isR;
    }

    public static int ChildIndexLeft(int parentIndex) => (parentIndex << 1) + 1;
    public static int ChildIndexRight(int parentIndex) => (parentIndex << 1) + 2;
    public static int ParentIndex(int childIndex) => childIndex <= 0 ? -1 : (childIndex - 1) >> 1;

    private bool Delete(TKey key, int index)
    {
      if (Search(key, index) is var deleteIndex && deleteIndex > -1)
      {
        Count--;

        m_data[deleteIndex] = Empty;

        for (var replaceIndex = m_data.Length - 1; replaceIndex > deleteIndex; replaceIndex--)
          if (!m_data[replaceIndex].IsEmpty)
          {
            m_data[deleteIndex] = m_data[replaceIndex];
            m_data[replaceIndex] = Empty;
            break;
          }

        return true;
      }

      return false;
    }
    /// <summary>Deletes the first node with a matching key from the tree.</summary>
    /// <returns>Whether the key was found and a delete occured.</returns>
    public bool Delete(TKey key) => Delete(key, 0);

    private System.Collections.Generic.IEnumerable<IBinaryTreeArrayNode<TKey, TValue>> TraverseInOrder(int index)
    {
      if (index < 0 || index >= m_data.Length)
        yield break;

      foreach (var nodeL in TraverseInOrder(ChildIndexLeft(index)))
        if (!nodeL.IsEmpty)
          yield return nodeL;
      var node = m_data[index];
      if (!node.IsEmpty)
        yield return node;
      foreach (var nodeR in TraverseInOrder(ChildIndexRight(index)))
        if (!nodeR.IsEmpty)
          yield return nodeR;
    }
    /// <summary>Depth-first search (DFS), in-order (LNR). In a binary search tree, in-order traversal retrieves data in sorted order.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Tree_traversal#In-order"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Depth-first_search"/>
    public System.Collections.Generic.IEnumerable<IBinaryTreeArrayNode<TKey, TValue>> TraverseInOrder() => TraverseInOrder(0);

    private System.Collections.Generic.IEnumerable<IBinaryTreeArrayNode<TKey, TValue>> TraversePostOrder(int index)
    {
      if (index < 0 || index >= m_data.Length)
        yield break;

      foreach (var nodeL in TraversePostOrder(ChildIndexLeft(index)))
        if (!nodeL.IsEmpty)
          yield return nodeL;
      foreach (var nodeR in TraversePostOrder(ChildIndexRight(index)))
        if (!nodeR.IsEmpty)
          yield return nodeR;
      var node = m_data[index];
      if (!node.IsEmpty)
        yield return node;
    }
    /// <summary>Depth-first search (DFS), post-order (LRN). The trace of a traversal is called a sequentialisation of the tree. The traversal trace is a list of each visited root.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Tree_traversal#Post-order"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Depth-first_search"/>
    public System.Collections.Generic.IEnumerable<IBinaryTreeArrayNode<TKey, TValue>> TraversePostOrder() => TraversePostOrder(0);

    private System.Collections.Generic.IEnumerable<IBinaryTreeArrayNode<TKey, TValue>> TraversePreOrder(int index)
    {
      if (index < 0 || index >= m_data.Length)
        yield break;

      var node = m_data[index];
      if (!node.IsEmpty)
        yield return node;
      foreach (var nodeL in TraversePreOrder(ChildIndexLeft(index)))
        if (!nodeL.IsEmpty)
          yield return nodeL;
      foreach (var nodeR in TraversePreOrder(ChildIndexRight(index)))
        if (!nodeR.IsEmpty)
          yield return nodeR;
    }
    /// <summary>Depth-first search (DFS), pre-order (NLR). The pre-order traversal is a topologically sorted one, because a parent node is processed before any of its child nodes is done.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Tree_traversal#Pre-order"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Depth-first_search"/>
    public System.Collections.Generic.IEnumerable<IBinaryTreeArrayNode<TKey, TValue>> TraversePreOrder() => TraversePreOrder(0);

    private void Insert(TKey key, TValue value, int index)
    {
      if (index >= m_data.Length) // Extend array if needed.
      {
        var newIndices = m_data.Length;
        System.Array.Resize(ref m_data, index + 1);
        System.Array.Fill(m_data, Empty, newIndices, m_data.Length - newIndices);
      }

      if (m_data[index] is var indexItem && indexItem.IsEmpty)
      {
        m_data[index] = new BinaryTreeArrayValue(key, value);

        Count++;
      }
      else if (key.CompareTo(indexItem.Key) < 0)
        Insert(key, value, ChildIndexLeft(index));
      else
        Insert(key, value, ChildIndexRight(index));
    }
    /// <summary>Creates a new node from the key-value and adds the node into the tree.</summary>
    public void Insert(TKey key, TValue value) => Insert(key, value, 0);

    private int Search(TKey key, int index)
    {
      if (index >= m_data.Length - 1)
        return -1;

      var node = m_data[index];

      if (node.IsEmpty)
        return -1;

      if (node.Key.CompareTo(key) != 0)
      {
        if (Search(key, ChildIndexLeft(index)) is var indexL && indexL > -1)
          return indexL;
        if (Search(key, ChildIndexRight(index)) is var indexR && indexR > -1)
          return indexR;
      }
      else
        return index;

      return -1;
    }
    /// <summary>Searches the tree for the first node with a matching key.</summary>
    public IBinaryTreeArrayNode<TKey, TValue> Search(TKey key) => Search(key, 0) is var index && index > -1 ? m_data[index] : Empty;

    public override string ToString() => $"{GetType().Name} {{ Count = {Count} }}";

    private sealed class BinaryTreeArrayValue
      : IBinaryTreeArrayNode<TKey, TValue>
    {
      private readonly TKey m_key;
      private readonly TValue m_value;

      public BinaryTreeArrayValue(TKey key, TValue value)
      {
        m_key = key;
        m_value = value;
      }

      public bool IsEmpty => false;
      public TKey Key => m_key;
      public TValue Value => m_value;

      public override string ToString() => $"{GetType().Name} {{ Key = \"{m_key}\", Value = \"{m_value}\" }}";
    }

    private sealed class BinaryTreeArrayEmpty
      : IBinaryTreeArrayNode<TKey, TValue>
    {
      public bool IsEmpty => true;
      public TKey Key => throw new System.InvalidOperationException(nameof(BinaryTreeArrayEmpty));
      public TValue Value => throw new System.InvalidOperationException(nameof(BinaryTreeArrayEmpty));

      public override string ToString() => GetType().Name;
    }
  }
}
