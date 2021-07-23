using System.Linq;

namespace Flux.Model.Maze
{
  public class SidewinderMaze
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

      var run = new System.Collections.Generic.List<Cell>();

      for (int r = 0; r < grid.Size.Height; r++)
      {
        run.Clear();

        foreach (var cell in grid.GetValues().Skip(r * grid.Size.Width).Take(grid.Size.Width))
        {
          run.Add(cell);

          var atBoundary1 = !cell.Edges.ContainsKey(direction1);
          var atBoundary2 = !cell.Edges.ContainsKey(direction2);

          if (atBoundary2 || (!atBoundary1 && Rng.Next(2) == 0)) // should close out
          {
            if (run.RandomElement(out var member, Rng) && member.Edges.ContainsKey(direction1))
              member.ConnectPath(member.Edges[direction1], true);

            run.Clear();
          }
          else
            cell.ConnectPath(cell.Edges[(int)CardinalDirection.E], true);
        }
      }
    }
  }
}
