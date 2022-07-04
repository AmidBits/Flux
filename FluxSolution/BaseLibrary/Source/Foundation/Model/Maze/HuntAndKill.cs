using System.Linq;

namespace Flux.Model.Maze
{
  public sealed class HuntAndKillMaze
    : AMaze
  {
    public override void CarveMaze(Grid grid)
    {
      if (grid is null) throw new System.ArgumentNullException(nameof(grid));

      grid.GetValues().TryGetRandomElement(out var current, RandomNumberGenerator);

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

          foreach (var cell in grid.GetValues().Where(c => !c.Paths.Any()))
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
