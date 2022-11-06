using System.Linq;

namespace Flux.DataStructures
{
  public interface IOctree
  {
    Point3 Position { get; set; }
  }

  /// <summary></summary>
  public sealed class Octree<T> 
    where T : IOctree
  {
    public Point3 BoundaryHigh { get; private set; }
    public Point3 BoundaryLow { get; private set; }

    private readonly System.Collections.Generic.IList<T> m_items = new System.Collections.Generic.List<T>();
    public System.Collections.Generic.IReadOnlyList<T> Items => (System.Collections.Generic.IReadOnlyList<T>)m_items;

    public int MaximumItems { get; set; }

    private readonly System.Collections.Generic.IList<Octree<T>> m_subNodes = new System.Collections.Generic.List<Octree<T>>();
    public System.Collections.Generic.IReadOnlyList<Octree<T>> SubNodes => (System.Collections.Generic.IReadOnlyList<Octree<T>>)m_subNodes;

    public Octree(Point3 boundaryLow, Point3 boundaryHigh)
    {
      BoundaryLow = boundaryLow;
      BoundaryHigh = boundaryHigh;

      MaximumItems = 1;
    }

    public bool InScope(Point3 position)
      => position.X >= BoundaryLow.X && position.X <= BoundaryHigh.X && position.Y >= BoundaryLow.Y && position.Y <= BoundaryHigh.Y && position.Z >= BoundaryLow.Z && position.Z <= BoundaryHigh.Z;

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
              if (System.Math.Abs(BoundaryHigh.X - BoundaryLow.X) > 1 && System.Math.Abs(BoundaryHigh.Y - BoundaryLow.Y) > 1 && System.Math.Abs(BoundaryHigh.Z - BoundaryLow.Z) > 1) // Should be || (OR) instead of && (AND)?
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

    public TResult SearchNodes<TResult>(TResult seed, System.Func<Octree<T>, TResult, TResult> aggregator)
    {
      if (aggregator is null) throw new System.ArgumentNullException(nameof(aggregator));

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
      if (m_subNodes.Count == 0)
      {
        var midPoint = new Point3((BoundaryLow.X + BoundaryHigh.X) / 2, (BoundaryLow.Y + BoundaryHigh.Y) / 2, (BoundaryLow.Z + BoundaryHigh.Z) / 2);

        m_subNodes.Add(new Octree<T>(new Point3(midPoint.X, midPoint.Y, midPoint.Z), new Point3(BoundaryHigh.X, BoundaryHigh.Y, BoundaryHigh.Z)));
        m_subNodes.Add(new Octree<T>(new Point3(BoundaryLow.X, midPoint.Y, midPoint.Z), new Point3(midPoint.X - 1, BoundaryHigh.Y, BoundaryHigh.Z)));
        m_subNodes.Add(new Octree<T>(new Point3(BoundaryLow.X, BoundaryLow.Y, midPoint.Z), new Point3(midPoint.X - 1, midPoint.Y - 1, BoundaryHigh.Z)));
        m_subNodes.Add(new Octree<T>(new Point3(midPoint.X, BoundaryLow.Y, midPoint.Z), new Point3(BoundaryHigh.X, midPoint.Y - 1, BoundaryHigh.Z)));
        m_subNodes.Add(new Octree<T>(new Point3(midPoint.X, midPoint.Y, BoundaryLow.Z), new Point3(BoundaryHigh.X, BoundaryHigh.Y, midPoint.Z - 1)));
        m_subNodes.Add(new Octree<T>(new Point3(BoundaryLow.X, midPoint.Y, BoundaryLow.Z), new Point3(midPoint.X - 1, BoundaryHigh.Y, midPoint.Z - 1)));
        m_subNodes.Add(new Octree<T>(new Point3(BoundaryLow.X, BoundaryLow.Y, BoundaryLow.Z), new Point3(midPoint.X - 1, midPoint.Y - 1, midPoint.Z - 1)));
        m_subNodes.Add(new Octree<T>(new Point3(midPoint.X, BoundaryLow.Y, BoundaryLow.Z), new Point3(BoundaryHigh.X, midPoint.Y - 1, midPoint.Z - 1)));
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
