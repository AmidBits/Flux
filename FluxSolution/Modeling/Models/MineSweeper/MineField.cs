﻿// https://github.com/gmamaladze/minesweeper
namespace Flux.Model.MineSweeper
{
  public sealed class MineField
  {
    public Field Field { get; }
    public Mines Mines { get; }
    public Warnings Warnings { get; }

    public bool IsEmptyAt(CartesianCoordinate2I point)
      => !Mines.HasMineAt(point) && !Warnings.HasWarningAt(point);

    public MineField(Size2 fieldSize, int mineCount)
    {
      Field = new Field(fieldSize);
      Mines = Mines.Create(Field, mineCount);
      Warnings = Warnings.Create(Field, Mines);
    }
  }
}