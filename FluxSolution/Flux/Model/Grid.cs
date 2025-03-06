namespace Flux.Model
{
  public class Grid<TValue> where TValue : notnull
  {
    private readonly Flux.DataStructures.OrderedDictionary<(int row, int column), TValue> m_data = new();

    private readonly int m_rows;
    private readonly int m_columns;

    public Grid(int rows, int columns)
    {
      m_rows = rows;
      m_columns = columns;
    }

    /// <summary>
    /// <para>The number of squares available for the <see cref="Grid{TValue}"/>.</para>
    /// </summary>
    public int Length => m_rows * m_columns;

    public int Count => m_data.Count;

    public int Rows => m_rows;
    public int Columns => m_columns;

    public System.Drawing.Point Size => new(m_columns, m_rows);

    public System.Collections.Generic.IReadOnlyCollection<(int row, int column)> Keys => (System.Collections.Generic.IReadOnlyCollection<(int row, int column)>)m_data.Keys;
    public System.Collections.Generic.IReadOnlyCollection<TValue> Values => (System.Collections.Generic.IReadOnlyCollection<TValue>)m_data.Values;

    /// <summary>The preferred way to access the grid values.</summary>
    public TValue this[int row, int column]
    {
      get => m_data[KeyFrom(row, column)];
      set => m_data[KeyFrom(row, column)] = value;
    }
    public TValue this[int uniqueIndex]
    {
      get => m_data[KeyFrom(uniqueIndex)];
      set => m_data[KeyFrom(uniqueIndex)] = value;
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

    public bool ContainsKey(int row, int column) => m_data.ContainsKey(KeyFrom(row, column));
    public bool ContainsKey(int uniqueIndex) => m_data.ContainsKey(KeyFrom(uniqueIndex));

    public bool ContainsValue(TValue value) => m_data.ContainsValue(value);

    public bool IsWithinBounds(int row, int column) => row >= 0 && row < m_rows && column >= 0 && column < m_columns;
    public bool IsWithinBounds(int linearIndex)
      => linearIndex >= 0 && linearIndex < (m_rows * m_columns)
      ? IsWithinBounds(linearIndex / m_columns, linearIndex % m_columns)
      : throw new System.ArgumentOutOfRangeException(nameof(linearIndex));

    public void Swap(int sourceRow, int sourceColumn, int targetRow, int targetColumn)
    {
      if (TryGetValue(sourceRow, sourceColumn, out TValue sourceValue) && TryGetValue(targetRow, targetColumn, out TValue targetValue))
      {
        this[sourceRow, sourceColumn] = targetValue;
        this[targetRow, targetColumn] = sourceValue;
      }
      else throw new System.ArgumentOutOfRangeException($"{sourceRow}, {sourceColumn}");
    }
    public void Swap(int sourceIndex, int targetIndex)
    {
      if (TryGetValue(sourceIndex, out TValue sourceValue) && TryGetValue(targetIndex, out TValue targetValue))
      {
        this[sourceIndex] = targetValue;
        this[targetIndex] = sourceValue;
      }
      else throw new System.ArgumentOutOfRangeException($"{sourceIndex} or {targetIndex}");
    }

    public bool TryGetIndex(TValue value, out int index) => m_data.TryGetIndex(value, out index);

    public bool TryGetKey(TValue value, out (int row, int column) key) => m_data.TryGetKey(value, out key);

    public bool TryGetValue(int row, int column, out TValue value) => m_data.TryGetValue(KeyFrom(row, column), out value!);
    public bool TryGetValue(int uniqueIndex, out TValue value) => m_data.TryGetValue(KeyFrom(uniqueIndex), out value!);

    /// <summary></summary>
    /// <param name="resultSelector"></param>
    /// <param name="includeArrayFrameSlots">Whether the two-dimensional array is surrounded by empty slots.</param>
    /// <returns>A two-dimensional array.</returns>
    public object[,] ToTwoDimensionalArray(System.Func<TValue, object> resultSelector, bool includeArrayFrameSlots = true)
    {
      var extra = includeArrayFrameSlots ? 1 : 0;

      var array = new object[extra + m_rows + extra, extra + m_columns + extra];

      for (var row = 0; row < m_rows; row++)
        for (var column = 0; column < m_columns; column++)
          array[row + extra, column + extra] = resultSelector(TryGetValue(row, column, out var value) ? value : default!);

      return array;
    }

    public string ToConsoleBlock(System.Func<TValue, object> resultSelector)
      => System.Environment.NewLine + ToTwoDimensionalArray(resultSelector).Rank2ToConsoleString(ConsoleFormatOptions.Default with { HorizontalSeparator = null, VerticalSeparator = null, UniformWidth = true, CenterContent = true }).ToString();
    public string ToConsoleBlock()
      => ToConsoleBlock(v => v.Equals(default(TValue)) ? "\u00B7" : "V");
  }
}
