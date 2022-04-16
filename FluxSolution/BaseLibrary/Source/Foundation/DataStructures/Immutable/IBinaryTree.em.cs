namespace Flux
{
  public static partial class IBinaryTreeEm
  {
    public static int GetNodeCount<TValue>(this DataStructures.Immutable.IBinaryTree<TValue> source)
      => source?.IsEmpty ?? throw new System.ArgumentNullException(nameof(source)) ? 0 : 1 + GetNodeCount(source.Left) + GetNodeCount(source.Right);

    public static int Minimax<TValue>(this DataStructures.Immutable.IBinaryTree<TValue> source, int depth, bool isMax, int maxHeight, System.Func<TValue, int> valueSelector)
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

    public static System.Text.StringBuilder ToConsoleBlock<TValue>(this DataStructures.Immutable.IBinaryTree<TValue> source)
    {
      const string padUpRight = "\u2514\u2500\u2500";
      const string padVerticalRight = "\u251C\u2500\u2500";
      const string padVertical = "\u2502  ";
      const string padSpaces = "   ";

      var sb = new System.Text.StringBuilder();
      TraversePreOrder(source, string.Empty, string.Empty);
      return sb;

      void TraverseNodes(DataStructures.Immutable.IBinaryTree<TValue> node, string padding, string pointer, bool hasRightSibling)
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

      void TraversePreOrder(DataStructures.Immutable.IBinaryTree<TValue> root, string padding, string pointer)
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
