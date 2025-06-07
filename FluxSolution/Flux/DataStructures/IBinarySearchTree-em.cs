namespace Flux
{
  public static partial class Em
  {
    /// <summary>
    /// <para>Finds the level of the node containing the <paramref name="key"/>.</para>
    /// <see href="https://www.geeksforgeeks.org/get-level-of-a-node-in-a-binary-tree/"/>
    /// </summary>
    public static int GetLevelOf<TKey, TValue>(this DataStructures.IBinarySearchTree<TKey, TValue> source, TKey key)
      where TKey : System.IComparable<TKey>
    {
      return GetLevelOfKey(source, key, 1);

      int GetLevelOfKey(DataStructures.IBinarySearchTree<TKey, TValue> source, TKey key, int level = 1)
      {
        if (source.IsEmpty) return 0;

        if (source.Key.CompareTo(key) == 0)
          return level;

        var downLevel = GetLevelOfKey(source.Left, key, level + 1);

        if (downLevel == 0) downLevel = GetLevelOfKey(source.Right, key, level + 1);

        return downLevel;
      }
    }

    /// <summary>Gets the BST maximum node.</summary>
    public static DataStructures.IBinarySearchTree<TKey, TValue> GetMaximum<TKey, TValue>(this DataStructures.IBinarySearchTree<TKey, TValue> source)
      where TKey : System.IComparable<TKey>
    {
      System.ArgumentNullException.ThrowIfNull(source);

      if (source.IsEmpty) return source;

      var node = source;
      while (!node.Right.IsEmpty)
        node = node.Right;
      return node;
    }

    /// <summary>Gets the BST minimum node.</summary>
    public static DataStructures.IBinarySearchTree<TKey, TValue> GetMinimum<TKey, TValue>(this DataStructures.IBinarySearchTree<TKey, TValue> source)
      where TKey : System.IComparable<TKey>
    {
      System.ArgumentNullException.ThrowIfNull(source);

      if (source.IsEmpty) return source;

      var node = source;
      while (!node.Left.IsEmpty)
        node = node.Left;
      return node;
    }

    /// <summary>Gets the BST predecessor (or "previous") node.</summary>
    public static DataStructures.IBinarySearchTree<TKey, TValue> GetPredecessor<TKey, TValue>(this DataStructures.IBinarySearchTree<TKey, TValue> source)
      where TKey : System.IComparable<TKey>
    {
      System.ArgumentNullException.ThrowIfNull(source);

      return source.IsEmpty ? source : source.Left.GetMaximum();
    }

    /// <summary>Gets the BST successor (or "next") node.</summary>
    public static DataStructures.IBinarySearchTree<TKey, TValue> GetSuccessor<TKey, TValue>(this DataStructures.IBinarySearchTree<TKey, TValue> source)
      where TKey : System.IComparable<TKey>
    {
      System.ArgumentNullException.ThrowIfNull(source);

      return source.IsEmpty ? source : source.Right.GetMinimum();
    }

    /// <summary>
    /// <para>Computes the tree-sum of a <see cref="Flux.DataStructures.IBinaryTree{TValue}"/> structure.</para>
    /// <see href="https://www.geeksforgeeks.org/check-if-a-given-binary-tree-is-sumtree/"/>
    /// </summary>
    public static TSummable GetTreeSum<TKey, TValue, TSummable>(this DataStructures.IBinarySearchTree<TKey, TValue> source, System.Func<TKey, TValue, TSummable> summableSelector)
      where TKey : System.IComparable<TKey>
      where TSummable : System.Numerics.INumber<TSummable>
      => source.IsEmpty ? TSummable.Zero : source.Left.GetTreeSum(summableSelector) + summableSelector(source.Key, source.Value) + source.Right.GetTreeSum(summableSelector);

    /// <summary>
    /// <para>Returns whether the <paramref name="source"/> is a binary search tree. Returns false if the <paramref name="source"/> or any sub-nodes violates the BST property. The <paramref name="minValue"/> and <paramref name="maxValue"/> should be the minimum and maximum values possible.<</para>
    /// </summary>
    /// <param name="source">The tree to test.</param>
    /// <param name="minValue">The minimum value of <typeparamref name="TValue"/>.</param>
    /// <param name="maxValue">The maximum value of <typeparamref name="TValue"/>.</param>
    /// <returns></returns>
    public static bool IsBst<TKey, TValue>(this DataStructures.IBinarySearchTree<TKey, TValue> source)
      where TKey : System.IComparable<TKey>
    {
      System.ArgumentNullException.ThrowIfNull(source);

      if (source.IsEmpty)
        return true;

      if (!source.Left.IsEmpty && source.Left.GetMaximum().Key.CompareTo(source.Key) > 0)
        return false;

      if (!source.Right.IsEmpty && source.Right.GetMinimum().Key.CompareTo(source.Key) < 0)
        return false;

      return source.Left.IsBst() && source.Right.IsBst();
    }

    /// <summary>Returns whether <paramref name="source"/> is a binary-search-tree considering only its <typeparamref name="TKey"/> property. Returns false if <paramref name="source"/> or any sub-nodes violates the BST property (considering only <typeparamref name="TKey"/>).</summary>
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

    public static bool IsSumTree<TKey, TValue, TSummable>(this DataStructures.IBinarySearchTree<TKey, TValue> source, System.Func<TKey, TValue, TSummable> summableSelector)
      where TKey : System.IComparable<TKey>
      where TSummable : System.Numerics.INumber<TSummable>
    {
      if (source.IsLeaf())
        return true;

      if (source.Left.IsSumTree(summableSelector) && source.Right.IsSumTree(summableSelector))
      {
        TSummable sumLeft = source.Left.IsEmpty
          ? TSummable.Zero
          : source.Left.IsLeaf()
          ? summableSelector(source.Left.Key, source.Left.Value)
          : TSummable.CreateChecked(2) * summableSelector(source.Left.Key, source.Left.Value);

        TSummable sumRight = source.Right.IsEmpty
          ? TSummable.Zero
          : source.Right.IsLeaf()
          ? summableSelector(source.Right.Key, source.Right.Value)
          : TSummable.CreateChecked(2) * summableSelector(source.Right.Key, source.Right.Value);

        return summableSelector(source.Key, source.Value) == sumLeft + sumRight;
      }

      return false;
    }

    public static int Minimax<TKey, TValue>(this DataStructures.IBinarySearchTree<TKey, TValue> source, int depth, bool isMax, int maxHeight, System.Func<TKey, TValue, int> valueSelector)
      where TKey : System.IComparable<TKey>
    {
      System.ArgumentNullException.ThrowIfNull(source);

      if (source.IsEmpty) throw new System.ArgumentException(source.GetType().Name);

      System.ArgumentNullException.ThrowIfNull(valueSelector);

      if (depth == maxHeight)
        return valueSelector(source.Key, source.Value); // Terminating condition.

      int? left = null, right = null;

      if (isMax)
      {
        if (!source.Left.IsEmpty)
          left = source.Left.Minimax(depth + 1, false, maxHeight, valueSelector);
        if (!source.Right.IsEmpty)
          right = source.Right.Minimax(depth + 1, false, maxHeight, valueSelector);
      }
      else
      {
        if (!source.Left.IsEmpty)
          left = source.Left.Minimax(depth + 1, true, maxHeight, valueSelector);
        if (!source.Right.IsEmpty)
          right = source.Right.Minimax(depth + 1, true, maxHeight, valueSelector);
      }

      if (left.HasValue && right.HasValue)
        return isMax ? int.Max(left.Value, right.Value) : int.Min(left.Value, right.Value);
      else if (left.HasValue)
        return left.Value;
      else if (right.HasValue)
        return right.Value;
      else // Neither has values.
        return valueSelector(source.Key, source.Value);
    }

    //public static SpanMaker<char> ToConsoleBlock<TKey, TValue>(this DataStructures.IBinarySearchTree<TKey, TValue> source)
    //  where TKey : System.IComparable<TKey>
    //{
    //  const string padUpRight = "\u2514\u2500\u2500";
    //  const string padVerticalRight = "\u251C\u2500\u2500";
    //  const string padVertical = "\u2502  ";
    //  const string padSpaces = "   ";

    //  var sm = new SpanMaker<char>();
    //  TraversePreOrder(source, string.Empty, string.Empty, ref sm);
    //  return sm;

    //  void TraverseNodes(DataStructures.IBinaryTree<TValue> node, string padding, string pointer, bool hasRightSibling, ref SpanMaker<char> sm)
    //  {
    //    if (node.IsEmpty)
    //      return;

    //    sm.Append(1, padding);
    //    sm.Append(1, pointer);
    //    sm.Append(1, $"{node.Value}");
    //    sm.Append(1, System.Environment.NewLine);

    //    var paddingForBoth = padding + (hasRightSibling ? padVertical : padSpaces);

    //    TraverseNodes(node.Left, paddingForBoth, node.Right.IsEmpty ? padVerticalRight : padUpRight, !node.Right.IsEmpty, ref sm);
    //    TraverseNodes(node.Right, paddingForBoth, padUpRight, false, ref sm);
    //  }

    //  void TraversePreOrder(DataStructures.IBinaryTree<TValue> root, string padding, string pointer, ref SpanMaker<char> sm)
    //  {
    //    if (root.IsEmpty)
    //      return;

    //    sm.Append(1, $"{root.Value}");
    //    sm.Append(1, System.Environment.NewLine);

    //    TraverseNodes(root.Left, string.Empty, root.Right.IsEmpty ? padVerticalRight : padUpRight, !root.Right.IsEmpty, ref sm);
    //    TraverseNodes(root.Right, string.Empty, padUpRight, false, ref sm);
    //  }
    //}

    /// <summary>
    /// <para>Breadth-first search (BFS), level order. Traversal yields the binary tree levels starting with the root, then its two possible children, then their children, and so on "in generations".</para>
    /// <para>Contrasting with depth-first order is breadth-first order, which always attempts to visit the node closest to the root that it has not already visited. See breadth-first search for more information. Also called a level-order traversal.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Tree_traversal#Breadth-first_search_/_level_order"/></para>
    /// <para><seealso href="https://en.wikipedia.org/wiki/Breadth-first_search"/></para>
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="source"></param>
    /// <param name="maxDepth"></param>
    /// <returns></returns>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>> TraverseBfsLevelOrder<TKey, TValue>(this DataStructures.IBinarySearchTree<TKey, TValue> source, int maxDepth)
      where TKey : System.IComparable<TKey>
    {
      System.ArgumentNullException.ThrowIfNull(source);

      if (source.IsEmpty) yield break;

      var level = new System.Collections.Generic.Queue<DataStructures.IBinarySearchTree<TKey, TValue>>();
      level.Enqueue(source);

      for (var depth = 0; level.Count > 0 && depth < maxDepth; depth++)
      {
        var nextLevel = new System.Collections.Generic.Queue<DataStructures.IBinarySearchTree<TKey, TValue>>();

        while (level.Count > 0)
        {
          var node = level.Dequeue();

          yield return new(node.Key, node.Value);

          if (!node.Left.IsEmpty) nextLevel.Enqueue(node.Left);
          if (!node.Right.IsEmpty) nextLevel.Enqueue(node.Right);
        }

        level = nextLevel;
      }
    }

    /// <summary>
    /// <para>Breadth-first search (BFS), chunked level order. Traversal yields the binary tree levels, in chunks, starting with the root, then its two children, then their children, and so on "in generations".</para>
    /// <para>Contrasting with depth-first order is breadth-first order, which always attempts to visit the node closest to the root that it has not already visited. See breadth-first search for more information. Also called a level-order traversal.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Tree_traversal#Breadth-first_search_/_level_order"/></para>
    /// <para><seealso href="https://en.wikipedia.org/wiki/Breadth-first_search"/></para>
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>[]> TraverseBfsLevelOrderChunked<TKey, TValue>(this DataStructures.IBinarySearchTree<TKey, TValue> source)
      where TKey : System.IComparable<TKey>
    {
      System.ArgumentNullException.ThrowIfNull(source);

      if (source.IsEmpty) yield break;

      var level = new System.Collections.Generic.Queue<DataStructures.IBinarySearchTree<TKey, TValue>>();
      level.Enqueue(source);

      while (level.Count > 0)
      {
        yield return level.Select(q => new System.Collections.Generic.KeyValuePair<TKey, TValue>(q.Key, q.Value)).ToArray();

        for (var index = level.Count; index > 0; index--)
        {
          var node = level.Dequeue();

          if (!node.Left.IsEmpty) level.Enqueue(node.Left);
          if (!node.Right.IsEmpty) level.Enqueue(node.Right);
        }
      }
    }

    /// <summary>
    /// <para>Depth-first search (DFS), in-order (LNR). For a binary-search-tree the in-order traversal retrieves data in sorted order.</para>
    /// <para>In in-order, we always recursively traverse the current node's left subtree; next, we visit the current node, and lastly, we recursively traverse the current node's right subtree.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Tree_traversal#In-order"/></para>
    /// <para><seealso href="https://en.wikipedia.org/wiki/Depth-first_search"/></para>
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>> TraverseDfsInOrder<TKey, TValue>(this DataStructures.IBinarySearchTree<TKey, TValue> source)
      where TKey : System.IComparable<TKey>
    {
      System.ArgumentNullException.ThrowIfNull(source);

      if (source.IsEmpty) yield break;

      var stack = new System.Collections.Generic.Stack<DataStructures.IBinarySearchTree<TKey, TValue>>();

      var node = source;

      while (stack.Count > 0 || !node.IsEmpty)
      {
        if (node.IsEmpty)
        {
          node = stack.Pop();

          yield return new(node.Key, node.Value);

          node = node.Right;
        }
        else
        {
          stack.Push(node);

          node = node.Left;
        }
      }
    }

    /// <summary>
    /// <para>Depth-first search (DFS), in-order reverse (RNL). In a binary search tree, in-order traversal retrieves data in sorted order. This is in-order reversed.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Tree_traversal#In-order"/></para>
    /// <para><seealso href="https://en.wikipedia.org/wiki/Depth-first_search"/></para>
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>> TraverseDfsInReverseOrder<TKey, TValue>(this DataStructures.IBinarySearchTree<TKey, TValue> source)
      where TKey : System.IComparable<TKey>
    {
      System.ArgumentNullException.ThrowIfNull(source);

      if (source.IsEmpty) yield break;

      var stack = new System.Collections.Generic.Stack<DataStructures.IBinarySearchTree<TKey, TValue>>();

      var node = source;

      while (stack.Count > 0 || !node.IsEmpty)
      {
        if (node.IsEmpty)
        {
          node = stack.Pop();

          yield return new(node.Key, node.Value);

          node = node.Left;
        }
        else
        {
          stack.Push(node);

          node = node.Right;
        }
      }
    }

    /// <summary>
    /// <para>Depth-first search (DFS), post-order (LRN). The trace of a traversal is called a sequentialisation of the tree. The traversal trace is a list of each visited root.</para>
    /// <para>In post-order, we always recursively traverse the current node's left subtree; next, we recursively traverse the current node's right subtree and then visit the current node. Post-order traversal can be useful to get postfix expression of a binary expression tree.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Tree_traversal#Post-order"/></para>
    /// <para><seealso href="https://en.wikipedia.org/wiki/Depth-first_search"/></para>
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>> TraverseDfsPostOrder<TKey, TValue>(this DataStructures.IBinarySearchTree<TKey, TValue> source)
      where TKey : System.IComparable<TKey>
    {
      System.ArgumentNullException.ThrowIfNull(source);

      if (source.IsEmpty) yield break;

      var stack = new System.Collections.Generic.Stack<DataStructures.IBinarySearchTree<TKey, TValue>>();

      var lastNodeVisited = default(DataStructures.IBinarySearchTree<TKey, TValue>);

      var node = source;

      while (stack.Count > 0 || !node.IsEmpty)
      {
        if (node.IsEmpty)
        {
          var peek = stack.Peek();

          if (!peek.Right.IsEmpty && lastNodeVisited != peek.Right)
          {
            node = peek.Right;
          }
          else
          {
            yield return new(peek.Key, peek.Value);

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

    /// <summary>
    /// <para>Depth-first search (DFS), pre-order (NLR). The pre-order traversal is a topologically sorted one, because a parent node is processed before any of its child nodes is done.</para>
    /// <para>In pre-order, we always visit the current node; next, we recursively traverse the current node's left subtree, and then we recursively traverse the current node's right subtree. The pre-order traversal is a topologically sorted one, because a parent node is processed before any of its child nodes is done.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Tree_traversal#Pre-order"/></para>
    /// <para><seealso href="https://en.wikipedia.org/wiki/Depth-first_search"/></para>
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>> TraverseDfsPreOrder<TKey, TValue>(this DataStructures.IBinarySearchTree<TKey, TValue> source)
      where TKey : System.IComparable<TKey>
    {
      System.ArgumentNullException.ThrowIfNull(source);

      if (source.IsEmpty) yield break;

      var stack = new System.Collections.Generic.Stack<DataStructures.IBinarySearchTree<TKey, TValue>>();

      stack.Push(source);

      while (stack.Count > 0)
      {
        var node = stack.Pop();

        yield return new(node.Key, node.Value);

        if (!node.Right.IsEmpty) stack.Push(node.Right);
        if (!node.Left.IsEmpty) stack.Push(node.Left);
      }
    }

    /// <summary>
    /// <para>Traverse all nodes diagonally (left to right and top to bottom) from a <see cref="Flux.DataStructures.IBinaryTree{TValue}"/>.</para>
    /// <see href="https://www.geeksforgeeks.org/diagonal-traversal-of-binary-tree/"/>
    /// </summary>
    /// <remarks>The <paramref name="source"/> and <paramref name="source"/>.Right nodes are prioritized over <paramref name="source"/>.Left values.</remarks>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>> TraverseDiagonal<TKey, TValue>(this DataStructures.IBinarySearchTree<TKey, TValue> source)
      where TKey : System.IComparable<TKey>
    {
      System.ArgumentNullException.ThrowIfNull(source);

      var queue = new System.Collections.Generic.Queue<DataStructures.IBinarySearchTree<TKey, TValue>>(); // The leftQueue will be a queue which will store all left pointers while traversing the tree, and will be utilized when at any point right pointer is empty.

      var node = source;

      while (!node.IsEmpty)
      {
        yield return new(node.Key, node.Value);

        if (!node.Left.IsEmpty)
          queue.Enqueue(node.Left); // If left child available, add it to queue.

        if (!node.Right.IsEmpty)
          node = node.Right; // If right child, transfer the node to right.
        else // Right child is empty, so if queue (with lefties) is not empty, traverse it further, or else empty an it's done.
          node = (queue.Count > 0) ? queue.Dequeue() : node.Right;
      }
    }

    /// <summary>
    /// <para>Traverse nodes around the perimeter (counter-clockwise) from a <see cref="Flux.DataStructures.IBinaryTree{TValue}"/>.</para>
    /// <see href="https://www.geeksforgeeks.org/boundary-traversal-of-binary-tree/"/>
    /// </summary>
    /// <remarks>This traversal does not necessarily return ALL nodes in the tree, only the ones along the perimeter of the tree outline.</remarks>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>> TraversePerimeter<TKey, TValue>(this DataStructures.IBinarySearchTree<TKey, TValue> source)
      where TKey : System.IComparable<TKey>
    {
      System.ArgumentNullException.ThrowIfNull(source);

      if (source.IsEmpty) yield break;

      if (!IsLeaf(source)) // If leaf then its done by addLeaves.
        yield return new(source.Key, source.Value);

      foreach (var bst in AddLeftBound(source))
        yield return new(bst.Key, bst.Value);

      foreach (var bst in AddLeaves(source))
        yield return new(bst.Key, bst.Value);

      foreach (var bst in AddRightBound(source))
        yield return new(bst.Key, bst.Value);

      static bool IsLeaf(DataStructures.IBinaryTree<TValue> node)
        => node.Left.IsEmpty && node.Right.IsEmpty;

      // Go left left until no left. Don't include leaf nodes (it leads to duplication).
      static System.Collections.Generic.IEnumerable<DataStructures.IBinarySearchTree<TKey, TValue>> AddLeftBound(DataStructures.IBinarySearchTree<TKey, TValue> root)
      {
        root = root.Left;

        while (!root.IsEmpty)
        {
          if (!IsLeaf(root))
            yield return root;

          root = root.Left.IsEmpty ? root.Right : root.Left;
        }
      }

      // Go right right until no right. Don't include leaf nodes (it leads to duplication).
      static System.Collections.Generic.IEnumerable<DataStructures.IBinarySearchTree<TKey, TValue>> AddRightBound(DataStructures.IBinarySearchTree<TKey, TValue> root)
      {
        root = root.Right;

        var stack = new Stack<DataStructures.IBinarySearchTree<TKey, TValue>>(); // As we need the reverse of this for counter-clockwise.

        while (!root.IsEmpty)
        {
          if (!IsLeaf(root))
            stack.Push(root);

          root = root.Right.IsEmpty ? root.Left : root.Right;
        }

        while (stack.Count > 0)
        {
          yield return stack.Peek();

          stack.Pop();
        }
      }

      // Do inorder/preorder, if leaf node add to the list.
      static System.Collections.Generic.IEnumerable<DataStructures.IBinarySearchTree<TKey, TValue>> AddLeaves(DataStructures.IBinarySearchTree<TKey, TValue> root)
      {
        if (root.IsEmpty)
          yield break;

        if (IsLeaf(root))
        {
          yield return root; // just store leaf nodes

          yield break;
        }

        foreach (var bst in AddLeaves(root.Left))
          yield return bst;

        foreach (var bst in AddLeaves(root.Right))
          yield return bst;
      }
    }

    /// <summary>
    /// <para>Traverse all nodes in the tree from top to bottom by level (left &lt;-> right), sort of like a snake.</para>
    /// <para><see href="https://www.geeksforgeeks.org/zigzag-tree-traversal/"/></para>
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>> TraverseZigZag<TKey, TValue>(this DataStructures.IBinarySearchTree<TKey, TValue> source)
      where TKey : System.IComparable<TKey>
    {
      System.ArgumentNullException.ThrowIfNull(source);

      if (source.IsEmpty) yield break;

      var currentLevel = new Stack<DataStructures.IBinarySearchTree<TKey, TValue>>();
      var nextLevel = new Stack<DataStructures.IBinarySearchTree<TKey, TValue>>();

      currentLevel.Push(source);

      bool leftToRight = true;

      while (currentLevel.Count > 0)
      {
        var node = currentLevel.Pop();

        yield return new(node.Key, node.Value);

        if (leftToRight)
        {
          if (!node.Left.IsEmpty) nextLevel.Push(node.Left);
          if (!node.Right.IsEmpty) nextLevel.Push(node.Right);
        }
        else
        {
          if (!node.Right.IsEmpty) nextLevel.Push(node.Right);
          if (!node.Left.IsEmpty) nextLevel.Push(node.Left);
        }

        if (currentLevel.Count == 0)
        {
          leftToRight = !leftToRight;

          (nextLevel, currentLevel) = (currentLevel, nextLevel);
        }
      }
    }
  }
}
