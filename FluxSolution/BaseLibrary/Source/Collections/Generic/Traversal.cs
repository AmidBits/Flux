using System.Linq;

namespace Flux.Collections.Generic
{
  public static partial class Traversal
  {
    /// <summary>Perform a breadth first search (BFS) on a tree hierarchy.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Breadth-first_search"/>
    public static System.Collections.Generic.IEnumerable<(int depth, T node)> BreadthFirstSearch<T>(T source, int maxDepth, System.Func<(int depth, T item), bool> branchPredicate, System.Func<T, System.Collections.Generic.IEnumerable<T>> selectorChildren)
    {
      var items = new System.Collections.Generic.List<T> { source };

      for (var depth = 0; items.Count != 0 && depth < maxDepth; depth++)
      {
        var nextItems = new System.Collections.Generic.List<T>();

        foreach (var item in items)
        {
          var element = (depth, item);

          if (branchPredicate(element))
          {
            yield return element;

            nextItems.AddRange(selectorChildren(item));
          }
        }

        items = nextItems;
      }
    }

    /// <summary>Perform a depth first search (DFS) on a tree hierarchy.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Depth-first_search"/>
    public static System.Collections.Generic.IEnumerable<(int depth, T node)> DepthFirstSearch<T>(T source, int maxDepth, System.Func<(int depth, T item), bool> branchPredicate, System.Func<T, System.Collections.Generic.IEnumerable<T>> selectorChildren)
    {
      var stack = new System.Collections.Generic.Stack<System.Collections.Generic.Queue<T>>();

      stack.Push(new System.Collections.Generic.Queue<T>(System.Linq.Enumerable.Empty<T>().Append((T)source)));

      while (stack.Count > 0)
      {
        if (stack.Peek().Count > 0)
        {
          var element = (depth: stack.Count - 1, item: stack.Peek().Dequeue());

          if (branchPredicate(element))
          {
            yield return element;

            if (stack.Count < maxDepth)
            {
              stack.Push(new System.Collections.Generic.Queue<T>(selectorChildren(element.item)));
            }
          }
        }
        else
        {
          stack.Pop();
        }
      }
    }

    /// <summary>Perform a iterative deepening depth first search (IDDFS) on a tree hierarchy.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Iterative_deepening_depth-first_search"/>
    public static T IterativeDeepeningDepthFirstSearch<T>(T node, int maxDepth, System.Func<T, bool> predicateGoal, System.Func<T, System.Collections.Generic.IEnumerable<T>> selectorChildren)
      where T : class
    {
      for (int depth = 0; depth <= maxDepth; depth++)
      {
        var (itemFound, moreItems) = DepthLimitedSearch(node, depth, predicateGoal, selectorChildren);
        if (itemFound != null) return itemFound;
        else if (!moreItems) break;
      }

      return default!;
    }

    /// <summary>Perform a depth limited search (DLS) on a tree hierarchy.</summary>
    /// <seealso cref="https://en.wikipedia.org/wiki/Iterative_deepening_depth-first_search#Algorithm_for_Directed_Graphs"/>
    public static (T itemFound, bool moreItems) DepthLimitedSearch<T>(T node, int depth, System.Func<T, bool> predicateGoal, System.Func<T, System.Collections.Generic.IEnumerable<T>> selectorChildren)
      where T : class
    {
      if (depth == 0)
      {
        if (predicateGoal?.Invoke(node) ?? throw new System.ArgumentNullException(nameof(predicateGoal))) return (node, true);
        else return (default!, true);
      }
      else if (depth > 0)
      {
        var anyRemaining = false;

        foreach (T childNode in selectorChildren?.Invoke(node) ?? throw new System.ArgumentNullException(nameof(selectorChildren)))
        {
          var (itemFound, moreItems) = DepthLimitedSearch(childNode, depth - 1, predicateGoal, selectorChildren);
          if (itemFound != null) return (itemFound, true);
          if (moreItems) anyRemaining = true;
        }

        return (default!, anyRemaining);
      }
      else throw new System.ArgumentOutOfRangeException(nameof(depth));
    }
  }
}

//public abstract class TraversableTree<T>
//{
//  public abstract System.Collections.Generic.IEnumerable<T> GetRoot();

//  public abstract System.Collections.Generic.IEnumerable<T> GetSubNodes(T node);

//  public System.Collections.Generic.IEnumerable<(int depth, T item)> BreadthFirstSearch(System.Collections.Generic.IEnumerable<T> items, System.Func<(int depth, T item), bool> selector, int maxDepth = int.MaxValue)
//  {
//    for (var depth = 0; items != null && items.Any() && depth < maxDepth; depth = unchecked(depth + 1))
//    {
//      foreach (var item in items)
//      {
//        var element = (depth, item);

//        if (selector(element))
//        {
//          yield return element;
//        }
//      }

//      items = items.SelectMany(item => GetSubNodes(item));
//    }
//  }

//  public System.Collections.Generic.IEnumerable<(int depth, T item)> DepthFirstSearch(System.Collections.Generic.IEnumerable<T> items, System.Func<(int depth, T item), bool> selector, int maxDepth = int.MaxValue)
//  {
//    var stack = new System.Collections.Generic.Stack<System.Collections.Generic.Queue<T>>();

//    stack.Push(new System.Collections.Generic.Queue<T>(items));

//    while (stack.Count > 0)
//    {
//      if (stack.Peek().Count > 0)
//      {
//        var element = (depth: stack.Count - 1, item: stack.Peek().Dequeue());

//        if (selector(element))
//        {
//          yield return element;
//        }

//        if (stack.Count < maxDepth)
//        {
//          stack.Push(new System.Collections.Generic.Queue<T>(GetSubNodes(element.item)));
//        }
//      }
//      else
//      {
//        stack.Pop();
//      }
//    }
//  }
//}
