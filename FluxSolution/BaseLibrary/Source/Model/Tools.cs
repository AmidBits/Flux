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

      for (int row = 0; row < 3; row++)
      {
        if (board[row, 0] == board[row, 1] && board[row, 1] == board[row, 2])
        {
          if (board[row, 0] == player) return +10;
          else if (board[row, 0] == opponent) return -10;
        }
      }

      for (int col = 0; col < 3; col++)
      {
        if (board[0, col] == board[1, col] && board[1, col] == board[2, col])
        {
          if (board[0, col] == player) return +10;
          else if (board[0, col] == opponent) return -10;
        }
      }

      if (board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2])
      {
        if (board[0, 0] == player) return +10;
        else if (board[0, 0] == opponent) return -10;
      }

      if (board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0])
      {
        if (board[0, 2] == player) return +10;
        else if (board[0, 2] == opponent) return -10;
      }

      return 0; // No winner.
    }

    /// <summary>This is the minimax function. It considers all the possible ways the game can go and returns the value of the board.</summary>
    public static int Minimax(char[,] board, int depth, bool isMax, char player, char opponent, char empty)
    {
      int score = Evaluate(board, player, opponent);

      if (score == 10) return score - depth; // Maximizer won.
      if (score == -10) return score + depth; // Minimizer won.

      if (HasMoreMoves(board, empty) == false) return 0; // No more moves and no winner, it's a tie.

      var best = isMax ? int.MinValue : int.MaxValue;

      for (int i = 0; i < 3; i++)
      {
        for (int j = 0; j < 3; j++)
        {
          if (board[i, j] == empty)
          {
            if (isMax)
            {
              board[i, j] = player;
              best = System.Math.Max(best, Minimax(board, depth + 1, !isMax, player, opponent, empty));
              board[i, j] = empty;
            }
            else
            {
              board[i, j] = opponent;
              best = System.Math.Min(best, Minimax(board, depth + 1, !isMax, player, opponent, empty));
              board[i, j] = empty;
            }
          }
        }
      }

      return best;
    }

    /// <summary>Returns the optimal value a maximizer can obtain.</summary>
    /// <param name="depth">The current depth in game tree.</param>
    /// <param name="nodeIndex">The index of current node in scores[].</param>
    /// <param name="isMax">True if current move is of maximizer, else false if minimizer.</param>
    /// <param name="scores">The leaves of Game tree.</param>
    /// <param name="maxHeight">The maximum height of Game tree.</param>
    /// <returns></returns>
    public static int Minimax(int depth, int nodeIndex, bool isMax, int[] scores, int maxHeight)
    {
      if (scores is null) throw new System.ArgumentNullException(nameof(scores));

      if (depth == maxHeight) return scores[nodeIndex]; // Terminating condition.

      if (isMax) return System.Math.Max(Minimax(depth + 1, nodeIndex * 2, false, scores, maxHeight), Minimax(depth + 1, nodeIndex * 2 + 1, false, scores, maxHeight));
      else return System.Math.Min(Minimax(depth + 1, nodeIndex * 2, true, scores, maxHeight), Minimax(depth + 1, nodeIndex * 2 + 1, true, scores, maxHeight));
    }

    /// <summary>This will return the best possible move for the player.</summary>
    public static System.Collections.Generic.IEnumerable<Move> FindMoves(char[,] board, char player, char opponent, char empty)
    {
      if (board is null) throw new System.ArgumentNullException(nameof(board));

      for (int i = 0; i < 3; i++)
      {
        for (int j = 0; j < 3; j++)
        {
          if (board[i, j] == empty)
          {
            board[i, j] = player;
            int score = Minimax(board, 0, false, player, opponent, empty);
            board[i, j] = '_';

            yield return new Move(i, j, score);
          }
        }
      }
    }
  }
#pragma warning restore CA1814 // Prefer jagged arrays over multidimensional
}
