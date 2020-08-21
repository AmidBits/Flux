using System.Linq;

// https://www.geeksforgeeks.org/minimax-algorithm-in-game-theory-set-1-introduction/
// https://www.geeksforgeeks.org/minimax-algorithm-in-game-theory-set-2-evaluation-function/
// https://www.geeksforgeeks.org/minimax-algorithm-in-game-theory-set-3-tic-tac-toe-ai-finding-optimal-move/
#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional
namespace Flux.Model.TicTacToe
{
  public class Move
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
      => $"<({Row}, {Column}) {Score}>";
  }

  public enum State
  {
    Empty,
    Player1,
    Player2
  }

  public class Board
    : System.Collections.Generic.IEnumerable<State>
  {
    private State[] m_state = new State[9];

    /// <summary>Gets or sets the state of the specified [row, column] square on the board.</summary>
    public State this[int row, int column]
    {
      get 
        => m_state[row * 3 + column];
      set 
        => m_state[row * 3 + column] = value;
    }

    public Board() 
      => Clear();

    public void Clear() 
      => System.Array.Fill(m_state, State.Empty);

    public int Evaluate(bool isMax)
    {
      var player1 = isMax ? State.Player1 : State.Player2;
      var player2 = isMax ? State.Player2 : State.Player1;

      for (int row = 0; row < 3; row++) // Any row winner?
      {
        var boardR1 = this[row, 1];
        if (this[row, 0] == boardR1 && boardR1 == this[row, 2])
        {
          if (boardR1 == player1) return +10;
          else if (boardR1 == player2) return -10;
        }
      }

      for (int col = 0; col < 3; col++) // Any column winner?
      {
        var board1C = this[1, col];
        if (this[0, col] == board1C && board1C == this[2, col])
        {
          if (board1C == player1) return +10;
          else if (board1C == player2) return -10;
        }
      }

      var board11 = this[1, 1];

      if ((this[0, 0] == board11 && board11 == this[2, 2]) || (this[0, 2] == board11 && board11 == this[2, 0])) // Any diagonal winner?
      {
        if (board11 == player1) return +10;
        else if (board11 == player2) return -10;
      }

      return 0; // No winner yet.
    }

    public System.Collections.Generic.IList<Move> GetOptionsForPlayer1()
    {
      var moves = new System.Collections.Generic.List<Move>();

      for (var r = 0; r < 3; r++)
      {
        for (var c = 0; c < 3; c++)
        {
          if (this[r, c] == State.Empty)
          {
            this[r, c] = State.Player1;
            moves.Add(new Move(r, c, Minimax(0, false)));
            this[r, c] = State.Empty;
          }
        }
      }

      return moves;
    }
    public System.Collections.Generic.IList<Move> GetOptionsForPlayer2()
    {
      var moves = new System.Collections.Generic.List<Move>();

      for (var r = 0; r < 3; r++)
      {
        for (var c = 0; c < 3; c++)
        {
          if (this[r, c] == State.Empty)
          {
            this[r, c] = State.Player2;
            moves.Add(new Move(r, c, Minimax(0, true)));
            this[r, c] = State.Empty;
          }
        }
      }

      return moves;
    }

    public bool HasEmptySquares()
    {
      foreach (var state in this)
        if (state == State.Empty)
          return true;

      return false;
    }

    public int Minimax(int depth, bool isMax)
    {
      int score = Evaluate(isMax);

      if (score == 10) return score - depth; // Maximizer won.
      if (score == -10) return score + depth; // Minimizer won.

      if (!HasEmptySquares()) return 0; // No more moves and no winner, it's a tie.

      var best = isMax ? int.MinValue : int.MaxValue;

      for (int i = 0; i < 3; i++)
        for (int j = 0; j < 3; j++)
          if (this[i, j] == State.Empty)
          {
            if (isMax)
            {
              this[i, j] = State.Player1;
              best = System.Math.Max(best, Minimax(depth + 1, !isMax));
            }
            else
            {
              this[i, j] = State.Player2;
              best = System.Math.Min(best, Minimax(depth + 1, !isMax));
            }

            this[i, j] = State.Empty;
          }

      return best;
    }

    public System.Collections.Generic.IDictionary<State, int> StateCounts()
      => GetRowMajorOrder().GroupBy(s => s).ToDictionary(g => g.Key, g => g.Count());

    //public override string ToString<T>(System.Func<State, T> selector)
    //  =>

    public override string ToString()
      => GetRowMajorOrder2D(s => s switch { State.Empty => '-', State.Player1 => 'X', State.Player2 => 'O', _ => '?' }).ToConsoleString(false, '\0', '\0');

    // This is the evaluation function as discussed in the previous article ( http://goo.gl/sJgv68 ) 
    //public static int EvaluateBoard(char[,] board, char player, char opponent)
    //{
    //  if (board is null) throw new System.ArgumentNullException(nameof(board));

    //  for (int row = 0; row < 3; row++) // Any row winner?
    //  {
    //    var boardR1 = board[row, 1];
    //    if (board[row, 0] == boardR1 && boardR1 == board[row, 2])
    //    {
    //      if (boardR1 == player) return +10;
    //      else if (boardR1 == opponent) return -10;
    //    }
    //  }

    //  for (int col = 0; col < 3; col++) // Any column winner?
    //  {
    //    var board1C = board[1, col];
    //    if (board[0, col] == board1C && board1C == board[2, col])
    //    {
    //      if (board1C == player) return +10;
    //      else if (board1C == opponent) return -10;
    //    }
    //  }

    //  var board11 = board[1, 1];

    //  if ((board[0, 0] == board11 && board11 == board[2, 2]) || (board[0, 2] == board11 && board11 == board[2, 0])) // Any diagonal winner?
    //  {
    //    if (board11 == player) return +10;
    //    else if (board11 == opponent) return -10;
    //  }

    //  return 0; // No winner.
    //}

    /// <summary>This will return the best possible move for the player.</summary>
    //public static System.Collections.Generic.IEnumerable<Move> GetMoves(char[,] board, char player, char opponent, char empty)
    //{
    //  if (board is null) throw new System.ArgumentNullException(nameof(board));

    //  for (int i = 0; i < 3; i++)
    //    for (int j = 0; j < 3; j++)
    //      if (board[i, j] == empty)
    //      {
    //        board[i, j] = player;
    //        int score = Minimax(board, 0, false, player, opponent, empty);
    //        board[i, j] = empty;

    //        yield return new Move(i, j, score);
    //      }
    //}

    //public static bool HasEmptySquares(char[,] board, char empty)
    //{
    //  if (board is null) throw new System.ArgumentNullException(nameof(board));

    //  foreach (var square in board)
    //    if (square == empty)
    //      return true;

    //  return false;
    //}

    //public static bool IsEmpty(char[,] board, char empty)
    //{
    //  if (board is null) throw new System.ArgumentNullException(nameof(board));

    //  foreach (var square in board)
    //    if (square != empty)
    //      return false;

    //  return true;
    //}

    /// <summary>This is the minimax function. It considers all the possible ways the game can go and returns the value of the board.</summary>
    //public static int Minimax(char[,] board, int depth, bool isMax, char player, char opponent, char empty)
    //{
    //  int score = EvaluateBoard(board, player, opponent);

    //  if (score == 10) return score - depth; // Maximizer won.
    //  if (score == -10) return score + depth; // Minimizer won.

    //  if (!HasEmptySquares(board, empty)) return 0; // No more moves and no winner, it's a tie.

    //  var best = isMax ? int.MinValue : int.MaxValue;

    //  for (int i = 0; i < 3; i++)
    //  {
    //    for (int j = 0; j < 3; j++)
    //    {
    //      if (board[i, j] == empty)
    //      {
    //        if (isMax)
    //        {
    //          board[i, j] = player;
    //          best = System.Math.Max(best, Minimax(board, depth + 1, !isMax, player, opponent, empty));
    //          board[i, j] = empty;
    //        }
    //        else
    //        {
    //          board[i, j] = opponent;
    //          best = System.Math.Min(best, Minimax(board, depth + 1, !isMax, player, opponent, empty));
    //          board[i, j] = empty;
    //        }
    //      }
    //    }
    //  }

    //  return best;
    //}

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

    public State[] GetRowMajorOrder()
      => m_state.ToArray();
    public T[,] GetRowMajorOrder2D<T>(System.Func<State, T> selector)
      => GetRowMajorOrder().Select(selector).ToArray().ToTwoDimensionalArray(3, 3);

    // IEnumerable<State>
    public System.Collections.Generic.IEnumerator<State> GetEnumerator()
      => GetRowMajorOrder().ToList().GetEnumerator();
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
      => GetEnumerator();
  }
}
#pragma warning restore CA1814 // Prefer jagged arrays over multidimensional
