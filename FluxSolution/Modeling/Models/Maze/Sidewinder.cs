using System;

namespace Flux.Model.Maze
{
  public sealed class SidewinderMaze
    : AMaze
  {
    public Units.InterCardinalDirection Diagonal { get; set; } = Units.InterCardinalDirection.NE;

    public override void CarveMaze(MazeGrid grid)
    {
      if (grid is null) throw new System.ArgumentNullException(nameof(grid));

      var direction1 = (int)Diagonal - 4;
      var direction2 = (direction1 + 8) % 32;

      var run = new System.Collections.Generic.List<Cell>();

      for (int r = 0; r < grid.Size.Y; r++)
      {
        run.Clear();

        foreach (var cell in grid.GetValues().Skip(r * grid.Size.X).Take(grid.Size.X))
        {
          run.Add(cell);

          var atBoundary1 = !cell.Edges.ContainsKey(direction1);
          var atBoundary2 = !cell.Edges.ContainsKey(direction2);

          if (atBoundary2 || (!atBoundary1 && RandomNumberGenerator.Next(2) == 0)) // should close out
          {
            if (run.AsSpan().AsReadOnlySpan().TryRandomElement(out var member, RandomNumberGenerator) && member.Edges.TryGetValue(direction1, out var edgeCell))
              member.ConnectPath(edgeCell, true);

            run.Clear();
          }
          else
            cell.ConnectPath(cell.Edges[(int)Units.CardinalDirection.E], true);
        }
      }
    }
  }
}
