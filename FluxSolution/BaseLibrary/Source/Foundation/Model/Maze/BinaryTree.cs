using System.Linq;

namespace Flux.Model.Maze
{
  public class BinaryTreeMaze
    : AMaze
  {
    public InterCardinalDirection Diagonal { get; set; } = InterCardinalDirection.NE;

    public override void CarveMaze(Grid grid)
    {
      if (grid is null) throw new System.ArgumentNullException(nameof(grid));

      var direction1 = (int)Diagonal - 4;
      var direction2 = (direction1 + 8) % 32;

      foreach (var cell in grid.GetValues())
      {
        var direction = cell.Edges.Where(kvp => kvp.Key == direction1 || kvp.Key == direction2);

        if (direction.Any())
        {
          direction.RandomElement(out var element, Rng);

          cell.ConnectPath(element.Value, true);
        }
      }
    }
  }
}