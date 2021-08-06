// https://github.com/gmamaladze/minesweeper
namespace Flux.Model.Gaming.MineSweeper
{
  public class MineField
  {
    public Field Field { get; }
    public Mines Mines { get; }
    public Warnings Warnings { get; }

    public bool IsEmptyAt(Geometry.Point2 point)
      => !Mines.HasMineAt(point) && !Warnings.HasWarningAt(point);

    public MineField(Geometry.Size2 fieldSize, int mineCount)
    {
      Field = new Field(fieldSize);
      Mines = Mines.Create(Field, mineCount);
      Warnings = Warnings.Create(Field, Mines);
    }
  }
}
