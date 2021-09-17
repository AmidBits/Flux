// https://github.com/gmamaladze/minesweeper
namespace Flux.Model.MineSweeper
{
  public static class EmMineSweeper
  {
    public static System.Collections.Generic.IEnumerable<Geometry.Point2> AllPoints(this Geometry.Size2 size)
    {
      for (var i = 0; i < size.Width; i++)
        for (var j = 0; j < size.Height; j++)
          yield return new Geometry.Point2(i, j);
    }
  }
}
