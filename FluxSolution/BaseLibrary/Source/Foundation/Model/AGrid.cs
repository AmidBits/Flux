//using System.Collections;
//using System.Collections.Generic;

namespace Flux.Model
{
  public abstract class AGrid<TValue>
    : System.Collections.Generic.IEnumerable<TValue>
  {
    public int Count { get; }

    public Geometry.Size2 Size { get; }

    private readonly TValue[] Values;// { get; protected set; }

    public (int row, int column) IndexToRowColumn(int index)
      => (index / Size.Width, index % Size.Width);
    public int RowColumnToIndex(int row, int column)
      => column + (row * Size.Width);

    public AGrid(int rows, int columns)
    {
      Count = rows * columns;

      Size = new Geometry.Size2(columns, rows);

      Values = new TValue[Count];

      Reset();
    }
    public AGrid(Geometry.Size2 size)
      : this(size.Height, size.Width)
    { }

    public TValue this[int index]
    {
      get => Values[index];
      set => Values[index] = value;
    }
    public TValue this[int row, int column]
    {
      get => Values[RowColumnToIndex(row, column)];
      set => Values[RowColumnToIndex(row, column)] = value;
    }

    public TValue GetValue(int index)
      => this[index];
    public TValue GetValue(int row, int column)
      => this[row, column];

    public System.Collections.Generic.IEnumerable<TValue> GetValues()
    {
      var valuesLength = Count;

      for (var index = 0; index < valuesLength; index++)
        yield return GetValue(index);
    }

    public void SetValue(int index, TValue value)
      => this[index] = value;
    public void SetValue(int row, int column, TValue value)
      => this[row, column] = value;

    public virtual void Reset()
    {
      for (var index = Count - 1; index >= 0; index--)
        Values[index] = default!;
    }

    public System.Collections.Generic.IEnumerator<TValue> GetEnumerator()
      => GetValues().GetEnumerator();
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
      => GetEnumerator();
  }
}
