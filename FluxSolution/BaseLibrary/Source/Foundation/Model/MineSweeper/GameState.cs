using System.Collections.Immutable;
using System.Linq;

// https://github.com/gmamaladze/minesweeper
namespace Flux.Model.MineSweeper
{
  public sealed class GameState
  {
    private readonly IImmutableStack<Covers> m_moves;

    public Geometry.Point2 CursorPosition { get; set; }

    private GameState(IImmutableStack<Covers> moves, Geometry.Point2 cursorPosition)
    {
      m_moves = moves;
      CursorPosition = cursorPosition;
    }

    public static GameState Create(Covers covers)
      => new(ImmutableStack.Create(covers), new Geometry.Point2(0, 0));

    public GameState Do(Covers covers)
      => new(m_moves.Push(covers), CursorPosition);

    public GameState Do(Geometry.Point2 cursorPosition)
      => new(m_moves, cursorPosition);

    public GameState Undo()
      => m_moves.Pop() is var moves && moves.IsEmpty ? this : new GameState(moves, CursorPosition);

    public GameResult Evaluate(Mines mines)
    {
      if (mines is null) throw new System.ArgumentNullException(nameof(mines));

      var covers = Covers();

      return new(mines.Any(mine => !covers.IsCovered(mine)), covers.Count - mines.Count);
    }

    public Covers Covers()
      => m_moves.Peek();
  }
}
