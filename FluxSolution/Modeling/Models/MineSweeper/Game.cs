using System.Linq;

// https://github.com/gmamaladze/minesweeper
namespace Flux.Model.MineSweeper
{
  public static class Game
  {
    public static void RenderInConsole(GameState gameState, MineField mineField)
    {
      if (gameState is null) throw new System.ArgumentNullException(nameof(gameState));
      if (mineField is null) throw new System.ArgumentNullException(nameof(mineField));

      var charField = new char[mineField.Field.Size.Height, mineField.Field.Size.Width];
      var covers = gameState.Covers();

      for (var i = 0; i < mineField.Field.Size.Height; i++)
      {
        for (var j = 0; j < mineField.Field.Size.Width; j++)
        {
          var p = new CartesianCoordinate2I(i, j);

          var isUncovered = !covers.IsCovered(p);

          var character = 'X';

          if (character == 'X' && isUncovered && mineField.Mines.HasMineAt(p))
            character = 'M';
          if (character == 'X' && isUncovered && mineField.Warnings.WarningsAt(p) is var adjacent && adjacent > 0)
            character = (char)(adjacent + '0');
          if (character == 'X' && isUncovered)
            character = ' ';

          charField[i, j] = character;
        }
      }

      System.Console.Clear();
      System.Console.WriteLine(Formatting.ArrayFormatter.Separated.TwoToConsoleString(charField, (e, i) => e.ToString()));
      for (var i = 0; i < mineField.Field.Size.Height; i++)
        for (var j = 0; j < mineField.Field.Size.Width; j++)
          if (charField[i, j] == 'M')
            System.Console.WriteLine($"Boom, a mine found at [{i}, {j}]! Game over.");
    }

    public static void PlayInConsole()
    {
      var mineField = new Model.MineSweeper.MineField(new Size2(10, 10), 10);
      var gameState = Model.MineSweeper.Game.Start(mineField);
      //var gameResult = gameState.Evaluate(mineField.Mines);

      RenderInConsole(gameState, mineField);

      while (System.Console.ReadLine() is var line && !string.IsNullOrEmpty(line))
      {
        if (CartesianCoordinate2I.TryParse(line, out var point))
        {
          gameState.CursorPosition = point;

          gameState = Game.Uncover(gameState, mineField);

          //gameResult = gameState.Evaluate(mineField.Mines);
        }

        RenderInConsole(gameState, mineField);
      }
    }

    public static GameState Start(MineField mineField)
      => GameState.Create(Covers.Create((mineField ?? throw new System.ArgumentNullException(nameof(mineField))).Field.Size));

    public static GameState Undo(GameState current)
      => (current ?? throw new System.ArgumentNullException(nameof(current))).Undo();

    public static GameState Quit(GameState current, MineField mineField)
      => (current ?? throw new System.ArgumentNullException(nameof(current))).Do(current.Covers().UncoverRange((mineField ?? throw new System.ArgumentNullException(nameof(mineField))).Mines));

    public static GameState Uncover(GameState current, MineField mineField)
      => (current ?? throw new System.ArgumentNullException(nameof(current))).Do(current.Covers().UncoverDeep((mineField ?? throw new System.ArgumentNullException(nameof(mineField))), current.CursorPosition));

    public static GameState SwitchFlag(GameState current)
      => (current ?? throw new System.ArgumentNullException(nameof(current))).Do(current.Covers().SwitchFlag(current.CursorPosition));

    private static Covers UncoverDeep(this Covers covers, MineField mineField, CartesianCoordinate2I point)
    {
      if (!covers.IsCovered(point))
        return covers;
      if (!mineField.IsEmptyAt(point))
        return covers.Uncover(point);

      return mineField.Field.GetNeighbours(point).Aggregate(covers.Uncover(point), (current, neighbor) => current.UncoverDeep(mineField, neighbor));
    }
  }
}
