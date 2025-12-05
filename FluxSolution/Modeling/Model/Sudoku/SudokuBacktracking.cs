namespace Flux.Model.Sudoku
{
  public class SudokuBacktracking
  {
    private int[] m_sudoku;

    private System.Collections.Generic.List<int> m_unassignedPositions;

    public SudokuBacktracking(string string81)
    {
      m_sudoku = Utilities.ConvertToInt81(string81);

      m_unassignedPositions = [.. m_sudoku.Select((e, i) => (e, i)).Where(t => t.e == 0).Select(t => t.i)];

      Original = Utilities.ConvertToString81(m_sudoku).AsSpan().ToString();
    }

    public string Original { get; }

    public string Sudoku => new string(Utilities.ConvertToString81(m_sudoku));

    private int GetUnassignedPosition() => m_unassignedPositions.Count > 0 ? m_unassignedPositions[0] : -1;

    private int Solve(int position)
    {
      if (position == -1)
        return position;

      for (var n = 1; n <= 9; n++)
      {
        if (CheckValidity(n, position % 9, position / 9))
        {
          m_sudoku[position] = n;
          m_unassignedPositions.Remove(position);

          var result = Solve(GetUnassignedPosition());

          if (result == -1)
            return result;

          m_unassignedPositions.Add(position);
          m_sudoku[position] = 0;
        }
      }

      return position;
    }

    public bool Solve()
      => Solve(GetUnassignedPosition()) == -1;

    /// <summary>
    /// <para>Check the validity of digit (1-9) and grid location [x, y].</para>
    /// </summary>
    /// <param name="digit"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public bool CheckValidity(int digit, int x, int y)
    {
      for (var i = 0; i < 9; i++)
        if (m_sudoku[y * 9 + i] == digit || m_sudoku[i * 9 + x] == digit)
          return false;

      var startX = x / 3 * 3;
      var startY = y / 3 * 3;

      for (int i = startY; i < startY + 3; i++)
        for (int j = startX; j < startX + 3; j++)
          if (m_sudoku[i * 9 + j] == digit)
            return false;

      return true;
    }

    public override string ToString()
      => Utilities.ToConsoleString<char>(Original) + System.Environment.NewLine + Utilities.ToConsoleString(m_sudoku);
  }

}
