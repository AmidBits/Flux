namespace Flux.DataStructures
{
  public interface IQuadtree
  {
    System.Drawing.Point Position { get; set; }
  }

  /// <summary>
  /// <para></para>
  /// <para><see href="https://en.wikipedia.org/wiki/Quadtree"/></para>
  /// <para><seealso href="https://www.geeksforgeeks.org/quad-tree/"/></para> 
  /// <para><seealso href="https://jimkang.com/quadtreevis/"/></para>
  /// </summary>
  public sealed class Quadtree<T>
    where T : IQuadtree
  {
    public System.Drawing.Point BoundaryHigh { get; private set; }
    public System.Drawing.Point BoundaryLow { get; private set; }

    private readonly System.Collections.Generic.List<T> m_items = new();
    /// <summary>A list of items in this tree.</summary>
    public System.Collections.Generic.IReadOnlyList<T> Items => (System.Collections.Generic.IReadOnlyList<T>)m_items;

    public int MaximumItems { get; set; }

    private System.Collections.Generic.List<Quadtree<T>> m_nodes = new();
    /// <summary>A list of sub-trees.</summary>
    public System.Collections.Generic.IReadOnlyList<Quadtree<T>> Nodes => (System.Collections.Generic.IReadOnlyList<Quadtree<T>>)m_nodes;

    public Quadtree(System.Drawing.Point boundaryLow, System.Drawing.Point boundaryHigh)
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

    public bool InScope(System.Drawing.Point position)
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
              if (int.Abs(BoundaryHigh.X - BoundaryLow.X) > 1 && int.Abs(BoundaryHigh.Y - BoundaryLow.Y) > 1)
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
      System.ArgumentNullException.ThrowIfNull(aggregator);

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
        var midPoint = new System.Drawing.Point((BoundaryLow.X + BoundaryHigh.X) / 2, (BoundaryLow.Y + BoundaryHigh.Y) / 2);

        m_nodes = new()
        {
          new(new System.Drawing.Point(midPoint.X, midPoint.Y), new System.Drawing.Point(BoundaryHigh.X, BoundaryHigh.Y)),
          new(new System.Drawing.Point(BoundaryLow.X, midPoint.Y), new System.Drawing.Point(midPoint.X - 1, BoundaryHigh.Y)),
          new(new System.Drawing.Point(BoundaryLow.X, BoundaryLow.Y), new System.Drawing.Point(midPoint.X - 1, midPoint.Y - 1)),
          new(new System.Drawing.Point(midPoint.X, BoundaryLow.Y), new System.Drawing.Point(BoundaryHigh.X, midPoint.Y - 1))
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
