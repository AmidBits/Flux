using System.Linq;

namespace Flux.DataStructures.Immutable
{
  public sealed class BinarySearchTree<TKey, TValue>
    : IBinarySearchTree<TKey, TValue>
    where TKey : System.IComparable<TKey>
  {
    public static readonly IBinarySearchTree<TKey, TValue> Empty = new EmptyBinarySearchTree();

    private readonly TKey m_key;
    private readonly IBinarySearchTree<TKey, TValue> m_left;
    private readonly IBinarySearchTree<TKey, TValue> m_right;
    private readonly TValue m_value;

    private BinarySearchTree(TKey key, TValue value, IBinarySearchTree<TKey, TValue> left, IBinarySearchTree<TKey, TValue> right)
    {
      m_key = key;
      m_left = left;
      m_right = right;
      m_value = value;
    }

    // IBinaryTree<TValue>
    public bool IsEmpty
      => false;
    IBinaryTree<TValue> IBinaryTree<TValue>.Left
      => m_left;
    IBinaryTree<TValue> IBinaryTree<TValue>.Right
      => m_right;
    public TValue Value
      => m_value;

    // IBinarySearchTree<TKey, TValue>
    public TKey Key
      => m_key;
    public IBinarySearchTree<TKey, TValue> Left
      => m_left;
    public IBinarySearchTree<TKey, TValue> Right
      => m_right;
    public IBinarySearchTree<TKey, TValue> Add(TKey key, TValue value)
      => key.CompareTo(Key) > 0 ? new BinarySearchTree<TKey, TValue>(Key, Value, Left, Right.Add(key, value)) : new BinarySearchTree<TKey, TValue>(Key, Value, Left.Add(key, value), Right);
    public IBinarySearchTree<TKey, TValue> Remove(TKey key)
    {
      switch (key.CompareTo(Key))
      {
        case var lt when lt < 0:
          return new BinarySearchTree<TKey, TValue>(Key, Value, Left.Remove(key), Right);
        case var gt when gt > 0:
          return new BinarySearchTree<TKey, TValue>(Key, Value, Left, Right.Remove(key));
        default:
          if (Right.IsEmpty && Left.IsEmpty) return Empty; // If this is a leaf, just remove it by returning Empty.
          else if (Right.IsEmpty && !Left.IsEmpty) return Left; // If we have only a left child, replace the node with the child.
          else if (!Right.IsEmpty && Left.IsEmpty) return Right; // If we have only a right child, replace the node with the child.
          else // We have two children. Remove the next-highest node and replace this node with it.
          {
            var successor = Right;
            while (!successor.Left.IsEmpty) successor = successor.Left;
            return new BinarySearchTree<TKey, TValue>(successor.Key, successor.Value, Left, Right.Remove(successor.Key));
          }
      }
    }
    public IBinarySearchTree<TKey, TValue> Search(TKey key)
      => IsEmpty ? Empty : (key.CompareTo(Key) switch { var gt when gt > 0 => Right.Search(key), var lt when lt < 0 => Left.Search(key), _ => this });

    // IMap<TKey, TValue>
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
      => Search(key) is var tree && tree.IsEmpty ? throw new System.Exception(@"Key not found.") : tree.Value;
    IMap<TKey, TValue> IMap<TKey, TValue>.Remove(TKey key)
      => Remove(key);

    public override string ToString()
      => $"{GetType().Name} {{ Left = {(Left.IsEmpty ? '-' : 'L')}, Right = {(Right.IsEmpty ? '-' : 'R')}, Key = {m_key}, Value = {m_value} }}";

    private sealed class EmptyBinarySearchTree
      : IBinarySearchTree<TKey, TValue>
    {
      // IBinaryTree<TValue>
      public bool IsEmpty
        => true;
      IBinaryTree<TValue> IBinaryTree<TValue>.Left
        => throw new System.Exception(nameof(EmptyBinarySearchTree));
      IBinaryTree<TValue> IBinaryTree<TValue>.Right
        => throw new System.Exception(nameof(EmptyBinarySearchTree));
      public TValue Value
        => throw new System.Exception(nameof(EmptyBinarySearchTree));

      // IBinarySearchTree<TKey, TValue>
      public TKey Key
        => throw new System.Exception(nameof(EmptyBinarySearchTree));
      public IBinarySearchTree<TKey, TValue> Left
        => throw new System.Exception(nameof(EmptyBinarySearchTree));
      public IBinarySearchTree<TKey, TValue> Right
        => throw new System.Exception(nameof(EmptyBinarySearchTree));
      public IBinarySearchTree<TKey, TValue> Add(TKey key, TValue value)
        => new BinarySearchTree<TKey, TValue>(key, value, this, this);
      public IBinarySearchTree<TKey, TValue> Remove(TKey key)
        => throw new System.Exception(nameof(EmptyBinarySearchTree));
      public IBinarySearchTree<TKey, TValue> Search(TKey key)
        => this;

      // IMap<TKey, TValue>
      public System.Collections.Generic.IEnumerable<TKey> Keys
      { get { yield break; } }
      public System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>> Pairs
      { get { yield break; } }
      public System.Collections.Generic.IEnumerable<TValue> Values
      { get { yield break; } }
      IMap<TKey, TValue> IMap<TKey, TValue>.Add(TKey key, TValue value)
        => Add(key, value);
      public bool Contains(TKey key)
        => false;
      public TValue Lookup(TKey key)
        => throw new System.Exception(nameof(EmptyBinarySearchTree));
      IMap<TKey, TValue> IMap<TKey, TValue>.Remove(TKey key)
        => Remove(key);

      public override string ToString()
        => $"{GetType().Name}{System.Environment.NewLine}{this.ToConsoleBlock()}";
    }
  }
}