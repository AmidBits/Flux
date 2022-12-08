using System.Collections.Immutable;

// https://github.com/gmamaladze/minesweeper
namespace Flux.Model.MineSweeper
{
  public sealed class Covers
    : System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<Point2, bool>>
  {
    private readonly System.Collections.Immutable.IImmutableDictionary<Point2, bool> m_covers;

    public int Count
      => m_covers.Count;

    private Covers(System.Collections.Immutable.IImmutableDictionary<Point2, bool> covers)
      => m_covers = covers;

    public bool HasFlag(Point2 point)
      => m_covers[point];
    public bool IsCovered(Point2 point)
      => m_covers.ContainsKey(point);

    public Covers SwitchFlag(Point2 point)
      => m_covers.TryGetValue(point, out var hasFlag) ? new Covers(m_covers.SetItem(point, !hasFlag)) : this;

    public Covers Uncover(Point2 point)
      => IsCovered(point) ? new Covers(m_covers.Remove(point)) : this;

    public Covers UncoverRange(System.Collections.Generic.IEnumerable<Point2> points)
      => new(m_covers.RemoveRange(points));

    // Statics
    public static Covers Create(Size2<int> size)
      => new(size.AllPoints().ToImmutableDictionary(p => p, p => false));
    // IEnumerable
    public System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<Point2, bool>> GetEnumerator()
      => m_covers.GetEnumerator();
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
      => GetEnumerator();
  }
}
