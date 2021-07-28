namespace Flux.Model
{
  public class SquareGrid<TValue>
    : System.Collections.Generic.IEnumerable<TValue>
  {
    private readonly System.Collections.Generic.IDictionary<Geometry.Point2, TValue> m_map;

    /// <summary>The size of the grid. The width represents the number of columns, and the height the number of rows.</summary>
    public Geometry.Size2 Size { get; }

    public SquareGrid(Geometry.Size2 size)
    {
      m_map = new System.Collections.Generic.SortedDictionary<Geometry.Point2, TValue>();

      Size = size;
    }

    /// <summary>The preferred way to access the grid values.</summary>
    public TValue this[Geometry.Point2 point]
    {
      get => m_map.TryGetValue(point, out var value) ? value : default!;
      set => m_map[point] = value!;
    }

    /// <summary>Access with [row, column] is provided for convenience in for loops.</summary>
    public TValue this[int row, int column]
    {
      get => this[new Geometry.Point2(column, row)];
      set => this[new Geometry.Point2(column, row)] = value;
    }

    public Geometry.Point2 IndexToPoint(int index)
      => new Geometry.Point2(index % Size.Width, index / Size.Width);
    public int PointToIndex(Geometry.Point2 point)
      => point.X + (point.Y * Size.Width);

    /// <summary>Access with [index] is provided for convenience in loops.</summary>
    public TValue this[int index]
    {
      get => this[IndexToPoint(index)];
      set => this[IndexToPoint(index)] = value;
    }

    public System.Collections.Generic.IEnumerator<TValue> GetEnumerator()
      => m_map.Values.GetEnumerator();
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
      => m_map.Values.GetEnumerator();
  }
}
