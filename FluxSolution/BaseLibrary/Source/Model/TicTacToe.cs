namespace Flux.Model
{
#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional
  // https://www.geeksforgeeks.org/minimax-algorithm-in-game-theory-set-3-tic-tac-toe-ai-finding-optimal-move/
  public class TicTacToe
  {
    public struct Move
    {
      public int row, col, val;

      public override string ToString()
      {
        return $"R={row}, C={col}, V={val}";
      }
    };

    // This function returns true if there are moves remaining on the board. It returns false if there are no moves left to play. 
    public static bool HasMoreMoves(char[,] board, char empty)
    {
      if (board is null) throw new System.ArgumentNullException(nameof(board));

      for (int i = 0; i < 3; i++)
        for (int j = 0; j < 3; j++)
          if (board[i, j] == empty)
            return true;

      return false;
    }

    // This is the evaluation function as discussed in the previous article ( http://goo.gl/sJgv68 ) 
    public static int Evaluate(char[,] board, char player, char opponent)
    {
      if (board is null) throw new System.ArgumentNullException(nameof(board));

      for (int row = 0; row < 3; row++) // Check rows.
      {
        if (board[row, 0] == board[row, 1] && board[row, 1] == board[row, 2])
        {
          if (board[row, 0] == player) return +10;
          else if (board[row, 0] == opponent) return -10;
        }
      }

      for (int col = 0; col < 3; col++) // Check columns.
      {
        if (board[0, col] == board[1, col] && board[1, col] == board[2, col])
        {
          if (board[0, col] == player) return +10;
          else if (board[0, col] == opponent) return -10;
        }
      }

      if (board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2]) // Check diagonal top-left to bottom-right.
      {
        if (board[0, 0] == player) return +10;
        else if (board[0, 0] == opponent) return -10;
      }

      if (board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0]) // Check diagonal top-right to bottom-left.
      {
        if (board[0, 2] == player) return +10;
        else if (board[0, 2] == opponent) return -10;
      }

      return 0;
    }

    // This is the evaluation function as discussed in the previous article ( http://goo.gl/sJgv68 ) 
    //public int Evaluate(char[,] b)
    //{
    //  // Checking for Rows for X or O victory. 
    //  for (int row = 0; row < 3; row++)
    //  {
    //    if (board[row, 0] == board[row, 1] && board[row, 1] == board[row, 2])
    //    {
    //      if (board[row, 0] == Player) return +10;
    //      else if (board[row, 0] == Opponent) return -10;
    //    }
    //  }

    //  // Checking for Columns for X or O victory. 
    //  for (int col = 0; col < 3; col++)
    //  {
    //    if (board[0, col] == board[1, col] && board[1, col] == board[2, col])
    //    {
    //      if (board[0, col] == Player) return +10;
    //      else if (board[0, col] == Opponent) return -10;
    //    }
    //  }

    //  // Checking for Diagonals for X or O victory. 
    //  if (board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2])
    //  {
    //    if (board[0, 0] == Player) return +10;
    //    else if (board[0, 0] == Opponent) return -10;
    //  }

    //  if (board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0])
    //  {
    //    if (board[0, 2] == Player) return +10;
    //    else if (board[0, 2] == Opponent) return -10;
    //  }

    //  // Else if none of them have won then return 0 
    //  return 0;
    //}

    /// <summary>This is the minimax function. It considers all the possible ways the game can go and returns the value of the board.</summary>
    /// <returns>The value of the board.</returns>
    public static int Minimax(char[,] board, int depth, bool isMaximizingPlayer, char player, char opponent, char empty)
    {
      int score = Evaluate(board, player, opponent);

      //if (score == +10) return score - depth; // Maximizer won? Return evaluated score.
      //if (score == -10) return score + depth; // Minimizer won? Return minimizer score.
      if (score == +10) return score; // Maximizer won? Return evaluated score.
      if (score == -10) return score; // Minimizer won? Return minimizer score.

      if (HasMoreMoves(board, empty) == false) return 0; // No more moves? It's a tie.

      if (isMaximizingPlayer)
      {
        var best = -1000;

        for (int i = 0; i < 3; i++)
        {
          for (int j = 0; j < 3; j++)
          {
            if (board[i, j] == empty)
            {
              board[i, j] = player; // Perform the move.

              best = System.Math.Max(best, Minimax(board, depth + 1, !isMaximizingPlayer, player, opponent, empty)); // Call minimax recursively and choose the maximum value.

              board[i, j] = empty; // Undo the move.
            }
          }
        }

        return best;
      }
      else // This is the minimizer's move. 
      {
        var best = 1000;

        for (int i = 0; i < 3; i++)
        {
          for (int j = 0; j < 3; j++)
          {
            if (board[i, j] == empty)
            {
              board[i, j] = opponent; // Perform the move 

              best = System.Math.Min(best, Minimax(board, depth + 1, !isMaximizingPlayer, player, opponent, empty)); // Call minimax recursively and choose the minimum value.

              board[i, j] = empty; // Undo the move.
            }
          }
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

      int bestVal = -1000;
      var bestMove = new Move
      {
        row = -1,
        col = -1
      };

      // Traverse all cells, evaluate minimax function for all empty cells. And return the cell with optimal value. 
      for (int i = 0; i < 3; i++)
      {
        for (int j = 0; j < 3; j++)
        {
          // Check if cell is empty 
          if (board[i, j] == empty)
          {
            // Make the move 
            board[i, j] = player;

            // compute evaluation function for this move. 
            int moveValue = Minimax(board, 0, false, player, opponent, empty);

            //System.Console.WriteLine($"moveValue={moveValue}");

            // Undo the move 
            board[i, j] = empty;

            // If the value of the current move is 
            // more than the best value, then update 
            // best/ 
            if (moveValue > bestVal)
            {
              bestMove.row = i;
              bestMove.col = j;
              bestMove.val = moveValue;

              bestVal = moveValue;
            }
          }
        }
      }

      System.Console.WriteLine("The value of the best Move is : {0}\n\n", bestVal);

      return bestMove;
    }
  }
#pragma warning restore CA1814 // Prefer jagged arrays over multidimensional
}
