namespace Flux
{
  public static partial class XtensionsImmutable
  {
    public static int GetNodeCount<TValue>(this Collections.Immutable.IImmutableBinaryTree<TValue> source)
      => source?.IsEmpty ?? throw new System.ArgumentNullException(nameof(source)) ? 0 : 1 + source.Left.GetNodeCount() + source.Right.GetNodeCount();
  }

  namespace Collections.Immutable
  {
    public interface IImmutableBinaryTree<TValue>
    {
      bool IsEmpty { get; }
      IImmutableBinaryTree<TValue> Left { get; }
      IImmutableBinaryTree<TValue> Right { get; }
      TValue Value { get; }
    }
  }
}
