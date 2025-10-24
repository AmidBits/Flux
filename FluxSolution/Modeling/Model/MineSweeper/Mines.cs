using System.Collections.Generic;
using System.Linq;

// https://github.com/gmamaladze/minesweeper
namespace Flux.Model.MineSweeper
{
  public sealed class Mines
    : System.Collections.Generic.IReadOnlySet<System.Drawing.Point>
  {
    private readonly System.Collections.Generic.IReadOnlySet<System.Drawing.Point> m_mines;

    private Mines(System.Collections.Generic.IReadOnlySet<System.Drawing.Point> mines)
      => m_mines = mines;

    public bool HasMineAt(System.Drawing.Point point)
      => m_mines.Contains(point);

    public static Mines Create(Field field, int count)
      => new(System.Linq.Enumerable.Repeat(RandomNumberGenerators.SscRng.Shared, count * 2).Select(r => new System.Drawing.Point(r.Next(field.Size.X), r.Next(field.Size.Y))).Distinct().Take(count).ToHashSet());

    #region IReadOnlySet implementation
    public int Count
      => m_mines.Count;
    public bool Contains(System.Drawing.Point item)
      => m_mines.Contains(item);
    public bool IsProperSubsetOf(IEnumerable<System.Drawing.Point> other)
      => m_mines.IsProperSubsetOf(other);
    public bool IsProperSupersetOf(IEnumerable<System.Drawing.Point> other)
      => m_mines.IsProperSupersetOf(other);
    public bool IsSubsetOf(IEnumerable<System.Drawing.Point> other)
      => m_mines.IsSubsetOf(other);
    public bool IsSupersetOf(IEnumerable<System.Drawing.Point> other)
      => m_mines.IsSupersetOf(other);
    public bool Overlaps(IEnumerable<System.Drawing.Point> other)
      => m_mines.Overlaps(other);
    public bool SetEquals(IEnumerable<System.Drawing.Point> other)
      => m_mines.SetEquals(other);
    public System.Collections.Generic.IEnumerator<System.Drawing.Point> GetEnumerator()
      => m_mines.GetEnumerator();
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
      => GetEnumerator();
    #endregion IReadOnlySet implementation
  }
}
