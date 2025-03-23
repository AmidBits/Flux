// https://github.com/gmamaladze/minesweeper
namespace Flux.Model.MineSweeper
{
  public sealed class Field
  {
    public System.Drawing.Point Size { get; }

    public Field(System.Drawing.Point size)
      => Size = size;

    public System.Collections.Generic.IEnumerable<System.Drawing.Point> GetNeighbours(System.Drawing.Point point)
    {
      if (new System.Drawing.Point(point.X + 1, point.Y + 0) is var right && IsInRange(right))
        yield return right;
      if (new System.Drawing.Point(point.X + 1, point.Y + -1) is var upRight && IsInRange(upRight))
        yield return upRight;
      if (new System.Drawing.Point(point.X + 0, point.Y + -1) is var up && IsInRange(up))
        yield return up;
      if (new System.Drawing.Point(point.X + -1, point.Y + -1) is var upLeft && IsInRange(upLeft))
        yield return upLeft;
      if (new System.Drawing.Point(point.X + -1, point.Y + 0) is var left && IsInRange(left))
        yield return left;
      if (new System.Drawing.Point(point.X + -1, point.Y + 1) is var downLeft && IsInRange(downLeft))
        yield return downLeft;
      if (new System.Drawing.Point(point.X + 0, point.Y + 1) is var down && IsInRange(down))
        yield return down;
      if (new System.Drawing.Point(point.X + 1, point.Y + 1) is var downRight && IsInRange(downRight))
        yield return downRight;
    }

    public bool IsInRange(System.Drawing.Point point)
      => point.X >= 0 && point.Y >= 0 && point.X < Size.X && point.Y < Size.Y;

    public static bool IsEmptyAt(Mines mines, Warnings warnings, System.Drawing.Point point)
      => !(mines?.HasMineAt(point) ?? false) && !(warnings?.HasWarningAt(point) ?? false);
  }
}
