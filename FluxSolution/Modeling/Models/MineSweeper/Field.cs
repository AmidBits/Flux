﻿// https://github.com/gmamaladze/minesweeper
namespace Flux.Model.MineSweeper
{
  public sealed class Field
  {
    public Size2 Size { get; }

    public Field(Size2 size)
      => Size = size;

    public System.Collections.Generic.IEnumerable<CartesianCoordinate2I> GetNeighbours(CartesianCoordinate2I point)
    {
      if (point + new CartesianCoordinate2I(1, 0) is var right && IsInRange(right))
        yield return right;
      if (point + new CartesianCoordinate2I(1, -1) is var upRight && IsInRange(upRight))
        yield return upRight;
      if (point + new CartesianCoordinate2I(0, -1) is var up && IsInRange(up))
        yield return up;
      if (point + new CartesianCoordinate2I(-1, -1) is var upLeft && IsInRange(upLeft))
        yield return upLeft;
      if (point + new CartesianCoordinate2I(-1, 0) is var left && IsInRange(left))
        yield return left;
      if (point + new CartesianCoordinate2I(-1, 1) is var downLeft && IsInRange(downLeft))
        yield return downLeft;
      if (point + new CartesianCoordinate2I(0, 1) is var down && IsInRange(down))
        yield return down;
      if (point + new CartesianCoordinate2I(1, 1) is var downRight && IsInRange(downRight))
        yield return downRight;
    }

    public bool IsInRange(CartesianCoordinate2I point)
      => point.X >= 0 && point.Y >= 0 && point.X < Size.Width && point.Y < Size.Height;

    public static bool IsEmptyAt(Mines mines, Warnings warnings, CartesianCoordinate2I point)
      => !(mines?.HasMineAt(point) ?? false) && !(warnings?.HasWarningAt(point) ?? false);
  }
}