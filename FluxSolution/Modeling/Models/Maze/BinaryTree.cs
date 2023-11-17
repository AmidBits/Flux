using System.Linq;

namespace Flux.Model.Maze
{
  public sealed class BinaryTreeMaze
    : AMaze
  {
    public Units.InterCardinalDirection Diagonal { get; set; } = Units.InterCardinalDirection.NE;

    public override void CarveMaze(MazeGrid grid)
    {
      System.ArgumentNullException.ThrowIfNull(grid);

      var direction1 = (int)Diagonal - 4;
      var direction2 = (direction1 + 8) % 32;

      foreach (var cell in grid.GetValues())
      {
        var direction = cell.Edges.Where(kvp => kvp.Key == direction1 || kvp.Key == direction2);

        if (direction.Any())
        {
          direction.TryRandom(out var element, RandomNumberGenerator);

          cell.ConnectPath(element.Value, true);
        }
      }
    }
  }
}
