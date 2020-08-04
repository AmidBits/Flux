using System.Linq;

namespace Flux.Collections.Immutable
{
  public sealed class AvlTree<TKey, TValue>
    : IBinarySearchTree<TKey, TValue>
    where TKey : System.IComparable<TKey>
  {
    public static readonly IBinarySearchTree<TKey, TValue> Empty = new EmptyAvlTree();

    private readonly int m_height;
    private readonly TKey m_key;
    private readonly IBinarySearchTree<TKey, TValue> m_left;
    private readonly IBinarySearchTree<TKey, TValue> m_right;
    private readonly TValue m_value;

    private AvlTree(TKey key, TValue value, IBinarySearchTree<TKey, TValue> left, IBinarySearchTree<TKey, TValue> right)
    {
      m_key = key;
      m_value = value;
      m_left = left;
      m_right = right;
      m_height = 1 + System.Math.Max(Height(left), Height(right));
    }

    #region IBinaryTree Implementation
    public bool IsEmpty
      => false;
    IImmutableBinaryTree<TValue> IImmutableBinaryTree<TValue>.Left
      => m_left;
    IImmutableBinaryTree<TValue> IImmutableBinaryTree<TValue>.Right
      => m_right;
    public TValue Value
      => m_value;
    #endregion IBinaryTree Implementation

    #region IBinarySearchTree Implementation
    public TKey Key
      => m_key;
    public IBinarySearchTree<TKey, TValue> Left
      => m_left;
    public IBinarySearchTree<TKey, TValue> Right
      => m_right;
    public IBinarySearchTree<TKey, TValue> Add(TKey key, TValue value)
      => MakeBalanced(key.CompareTo(Key) > 0 ? new AvlTree<TKey, TValue>(Key, Value, Left, Right.Add(key, value)) : new AvlTree<TKey, TValue>(Key, Value, Left.Add(key, value), Right));
    public IBinarySearchTree<TKey, TValue> Remove(TKey key)
      => MakeBalanced(key.CompareTo(Key) is var cmp && cmp < 0 ? new AvlTree<TKey, TValue>(Key, Value, Left.Remove(key), Right) : cmp > 0 ? new AvlTree<TKey, TValue>(Key, Value, Left, Right.Remove(key)) : Right.IsEmpty && Left.IsEmpty ? Empty : Right.IsEmpty && !Left.IsEmpty ? Left : Left.IsEmpty && !Right.IsEmpty ? Right : this.GetSuccessorNode() is var successor && !successor.IsEmpty ? new AvlTree<TKey, TValue>(successor.Key, successor.Value, Left, Right.Remove(successor.Key)) : Empty);
    public IBinarySearchTree<TKey, TValue> Search(TKey key)
      => (key.CompareTo(Key)) switch { var cmp when cmp > 0 => Right.Search(key), var cmp when cmp < 0 => Left.Search(key), _ => this, };
    #endregion IBinarySearchTree Implementation

    #region IMap Implementation
    public System.Collections.Generic.IEnumerable<TKey> Keys
      => this.GetNodesInOrder().Select(t => t.Key);
    public System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>> Pairs
      => this.GetNodesInOrder().Select(t => new System.Collections.Generic.KeyValuePair<TKey, TValue>(t.Key, t.Value));
    public System.Collections.Generic.IEnumerable<TValue> Values
      => this.GetNodesInOrder().Select(t => t.Value);
    IMap<TKey, TValue> IMap<TKey, TValue>.Add(TKey key, TValue value)
      => Add(key, value);
    public bool Contains(TKey key)
      => !Search(key).IsEmpty;
    public TValue Lookup(TKey key)
      => Search(key) is var tree && tree.IsEmpty ? throw new System.Exception("not found") : tree.Value;
    IMap<TKey, TValue> IMap<TKey, TValue>.Remove(TKey key)
      => Remove(key);
    #endregion IMap Implementation

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

    #region Static Helpers
    private static int Balance(IBinarySearchTree<TKey, TValue> tree)
      => tree.IsEmpty ? 0 : Height(tree.Right) - Height(tree.Left);
    private static int Height(IBinarySearchTree<TKey, TValue> tree)
      => tree.IsEmpty ? 0 : ((AvlTree<TKey, TValue>)tree).m_height;
    private static bool IsRightHeavy(IBinarySearchTree<TKey, TValue> tree)
      => Balance(tree) >= 2;
    private static bool IsLeftHeavy(IBinarySearchTree<TKey, TValue> tree)
      => Balance(tree) <= -2;
    private static IBinarySearchTree<TKey, TValue> MakeBalanced(IBinarySearchTree<TKey, TValue> tree)
      => IsRightHeavy(tree) ? IsLeftHeavy(tree.Right) ? RotateLeftDouble(tree) : RotateLeft(tree) : IsLeftHeavy(tree) ? IsRightHeavy(tree.Left) ? RotateRightDouble(tree) : RotateRight(tree) : tree;
    private static IBinarySearchTree<TKey, TValue> RotateLeft(IBinarySearchTree<TKey, TValue> tree)
      => tree.Right.IsEmpty ? tree : new AvlTree<TKey, TValue>(tree.Right.Key, tree.Right.Value, new AvlTree<TKey, TValue>(tree.Key, tree.Value, tree.Left, tree.Right.Left), tree.Right.Right);
    private static IBinarySearchTree<TKey, TValue> RotateLeftDouble(IBinarySearchTree<TKey, TValue> tree)
      => tree.Right.IsEmpty ? tree : RotateLeft(new AvlTree<TKey, TValue>(tree.Key, tree.Value, tree.Left, RotateRight(tree.Right)));
    private static IBinarySearchTree<TKey, TValue> RotateRight(IBinarySearchTree<TKey, TValue> tree)
      => tree.Left.IsEmpty ? tree : new AvlTree<TKey, TValue>(tree.Left.Key, tree.Left.Value, tree.Left.Left, new AvlTree<TKey, TValue>(tree.Key, tree.Value, tree.Left.Right, tree.Right));
    private static IBinarySearchTree<TKey, TValue> RotateRightDouble(IBinarySearchTree<TKey, TValue> tree)
      => tree.Left.IsEmpty ? tree : RotateRight(new AvlTree<TKey, TValue>(tree.Key, tree.Value, RotateLeft(tree.Left), tree.Right));
    #endregion Static Helpers

    private sealed class EmptyAvlTree
      : IBinarySearchTree<TKey, TValue>
    {
      #region IBinaryTree Implementation
      public bool IsEmpty
        => true;
      IImmutableBinaryTree<TValue> IImmutableBinaryTree<TValue>.Left
        => throw new System.Exception(nameof(EmptyAvlTree));
      IImmutableBinaryTree<TValue> IImmutableBinaryTree<TValue>.Right
        => throw new System.Exception(nameof(EmptyAvlTree));
      public TValue Value
        => throw new System.Exception(nameof(EmptyAvlTree));
      #endregion IBinaryTree Implementation

      #region IBinarySearchTree Implementation
      public TKey Key
        => throw new System.Exception(nameof(EmptyAvlTree));
      public IBinarySearchTree<TKey, TValue> Left
        => throw new System.Exception(nameof(EmptyAvlTree));
      public IBinarySearchTree<TKey, TValue> Right
        => throw new System.Exception(nameof(EmptyAvlTree));
      public IBinarySearchTree<TKey, TValue> Add(TKey key, TValue value)
        => new AvlTree<TKey, TValue>(key, value, this, this);
      public IBinarySearchTree<TKey, TValue> Remove(TKey key)
        => throw new System.Exception(nameof(EmptyAvlTree));
      public IBinarySearchTree<TKey, TValue> Search(TKey key)
        => this;
      #endregion IBinarySearchTree Implementation

      #region IMap Implementation
      public System.Collections.Generic.IEnumerable<TKey> Keys { get { yield break; } }
      public System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>> Pairs { get { yield break; } }
      public System.Collections.Generic.IEnumerable<TValue> Values { get { yield break; } }

      IMap<TKey, TValue> IMap<TKey, TValue>.Add(TKey key, TValue value)
        => Add(key, value);
      public bool Contains(TKey key)
        => false;
      public TValue Lookup(TKey key)
        => throw new System.Exception(nameof(EmptyAvlTree));
      IMap<TKey, TValue> IMap<TKey, TValue>.Remove(TKey key)
        => Remove(key);
      #endregion IMap Implementation
    }
  }
}
