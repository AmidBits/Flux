//using System.Linq;

//// https://www.geeksforgeeks.org/minimax-algorithm-in-game-theory-set-1-introduction/
//// https://www.geeksforgeeks.org/minimax-algorithm-in-game-theory-set-2-evaluation-function/
//// https://www.geeksforgeeks.org/minimax-algorithm-in-game-theory-set-3-tic-tac-toe-ai-finding-optimal-move/
//// https://en.wikipedia.org/wiki/Negamax
//namespace Flux.Model.Negamax
//{
//	public class ZeroSumTwoPlayer<T>
//	{
//		public System.Collections.Generic.IList<T> Board { get; set; }

//		public int Evaluate(bool isMax)
//		{
//			var player1 = isMax ? State.Player1 : State.Player2;
//			var player2 = isMax ? State.Player2 : State.Player1;

//			for (int row = 0; row < 3; row++) // Any row winner?
//			{
//				var boardR1 = this[row, 1];
//				if (this[row, 0] == boardR1 && boardR1 == this[row, 2])
//				{
//					if (boardR1 == player1) return +10;
//					else if (boardR1 == player2) return -10;
//				}
//			}

//			for (int col = 0; col < 3; col++) // Any column winner?
//			{
//				var board1C = this[1, col];
//				if (this[0, col] == board1C && board1C == this[2, col])
//				{
//					if (board1C == player1) return +10;
//					else if (board1C == player2) return -10;
//				}
//			}

//			var board11 = this[1, 1];

//			if ((this[0, 0] == board11 && board11 == this[2, 2]) || (this[0, 2] == board11 && board11 == this[2, 0])) // Any diagonal winner?
//			{
//				if (board11 == player1) return +10;
//				else if (board11 == player2) return -10;
//			}

//			return 0; // No winner yet.
//		}

//		public System.Collections.Generic.IList<Move> GetOptionsForPlayer1()
//		{
//			var moves = new System.Collections.Generic.List<Move>();

//			for (var r = 0; r < 3; r++)
//			{
//				for (var c = 0; c < 3; c++)
//				{
//					if (this[r, c] == State.Empty)
//					{
//						this[r, c] = State.Player1;
//						moves.Add(new Move(r, c, Minimax(0, false)));
//						this[r, c] = State.Empty;
//					}
//				}
//			}

//			return moves;
//		}
//		public System.Collections.Generic.IList<Move> GetOptionsForPlayer2()
//		{
//			var moves = new System.Collections.Generic.List<Move>();

//			for (var r = 0; r < 3; r++)
//			{
//				for (var c = 0; c < 3; c++)
//				{
//					if (this[r, c] == State.Empty)
//					{
//						this[r, c] = State.Player2;
//						moves.Add(new Move(r, c, Minimax(0, true)));
//						this[r, c] = State.Empty;
//					}
//				}
//			}

//			return moves;
//		}

//		public bool HasEmptySquares()
//		{
//			foreach (var state in this)
//				if (state == State.Empty)
//					return true;

//			return false;
//		}

//		public int Minimax(int depth, bool isMax)
//		{
//			int score = Evaluate(isMax);

//			if (score == 10)
//				return score - depth; // Maximizer won.
//			if (score == -10)
//				return score + depth; // Minimizer won.

//			if (!HasEmptySquares())
//				return 0; // No more moves and no winner, it's a tie.

//			var best = isMax ? int.MinValue : int.MaxValue;

//			for (int i = 0; i < 3; i++)
//				for (int j = 0; j < 3; j++)
//					if (this[i, j] == State.Empty)
//					{
//						if (isMax)
//						{
//							this[i, j] = State.Player1;
//							best = System.Math.Max(best, Minimax(depth + 1, !isMax));
//						}
//						else
//						{
//							this[i, j] = State.Player2;
//							best = System.Math.Min(best, Minimax(depth + 1, isMax));
//						}

//						this[i, j] = State.Empty;
//					}

//			return best;
//		}

//		/// <summary></summary>
//		/// <param name="node">The index of the root node.</param>
//		/// <param name="depth">This is the depth allowed (the depth decrease by one each recursion).</param>
//		/// <param name="color">The player, either 1 or -1.</param>
//		/// <param name="availableMovesSelector">The possible moves (e.g. empty indices).</param>
//		/// <param name="heuristicValueSelector">The heuristic value of node (i.e. some form of evaluation function).</param>
//		/// <returns></returns>
//		public static int Negamax(int node, int depth, int color, System.Func<int, int[]> availableMovesSelector, System.Func<int, int> heuristicValueSelector)
//		{
//			var moves = availableMovesSelector?.Invoke(node) ?? throw new System.ArgumentNullException(nameof(availableMovesSelector));

//			if (depth == 0 || moves.Length == 0)
//				return color * heuristicValueSelector?.Invoke(node) ?? throw new System.ArgumentNullException(nameof(availableMovesSelector));

//			var value = int.MinValue;

//			foreach (var next in moves)
//				value = System.Math.Max(value, -Negamax(next, depth - 1, -color, availableMovesSelector, heuristicValueSelector));

//			return value;
//		}

//		/// <summary>Returns the optimal value a maximizer can obtain.</summary>
//		/// <param name="depth">The current depth in game tree.</param>
//		/// <param name="nodeIndex">The index of current node in scores[].</param>
//		/// <param name="isMax">True if current move is of maximizer, else false if minimizer.</param>
//		/// <param name="scores">The leaves of Game tree.</param>
//		/// <param name="maxHeight">The maximum height of Game tree.</param>
//		/// <returns></returns>
//		public static int Minimax(int depth, int nodeIndex, bool isMax, int[] scores, int maxHeight)
//		{
//			if (scores is null) throw new System.ArgumentNullException(nameof(scores));

//			if (depth == maxHeight) return scores[nodeIndex]; // Terminating condition.

//			if (isMax) return System.Math.Max(Minimax(depth + 1, nodeIndex * 2, false, scores, maxHeight), Minimax(depth + 1, nodeIndex * 2 + 1, false, scores, maxHeight));
//			else return System.Math.Min(Minimax(depth + 1, nodeIndex * 2, true, scores, maxHeight), Minimax(depth + 1, nodeIndex * 2 + 1, true, scores, maxHeight));
//		}
//	}
//}
