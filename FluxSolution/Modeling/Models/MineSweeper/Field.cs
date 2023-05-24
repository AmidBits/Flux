// https://github.com/gmamaladze/minesweeper
namespace Flux.Model.MineSweeper
{
  public sealed class Field
  {
    public Geometry.CartesianCoordinate2<int> Size { get; }

    public Field(Geometry.CartesianCoordinate2<int> size)
      => Size = size;

    public System.Collections.Generic.IEnumerable<Geometry.CartesianCoordinate2<int>> GetNeighbours(Geometry.CartesianCoordinate2<int> point)
    {
      if (new Geometry.CartesianCoordinate2<int>(point.X + 1, point.Y + 0) is var right && IsInRange(right))
        yield return right;
      if (new Geometry.CartesianCoordinate2<int>(point.X + 1, point.Y + -1) is var upRight && IsInRange(upRight))
        yield return upRight;
      if (new Geometry.CartesianCoordinate2<int>(point.X + 0, point.Y + -1) is var up && IsInRange(up))
        yield return up;
      if (new Geometry.CartesianCoordinate2<int>(point.X + -1, point.Y + -1) is var upLeft && IsInRange(upLeft))
        yield return upLeft;
      if (new Geometry.CartesianCoordinate2<int>(point.X + -1, point.Y + 0) is var left && IsInRange(left))
        yield return left;
      if (new Geometry.CartesianCoordinate2<int>(point.X + -1, point.Y + 1) is var downLeft && IsInRange(downLeft))
        yield return downLeft;
      if (new Geometry.CartesianCoordinate2<int>(point.X + 0, point.Y + 1) is var down && IsInRange(down))
        yield return down;
      if (new Geometry.CartesianCoordinate2<int>(point.X + 1, point.Y + 1) is var downRight && IsInRange(downRight))
        yield return downRight;
    }

    public bool IsInRange(Geometry.CartesianCoordinate2<int> point)
      => point.X >= 0 && point.Y >= 0 && point.X < Size.X && point.Y < Size.Y;

    public static bool IsEmptyAt(Mines mines, Warnings warnings, Geometry.CartesianCoordinate2<int> point)
      => !(mines?.HasMineAt(point) ?? false) && !(warnings?.HasWarningAt(point) ?? false);
  }
}
