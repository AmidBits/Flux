using System.Collections.Immutable;

// https://github.com/gmamaladze/minesweeper
namespace Flux.Model.MineSweeper
{
  public sealed class Covers
    : System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<CartesianCoordinate2I, bool>>
  {
    private readonly System.Collections.Immutable.IImmutableDictionary<CartesianCoordinate2I, bool> m_covers;

    public int Count
      => m_covers.Count;

    private Covers(System.Collections.Immutable.IImmutableDictionary<CartesianCoordinate2I, bool> covers)
      => m_covers = covers;

    public bool HasFlag(CartesianCoordinate2I point)
      => m_covers[point];
    public bool IsCovered(CartesianCoordinate2I point)
      => m_covers.ContainsKey(point);

    public Covers SwitchFlag(CartesianCoordinate2I point)
      => m_covers.TryGetValue(point, out var hasFlag) ? new Covers(m_covers.SetItem(point, !hasFlag)) : this;

    public Covers Uncover(CartesianCoordinate2I point)
      => IsCovered(point) ? new Covers(m_covers.Remove(point)) : this;

    public Covers UncoverRange(System.Collections.Generic.IEnumerable<CartesianCoordinate2I> points)
      => new(m_covers.RemoveRange(points));

    // Statics
    public static Covers Create(Size2 size)
      => new(size.AllPoints().ToImmutableDictionary(p => p, p => false));
    // IEnumerable
    public System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<CartesianCoordinate2I, bool>> GetEnumerator()
      => m_covers.GetEnumerator();
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
      => GetEnumerator();
  }
}
