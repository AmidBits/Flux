namespace Flux.DataStructures.Immutable
{
  /// <summary>
  /// <para>An immutable binary tree.</para>
  /// <para><see href="https://ericlippert.com/2007/12/18/immutability-in-c-part-six-a-simple-binary-tree/"/></para>
  /// <para><seealso href="https://en.wikipedia.org/wiki/Binary_tree"/></para>
  /// </summary>
  /// <typeparam name="TValue">The type of value of the immutable binary tree node.</typeparam>
  /// <remarks>This implementation is courtesy Eric Lippert.</remarks>
  public sealed class ImmutableBinaryTree<TValue>
    : IBinaryTree<TValue>
    where TValue : System.IEquatable<TValue>
  {
    public static readonly IBinaryTree<TValue> Empty = new EmptyBinaryTree();

    private readonly IBinaryTree<TValue> m_left;
    private readonly IBinaryTree<TValue> m_right;
    private readonly TValue m_value;

    public ImmutableBinaryTree(TValue value, IBinaryTree<TValue> left, IBinaryTree<TValue> right)
    {
      m_left = left ?? Empty;
      m_right = right ?? Empty;
      m_value = value;
    }

    /// <summary>
    /// <para>Creates a new <see cref="Flux.DataStructures.IBinaryTree{TValue}"/> based on the <paramref name="inOrder"/> and <paramref name="levelOrder"/> arrays.</para>
    /// </summary>
    /// <param name="inOrder">The values from a binary-tree using in-order traversal.</param>
    /// <param name="levelOrder">The values from a binary-tree using level-order traversal.</param>
    /// <returns></returns>
    public static Flux.DataStructures.IBinaryTree<TValue> Create(TValue[] inOrder, TValue[] levelOrder)
    {
      return ConstructTree(ImmutableBinaryTree<TValue>.Empty, levelOrder, inOrder, 0, inOrder.Length - 1);

      static Flux.DataStructures.IBinaryTree<TValue> ConstructTree(Flux.DataStructures.IBinaryTree<TValue> startNode, TValue[] levelOrder, TValue[] inOrder, int inStart, int inEnd)
      {
        if (inStart > inEnd)
          return ImmutableBinaryTree<TValue>.Empty;

        if (inStart == inEnd)
          return new ImmutableBinaryTree<TValue>(inOrder[inStart], ImmutableBinaryTree<TValue>.Empty, ImmutableBinaryTree<TValue>.Empty);

        var found = false;
        var index = 0;

        for (var i = 0; i < levelOrder.Length - 1; i++) // It represents the index in inOrder array of element that appear first in levelOrder array.
        {
          var data = levelOrder[i];

          for (var j = inStart; j < inEnd; j++)
          {
            if (data.Equals(inOrder[j]))
            {
              startNode = new ImmutableBinaryTree<TValue>(data, ImmutableBinaryTree<TValue>.Empty, ImmutableBinaryTree<TValue>.Empty);
              index = j;
              found = true;
              break;
            }
          }

          if (found == true)
            break;
        }

        startNode = new ImmutableBinaryTree<TValue>(
          startNode.Value,
          ConstructTree(startNode, levelOrder, inOrder, inStart, index - 1), // Elements before index are part of left child of startNode.
          ConstructTree(startNode, levelOrder, inOrder, index + 1, inEnd) // Elements after index are part of right child of startNode.
        );

        return startNode;
      }
    }

    // IBinaryTree<TValue>
    public bool IsEmpty => false;
    public IBinaryTree<TValue> Left => m_left;
    public IBinaryTree<TValue> Right => m_right;
    public TValue Value => m_value;

    public override string ToString() => $"{GetType().Name} {{ Left = {(Left.IsEmpty ? '-' : 'L')}, Right = {(Right.IsEmpty ? '-' : 'R')}, Value = {m_value} }}";

    private sealed class EmptyBinaryTree : IBinaryTree<TValue>
    {
      // IBinaryTree<TValue>
      public bool IsEmpty => true;
      public IBinaryTree<TValue> Left => throw new System.Exception(nameof(EmptyBinaryTree));
      public IBinaryTree<TValue> Right => throw new System.Exception(nameof(EmptyBinaryTree));
      public TValue Value => throw new System.Exception(nameof(EmptyBinaryTree));

      public override string ToString() => $"{GetType().Name}{System.Environment.NewLine}{this.ToConsoleBlock()}";
    }
  }
}
