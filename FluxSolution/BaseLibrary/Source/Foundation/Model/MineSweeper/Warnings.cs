using System.Linq;

// https://github.com/gmamaladze/minesweeper
namespace Flux.Model.MineSweeper
{
  public sealed class Warnings
    : System.Collections.Generic.IReadOnlyDictionary<CartesianCoordinateI2, int>
  {
    private readonly System.Collections.Generic.IDictionary<CartesianCoordinateI2, int> m_warnings;

    public bool HasWarningAt(CartesianCoordinateI2 point)
      => m_warnings.ContainsKey(point);
    public int WarningsAt(CartesianCoordinateI2 point)
      => m_warnings.TryGetValue(point, out var result) ? result : 0;

    private Warnings(System.Collections.Generic.IDictionary<CartesianCoordinateI2, int> warnings)
      => m_warnings = warnings;

    #region IReadOnlyDictionary implementation
    public int Count
      => m_warnings.Count;
    public System.Collections.Generic.IEnumerable<CartesianCoordinateI2> Keys
      => m_warnings.Keys;
    public System.Collections.Generic.IEnumerable<int> Values
      => m_warnings.Values;

    public int this[CartesianCoordinateI2 key]
      => m_warnings[key];

    public bool ContainsKey(CartesianCoordinateI2 key)
      => m_warnings.ContainsKey(key);
    public System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<CartesianCoordinateI2, int>> GetEnumerator()
      => m_warnings.GetEnumerator();
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
      => GetEnumerator();
    public bool TryGetValue(CartesianCoordinateI2 key, [System.Diagnostics.CodeAnalysis.MaybeNullWhen(false)] out int value)
      => m_warnings.TryGetValue(key, out value);
    #endregion IReadOnlyDictionary implementation

    public static Warnings Create(Field field, Mines mines)
      => new(mines.SelectMany(mine => field.GetNeighbours(mine)).GroupBy(neighbor => neighbor).ToDictionary(g => g.Key, g => g.Count()));
  }
}
