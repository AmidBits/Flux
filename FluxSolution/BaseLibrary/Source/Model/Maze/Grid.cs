using System.Linq;

namespace Flux.Model.Maze
{
  public class Grid
    : Grid<Cell>, System.ICloneable
  {
    public Grid(int rows, int columns)
      : base(rows, columns)
    {
    }

    /// <summary>Returns a sequence with all dead end (singly linked) cells.</summary>
    public System.Collections.Generic.IEnumerable<Cell> GetDeadEnds()
      => Values.Where(c => c.Paths.Count == 1);

    /// <summary>Reset edges with one optional 4-way N,E,S,W and/or one 4-way NE,SE,SW,NW.</summary>
    public void ResetEdges(bool orthogonal, bool diagonal)
    {
      for (var r = 0; r < Rows; r++)
      {
        for (var c = 0; c < Columns; c++)
        {
          var cell = this[r, c];

          cell.Edges.Clear();

          if (orthogonal || diagonal)
          {
            var north = (r > 0);
            var east = (c < (Columns - 1));
            var south = (r < (Rows - 1));
            var west = (c > 0);

            if (orthogonal && north) { cell.Edges.Add((int)EightWindCompassRose.N, this[r - 1, c]); }
            if (diagonal && north && east) { cell.Edges.Add((int)EightWindCompassRose.NE, this[r - 1, c + 1]); }
            if (orthogonal && east) { cell.Edges.Add((int)EightWindCompassRose.E, this[r, c + 1]); }
            if (diagonal && south && east) { cell.Edges.Add((int)EightWindCompassRose.SE, this[r + 1, c + 1]); }
            if (orthogonal && south) { cell.Edges.Add((int)EightWindCompassRose.S, this[r + 1, c]); }
            if (diagonal && south && west) { cell.Edges.Add((int)EightWindCompassRose.SW, this[r + 1, c - 1]); }
            if (orthogonal && west) { cell.Edges.Add((int)EightWindCompassRose.W, this[r, c - 1]); }
            if (diagonal && north && west) { cell.Edges.Add((int)EightWindCompassRose.NW, this[r - 1, c - 1]); }
          }
        }
      }
    }

    /// <summary>Reset all pathway connections to either connected state or not.</summary>
    public void ResetPaths(bool asConnected)
    {
      foreach (var cell in Values)
        cell.ResetPaths(asConnected);
    }

    // System.ICloneable
    public object Clone()
      => new Grid(Rows, Columns) { Values = Values.ToArray() };
  }
}
