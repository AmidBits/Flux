namespace Flux
{
  public static partial class IBinarySearchTreeEm
  {
    /// <summary>Gets the maximum (with the greatest key) node.</summary>
    public static Collections.Immutable.IBinarySearchTree<TKey, TValue> GetMaximumNode<TKey, TValue>(this Collections.Immutable.IBinarySearchTree<TKey, TValue> source)
      where TKey : System.IComparable<TKey>
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      if (source.IsEmpty) return source;

      var node = source;
      while (!node.Right.IsEmpty)
        node = node.Right;
      return node;
    }
    /// <summary>Gets the minimum (with the least key) node.</summary>
    public static Collections.Immutable.IBinarySearchTree<TKey, TValue> GetMinimumNode<TKey, TValue>(this Collections.Immutable.IBinarySearchTree<TKey, TValue> source)
      where TKey : System.IComparable<TKey>
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      if (source.IsEmpty) return source;

      var node = source;
      while (!node.Left.IsEmpty)
        node = node.Left;
      return node;
    }

    /// <summary>Gets the predecessor (or "previous") node by key.</summary>
    public static Collections.Immutable.IBinarySearchTree<TKey, TValue> GetPredecessorNode<TKey, TValue>(this Collections.Immutable.IBinarySearchTree<TKey, TValue> source)
      where TKey : System.IComparable<TKey>
      => (source ?? throw new System.ArgumentNullException(nameof(source))).IsEmpty ? source : source.Left.GetMaximumNode();
    /// <summary>Gets the successor (or "next") node by key.</summary>
    public static Collections.Immutable.IBinarySearchTree<TKey, TValue> GetSuccessorNode<TKey, TValue>(this Collections.Immutable.IBinarySearchTree<TKey, TValue> source)
      where TKey : System.IComparable<TKey>
      => (source ?? throw new System.ArgumentNullException(nameof(source))).IsEmpty ? source : source.Right.GetMinimumNode();

    /// <summary>Depth-first search (DFS), in-order (LNR). In a binary search tree, in-order traversal retrieves data in sorted order.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Tree_traversal#In-order"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Depth-first_search"/>
    public static System.Collections.Generic.IEnumerable<Collections.Immutable.IBinarySearchTree<TKey, TValue>> GetNodesInOrder<TKey, TValue>(this Collections.Immutable.IBinarySearchTree<TKey, TValue> source)
      where TKey : System.IComparable<TKey>
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var stack = new System.Collections.Generic.Stack<Collections.Immutable.IBinarySearchTree<TKey, TValue>>();

      var node = source;

      while (stack.Count > 0 || !node.IsEmpty)
      {
        if (node.IsEmpty)
        {
          node = stack.Pop();

          yield return node;

          node = node.Right;
        }
        else
        {
          stack.Push(node);

          node = node.Left;
        }
      }
    }
    /// <summary>Depth-first search (DFS), in-order reverse (RNL). In a binary search tree, in-order traversal retrieves data in sorted order. This is in-order reversed.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Tree_traversal#In-order"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Depth-first_search"/>
    public static System.Collections.Generic.IEnumerable<Collections.Immutable.IBinarySearchTree<TKey, TValue>> GetNodesInOrderReversed<TKey, TValue>(this Collections.Immutable.IBinarySearchTree<TKey, TValue> source)
      where TKey : System.IComparable<TKey>
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var stack = new System.Collections.Generic.Stack<Collections.Immutable.IBinarySearchTree<TKey, TValue>>();

      var node = source;

      while (stack.Count > 0 || !node.IsEmpty)
      {
        if (node.IsEmpty)
        {
          node = stack.Pop();

          yield return node;

          node = node.Left;
        }
        else
        {
          stack.Push(node);

          node = node.Right;
        }
      }
    }

    /// <summary>Breadth-first search (BFS), level order. Traversal yields the binary tree levels starting with the root, then its two possible children, then their children, and so on "in generations".</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Tree_traversal#Breadth-first_search_/_level_order"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Breadth-first_search"/>
    public static System.Collections.Generic.IEnumerable<Collections.Immutable.IBinarySearchTree<TKey, TValue>> GetNodesLevelOrder<TKey, TValue>(this Collections.Immutable.IBinarySearchTree<TKey, TValue> source, int maxDepth)
      where TKey : System.IComparable<TKey>
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      if (source.IsEmpty) yield break;

      var level = new System.Collections.Generic.Queue<Collections.Immutable.IBinarySearchTree<TKey, TValue>>();
      level.Enqueue(source);

      for (var depth = 0; level.Count > 0 && depth < maxDepth; depth++)
      {
        var nextLevel = new System.Collections.Generic.Queue<Collections.Immutable.IBinarySearchTree<TKey, TValue>>();

        while (level.Count > 0)
        {
          var node = level.Dequeue();

          yield return node;

          if (!node.Left.IsEmpty) nextLevel.Enqueue(node.Left);
          if (!node.Right.IsEmpty) nextLevel.Enqueue(node.Right);
        }

        level = nextLevel;
      }
    }
    /// <summary>Breadth-first search (BFS), chunked level order. Traversal yields the binary tree levels, in chunks, starting with the root, then its two children, then their children, and so on "in generations".</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Tree_traversal#Breadth-first_search_/_level_order"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Breadth-first_search"/>
    public static System.Collections.Generic.IEnumerable<Collections.Immutable.IBinarySearchTree<TKey, TValue>[]> GetNodesLevelOrderChunked<TKey, TValue>(this Collections.Immutable.IBinarySearchTree<TKey, TValue> source)
      where TKey : System.IComparable<TKey>
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      if (source.IsEmpty) yield break;

      var level = new System.Collections.Generic.Queue<Collections.Immutable.IBinarySearchTree<TKey, TValue>>();
      level.Enqueue(source);

      while (level.Count > 0)
      {
        yield return level.ToArray();

        for (var index = level.Count; index > 0; index--)
        {
          var node = level.Dequeue();

          if (!node.Left.IsEmpty) level.Enqueue(node.Left);
          if (!node.Right.IsEmpty) level.Enqueue(node.Right);
        }
      }
    }

    /// <summary>Depth-first search (DFS), post-order (LRN). The trace of a traversal is called a sequentialisation of the tree. The traversal trace is a list of each visited root.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Tree_traversal#Post-order"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Depth-first_search"/>
    public static System.Collections.Generic.IEnumerable<Collections.Immutable.IBinarySearchTree<TKey, TValue>> GetNodesPostOrder<TKey, TValue>(this Collections.Immutable.IBinarySearchTree<TKey, TValue> source)
      where TKey : System.IComparable<TKey>
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      if (source.IsEmpty) yield break;

      var stack = new System.Collections.Generic.Stack<Collections.Immutable.IBinarySearchTree<TKey, TValue>>();

      var lastNodeVisited = default(Collections.Immutable.IBinarySearchTree<TKey, TValue>);

      var node = source;

      while (stack.Count > 0 || !node.IsEmpty)
      {
        if (node.IsEmpty)
        {
          var peekNode = stack.Peek();

          if (!peekNode.Right.IsEmpty && lastNodeVisited != peekNode.Right)
          {
            node = peekNode.Right;
          }
          else
          {
            yield return peekNode;

            lastNodeVisited = stack.Pop();
          }
        }
        else
        {
          stack.Push(node);

          node = node.Left;
        }
      }
    }

    /// <summary>Depth-first search (DFS), pre-order (NLR). The pre-order traversal is a topologically sorted one, because a parent node is processed before any of its child nodes is done.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Tree_traversal#Pre-order"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Depth-first_search"/>
    public static System.Collections.Generic.IEnumerable<Collections.Immutable.IBinarySearchTree<TKey, TValue>> GetNodesPreOrder<TKey, TValue>(this Collections.Immutable.IBinarySearchTree<TKey, TValue> source)
      where TKey : System.IComparable<TKey>
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      if (source.IsEmpty) yield break;

      var stack = new System.Collections.Generic.Stack<Collections.Immutable.IBinarySearchTree<TKey, TValue>>();

      stack.Push(source);

      while (stack.Count > 0)
      {
        var node = stack.Pop();

        yield return node;

        if (!node.Right.IsEmpty) stack.Push(node.Right);
        if (!node.Left.IsEmpty) stack.Push(node.Left);
      }
    }

    ///// <summary>Depth-first search (DFS), by Eric Lippert for immutable AVL tree enumeration.</summary>
    //public static System.Collections.Generic.IEnumerable<T> InOrder<T>(this Collections.Immutable.IBinaryTree<T> source)
    //{
    //  var stack = Collections.Immutable.Stack<Collections.Immutable.IBinaryTree<T>>.Empty;

    //  for (var current = source; !current.IsEmpty || !stack.IsEmpty; current = current.Right)
    //  {
    //    while (!current.IsEmpty)
    //    {
    //      stack = stack.Push(current);
    //      current = current.Left;
    //    }

    //    current = stack.Peek();
    //    stack = stack.Pop();

    //    yield return current.Value;
    //  }
    //}
  }

  namespace Collections.Immutable
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
}
