namespace Flux.Model.Maze
{
  public abstract class AMaze
    : IMazeBraidable, IMazeCarvable
  {
    public System.Random RandomNumberGenerator { get; set; } = new System.Random();

    public virtual void BraidMaze(MazeGrid grid, double threshold = 0.5)
    {
      if (grid is null) throw new System.ArgumentNullException(nameof(grid));

      foreach (var cell in grid.GetDeadEnds().ToList())
      {
        if (RandomNumberGenerator.NextDouble() <= threshold)
        {
          var unlinkedNeighbors = cell.Edges.Where(kvp => !cell.Paths.ContainsValue(kvp.Value));

          var preferredNeighbors = unlinkedNeighbors.OrderBy(kvp => kvp.Value.Paths.Count);

          if (preferredNeighbors.Any())
          {
            preferredNeighbors.TryRandom(out var neighbor, RandomNumberGenerator);

            cell.ConnectPath(neighbor.Value, true);
          }
        }
      }
    }

    public abstract void CarveMaze(MazeGrid grid);
  }
}
