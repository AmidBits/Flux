namespace Flux.DataStructure.Immutable
{
  /// <summary>
	/// <para>An immutable binary search tree (BST).</para>
  /// <para><see href="https://ericlippert.com/2008/01/18/immutability-in-c-part-eight-even-more-on-binary-trees/"/></para>
  /// <para><seealso href="https://en.wikipedia.org/wiki/Binary_search_tree"/></para>
  /// </summary>
  /// <typeparam name="TKey">The type of key of the immutable BST node. This is used to access the associated <typeparamref name="TValue"/>.</typeparam>
  /// <typeparam name="TValue">The type of value of the immutable BST node.</typeparam>
  /// <remarks>The original implementation courtesy Eric Lippert.</remarks>
  public sealed class ImmutableBinarySearchTree<TKey, TValue>
    : IBinarySearchTree<TKey, TValue>
    where TKey : System.IComparable<TKey>
  {
    public static IBinarySearchTree<TKey, TValue> Empty { get; } = new EmptyBinarySearchTree();

    private readonly TKey m_key;
    private readonly IBinarySearchTree<TKey, TValue> m_left;
    private readonly IBinarySearchTree<TKey, TValue> m_right;
    private readonly TValue m_value;

    private ImmutableBinarySearchTree(TKey key, TValue value, IBinarySearchTree<TKey, TValue> left, IBinarySearchTree<TKey, TValue> right)
    {
      m_key = key;
      m_left = left;
      m_right = right;
      m_value = value;
    }

    #region IBinaryTree<TValue>

    public bool IsEmpty => false;
    IBinaryTree<TValue> IBinaryTree<TValue>.Left => m_left;
    IBinaryTree<TValue> IBinaryTree<TValue>.Right => m_right;
    public TValue Value => m_value;

    #endregion // IBinaryTree<TValue>

    #region IBinarySearchTree<TKey, TValue>

    public TKey Key => m_key;
    public IBinarySearchTree<TKey, TValue> Left => m_left;
    public IBinarySearchTree<TKey, TValue> Right => m_right;

    public IBinarySearchTree<TKey, TValue> Add(TKey key, TValue value)
      => key.CompareTo(Key) > 0
      ? new ImmutableBinarySearchTree<TKey, TValue>(Key, Value, Left, Right.Add(key, value))
      : new ImmutableBinarySearchTree<TKey, TValue>(Key, Value, Left.Add(key, value), Right);

    public IBinarySearchTree<TKey, TValue> Remove(TKey key)
    {
      switch (key.CompareTo(Key))
      {
        case var lt when lt < 0:
          return new ImmutableBinarySearchTree<TKey, TValue>(Key, Value, Left.Remove(key), Right);
        case var gt when gt > 0:
          return new ImmutableBinarySearchTree<TKey, TValue>(Key, Value, Left, Right.Remove(key));
        default:
          if (Right.IsEmpty && Left.IsEmpty) return Empty; // If this is a leaf, just remove it by returning Empty.
          else if (Right.IsEmpty && !Left.IsEmpty) return Left; // If we have only a left child, replace the node with the child.
          else if (!Right.IsEmpty && Left.IsEmpty) return Right; // If we have only a right child, replace the node with the child.
          else // We have two children. Remove the next-highest node and replace this node with it.
          {
            var successor = Right;
            while (!successor.Left.IsEmpty) successor = successor.Left;
            return new ImmutableBinarySearchTree<TKey, TValue>(successor.Key, successor.Value, Left, Right.Remove(successor.Key));
          }
      }
    }

    public IBinarySearchTree<TKey, TValue> Search(TKey key)
      => IsEmpty
      ? Empty
      : (key.CompareTo(Key) switch { var gt when gt > 0 => Right.Search(key), var lt when lt < 0 => Left.Search(key), _ => this });

    #endregion // IBinarySearchTree<TKey, TValue>

    #region IMap<TKey, TValue>

    public System.Collections.Generic.IEnumerable<TKey> Keys => this.TraverseDfsInOrder().Select(t => t.Key);
    public System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>> Pairs => this.TraverseDfsInOrder();
    public System.Collections.Generic.IEnumerable<TValue> Values => this.TraverseDfsInOrder().Select(t => t.Value);
    IMap<TKey, TValue> IMap<TKey, TValue>.Add(TKey key, TValue value) => Add(key, value);
    public bool Contains(TKey key) => !Search(key).IsEmpty;
    public TValue Lookup(TKey key)
      => Search(key) is var tree && tree.IsEmpty
      ? throw new System.Exception(@"Key not found.")
      : tree.Value;
    IMap<TKey, TValue> IMap<TKey, TValue>.Remove(TKey key) => Remove(key);

    #endregion // IMap<TKey, TValue>

    public override string ToString() => $"{GetType().Name} {{ Left = {(Left.IsEmpty ? '-' : 'L')}, Right = {(Right.IsEmpty ? '-' : 'R')}, Key = {m_key}, Value = {m_value} }}";

    private sealed class EmptyBinarySearchTree
      : IBinarySearchTree<TKey, TValue>
    {
      #region IBinaryTree<TValue>

      public bool IsEmpty => true;
      IBinaryTree<TValue> IBinaryTree<TValue>.Left => throw new System.Exception(nameof(EmptyBinarySearchTree));
      IBinaryTree<TValue> IBinaryTree<TValue>.Right => throw new System.Exception(nameof(EmptyBinarySearchTree));
      public TValue Value => throw new System.Exception(nameof(EmptyBinarySearchTree));

      #endregion // IBinaryTree<TValue>

      #region IBinarySearchTree<TKey, TValue>

      public TKey Key => throw new System.Exception(nameof(EmptyBinarySearchTree));
      public IBinarySearchTree<TKey, TValue> Left => throw new System.Exception(nameof(EmptyBinarySearchTree));
      public IBinarySearchTree<TKey, TValue> Right => throw new System.Exception(nameof(EmptyBinarySearchTree));
      public IBinarySearchTree<TKey, TValue> Add(TKey key, TValue value) => new ImmutableBinarySearchTree<TKey, TValue>(key, value, this, this);
      public IBinarySearchTree<TKey, TValue> Remove(TKey key) => throw new System.Exception(nameof(EmptyBinarySearchTree));
      public IBinarySearchTree<TKey, TValue> Search(TKey key) => this;

      #endregion // IBinarySearchTree<TKey, TValue>

      #region IMap<TKey, TValue>

      public System.Collections.Generic.IEnumerable<TKey> Keys { get { yield break; } }
      public System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>> Pairs { get { yield break; } }
      public System.Collections.Generic.IEnumerable<TValue> Values { get { yield break; } }
      IMap<TKey, TValue> IMap<TKey, TValue>.Add(TKey key, TValue value) => Add(key, value);
      public bool Contains(TKey key) => false;
      public TValue Lookup(TKey key) => throw new System.Exception(nameof(EmptyBinarySearchTree));
      IMap<TKey, TValue> IMap<TKey, TValue>.Remove(TKey key) => Remove(key);

      #endregion // IMap<TKey, TValue>

      public override string ToString() => $"{GetType().Name}{System.Environment.NewLine}{this.ToConsoleBlock()}";
    }
  }
}
