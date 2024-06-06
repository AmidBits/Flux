namespace Flux.DataStructures
{
  public interface IBinaryTreeArrayNode<TKey, TValue>
    where TKey : System.IComparable<TKey>
  {
    /// <summary>Determines whether the node is empty.</summary>
    bool IsEmpty { get; }

    /// <summary>The key of the node.</summary>
    TKey Key { get; }
    /// <summary>The value of the node.</summary>
    TValue Value { get; }
  }
}
