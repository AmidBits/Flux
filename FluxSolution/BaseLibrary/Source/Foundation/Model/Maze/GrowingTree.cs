using System.Linq;

namespace Flux.Model.Maze
{
  public sealed class GrowingTreeMaze
    : AMaze
  {
    public System.Func<System.Collections.Generic.IEnumerable<Cell>, System.Random, Cell> Selector { get; set; } = (list, rng) =>
    {
      list.TryGetRandomElement(out var element, rng);

      return element; // Prim's algorithm by default
    };

    public override void CarveMaze(Grid grid)
    {
      if (grid is null) throw new System.ArgumentNullException(nameof(grid));

      var active = new System.Collections.Generic.List<Cell> { Selector(grid.GetValues(), RandomNumberGenerator) };

      while (active.Any())
      {
        var cell = Selector(active, RandomNumberGenerator);

        var unvisited = cell.GetEdgesWithoutPaths();
        if (unvisited.Any())
        {
          unvisited.TryGetRandomElement(out var element, RandomNumberGenerator);

          active.Add(cell.ConnectPath(element, true));
        }
        else
          active.Remove(cell);
      }
    }
  }
}
