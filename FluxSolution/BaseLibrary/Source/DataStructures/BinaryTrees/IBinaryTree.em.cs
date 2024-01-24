namespace Flux
{
  public static partial class Em
  {
    /// <summary>
    /// <para>Computes the number of nodes in the <paramref name="source"/> tree (including <paramref name="source"/>).</para>
    /// </summary>
    public static int GetTreeCount<TValue>(this DataStructures.IBinaryTree<TValue> source)
      => source.IsEmpty ? 0 : GetTreeCount(source.Left) + 1 + GetTreeCount(source.Right);

    /// <summary>
    /// <para>The diameter (or width) of a tree is defined as the number of nodes on the longest path between two end nodes.</para>
    /// <see href="https://www.geeksforgeeks.org/diameter-of-a-binary-tree/"/>
    /// </summary>
    public static int GetTreeDiameter<TValue>(this DataStructures.IBinaryTree<TValue> source)
    {
      if (source.IsEmpty)
        return 0;

      var lHeight = GetTreeHeight(source.Left);
      var rHeight = GetTreeHeight(source.Right);

      var lDiameter = GetTreeDiameter(source.Left);
      var rDiameter = GetTreeDiameter(source.Right);

      return System.Math.Max(lHeight + 1 + rHeight, System.Math.Max(lDiameter, rDiameter));
    }

    /// <summary>
    /// <para>Computes the "height" of the <paramref name="source"/> tree.</para>
    /// <see href="https://www.geeksforgeeks.org/find-the-maximum-depth-or-height-of-a-tree/"/>
    /// </summary>
    public static int GetTreeHeight<TValue>(this DataStructures.IBinaryTree<TValue> source)
      => source.IsEmpty ? 0 : System.Math.Max(GetTreeCount(source.Left), GetTreeCount(source.Right));

    /// <summary>
    /// <para>Finds the level of the node containing the <paramref name="value"/>.</para>
    /// <see href="https://www.geeksforgeeks.org/get-level-of-a-node-in-a-binary-tree/"/>
    /// </summary>
    public static int GetTreeLevel<TValue>(this DataStructures.IBinaryTree<TValue> node, TValue value, int level = 1)
      where TValue : System.IEquatable<TValue>
    {
      if (node.IsEmpty)
        return 0;

      if (node.Value.Equals(value))
        return level;

      var sublevel = GetTreeLevel(node.Left, value, level + 1);

      if (sublevel != 0)
        return sublevel;

      return GetTreeLevel(node.Right, value, level + 1);
    }

    /// <summary>
    /// <para></para>
    /// <see href="https://www.geeksforgeeks.org/check-if-a-given-binary-tree-is-sumtree/"/>
    /// </summary>
    public static TValue GetTreeSum<TValue>(this DataStructures.IBinaryTree<TValue> source)
      where TValue : System.Numerics.INumber<TValue>
      => source.IsEmpty ? TValue.Zero : source.Left.GetTreeSum() + source.Value + source.Right.GetTreeSum();

    /// <summary>
    /// <para>Returns whether the <paramref name="source"/> is a binary search tree. Returns false if the <paramref name="source"/> or any sub-nodes violates the BST property. The <paramref name="minValue"/> and <paramref name="maxValue"/> should be the minimum and maximum values possible.<</para>
    /// </summary>
    /// <param name="source">The tree to test.</param>
    /// <param name="minValue">The minimum value of <typeparamref name="TValue"/>.</param>
    /// <param name="maxValue">The maximum value of <typeparamref name="TValue"/>.</param>
    /// <returns></returns>
    public static bool IsBinarySearchTree<TValue>(this DataStructures.IBinaryTree<TValue> source)
      where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue>
    {
      System.ArgumentNullException.ThrowIfNull(source);

      if (source.IsEmpty)
        return true;

      if (!source.Left.IsEmpty && MaximumValue(source.Left) > source.Value)
        return false;

      if (!source.Right.IsEmpty && MinimumValue(source.Right) < source.Value)
        return false;

      if (!IsBinarySearchTree(source.Left) || !IsBinarySearchTree(source.Right))
        return false;

      return true;
    }

    /// <summary>Returns whether <paramref name="source"/> is a binary-search-tree considering only its <typeparamref name="TValue"/> property. Returns false if <paramref name="source"/> or any sub-nodes violates the BST property (considering only <typeparamref name="TValue"/>.</summary>
    /// <param name="minValue">The minimum value for the type.</param>
    /// <param name="maxValue">The maximum value for the type.</param>
    /// <param name="valueDecrementor">Return the value decremented.</param>
    /// <param name="valueIncrementor">Return the value incremented.</param>
    public static bool IsBstByValue<TValue>(this DataStructures.IBinaryTree<TValue> source, TValue minValue, TValue maxValue, System.Func<TValue, TValue> valueDecrementor, System.Func<TValue, TValue> valueIncrementor)
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
    /// <para></para>
    /// <see href="https://www.geeksforgeeks.org/check-if-a-given-binary-tree-is-sumtree/"/>
    /// </summary>
    public static bool IsSumTree<TValue>(this DataStructures.IBinaryTree<TValue> source)
      where TValue : System.Numerics.INumber<TValue>
      => source.IsEmpty || (source.Left.IsEmpty && source.Right.IsEmpty) || ((source.Value == source.Left.GetTreeSum() + source.Right.GetTreeSum()) && source.Left.IsSumTree() && source.Right.IsSumTree());

    public static TValue MaximumValue<TValue>(this DataStructures.IBinaryTree<TValue> source)
      where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumberBase<TValue>
    {
      if (source.IsEmpty)
        return TValue.MinValue;

      return TValue.MaxMagnitudeNumber(source.Value, TValue.MaxMagnitudeNumber(MaximumValue(source.Left), MaximumValue(source.Right)));
    }

    public static TValue MinimumValue<TValue>(this DataStructures.IBinaryTree<TValue> source)
      where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumberBase<TValue>
    {
      if (source.IsEmpty)
        return TValue.MaxValue;

      return TValue.MinMagnitudeNumber(source.Value, TValue.MinMagnitudeNumber(MinimumValue(source.Left), MinimumValue(source.Right)));
    }

    public static int Minimax<TValue>(this DataStructures.IBinaryTree<TValue> source, int depth, bool isMax, int maxHeight, System.Func<TValue, int> valueSelector)
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

    /// <summary>
    /// <para>Traverse all nodes in a boundary or perimeter pattern (counter-clockwise) starting at the <paramref name="source"/>.</para>
    /// <see href="https://www.geeksforgeeks.org/boundary-traversal-of-binary-tree/"/>
    /// </summary>
    /// // https://www.geeksforgeeks.org/tree-traversals-inorder-preorder-and-postorder/
    public static System.Collections.Generic.List<DataStructures.IBinaryTree<TValue>> TraverseBoundary<TValue>(this DataStructures.IBinaryTree<TValue> source)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      var ans = new List<DataStructures.IBinaryTree<TValue>>();

      if (source.IsEmpty) return ans;

      if (!isLeaf(source))
        ans.Add(source); // if leaf then its done by addLeaves

      addLeftBound(source, ans);
      addLeaves(source, ans);
      addRightBound(source, ans);

      return ans;

      static bool isLeaf(DataStructures.IBinaryTree<TValue> node)
        => node.Left.IsEmpty && node.Right.IsEmpty;

      static void addLeftBound(DataStructures.IBinaryTree<TValue> root, List<DataStructures.IBinaryTree<TValue>> ans)
      {
        // Go left left and no left then right but again check from left.
        root = root.Left;

        while (!root.IsEmpty)
        {
          if (!isLeaf(root))
          {
            ans.Add(root);
          }

          root = root.Left.IsEmpty ? root.Right : root.Left;
        }
      }

      static void addRightBound(DataStructures.IBinaryTree<TValue> root, List<DataStructures.IBinaryTree<TValue>> ans)
      {
        // Go right right and no right then left but again check from right.
        root = root.Right;

        // As we need the reverse of this for Anticlockwise
        var stk = new Stack<DataStructures.IBinaryTree<TValue>>();

        while (!root.IsEmpty)
        {
          if (!isLeaf(root))
          {
            stk.Push(root);
          }
          root = root.Right.IsEmpty ? root.Left : root.Right;
        }

        while (stk.Count > 0)
        {
          ans.Add(stk.Peek());
          stk.Pop();
        }
      }

      // its kind of predorder
      static void addLeaves(DataStructures.IBinaryTree<TValue> root, List<DataStructures.IBinaryTree<TValue>> ans)
      {
        if (root.IsEmpty) return;

        if (isLeaf(root))
        {
          ans.Add(root); // just store leaf nodes

          return;
        }

        addLeaves(root.Left, ans);
        addLeaves(root.Right, ans);
      }
    }

    /// <summary>
    /// <para>Traverse all nodes diagonally (left to right and top to bottom) starting at <paramref name="source"/>.</para>
    /// <see href="https://www.geeksforgeeks.org/diagonal-traversal-of-binary-tree/"/>
    /// </summary>
    public static System.Collections.Generic.List<DataStructures.IBinaryTree<TValue>> TraverseDiagonal<TValue>(this DataStructures.IBinaryTree<TValue> source)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      var diagonals = new System.Collections.Generic.List<DataStructures.IBinaryTree<TValue>>();

      var queue = new System.Collections.Generic.Queue<DataStructures.IBinaryTree<TValue>>(); // The leftQueue will be a queue which will store all left pointers while traversing the tree, and will be utilized when at any point right pointer is empty.

      var node = source;

      while (!node.IsEmpty)
      {
        diagonals.Add(node); // Add current node to output.

        if (!node.Left.IsEmpty)
          queue.Enqueue(node.Left); // If left child available, add it to queue.

        if (!node.Right.IsEmpty)
          node = node.Right; // If right child, transfer the node to right.
        else // Right child is empty, so if queue (with lefties) is not empty, traverse it further, or else empty an it's done.
          node = (queue.Count > 0) ? queue.Dequeue() : node.Right;
      }

      return diagonals;
    }

    /// <summary>Depth-first search (DFS), in-order (LNR). In a binary search tree, in-order traversal retrieves data in sorted order.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Tree_traversal#In-order"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Depth-first_search"/>
    public static System.Collections.Generic.IEnumerable<DataStructures.IBinaryTree<TValue>> TraverseInOrder<TValue>(this DataStructures.IBinaryTree<TValue> source)
    {
      System.ArgumentNullException.ThrowIfNull(source);

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
    /// <see href="https://en.wikipedia.org/wiki/Tree_traversal#In-order"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Depth-first_search"/>
    public static System.Collections.Generic.IEnumerable<DataStructures.IBinaryTree<TValue>> TraverseInReverseOrder<TValue>(this DataStructures.IBinaryTree<TValue> source)
    {
      System.ArgumentNullException.ThrowIfNull(source);

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
    /// <see href="https://en.wikipedia.org/wiki/Tree_traversal#Breadth-first_search_/_level_order"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Breadth-first_search"/>
    public static System.Collections.Generic.IEnumerable<DataStructures.IBinaryTree<TValue>> TraverseLevelOrder<TValue>(this DataStructures.IBinaryTree<TValue> source, int maxDepth)
    {
      System.ArgumentNullException.ThrowIfNull(source);

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
    /// <see href="https://en.wikipedia.org/wiki/Tree_traversal#Breadth-first_search_/_level_order"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Breadth-first_search"/>
    public static System.Collections.Generic.IEnumerable<DataStructures.IBinaryTree<TValue>[]> TraverseLevelOrderChunked<TValue>(this DataStructures.IBinaryTree<TValue> source)
    {
      System.ArgumentNullException.ThrowIfNull(source);

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
    /// <see href="https://en.wikipedia.org/wiki/Tree_traversal#Post-order"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Depth-first_search"/>
    public static System.Collections.Generic.IEnumerable<DataStructures.IBinaryTree<TValue>> TraversePostOrder<TValue>(this DataStructures.IBinaryTree<TValue> source)
    {
      System.ArgumentNullException.ThrowIfNull(source);

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
    /// <see href="https://en.wikipedia.org/wiki/Tree_traversal#Pre-order"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Depth-first_search"/>
    public static System.Collections.Generic.IEnumerable<DataStructures.IBinaryTree<TValue>> TraversePreOrder<TValue>(this DataStructures.IBinaryTree<TValue> source)
    {
      System.ArgumentNullException.ThrowIfNull(source);

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
