namespace Flux.Model.Sudoku
{
  public static partial class Utilities
  {
    /// <summary>
    /// <para>An array of the 27 units: 9 rows, 9 columns, and 9 blocks.</para>
    /// </summary>
    public static int[][] UnitSquares => CreateAllUnits();

    /// <summary>
    /// <para>Block indices.</para>
    /// </summary>
    public static int[][] Blocks => [[0, 1, 2], [3, 4, 5], [6, 7, 8]];

    /// <summary>
    /// <para>Column indices.</para>
    /// </summary>
    public static int[] Columns => [.. System.Linq.Enumerable.Range(0, 9)];

    /// <summary>
    /// <para>Row indices.</para>
    /// </summary>
    public static int[] Rows => [.. System.Linq.Enumerable.Range(0, 9)];

    /// <summary>
    /// <para>A list of all squares representing the Sudoku board.</para>
    /// <para>E.g. 20 = "C2".</para>
    /// </summary>
    public static int[] Squares => [.. System.Linq.Enumerable.Range(0, 81)];

    /// <summary>
    /// <para>A list of all squares and their respective list of 20 peer squares. Using square numbers 0-80.</para>
    /// <para>E.g. { "C2", ["A2", "B2", "D2", "E2", "F2", "G2", "H2", "I2", "C1", "C3", "C4", "C5", "C6", "C7", "C8", "C9", "A1", "A3", "B1", "B3"] }.</para>
    /// </summary>
    public static int[][] SquarePeers => [.. Squares.Select(square => SquareUnits[square].SelectMany(units => units).Distinct().Where(peers => peers != square).ToArray())];

    /// <summary>
    /// <para>A list of all squares and their respective list of 3 unit lists (columns, rows and box).</para>
    /// <para>E.g. { "C2", [ ["A2", "B2", "C2", "D2", "E2", "F2", "G2", "H2", "I2"], ["C1", "C2", "C3", "C4", "C5", "C6", "C7", "C8", "C9"], ["A1", "A2", "A3", "B1", "B2", "B3", "C1", "C2", "C3"] ] }.</para>
    /// </summary>
    public static int[][][] SquareUnits => [.. Squares.Select(square => new int[][] { [.. GetUnitColumn(square)], [.. GetUnitRow(square)], [.. GetUnitBlock(square)] }.ToArray())];

    /// <summary>
    /// <para>Adjust all integers in an array that are not 1-9 to a 0.</para>
    /// </summary>
    /// <param name="int81"></param>
    /// <param name="empty"></param>
    public static void AdjustInt81(System.Span<int> int81, int empty = 0)
    {
      for (var index = int81.Length - 1; index >= 0; index--)
        if (int81[index] is < 1 or > 9)
          int81[index] = empty;
    }

    /// <summary>
    /// <para>Adjust all characters in an array that are not '1'-'9' to a '0'.</para>
    /// </summary>
    /// <param name="string81"></param>
    /// <param name="empty"></param>
    public static void AdjustString81(System.Span<char> string81, char empty = '0')
    {
      for (var index = string81.Length - 1; index >= 0; index--)
        if (string81[index] is < '1' or > '9')
          string81[index] = empty;
    }

    /// <summary>
    /// <para>Create a new array of 81 integers from an 81 character array.</para>
    /// </summary>
    /// <param name="string81"></param>
    /// <param name="empty"></param>
    /// <returns></returns>
    public static int[] ConvertToInt81(System.ReadOnlySpan<char> string81, int empty = 0)
    {
      var int81 = new int[81];

      for (var index = string81.Length - 1; index >= 0; index--)
        int81[index] = string81[index] is var c && (c is >= '1' and <= '9') ? string81[index] - '0' : empty;

      return int81;
    }

    /// <summary>
    /// <para>Create a new array of 81 characters from an 81 integer array.</para>
    /// </summary>
    /// <param name="int81"></param>
    /// <param name="empty"></param>
    /// <returns></returns>
    public static char[] ConvertToString81(System.ReadOnlySpan<int> int81, char empty = '0')
    {
      var string81 = new char[81];

      for (var index = int81.Length - 1; index >= 0; index--)
        string81[index] = int81[index] is var n && (n is >= 1 and <= 9) ? (char)(n + '0') : empty;

      return string81;
    }

    /// <summary>
    /// <para>Initialize ALL_UNITS to be an array of the 27 units: rows, columns, and blocks.</para>
    /// </summary>
    /// <returns></returns>
    public static int[][] CreateAllUnits()
    {
      var au = new int[3 * 9][];

      var i = 0;

      foreach (var r in Rows)
        au[i++] = Cross([r], Columns);

      foreach (var c in Columns)
        au[i++] = Cross(Rows, [c]);

      foreach (var rb in Blocks)
        foreach (var cb in Blocks)
          au[i++] = Cross(rb, cb);

      return au;
    }

    /// <summary>
    /// <para>Return an array of all squares in the intersection of these rows and cols.</para>
    /// </summary>
    /// <param name="rows"></param>
    /// <param name="columns"></param>
    /// <returns></returns>
    private static int[] Cross(int[] rows, int[] columns)
    {
      var result = new int[rows.Length * columns.Length];

      var i = 0;

      foreach (var r in rows)
        foreach (var c in columns)
          result[i++] = 9 * r + c;

      return result;
    }

    /// <summary>Create a new sequence representing the 9 sudoku "block" squares the index is found in.</summary>
    public static System.Collections.Generic.IEnumerable<int> GetUnitBlock(int index)
    {
      var baseUnitBlockIndex = (index % 9 / 3 * 3) + (index / 27 * 27);

      yield return baseUnitBlockIndex + 0;
      yield return baseUnitBlockIndex + 1;
      yield return baseUnitBlockIndex + 2;
      yield return baseUnitBlockIndex + 9;
      yield return baseUnitBlockIndex + 10;
      yield return baseUnitBlockIndex + 11;
      yield return baseUnitBlockIndex + 18;
      yield return baseUnitBlockIndex + 19;
      yield return baseUnitBlockIndex + 20;
    }

    /// <summary>Create a new sequence representing the 9 sudoku "column" (vertical) squares the index is found in.</summary>
    public static System.Collections.Generic.IEnumerable<int> GetUnitColumn(int index)
    {
      var baseUnitColumnIndex = index % 9;
      yield return baseUnitColumnIndex + 0;
      yield return baseUnitColumnIndex + 9;
      yield return baseUnitColumnIndex + 18;
      yield return baseUnitColumnIndex + 27;
      yield return baseUnitColumnIndex + 36;
      yield return baseUnitColumnIndex + 45;
      yield return baseUnitColumnIndex + 54;
      yield return baseUnitColumnIndex + 63;
      yield return baseUnitColumnIndex + 72;
    }

    /// <summary>Create a new sequence representing the 9 sudoku "row" (horizontal) squares the index is found in.</summary>
    public static System.Collections.Generic.IEnumerable<int> GetUnitRow(int index)
    {
      var baseUnitRowIndex = index / 9 * 9;
      yield return baseUnitRowIndex + 0;
      yield return baseUnitRowIndex + 1;
      yield return baseUnitRowIndex + 2;
      yield return baseUnitRowIndex + 3;
      yield return baseUnitRowIndex + 4;
      yield return baseUnitRowIndex + 5;
      yield return baseUnitRowIndex + 6;
      yield return baseUnitRowIndex + 7;
      yield return baseUnitRowIndex + 8;
    }

    public static void WriteConsole(int[] int81) => System.Console.WriteLine(ToConsoleString(int81));

    public static string ToConsoleString<T>(System.ReadOnlySpan<T> t81)
    {
      var sb = new System.Text.StringBuilder();

      for (var i = 0; i < 9; i++)
      {
        for (var j = 0; j < 9; j++)
        {
          sb.Append($"{t81[i * 9 + j]}" + " ");

          if (j == 2 || j == 5)
            sb.Append("| ");
        }

        sb.Append('\n');

        if (i == 2 || i == 5)
          sb.Append("------ ------- ------\n");
      }

      sb.Replace('0', ' ');

      return sb.ToString();
    }

    //public static bool Verify(int[] solution, int[] int81)
    //{
    //  if (solution == null)
    //    return false;

    //  // Check that all squares have a single digit, and no filled square in the puzzle was changed in the solution.
    //  foreach (var s in Squares)
    //    if (solution[s] is < 1 or > 9 || int81[s] is < 1 or > 9 || solution[s] != int81[s])
    //      return false;

    //  // Check that each unit is a permutation of digits
    //  foreach (int[] u in AllUnits())
    //  {
    //    var unit_digits = 0; // All the digits in a unit.

    //    foreach (var s in u)
    //      unit_digits |= solution[s];

    //    if (unit_digits != ALL_DIGITS)
    //    {
    //      return false;
    //    }
    //  }
    //  return true;
    //}

  }
}
