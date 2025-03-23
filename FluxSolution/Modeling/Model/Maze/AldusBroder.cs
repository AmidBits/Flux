namespace Flux.Model.Maze
{
  public sealed class AldusBroderMaze
    : AMaze
  {
    public override void CarveMaze(MazeGrid grid)
    {
      System.ArgumentNullException.ThrowIfNull(grid);

      grid.Values.TryRandom(out var current, RandomNumberGenerator);

      var unvisitedCount = grid.Count - 1;

      while (unvisitedCount > 0)
      {
        current.Edges.Select(kvp => kvp.Value).TryRandom(out var neighbor, RandomNumberGenerator);

        if (!neighbor.Paths.Any())
        {
          current.ConnectPath(neighbor, true);

          unvisitedCount--;
        }

        current = neighbor;
      }
    }
  }
}
