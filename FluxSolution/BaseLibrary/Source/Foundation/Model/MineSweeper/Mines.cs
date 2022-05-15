using System.Collections.Generic;
using System.Linq;

// https://github.com/gmamaladze/minesweeper
namespace Flux.Model.MineSweeper
{
  public sealed class Mines
    : System.Collections.Generic.IReadOnlySet<CartesianCoordinateI2>
  {
    private readonly System.Collections.Generic.IReadOnlySet<CartesianCoordinateI2> m_mines;

    private Mines(System.Collections.Generic.IReadOnlySet<CartesianCoordinateI2> mines)
      => m_mines = mines;

    public bool HasMineAt(CartesianCoordinateI2 point)
      => m_mines.Contains(point);

    public static Mines Create(Field field, int count)
      => new(System.Linq.Enumerable.Repeat(Randomization.NumberGenerator.Crypto, count * 2).Select(r => new CartesianCoordinateI2(r.Next(field.Size.Width), r.Next(field.Size.Height))).Distinct().Take(count).ToHashSet());

    #region IReadOnlySet implementation
    public int Count
      => m_mines.Count;
    public bool Contains(CartesianCoordinateI2 item)
      => m_mines.Contains(item);
    public bool IsProperSubsetOf(IEnumerable<CartesianCoordinateI2> other)
      => m_mines.IsProperSubsetOf(other);
    public bool IsProperSupersetOf(IEnumerable<CartesianCoordinateI2> other)
      => m_mines.IsProperSupersetOf(other);
    public bool IsSubsetOf(IEnumerable<CartesianCoordinateI2> other)
      => m_mines.IsSubsetOf(other);
    public bool IsSupersetOf(IEnumerable<CartesianCoordinateI2> other)
      => m_mines.IsSupersetOf(other);
    public bool Overlaps(IEnumerable<CartesianCoordinateI2> other)
      => m_mines.Overlaps(other);
    public bool SetEquals(IEnumerable<CartesianCoordinateI2> other)
      => m_mines.SetEquals(other);
    public System.Collections.Generic.IEnumerator<CartesianCoordinateI2> GetEnumerator()
      => m_mines.GetEnumerator();
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
      => GetEnumerator();
    #endregion IReadOnlySet implementation
  }
}
