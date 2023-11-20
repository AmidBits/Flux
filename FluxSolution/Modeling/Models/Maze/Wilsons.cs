namespace Flux.Model.Maze
{
  public sealed class WilsonsMaze
    : AMaze
  {
    public override void CarveMaze(MazeGrid grid)
    {
      System.ArgumentNullException.ThrowIfNull(grid);

      RandomNumberGenerator = new System.Random(23);

      var path = new System.Collections.Generic.List<Cell>();

      var unvisited = grid.GetValues().ToList();
      unvisited.TryRandom(out var unvisitedElement, RandomNumberGenerator);
      unvisited.Remove(unvisitedElement);

      while (unvisited.Any())
      {
        unvisited.TryRandom(out Cell cell, RandomNumberGenerator);

        path.Clear();
        path.Add(cell);

        while (unvisited.Contains(cell))
        {
          cell.Edges.Select(kvp => kvp.Value).TryRandom(out cell, RandomNumberGenerator);

          if (path.IndexOf(cell) is int position && position > -1)
            path.RemoveRange(position, path.Count - position);
          else
            path.Add(cell);

          for (int i = 0; i < path.Count - 1; i++)
          {
            path[i].ConnectPath(path[i + 1], true);

            unvisited.Remove(path[i]);
          }
        }
      }
    }
  }
}
