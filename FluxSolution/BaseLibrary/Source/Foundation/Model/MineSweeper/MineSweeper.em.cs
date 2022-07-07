// https://github.com/gmamaladze/minesweeper
namespace Flux.Model.MineSweeper
{
  public static class EmMineSweeper
  {
    public static System.Collections.Generic.IEnumerable<GridCoordinate2> AllPoints(this Size2 size)
    {
      for (var i = 0; i < size.Width; i++)
        for (var j = 0; j < size.Height; j++)
          yield return new GridCoordinate2(i, j);
    }
  }
}
