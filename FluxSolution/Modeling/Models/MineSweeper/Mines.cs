using System.Collections.Generic;
using System.Linq;

// https://github.com/gmamaladze/minesweeper
namespace Flux.Model.MineSweeper
{
  public sealed class Mines
    : System.Collections.Generic.IReadOnlySet<CoordinateSystems.CartesianCoordinate2<int>>
  {
    private readonly System.Collections.Generic.IReadOnlySet<CoordinateSystems.CartesianCoordinate2<int>> m_mines;

    private Mines(System.Collections.Generic.IReadOnlySet<CoordinateSystems.CartesianCoordinate2<int>> mines)
      => m_mines = mines;

    public bool HasMineAt(CoordinateSystems.CartesianCoordinate2<int> point)
      => m_mines.Contains(point);

    public static Mines Create(Field field, int count)
      => new(System.Linq.Enumerable.Repeat(Random.NumberGenerators.Crypto, count * 2).Select(r => new CoordinateSystems.CartesianCoordinate2<int>(r.Next(field.Size.Width), r.Next(field.Size.Height))).Distinct().Take(count).ToHashSet());

    #region IReadOnlySet implementation
    public int Count
      => m_mines.Count;
    public bool Contains(CoordinateSystems.CartesianCoordinate2<int> item)
      => m_mines.Contains(item);
    public bool IsProperSubsetOf(IEnumerable<CoordinateSystems.CartesianCoordinate2<int>> other)
      => m_mines.IsProperSubsetOf(other);
    public bool IsProperSupersetOf(IEnumerable<CoordinateSystems.CartesianCoordinate2<int>> other)
      => m_mines.IsProperSupersetOf(other);
    public bool IsSubsetOf(IEnumerable<CoordinateSystems.CartesianCoordinate2<int>> other)
      => m_mines.IsSubsetOf(other);
    public bool IsSupersetOf(IEnumerable<CoordinateSystems.CartesianCoordinate2<int>> other)
      => m_mines.IsSupersetOf(other);
    public bool Overlaps(IEnumerable<CoordinateSystems.CartesianCoordinate2<int>> other)
      => m_mines.Overlaps(other);
    public bool SetEquals(IEnumerable<CoordinateSystems.CartesianCoordinate2<int>> other)
      => m_mines.SetEquals(other);
    public System.Collections.Generic.IEnumerator<CoordinateSystems.CartesianCoordinate2<int>> GetEnumerator()
      => m_mines.GetEnumerator();
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
      => GetEnumerator();
    #endregion IReadOnlySet implementation
  }
}
