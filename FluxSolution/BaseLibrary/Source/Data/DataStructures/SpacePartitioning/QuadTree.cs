using System.Linq;

namespace Flux.DataStructures
{
  public interface IQuadtree
  {
    Numerics.CartesianCoordinate2<int> Position { get; set; }
  }

  /// <summary></summary>
  /// <seealso cref="https://www.geeksforgeeks.org/quad-tree/"/> 
  /// <seealso cref="https://jimkang.com/quadtreevis/"/>
  public sealed class Quadtree<T>
    where T : IQuadtree
  {
    public Numerics.CartesianCoordinate2<int> BoundaryHigh { get; private set; }
    public Numerics.CartesianCoordinate2<int> BoundaryLow { get; private set; }

    private readonly System.Collections.Generic.IList<T> m_items = new System.Collections.Generic.List<T>();
    /// <summary>A list of items in this tree.</summary>
    public System.Collections.Generic.IReadOnlyList<T> Items => (System.Collections.Generic.IReadOnlyList<T>)m_items;

    public int MaximumItems { get; set; }

    private System.Collections.Generic.IList<Quadtree<T>> m_nodes = new System.Collections.Generic.List<Quadtree<T>>();
    /// <summary>A list of sub-trees.</summary>
    public System.Collections.Generic.IReadOnlyList<Quadtree<T>> Nodes => (System.Collections.Generic.IReadOnlyList<Quadtree<T>>)m_nodes;

    public Quadtree(Numerics.CartesianCoordinate2<int> boundaryLow, Numerics.CartesianCoordinate2<int> boundaryHigh)
    {
      BoundaryLow = boundaryLow;
      BoundaryHigh = boundaryHigh;

      MaximumItems = 1;
    }

    public void Clear()
    {
      m_items.Clear();
      m_nodes.Clear();
    }

    public bool InScope(Numerics.CartesianCoordinate2<int> position)
      => position.X >= BoundaryLow.X && position.X <= BoundaryHigh.X && position.Y >= BoundaryLow.Y && position.Y <= BoundaryHigh.Y;

    public bool Insert(T item)
    {
      if (InScope(item.Position))
      {
        if (m_nodes.Count == 0)
        {
          m_items.Add(item);

          if (m_items.Count > MaximumItems)
          {
            if (m_items.Select(i => i.Position).Distinct().Count() > 1)
            {
              if (System.Math.Abs(BoundaryHigh.X - BoundaryLow.X) > 1 && System.Math.Abs(BoundaryHigh.Y - BoundaryLow.Y) > 1)
              {
                Split();
              }
            }
          }
        }
        else
        {
          foreach (var node in m_nodes)
          {
            if (node.Insert(item))
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
      if (aggregator is null) throw new System.ArgumentNullException(nameof(aggregator));

      seed = aggregator(this, seed);

      if (m_nodes.Count > 0)
      {
        foreach (var node in m_nodes)
        {
          seed = node.SearchNodes(seed, aggregator);
        }
      }

      return seed;
    }

    public void Split()
    {
      if (m_nodes is null)
      {
        var midPoint = new Numerics.CartesianCoordinate2<int>((BoundaryLow.X + BoundaryHigh.X) / 2, (BoundaryLow.Y + BoundaryHigh.Y) / 2);

        m_nodes = new System.Collections.Generic.List<Quadtree<T>>()
        {
           new Quadtree<T>(new Numerics.CartesianCoordinate2<int>(midPoint.X, midPoint.Y), new Numerics.CartesianCoordinate2<int>(BoundaryHigh.X, BoundaryHigh.Y)),
           new Quadtree<T>(new Numerics.CartesianCoordinate2<int>(BoundaryLow.X, midPoint.Y), new Numerics.CartesianCoordinate2<int>(midPoint.X - 1, BoundaryHigh.Y)),
           new Quadtree<T>(new Numerics.CartesianCoordinate2<int>(BoundaryLow.X, BoundaryLow.Y), new Numerics.CartesianCoordinate2<int>(midPoint.X - 1, midPoint.Y - 1)),
           new Quadtree<T>(new Numerics.CartesianCoordinate2<int>(midPoint.X, BoundaryLow.Y), new Numerics.CartesianCoordinate2<int>(BoundaryHigh.X, midPoint.Y - 1))
        };
      }

      if (m_items.Count > 0)
      {
        while (m_items.Count > 0)
        {
          var item = m_items.ElementAt(0);

          foreach (var node in m_nodes)
          {
            if (node.Insert(item))
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