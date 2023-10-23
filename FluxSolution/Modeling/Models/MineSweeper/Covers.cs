using System.Collections.Immutable;

// https://github.com/gmamaladze/minesweeper
namespace Flux.Model.MineSweeper
{
  public sealed class Covers
    : System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.Drawing.Point, bool>>
  {
    private readonly System.Collections.Immutable.IImmutableDictionary<System.Drawing.Point, bool> m_covers;

    public int Count
      => m_covers.Count;

    private Covers(System.Collections.Immutable.IImmutableDictionary<System.Drawing.Point, bool> covers)
      => m_covers = covers;

    public bool HasFlag(System.Drawing.Point point)
      => m_covers[point];
    public bool IsCovered(System.Drawing.Point point)
      => m_covers.ContainsKey(point);

    public Covers SwitchFlag(System.Drawing.Point point)
      => m_covers.TryGetValue(point, out var hasFlag) ? new Covers(m_covers.SetItem(point, !hasFlag)) : this;

    public Covers Uncover(System.Drawing.Point point)
      => IsCovered(point) ? new Covers(m_covers.Remove(point)) : this;

    public Covers UncoverRange(System.Collections.Generic.IEnumerable<System.Drawing.Point> points)
      => new(m_covers.RemoveRange(points));

    // Statics
    public static Covers Create(System.Drawing.Point size)
      => new(size.AllPoints().ToImmutableDictionary(p => p, p => false));
    // IEnumerable
    public System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<System.Drawing.Point, bool>> GetEnumerator()
      => m_covers.GetEnumerator();
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
      => GetEnumerator();
  }
}
