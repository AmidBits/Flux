using System.Linq;

namespace Flux.Model.Maze
{
  public class WilsonsMaze
    : AMaze
  {
    public override void CarveMaze(Grid grid)
    {
      if (grid is null) throw new System.ArgumentNullException(nameof(grid));

      var path = new System.Collections.Generic.List<Cell>();

      var unvisited = grid.GetValues().ToList();
      unvisited.RandomElement(out var unvisitedElement, Rng);
      unvisited.Remove(unvisitedElement);

      Cell cell;

      while (unvisited.Any())
      {
        unvisited.RandomElement(out cell, Rng);

        path.Clear();
        path.Add(cell);

        while (unvisited.Contains(cell))
        {
          cell.Edges.Select(kvp => kvp.Value).RandomElement(out cell, Rng);

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
