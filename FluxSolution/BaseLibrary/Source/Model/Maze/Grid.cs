using System.Linq;

namespace Flux.Model.Maze
{
  public class Grid
    : AGrid<Cell>, System.ICloneable
  {
    public Grid(Media.Geometry.Size2 size)
      : base(size.Height, size.Width)
    {
      for (var h = size.Height - 1; h >= 0; h--)
        for (var w = size.Width - 1; w >= 0; w--)
          this[h, w] = new Cell();
      // Instantiate each cell?
    }

    /// <summary>Returns a sequence with all dead end (singly linked) cells.</summary>
    public System.Collections.Generic.IEnumerable<Cell> GetDeadEnds()
      => GetValues().Where(cell => cell.Paths.Count == 1);

    /// <summary>Reset edges with one optional 4-way N,E,S,W and/or one 4-way NE,SE,SW,NW.</summary>
    public void ResetEdges(bool orthogonal, bool diagonal)
    {
      for (var y = 0; y < Size.Height; y++) // 
      {
        for (var x = 0; x < Size.Width; x++)
        {
          var cell = this[y, x];

          cell.Edges.Clear();

          var n = (y > 0); // North (positive vertical axis).
          var e = (x < (Size.Width - 1)); // East (negative horizontal axis).
          var s = (y < (Size.Height - 1)); // South (negative vertical axis).
          var w = (x > 0); // West (positive horizontal axis).

          if (orthogonal)
          {
            if (n) cell.Edges.Add((int)Media.EightWindCompassRose.N, this[y - 1, x]);
            if (e) cell.Edges.Add((int)Media.EightWindCompassRose.E, this[y, x + 1]);
            if (s) cell.Edges.Add((int)Media.EightWindCompassRose.S, this[y + 1, x]);
            if (w) cell.Edges.Add((int)Media.EightWindCompassRose.W, this[y, x - 1]);
          }

          if (diagonal)
          {
            if (n && e) cell.Edges.Add((int)Media.EightWindCompassRose.NE, this[y - 1, x + 1]);
            if (s && e) cell.Edges.Add((int)Media.EightWindCompassRose.SE, this[y + 1, x + 1]);
            if (s && w) cell.Edges.Add((int)Media.EightWindCompassRose.SW, this[y + 1, x - 1]);
            if (n && w) cell.Edges.Add((int)Media.EightWindCompassRose.NW, this[y - 1, x - 1]);
          }
        }
      }
    }

    /// <summary>Reset all pathway connection states as either connected or not.</summary>
    public void ResetPaths(bool isConnected)
    {
      foreach (var cell in GetValues())
      {
        cell.Paths.Clear();

        if (isConnected)
          foreach (var edge in cell.Edges)
            cell.Paths[edge.Key] = edge.Value;
      }
    }

    // System.ICloneable
    public object Clone()
    {
      var grid = new Grid(Size);

      for (var index = 0; index < grid.Count; index++)
        grid.SetValue(index, GetValue(index));

      return grid;
    }
  }
}
