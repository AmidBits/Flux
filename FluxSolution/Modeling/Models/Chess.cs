using System.Linq;
using Flux.Geometry;
using Flux.Maths;

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
    public System.Collections.Generic.List<Geometry.CartesianCoordinate2<int>> Position { get; } = new System.Collections.Generic.List<Geometry.CartesianCoordinate2<int>>();

    public ChessPiece(ChessPieceType type, Geometry.CartesianCoordinate2<int> vector)
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

    public static readonly Geometry.CartesianCoordinate2<int> BoardSize = new(Files.Length, Ranks.Length);

    public static readonly System.Collections.Generic.List<string> Labels = Ranks.SelectMany(rl => Files.Select(cl => $"{cl}{rl}")).ToList();

    public static bool IsValidIndex(int index)
      => index >= 0 && index <= 63;
    public static bool IsValidLabel(string column, string row)
      => System.Array.Exists(Files, f => f.Equals(column, System.StringComparison.Ordinal)) && System.Array.Exists(Ranks, f => f.Equals(row, System.StringComparison.Ordinal));
    public static bool IsValidVector(Geometry.CartesianCoordinate2<int> vector)
      => vector.X >= 0 && vector.X <= 7 && vector.Y >= 0 && vector.Y <= 7;

    public static (string column, string row) IndexToLabel(int index)
    {
      var (x, y) = CartesianCoordinate2<int>.ConvertUniqueIndexToCartesian2(index, BoardSize.X);

      return (Files[(int)x], Ranks[(int)y]);
    }

    public static Geometry.CartesianCoordinate2<int> IndexToVector(int index)
    {
      var (x, y) = CartesianCoordinate2<int>.ConvertUniqueIndexToCartesian2(index, BoardSize.X);

      return new Geometry.CartesianCoordinate2<int>((int)x, (int)y);
    }

    public static int LabelToIndex(string column, string row)
    {
      var x = System.Array.IndexOf(Files, column);
      var y = System.Array.IndexOf(Ranks, row);

      return (int)CartesianCoordinate2<int>.ConvertCartesian2ToUniqueIndex(x, y, BoardSize.X);
    }

    public static Geometry.CartesianCoordinate2<int> LabelToVector(string column, string row)
      => new(System.Array.IndexOf(Files, column), System.Array.IndexOf(Ranks, row));

    public static int VectorToIndex(Geometry.CartesianCoordinate2<int> vector)
      => (int)CartesianCoordinate2<int>.ConvertCartesian2ToUniqueIndex(vector.X, vector.Y, BoardSize.X);
    public static (string column, string row) VectorToLabel(Geometry.CartesianCoordinate2<int> vector)
      => (Files[vector.X], Ranks[vector.Y]);

    public static System.Collections.Generic.IEnumerable<Geometry.CartesianCoordinate2<int>> GetMovesOfBishop(Geometry.CartesianCoordinate2<int> vector)
    {
      return BlankMoves().Where(v => IsValidVector(v));

      System.Collections.Generic.IEnumerable<Geometry.CartesianCoordinate2<int>> BlankMoves()
      {
        for (var i = 1; i < 8; i++)
        {
          yield return new Geometry.CartesianCoordinate2<int>(vector.X + i, vector.Y + i);
          yield return new Geometry.CartesianCoordinate2<int>(vector.X + i, vector.Y - i);
          yield return new Geometry.CartesianCoordinate2<int>(vector.X - i, vector.Y - i);
          yield return new Geometry.CartesianCoordinate2<int>(vector.X - i, vector.Y + i);
        }
      }
    }
    public static System.Collections.Generic.IEnumerable<Geometry.CartesianCoordinate2<int>> GetMovesOfKing(Geometry.CartesianCoordinate2<int> vector)
    {
      return BlankMoves().Where(v => IsValidVector(v));

      System.Collections.Generic.IEnumerable<Geometry.CartesianCoordinate2<int>> BlankMoves()
      {
        yield return new Geometry.CartesianCoordinate2<int>(vector.X + 1, vector.Y + 1);
        yield return new Geometry.CartesianCoordinate2<int>(vector.X + 1, vector.Y);
        yield return new Geometry.CartesianCoordinate2<int>(vector.X + 1, vector.Y - 1);
        yield return new Geometry.CartesianCoordinate2<int>(vector.X, vector.Y - 1);
        yield return new Geometry.CartesianCoordinate2<int>(vector.X - 1, vector.Y - 1);
        yield return new Geometry.CartesianCoordinate2<int>(vector.X - 1, vector.Y);
        yield return new Geometry.CartesianCoordinate2<int>(vector.X - 1, vector.Y + 1);
        yield return new Geometry.CartesianCoordinate2<int>(vector.X, vector.Y + 1);
      }
    }
    public static System.Collections.Generic.IEnumerable<Geometry.CartesianCoordinate2<int>> GetMovesOfKnight(Geometry.CartesianCoordinate2<int> vector)
    {
      return BlankMoves().Where(v => IsValidVector(v));

      System.Collections.Generic.IEnumerable<Geometry.CartesianCoordinate2<int>> BlankMoves()
      {
        yield return new Geometry.CartesianCoordinate2<int>(vector.X + 1, vector.Y + 2);
        yield return new Geometry.CartesianCoordinate2<int>(vector.X + 2, vector.Y + 1);
        yield return new Geometry.CartesianCoordinate2<int>(vector.X + 2, vector.Y - 1);
        yield return new Geometry.CartesianCoordinate2<int>(vector.X + 1, vector.Y - 2);
        yield return new Geometry.CartesianCoordinate2<int>(vector.X - 1, vector.Y - 2);
        yield return new Geometry.CartesianCoordinate2<int>(vector.X - 2, vector.Y - 1);
        yield return new Geometry.CartesianCoordinate2<int>(vector.X - 2, vector.Y + 1);
        yield return new Geometry.CartesianCoordinate2<int>(vector.X - 1, vector.Y + 2);
      }
    }
    public static System.Collections.Generic.IEnumerable<Geometry.CartesianCoordinate2<int>> GetMovesOfQueen(Geometry.CartesianCoordinate2<int> vector)
    {
      return BlankerMoves().Where(v => IsValidVector(v));

      System.Collections.Generic.IEnumerable<Geometry.CartesianCoordinate2<int>> BlankerMoves()
      {
        for (var i = 1; i < 8; i++)
        {
          yield return new Geometry.CartesianCoordinate2<int>(vector.X + i, vector.Y + i);
          yield return new Geometry.CartesianCoordinate2<int>(vector.X + i, vector.Y);
          yield return new Geometry.CartesianCoordinate2<int>(vector.X + i, vector.Y - i);
          yield return new Geometry.CartesianCoordinate2<int>(vector.X, vector.Y - i);
          yield return new Geometry.CartesianCoordinate2<int>(vector.X - i, vector.Y - i);
          yield return new Geometry.CartesianCoordinate2<int>(vector.X - i, vector.Y);
          yield return new Geometry.CartesianCoordinate2<int>(vector.X - i, vector.Y + i);
          yield return new Geometry.CartesianCoordinate2<int>(vector.X, vector.Y + i);
        }
      }
    }
    public static System.Collections.Generic.IEnumerable<Geometry.CartesianCoordinate2<int>> GetMovesOfPawn(Geometry.CartesianCoordinate2<int> vector)
    {
      return BlanketMoves().Where(v => IsValidVector(v));

      System.Collections.Generic.IEnumerable<Geometry.CartesianCoordinate2<int>> BlanketMoves()
      {
        // Moves going "upwards" on the board.
        {
          yield return new Geometry.CartesianCoordinate2<int>(vector.X, vector.Y + 1);

          if (vector.Y == 1)
          {
            yield return new Geometry.CartesianCoordinate2<int>(vector.X, vector.Y + 2); // Initial optional move (up).
          }

          yield return new Geometry.CartesianCoordinate2<int>(vector.X - 1, vector.Y + 1); // Capture move (up).
          yield return new Geometry.CartesianCoordinate2<int>(vector.X + 1, vector.Y + 1); // Capture move (up).
        }

        // Moves going "downwards" on the board.
        {
          yield return new Geometry.CartesianCoordinate2<int>(vector.X, vector.Y - 1);

          if (vector.Y == 6)
          {
            yield return new Geometry.CartesianCoordinate2<int>(vector.X, vector.Y - 2); // Initial optional move (down).
          }

          yield return new Geometry.CartesianCoordinate2<int>(vector.X + 1, vector.Y - 1); // Capture move (down).
          yield return new Geometry.CartesianCoordinate2<int>(vector.X - 1, vector.Y - 1); // Capture move (down).
        }
      }
    }
    public static System.Collections.Generic.IEnumerable<Geometry.CartesianCoordinate2<int>> GetMovesOfRook(Geometry.CartesianCoordinate2<int> vector)
    {
      return BlanketMoves().Where(v => IsValidVector(v));

      System.Collections.Generic.IEnumerable<Geometry.CartesianCoordinate2<int>> BlanketMoves()
      {
        for (var i = 1; i < 8; i++)
        {
          yield return new Geometry.CartesianCoordinate2<int>(vector.X + i, vector.Y);
          yield return new Geometry.CartesianCoordinate2<int>(vector.X, vector.Y - i);
          yield return new Geometry.CartesianCoordinate2<int>(vector.X - i, vector.Y);
          yield return new Geometry.CartesianCoordinate2<int>(vector.X, vector.Y + i);
        }
      }
    }
  }
}
