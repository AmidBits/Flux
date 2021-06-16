namespace Flux.Model.Maze
{
  public class RecursiveDivisionMaze
    : AMaze
  {
    public override void CarveMaze(Grid grid)
    {
      if (grid is null) throw new System.ArgumentNullException(nameof(grid));

      Divide(grid, 0, 0, grid.Size.Height, grid.Size.Width);
    }

    private void Divide(Grid grid, int row, int column, int height, int width)
    {
      //if (height < 5 && width < 5 && _random.Next(4) == 0)
      if (height <= 1 || width <= 1)
        return;

      if (height > width)
        DivideHorizontally(grid, row, column, height, width);
      else
        DivideVertically(grid, row, column, height, width);
    }
    private void DivideHorizontally(Grid grid, int row, int column, int height, int width)
    {
      var divideSouthOf = Rng.Next(height - 1);
      var passageAt = Rng.Next(width);

      for (var i = 0; i < width; i++)
      {
        if (i != passageAt)
        {
          var cell = grid[row + divideSouthOf, column + i];

          cell.DisconnectPath(cell.Edges[(int)EightWindCompassRose.S], true);
        }
      }

      Divide(grid, row, column, divideSouthOf + 1, width);
      Divide(grid, row + divideSouthOf + 1, column, height - divideSouthOf - 1, width);
    }
    private void DivideVertically(Grid grid, int row, int column, int height, int width)
    {
      var divideEastOf = Rng.Next(width - 1);
      var passageAt = Rng.Next(height);

      for (var i = 0; i < height; i++)
      {
        if (i != passageAt)
        {
          var cell = grid[row + i, column + divideEastOf];

          cell.DisconnectPath(cell.Edges[(int)EightWindCompassRose.E], true);
        }
      }

      Divide(grid, row, column, height, divideEastOf + 1);
      Divide(grid, row, column + divideEastOf + 1, height, width - divideEastOf - 1);
    }
  }
}
