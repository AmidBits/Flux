using System.Linq;

namespace Flux.Model.Maze
{
  public class AldusBroderMaze
    : AMaze
  {
    public override void CarveMaze(Grid grid)
    {
      if (grid is null) throw new System.ArgumentNullException(nameof(grid));

      grid.GetValues().RandomElement(out var current, Rng);

      var unvisitedCount = grid.Count - 1;

      while (unvisitedCount > 0)
      {
        current.Edges.Select(kvp => kvp.Value).RandomElement(out var neighbor, Rng);

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
