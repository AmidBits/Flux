namespace Flux
{
  public static partial class Em
  {
    /// <summary>
    /// <para>Computes the number of nodes in a <see cref="Flux.DataStructures.IBinaryTree{TValue}"/>.</para>
    /// </summary>
    public static int GetCount<TValue>(this DataStructures.BinaryTrees.IBinaryTree<TValue> source)
      => source.IsEmpty
      ? 0
      : source.Left.GetCount() + 1 + source.Right.GetCount();

    /// <summary>
    /// <para>Computes the diameter (or width) of a <see cref="Flux.DataStructures.IBinaryTree{TValue}"/> structure.</para>
    /// <see href="https://www.geeksforgeeks.org/diameter-of-a-binary-tree/"/>
    /// </summary>
    /// <remarks>The diameter (or width) is defined as the number of nodes on the longest path between two end nodes.</remarks>
    public static int GetDiameter<TValue>(this DataStructures.BinaryTrees.IBinaryTree<TValue> source)
    {
      var height = 0;

      return GetDiameter(source, ref height);

      static int GetDiameter(DataStructures.BinaryTrees.IBinaryTree<TValue> source, ref int height)
      {
        var leftHeight = 0; // Height of left subtree.
        var rightHeight = 0; // Height of right subtree.

        if (source.IsEmpty) return height = 0; // The diameter is also 0.

        var leftDiameter = GetDiameter(source.Left, ref leftHeight); // Get the leftHeight and store the returned leftDiameter.
        var rightDiameter = GetDiameter(source.Right, ref rightHeight); // Get the rightHeight and store the returned rightDiameter.

        height = int.Max(leftHeight, rightHeight) + 1; // Height of current node is max of heights of left and right subtrees plus 1.

        return int.Max(leftHeight + rightHeight + 1, int.Max(leftDiameter, rightDiameter));
      }
    }
    //=> source.IsEmpty
    //  ? 0
    //  : System.Math.Max(
    //      source.Left.GetMaxDepth() + 1 + source.Right.GetMaxDepth(),
    //      System.Math.Max(source.Left.GetDiameter(), source.Right.GetDiameter())
    //    );

    /// <summary>
    /// <para>Computes the max-depth (or height) of a <see cref="Flux.DataStructures.IBinaryTree{TValue}"/> structure.</para>
    /// <see href="https://www.geeksforgeeks.org/find-the-maximum-depth-or-height-of-a-tree/"/>
    /// </summary>
    /// <remarks>The max-depth (or height) of the tree is the number of vertices in the tree from the root to the deepest node.</remarks>
    public static int GetMaxDepth<TValue>(this DataStructures.BinaryTrees.IBinaryTree<TValue> source)
      => source.IsEmpty
      ? 0
      : 1 + System.Math.Max(
          source.Left.GetMaxDepth(),
          source.Right.GetMaxDepth()
        );

    /// <summary>
    /// <para>Computes the tree-sum of a <see cref="Flux.DataStructures.IBinaryTree{TValue}"/> structure.</para>
    /// <see href="https://www.geeksforgeeks.org/check-if-a-given-binary-tree-is-sumtree/"/>
    /// </summary>
    public static TSummable GetTreeSum<TValue, TSummable>(this DataStructures.BinaryTrees.IBinaryTree<TValue> source, System.Func<TValue, TSummable> summableSelector)
      where TSummable : System.Numerics.INumber<TSummable>
      => source.IsEmpty ? TSummable.Zero : source.Left.GetTreeSum(summableSelector) + summableSelector(source.Value) + source.Right.GetTreeSum(summableSelector);

    /// <summary>Indicates whether a <see cref="Flux.DataStructures.IBinaryTree{TValue}"/> is a binary-search-tree considering only its <typeparamref name="TValue"/> property. Returns false if <paramref name="source"/> or any sub-nodes violates the BST property (considering only <typeparamref name="TValue"/>).</summary>
    /// <param name="minValue">The minimum value for the type.</param>
    /// <param name="maxValue">The maximum value for the type.</param>
    /// <param name="valueDecrementor">Return the value decremented.</param>
    /// <param name="valueIncrementor">Return the value incremented.</param>
    public static bool IsBstByValue<TValue>(this DataStructures.BinaryTrees.IBinaryTree<TValue> source, TValue minValue, TValue maxValue, System.Func<TValue, TValue> valueDecrementor, System.Func<TValue, TValue> valueIncrementor)
      where TValue : System.IComparable<TValue>
    {
      System.ArgumentNullException.ThrowIfNull(source);
      System.ArgumentNullException.ThrowIfNull(valueDecrementor);
      System.ArgumentNullException.ThrowIfNull(valueIncrementor);

      if (source.IsEmpty) return true;
      if (source.Value.CompareTo(minValue) < 0 || source.Value.CompareTo(maxValue) > 0) return false;

      return IsBstByValue(source.Left, minValue, valueDecrementor(source.Value), valueDecrementor, valueIncrementor) && IsBstByValue(source.Right, valueIncrementor(source.Value), maxValue, valueDecrementor, valueIncrementor);
    }

    /// <summary>
    /// <para>Indicates whether a <see cref="Flux.DataStructures.IBinaryTree{TValue}"/> is a leaf.</para>
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static bool IsLeaf<TValue>(this DataStructures.BinaryTrees.IBinaryTree<TValue> source)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      return source.IsEmpty || (source.Left.IsEmpty && source.Right.IsEmpty);
    }

    /// <summary>
    /// <para>Indicates whether a <see cref="Flux.DataStructures.IBinaryTree{TValue}"/> is a sum-tree.</para>
    /// <see href="https://www.geeksforgeeks.org/check-if-a-given-binary-tree-is-sumtree/"/>
    /// </summary>
    /// <remarks>A sum-tree is a binary tree where the value of a node is equal to the sum of the nodes present in its left subtree and right subtree. An empty tree is sum-tree and the sum of an empty tree can be considered as 0. A leaf node is also considered as sum-tree.</remarks>
    public static bool IsSumTree<TValue, TNumber>(this DataStructures.BinaryTrees.IBinaryTree<TValue> source, System.Func<TValue, TNumber> numberSelector)
      where TNumber : System.Numerics.INumber<TNumber>
      => source.IsLeaf()
      || (
        (numberSelector(source.Value) == source.Left.GetTreeSum(numberSelector) + source.Right.GetTreeSum(numberSelector))
        && source.Left.IsSumTree(numberSelector)
        && source.Right.IsSumTree(numberSelector)
      );

    public static int Minimax<TValue>(this DataStructures.BinaryTrees.IBinaryTree<TValue> source, int depth, bool isMax, int maxHeight, System.Func<TValue, int> valueSelector)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      if (source.IsEmpty) throw new System.ArgumentException(source.GetType().Name);

      System.ArgumentNullException.ThrowIfNull(valueSelector);

      if (depth == maxHeight)
        return valueSelector(source.Value); // Terminating condition.

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
        return isMax ? System.Math.Max(left.Value, right.Value) : System.Math.Min(left.Value, right.Value);
      else if (left.HasValue)
        return left.Value;
      else if (right.HasValue)
        return right.Value;
      else // Neither has values.
        return valueSelector(source.Value);
    }

    public static SpanMaker<char> ToConsoleBlock<TValue>(this DataStructures.BinaryTrees.IBinaryTree<TValue> source)
    {
      const string padUpRight = "\u2514\u2500\u2500";
      const string padVerticalRight = "\u251C\u2500\u2500";
      const string padVertical = "\u2502  ";
      const string padSpaces = "   ";

      var sm = new SpanMaker<char>();
      TraversePreOrder(source, string.Empty, string.Empty, ref sm);
      return sm;

      void TraverseNodes(DataStructures.BinaryTrees.IBinaryTree<TValue> node, string padding, string pointer, bool hasRightSibling, ref SpanMaker<char> sm)
      {
        if (node.IsEmpty)
          return;

        sm.Append(1, padding);
        sm.Append(1, pointer);
        sm.Append(1, $"{node.Value}");
        sm.Append(1, System.Environment.NewLine);

        var paddingForBoth = padding + (hasRightSibling ? padVertical : padSpaces);

        TraverseNodes(node.Left, paddingForBoth, node.Right.IsEmpty ? padVerticalRight : padUpRight, !node.Right.IsEmpty, ref sm);
        TraverseNodes(node.Right, paddingForBoth, padUpRight, false, ref sm);
      }

      void TraversePreOrder(DataStructures.BinaryTrees.IBinaryTree<TValue> root, string padding, string pointer, ref SpanMaker<char> sm)
      {
        if (root.IsEmpty)
          return;

        sm.Append(1, $"{root.Value}");
        sm.Append(1, System.Environment.NewLine);

        TraverseNodes(root.Left, string.Empty, root.Right.IsEmpty ? padVerticalRight : padUpRight, !root.Right.IsEmpty, ref sm);
        TraverseNodes(root.Right, string.Empty, padUpRight, false, ref sm);
      }
    }

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
    public static System.Collections.Generic.IEnumerable<TValue> TraverseBfsLevelOrder<TValue>(this DataStructures.BinaryTrees.IBinaryTree<TValue> source, int maxDepth)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      if (source.IsEmpty) yield break;

      var level = new System.Collections.Generic.Queue<DataStructures.BinaryTrees.IBinaryTree<TValue>>();
      level.Enqueue(source);

      for (var depth = 0; level.Count > 0 && depth < maxDepth; depth++)
      {
        var nextLevel = new System.Collections.Generic.Queue<DataStructures.BinaryTrees.IBinaryTree<TValue>>();

        while (level.Count > 0)
        {
          var node = level.Dequeue();

          yield return node.Value;

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
    public static System.Collections.Generic.IEnumerable<TValue[]> TraverseBfsLevelOrderChunked<TValue>(this DataStructures.BinaryTrees.IBinaryTree<TValue> source)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      if (source.IsEmpty) yield break;

      var level = new System.Collections.Generic.Queue<DataStructures.BinaryTrees.IBinaryTree<TValue>>();
      level.Enqueue(source);

      while (level.Count > 0)
      {
        yield return level.Select(bt => bt.Value).ToArray();

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
    public static System.Collections.Generic.IEnumerable<TValue> TraverseDfsInOrder<TValue>(this DataStructures.BinaryTrees.IBinaryTree<TValue> source)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      var stack = new System.Collections.Generic.Stack<DataStructures.BinaryTrees.IBinaryTree<TValue>>();

      var node = source;

      while (stack.Count > 0 || !node.IsEmpty)
      {
        if (node.IsEmpty)
        {
          node = stack.Pop();

          yield return node.Value;

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
    public static System.Collections.Generic.IEnumerable<TValue> TraverseDfsInReverseOrder<TValue>(this DataStructures.BinaryTrees.IBinaryTree<TValue> source)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      var stack = new System.Collections.Generic.Stack<DataStructures.BinaryTrees.IBinaryTree<TValue>>();

      var node = source;

      while (stack.Count > 0 || !node.IsEmpty)
      {
        if (node.IsEmpty)
        {
          node = stack.Pop();

          yield return node.Value;

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
    public static System.Collections.Generic.IEnumerable<TValue> TraverseDfsPostOrder<TValue>(this DataStructures.BinaryTrees.IBinaryTree<TValue> source)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      if (source.IsEmpty) yield break;

      var stack = new System.Collections.Generic.Stack<DataStructures.BinaryTrees.IBinaryTree<TValue>>();

      var lastNodeVisited = default(DataStructures.BinaryTrees.IBinaryTree<TValue>);

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
            yield return peekNode.Value;

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
    public static System.Collections.Generic.IEnumerable<TValue> TraverseDfsPreOrder<TValue>(this DataStructures.BinaryTrees.IBinaryTree<TValue> source)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      if (source.IsEmpty) yield break;

      var stack = new System.Collections.Generic.Stack<DataStructures.BinaryTrees.IBinaryTree<TValue>>();

      stack.Push(source);

      while (stack.Count > 0)
      {
        var node = stack.Pop();

        yield return node.Value;

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
    public static System.Collections.Generic.IEnumerable<TValue> TraverseDiagonal<TValue>(this DataStructures.BinaryTrees.IBinaryTree<TValue> source)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      var queue = new System.Collections.Generic.Queue<DataStructures.BinaryTrees.IBinaryTree<TValue>>(); // The leftQueue will be a queue which will store all left pointers while traversing the tree, and will be utilized when at any point right pointer is empty.

      var node = source;

      while (!node.IsEmpty)
      {
        yield return node.Value;

        if (!node.Left.IsEmpty)
          queue.Enqueue(node.Left); // If left child available, add it to queue.

        if (!node.Right.IsEmpty)
          node = node.Right; // If right child, transfer the node to right.
        else // Right child is empty, so if queue (with lefties) is not empty, traverse it further, or else empty an it's done.
          node = (queue.Count > 0) ? queue.Dequeue() : node.Right;
      }
    }

    /// <summary>
    /// <para>Traverse nodes around the boundary (or perimeter) pattern (counter-clockwise) from a <see cref="Flux.DataStructures.IBinaryTree{TValue}"/>.</para>
    /// <see href="https://www.geeksforgeeks.org/boundary-traversal-of-binary-tree/"/>
    /// </summary>
    /// <remarks>This traversal does not necessarily return ALL nodes in the tree, only the boundary ones, or the ones along the perimeter of the tree.</remarks>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static System.Collections.Generic.IEnumerable<TValue> TraversePerimeter<TValue>(this DataStructures.BinaryTrees.IBinaryTree<TValue> source)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      if (source.IsEmpty) yield break;

      if (!IsLeaf(source))
        yield return source.Value; // if leaf then its done by addLeaves

      foreach (var bt in AddLeftBound(source))
        yield return bt.Value;

      foreach (var bt in AddLeaves(source))
        yield return bt.Value;

      foreach (var bt in AddRightBound(source))
        yield return bt.Value;

      static bool IsLeaf(DataStructures.BinaryTrees.IBinaryTree<TValue> node)
        => node.Left.IsEmpty && node.Right.IsEmpty;

      // Go left left until no left. Don't include leaf nodes (it leads to duplication).
      static System.Collections.Generic.IEnumerable<DataStructures.BinaryTrees.IBinaryTree<TValue>> AddLeftBound(DataStructures.BinaryTrees.IBinaryTree<TValue> root)
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
      static System.Collections.Generic.IEnumerable<DataStructures.BinaryTrees.IBinaryTree<TValue>> AddRightBound(DataStructures.BinaryTrees.IBinaryTree<TValue> root)
      {
        root = root.Right;

        var stack = new Stack<DataStructures.BinaryTrees.IBinaryTree<TValue>>(); // As we need the reverse of this for counter-clockwise.

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
      static System.Collections.Generic.IEnumerable<DataStructures.BinaryTrees.IBinaryTree<TValue>> AddLeaves(DataStructures.BinaryTrees.IBinaryTree<TValue> root)
      {
        if (root.IsEmpty)
          yield break;

        if (IsLeaf(root))
        {
          yield return root; // just store leaf nodes

          yield break;
        }

        foreach (var bt in AddLeaves(root.Left))
          yield return bt;

        foreach (var bt in AddLeaves(root.Right))
          yield return bt;
      }
    }

    /// <summary>
    /// <para>Traverse all nodes in the tree from top to bottom by level (left &lt;-> right), sort of like a snake.</para>
    /// <para><see href="https://www.geeksforgeeks.org/zigzag-tree-traversal/"/></para>
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static System.Collections.Generic.IEnumerable<TValue> TraverseZigZag<TValue>(this DataStructures.BinaryTrees.IBinaryTree<TValue> source)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      if (source.IsEmpty) yield break;

      var currentLevel = new Stack<DataStructures.BinaryTrees.IBinaryTree<TValue>>();
      var nextLevel = new Stack<DataStructures.BinaryTrees.IBinaryTree<TValue>>();

      currentLevel.Push(source);

      bool leftToRight = true;

      while (currentLevel.Count > 0)
      {
        var node = currentLevel.Pop();

        yield return node.Value;

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
          var temp = currentLevel;
          currentLevel = nextLevel;
          nextLevel = temp;
        }
      }
    }

    ///// <summary>Depth-first search (DFS), by Eric Lippert for immutable AVL tree enumeration.</summary>
    //public static System.Collections.Generic.IEnumerable<T> TraverseInOrderLippert<T>(this DataStructures.IBinaryTree<T> source)
    //{
    //  var stack = System.Collections.Immutable.ImmutableStack<DataStructures.IBinaryTree<T>>.Empty;

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
}
