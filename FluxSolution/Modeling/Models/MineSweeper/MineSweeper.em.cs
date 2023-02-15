// https://github.com/gmamaladze/minesweeper
namespace Flux.Model.MineSweeper
{
  public static class EmMineSweeper
  {
    public static System.Collections.Generic.IEnumerable<Numerics.CartesianCoordinate2<int>> AllPoints(this Numerics.CartesianCoordinate2<int> size)
    {
      for (var i = 0; i < size.X; i++)
        for (var j = 0; j < size.Y; j++)
          yield return new(i, j);
    }
  }
}
