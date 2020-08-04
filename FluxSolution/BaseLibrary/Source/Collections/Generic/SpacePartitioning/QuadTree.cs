using System.Linq;

namespace Flux.Collections.Generic
{
  public interface IQuadtree
  {
    Flux.Model.Vector2I Position { get; set; }
  }

  /// <summary></summary>
  /// <seealso cref="https://www.geeksforgeeks.org/quad-tree/"/> 
  /// <seealso cref="https://jimkang.com/quadtreevis/"/>
  public class Quadtree<T> where T : IQuadtree
  {
    public Flux.Model.Vector2I BoundaryHigh { get; private set; }
    public Flux.Model.Vector2I BoundaryLow { get; private set; }

    private System.Collections.Generic.IList<T> m_items = new System.Collections.Generic.List<T>();
    public System.Collections.Generic.IReadOnlyList<T> Items => (System.Collections.Generic.IReadOnlyList<T>)m_items;

    public int MaximumItems { get; set; }

    private System.Collections.Generic.IList<Quadtree<T>> m_subNodes = new System.Collections.Generic.List<Quadtree<T>>();
    public System.Collections.Generic.IReadOnlyList<Quadtree<T>> SubNodes => (System.Collections.Generic.IReadOnlyList<Quadtree<T>>)m_subNodes;

    public Quadtree(Flux.Model.Vector2I boundaryLow, Flux.Model.Vector2I boundaryHigh)
    {
      BoundaryLow = boundaryLow;
      BoundaryHigh = boundaryHigh;

      MaximumItems = 1;
    }

    public bool InScope(Flux.Model.Vector2I position)
      => position.X >= BoundaryLow.X && position.X <= BoundaryHigh.X && position.Y >= BoundaryLow.Y && position.Y <= BoundaryHigh.Y;

    public bool InsertItem(T item)
    {
      if (InScope(item.Position))
      {
        if (m_subNodes.Count == 0)
        {
          m_items.Add(item);

          if (m_items.Count > MaximumItems)
          {
            if (m_items.Select(i => i.Position).Distinct().Count() > 1)
            {
              if (System.Math.Abs(BoundaryHigh.X - BoundaryLow.X) > 1 && System.Math.Abs(BoundaryHigh.Y - BoundaryLow.Y) > 1)
              {
                SubDistributeItems();
              }
            }
          }
        }
        else
        {
          foreach (var node in m_subNodes)
          {
            if (node.InsertItem(item))
            {
              break;
            }
          }
        }

        return true;
      }

      return false;
    }

    public TResult SearchNodes<TResult>(TResult seed, System.Func<Quadtree<T>, TResult, TResult> aggregator)
    {
      seed = aggregator(this, seed);

      if (m_subNodes.Count > 0)
      {
        foreach (var node in m_subNodes)
        {
          seed = node.SearchNodes(seed, aggregator);
        }
      }

      return seed;
    }

    public void SubDistributeItems()
    {
      if (m_subNodes is null)
      {
        var midPoint = new Flux.Model.Vector2I((BoundaryLow.X + BoundaryHigh.X) / 2, (BoundaryLow.Y + BoundaryHigh.Y) / 2);

        m_subNodes = new System.Collections.Generic.List<Quadtree<T>>()
        {
           new Quadtree<T>(new Flux.Model.Vector2I(midPoint.X, midPoint.Y), new Flux.Model.Vector2I(BoundaryHigh.X, BoundaryHigh.Y)),
           new Quadtree<T>(new Flux.Model.Vector2I(BoundaryLow.X, midPoint.Y), new Flux.Model.Vector2I(midPoint.X - 1, BoundaryHigh.Y)),
           new Quadtree<T>(new Flux.Model.Vector2I(BoundaryLow.X, BoundaryLow.Y), new Flux.Model.Vector2I(midPoint.X - 1, midPoint.Y - 1)),
           new Quadtree<T>(new Flux.Model.Vector2I(midPoint.X, BoundaryLow.Y), new Flux.Model.Vector2I(BoundaryHigh.X, midPoint.Y - 1))
        };
      }

      if (m_items.Count > 0)
      {
        while (m_items.Count > 0)
        {
          var item = m_items.ElementAt(0);

          foreach (var node in m_subNodes)
          {
            if (node.InsertItem(item))
            {
              m_items.RemoveAt(0);

              break;
            }
          }
        }

        m_items.Clear();
      }
    }
  }
}
