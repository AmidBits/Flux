namespace Flux.Model
{
  public class Grid<TValue>
    : IConsoleWritable, System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<(int row, int column), TValue>>
  {
    private readonly System.Collections.Generic.IDictionary<(int row, int column), TValue> m_values;

    private readonly int m_rows;
    private readonly int m_columns;

    public Grid(int rows, int columns)
    {
      m_values = new System.Collections.Generic.SortedDictionary<(int row, int column), TValue>();

      m_rows = rows;
      m_columns = columns;
    }

    /// <summary>The preferred way to access the grid values.</summary>
    public TValue this[int row, int column]
    {
      get => m_values[RowColumnToKey(row, column)];
      set => m_values[RowColumnToKey(row, column)] = value;
    }

    public (int row, int column) RowColumnToKey(int row, int column)
      => row >= 0 && row < m_rows ? column >= 0 && column < m_columns ? (row, column) : throw new System.ArgumentOutOfRangeException(nameof(column)) : throw new System.ArgumentOutOfRangeException(nameof(row));

    public (int row, int column) IndexToKey(int index)
      => (index / m_columns, index % m_columns);
    public int KeyToIndex((int row, int column) key)
      => key.column + (key.row * m_columns);

    public System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<(int row, int column), TValue>> GetEnumerator()
      => m_values.GetEnumerator();
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
      => GetEnumerator();

    public bool ContainsKey(int row, int column)
      => m_values.ContainsKey(RowColumnToKey(row, column));

    public bool TryGetValue(int row, int column, out TValue value)
      => m_values.TryGetValue(RowColumnToKey(row, column), out value!);

    public void SetValue(int row, int column, TValue value)
      => m_values[RowColumnToKey(row, column)] = value;

    public object[,] ToArray(System.Func<TValue, object> resultSelector, bool includeHeaderAxes = false)
    {
      var extra = (includeHeaderAxes ? 1 : 0);

      var array = new object[m_rows + extra, m_columns + extra];

      for (var row = 0; row < m_rows; row++)
        for (var column = 0; column < m_columns; column++)
          array[row + extra, column + extra] = resultSelector(TryGetValue(row, column, out var value) ? value : default!);

      return array;
    }

    public string ToConsoleBlock()
      => string.Join(System.Environment.NewLine, ToArray(v => default(TValue)?.Equals(v) ?? false ? "\u00B7" : "V").ToConsoleStrings('\0', '\0', true, true));
  }
}
