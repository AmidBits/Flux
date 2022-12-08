// https://github.com/gmamaladze/minesweeper
namespace Flux.Model.MineSweeper
{
  public sealed class Field
  {
    public Size2<int> Size { get; }

    public Field(Size2<int> size)
      => Size = size;

    public System.Collections.Generic.IEnumerable<Point2> GetNeighbours(Point2 point)
    {
      if (point + new Point2(1, 0) is var right && IsInRange(right))
        yield return right;
      if (point + new Point2(1, -1) is var upRight && IsInRange(upRight))
        yield return upRight;
      if (point + new Point2(0, -1) is var up && IsInRange(up))
        yield return up;
      if (point + new Point2(-1, -1) is var upLeft && IsInRange(upLeft))
        yield return upLeft;
      if (point + new Point2(-1, 0) is var left && IsInRange(left))
        yield return left;
      if (point + new Point2(-1, 1) is var downLeft && IsInRange(downLeft))
        yield return downLeft;
      if (point + new Point2(0, 1) is var down && IsInRange(down))
        yield return down;
      if (point + new Point2(1, 1) is var downRight && IsInRange(downRight))
        yield return downRight;
    }

    public bool IsInRange(Point2 point)
      => point.X >= 0 && point.Y >= 0 && point.X < Size.Width && point.Y < Size.Height;

    public static bool IsEmptyAt(Mines mines, Warnings warnings, Point2 point)
      => !(mines?.HasMineAt(point) ?? false) && !(warnings?.HasWarningAt(point) ?? false);
  }
}
