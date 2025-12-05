//namespace Flux.Model.Sudoku
//{
//  public interface ISudoku<TGrid>
//  {
//    int SelectSquare(TGrid grid);
//  }

//  public class SudokuJava
//    : ISudoku<int[]>
//  {
//    static readonly int N = 9; // Number of cells on a side of grid.
//    static readonly int[] Bits = { 1 << 0, 1 << 1, 1 << 2, 1 << 3, 1 << 4, 1 << 5, 1 << 6, 1 << 7, 1 << 8, 1 << 9 };
//    static readonly int ALL_DIGITS = 0b111111111;
//    static readonly int[] ROWS = System.Linq.Enumerable.Range(0, N).ToArray();
//    static readonly int[] COLS = ROWS;
//    //static readonly int[] SQUARES = System.Linq.Enumerable.Range(0, N * N).ToArray();
//    static readonly int[][] BLOCKS = new int[][] { new int[] { 0, 1, 2 }, new int[] { 3, 4, 5 }, new int[] { 6, 7, 8 } };
//    //static readonly int[][] ALL_UNITS = new int[3 * N][];
//    //static readonly int[][][] UNITS = CreateJaggedArray3(N * N, 3, N);
//    //static readonly int[][] PEERS = CreateJaggedArray2(N * N, 20);
//    //static readonly int[] NUM_DIGITS = new int[ALL_DIGITS + 1];
//    //static readonly int[] HIGHEST_DIGIT = new int[ALL_DIGITS + 1];

//    bool printFileStats = false;  // -f
//    bool printGrid = true; // -g
//    bool runNakedPairs = false;  // -n
//    //bool printPuzzleStats = false; // -p
//    bool reversePuzzle = false; // -r
//    bool runSearch = true;  // -s
//    //bool printThreadStats = false; // -t
//    bool verifySolution = true;  // -v
//    int nThreads = 26;    // -T
//    int repeat = 1;     // -R

//    int backtracks = 0;     // count total backtracks

//    public SudokuJava()
//    {
//      //// Initialize ALL_UNITS to be an array of the 27 units: rows, columns, and blocks
//      //int i = 0;
//      //foreach (int r in Utilities.Rows) { ALL_UNITS[i++] = Cross(new int[] { r }, COLS); }
//      //foreach (int c in Utilities.Columns) { ALL_UNITS[i++] = Cross(ROWS, new int[] { c }); }
//      //foreach (int[] rb in BLOCKS) { foreach (int[] cb in BLOCKS) { ALL_UNITS[i++] = Cross(rb, cb); } }

//      //// Initialize each UNITS[s] to be an array of the 3 units for square s.
//      //foreach (int s in SQUARES)
//      //{
//      //  i = 0;
//      //  foreach (int[] u in ALL_UNITS)
//      //  {
//      //    if (Member(s, u)) UNITS[s][i++] = u;
//      //  }
//      //}

//      //// Initialize each PEERS[s] to be an array of the 20 squares that are peers of square s.
//      //foreach (int s in SQUARES)
//      //{
//      //  i = 0;
//      //  foreach (int[] u in UNITS[s])
//      //  {
//      //    foreach (int s2 in u)
//      //    {
//      //      if (s2 != s && !Member(s2, PEERS[s], i))
//      //      {
//      //        PEERS[s][i++] = s2;
//      //      }
//      //    }
//      //  }
//      //}

//      //// Initialize NUM_DIGITS[val] to be the number of 1 bits in the bitset val
//      //// and HIGHEST_DIGIT[val] to the highest bit set in the bitset val
//      //for (int val = 0; val <= ALL_DIGITS; val++)
//      //{
//      //  NUM_DIGITS[val] = int.PopCount(val);
//      //  HIGHEST_DIGIT[val] = val.MostSignificant1Bit();
//      //}
//    }

//    public static int[][] CreateJaggedArray2(int d1, int d2)
//    {
//      var ja2 = new int[d1][];

//      for (var i1 = 0; i1 < d1; i1++)
//        ja2[i1] = new int[d2];

//      return ja2;
//    }

//    public static int[][][] CreateJaggedArray3(int d1, int d2, int d3)
//    {
//      var ja3 = new int[d1][][];

//      for (var i1 = 0; i1 < d1; i1++)
//        ja3[i1] = new int[d2][];

//      for (var i1 = 0; i1 < d1; i1++)
//        for (var i2 = 0; i2 < d2; i2++)
//          ja3[i1][i2] = new int[d3];

//      return ja3;
//    }

//    /** Return an array of all squares in the intersection of these rows and cols **/
//    public static int[] Cross(int[] rows, int[] cols)
//    {
//      var result = new int[rows.Length * cols.Length];

//      var i = 0;

//      foreach (var r in rows)
//        foreach (var c in cols)
//          result[i++] = N * r + c;

//      return result;
//    }

//    /** Return true iff item is an element of array, or of array[0:end]. **/
//    bool Member(int item, int[] array)
//      => Member(item, array, array.Length);

//    bool Member(int item, int[] array, int end)
//    {
//      for (int i = 0; i < end; ++i)
//      {
//        if (array[i] == item) { return true; }
//      }
//      return false;
//    }

//    /** Check if square s is consistent: that is, it has multiple possible values, or it has
//     ** one possible value which we can consistently fill. **/
//    bool ArcConsistent(int[] grid, int s)
//    {
//      int count = grid[s].GetPopCount();
//      return count >= 2 || (count == 1 && (Fill(grid, s, grid[s]) != null));
//    }

//    /** After we eliminate d from possibilities for grid[s], check each unit of s
//     ** and make sure there is some position in the unit where d can go.
//     ** If there is only one possible place for d, fill it with d. **/
//    bool DualConsistent(int[] grid, int s, int d)
//    {
//      foreach (int[] u in Utilities.SquareUnits[s])
//      {
//        int dPlaces = 0; // The number of possible places for d within unit u
//        int dplace = -1; // Try to find a place in the unit where d can go
//        foreach (int s2 in u)
//        {
//          if ((grid[s2] & d) > 0)
//          { // s2 is a possible place for d
//            dPlaces++;
//            if (dPlaces > 1) break;
//            dplace = s2;
//          }
//        }
//        if (dPlaces == 0 || (dPlaces == 1 && (Fill(grid, dplace, d) == null)))
//        {
//          return false;
//        }
//      }
//      return true;
//    }

//    /** Look for two squares in a unit with the same two possible values, and no other values.
//     ** For example, if s and s2 both have the possible values 8|9, then we know that 8 and 9
//     ** must go in those two squares. We don't know which is which, but we can eliminate 
//     ** 8 and 9 from any other square s3 that is in the unit. **/
//    bool NakedPairs(int[] grid, int s)
//    {
//      if (!runNakedPairs)
//        return true;

//      int val = grid[s];

//      if (val.GetPopCount() != 2)
//        return true; // Doesn't apply

//      foreach (var s2 in Utilities.SquarePeers[s])
//      {
//        if (grid[s2] == val)
//        {
//          // s and s2 are a naked pair; find what unit(s) they share
//          foreach (int[] u in Utilities.SquareUnits[s])
//          {
//            if (Member(s2, u))
//            {
//              foreach (int s3 in u)
//              { // s3 can't have either of the values in val (e.g. 8|9)
//                if (s3 != s && s3 != s2)
//                {
//                  int d = val.MostSignificant1Bit();
//                  int d2 = val - d;
//                  if (!Eliminate(grid, s3, d) || !Eliminate(grid, s3, d2))
//                  {
//                    return false;
//                  }
//                }
//              }
//            }
//          }
//        }
//      }
//      return true;
//    }

//    /** Eliminate digit d as a possibility for grid[s]. 
//     ** Run the 3 constraint propagation routines.
//     ** If constraint propagation detects a contradiction, return false. **/
//    bool Eliminate(int[] grid, int s, int d)
//    {
//      if ((grid[s] & d) == 0) { return true; } // d already eliminated from grid[s]
//      /*if (grid[s] > 0) */
//      grid[s] -= d;
//      return ArcConsistent(grid, s) && DualConsistent(grid, s, d) && NakedPairs(grid, s);
//    }


//    /** fill grid[s] = d. If this leads to contradiction, return null. **/
//    int[] Fill(int[] grid, int s, int d)
//    {
//      if ((grid == null) || ((grid[s] & d) == 0)) { return null; } // d not possible for grid[s]
//      grid[s] = d;
//      foreach (int p in Utilities.SquarePeers[s])
//      {
//        if (!Eliminate(grid, p, d))
//        { // If we can't eliminate d from all peers of s, then fail
//          return null;
//        }
//      }
//      return grid;
//    }

//    /** Initialize a grid from a puzzle.
//     ** First initialize every square in the new grid to ALL_DIGITS, meaning any value is possible.
//     ** Then, call `fill` on the puzzle's filled squares to initiate constraint propagation.  **/
//    int[] Initialize(int[] puzzle)
//    {
//      int[] grid = new int[N * N];
//      System.Array.Fill(grid, ALL_DIGITS);
//      foreach (int s in Utilities.Squares) { if (puzzle[s] != ALL_DIGITS) { Fill(grid, s, puzzle[s]); } }
//      return grid;
//    }

//    /** Search for a solution to grid. If there is an unfilled square, select one
//     ** and try--that is, search recursively--every possible digit for the square. **/
//    int[] Search(int[] grid, int[][] gridpool, int level)
//    {
//      if (grid == null)
//      {
//        return null;
//      }
//      int s = SelectSquare(grid);
//      if (s == -1)
//      {
//        return grid; // No squares to select means we are done!
//      }
//      foreach (int d in Bits)
//      {
//        // For each possible digit d that could fill square s, try it
//        if ((d & grid[s]) > 0)
//        {
//          // Copy grid's contents into gridpool[level], and use that at the next level
//          System.Array.Copy(grid, 0, gridpool[level], 0, grid.Length);
//          int[] result = Search(Fill(gridpool[level], s, d), gridpool, level + 1);
//          if (result != null)
//          {
//            return result;
//          }
//          backtracks += 1;
//        }
//      }
//      return null;
//    }


//    /** Solve a list of puzzles in a single thread. 
//     ** repeat -R<number> times; print each puzzle's stats if -p; print grid if -g; verify if -v. **/
//    public void Solve(int[] grid)
//    {
//      int[] puzzle = new int[N * N]; // Used to save a copy of the original grid
//      int[][] gridpool = CreateJaggedArray2(N * N, N * N); // Reuse grids during the search

//      System.Array.Copy(grid, 0, puzzle, 0, grid.Length);

//      for (int i = 0; i < repeat; ++i)
//      {
//        //long startTime = printPuzzleStats ? System.Diagnostics.Stopwatch.GetTimestamp() : 0;

//        int[] solution = Initialize(grid);                        // All the real work is

//        if (runSearch)
//          solution = Search(solution, gridpool, 0); // on these 2 lines.

//        //if (printPuzzleStats)
//        //  PrintStats(1, startTime, "Puzzle ");

//        if (i == 0 && (printGrid || (verifySolution && !Verify(solution, puzzle))))
//          PrintGrids("Puzzle ", grid, solution);
//      }
//    }

//    /** Verify that grid is a solution to the puzzle. **/
//    bool Verify(int[] grid, int[] puzzle)
//    {
//      if (grid == null) { return false; }
//      // Check that all squares have a single digit, and
//      // no filled square in the puzzle was changed in the solution.
//      foreach (int s in Utilities.Squares)
//      {
//        if ((grid[s].GetPopCount() != 1) || (puzzle[s].GetPopCount() == 1 && grid[s] != puzzle[s]))
//        {
//          return false;
//        }
//      }
//      // Check that each unit is a permutation of digits
//      foreach (var u in Utilities.UnitSquares)
//      {
//        int unit_digits = 0; // All the digits in a unit.
//        foreach (int s in u) { unit_digits |= grid[s]; }
//        if (unit_digits != ALL_DIGITS)
//        {
//          return false;
//        }
//      }
//      return true;
//    }

//    /// <summary>
//    /// <para>Choose an unfilled square with the minimum number of possible values.</para>
//    /// <para>If all squares are filled, return -1 (which means the puzzle is complete).</para>
//    /// </summary>
//    /// <param name="grid"></param>
//    /// <returns></returns>
//    public int SelectSquare(int[] grid)
//    {
//      int square = -1;
//      int min = N + 1;

//      foreach (var s in Utilities.Squares)
//      {
//        int c = grid[s].GetPopCount();
//        if (c == 2)
//        {
//          return s; // Can't get fewer than 2 possible digits
//        }
//        else if (c > 1 && c < min)
//        {
//          square = s;
//          min = c;
//        }
//      }
//      return square;
//    }


//    bool headerPrinted = false;

//    ///** Print stats on puzzles solved, average time, frequency, threads used, and name. **/
//    //void PrintStats(int nGrids, long startTime, String name)
//    //{
//    //  double usecs = (System.Diagnostics.Stopwatch.GetTimestamp() - startTime) / 1000.0;
//    //  string line = string.Format("{0} {1} {2} {3} {4} {5}",
//    //                nGrids, usecs / nGrids, 1000 * nGrids / usecs, nThreads, backtracks * 1.0 / nGrids, name);
//    //  lock (this)
//    //  { // So that printing from different threads doesn't get garbled
//    //    if (!headerPrinted)
//    //    {
//    //      System.Console.WriteLine("Puzzles   μsec     KHz Threads Backtracks Name\n"
//    //                       + "======= ====== ======= ======= ========== ====");
//    //      headerPrinted = true;
//    //    }
//    //    System.Console.WriteLine(line);
//    //    backtracks = 0;
//    //  }
//    //}

//    /** Print the original puzzle grid and the solution grid. **/
//    void PrintGrids(String name, int[] puzzle, int[] solution)
//    {
//      System.Console.WriteLine($"{name} {(Verify(solution, puzzle) ? "Solution:" : "FAILED: ")}");
//      System.Console.WriteLine(Utilities.ToConsoleString(puzzle));
//      if (solution == null) solution = new int[N * N];
//      System.Console.WriteLine(Utilities.ToConsoleString(solution));
//      return;
//      String bar = "------+-------+------";
//      String gap = "      "; // Space between the puzzle grid and solution grid
//      if (solution == null) solution = new int[N * N];
//      lock (this)
//      { // So that printing from different threads doesn't get garbled
//        System.Console.Write("\n%-22s%s%s\n", name + ":", gap,
//                        (Verify(solution, puzzle) ? "Solution:" : "FAILED:"));
//        for (int r = 0; r < N; ++r)
//        {
//          System.Console.WriteLine(rowString(puzzle, r) + gap + rowString(solution, r));
//          if (r == 2 || r == 5) System.Console.WriteLine(bar + gap + " " + bar);
//        }
//      }
//    }


//    string rowString(int[] grid, int r)
//    {
//      String row = "";
//      for (int s = r * 9; s < (r + 1) * 9; ++s)
//      {
//        row += (char)((grid[s].GetPopCount() == 9) ? '.' : (grid[s].GetPopCount() != 1) ? '?' :
//                       ('1' + grid[s].GetTrailingZeroCount()));
//        row += (s % 9 == 2 || s % 9 == 5 ? " | " : " ");
//      }
//      return row;
//    }
//  }
//}
