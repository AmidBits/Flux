using System.Linq;
using System.Runtime.CompilerServices;

namespace Flux.Model
{
  public enum ChessPieceType
  {
    Empty = '\u0020',
    WhiteKing = '\u2654',
    WhiteQueen = '\u2655',
    WhiteRook = '\u2656',
    WhiteBishop = '\u2657',
    WhiteKnight = '\u2658',
    WhitePawn = '\u2659',
    BlackKing = '\u265A',
    BlackQueen = '\u265B',
    BlackRook = '\u265C',
    BlackBishop = '\u265D',
    BlackKnight = '\u265E',
    BlackPawn = '\u265F',
  }

  public class ChessPiece
  {
    public ChessPieceType Type { get; }
    public System.Collections.Generic.List<Vector2I> Position { get; } = new System.Collections.Generic.List<Vector2I>();

    public ChessPiece(ChessPieceType type, Vector2I vector)
    {
      Type = type;
      Position.Add(vector);
    }
  }

  public static partial class Convert
  {
    public static class Chess // Correlated from top/left to bottom/right.
    {
      public static bool IsValidIndex(int index)
        => index >= 0 && index <= 63;
      public static bool IsValidLabel(char column, char row)
        => column >= 'a' && column <= 'h' && row >= '1' && row <= '8';
      public static bool IsValidVector(Vector2I vector)
        => vector.X >= 0 && vector.X <= 7 && vector.Y >= 0 && vector.Y <= 7;

      public static (char column, char row) IndexToLabel(int index)
        => ((char)((index % 8) + 'a'), (char)(8 - index / 8 + '0'));
      public static Vector2I IndexToVector(int index)
        => new Vector2I(index % 8, index / 8);

      public static int LabelToIndex(char column, char row)
        => IsValidLabel(column, row) ? ((8 - (row - '0')) * 8) + (column - 'a') : throw new System.ArgumentOutOfRangeException($"Invalid label: \"{nameof(column)}{nameof(row)}\"");
      public static Vector2I LabelToVector(char column, char row)
        => new Vector2I(column - 'a', 8 - (row - '0'));

      public static int VectorToIndex(Vector2I vector)
        => vector.Y * 8 + vector.X;
      public static (char column, char row) VectorToLabel(Vector2I vector)
        => ((char)(vector.X + 'a'), (char)(8 - vector.Y + '0'));

      public static (char column, char row) VectorToLabels(int x, int y) => (x >= 0 && x <= 7 ? (char)(x + 'a') : throw new System.ArgumentOutOfRangeException(nameof(x)), y >= 0 && y <= 7 ? (char)((-y + 7) + '1') : throw new System.ArgumentOutOfRangeException(nameof(y)));
      public static (int x, int y) LabelsToVector(char column, char row) => (column >= 'a' && column <= 'h' ? (column - 'a') : throw new System.ArgumentOutOfRangeException(nameof(column)), row >= '1' && row <= '8' ? -((row - '1') - 7) : throw new System.ArgumentOutOfRangeException(nameof(row)));

      public static bool IsValidVector(int x, int y) => x >= 0 && x < 8 && y >= 0 && y < 8;

      public static System.Collections.Generic.IEnumerable<(int x, int y)> GetMovesOfBishop(int x, int y)
      {
        return BlankMoves().Where(p => IsValidVector(p.x, p.y));

        System.Collections.Generic.IEnumerable<(int x, int y)> BlankMoves()
        {
          for (var i = 1; i < 8; i++)
          {
            yield return (x + i, y + i);
            yield return (x + i, y - i);
            yield return (x - i, y - i);
            yield return (x - i, y + i);
          }
        }
      }
      public static System.Collections.Generic.IEnumerable<(int x, int y)> GetMovesOfKing(int x, int y)
      {
        return BlankMoves().Where(p => IsValidVector(p.x, p.y));

        System.Collections.Generic.IEnumerable<(int x, int y)> BlankMoves()
        {
          yield return (x + 1, y + 1);
          yield return (x + 1, y);
          yield return (x + 1, y - 1);
          yield return (x, y - 1);
          yield return (x - 1, y - 1);
          yield return (x - 1, y);
          yield return (x - 1, y + 1);
          yield return (x, y + 1);
        }
      }
      public static System.Collections.Generic.IEnumerable<(int x, int y)> GetMovesOfKnight(int x, int y)
      {
        return BlankMoves().Where(p => IsValidVector(p.x, p.y));

        System.Collections.Generic.IEnumerable<(int x, int y)> BlankMoves()
        {
          yield return (x + 1, y + 2);
          yield return (x + 2, y + 1);
          yield return (x + 2, y - 1);
          yield return (x + 1, y - 2);
          yield return (x - 1, y - 2);
          yield return (x - 2, y - 1);
          yield return (x - 2, y + 1);
          yield return (x - 1, y + 2);
        }
      }
      public static System.Collections.Generic.IEnumerable<(int x, int y)> GetMovesOfQueen(int x, int y)
      {
        return BlankerMoves().Where(p => IsValidVector(p.x, p.y));

        System.Collections.Generic.IEnumerable<(int x, int y)> BlankerMoves()
        {
          for (var i = 1; i < 8; i++)
          {
            yield return (x + i, y + i);
            yield return (x + i, y);
            yield return (x + i, y - i);
            yield return (x, y - i);
            yield return (x - i, y - i);
            yield return (x - i, y);
            yield return (x - i, y + i);
            yield return (x, y + i);
          }
        }
      }
      public static System.Collections.Generic.IEnumerable<(int x, int y)> GetMovesOfPawn(int x, int y)
      {
        return BlanketMoves().Where(p => IsValidVector(p.x, p.y));

        System.Collections.Generic.IEnumerable<(int x, int y)> BlanketMoves()
        {
          // Moves going "upwards" on the board.
          {
            yield return (x, y + 1);

            if (y == 1)
            {
              yield return (x, y + 2); // Initial optional move (up).
            }

            yield return (x - 1, y + 1); // Capture move (up).
            yield return (x + 1, y + 1); // Capture move (up).
          }

          // Moves going "downwards" on the board.
          {
            yield return (x, y - 1);

            if (y == 6)
            {
              yield return (x, y - 2); // Initial optional move (down).
            }

            yield return (x + 1, y - 1); // Capture move (down).
            yield return (x - 1, y - 1); // Capture move (down).
          }
        }
      }
      public static System.Collections.Generic.IEnumerable<(int x, int y)> GetMovesOfRook(int x, int y)
      {
        return BlanketMoves().Where(p => IsValidVector(p.x, p.y));

        System.Collections.Generic.IEnumerable<(int x, int y)> BlanketMoves()
        {
          for (var i = 1; i < 8; i++)
          {
            yield return (x + i, y);
            yield return (x, y - i);
            yield return (x - i, y);
            yield return (x, y + i);
          }
        }
      }
    }
  }

  public interface IBoard
  {
    string ToLabel(int index);
    int ToIndex(string label);
  }

  public class Board<T>
    : IBoard
  //where T : notnull
  {
    public string[] Rows { get; private set; }
    public string[] Columns { get; private set; }

#if false
    protected System.Collections.Generic.Dictionary<string, T> Squares { get; private set; }

    public T this[int index] => Squares.ElementAt(index).Value;
    public T this[string label] => Squares[label];

    public T this[string row, string column] => Squares[$"{row},{column}"];
    public T this[char row, char column] => Squares[$"{row},{column}"];
#else
    protected System.Collections.Generic.List<T> Squares { get; private set; }

    public T this[int index] => Squares.ElementAt(index);
    public T this[string label] => Squares[ToIndex(label)];

    public T this[string row, string column] => Squares[ToIndex($"{row},{column}")];
    public T this[char row, char column] => Squares[ToIndex($"{row},{column}")];
#endif

    public T this[int row, int column] => (row > 0 && row <= Rows.Length && column > 0) ? (column <= Columns.Length) ? this[row * Columns.Length + column] : throw new System.ArgumentOutOfRangeException(nameof(column)) : throw new System.ArgumentOutOfRangeException(nameof(row));

    public Board(string[] rows, string[] columns)
    {
      Rows = rows;
      Columns = columns;

      var count = Columns.Length * Rows.Length;

#if false
      Squares = new System.Collections.Generic.Dictionary<string, T>(count);

      for (var index = 0; index < count; index++)
      {
        Squares.Add(ToLabel(index), default);
      }
#else
      Squares = new System.Collections.Generic.List<T>(count);

      for (var index = 0; index < count; index++)
      {
        Squares.Add(default);
      }
#endif
    }
    public Board(char[] rows, char[] columns)
      : this(rows.Select(c => c.ToString()).ToArray(), columns.Select(c => c.ToString()).ToArray())
    { }
    public Board(int rows, int columns)
      : this(System.Linq.Enumerable.Range(1, rows).Select(n => n.ToString()).ToArray(), System.Linq.Enumerable.Range(1, columns).Select(n => n.ToString()).ToArray())
    { }

    public string ToLabel(int index) => (index >= 0 && index < Squares.Count) ? $"{Rows[index / Columns.Length]},{Columns[index % Columns.Length]}" : throw new System.ArgumentOutOfRangeException(nameof(index));
    public int ToIndex(string label) => (label.Length > 3 && label.Split(',') is var labels && Rows.Contains(labels[0]) && Columns.Contains(labels[1])) ? System.Array.IndexOf(Rows, labels[0]) * Columns.Length + System.Array.IndexOf(Columns, labels[1]) : throw new System.ArgumentOutOfRangeException(nameof(label));
  }

  public class ChessBoard<T>
    : Board<T>
  {
    public ChessBoard()
      : base(@"87654321".ToCharArray(), @"abcdefgh".ToCharArray())
    { }
  }

  public class DraughtsBoard<T>
    : Board<T>
  {
    public DraughtsBoard()
      : base(8, 8)
    { }
  }

  public class GoBoard<T>
    : Board<T>
  {
    public GoBoard()
      : base(31, 31)
    { }
  }

  public class ReversiBoard<T>
    : Board<T>
  {
    public ReversiBoard()
      : base(@"12345678".Select(c => c.ToString()).ToArray(), @"abcdefgh".Select(c => c.ToString()).ToArray())
    { }
  }

  public class SudokuBoard<T>
    : Board<T>
  {
    public SudokuBoard()
      : base(@"ABCDEFGHI".ToCharArray(), @"123456789".ToCharArray())
    { }
  }

  public class TicTacToeBoard<T>
    : Board<T>
  {
    public TicTacToeBoard()
      : base(3, 3)
    { }
  }
}
