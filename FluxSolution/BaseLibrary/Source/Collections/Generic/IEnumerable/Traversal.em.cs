using System.Linq;

namespace Flux
{
  public static partial class XtensionsCollections
  {
    public enum TraverseBinarySearchTreeOrder
    {
      InOrder,
      PostOrder,
      PreOrder
    }

    public static System.Collections.Generic.IEnumerable<T> TraverseBinarySearchTree<T>(T node, System.Func<T, T> leftChildSelector, System.Func<T, T> rightChildSelector, TraverseBinarySearchTreeOrder order)
    {
      if (order == TraverseBinarySearchTreeOrder.PreOrder) yield return node;

      foreach (var subNode in TraverseBinarySearchTree<T>(leftChildSelector(node), leftChildSelector, rightChildSelector, order))
      {
        yield return subNode;
      }

      if (order == TraverseBinarySearchTreeOrder.InOrder) yield return node;

      foreach (var subNode in TraverseBinarySearchTree<T>(rightChildSelector(node), leftChildSelector, rightChildSelector, order))
      {
        yield return subNode;
      }

      if (order == TraverseBinarySearchTreeOrder.PostOrder) yield return node;
    }

    /// <summary>Traverses a tree in a breadth-first fashion, starting at a root node and using a user-defined function to get the children at each node of the tree.</summary>
    /// <remarks>The tree is not checked for loops. If the resulting sequence needs to be finite then it is the responsibility of <paramref name="childrenSelector"/> to ensure that loops are not produced.</remarks>
    public static System.Collections.Generic.IEnumerable<T> TraverseBreadthFirstSearch<T>(T node, System.Func<T, System.Collections.Generic.IEnumerable<T>> childrenSelector)
    {
      var queue = new System.Collections.Generic.Queue<T>();

      queue.Enqueue(node);

      while (queue.Count > 0)
      {
        var current = queue.Dequeue();

        yield return current;

        foreach (var child in childrenSelector?.Invoke(current) ?? throw new System.ArgumentNullException(nameof(childrenSelector)))
        {
          queue.Enqueue(child);
        }
      }
    }

    /// <summary>Traverses a tree in a depth-first fashion, starting at a root node and using a user-defined function to get the children at each node of the tree.</summary>
    /// <remarks>The tree is not checked for loops. If the resulting sequence needs to be finite then it is the responsibility of <paramref name="childrenSelector"/> to ensure that loops are not produced.</remarks>
    public static System.Collections.Generic.IEnumerable<T> TraverseDepthFirstSearch<T>(T node, System.Func<T, System.Collections.Generic.IEnumerable<T>> childrenSelector)
    {
      var stack = new System.Collections.Generic.Stack<System.Collections.Generic.Queue<T>>();

      stack.Push(new System.Collections.Generic.Queue<T>(System.Linq.Enumerable.Empty<T>().Append((T)node)));

      while (stack.Count > 0)
      {
        if (stack.Peek().Count > 0)
        {
          var current = stack.Peek().Dequeue();

          yield return current;

          stack.Push(new System.Collections.Generic.Queue<T>(childrenSelector?.Invoke(current) ?? throw new System.ArgumentNullException(nameof(childrenSelector))));
        }
        else
        {
          stack.Pop();
        }
      }
    }
  }
}
