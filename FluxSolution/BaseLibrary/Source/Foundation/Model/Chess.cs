using System.Linq;

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

  public sealed class ChessPiece
  {
    public ChessPieceType Type { get; }
    public System.Collections.Generic.List<CartesianCoordinate2I> Position { get; } = new System.Collections.Generic.List<CartesianCoordinate2I>();

    public ChessPiece(ChessPieceType type, CartesianCoordinate2I vector)
    {
      Type = type;
      Position.Add(vector);
    }
  }

  /// <summary>Chess helpers. A vector is a chess board as a grid of [0, 7] for both files (columns, or x) and ranks (rows, or y). A (unique) index is a row-major order set [0, 63] for a chess board. The typical [a1, h8] are used for labels.</summary>
  public static class Chess // Correlated from top/left to bottom/right.
  {
    public static readonly string[] Files = new string[] { "a", "b", "c", "d", "e", "f", "g", "h" };
    public static readonly string[] Ranks = new string[] { "8", "7", "6", "5", "4", "3", "2", "1" };

    public static readonly Size2 BoardSize = new(Files.Length, Ranks.Length);

    public static readonly System.Collections.Generic.List<string> Labels = Ranks.SelectMany(rl => Files.Select(cl => $"{cl}{rl}")).ToList();

    public static bool IsValidIndex(int index)
      => index >= 0 && index <= 63;
    public static bool IsValidLabel(string column, string row)
      => System.Array.Exists(Files, f => f.Equals(column, System.StringComparison.Ordinal)) && System.Array.Exists(Ranks, f => f.Equals(row, System.StringComparison.Ordinal));
    public static bool IsValidVector(CartesianCoordinate2I vector)
      => vector.X >= 0 && vector.X <= 7 && vector.Y >= 0 && vector.Y <= 7;

    public static (string column, string row) IndexToLabel(int index)
    {
      var p = CartesianCoordinate2I.FromUniqueIndex(index, BoardSize.Width);

      return (Files[p.X], Ranks[p.Y]);
    }

    public static CartesianCoordinate2I IndexToVector(int index)
      => CartesianCoordinate2I.FromUniqueIndex(index, BoardSize.Width);

    public static int LabelToIndex(string column, string row)
    {
      var p = new CartesianCoordinate2I(System.Array.IndexOf(Files, column), System.Array.IndexOf(Ranks, row));

      return (int)p.ToUniqueIndex(BoardSize.Width);
    }

    public static CartesianCoordinate2I LabelToVector(string column, string row)
      => new(System.Array.IndexOf(Files, column), System.Array.IndexOf(Ranks, row));

    public static int VectorToIndex(CartesianCoordinate2I vector)
      => (int)vector.ToUniqueIndex(BoardSize.Width);
    public static (string column, string row) VectorToLabel(CartesianCoordinate2I vector)
      => (Files[vector.X], Ranks[vector.Y]);

    public static System.Collections.Generic.IEnumerable<CartesianCoordinate2I> GetMovesOfBishop(CartesianCoordinate2I vector)
    {
      return BlankMoves().Where(v => IsValidVector(v));

      System.Collections.Generic.IEnumerable<CartesianCoordinate2I> BlankMoves()
      {
        for (var i = 1; i < 8; i++)
        {
          yield return new CartesianCoordinate2I(vector.X + i, vector.Y + i);
          yield return new CartesianCoordinate2I(vector.X + i, vector.Y - i);
          yield return new CartesianCoordinate2I(vector.X - i, vector.Y - i);
          yield return new CartesianCoordinate2I(vector.X - i, vector.Y + i);
        }
      }
    }
    public static System.Collections.Generic.IEnumerable<CartesianCoordinate2I> GetMovesOfKing(CartesianCoordinate2I vector)
    {
      return BlankMoves().Where(v => IsValidVector(v));

      System.Collections.Generic.IEnumerable<CartesianCoordinate2I> BlankMoves()
      {
        yield return new CartesianCoordinate2I(vector.X + 1, vector.Y + 1);
        yield return new CartesianCoordinate2I(vector.X + 1, vector.Y);
        yield return new CartesianCoordinate2I(vector.X + 1, vector.Y - 1);
        yield return new CartesianCoordinate2I(vector.X, vector.Y - 1);
        yield return new CartesianCoordinate2I(vector.X - 1, vector.Y - 1);
        yield return new CartesianCoordinate2I(vector.X - 1, vector.Y);
        yield return new CartesianCoordinate2I(vector.X - 1, vector.Y + 1);
        yield return new CartesianCoordinate2I(vector.X, vector.Y + 1);
      }
    }
    public static System.Collections.Generic.IEnumerable<CartesianCoordinate2I> GetMovesOfKnight(CartesianCoordinate2I vector)
    {
      return BlankMoves().Where(v => IsValidVector(v));

      System.Collections.Generic.IEnumerable<CartesianCoordinate2I> BlankMoves()
      {
        yield return new CartesianCoordinate2I(vector.X + 1, vector.Y + 2);
        yield return new CartesianCoordinate2I(vector.X + 2, vector.Y + 1);
        yield return new CartesianCoordinate2I(vector.X + 2, vector.Y - 1);
        yield return new CartesianCoordinate2I(vector.X + 1, vector.Y - 2);
        yield return new CartesianCoordinate2I(vector.X - 1, vector.Y - 2);
        yield return new CartesianCoordinate2I(vector.X - 2, vector.Y - 1);
        yield return new CartesianCoordinate2I(vector.X - 2, vector.Y + 1);
        yield return new CartesianCoordinate2I(vector.X - 1, vector.Y + 2);
      }
    }
    public static System.Collections.Generic.IEnumerable<CartesianCoordinate2I> GetMovesOfQueen(CartesianCoordinate2I vector)
    {
      return BlankerMoves().Where(v => IsValidVector(v));

      System.Collections.Generic.IEnumerable<CartesianCoordinate2I> BlankerMoves()
      {
        for (var i = 1; i < 8; i++)
        {
          yield return new CartesianCoordinate2I(vector.X + i, vector.Y + i);
          yield return new CartesianCoordinate2I(vector.X + i, vector.Y);
          yield return new CartesianCoordinate2I(vector.X + i, vector.Y - i);
          yield return new CartesianCoordinate2I(vector.X, vector.Y - i);
          yield return new CartesianCoordinate2I(vector.X - i, vector.Y - i);
          yield return new CartesianCoordinate2I(vector.X - i, vector.Y);
          yield return new CartesianCoordinate2I(vector.X - i, vector.Y + i);
          yield return new CartesianCoordinate2I(vector.X, vector.Y + i);
        }
      }
    }
    public static System.Collections.Generic.IEnumerable<CartesianCoordinate2I> GetMovesOfPawn(CartesianCoordinate2I vector)
    {
      return BlanketMoves().Where(v => IsValidVector(v));

      System.Collections.Generic.IEnumerable<CartesianCoordinate2I> BlanketMoves()
      {
        // Moves going "upwards" on the board.
        {
          yield return new CartesianCoordinate2I(vector.X, vector.Y + 1);

          if (vector.Y == 1)
          {
            yield return new CartesianCoordinate2I(vector.X, vector.Y + 2); // Initial optional move (up).
          }

          yield return new CartesianCoordinate2I(vector.X - 1, vector.Y + 1); // Capture move (up).
          yield return new CartesianCoordinate2I(vector.X + 1, vector.Y + 1); // Capture move (up).
        }

        // Moves going "downwards" on the board.
        {
          yield return new CartesianCoordinate2I(vector.X, vector.Y - 1);

          if (vector.Y == 6)
          {
            yield return new CartesianCoordinate2I(vector.X, vector.Y - 2); // Initial optional move (down).
          }

          yield return new CartesianCoordinate2I(vector.X + 1, vector.Y - 1); // Capture move (down).
          yield return new CartesianCoordinate2I(vector.X - 1, vector.Y - 1); // Capture move (down).
        }
      }
    }
    public static System.Collections.Generic.IEnumerable<CartesianCoordinate2I> GetMovesOfRook(CartesianCoordinate2I vector)
    {
      return BlanketMoves().Where(v => IsValidVector(v));

      System.Collections.Generic.IEnumerable<CartesianCoordinate2I> BlanketMoves()
      {
        for (var i = 1; i < 8; i++)
        {
          yield return new CartesianCoordinate2I(vector.X + i, vector.Y);
          yield return new CartesianCoordinate2I(vector.X, vector.Y - i);
          yield return new CartesianCoordinate2I(vector.X - i, vector.Y);
          yield return new CartesianCoordinate2I(vector.X, vector.Y + i);
        }
      }
    }
  }
}
