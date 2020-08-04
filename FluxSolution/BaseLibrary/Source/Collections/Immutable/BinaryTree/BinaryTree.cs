namespace Flux.Collections.Immutable
{
  public sealed class BinaryTree<TValue>
    : IImmutableBinaryTree<TValue>
  {
    public static readonly IImmutableBinaryTree<TValue> Empty = new EmptyBinaryTree();

    private readonly IImmutableBinaryTree<TValue> m_left;
    private readonly IImmutableBinaryTree<TValue> m_right;
    private readonly TValue m_value;

    public BinaryTree(TValue value, IImmutableBinaryTree<TValue> left, IImmutableBinaryTree<TValue> right)
    {
      m_left = left ?? Empty;
      m_right = right ?? Empty;
      m_value = value;
    }

    #region IBinaryTree Implementation
    public bool IsEmpty
      => false;
    public IImmutableBinaryTree<TValue> Left
      => m_left;
    public IImmutableBinaryTree<TValue> Right
      => m_right;
    public TValue Value
      => m_value;
    #endregion IBinaryTree Implementation

    private sealed class EmptyBinaryTree : IImmutableBinaryTree<TValue>
    {
      #region IBinaryTree Implementation
      public bool IsEmpty
        => true;
      public IImmutableBinaryTree<TValue> Left
        => throw new System.Exception(nameof(EmptyBinaryTree));
      public IImmutableBinaryTree<TValue> Right
        => throw new System.Exception(nameof(EmptyBinaryTree));
      public TValue Value
        => throw new System.Exception(nameof(EmptyBinaryTree));
      #endregion IBinaryTree Implementation
    }
  }
}
