namespace Flux.Model.Maze
{
  public sealed class WalkerCave
    : AMaze
  {
    public int RemovalCount = 7;

    public override void CarveMaze(MazeGrid grid)
    {
      System.ArgumentNullException.ThrowIfNull(grid);

      var counter = grid.Size.X * grid.Size.Y / RemovalCount;

      var current = grid[grid.Size.X / 2, grid.Size.Y / 2];

      while (counter > 0)
      {
        var next = current.Edges.Random(RandomNumberGenerator).Value;

        if (current.GetEdgesWithoutPaths().Contains(next))
        {
          foreach (var e in current.Edges)
            current.ConnectPath(e.Value, false);

          counter--;
        }

        current = next;
      }
    }
  }
}
