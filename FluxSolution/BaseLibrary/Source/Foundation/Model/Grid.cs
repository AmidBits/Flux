namespace Flux.Model
{
  public class Grid<TValue>
    : IConsoleWritable, System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<(int row, int column), TValue>>
  {
    private readonly System.Collections.Generic.IDictionary<(int row, int column), TValue> m_values;

    /// <summary>The size of the grid. The width represents the number of columns, and the height the number of rows.</summary>
    public Size2 Size { get; }

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
      => row >= 0 && row < Size.Height ? column >= 0 && column < Size.Width ? (row, column) : throw new System.ArgumentOutOfRangeException(nameof(column)) : throw new System.ArgumentOutOfRangeException(nameof(row));

    public (int row, int column) IndexToKey(int index)
      => (index / Size.Width, index % Size.Width);
    public int KeyToIndex((int row, int column) key)
      => key.column + (key.row * Size.Width);

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

    public string[,] ToArray()
    {
      var a = new string[Size.Height, Size.Width];

      for (var y = 0; y < Size.Height; y++)
      {
        for (var x = 0; x < Size.Width; x++)
        {
          if (TryGetValue(y, x, out var value))
            a[y, x] = "V";
          else
            a[y, x] = "\u00B7";
        }
      }

      return a;
    }

    public string ToConsoleBlock()
      => string.Join(System.Environment.NewLine, ToArray().ToConsoleStrings('\0', '\0', true, true));
  }
}
