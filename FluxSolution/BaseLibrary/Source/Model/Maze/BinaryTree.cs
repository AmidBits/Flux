using System.Linq;

namespace Flux.Model.Maze
{
  public class BinaryTreeMaze
    : AMaze
  {
    private InterCardinalDirection m_diagonal = InterCardinalDirection.NE;
    public InterCardinalDirection Diagonal
    {
      get { return m_diagonal; }
      set
      {
        switch (value)
        {
          case InterCardinalDirection.NE:
          case InterCardinalDirection.NW:
          case InterCardinalDirection.SE:
          case InterCardinalDirection.SW:
            m_diagonal = value;
            break;
          default:
            throw new System.ArgumentOutOfRangeException(nameof(value), @"Must be a diagonal.");
        }
      }
    }

    public override void CarveMaze(Grid grid)
    {
      if (grid is null) throw new System.ArgumentNullException(nameof(grid));

      var direction1 = (int)m_diagonal - 45;
      var direction2 = (int)m_diagonal + 45;

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
