using System.Collections.Immutable;

// https://github.com/gmamaladze/minesweeper
namespace Flux.Model.MineSweeper
{
  public sealed class Covers
    : System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<Geometry.CartesianCoordinate2<int>, bool>>
  {
    private readonly System.Collections.Immutable.IImmutableDictionary<Geometry.CartesianCoordinate2<int>, bool> m_covers;

    public int Count
      => m_covers.Count;

    private Covers(System.Collections.Immutable.IImmutableDictionary<Geometry.CartesianCoordinate2<int>, bool> covers)
      => m_covers = covers;

    public bool HasFlag(Geometry.CartesianCoordinate2<int> point)
      => m_covers[point];
    public bool IsCovered(Geometry.CartesianCoordinate2<int> point)
      => m_covers.ContainsKey(point);

    public Covers SwitchFlag(Geometry.CartesianCoordinate2<int> point)
      => m_covers.TryGetValue(point, out var hasFlag) ? new Covers(m_covers.SetItem(point, !hasFlag)) : this;

    public Covers Uncover(Geometry.CartesianCoordinate2<int> point)
      => IsCovered(point) ? new Covers(m_covers.Remove(point)) : this;

    public Covers UncoverRange(System.Collections.Generic.IEnumerable<Geometry.CartesianCoordinate2<int>> points)
      => new(m_covers.RemoveRange(points));

    // Statics
    public static Covers Create(Geometry.CartesianCoordinate2<int> size)
      => new(size.AllPoints().ToImmutableDictionary(p => p, p => false));
    // IEnumerable
    public System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<Geometry.CartesianCoordinate2<int>, bool>> GetEnumerator()
      => m_covers.GetEnumerator();
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
      => GetEnumerator();
  }
}
