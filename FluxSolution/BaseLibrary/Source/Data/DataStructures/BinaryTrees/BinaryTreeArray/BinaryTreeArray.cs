namespace Flux.DataStructures
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
      : 1 + GetTreeCount(ChildIndexLeft(index)) + GetTreeCount(ChildIndexRight(index));

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

    /// <summary>Determines whether this is a valid binary-search-tree.</summary>
    public bool IsBST(int index, TKey minimumKey, TKey maximumKey)
    {
      if (index >= m_data.Length - 1 || (m_data[index] is var item && item.IsEmpty)) return true;
      if (item.Key.CompareTo(minimumKey) < 0 || item.Key.CompareTo(maximumKey) > 0) return false;

      return IsBST(ChildIndexLeft(index), minimumKey, item.Key) && IsBST(ChildIndexRight(index), item.Key, maximumKey);
    }

    /// <summary>Determines whether this is a valid binary-tree.</summary>
    public bool IsBT(int index)
    {
      var item = m_data[index];

      var indexL = ChildIndexLeft(index);
      var indexR = ChildIndexRight(index);

      var isL = indexL > m_data.Length - 1 || (m_data[indexL] is var itemL && (itemL.IsEmpty || (itemL.Key.CompareTo(item.Key) < 0 && IsBT(indexL))));
      var isR = indexR > m_data.Length - 1 || (m_data[indexR] is var itemR && (itemR.IsEmpty || (itemR.Key.CompareTo(item.Key) > 0 && IsBT(indexR))));

      return isL && isR;
    }

    public static int ChildIndexLeft(int index) => (index << 1) + 1;
    public static int ChildIndexRight(int index) => (index << 1) + 2;
    public static int ParentIndex(int index) => index <= 0 ? -1 : (index - 1) >> 1;

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

    private System.Collections.Generic.IEnumerable<IBinaryTreeArrayNode<TKey, TValue>> GetNodesInOrder(int index)
    {
      if (index < 0 || index >= m_data.Length)
        yield break;

      foreach (var nodeL in GetNodesInOrder(ChildIndexLeft(index)))
        if (!nodeL.IsEmpty)
          yield return nodeL;
      var node = m_data[index];
      if (!node.IsEmpty)
        yield return node;
      foreach (var nodeR in GetNodesInOrder(ChildIndexRight(index)))
        if (!nodeR.IsEmpty)
          yield return nodeR;
    }
    /// <summary>Depth-first search (DFS), in-order (LNR). In a binary search tree, in-order traversal retrieves data in sorted order.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Tree_traversal#In-order"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Depth-first_search"/>
    public System.Collections.Generic.IEnumerable<IBinaryTreeArrayNode<TKey, TValue>> GetNodesInOrder() => GetNodesInOrder(0);

    private System.Collections.Generic.IEnumerable<IBinaryTreeArrayNode<TKey, TValue>> GetNodesPostOrder(int index)
    {
      if (index < 0 || index >= m_data.Length)
        yield break;

      foreach (var nodeL in GetNodesPostOrder(ChildIndexLeft(index)))
        if (!nodeL.IsEmpty)
          yield return nodeL;
      foreach (var nodeR in GetNodesPostOrder(ChildIndexRight(index)))
        if (!nodeR.IsEmpty)
          yield return nodeR;
      var node = m_data[index];
      if (!node.IsEmpty)
        yield return node;
    }
    /// <summary>Depth-first search (DFS), post-order (LRN). The trace of a traversal is called a sequentialisation of the tree. The traversal trace is a list of each visited root.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Tree_traversal#Post-order"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Depth-first_search"/>
    public System.Collections.Generic.IEnumerable<IBinaryTreeArrayNode<TKey, TValue>> GetNodesPostOrder() => GetNodesPostOrder(0);

    private System.Collections.Generic.IEnumerable<IBinaryTreeArrayNode<TKey, TValue>> GetNodesPreOrder(int index)
    {
      if (index < 0 || index >= m_data.Length)
        yield break;

      var node = m_data[index];
      if (!node.IsEmpty)
        yield return node;
      foreach (var nodeL in GetNodesPreOrder(ChildIndexLeft(index)))
        if (!nodeL.IsEmpty)
          yield return nodeL;
      foreach (var nodeR in GetNodesPreOrder(ChildIndexRight(index)))
        if (!nodeR.IsEmpty)
          yield return nodeR;
    }
    /// <summary>Depth-first search (DFS), pre-order (NLR). The pre-order traversal is a topologically sorted one, because a parent node is processed before any of its child nodes is done.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Tree_traversal#Pre-order"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Depth-first_search"/>
    public System.Collections.Generic.IEnumerable<IBinaryTreeArrayNode<TKey, TValue>> GetNodesPreOrder() => GetNodesPreOrder(0);

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

    //public System.Collections.Generic.IEnumerable<(int, IBinaryTreeArrayNode<TKey, TValue>)> TraverseBreadthFirstSearch(int index)
    //{
    //  var current = new System.Collections.Generic.Queue<int>();
    //  current.Enqueue(index);

    //  var next = new System.Collections.Generic.Queue<int>();
    //  var level = 0;

    //  while (current.Count > 0)
    //  {
    //    index = current.Dequeue();

    //    if (this[index] is var item && !item.IsEmpty)
    //      yield return (level, item);

    //    if (ChildIndexLeft(index) is var indexL && !this[indexL].IsEmpty)
    //      next.Enqueue(indexL);
    //    if (ChildIndexRight(index) is var indexR && !this[indexR].IsEmpty)
    //      next.Enqueue(indexR);

    //    if (current.Count == 0)
    //    {
    //      current = next;
    //      next = new System.Collections.Generic.Queue<int>();
    //      level++;
    //    }
    //  }
    //}

    //public System.Collections.Generic.IEnumerable<IBinaryTreeArrayNode<TKey, TValue>> TraverseDepthFirstSearchInOrder(int index)
    //{
    //  Flux.Collections.Generic.Traversal.BinaryTreeSearch()
    //  if (this[index] is var item && item.IsEmpty) yield break;

    //  foreach (var itemL in TraverseDepthFirstSearchInOrder(ChildIndexLeft(index))) yield return itemL;
    //  yield return item;
    //  foreach (var itemR in TraverseDepthFirstSearchInOrder(ChildIndexRight(index))) yield return itemR;
    //}
    //public System.Collections.Generic.IEnumerable<IBinaryTreeArrayNode<TKey, TValue>> TraverseDepthFirstSearchPostOrder(int index)
    //{
    //  if (this[index] is var item && item.IsEmpty) yield break;

    //  foreach (var itemL in TraverseDepthFirstSearchPostOrder(ChildIndexLeft(index))) yield return itemL;
    //  foreach (var itemR in TraverseDepthFirstSearchPostOrder(ChildIndexRight(index))) yield return itemR;
    //  yield return item;
    //}
    //public System.Collections.Generic.IEnumerable<IBinaryTreeArrayNode<TKey, TValue>> TraverseDepthFirstSearchPreOrder(int index)
    //{
    //  if (this[index] is var item && item.IsEmpty) yield break;

    //  yield return item;
    //  foreach (var itemL in TraverseDepthFirstSearchPreOrder(ChildIndexLeft(index))) yield return itemL;
    //  foreach (var itemR in TraverseDepthFirstSearchPreOrder(ChildIndexRight(index))) yield return itemR;
    //}

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
