using System.Linq;

namespace Flux
{
  public static class Test
  {
    public static string ToNotation(this Model.ChessPieceType type)
    => ((char)type).ToString() + type switch
    {
      Model.ChessPieceType.Empty => "--",
      Model.ChessPieceType.WhiteKing => "wK",
      Model.ChessPieceType.WhiteQueen => "wQ",
      Model.ChessPieceType.WhiteRook => "wR",
      Model.ChessPieceType.WhiteBishop => "wB",
      Model.ChessPieceType.WhiteKnight => "wN",
      Model.ChessPieceType.WhitePawn => "wP",
      Model.ChessPieceType.BlackKing => "bK",
      Model.ChessPieceType.BlackQueen => "bQ",
      Model.ChessPieceType.BlackRook => "bR",
      Model.ChessPieceType.BlackBishop => "bB",
      Model.ChessPieceType.BlackKnight => "bN",
      Model.ChessPieceType.BlackPawn => "bP",
      _ => "??"
    };
  }
}

namespace Flux.Model
{
  //public interface IMatrix<TValue>
  //  where TValue : notnull
  //{
  //  TValue this[int x, int y, int z] { get; set; }
  //}

  public class MatrixFixed<TValue>
    : System.ICloneable, System.IEquatable<MatrixFixed<TValue>>
    where TValue : notnull
  {
    protected TValue[] m_values;
    public System.Collections.Generic.IReadOnlyList<TValue> Values => m_values.ToList();
    public int LengthX { get; private set; }
    public int LengthY { get; private set; }
    private int LengthXY { get; set; }
    public int LengthZ { get; private set; }

    public TValue this[int index]
    {
      get => m_values[index];
      set => m_values[index] = value;
    }
    public TValue this[int x, int y, int z = 0]
    {
      get => m_values[CartesianToIndex(x, y, z)];
      set => m_values[CartesianToIndex(x, y, z)] = value;
    }
    public TValue this[int x, int y]
    {
      get => m_values[CartesianToIndex(x, y, 0)];
      set => m_values[CartesianToIndex(x, y, 0)] = value;
    }

    public MatrixFixed(int x, int y, int z = 1)
    {
      LengthX = x;
      LengthY = y;
      LengthXY = x * y;
      LengthZ = z;

      m_values = new TValue[x * y * z];
    }

    /// <summary>Creates a sequence of adjacent slots, relative to the specified (by index) slot.</summary>
    public System.Collections.Generic.IEnumerable<int> Adjacent(int index)
    {
      var (x, y, z) = IndexToCartesian(index);

      for (int zi = (z > 0 ? z - 1 : z), zmax = (z < LengthZ - 1 ? z + 1 : z); zi <= zmax; zi++)
      {
        for (int yi = (y > 0 ? y - 1 : y), ymax = (y < LengthY - 1 ? y + 1 : y); yi <= ymax; yi++)
        {
          for (int xi = (x > 0 ? x - 1 : x), xmax = (x < LengthX - 1 ? x + 1 : x); xi <= xmax; xi++)
          {
            if (CartesianToIndex(xi, yi, zi) is var ixyz && ixyz != index)
            {
              yield return ixyz;
            }
          }
        }
      }
    }
    /// <summary>Creates a sequence of adjacent slots, relative to the specified (by x, y and z) slot.</summary>
    public System.Collections.Generic.IEnumerable<int> Adjacent(int x, int y, int z)
      => Adjacent(CartesianToIndex(x, y, z));

    /// <summary>Converts an integer cartesian coordinate to a linear index, based on the x, y, z lengths.</summary>
    public int CartesianToIndex(int x, int y, int z)
      => x + y * LengthX + z * LengthXY;
    /// <summary>Converts a linear index to an integer cartesian coordinate, based on the x, y, z lengths.</summary>
    public (int x, int y, int z) IndexToCartesian(int index)
    {
      var irxy = index % LengthXY;

      return (irxy % LengthX, irxy / LengthX, index / (LengthXY));
    }

    public void Initialize(TValue value)
      => System.Array.Fill(m_values, value);

    public object Clone()
    {
      var clone = new MatrixFixed<TValue>(LengthX, LengthY, LengthZ);
      System.Array.Copy(m_values, clone.m_values, m_values.Length);
      return clone;
    }

    // Operators
    public static bool operator ==(MatrixFixed<TValue> a, MatrixFixed<TValue> b)
      => a.Equals(b);
    public static bool operator !=(MatrixFixed<TValue> a, MatrixFixed<TValue> b)
      => !a.Equals(b);
    // IEquatable
    public bool Equals([System.Diagnostics.CodeAnalysis.AllowNull] MatrixFixed<TValue> other)
    {
      if (other is null || other.m_values.Length != m_values.Length) return false;

      for (var index = 0; index < m_values.Length; index++)
      {
        var value = m_values[index];
        var otherValue = other.m_values[index];

        if (!(ReferenceEquals(value, otherValue) || value.Equals(otherValue))) return false;
      }

      return true;
    }
    // Object (overrides)
    public override bool Equals(object? obj)
      => obj is MatrixFixed<TValue> && Equals(obj);
    public override int GetHashCode()
      => Flux.HashCode.Combine(m_values);
    public override string? ToString()
    {
      return $"<{this.GetType().Name}, X={LengthX}, Y={LengthY}, Z={LengthZ}>";
    }
  }

  public class ChessGrid
    : MatrixFixed<ChessPieceType>
  {
    public const string LabelsOfFiles = @"abcdefgh";
    public const string LabelsOfRanks = @"87654321";

    public static readonly System.Collections.Generic.List<string> Squares = LabelsOfRanks.SelectMany(rl => LabelsOfFiles.Select(cl => $"{cl}{rl}")).ToList();

    public ChessPieceType this[char columnLabel, char rowLabel]
    {
      get
      {
        var squareLabel = new string(new char[] { columnLabel, rowLabel });

        return Squares.Contains(squareLabel) ? this[Squares.IndexOf(squareLabel)] : throw new System.ArgumentOutOfRangeException(nameof(squareLabel));
      }
      set
      {
        var squareLabel = new string(new char[] { columnLabel, rowLabel });

        this[Squares.IndexOf(squareLabel)] = Squares.Contains(squareLabel) ? value : throw new System.ArgumentOutOfRangeException(nameof(squareLabel));
      }
    }
    public ChessPieceType this[string squareLabel]
    {
      get => Squares.Contains(squareLabel) ? this[Squares.IndexOf(squareLabel)] : throw new System.ArgumentOutOfRangeException(nameof(squareLabel));
      set => this[Squares.IndexOf(squareLabel)] = Squares.Contains(squareLabel) ? value : throw new System.ArgumentOutOfRangeException(nameof(squareLabel));
    }

    public ChessGrid()
      : base(LabelsOfFiles.Length, LabelsOfRanks.Length)
    {
      Initialize(ChessPieceType.Empty);

      this["a1"] = ChessPieceType.WhiteRook;
      this["b1"] = ChessPieceType.WhiteKnight;
      this["c1"] = ChessPieceType.WhiteBishop;
      this["d1"] = ChessPieceType.WhiteKing;
      this["e1"] = ChessPieceType.WhiteQueen;
      this["f1"] = ChessPieceType.WhiteBishop;
      this["g1"] = ChessPieceType.WhiteKnight;
      this["h1"] = ChessPieceType.WhiteRook;

      this["a2"] = ChessPieceType.WhitePawn;
      this["b2"] = ChessPieceType.WhitePawn;
      this["c2"] = ChessPieceType.WhitePawn;
      this["d2"] = ChessPieceType.WhitePawn;
      this["e2"] = ChessPieceType.WhitePawn;
      this["f2"] = ChessPieceType.WhitePawn;
      this["g2"] = ChessPieceType.WhitePawn;
      this["h2"] = ChessPieceType.WhitePawn;

      this["a8"] = ChessPieceType.BlackRook;
      this["b8"] = ChessPieceType.BlackKnight;
      this["c8"] = ChessPieceType.BlackBishop;
      this["d8"] = ChessPieceType.BlackKing;
      this["e8"] = ChessPieceType.BlackQueen;
      this["f8"] = ChessPieceType.BlackBishop;
      this["g8"] = ChessPieceType.BlackKnight;
      this["h8"] = ChessPieceType.BlackRook;

      this["a7"] = ChessPieceType.BlackPawn;
      this["b7"] = ChessPieceType.BlackPawn;
      this["c7"] = ChessPieceType.BlackPawn;
      this["d7"] = ChessPieceType.BlackPawn;
      this["e7"] = ChessPieceType.BlackPawn;
      this["f7"] = ChessPieceType.BlackPawn;
      this["g7"] = ChessPieceType.BlackPawn;
      this["h7"] = ChessPieceType.BlackPawn;
    }
    public string ToString(System.Func<ChessPieceType, string> value)
    {
#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional
      var a = new string[LabelsOfRanks.Length + 2, LabelsOfFiles.Length + 2];
#pragma warning restore CA1814 // Prefer jagged arrays over multidimensional

      for (var ri = 0; ri < LabelsOfRanks.Length; ri++)
      {
        for (var ci = 0; ci < LabelsOfFiles.Length; ci++)
        {
          a[ri + 1, ci + 1] = this[LabelsOfFiles[ci], LabelsOfRanks[ri]].ToNotation();
        }
      }

      for (var ri = 0; ri < LabelsOfRanks.Length; ri++)
      {
        a[ri + 1, 0] = LabelsOfRanks[ri].ToString();
        a[ri + 1, 9] = LabelsOfRanks[ri].ToString();
      }

      for (var ci = 0; ci < LabelsOfFiles.Length; ci++)
      {
        a[0, ci + 1] = LabelsOfFiles[ci].ToString();
        a[9, ci + 1] = LabelsOfFiles[ci].ToString();
      }

      return a.ToConsoleString(true);
    }
    public override string ToString()
      => ToString(t => t.ToString());
  }
}
