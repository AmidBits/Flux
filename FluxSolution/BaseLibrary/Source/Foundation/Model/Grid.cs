namespace Flux.Model
{
  public class Grid<TValue>
    : IConsoleWritable, System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<(int row, int column), TValue>>
    where TValue : notnull
  {
    private readonly int m_rows;
    private readonly int m_columns;

    private readonly System.Collections.Generic.IDictionary<(int row, int column), TValue> m_values;

    public Grid(int rows, int columns)
    {
      m_rows = rows;
      m_columns = columns;

      m_values = new System.Collections.Generic.SortedDictionary<(int row, int column), TValue>();
    }

    public int Rows => m_rows;
    public int Columns => m_columns;

    public System.Collections.Generic.IReadOnlyDictionary<(int row, int column), TValue> Values
      => (System.Collections.Generic.IReadOnlyDictionary<(int row, int column), TValue>)m_values;

    /// <summary>The preferred way to access the grid values.</summary>
    public TValue this[int row, int column]
    {
      get => m_values[RowColumnToKey(row, column)];
      set => m_values[RowColumnToKey(row, column)] = value;
    }

    /// <summary>Creates a new <see cref="System.ValueTuple{TValue}"/> and performs all necessary boundary checks.</summary>
    /// <param name="row"></param>
    /// <param name="column"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public (int row, int column) RowColumnToKey(int row, int column)
      => row < 0 || row >= m_rows ? throw new System.ArgumentOutOfRangeException(nameof(row))
      : column < 0 || column >= m_columns ? throw new System.ArgumentOutOfRangeException(nameof(column))
      : (row, column);

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

    /// <summary></summary>
    /// <param name="resultSelector"></param>
    /// <param name="includeArrayFrameSlots">Whether the two-dimensional array is surrounded by empty slots.</param>
    /// <returns>A two-dimensional array.</returns>
    public object[,] ToArray(System.Func<TValue, object> resultSelector, bool includeArrayFrameSlots = true)
    {
      var extra = includeArrayFrameSlots ? 1 : 0;

      var array = new object[extra + m_rows + extra, extra + m_columns + extra];

      for (var row = 0; row < m_rows; row++)
        for (var column = 0; column < m_columns; column++)
          array[row + extra, column + extra] = resultSelector(TryGetValue(row, column, out var value) ? value : default!);

      return array;
    }

    public string ToConsoleBlock(System.Func<TValue, object> resultSelector)
      => string.Join(System.Environment.NewLine, ToArray(resultSelector).ToConsoleStrings('\0', '\0', true, true));
    public string ToConsoleBlock()
      => ToConsoleBlock(v => v.Equals(default(TValue)) ? "\u00B7" : "V");
  }
}
