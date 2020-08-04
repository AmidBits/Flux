namespace Flux.Model
{
  // https://www.geeksforgeeks.org/minimax-algorithm-in-game-theory-set-3-tic-tac-toe-ai-finding-optimal-move/
  public class TicTacToe
  {
    public const int MaxValue = +1000;
    public const int MinValue = -1000;

    public const char Player = 'x';
    public const char Opponent = 'o';
    public const char Empty = '*';

    public struct Move
    {
      public int row, col;

      public override string ToString()
      {
        return $"R={row}, C={col}";
      }
    };

    // This function returns true if there are moves remaining on the board. It returns false if there are no moves left to play. 
    bool AnysRemainingMove(char[,] board)
    {
      for (int i = 0; i < 3; i++)
        for (int j = 0; j < 3; j++)
          if (board[i, j] == Empty)
            return true;

      return false;
    }

    // This is the evaluation function as discussed in the previous article ( http://goo.gl/sJgv68 ) 
    public int Evaluate(char[,] b)
    {
      // Checking for Rows for X or O victory. 
      for (int row = 0; row < 3; row++)
      {
        if (b[row, 0] == b[row, 1] &&
            b[row, 1] == b[row, 2])
        {
          if (b[row, 0] == Player)
            return +10;
          else if (b[row, 0] == Opponent)
            return -10;
        }
      }

      // Checking for Columns for X or O victory. 
      for (int col = 0; col < 3; col++)
      {
        if (b[0, col] == b[1, col] &&
            b[1, col] == b[2, col])
        {
          if (b[0, col] == Player)
            return +10;

          else if (b[0, col] == Opponent)
            return -10;
        }
      }

      // Checking for Diagonals for X or O victory. 
      if (b[0, 0] == b[1, 1] && b[1, 1] == b[2, 2])
      {
        if (b[0, 0] == Player)
          return +10;
        else if (b[0, 0] == Opponent)
          return -10;
      }

      if (b[0, 2] == b[1, 1] && b[1, 1] == b[2, 0])
      {
        if (b[0, 2] == Player)
          return +10;
        else if (b[0, 2] == Opponent)
          return -10;
      }

      // Else if none of them have won then return 0 
      return 0;
    }

    /// <summary>This is the minimax function. It considers all the possible ways the game can go and returns the value of the board.</summary>
    /// <returns>The value of the board.</returns>
    public int Minimax(char[,] board, int depth, bool isMaximizingPlayer)
    {
      int score = Evaluate(board);

      if (score == +10) return score - depth; // Maximizer won? Return evaluated score.
      if (score == -10) return score + depth; // Minimizer won? Return minimizer score.

      if (AnysRemainingMove(board) == false) return 0; // No more moves? It's a tie.

      if (isMaximizingPlayer)
      {
        var best = MinValue;

        for (int i = 0; i < 3; i++)
        {
          for (int j = 0; j < 3; j++)
          {
            if (board[i, j] == Empty)
            {
              board[i, j] = Player; // Perform the move.

              best = System.Math.Max(best, Minimax(board, depth + 1, !isMaximizingPlayer)); // Call minimax recursively and choose the maximum value.

              board[i, j] = Empty; // Undo the move.
            }
          }
        }

        return best;
      }
      else // This is the minimizer's move. 
      {
        var best = MaxValue;

        for (int i = 0; i < 3; i++)
        {
          for (int j = 0; j < 3; j++)
          {
            if (board[i, j] == Empty)
            {
              board[i, j] = Opponent; // Perform the move 

              best = System.Math.Min(best, Minimax(board, depth + 1, !isMaximizingPlayer)); // Call minimax recursively and choose the minimum value.

              board[i, j] = Empty; // Undo the move.
            }
          }
        }

        return best;
      }
    }

    public int Minimax(int depth, int nodeIndex, bool isMaximizingPlayer, int[] values, int alpha, int beta)
    {
      if (depth == 3) return values[nodeIndex]; // Can only be 3 squares deep.

      if (isMaximizingPlayer)
      {
        var best = MinValue;

        for (int i = 0; i < 2; i++) // Recur for left and right children.
        {
          best = System.Math.Max(best, Minimax(depth + 1, nodeIndex * 2 + i, false, values, alpha, beta));

          alpha = System.Math.Max(alpha, best);

          if (beta <= alpha) break; // Alpha beta pruning.
        }

        return best;
      }
      else // This is the minimizer's move. 
      {
        var best = MaxValue;

        for (int i = 0; i < 2; i++) // Recur for left and right children.
        {
          best = System.Math.Min(best, Minimax(depth + 1, nodeIndex * 2 + i, true, values, alpha, beta));

          beta = System.Math.Min(beta, best);

          if (beta <= alpha) break; // Alpha beta pruning.
        }
        return best;
      }
    }

    /// <summary>This will return the best possible move for the player.</summary>
    public Move FindBestMove(char[,] board)
    {
      int bestVal = -1000;
      Move bestMove;
      bestMove.row = -1;
      bestMove.col = -1;

      // Traverse all cells, evaluate minimax function for 
      // all empty cells. And return the cell with optimal 
      // value. 
      for (int i = 0; i < 3; i++)
      {
        for (int j = 0; j < 3; j++)
        {
          // Check if cell is empty 
          if (board[i, j] == Empty)
          {
            // Make the move 
            board[i, j] = Player;

            // compute evaluation function for this 
            // move. 
            int moveVal = Minimax(board, 0, false);

            // Undo the move 
            board[i, j] = Empty;

            // If the value of the current move is 
            // more than the best value, then update 
            // best/ 
            if (moveVal > bestVal)
            {
              bestMove.row = i;
              bestMove.col = j;
              bestVal = moveVal;
            }
          }
        }
      }

      System.Console.WriteLine("The value of the best Move is : {0}\n\n", bestVal);

      return bestMove;
    }
  }
}
