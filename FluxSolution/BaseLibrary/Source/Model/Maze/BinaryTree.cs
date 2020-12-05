using System.Linq;

namespace Flux.Model.Maze
{
  public class BinaryTreeMaze
    : AMaze
  {
    private EightWindCompassRose _diagonal = EightWindCompassRose.NE;
    public EightWindCompassRose Diagonal
    {
      get { return _diagonal; }
      set
      {
        _diagonal = value switch
        {
          EightWindCompassRose.NE or EightWindCompassRose.NW or EightWindCompassRose.SE or EightWindCompassRose.SW => value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(value), @"Must be a diagonal."),
        };
      }
    }

    public override void CarveMaze(Grid grid)
    {
      if (grid is null) throw new System.ArgumentNullException(nameof(grid));

      var direction1 = (int)_diagonal - 45;
      var direction2 = (int)_diagonal + 45;

      foreach (var cell in grid.Values)
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
