using System.Linq;

namespace Flux.Model.Maze
{
  public class BinaryTreeMaze
    : AMaze
  {
    private int m_diagonal = (int)InterCardinalDirection.NE;
    public InterCardinalDirection Diagonal
    {
      get { return (InterCardinalDirection)m_diagonal; }
      set { m_diagonal = (int)value; }
    }

    public override void CarveMaze(Grid grid)
    {
      if (grid is null) throw new System.ArgumentNullException(nameof(grid));

      var direction1 = m_diagonal - 4;
      var direction2 = (m_diagonal + 4) % 32;

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
