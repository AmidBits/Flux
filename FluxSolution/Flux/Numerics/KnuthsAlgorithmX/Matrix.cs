namespace Flux.Numerics.KnuthsAlgorithmX
{
  /*
      var m = new Flux.KnuthsAlgorithmX.Matrix(7);

      m.AddRows(new[]
      {
        new[] { 2, 4, 5 },
        new[] { 0, 3, 6 },
        new[] { 1, 2, 5 },
        new[] { 4 },
        new[] { 0, 3 },
        new[] { 1, 6 },
        new[] { 3, 4, 6 },
        new[] { 2, 5 }
      });

      var solutions = m.GetAllExactCovers();
      var sols = solutions.Select(s => s.ToArray()).ToArray();
      var str = Flux.KnuthsAlgorithmX.Matrix.StringifySolutions(sols);
      var s0 = sols[0];
   */

  /// <summary>
  /// <para>An algorithm for solving the exact cover problem. It is a straightforward recursive, nondeterministic, depth-first, backtracking algorithm used by Donald Knuth to demonstrate an efficient implementation called DLX, which uses the dancing links technique.</para>
  /// <para>The exact cover problem is represented in Algorithm X by an incidence matrix A consisting of 0s and 1s. The goal is to select a subset of the rows such that the digit 1 appears in each column exactly once.</para>
  /// <para><see cref="https://en.wikipedia.org/wiki/Knuth%27s_Algorithm_X"/></para>
  /// <para><see href="https://github.com/dimchansky/AlgorithmX/tree/master"/></para>
  /// </summary>
  /// <remarks>Collections in C# are 0-based, but many tutorials and examples uses 1-based examples.</remarks>
  public sealed class Matrix
  {
    private readonly ColumnObject m_head = new(-1);
    private readonly ColumnObject[] m_indexedColumns;

    public Matrix(int columns)
    {
      m_indexedColumns = new ColumnObject[columns];

      for (var i = 0; i < m_indexedColumns.Length; i++)
      {
        var column = new ColumnObject(i);
        m_head.InsertLeft(column);
        m_indexedColumns[i] = column;
      }
    }

    public void AddRows(System.Collections.Generic.IEnumerable<System.Collections.Generic.IList<int>> rows)
    {
      foreach (var orderedColumnsRow in rows)
      {
        var lastElement = -1;

        DataObject? firstColumn = null;

        foreach (var columnIndex in orderedColumnsRow)
        {
          if (columnIndex <= lastElement) throw new ArgumentException("Column indexes aren't growing strictly.");

          lastElement = columnIndex;

          var column = m_indexedColumns[columnIndex];

          var dataObject = new DataObject(column);
          column.InsertUp(dataObject);
          column.Size += 1;

          if (firstColumn == null)
            firstColumn = dataObject;
          else
            firstColumn.InsertLeft(dataObject);
        }
      }
    }

    public System.Collections.Generic.IEnumerable<System.Collections.Generic.IEnumerable<System.Collections.Generic.IList<int>>> GetAllExactCovers()
      => Search(new System.Collections.Generic.Stack<DataObject>());

    public static System.Collections.Generic.IList<string> StringifySolutions(System.Collections.Generic.IEnumerable<System.Collections.Generic.IEnumerable<System.Collections.Generic.IEnumerable<int>>> solutions)
      => solutions.Select(solution => string.Join(" | ", solution.Select(row => string.Join(Static.CommaSpace, row)))).ToArray();

    private System.Collections.Generic.IEnumerable<System.Collections.Generic.IEnumerable<System.Collections.Generic.IList<int>>> Search(System.Collections.Generic.Stack<DataObject> partialSolution)
    {
      var headRight = m_head.Right;

      if (headRight == m_head)
        yield return partialSolution.Select(o => o.GetOrderedColumnsRow().ToArray());

      var c = headRight.Column;
      var s = c.Size;

      for (var j = headRight.Right; j != m_head; j = j.Right)
      {
        var jc = j.Column;

        var jsize = jc.Size;
        if (jsize >= s) continue;

        c = jc;
        s = jsize;
      }

      CoverColumn(c);

      for (var r = c.Down; r != c; r = r.Down)
      {
        partialSolution.Push(r);

        for (var j = r.Right; j != r; j = j.Right)
          CoverColumn(j.Column);

        foreach (var solution in Search(partialSolution))
          yield return solution;

        partialSolution.Pop();

        for (var j = r.Left; j != r; j = j.Left)
          UnCoverColumn(j.Column);
      }

      UnCoverColumn(c);
    }

    private static void CoverColumn(DataObject c)
    {
      c.Right.Left = c.Left;
      c.Left.Right = c.Right;

      for (var i = c.Down; i != c; i = i.Down)
      {
        for (var j = i.Right; j != i; j = j.Right)
        {
          j.Down.Up = j.Up;
          j.Up.Down = j.Down;
          j.Column.Size -= 1;
        }
      }
    }

    private static void UnCoverColumn(DataObject c)
    {
      for (var i = c.Up; i != c; i = i.Up)
      {
        for (var j = i.Left; j != i; j = j.Left)
        {
          j.Column.Size += 1;
          j.Down.Up = j;
          j.Up.Down = j;
        }
      }

      c.Right.Left = c;
      c.Left.Right = c;
    }
  }
}