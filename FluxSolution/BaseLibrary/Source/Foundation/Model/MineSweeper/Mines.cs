using System.Collections.Generic;
using System.Linq;

// https://github.com/gmamaladze/minesweeper
namespace Flux.Model.MineSweeper
{
  public sealed class Mines
    : System.Collections.Generic.IReadOnlySet<CartesianCoordinate2I>
  {
    private readonly System.Collections.Generic.IReadOnlySet<CartesianCoordinate2I> m_mines;

    private Mines(System.Collections.Generic.IReadOnlySet<CartesianCoordinate2I> mines)
      => m_mines = mines;

    public bool HasMineAt(CartesianCoordinate2I point)
      => m_mines.Contains(point);

    public static Mines Create(Field field, int count)
      => new(System.Linq.Enumerable.Repeat(Randomization.NumberGenerator.Crypto, count * 2).Select(r => new CartesianCoordinate2I(r.Next(field.Size.Width), r.Next(field.Size.Height))).Distinct().Take(count).ToHashSet());

    #region IReadOnlySet implementation
    public int Count
      => m_mines.Count;
    public bool Contains(CartesianCoordinate2I item)
      => m_mines.Contains(item);
    public bool IsProperSubsetOf(IEnumerable<CartesianCoordinate2I> other)
      => m_mines.IsProperSubsetOf(other);
    public bool IsProperSupersetOf(IEnumerable<CartesianCoordinate2I> other)
      => m_mines.IsProperSupersetOf(other);
    public bool IsSubsetOf(IEnumerable<CartesianCoordinate2I> other)
      => m_mines.IsSubsetOf(other);
    public bool IsSupersetOf(IEnumerable<CartesianCoordinate2I> other)
      => m_mines.IsSupersetOf(other);
    public bool Overlaps(IEnumerable<CartesianCoordinate2I> other)
      => m_mines.Overlaps(other);
    public bool SetEquals(IEnumerable<CartesianCoordinate2I> other)
      => m_mines.SetEquals(other);
    public System.Collections.Generic.IEnumerator<CartesianCoordinate2I> GetEnumerator()
      => m_mines.GetEnumerator();
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
      => GetEnumerator();
    #endregion IReadOnlySet implementation
  }
}
