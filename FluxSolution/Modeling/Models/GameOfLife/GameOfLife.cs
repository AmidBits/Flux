using Flux.Numerics;

namespace Flux.Model
{
  /// <summary>Plays Conway's Game of Life on the console with a random initial state.</summary>
  public sealed class GameOfLife
  {
    private System.Collections.BitArray m_deadOrAlive;
    private readonly bool m_canLifeLogicWrapAroundEdges;
    private readonly Numerics.CartesianCoordinate2<int> m_cellGrid;

    public GameOfLife(Numerics.CartesianCoordinate2<int> cellGrid, bool canLifeLogicWrapAroundEdges, double probabilityOfBeingInitiallyAlive)
    {
      m_deadOrAlive = new System.Collections.BitArray(cellGrid.Y * cellGrid.X);
      m_canLifeLogicWrapAroundEdges = canLifeLogicWrapAroundEdges;
      m_cellGrid = cellGrid;

      var random = new Flux.Random.Xoshiro256SS();

      for (var r = m_cellGrid.Y - 1; r >= 0; r--)
      {
        for (var c = m_cellGrid.X - 1; c >= 0; c--)
        {
          var index = Convert.Cartesian2ToMapIndex(c, r, m_cellGrid.X);

          m_deadOrAlive[index] = random.NextDouble() < probabilityOfBeingInitiallyAlive;
        }
      }
    }
    public GameOfLife()
      : this(new Numerics.CartesianCoordinate2<int>(40, 20), true, 0.25)
    {
    }

    public Numerics.CartesianCoordinate2<int> CellGrid
      => m_cellGrid;

    /// <summary>Moves the board to the next state based on Conway's rules.</summary>
    public void Update()
    {
      var array = new System.Collections.BitArray(m_cellGrid.Y * m_cellGrid.X);

      for (var r = m_cellGrid.Y - 1; r >= 0; r--)
      {
        for (var c = m_cellGrid.X - 1; c >= 0; c--)
        {
          var index = Convert.Cartesian2ToMapIndex(c, r, m_cellGrid.X);

          var state = m_deadOrAlive[index];

          var count = CountLiveNeighbors(c, r);

          // A live cell dies unless it has exactly 2 or 3 live neighbors. A dead cell comes to life if it has exactly 3 live neighbors. Otherwise the cell is dead.
          array[index] = (state && (count == 2 || count == 3)) || (!state && count == 3);
        }
      }

      m_deadOrAlive = array;
    }

    /// <summary>Returns the number of live neighbors around the cell at position (x,y).</summary>
    private int CountLiveNeighbors(int x, int y)
    {
      var cn = 0;

      for (var r = -1; r <= 1; r++) // Loop "rows".
      {
        if (!m_canLifeLogicWrapAroundEdges && (y + r < 0 || y + r >= m_cellGrid.Y))
          continue;

        var y1 = (y + r + m_cellGrid.Y) % m_cellGrid.Y; // Loop around the edges if y+j is off the board.

        for (var c = -1; c <= 1; c++) // Loop "columns".
        {
          if (!m_canLifeLogicWrapAroundEdges && (x + c < 0 || x + c >= m_cellGrid.X))
            continue;

          var x1 = (x + c + m_cellGrid.X) % m_cellGrid.X; // Loop around the edges if x+i is off the board.

          var pointIndex = Convert.Cartesian2ToMapIndex(x1, y1, m_cellGrid.X);

          cn += m_deadOrAlive[pointIndex] ? 1 : 0;
        }
      }

      var positionIndex = Convert.Cartesian2ToMapIndex(x, y, m_cellGrid.X);

      cn -= m_deadOrAlive[positionIndex] ? 1 : 0;

      return cn;
    }

    public System.Collections.Generic.IEnumerable<string> ToConsoleStrings()
    {
      for (var y = 0; y < m_cellGrid.Y; y++)
      {
        var sb = new System.Text.StringBuilder();

        for (var x = 0; x < m_cellGrid.X; x++)
        {
          var index = Convert.Cartesian2ToMapIndex(x, y, m_cellGrid.X);

          var c = m_deadOrAlive[index] ? '\u2588' : ' ';

          sb.Append(c);
          sb.Append(c); // Each cell is two characters wide for symmetrical visual.
        }

        yield return sb.ToString();
      }
    }
    public string ToConsoleBlock()
      => string.Join(System.Environment.NewLine, ToConsoleStrings());

    public void RunInConsole(int millisecondsFrameFreeze = 100)
    {
      Setup();

      while (!System.Console.KeyAvailable || System.Console.ReadKey(true).Key != System.ConsoleKey.Escape) // Run the game until the Escape key is pressed.
      {
        Draw();

        System.Threading.Thread.Sleep(millisecondsFrameFreeze);

        Update();
      }

      /// <summary>Setup the Console.</summary>
      void Setup()
      {
        System.Console.CursorVisible = false;

        // Pad +1 to prevent scrolling when drawing the board.

        var width = 2 * System.Math.Max(CellGrid.X, 8) + 1; // Multiply by 2 for a symmetrical double-width for each cell.
        var height = System.Math.Max(CellGrid.Y, 8) + 1;

        if (System.OperatingSystem.IsWindows())
        {
          System.Console.SetWindowSize(width, height);
          System.Console.SetBufferSize(width, height);
        }
      }

      void Draw()
      {
        System.Console.SetCursorPosition(0, 0);

        System.Console.Write(ToConsoleBlock());
      }
    }
  }
}
