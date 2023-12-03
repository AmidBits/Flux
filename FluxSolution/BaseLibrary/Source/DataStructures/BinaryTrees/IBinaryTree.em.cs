namespace Flux
{
  public static partial class Em
  {
    public static int GetNodeCount<TValue>(this DataStructures.IBinaryTree<TValue> source)
      => source?.IsEmpty ?? throw new System.ArgumentNullException(nameof(source)) ? 0 : 1 + GetNodeCount(source.Left) + GetNodeCount(source.Right);

    /// <summary>Depth-first search (DFS), in-order (LNR). In a binary search tree, in-order traversal retrieves data in sorted order.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Tree_traversal#In-order"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Depth-first_search"/>
    public static System.Collections.Generic.IEnumerable<DataStructures.IBinaryTree<TValue>> GetNodesInOrder<TKey, TValue>(this DataStructures.IBinaryTree<TValue> source)
      where TKey : System.IComparable<TKey>
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var stack = new System.Collections.Generic.Stack<DataStructures.IBinaryTree<TValue>>();

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
    public static System.Collections.Generic.IEnumerable<DataStructures.IBinaryTree<TValue>> GetNodesInOrderReversed<TKey, TValue>(this DataStructures.IBinaryTree<TValue> source)
      where TKey : System.IComparable<TKey>
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var stack = new System.Collections.Generic.Stack<DataStructures.IBinaryTree<TValue>>();

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
    public static System.Collections.Generic.IEnumerable<DataStructures.IBinaryTree<TValue>> GetNodesLevelOrder<TKey, TValue>(this DataStructures.IBinaryTree<TValue> source, int maxDepth)
      where TKey : System.IComparable<TKey>
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      if (source.IsEmpty) yield break;

      var level = new System.Collections.Generic.Queue<DataStructures.IBinaryTree<TValue>>();
      level.Enqueue(source);

      for (var depth = 0; level.Count > 0 && depth < maxDepth; depth++)
      {
        var nextLevel = new System.Collections.Generic.Queue<DataStructures.IBinaryTree<TValue>>();

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
    public static System.Collections.Generic.IEnumerable<DataStructures.IBinaryTree<TValue>[]> GetNodesLevelOrderChunked<TKey, TValue>(this DataStructures.IBinaryTree<TValue> source)
      where TKey : System.IComparable<TKey>
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      if (source.IsEmpty) yield break;

      var level = new System.Collections.Generic.Queue<DataStructures.IBinaryTree<TValue>>();
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
    public static System.Collections.Generic.IEnumerable<DataStructures.IBinaryTree<TValue>> GetNodesPostOrder<TKey, TValue>(this DataStructures.IBinaryTree<TValue> source)
      where TKey : System.IComparable<TKey>
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      if (source.IsEmpty) yield break;

      var stack = new System.Collections.Generic.Stack<DataStructures.IBinaryTree<TValue>>();

      var lastNodeVisited = default(DataStructures.IBinaryTree<TValue>);

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
    public static System.Collections.Generic.IEnumerable<DataStructures.IBinaryTree<TValue>> GetNodesPreOrder<TKey, TValue>(this DataStructures.IBinaryTree<TValue> source)
      where TKey : System.IComparable<TKey>
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      if (source.IsEmpty) yield break;

      var stack = new System.Collections.Generic.Stack<DataStructures.IBinaryTree<TValue>>();

      stack.Push(source);

      while (stack.Count > 0)
      {
        var node = stack.Pop();

        yield return node;

        if (!node.Right.IsEmpty) stack.Push(node.Right);
        if (!node.Left.IsEmpty) stack.Push(node.Left);
      }
    }

    public static int Minimax<TValue>(this DataStructures.IBinaryTree<TValue> source, int depth, bool isMax, int maxHeight, System.Func<TValue, int> valueSelector)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      else if (source.IsEmpty) throw new System.ArgumentException(source.GetType().Name);

      if (valueSelector is null) throw new System.ArgumentNullException(nameof(valueSelector));

      if (depth == maxHeight)
        return valueSelector(source.Value); // Terminating condition.

      int? left = null, right = null;

      if (isMax)
      {
        if (!source.Left.IsEmpty)
          left = Minimax(source.Left, depth + 1, false, maxHeight, valueSelector);
        if (!source.Right.IsEmpty)
          right = Minimax(source.Right, depth + 1, false, maxHeight, valueSelector);
      }
      else
      {
        if (!source.Left.IsEmpty)
          left = Minimax(source.Left, depth + 1, true, maxHeight, valueSelector);
        if (!source.Right.IsEmpty)
          right = Minimax(source.Right, depth + 1, true, maxHeight, valueSelector);
      }

      if (left.HasValue && right.HasValue)
        return isMax ? System.Math.Max(left.Value, right.Value) : System.Math.Min(left.Value, right.Value);
      else if (left.HasValue)
        return left.Value;
      else if (right.HasValue)
        return right.Value;
      else // Neither has values.
        return valueSelector(source.Value);
    }

    public static System.Text.StringBuilder ToConsoleBlock<TValue>(this DataStructures.IBinaryTree<TValue> source)
    {
      const string padUpRight = "\u2514\u2500\u2500";
      const string padVerticalRight = "\u251C\u2500\u2500";
      const string padVertical = "\u2502  ";
      const string padSpaces = "   ";

      var sb = new System.Text.StringBuilder();
      TraversePreOrder(source, string.Empty, string.Empty);
      return sb;

      void TraverseNodes(DataStructures.IBinaryTree<TValue> node, string padding, string pointer, bool hasRightSibling)
      {
        if (node.IsEmpty)
          return;

        sb.Append(padding);
        sb.Append(pointer);
        sb.Append(node.Value);
        sb.Append(System.Environment.NewLine);

        var paddingForBoth = padding + (hasRightSibling ? padVertical : padSpaces);

        TraverseNodes(node.Left, paddingForBoth, node.Right.IsEmpty ? padVerticalRight : padUpRight, !node.Right.IsEmpty);
        TraverseNodes(node.Right, paddingForBoth, padUpRight, false);
      }

      void TraversePreOrder(DataStructures.IBinaryTree<TValue> root, string padding, string pointer)
      {
        if (root.IsEmpty)
          return;

        sb.Append(root.Value);
        sb.Append(System.Environment.NewLine);

        TraverseNodes(root.Left, string.Empty, root.Right.IsEmpty ? padVerticalRight : padUpRight, !root.Right.IsEmpty);
        TraverseNodes(root.Right, string.Empty, padUpRight, false);
      }
    }
  }
}
