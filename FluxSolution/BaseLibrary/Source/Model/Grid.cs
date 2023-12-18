namespace Flux.Model
{
  public class Grid<TValue>
    : System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<(int row, int column), TValue>>
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

    public int Count
      => m_values.Count;

    public int Rows => m_rows;
    public int Columns => m_columns;

    public System.Drawing.Point Size
      => new(m_columns, m_rows);

    public System.Collections.Generic.IReadOnlyCollection<(int row, int column)> Keys
      => (System.Collections.Generic.IReadOnlyCollection<(int row, int column)>)m_values.Keys;
    public System.Collections.Generic.IReadOnlyCollection<TValue> Values
      => (System.Collections.Generic.IReadOnlyCollection<TValue>)m_values.Values;

    public System.Collections.Generic.IEnumerable<TValue> GetValues()
      => Values;

    /// <summary>The preferred way to access the grid values.</summary>
    public TValue this[int row, int column]
    {
      get => m_values[KeyFrom(row, column)];
      set => m_values[KeyFrom(row, column)] = value;
    }
    public TValue this[int uniqueIndex]
    {
      get => m_values[KeyFrom(uniqueIndex)];
      set => m_values[KeyFrom(uniqueIndex)] = value;
    }

    /// <summary>Creates a new <see cref="System.ValueTuple{TValue}"/> and performs all necessary boundary checks.</summary>
    /// <param name="row"></param>
    /// <param name="column"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    private (int row, int column) KeyFrom(int row, int column)
      => row < 0 || row >= m_rows ? throw new System.ArgumentOutOfRangeException(nameof(row))
      : column < 0 || column >= m_columns ? throw new System.ArgumentOutOfRangeException(nameof(column))
      : (row, column);
    private (int row, int column) KeyFrom(int uniqueIndex)
      => uniqueIndex < 0 || uniqueIndex >= (m_rows * m_columns)
      ? throw new System.ArgumentOutOfRangeException(nameof(uniqueIndex))
      : (uniqueIndex / m_columns, uniqueIndex % m_columns);

    public bool ContainsKey(int row, int column)
      => m_values.ContainsKey(KeyFrom(row, column));
    public bool ContainsKey(int uniqueIndex)
      => m_values.ContainsKey(KeyFrom(uniqueIndex));

    public TValue GetValue(int row, int column)
      => m_values.TryGetValue(KeyFrom(row, column), out var value) ? value : throw new System.ArgumentException("The row/column values cannot be found.");
    public TValue GetValue(int uniqueIndex)
      => m_values.TryGetValue(KeyFrom(uniqueIndex), out var value) ? value : throw new System.ArgumentOutOfRangeException(nameof(uniqueIndex));

    public void SetValue(int row, int column, TValue value)
      => m_values[KeyFrom(row, column)] = value;
    public void SetValue(int uniqueIndex, TValue value)
      => m_values[KeyFrom(uniqueIndex)] = value;

    public bool TryGetValue(int row, int column, out TValue value)
      => m_values.TryGetValue(KeyFrom(row, column), out value!);
    public bool TryGetValue(int uniqueIndex, out TValue value)
      => m_values.TryGetValue(KeyFrom(uniqueIndex), out value!);

    public System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<(int row, int column), TValue>> GetEnumerator()
      => m_values.GetEnumerator();
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
      => GetEnumerator();

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
      => string.Join(System.Environment.NewLine, ToArray(resultSelector).Rank2ToConsoleString(new ConsoleStringOptions() { HorizontalSeparator = '\0', VerticalSeparator = '\0', UniformWidth = true, CenterContent = true }));
    public string ToConsoleBlock()
      => ToConsoleBlock(v => v.Equals(default(TValue)) ? "\u00B7" : "V");
  }
}
