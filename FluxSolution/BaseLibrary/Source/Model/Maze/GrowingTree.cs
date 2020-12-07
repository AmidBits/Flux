using System.Linq;

namespace Flux.Model.Maze
{
  public class GrowingTreeMaze
    : AMaze
  {
    public System.Func<System.Collections.Generic.IList<Cell>, System.Random, Cell> Selector { get; set; } = (list, random) =>
    {
      list.RandomElement(out var element);

      return element; // Prim's algorithm by default
    };

    public override void CarveMaze(Grid grid)
    {
      if (grid is null) throw new System.ArgumentNullException(nameof(grid));

      var active = new System.Collections.Generic.List<Cell> { Selector(grid.Values, Rng) };

      while (active.Any())
      {
        var cell = Selector(active, Rng);

        var unvisited = cell.GetEdgesWithoutPaths();
        if (unvisited.Any())
        {
          unvisited.RandomElement(out var element, Rng);

          active.Add(cell.ConnectPath(element, true));
        }
        else
          active.Remove(cell);
      }
    }
  }
}
