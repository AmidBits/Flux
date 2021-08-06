namespace Flux.Model.Gaming.GameOfLife
{
  /// <summary>Plays Conway's Game of Life on the console with a random initial state.</summary>
  public class Game
  {
    public System.Collections.BitArray BitArray { get; private set; }
    public bool CanLoopEdges { get; }
    public Geometry.Size2 GridSize { get; }

    public Game(int height, int width, bool canLoopEdges, double mu = 0.5)
    {
      GridSize = new Geometry.Size2(width, height);
      CanLoopEdges = canLoopEdges;
      BitArray = new System.Collections.BitArray(GridSize.Height * GridSize.Width);

      var random = new Flux.Randomization.Xoshiro256SS();

      for (var r = GridSize.Height - 1; r >= 0; r--)
      {
        for (var c = GridSize.Width - 1; c >= 0; c--)
        {
          var index = (int)Geometry.Point2.ToUniqueIndex(c, r, GridSize);

          BitArray[index] = random.NextDouble() < mu;
        }
      }
    }

    /// <summary>Moves the board to the next state based on Conway's rules.</summary>
    public void Update()
    {
      var array = new System.Collections.BitArray(GridSize.Height * GridSize.Width);

      for (var r = GridSize.Height - 1; r >= 0; r--)
      {
        for (var c = GridSize.Width - 1; c >= 0; c--)
        {
          var point = new Geometry.Point2(c, r);
          var index = (int)Geometry.Point2.ToUniqueIndex(point, GridSize);

          var count = CountLiveNeighbors(point);
          var state = BitArray[index];

          if (state && (count == 2 || count == 3)) // A live cell dies unless it has exactly 2 or 3 live neighbors.
            array[index] = true;
          else if (!state && count == 3) // A dead cell remains dead unless it has exactly 3 live neighbors.
            array[index] = true;
          else
            array[index] = false;
        }
      }

      BitArray = array;
    }

    /// <summary>Returns the number of live neighbors around the cell at position (x,y).</summary>
    private int CountLiveNeighbors(Geometry.Point2 position)
    {
      var cn = 0;

      for (var r = -1; r <= 1; r++) // Loop "rows".
      {
        if (!CanLoopEdges && (position.Y + r < 0 || position.Y + r >= GridSize.Height))
          continue;

        var y = (position.Y + r + GridSize.Height) % GridSize.Height; // Loop around the edges if y+j is off the board.

        for (var c = -1; c <= 1; c++) // Loop "columns".
        {
          if (!CanLoopEdges && (position.X + c < 0 || position.X + c >= GridSize.Width))
            continue;

          var x = (position.X + c + GridSize.Width) % GridSize.Width; // Loop around the edges if x+i is off the board.

          var pointIndex = (int)Geometry.Point2.ToUniqueIndex(x, y, GridSize);

          cn += BitArray[pointIndex] ? 1 : 0;
        }
      }

      var positionIndex = (int)Geometry.Point2.ToUniqueIndex(position, GridSize);

      cn -= BitArray[positionIndex] ? 1 : 0;

      return cn;
    }

    public string ToConsoleBlock()
    {
      var sb = new System.Text.StringBuilder();

      for (var y = 0; y < GridSize.Height; y++)
      {
        for (var x = 0; x < GridSize.Width; x++)
        {
          var index = (int)Geometry.Point2.ToUniqueIndex(x, y, GridSize);

          var c = BitArray[index] ? '\u2588' : ' ';

          sb.Append(c);
          sb.Append(c); // Each cell is two characters wide for symmetrical visual.
        }

        sb.Append('\n');
      }

      return sb.ToString();
    }
  }
}
