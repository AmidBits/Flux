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

  public class ChessPiece
  {
    public ChessPieceType Type { get; }
    public System.Collections.Generic.List<Media.Geometry.Point2> Position { get; } = new System.Collections.Generic.List<Media.Geometry.Point2>();

    public ChessPiece(ChessPieceType type, Media.Geometry.Point2 vector)
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

    public static readonly System.Collections.Generic.List<string> Labels = Ranks.SelectMany(rl => Files.Select(cl => $"{cl}{rl}")).ToList();

    public static bool IsValidIndex(int index)
      => index >= 0 && index <= 63;
    public static bool IsValidLabel(string column, string row)
      => System.Array.Exists(Files, f => f.Equals(column, System.StringComparison.Ordinal)) && System.Array.Exists(Ranks, f => f.Equals(row, System.StringComparison.Ordinal));
    public static bool IsValidVector(Media.Geometry.Point2 vector)
      => vector.X >= 0 && vector.X <= 7 && vector.Y >= 0 && vector.Y <= 7;

    public static (string column, string row) IndexToLabel(int index)
      => Media.Geometry.Point2.FromUniqueIndex(index, Files.Length).ToLabels(Files, Ranks);
    public static Media.Geometry.Point2 IndexToVector(int index)
      => Media.Geometry.Point2.FromUniqueIndex(index, Files.Length);

    public static int LabelToIndex(string column, string row)
      => Media.Geometry.Point2.FromLabels(column, row, Files, Ranks).ToUniqueIndex(Files.Length);
    public static Media.Geometry.Point2 LabelToVector(string column, string row)
      => Media.Geometry.Point2.FromLabels(column, row, Files, Ranks);

    public static int VectorToIndex(Media.Geometry.Point2 vector)
      => vector.ToUniqueIndex(Files.Length);
    public static (string column, string row) VectorToLabel(Media.Geometry.Point2 vector)
      => vector.ToLabels(Files, Ranks);

    public static System.Collections.Generic.IEnumerable<Media.Geometry.Point2> GetMovesOfBishop(Media.Geometry.Point2 vector)
    {
      return BlankMoves().Where(v => IsValidVector(v));

      System.Collections.Generic.IEnumerable<Media.Geometry.Point2> BlankMoves()
      {
        for (var i = 1; i < 8; i++)
        {
          yield return new Media.Geometry.Point2(vector.X + i, vector.Y + i);
          yield return new Media.Geometry.Point2(vector.X + i, vector.Y - i);
          yield return new Media.Geometry.Point2(vector.X - i, vector.Y - i);
          yield return new Media.Geometry.Point2(vector.X - i, vector.Y + i);
        }
      }
    }
    public static System.Collections.Generic.IEnumerable<Media.Geometry.Point2> GetMovesOfKing(Media.Geometry.Point2 vector)
    {
      return BlankMoves().Where(v => IsValidVector(v));

      System.Collections.Generic.IEnumerable<Media.Geometry.Point2> BlankMoves()
      {
        yield return new Media.Geometry.Point2(vector.X + 1, vector.Y + 1);
        yield return new Media.Geometry.Point2(vector.X + 1, vector.Y);
        yield return new Media.Geometry.Point2(vector.X + 1, vector.Y - 1);
        yield return new Media.Geometry.Point2(vector.X, vector.Y - 1);
        yield return new Media.Geometry.Point2(vector.X - 1, vector.Y - 1);
        yield return new Media.Geometry.Point2(vector.X - 1, vector.Y);
        yield return new Media.Geometry.Point2(vector.X - 1, vector.Y + 1);
        yield return new Media.Geometry.Point2(vector.X, vector.Y + 1);
      }
    }
    public static System.Collections.Generic.IEnumerable<Media.Geometry.Point2> GetMovesOfKnight(Media.Geometry.Point2 vector)
    {
      return BlankMoves().Where(v => IsValidVector(v));

      System.Collections.Generic.IEnumerable<Media.Geometry.Point2> BlankMoves()
      {
        yield return new Media.Geometry.Point2(vector.X + 1, vector.Y + 2);
        yield return new Media.Geometry.Point2(vector.X + 2, vector.Y + 1);
        yield return new Media.Geometry.Point2(vector.X + 2, vector.Y - 1);
        yield return new Media.Geometry.Point2(vector.X + 1, vector.Y - 2);
        yield return new Media.Geometry.Point2(vector.X - 1, vector.Y - 2);
        yield return new Media.Geometry.Point2(vector.X - 2, vector.Y - 1);
        yield return new Media.Geometry.Point2(vector.X - 2, vector.Y + 1);
        yield return new Media.Geometry.Point2(vector.X - 1, vector.Y + 2);
      }
    }
    public static System.Collections.Generic.IEnumerable<Media.Geometry.Point2> GetMovesOfQueen(Media.Geometry.Point2 vector)
    {
      return BlankerMoves().Where(v => IsValidVector(v));

      System.Collections.Generic.IEnumerable<Media.Geometry.Point2> BlankerMoves()
      {
        for (var i = 1; i < 8; i++)
        {
          yield return new Media.Geometry.Point2(vector.X + i, vector.Y + i);
          yield return new Media.Geometry.Point2(vector.X + i, vector.Y);
          yield return new Media.Geometry.Point2(vector.X + i, vector.Y - i);
          yield return new Media.Geometry.Point2(vector.X, vector.Y - i);
          yield return new Media.Geometry.Point2(vector.X - i, vector.Y - i);
          yield return new Media.Geometry.Point2(vector.X - i, vector.Y);
          yield return new Media.Geometry.Point2(vector.X - i, vector.Y + i);
          yield return new Media.Geometry.Point2(vector.X, vector.Y + i);
        }
      }
    }
    public static System.Collections.Generic.IEnumerable<Media.Geometry.Point2> GetMovesOfPawn(Media.Geometry.Point2 vector)
    {
      return BlanketMoves().Where(v => IsValidVector(v));

      System.Collections.Generic.IEnumerable<Media.Geometry.Point2> BlanketMoves()
      {
        // Moves going "upwards" on the board.
        {
          yield return new Media.Geometry.Point2(vector.X, vector.Y + 1);

          if (vector.Y == 1)
          {
            yield return new Media.Geometry.Point2(vector.X, vector.Y + 2); // Initial optional move (up).
          }

          yield return new Media.Geometry.Point2(vector.X - 1, vector.Y + 1); // Capture move (up).
          yield return new Media.Geometry.Point2(vector.X + 1, vector.Y + 1); // Capture move (up).
        }

        // Moves going "downwards" on the board.
        {
          yield return new Media.Geometry.Point2(vector.X, vector.Y - 1);

          if (vector.Y == 6)
          {
            yield return new Media.Geometry.Point2(vector.X, vector.Y - 2); // Initial optional move (down).
          }

          yield return new Media.Geometry.Point2(vector.X + 1, vector.Y - 1); // Capture move (down).
          yield return new Media.Geometry.Point2(vector.X - 1, vector.Y - 1); // Capture move (down).
        }
      }
    }
    public static System.Collections.Generic.IEnumerable<Media.Geometry.Point2> GetMovesOfRook(Media.Geometry.Point2 vector)
    {
      return BlanketMoves().Where(v => IsValidVector(v));

      System.Collections.Generic.IEnumerable<Media.Geometry.Point2> BlanketMoves()
      {
        for (var i = 1; i < 8; i++)
        {
          yield return new Media.Geometry.Point2(vector.X + i, vector.Y);
          yield return new Media.Geometry.Point2(vector.X, vector.Y - i);
          yield return new Media.Geometry.Point2(vector.X - i, vector.Y);
          yield return new Media.Geometry.Point2(vector.X, vector.Y + i);
        }
      }
    }
  }
}
