namespace Flux
{
	public static partial class CollectionsImmutableEm
	{
		public static int GetNodeCount<TValue>(this Collections.Immutable.IBinaryTree<TValue> source)
			=> source?.IsEmpty ?? throw new System.ArgumentNullException(nameof(source)) ? 0 : 1 + GetNodeCount(source.Left) + GetNodeCount(source.Right);

		public static int Minimax<TValue>(this Collections.Immutable.IBinaryTree<TValue> source, int depth, bool isMax, int maxHeight, System.Func<TValue, int> valueSelector)
		{
			if (source is null) throw new System.ArgumentNullException(nameof(source));
			else if (source.IsEmpty) throw new System.ArgumentException(source.GetType().Name);

			if (valueSelector is null) throw new System.ArgumentNullException(nameof(valueSelector));

			if (depth == maxHeight)
				return valueSelector(source.Value); // Terminating condition.

			int? left = null, right = null;

			if (isMax)
			{
				if (!source.Left.IsEmpty)
					left = Minimax(source.Left, depth + 1, false, maxHeight, valueSelector);
				if (!source.Right.IsEmpty)
					right = Minimax(source.Right, depth + 1, false, maxHeight, valueSelector);
			}
			else
			{
				if (!source.Left.IsEmpty)
					left = Minimax(source.Left, depth + 1, true, maxHeight, valueSelector);
				if (!source.Right.IsEmpty)
					right = Minimax(source.Right, depth + 1, true, maxHeight, valueSelector);
			}

			if (left.HasValue && right.HasValue)
				return isMax ? System.Math.Max(left.Value, right.Value) : System.Math.Min(left.Value, right.Value);
			else if (left.HasValue)
				return left.Value;
			else if (right.HasValue)
				return right.Value;
			else // Neither has values.
				return valueSelector(source.Value);
		}
	}
}
