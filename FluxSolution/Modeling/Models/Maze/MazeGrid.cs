namespace Flux.Model.Maze
{
  public sealed class MazeGrid
    : Grid<Cell>, System.ICloneable
  {
    public MazeGrid(Geometry.ICartesianCoordinate2<int> size)
      : base(size.Y, size.X)
    {
      for (var h = size.Y - 1; h >= 0; h--)
        for (var w = size.X - 1; w >= 0; w--)
          this[h, w] = new Cell();
      // Instantiate each cell?
    }

    /// <summary>Returns a sequence with all dead end (singly linked) cells.</summary>
    public System.Collections.Generic.IEnumerable<Cell> GetDeadEnds()
      => GetValues().Where(cell => cell.Paths.Count == 1);

    /// <summary>Reset edges with one optional 4-way N,E,S,W and/or one 4-way NE,SE,SW,NW.</summary>
    public void ResetEdges(bool orthogonal, bool diagonal)
    {
      for (var y = 0; y < Size.Y; y++) // 
      {
        for (var x = 0; x < Size.X; x++)
        {
          var cell = this[y, x];

          cell.Edges.Clear();

          var n = (y > 0); // North (positive vertical axis).
          var e = (x < (Size.X - 1)); // East (negative horizontal axis).
          var s = (y < (Size.Y - 1)); // South (negative vertical axis).
          var w = (x > 0); // West (positive horizontal axis).

          if (orthogonal)
          {
            if (n) cell.Edges.Add((int)EightWindCompassRose.N, this[y - 1, x]);
            if (e) cell.Edges.Add((int)EightWindCompassRose.E, this[y, x + 1]);
            if (s) cell.Edges.Add((int)EightWindCompassRose.S, this[y + 1, x]);
            if (w) cell.Edges.Add((int)EightWindCompassRose.W, this[y, x - 1]);
          }

          if (diagonal)
          {
            if (n && e) cell.Edges.Add((int)EightWindCompassRose.NE, this[y - 1, x + 1]);
            if (s && e) cell.Edges.Add((int)EightWindCompassRose.SE, this[y + 1, x + 1]);
            if (s && w) cell.Edges.Add((int)EightWindCompassRose.SW, this[y + 1, x - 1]);
            if (n && w) cell.Edges.Add((int)EightWindCompassRose.NW, this[y - 1, x - 1]);
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
      var grid = new MazeGrid(Size);

      for (var index = 0; index < grid.Count; index++)
        grid.SetValue(index, GetValue(index));

      return grid;
    }
  }
}
