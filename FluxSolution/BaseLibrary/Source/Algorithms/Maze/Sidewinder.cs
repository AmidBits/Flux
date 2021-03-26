using System.Linq;

namespace Flux.Model.Maze
{
  public class SidewinderMaze
    : AMaze
  {
    private EightWindCompassRose m_diagonal = EightWindCompassRose.NE;
    public EightWindCompassRose Diagonal
    {
      get { return m_diagonal; }
      set
      {
        switch (value)
        {
          case EightWindCompassRose.NE:
          case EightWindCompassRose.NW:
          case EightWindCompassRose.SE:
          case EightWindCompassRose.SW:
            m_diagonal = value;
            break;
          default:
            throw new System.ArgumentOutOfRangeException(nameof(value), "Must be a diagonal.");
        }
      }
    }

    public override void CarveMaze(Grid grid)
    {
      if (grid is null) throw new System.ArgumentNullException(nameof(grid));

      var direction1 = (int)m_diagonal - 45;
      var direction2 = (int)m_diagonal + 45;

      var run = new System.Collections.Generic.List<Cell>();

      for (int r = 0; r < grid.Size.Height; r++)
      {
        run.Clear();

        foreach (var cell in grid.Values.Skip(r * grid.Size.Width).Take(grid.Size.Width))
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
            cell.ConnectPath(cell.Edges[(int)EightWindCompassRose.E], true);
        }
      }
    }
  }
}
