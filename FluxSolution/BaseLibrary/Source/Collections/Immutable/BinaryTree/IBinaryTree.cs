namespace Flux
{
  public static partial class XtensionsImmutable
  {
    public static int GetNodeCount<TValue>(this Collections.Immutable.IBinaryTree<TValue> source)
      => source?.IsEmpty ?? throw new System.ArgumentNullException(nameof(source)) ? 0 : 1 + source.Left.GetNodeCount() + source.Right.GetNodeCount();
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
