namespace Flux
{
  public static partial class XtendImmutable
  {
    public static int GetNodeCount<TValue>(this Collections.Immutable.IBinaryTree<TValue> source)
      => source?.IsEmpty ?? throw new System.ArgumentNullException(nameof(source)) ? 0 : 1 + GetNodeCount(source.Left) + GetNodeCount(source.Right);

    //public static int Minimax<TValue>(this Collections.Immutable.IBinaryTree<TValue> source, int depth, bool isMax, int maxHeight, System.Func<TValue, int> valueSelector)
    //{
    //  if (source is null) throw new System.ArgumentNullException(nameof(source));
    //  else if (source.IsEmpty) throw new System.ArgumentNullException(source.GetType().Name);

    //  if (depth == maxHeight) // Terminating condition.
    //    return valueSelector?.Invoke(source.Value) ?? throw new System.ArgumentNullException(nameof(valueSelector));

    //  if (isMax)
    //  {
    //    if (source.Left.IsEmpty && source.Right.IsEmpty) return valueSelector(source.Value);
    //    else if (!source.Left.IsEmpty && source.Right.IsEmpty && Minimax(source.Left, depth + 1, false, maxHeight, valueSelector) is var left)
    //      return left;
    //    else if (source.Left.IsEmpty && !source.Right.IsEmpty && Minimax(source.Right, depth + 1, false, maxHeight, valueSelector) is var right)
    //      return right;
    //    else if (!source.Left.IsEmpty && !source.Right.IsEmpty)
    //      return System.Math.Max(left, right);
    //  }

    //  return System.Math.Max(left.HasValue ? left.Value : right!.Value, right.HasValue ? right.Value : left!.Value);
    //}
    //  else
    //  {
    //    if (source.Left.IsEmpty && source.Right.IsEmpty) return valueSelector(source.Value);
    //    if ((!source.Left.IsEmpty && Minimax(source.Right, depth + 1, true, maxHeight, valueSelector) is var left || (!source.Right.IsEmpty && Minimax(source.Left, depth + 1, true, maxHeight, valueSelector) is var right)
    //      {

    //    }

    //    return System.Math.Min(left.HasValue? left.Value : right!.Value, right.HasValue? right.Value : left!.Value);
  }

  namespace Collections.Immutable
  {
    public interface IBinaryTree<TValue>
    {
      bool IsEmpty { get; }
      IBinaryTree<TValue> Left { get; }
      IBinaryTree<TValue> Right { get; }
      TValue Value { get; }
    }
  }
}
