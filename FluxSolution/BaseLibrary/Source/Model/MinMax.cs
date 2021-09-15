namespace Flux.Model.TicTacToe2
{
  public sealed class Move
  {
    public int Row { get; set; }
    public int Column { get; set; }

    public int Score { get; set; }

    public Move(int row, int column, int score)
    {
      Row = row;
      Column = column;

      Score = score;
    }

    public override string ToString()
      => $"<[{Row}, {Column}] = {Score}>";
  }

  public class Game
  {
    public virtual int GetGrundy(int n, int k)
      => n % (k + 1);

    public static int GetMex(System.Collections.Generic.HashSet<int> set)
    {
      var mex = 0;
      while (set.Contains(mex))
        mex++;
      return mex;
    }

    public const int CcPlayer1 = 1;
    public const int CcPlayer2 = 2;
    private const int CcEmpty = 0;

    public const int Rows = 3;
    public const int Columns = 3;

    public static bool IsMovesLeft(int[,] board)
    {
      for (var r = 0; r < Rows; r++)
        for (var c = 0; c < Columns; c++)
          if (board[r, c] == CcEmpty)
            return true;

      return false;
    }

    public const int MaxWinScore = sbyte.MaxValue;
    public const int MinWinScore = sbyte.MinValue;

    public const int MaxMoveScore = short.MinValue;
    public const int MinMoveScore = short.MaxValue;

    public static int EvaluateBoard(int[,] board)
    {
      for (var r = 0; r < Rows; r++) // Rows.
      {
        if (board[r, 0] == board[r, 1] && board[r, 1] == board[r, 2])
        {
          if (board[r, 0] == CcPlayer1) return MaxWinScore;
          else if (board[r, 0] == CcPlayer2) return MinWinScore;
        }
      }

      for (var c = 0; c < Columns; c++) // Columns.
      {
        if (board[0, c] == board[1, c] && board[1, c] == board[2, c])
        {
          if (board[0, c] == CcPlayer1) return MaxWinScore;
          else if (board[0, c] == CcPlayer2) return MinWinScore;
        }
      }

      if (board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2]) // Diagonal top-left to bottom-right.
      {
        if (board[0, 0] == CcPlayer1) return MaxWinScore;
        else if (board[0, 0] == CcPlayer2) return MinWinScore;
      }

      if (board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0]) // Diagonal top-right to bottom-left.
      {
        if (board[0, 2] == CcPlayer1) return MaxWinScore;
        else if (board[0, 2] == CcPlayer2) return MinWinScore;
      }

      return 0; // No winner yet.
    }
    public static System.Collections.Generic.SortedDictionary<Geometry.Point2, int> GetMovesForPlayer1(int[,] board)
    {
      var moves = new System.Collections.Generic.SortedDictionary<Geometry.Point2, int>();
      for (int r = 0; r < Rows; r++)
        for (int c = 0; c < Columns; c++)
          if (board[r, c] == CcEmpty) // can only try empty squares.
          {
            board[r, c] = CcPlayer1; // Make move.
            var score = BoardMinimax(board, 0, false); // Evaluate move.
            board[r, c] = CcEmpty; // Undo move.
            moves.Add(new Geometry.Point2(c, r), score);
          }
      return moves;
    }
    public static System.Collections.Generic.SortedDictionary<Geometry.Point2, int> GetMovesForPlayer2(int[,] board)
    {
      var moves = new System.Collections.Generic.SortedDictionary<Geometry.Point2, int>();
      for (int r = 0; r < Rows; r++)
        for (int c = 0; c < Columns; c++)
          if (board[r, c] == CcEmpty) // can only try empty squares.
          {
            board[r, c] = CcPlayer2; // Make move.
            var score = BoardMinimax(board, 0, true); // Evaluate move.
            board[r, c] = CcEmpty; // Undo move.
            moves.Add(new Geometry.Point2(c, r), score);
          }
      return moves;
    }
    public static int BoardMinimax(int[,] board, int depth, bool isMax)
    {
      var score = EvaluateBoard(board);

      if (score == MaxWinScore)
        return score - depth;

      if (score == MinWinScore)
        return score - depth;

      if (IsMovesLeft(board) == false)
        return 0; // Tie.

      if (isMax)
      {
        var best = MaxMoveScore;
        for (int i = 0; i < 3; i++)
          for (int j = 0; j < 3; j++)
            if (board[i, j] == CcEmpty)
            {
              board[i, j] = CcPlayer1;
              best = System.Math.Max(best, BoardMinimax(board, depth + 1, !isMax));
              board[i, j] = CcEmpty;
            }
        return best;
      }
      else
      {
        var best = MinMoveScore;
        for (int i = 0; i < 3; i++)
          for (int j = 0; j < 3; j++)
            // Check if cell is empty
            if (board[i, j] == CcEmpty)
            {
              board[i, j] = CcPlayer2;
              best = System.Math.Min(best, BoardMinimax(board, depth + 1, !isMax));
              board[i, j] = CcEmpty;
            }
        return best;
      }
    }

    public int Minimax(int depthIndex, int nodeIndex, bool isMax, int[] scoreTree, int alpha, int beta, int maxDepthIndex)
    {
      if (depthIndex == maxDepthIndex)
        return scoreTree[nodeIndex];

      if (isMax)
      {
        var max = (int)short.MinValue;
        for (var index = 0; index < 2; index++)
        {
          max = System.Math.Max(max, Minimax(depthIndex + 1, nodeIndex * 2 + index, !isMax, scoreTree, alpha, beta, maxDepthIndex));
          alpha = System.Math.Max(alpha, max);

          if (beta <= alpha)
            break;
        }
        return max;
      }
      else
      {
        var min = (int)short.MaxValue;
        for (var index = 0; index < 2; index++)
        {
          min = System.Math.Min(min, Minimax(depthIndex + 1, nodeIndex * 2 + index, !isMax, scoreTree, alpha, beta, maxDepthIndex));
          beta = System.Math.Min(beta, min);

          if (beta <= alpha)
            break;
        }
        return min;
      }
    }
    public int Minimax(bool isMax, int[] scoreTree)
      => Minimax(0, 0, isMax, scoreTree, int.MinValue, int.MaxValue, System.Math.ILogB(scoreTree.Length));
  }
}
