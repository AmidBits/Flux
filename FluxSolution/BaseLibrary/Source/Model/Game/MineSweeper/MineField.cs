using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

// https://github.com/gmamaladze/minesweeper
namespace Flux.Model.Game.MineSweeper
{
	public static class EmMineSweeper
	{
		public static System.Collections.Generic.IEnumerable<Geometry.Point2> AllPoints(this Geometry.Size2 size)
		{
			for (var i = 0; i < size.Width; i++)
				for (var j = 0; j < size.Height; j++)
					yield return new Geometry.Point2(i, j);
		}
	}

	public class Covers
		: System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<Geometry.Point2, bool>>
	{
		private readonly System.Collections.Immutable.IImmutableDictionary<Geometry.Point2, bool> m_covers;

		public int Count
			=> m_covers.Count;

		private Covers(System.Collections.Immutable.IImmutableDictionary<Geometry.Point2, bool> covers)
			=> m_covers = covers;

		public bool HasFlag(Geometry.Point2 point)
			=> m_covers[point];
		public bool IsCovered(Geometry.Point2 point)
			=> m_covers.ContainsKey(point);

		public Covers SwitchFlag(Geometry.Point2 point)
			=> m_covers.TryGetValue(point, out var hasFlag) ? new Covers(m_covers.SetItem(point, !hasFlag)) : this;

		public Covers Uncover(Geometry.Point2 point)
			=> IsCovered(point) ? new Covers(m_covers.Remove(point)) : this;

		public Covers UncoverRange(System.Collections.Generic.IEnumerable<Geometry.Point2> points)
			=> new Covers(m_covers.RemoveRange(points));

		// Statics
		public static Covers Create(Geometry.Size2 size)
			=> new Covers(size.AllPoints().ToImmutableDictionary(p => p, p => false));
		// IEnumerable
		public System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<Geometry.Point2, bool>> GetEnumerator()
			=> m_covers.GetEnumerator();
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
			=> GetEnumerator();
	}

	public class Field
	{
		public Geometry.Size2 Size { get; }

		public Field(Geometry.Size2 size)
			=> Size = size;

		public System.Collections.Generic.IEnumerable<Geometry.Point2> GetNeighbours(Geometry.Point2 point)
		{
			if (point + new Geometry.Point2(1, 0) is var right && IsInRange(right))
				yield return right;
			if (point + new Geometry.Point2(1, -1) is var upRight && IsInRange(upRight))
				yield return upRight;
			if (point + new Geometry.Point2(0, -1) is var up && IsInRange(up))
				yield return up;
			if (point + new Geometry.Point2(-1, -1) is var upLeft && IsInRange(upLeft))
				yield return upLeft;
			if (point + new Geometry.Point2(-1, 0) is var left && IsInRange(left))
				yield return left;
			if (point + new Geometry.Point2(-1, 1) is var downLeft && IsInRange(downLeft))
				yield return downLeft;
			if (point + new Geometry.Point2(0, 1) is var down && IsInRange(down))
				yield return down;
			if (point + new Geometry.Point2(1, 1) is var downRight && IsInRange(downRight))
				yield return downRight;
		}

		public bool IsInRange(Geometry.Point2 point)
			=> point.X >= 0 && point.Y >= 0 && point.X < Size.Width && point.Y < Size.Height;

		public static bool IsEmptyAt(Mines mines, Warnings warnings, Geometry.Point2 point)
			=> !(mines?.HasMineAt(point) ?? false) && !(warnings?.HasWarningAt(point) ?? false);
	}

	public class Mines
		: System.Collections.Generic.IReadOnlySet<Geometry.Point2>
	{
		private readonly System.Collections.Generic.IReadOnlySet<Geometry.Point2> m_mines;

		private Mines(System.Collections.Generic.IReadOnlySet<Geometry.Point2> mines)
			=> m_mines = mines;

		public bool HasMineAt(Geometry.Point2 point)
			=> m_mines.Contains(point);

		public static Mines Create(Field field, int count)
			=> new Mines(System.Linq.Enumerable.Repeat(Random.NumberGenerator.Crypto, count * 2).Select(r => new Geometry.Point2(r.Next(field.Size.Width), r.Next(field.Size.Height))).Distinct().Take(count).ToHashSet());

		#region IReadOnlySet implementation
		public int Count
			=> m_mines.Count;
		public bool Contains(Geometry.Point2 item)
			=> m_mines.Contains(item);
		public bool IsProperSubsetOf(IEnumerable<Geometry.Point2> other)
			=> m_mines.IsProperSubsetOf(other);
		public bool IsProperSupersetOf(IEnumerable<Geometry.Point2> other)
			=> m_mines.IsProperSupersetOf(other);
		public bool IsSubsetOf(IEnumerable<Geometry.Point2> other)
			=> m_mines.IsSubsetOf(other);
		public bool IsSupersetOf(IEnumerable<Geometry.Point2> other)
			=> m_mines.IsSupersetOf(other);
		public bool Overlaps(IEnumerable<Geometry.Point2> other)
			=> m_mines.Overlaps(other);
		public bool SetEquals(IEnumerable<Geometry.Point2> other)
			=> m_mines.SetEquals(other);
		public System.Collections.Generic.IEnumerator<Geometry.Point2> GetEnumerator()
			=> m_mines.GetEnumerator();
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
			=> GetEnumerator();
		#endregion IReadOnlySet implementation
	}

#pragma warning disable CA1710 // Identifiers should have correct suffix
	public class Warnings
		: System.Collections.Generic.IReadOnlyDictionary<Geometry.Point2, int>
	{
		private readonly System.Collections.Generic.IDictionary<Geometry.Point2, int> m_warnings;

		public bool HasWarningAt(Geometry.Point2 point)
			=> m_warnings.ContainsKey(point);
		public int WarningsAt(Geometry.Point2 point)
			=> m_warnings.TryGetValue(point, out var result) ? result : 0;

		private Warnings(System.Collections.Generic.IDictionary<Geometry.Point2, int> warnings)
			=> m_warnings = warnings;

		#region IReadOnlyDictionary implementation
		public int Count
			=> m_warnings.Count;
		public System.Collections.Generic.IEnumerable<Geometry.Point2> Keys
			=> m_warnings.Keys;
		public System.Collections.Generic.IEnumerable<int> Values
			=> m_warnings.Values;
#pragma warning disable CA1043 // Use Integral Or String Argument For Indexers
		public int this[Geometry.Point2 key]
			=> m_warnings[key];
#pragma warning restore CA1043 // Use Integral Or String Argument For Indexers
		public bool ContainsKey(Geometry.Point2 key)
			=> m_warnings.ContainsKey(key);
		public System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<Geometry.Point2, int>> GetEnumerator()
			=> m_warnings.GetEnumerator();
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
			=> GetEnumerator();
		public bool TryGetValue(Geometry.Point2 key, [System.Diagnostics.CodeAnalysis.MaybeNullWhen(false)] out int value)
			=> m_warnings.TryGetValue(key, out value);
		#endregion IReadOnlyDictionary implementation

		public static Warnings Create(Field field, Mines mines)
			=> new Warnings(mines.SelectMany(mine => field.GetNeighbours(mine)).GroupBy(neighbor => neighbor).ToDictionary(g => g.Key, g => g.Count()));
	}
#pragma warning restore CA1710 // Identifiers should have correct suffix

	public class MineField
	{
		public Field Field { get; }
		public Mines Mines { get; }
		public Warnings Warnings { get; }

		public bool IsEmptyAt(Geometry.Point2 point)
			=> !Mines.HasMineAt(point) && !Warnings.HasWarningAt(point);

		public MineField(Geometry.Size2 fieldSize, int mineCount)
		{
			Field = new Field(fieldSize);
			Mines = Mines.Create(Field, mineCount);
			Warnings = Warnings.Create(Field, Mines);
		}
	}

	public static class Game
	{
		public static void RenderInConsole(Flux.Model.Game.MineSweeper.GameState gameState, Flux.Model.Game.MineSweeper.MineField mineField)
		{
			var charField = new char[mineField.Field.Size.Height, mineField.Field.Size.Width];

			var covers = gameState.Covers();

			for (var i = 0; i < mineField.Field.Size.Height; i++)
			{
				for (var j = 0; j < mineField.Field.Size.Width; j++)
				{
					var p = new Flux.Geometry.Point2(i, j);

					var isUncovered = !covers.IsCovered(p);

					var character = 'X';

					if (character == 'X' && isUncovered && mineField.Mines.HasMineAt(p))
						character = 'M';
					if (character == 'X' && isUncovered && mineField.Warnings.WarningsAt(p) is var adjacent && adjacent > 0)
						character = (char)(adjacent + '0');
					if (character == 'X' && isUncovered)
						character = ' ';

					charField[i, j] = character;
				}
			}

			System.Console.Clear();
			System.Console.WriteLine(charField.ToConsoleString2d((e, i) => e.ToString()));
			if (charField.AsParallel().AsOrdered().OfType<char>().Any(c => c == 'M'))
				System.Console.WriteLine(@"Mine!!!");
		}

		public static void PlayInConsole()
		{
			var mineField = new Flux.Model.Game.MineSweeper.MineField(new Flux.Geometry.Size2(10, 10), 10);
			var gameState = Flux.Model.Game.MineSweeper.Game.Start(mineField);
			var gameResult = gameState.Evaluate(mineField.Mines);

			RenderInConsole(gameState, mineField);

			while (System.Console.ReadLine() is var line && !string.IsNullOrEmpty(line))
			{
				if (Flux.Geometry.Point2.TryParse(line, out var point))
				{
					gameState.CursorPosition = point;

					gameState = Flux.Model.Game.MineSweeper.Game.Uncover(gameState, mineField);

					gameResult = gameState.Evaluate(mineField.Mines);
				}

				RenderInConsole(gameState, mineField);
			}
		}

		public static GameState Start(MineField mineField)
			=> GameState.Create(Covers.Create((mineField ?? throw new System.ArgumentNullException(nameof(mineField))).Field.Size));

		public static GameState Undo(GameState current)
			=> (current ?? throw new System.ArgumentNullException(nameof(current))).Undo();

		public static GameState Quit(GameState current, MineField mineField)
			=> (current ?? throw new System.ArgumentNullException(nameof(current))).Do(current.Covers().UncoverRange((mineField ?? throw new System.ArgumentNullException(nameof(mineField))).Mines));

		public static GameState Uncover(GameState current, MineField mineField)
			=> (current ?? throw new System.ArgumentNullException(nameof(current))).Do(current.Covers().UncoverDeep((mineField ?? throw new System.ArgumentNullException(nameof(mineField))), current.CursorPosition));

		public static GameState SwitchFlag(GameState current)
			=> (current ?? throw new System.ArgumentNullException(nameof(current))).Do(current.Covers().SwitchFlag(current.CursorPosition));

		private static Covers UncoverDeep(this Covers covers, MineField mineField, Geometry.Point2 point)
		{
			if (!covers.IsCovered(point))
				return covers;
			if (!mineField.IsEmptyAt(point))
				return covers.Uncover(point);

			return mineField.Field.GetNeighbours(point).Aggregate(covers.Uncover(point), (current, neighbor) => current.UncoverDeep(mineField, neighbor));
		}
	}

	public class GameState
	{
		private readonly IImmutableStack<Covers> m_moves;

		public Geometry.Point2 CursorPosition { get; set; }

		private GameState(IImmutableStack<Covers> moves, Geometry.Point2 cursorPosition)
		{
			m_moves = moves;
			CursorPosition = cursorPosition;
		}

		public static GameState Create(Covers covers)
			=> new GameState(ImmutableStack.Create(covers), new Geometry.Point2(0, 0));

		public GameState Do(Covers covers)
			=> new GameState(m_moves.Push(covers), CursorPosition);

		public GameState Do(Geometry.Point2 cursorPosition)
			=> new GameState(m_moves, cursorPosition);

		public GameState Undo()
			=> m_moves.Pop() is var moves && moves.IsEmpty ? this : new GameState(moves, CursorPosition);

		public GameResult Evaluate(Mines mines)
		{
			if (mines is null) throw new System.ArgumentNullException(nameof(mines));

			var covers = Covers();

			return new GameResult(mines.Any(mine => !covers.IsCovered(mine)), covers.Count - mines.Count);
		}

		public Covers Covers()
			=> m_moves.Peek();
	}

	public class GameResult
	{
		public GameResult(bool hasFailed, int coveredCount)
		{
			HasFailed = hasFailed;
			CoveredCount = coveredCount;
		}

		public bool HasFailed { get; }
		public int CoveredCount { get; }

		public bool IsGameOver()
		{
			return HasFailed || CoveredCount == 0;
		}
	}
}
