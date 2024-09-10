namespace Flux.DataStructures.Immutable
{
  /// <summary>
  /// <para>The AVL tree (named after inventors Adelson-Velsky and Landis) is a self-balancing binary search tree.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/AVL_tree"/></para>
  /// <para><see href="https://ericlippert.com/2008/01/21/immutability-in-c-part-nine-academic-plus-my-avl-tree-implementation/"/></para>
  /// </summary>
  /// <typeparam name="TKey">The type of key for the immutable AVL tree node. This is used to access the associated <typeparamref name="TValue"/>.</typeparam>
  /// <typeparam name="TValue">The type of value for the immutable AVL tree node.</typeparam>
  /// <remarks>
  /// <para>The original implementation is courtesy Eric Lippert.</para>
  /// </remarks>
  public sealed class ImmutableAvlTree<TKey, TValue>
    : IBalancedBinarySearchTree<TKey, TValue>
    where TKey : System.IComparable<TKey>
  {
    public static readonly IBinarySearchTree<TKey, TValue> Empty = new EmptyAvlTree();

    private readonly int m_height;
    private readonly TKey m_key;
    private readonly IBinarySearchTree<TKey, TValue> m_left;
    private readonly IBinarySearchTree<TKey, TValue> m_right;
    private readonly TValue m_value;

    private ImmutableAvlTree(TKey key, TValue value, IBinarySearchTree<TKey, TValue> left, IBinarySearchTree<TKey, TValue> right)
    {
      m_key = key;
      m_value = value;
      m_left = left;
      m_right = right;
      m_height = 1 + System.Math.Max(Height(left), Height(right));
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
      => MakeBalanced(key.CompareTo(Key) > 0
        ? new ImmutableAvlTree<TKey, TValue>(Key, Value, Left, Right.Add(key, value))
        : new ImmutableAvlTree<TKey, TValue>(Key, Value, Left.Add(key, value), Right)
      );
    public IBinarySearchTree<TKey, TValue> Remove(TKey key)
      => MakeBalanced(key.CompareTo(Key) is var cmp && cmp < 0
        ? new ImmutableAvlTree<TKey, TValue>(Key, Value, Left.Remove(key), Right)
        : cmp > 0
        ? new ImmutableAvlTree<TKey, TValue>(Key, Value, Left, Right.Remove(key))
        : Right.IsEmpty && Left.IsEmpty
        ? Empty
        : Right.IsEmpty && !Left.IsEmpty
        ? Left
        : Left.IsEmpty && !Right.IsEmpty
        ? Right
        : this.GetSuccessorNode() is var successor && !successor.IsEmpty
        ? new ImmutableAvlTree<TKey, TValue>(successor.Key, successor.Value, Left, Right.Remove(successor.Key)) : Empty
      );
    public IBinarySearchTree<TKey, TValue> Search(TKey key)
      => (key.CompareTo(Key)) switch
      {
        var cmp when cmp > 0 => Right.Search(key),
        var cmp when cmp < 0 => Left.Search(key),
        _ => this,
      };

    #endregion // IBinarySearchTree<TKey, TValue>

    #region IMap<TKey, TValue>

    public System.Collections.Generic.IEnumerable<TKey> Keys => this.TraverseInOrder().Select(t => (IBinarySearchTree<TKey, TValue>)t).Select(t => t.Key);
    public System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>> Pairs => this.TraverseInOrder().Select(t => (IBinarySearchTree<TKey, TValue>)t).Select(t => new System.Collections.Generic.KeyValuePair<TKey, TValue>(t.Key, t.Value));
    public System.Collections.Generic.IEnumerable<TValue> Values => this.TraverseInOrder().Select(t => t.Value);

    IMap<TKey, TValue> IMap<TKey, TValue>.Add(TKey key, TValue value) => Add(key, value);
    public bool Contains(TKey key) => !Search(key).IsEmpty;
    public TValue Lookup(TKey key) => Search(key) is var tree && tree.IsEmpty ? throw new System.Exception("Not found.") : tree.Value;
    IMap<TKey, TValue> IMap<TKey, TValue>.Remove(TKey key) => Remove(key);

    #endregion // IMap<TKey, TValue>

    //private System.Collections.Generic.IEnumerable<IBinarySearchTree<TKey, TValue>> Enumerate()
    //{
    //  var stack = Stack<IBinarySearchTree<TKey, TValue>>.Empty;

    //  for (IBinarySearchTree<TKey, TValue> current = this; !current.IsEmpty || !stack.IsEmpty; current = current.Right)
    //  {
    //    while (!current.IsEmpty)
    //    {
    //      stack = stack.Push(current);
    //      current = current.Left;
    //    }

    //    current = stack.Peek();
    //    stack = stack.Pop();

    //    yield return current;
    //  }
    //}

    #region AVL Helpers

    private static int Balance(IBinarySearchTree<TKey, TValue> tree)
      => tree.IsEmpty ? 0 : Height(tree.Right) - Height(tree.Left);
    private static int Height(IBinarySearchTree<TKey, TValue> tree)
      => tree.IsEmpty ? 0 : ((ImmutableAvlTree<TKey, TValue>)tree).m_height;
    private static bool IsRightHeavy(IBinarySearchTree<TKey, TValue> tree)
      => Balance(tree) >= 2;
    private static bool IsLeftHeavy(IBinarySearchTree<TKey, TValue> tree)
      => Balance(tree) <= -2;
    private static IBinarySearchTree<TKey, TValue> MakeBalanced(IBinarySearchTree<TKey, TValue> tree)
      => IsRightHeavy(tree) ? IsLeftHeavy(tree.Right) ? RotateLeftDouble(tree) : RotateLeft(tree) : IsLeftHeavy(tree) ? IsRightHeavy(tree.Left) ? RotateRightDouble(tree) : RotateRight(tree) : tree;
    private static IBinarySearchTree<TKey, TValue> RotateLeft(IBinarySearchTree<TKey, TValue> tree)
      => tree.Right.IsEmpty ? tree : new ImmutableAvlTree<TKey, TValue>(tree.Right.Key, tree.Right.Value, new ImmutableAvlTree<TKey, TValue>(tree.Key, tree.Value, tree.Left, tree.Right.Left), tree.Right.Right);
    private static IBinarySearchTree<TKey, TValue> RotateLeftDouble(IBinarySearchTree<TKey, TValue> tree)
      => tree.Right.IsEmpty ? tree : RotateLeft(new ImmutableAvlTree<TKey, TValue>(tree.Key, tree.Value, tree.Left, RotateRight(tree.Right)));
    private static IBinarySearchTree<TKey, TValue> RotateRight(IBinarySearchTree<TKey, TValue> tree)
      => tree.Left.IsEmpty ? tree : new ImmutableAvlTree<TKey, TValue>(tree.Left.Key, tree.Left.Value, tree.Left.Left, new ImmutableAvlTree<TKey, TValue>(tree.Key, tree.Value, tree.Left.Right, tree.Right));
    private static IBinarySearchTree<TKey, TValue> RotateRightDouble(IBinarySearchTree<TKey, TValue> tree)
      => tree.Left.IsEmpty ? tree : RotateRight(new ImmutableAvlTree<TKey, TValue>(tree.Key, tree.Value, RotateLeft(tree.Left), tree.Right));

    #endregion // AVL Helpers

    public override string ToString() => $"{GetType().Name} {{ Left = {(Left.IsEmpty ? '-' : 'L')}, Right = {(Right.IsEmpty ? '-' : 'R')}, Key = {m_key}, Value = {m_value}, Height = {m_height} }}";

    private sealed class EmptyAvlTree
      : IBinarySearchTree<TKey, TValue>
    {
      #region IBinaryTree<TValue>

      public bool IsEmpty => true;
      IBinaryTree<TValue> IBinaryTree<TValue>.Left => throw new System.Exception(nameof(EmptyAvlTree));
      IBinaryTree<TValue> IBinaryTree<TValue>.Right => throw new System.Exception(nameof(EmptyAvlTree));
      public TValue Value => throw new System.Exception(nameof(EmptyAvlTree));

      #endregion // IBinaryTree<TValue>

      #region IBinarySearchTree<TKey, TValue>

      public TKey Key => throw new System.Exception(nameof(EmptyAvlTree));
      public IBinarySearchTree<TKey, TValue> Left => throw new System.Exception(nameof(EmptyAvlTree));
      public IBinarySearchTree<TKey, TValue> Right => throw new System.Exception(nameof(EmptyAvlTree));
      public IBinarySearchTree<TKey, TValue> Add(TKey key, TValue value) => new ImmutableAvlTree<TKey, TValue>(key, value, this, this);
      public IBinarySearchTree<TKey, TValue> Remove(TKey key) => throw new System.Exception(nameof(EmptyAvlTree));
      public IBinarySearchTree<TKey, TValue> Search(TKey key) => this;

      #endregion // IBinarySearchTree<TKey, TValue>

      #region IMap<TKey, TValue>

      public System.Collections.Generic.IEnumerable<TKey> Keys { get { yield break; } }
      public System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>> Pairs { get { yield break; } }
      public System.Collections.Generic.IEnumerable<TValue> Values { get { yield break; } }
      IMap<TKey, TValue> IMap<TKey, TValue>.Add(TKey key, TValue value) => Add(key, value);
      public bool Contains(TKey key) => false;
      public TValue Lookup(TKey key) => throw new System.Exception(nameof(EmptyAvlTree));
      IMap<TKey, TValue> IMap<TKey, TValue>.Remove(TKey key) => Remove(key);

      #endregion // IMap<TKey, TValue>

      public override string ToString() => $"{GetType().Name}{System.Environment.NewLine}{this.ToConsoleBlock()}";
    }
  }
}
