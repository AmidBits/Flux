//using System.Collections;
//using System.Collections.Generic;

namespace Flux.Model
{
  public abstract class AGrid<TValue>
    : System.Collections.Generic.IEnumerable<TValue>
  {
    public int Count => Size.Width * Size.Height;

    public Media.Geometry.Size2 Size { get; }

    private System.Collections.Generic.IList<TValue> Values;// { get; protected set; }

    public (int row, int column) IndexToRowColumn(int index)
      => (index / Size.Width, index % Size.Width);
    public int RowColumnToIndex(int row, int column)
      => column + (row * Size.Width);

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

    public AGrid(int rows, int columns)
    {
      Size = new Media.Geometry.Size2(columns, rows);

      Values = new TValue[rows * columns];

      Reset();
    }

    public TValue GetValue(int index)
      => this[index];
    public TValue GetValue(int row, int column)
      => this[row, column];

    public System.Collections.Generic.IEnumerable<TValue> GetValues()
    {
      for (var index = 0; index < Values.Count; index++)
        yield return GetValue(index);
    }

    public void SetValue(int index, TValue value)
      => this[index] = value;
    public void SetValue(int row, int column, TValue value)
      => this[row, column] = value;

    public virtual void Reset()
    {
      for (var index = 0; index < Values.Count; index++)
        Values[index] = default!;
    }

    public System.Collections.Generic.IEnumerator<TValue> GetEnumerator()
      => GetValues().GetEnumerator();
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
      => GetEnumerator();
  }
}
