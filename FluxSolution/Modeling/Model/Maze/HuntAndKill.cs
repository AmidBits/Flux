using System.Linq;

namespace Flux.Model.Maze
{
  public sealed class HuntAndKillMaze
    : AMaze
  {
    public override void CarveMaze(MazeGrid grid)
    {
      System.ArgumentNullException.ThrowIfNull(grid);

      grid.Values.TryGetRandomElement(out var current, RandomNumberGenerator);

      while (current != null)
      {
        var unvisited = current.GetEdgesWithoutPaths();

        if (unvisited.Any())
        {
          unvisited.TryGetRandomElement(out var unvisitedElement, RandomNumberGenerator);

          current = current.ConnectPath(unvisitedElement, true);
        }
        else
        {
          current = null;

          foreach (var cell in grid.Values.Where(c => !c.Paths.Any()))
          {
            var visited = cell.GetEdgesWithPaths();

            if (visited.Any())
            {
              visited.TryGetRandomElement(out var visitedElement, RandomNumberGenerator);

              cell.ConnectPath(visitedElement, true);

              current = cell;

              break;
            }
          }
        }
      }
    }
  }
}
