using System.Linq;

namespace Flux.Model.Maze
{
  public class BinaryTreeMaze
    : AMaze
  {
    private Media.InterCardinalDirection m_diagonal = Media.InterCardinalDirection.NE;
    public Media.InterCardinalDirection Diagonal
    {
      get { return m_diagonal; }
      set
      {
        switch (value)
        {
          case Media.InterCardinalDirection.NE:
          case Media.InterCardinalDirection.NW:
          case Media.InterCardinalDirection.SE:
          case Media.InterCardinalDirection.SW:
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
