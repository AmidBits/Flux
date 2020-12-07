using System.Linq;

namespace Flux.Model.Maze
{
  public class HuntAndKillMaze
    : AMaze
  {
    public override void CarveMaze(Grid grid)
    {
      if (grid is null) throw new System.ArgumentNullException(nameof(grid));

      grid.Values.RandomElement(out var current, Rng);

      while (current != null)
      {
        var unvisited = current.GetEdgesWithoutPaths();
        if (unvisited.Any())
        {
          unvisited.RandomElement(out var unvisitedElement, Rng);

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
              visited.RandomElement(out var visitedElement, Rng);

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
