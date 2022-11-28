//using System.Linq;

//namespace Flux.Model.TicTacToe2
//{
//  public struct Move
//  {
//    public Point2 Point { get; }

//    public int MaxScore { get; }
//    public int MinScore { get; }

//    public Move(int row, int column, int maxScore, int minScore)
//    {
//      Point = new Point2(column, row);

//      MaxScore = maxScore;
//      MinScore = minScore;
//    }

//    public override string ToString()
//      => $"{GetType().Name} {{ {Point} = {MaxScore,4} > {MinScore,4} }}";
//  }

//  public sealed class Game
//  {
//    public static int GetGrundy(int n, int k)
//      => n % (k + 1);

//    public static int GetMex(System.Collections.Generic.HashSet<int> set)
//    {
//      var mex = 0;
//      while (set.Contains(mex))
//        mex++;
//      return mex;
//    }

//    public static bool IsMovesLeft(int[,] board)
//    {
//      var rows = board.GetLength(0);
//      var columns = board.GetLength(1);

//      for (int r = rows - 1; r >= 0; r--)
//        for (int c = columns - 1; c >= 0; c--)
//          if (board[r, c] == 0)
//            return true;

//      return false;
//    }

//    public const int MaxWinScore = +127;
//    public const int MinWinScore = -127;

//    public const int MaxMoveScore = -32767;
//    public const int MinMoveScore = +32767;

//    public const int Length = 3;

//    public static int EvaluateBoard4(int[,] board)
//    {
//      var rows = board.GetLength(0);
//      var columns = board.GetLength(1);

//      for (var r = rows - Length; r >= 0; r--)
//      {
//        for (var c = columns - 1; c >= 0; c--)
//        {
//          if (board[r, c] is var value && value != 0)
//            if (CheckVertical(board, r, c, value))
//            {
//              if (value == 1) return MaxWinScore;
//              else if (value == 2) return MinWinScore;
//            }
//        }
//      }

//      for (var r = rows - 1; r >= 0; r--)
//      {
//        for (var c = columns - Length; c >= 0; c--)
//        {
//          if (board[r, c] is var value && value != 0)
//            if (CheckHorizontal(board, r, c, value))
//            {
//              if (value == 1) return MaxWinScore;
//              else if (value == 2) return MinWinScore;
//            }
//        }
//      }

//      for (var c = columns - Length; c >= 0; c--)
//      {
//        for (var r = rows - 1; r >= Length - 1; r--)
//        {
//          if (board[r, c] is var value && value != 0)
//            if (CheckDiagonalRightUp(board, r, c, value))
//            {
//              if (value == 1) return MaxWinScore;
//              else if (value == 2) return MinWinScore;
//            }
//        }
//        for (var r = rows - Length; r >= 0; r--)
//        {
//          if (board[r, c] is var value && value != 0)
//            if (CheckDiagonalRightDown(board, r, c, value))
//            {
//              if (value == 1) return MaxWinScore;
//              else if (value == 2) return MinWinScore;
//            }
//        }
//      }

//      return 0;

//      static bool CheckDiagonalRightDown(int[,] board, int r, int c, int value)
//      {
//        for (var l = Length - 1; l >= 1; l--)
//          if (board[r + l, c + l] != value)
//            return false;
//        return true;
//      }
//      static bool CheckDiagonalRightUp(int[,] board, int r, int c, int value)
//      {
//        for (var l = Length - 1; l >= 1; l--)
//          if (board[r - l, c + l] != value)
//            return false;
//        return true;
//      }
//      static bool CheckVertical(int[,] board, int r, int c, int value)
//      {
//        for (var l = Length - 1; l >= 1; l--)
//          if (board[r + l, c] != value)
//            return false;
//        return true;
//      }
//      static bool CheckHorizontal(int[,] board, int r, int c, int value)
//      {
//        for (var l = Length - 1; l >= 1; l--)
//          if (board[r, c + l] != value)
//            return false;
//        return true;
//      }
//    }

//    public static int EvaluateBoard(int[,] board)
//    {
//      var rows = board.GetLength(0);
//      var columns = board.GetLength(1);

//      for (var r = 0; r < rows; r++) // Rows.
//      {
//        if (board[r, 0] == board[r, 1] && board[r, 1] == board[r, 2])
//        {
//          if (board[r, 0] == 1) return MaxWinScore;
//          else if (board[r, 0] == 2) return MinWinScore;
//        }
//      }

//      for (var c = 0; c < columns; c++) // Columns.
//      {
//        if (board[0, c] == board[1, c] && board[1, c] == board[2, c])
//        {
//          if (board[0, c] == 1) return MaxWinScore;
//          else if (board[0, c] == 2) return MinWinScore;
//        }
//      }

//      if (board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2]) // Diagonal top-left to bottom-right.
//      {
//        if (board[0, 0] == 1) return MaxWinScore;
//        else if (board[0, 0] == 2) return MinWinScore;
//      }

//      if (board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0]) // Diagonal top-right to bottom-left.
//      {
//        if (board[0, 2] == 1) return MaxWinScore;
//        else if (board[0, 2] == 2) return MinWinScore;
//      }

//      return 0; // No winner yet.
//    }
//    public static System.Collections.Generic.Dictionary<int, int> GetCounts(int[,] board)
//    {
//      var rows = board.GetLength(0);
//      var columns = board.GetLength(1);

//      var counts = new System.Collections.Generic.Dictionary<int, int>();

//      for (var r = rows - 1; r >= 0; r--)
//        for (var c = columns - 1; c >= 0; c--)
//          if (board[r, c] is var key && counts.ContainsKey(key))
//            counts[key]++;
//          else
//            counts.Add(key, 1);

//      return counts;
//    }
//    public static System.Collections.Generic.Dictionary<int, int> GetCounts(int[,] board, out int playerUp)
//    {
//      var counts = GetCounts(board);

//      if (!counts.ContainsKey(0))
//        counts.Add(0, 0);
//      if (!counts.ContainsKey(1))
//        counts.Add(1, 0);
//      if (!counts.ContainsKey(2))
//        counts.Add(2, 0);

//      playerUp = counts[1] < counts[2] ? 1 : counts[2] < counts[1] ? 2 : counts[0] > 0 ? 3 : 0;

//      return counts;
//    }

//    public static bool TryGetMove(int[,] board, Point2 location, out int maxScore, out int minScore)
//    {
//      if (board[location.Y, location.X] == 0) // can only try empty squares.
//      {
//        board[location.Y, location.X] = 1; // Make move.
//        maxScore = BoardMinimax(board, 0, false); // Evaluate player min move.
//        board[location.Y, location.X] = 0; // Undo move.

//        board[location.Y, location.X] = 2; // Make move.
//        minScore = BoardMinimax(board, 0, true); // Evaluate player max move.
//        board[location.Y, location.X] = 0; // Undo move.

//        return true;
//      }
//      else
//      {
//        maxScore = 0;
//        minScore = 0;

//        return false;
//      }
//    }
//    public static bool TryGetMove4(int[,] board, int r, int c, out int maxScore, out int minScore)
//    {
//      if (board[r, c] == 0) // can only try empty squares.
//      {
//        board[r, c] = 1; // Make move.
//        maxScore = BoardMinimax4(board, 0, false, int.MinValue, int.MaxValue); // Evaluate player min move.
//        //board[r,c] = 0; // Undo move.

//        board[r, c] = 2; // Make move.
//        minScore = BoardMinimax4(board, 0, true, int.MinValue, int.MaxValue); // Evaluate player max move.

//        board[r, c] = 0; // Undo move.

//        return true;
//      }
//      else
//      {
//        maxScore = 0;
//        minScore = 0;

//        return false;
//      }
//    }

//    public static void GetBestMove(int[,] board, out Move maxMove, out Move minMove)
//    {
//      using var e = GetMoves(board).GetEnumerator();

//      if (e.MoveNext())
//      {
//        maxMove = e.Current;
//        minMove = e.Current;

//        while (e.MoveNext() && e.Current is var nextMove)
//        {
//          if (nextMove.MaxScore > maxMove.MaxScore)
//            maxMove = nextMove;
//          else if (nextMove.MaxScore == maxMove.MaxScore && nextMove.MinScore < maxMove.MinScore)
//            maxMove = nextMove;

//          if (nextMove.MinScore < minMove.MinScore)
//            minMove = nextMove;
//          else if (nextMove.MinScore == minMove.MinScore && nextMove.MaxScore > minMove.MaxScore)
//            minMove = nextMove;
//        }
//      }
//      else throw new System.ArgumentException(@"There are no moves.");
//    }

//    public static System.Collections.Generic.List<Move> GetMoves4(int[,] board, out Move maxMove, out Move minMove)
//    {
//      var rows = board.GetLength(0);
//      var columns = board.GetLength(1);

//      var moves = new System.Collections.Generic.List<Move>();

//      for (var r = rows - 1; r >= 0; r--)
//        for (var c = columns - 1; c >= 0; c--)
//          if (TryGetMove4(board, r, c, out var maxScore, out var minScore))
//            moves.Add(new Move(r, c, maxScore, minScore));

//      using var e = moves.GetEnumerator();

//      if (e.MoveNext())
//      {
//        maxMove = e.Current;
//        minMove = e.Current;

//        while (e.MoveNext() && e.Current is var nextMove)
//        {
//          if (nextMove.MaxScore > maxMove.MaxScore)
//            maxMove = nextMove;
//          else if (nextMove.MaxScore == maxMove.MaxScore && nextMove.MinScore > maxMove.MinScore)
//            maxMove = nextMove;

//          if (nextMove.MinScore < minMove.MinScore)
//            minMove = nextMove;
//          else if (nextMove.MinScore == minMove.MinScore && nextMove.MaxScore < minMove.MaxScore)
//            minMove = nextMove;
//        }
//      }
//      else throw new System.ArgumentException(@"There are no moves.");

//      return moves;
//    }
//    public static System.Collections.Generic.List<Move> GetMoves(int[,] board)
//    {
//      var rows = board.GetLength(0);
//      var columns = board.GetLength(1);

//      var moves = new System.Collections.Generic.List<Move>();
//      for (var r = rows - 1; r >= 0; r--)
//        for (var c = columns - 1; c >= 0; c--)
//          if (TryGetMove(board, new Point2(c, r), out var maxScore, out var minScore))
//            moves.Add(new Move(r, c, maxScore, minScore));
//      return moves;
//    }

//    public static int BoardMinimax4(int[,] board, int depth, bool isMax, int alpha, int beta)
//    {
//      var rows = board.GetLength(0);
//      var columns = board.GetLength(1);

//      var score = EvaluateBoard4(board);

//      if (score == MaxWinScore)
//        return score - depth;

//      if (score == MinWinScore)
//        return score + depth;

//      if (IsMovesLeft(board) == false)
//        return 0; // Tie.

//      if (isMax)
//      {
//        var best = MaxMoveScore;
//        for (var r = rows - 1; r >= 0; r--)
//          for (var c = columns - 1; c >= 0; c--)
//            if (board[r, c] == 0)
//            {
//              board[r, c] = 1;
//              best = System.Math.Max(best, BoardMinimax4(board, depth + 1, !isMax, alpha, beta));
//              board[r, c] = 0;
//              alpha = System.Math.Max(alpha, best);
//              if (beta <= alpha)
//                return best;
//            }
//        return best;
//      }
//      else
//      {
//        var best = MinMoveScore;
//        for (var r = rows - 1; r >= 0; r--)
//          for (var c = columns - 1; c >= 0; c--)
//            if (board[r, c] == 0)
//            {
//              board[r, c] = 2;
//              best = System.Math.Min(best, BoardMinimax4(board, depth + 1, !isMax, alpha, beta));
//              board[r, c] = 0;
//              beta = System.Math.Min(beta, best);
//              if (beta <= alpha)
//                return best;
//            }
//        return best;
//      }
//    }

//    public static int BoardMinimax(int[,] board, int depth, bool isMax)
//    {
//      var score = EvaluateBoard(board);

//      if (score == MaxWinScore)
//        return score - depth;

//      if (score == MinWinScore)
//        return score + depth;

//      if (IsMovesLeft(board) == false)
//        return 0; // Tie.

//      if (isMax)
//      {
//        var best = MaxMoveScore;
//        for (int i = 0; i < 3; i++)
//          for (int j = 0; j < 3; j++)
//            if (board[i, j] == 0)
//            {
//              board[i, j] = 1;
//              best = System.Math.Max(best, BoardMinimax(board, depth + 1, !isMax));
//              board[i, j] = 0;
//            }
//        return best;
//      }
//      else
//      {
//        var best = MinMoveScore;
//        for (int i = 0; i < 3; i++)
//          for (int j = 0; j < 3; j++)
//            // Check if cell is empty
//            if (board[i, j] == 0)
//            {
//              board[i, j] = 2;
//              best = System.Math.Min(best, BoardMinimax(board, depth + 1, !isMax));
//              board[i, j] = 0;
//            }
//        return best;
//      }
//    }

//    public int Minimax(int depthIndex, int nodeIndex, bool isMax, int[] scoreTree, int alpha, int beta, int maxDepthIndex)
//    {
//      if (depthIndex == maxDepthIndex)
//        return scoreTree[nodeIndex];

//      if (isMax)
//      {
//        var max = (int)short.MinValue;
//        for (var index = 0; index < 2; index++)
//        {
//          max = System.Math.Max(max, Minimax(depthIndex + 1, nodeIndex * 2 + index, !isMax, scoreTree, alpha, beta, maxDepthIndex));
//          alpha = System.Math.Max(alpha, max);

//          if (beta <= alpha)
//            break;
//        }
//        return max;
//      }
//      else
//      {
//        var min = (int)short.MaxValue;
//        for (var index = 0; index < 2; index++)
//        {
//          min = System.Math.Min(min, Minimax(depthIndex + 1, nodeIndex * 2 + index, !isMax, scoreTree, alpha, beta, maxDepthIndex));
//          beta = System.Math.Min(beta, min);

//          if (beta <= alpha)
//            break;
//        }
//        return min;
//      }
//    }
//    public int Minimax(bool isMax, int[] scoreTree)
//      => Minimax(0, 0, isMax, scoreTree, int.MinValue, int.MaxValue, System.Math.ILogB(scoreTree.Length));
//  }
//}
