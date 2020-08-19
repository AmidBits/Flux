namespace Flux.Model.TicTacToe
{
  public struct Move
  {
    public int Column { get; set; }
    public int Row { get; set; }
    public int Score { get; set; }

    public Move(int row, int column, int score)
    {
      Column = column;
      Row = row;
      Score = score;
    }

    public override string ToString()
      => $"<({Row}, {Column}) {Score}>";
  };

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional
  // https://www.geeksforgeeks.org/minimax-algorithm-in-game-theory-set-3-tic-tac-toe-ai-finding-optimal-move/
  public class Tools
  {
    // This function returns true if there are moves remaining on the board. It returns false if there are no moves left to play. 
    public static bool HasMoreMoves(char[,] board, char empty)
    {
      if (board is null) throw new System.ArgumentNullException(nameof(board));

      foreach (var square in board)
        if (square == empty)
          return true;

      return false;
    }

    // This is the evaluation function as discussed in the previous article ( http://goo.gl/sJgv68 ) 
    public static int Evaluate(char[,] board, char player, char opponent)
    {
      if (board is null) throw new System.ArgumentNullException(nameof(board));

      for (var r = 0; r < 3; r++)
      {
        var br1 = board[r, 1];

        if (board[r, 0] == br1 && br1 == board[r, 2])
        {
          if (br1 == player) return +10;
          else if (br1 == opponent) return -10;
        }
      }

      for (var c = 0; c < 3; c++)
      {
        var b1c = board[1, c];

        if (board[0, c] == b1c && b1c == board[2, c])
        {
          if (b1c == player) return +10;
          else if (b1c == opponent) return -10;
        }
      }

      var b11 = board[1, 1];

      if (board[0, 0] == b11 && b11 == board[2, 2]) // Check diagonal top-left to bottom-right.
      {
        if (b11 == player) return +10;
        else if (b11 == opponent) return -10;
      }

      if (board[0, 2] == b11 && b11 == board[2, 0]) // Check diagonal top-right to bottom-left.
      {
        if (b11 == player) return +10;
        else if (b11 == opponent) return -10;
      }

      return 0;
    }

    /// <summary>This is the minimax function. It considers all the possible ways the game can go and returns the value of the board.</summary>
    /// <returns>The value of the board.</returns>
    public static int Minimax(char[,] board, int depth, bool isMaximizingPlayer, char player, char opponent, char empty)
    {
      var score = Evaluate(board, player, opponent);

      //if (score == +10) return score - depth; // Maximizer won? Return evaluated score.
      //if (score == -10) return score + depth; // Minimizer won? Return minimizer score.
      if (score == +10) return score; // Maximizer won? Return evaluated score.
      if (score == -10) return score; // Minimizer won? Return minimizer score.

      if (HasMoreMoves(board, empty) == false) return 0; // No more moves? It's a tie.

      if (isMaximizingPlayer)
      {
        var bestScore = int.MinValue;

        for (var i = 0; i < 3; i++)
          for (var j = 0; j < 3; j++)
            if (board[i, j] == empty)
            {
              board[i, j] = player; // Perform the move.
              bestScore = System.Math.Max(bestScore, Minimax(board, depth + 1, isMaximizingPlayer, player, opponent, empty)); // Call minimax recursively and choose the maximum value.
              board[i, j] = empty; // Undo the move.
            }

        return bestScore;
      }
      else // This is the minimizer's move. 
      {
        var best = int.MaxValue;

        for (var i = 0; i < 3; i++)
          for (var j = 0; j < 3; j++)
            if (board[i, j] == empty)
            {
              board[i, j] = opponent; // Perform the move 
              best = System.Math.Min(best, Minimax(board, depth + 1, !isMaximizingPlayer, player, opponent, empty)); // Call minimax recursively and choose the minimum value.
              board[i, j] = empty; // Undo the move.
            }

        return best;
      }
    }
    ///// <summary>This is the minimax function. It considers all the possible ways the game can go and returns the value of the board.</summary>
    ///// <returns>The value of the board.</returns>
    //public int Minimax(char[,] board, int depth, bool isMaximizingPlayer)
    //{
    //  int score = Evaluate(board, Player, Opponent);

    //  //if (score == +10) return score - depth; // Maximizer won? Return evaluated score.
    //  //if (score == -10) return score + depth; // Minimizer won? Return minimizer score.
    //  if (score == +10) return score; // Maximizer won? Return evaluated score.
    //  if (score == -10) return score; // Minimizer won? Return minimizer score.

    //  if (!AnyMoves(board, Empty)) return 0; // No more moves? It's a tie.

    //  if (isMaximizingPlayer)
    //  {
    //    var best = MinValue;

    //    for (int i = 0; i < 3; i++)
    //    {
    //      for (int j = 0; j < 3; j++)
    //      {
    //        if (board[i, j] == Empty)
    //        {
    //          board[i, j] = Player; // Perform the move.

    //          best = System.Math.Max(best, Minimax(board, depth + 1, !isMaximizingPlayer)); // Call minimax recursively and choose the maximum value.

    //          board[i, j] = Empty; // Undo the move.
    //        }
    //      }
    //    }

    //    return best;
    //  }
    //  else // This is the minimizer's move. 
    //  {
    //    var best = MaxValue;

    //    for (int i = 0; i < 3; i++)
    //    {
    //      for (int j = 0; j < 3; j++)
    //      {
    //        if (board[i, j] == Empty)
    //        {
    //          board[i, j] = Opponent; // Perform the move 

    //          best = System.Math.Min(best, Minimax(board, depth + 1, !isMaximizingPlayer)); // Call minimax recursively and choose the minimum value.

    //          board[i, j] = Empty; // Undo the move.
    //        }
    //      }
    //    }

    //    return best;
    //  }
    //}

    //public int Minimax(int depth, int nodeIndex, bool isMaximizingPlayer, int[] values, int alpha, int beta)
    //{
    //  if (depth == 3) return values[nodeIndex]; // Can only be 3 squares deep.

    //  if (isMaximizingPlayer)
    //  {
    //    var best = -1000;

    //    for (int i = 0; i < 2; i++) // Recur for left and right children.
    //    {
    //      best = System.Math.Max(best, Minimax(depth + 1, nodeIndex * 2 + i, false, values, alpha, beta));

    //      alpha = System.Math.Max(alpha, best);

    //      if (beta <= alpha) break; // Alpha beta pruning.
    //    }

    //    return best;
    //  }
    //  else // This is the minimizer's move. 
    //  {
    //    var best = 1000;

    //    for (int i = 0; i < 2; i++) // Recur for left and right children.
    //    {
    //      best = System.Math.Min(best, Minimax(depth + 1, nodeIndex * 2 + i, true, values, alpha, beta));

    //      beta = System.Math.Min(beta, best);

    //      if (beta <= alpha) break; // Alpha beta pruning.
    //    }
    //    return best;
    //  }
    //}

    /// <summary>This will return the best possible move for the player.</summary>
    public static Move FindBestMove(char[,] board, char player, char opponent, char empty)
    {
      if (board is null) throw new System.ArgumentNullException(nameof(board));

      var best = new Move
      {
        Column = -1,
        Row = -1,
        Score = -1000
      };

      // Traverse all cells, evaluate minimax function for all empty cells. And return the cell with optimal value. 
      for (int i = 0; i < 3; i++)
      {
        for (int j = 0; j < 3; j++)
        {
          if (board[i, j] == empty)
          {
            board[i, j] = player; // Make move.

            var moveScore = Minimax(board, 0, false, player, opponent, empty);

            board[i, j] = empty; // Undo move.

            // Update best score with score of the move if it's greater.
            if (moveScore > best.Score)
            {
              best.Row = i;
              best.Column = j;
              best.Score = moveScore;
            }
          }
        }
      }

      return best;
    }
  }
#pragma warning restore CA1814 // Prefer jagged arrays over multidimensional
}
