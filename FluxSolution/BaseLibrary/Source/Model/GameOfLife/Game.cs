﻿namespace Flux.Model.GameOfLife
{
  /// <summary>Plays Conway's Game of Life on the console with a random initial state.</summary>
  public class Game
  {
    private System.Collections.BitArray m_deadOrAlive;
    private readonly bool m_canLifeLogicWrapAroundEdges;
    private readonly Geometry.Size2 m_cellGrid;

    public Game(Geometry.Size2 cellGrid, bool canLifeLogicWrapAroundEdges, double probabilityOfBeingInitiallyAlive)
    {
      m_deadOrAlive = new System.Collections.BitArray(cellGrid.Height * cellGrid.Width);
      m_canLifeLogicWrapAroundEdges = canLifeLogicWrapAroundEdges;
      m_cellGrid = cellGrid;

      var random = new Flux.Randomization.Xoshiro256SS();

      for (var r = m_cellGrid.Height - 1; r >= 0; r--)
      {
        for (var c = m_cellGrid.Width - 1; c >= 0; c--)
        {
          var index = (int)Geometry.Point2.ToUniqueIndex(c, r, m_cellGrid);

          m_deadOrAlive[index] = random.NextDouble() < probabilityOfBeingInitiallyAlive;
        }
      }
    }
    public Game(int height, int width, bool canLifeLogicWrapAroundEdges, double probabilityOfBeingInitiallyAlive)
      : this(new Geometry.Size2(height, width), canLifeLogicWrapAroundEdges, probabilityOfBeingInitiallyAlive)
    { }

    public Geometry.Size2 CellGrid
      => m_cellGrid;

    /// <summary>Moves the board to the next state based on Conway's rules.</summary>
    public void Update()
    {
      var array = new System.Collections.BitArray(m_cellGrid.Height * m_cellGrid.Width);

      for (var r = m_cellGrid.Height - 1; r >= 0; r--)
      {
        for (var c = m_cellGrid.Width - 1; c >= 0; c--)
        {
          var index = (int)m_cellGrid.PointToUniqueIndex(c, r);

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
        if (!m_canLifeLogicWrapAroundEdges && (y + r < 0 || y + r >= m_cellGrid.Height))
          continue;

        var y1 = (y + r + m_cellGrid.Height) % m_cellGrid.Height; // Loop around the edges if y+j is off the board.

        for (var c = -1; c <= 1; c++) // Loop "columns".
        {
          if (!m_canLifeLogicWrapAroundEdges && (x + c < 0 || x + c >= m_cellGrid.Width))
            continue;

          var x1 = (x + c + m_cellGrid.Width) % m_cellGrid.Width; // Loop around the edges if x+i is off the board.

          var pointIndex = (int)Geometry.Point2.ToUniqueIndex(x1, y1, m_cellGrid);

          cn += m_deadOrAlive[pointIndex] ? 1 : 0;
        }
      }

      var positionIndex = (int)m_cellGrid.PointToUniqueIndex(x, y);

      cn -= m_deadOrAlive[positionIndex] ? 1 : 0;

      return cn;
    }

    public System.Collections.Generic.IEnumerable<string> ToConsoleStrings()
    {
      for (var y = 0; y < m_cellGrid.Height; y++)
      {
        var sb = new System.Text.StringBuilder();

        for (var x = 0; x < m_cellGrid.Width; x++)
        {
          var index = (int)Geometry.Point2.ToUniqueIndex(x, y, m_cellGrid);

          var c = m_deadOrAlive[index] ? '\u2588' : ' ';

          sb.Append(c);
          sb.Append(c); // Each cell is two characters wide for symmetrical visual.
        }

        yield return sb.ToString();
      }
    }
    public string ToConsoleBlock()
      => string.Join(System.Environment.NewLine, ToConsoleStrings());
  }
}