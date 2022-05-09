using System.Linq;

namespace Flux.Model.Maze
{
  public sealed class GrowingTreeMaze
    : AMaze
  {
    public System.Func<System.Collections.Generic.IEnumerable<Cell>, System.Random, Cell> Selector { get; set; } = (list, random) =>
    {
      list.TryGetRandomElement(out var element);

      return element; // Prim's algorithm by default
    };

    public override void CarveMaze(Grid grid)
    {
      if (grid is null) throw new System.ArgumentNullException(nameof(grid));

      var active = new System.Collections.Generic.List<Cell> { Selector(grid.GetValues(), Rng) };

      while (active.Any())
      {
        var cell = Selector(active, Rng);

        var unvisited = cell.GetEdgesWithoutPaths();
        if (unvisited.Any())
        {
          unvisited.TryGetRandomElement(out var element, Rng);

          active.Add(cell.ConnectPath(element, true));
        }
        else
          active.Remove(cell);
      }
    }
  }
}
