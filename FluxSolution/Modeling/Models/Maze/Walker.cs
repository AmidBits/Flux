namespace Flux.Model.Maze
{
  public sealed class WalkerCave
    : AMaze
  {
    public int RemovalCount = 2;

    public override void CarveMaze(MazeGrid grid)
    {
      if (grid is null) throw new System.ArgumentNullException(nameof(grid));

      var counter = grid.Size.X * grid.Size.Y / RemovalCount;

      var current = grid[grid.Size.X / 2, grid.Size.Y / 2];

      while (counter > 0)
      {
        var next = current.Edges.RandomElement(RandomNumberGenerator).Value;

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
