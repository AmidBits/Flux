namespace Flux.DataStructures
{
	public static partial class Traversal
	{
		public enum BinaryTreeSearchDepthOrder
		{
			InOrder,
			PostOrder,
			PreOrder,
			ReverseInOrder,
		}

		/// <summary>Performs a depth-first-search (DFS) on a binary tree type hierarchy, using a recursive algorithm.</summary>
		/// <see cref="https://en.wikipedia.org/wiki/Binary_search_algorithm"/>
		public static System.Collections.Generic.IEnumerable<T> BinaryTreeSearchDfsR<T>(T node, System.Func<T, T> selectorChildLeft, System.Func<T, T> selectorChildRight, BinaryTreeSearchDepthOrder order, System.Func<T, bool> predicate)
		{
			if (node is null) throw new System.ArgumentNullException(nameof(node));
			if (selectorChildLeft is null) throw new System.ArgumentNullException(nameof(selectorChildLeft));
			if (selectorChildRight is null) throw new System.ArgumentNullException(nameof(selectorChildRight));
			if (predicate is null) throw new System.ArgumentNullException(nameof(predicate));

			if (!predicate(node))
				yield break;

			if (order == BinaryTreeSearchDepthOrder.PreOrder)
				yield return node;

			if (order == BinaryTreeSearchDepthOrder.ReverseInOrder)
				foreach (var subNode in BinaryTreeSearchDfsR(selectorChildRight(node), selectorChildLeft, selectorChildRight, order, predicate))
					yield return subNode;
			else
				foreach (var subNode in BinaryTreeSearchDfsR(selectorChildLeft(node), selectorChildLeft, selectorChildRight, order, predicate))
					yield return subNode;

			if (order == BinaryTreeSearchDepthOrder.InOrder || order == BinaryTreeSearchDepthOrder.ReverseInOrder)
				yield return node;

			if (order == BinaryTreeSearchDepthOrder.ReverseInOrder)
				foreach (var subNode in BinaryTreeSearchDfsR(selectorChildLeft(node), selectorChildLeft, selectorChildRight, order, predicate))
					yield return subNode;
			else
				foreach (var subNode in BinaryTreeSearchDfsR(selectorChildRight(node), selectorChildLeft, selectorChildRight, order, predicate))
					yield return subNode;

			if (order == BinaryTreeSearchDepthOrder.PostOrder)
				yield return node;
		}

		/// <summary>Performs a breadth-first-search (BFS) on a binary tree type hierarchy, using an iterative algorithm.</summary>
		/// <see cref="https://en.wikipedia.org/wiki/Breadth-first_search"/>
		public static System.Collections.Generic.IEnumerable<(int depth, T node)> BinaryTreeSearchBfs<T>(T node, System.Func<T, T> selectorChildLeft, System.Func<T, T> selectorChildRight, int maxDepth, System.Func<(int depth, T node), bool> predicate)
		{
			if (node is null) throw new System.ArgumentNullException(nameof(node));
			if (selectorChildLeft is null) throw new System.ArgumentNullException(nameof(selectorChildLeft));
			if (selectorChildRight is null) throw new System.ArgumentNullException(nameof(selectorChildRight));
			if (maxDepth <= 0) throw new System.ArgumentOutOfRangeException(nameof(maxDepth));
			if (predicate is null) throw new System.ArgumentNullException(nameof(predicate));

			var items = new System.Collections.Generic.List<(int depth, T node)>();

			if ((0, node) is var element && predicate(element))
				items.Add(element);

			for (var depth = 0; items.Count > 0 && depth < maxDepth; depth++)
			{
				var nextItems = new System.Collections.Generic.List<(int depth, T value)>();

				foreach (var item in items)
				{
					yield return item;

					if ((depth + 1, selectorChildLeft(item.node)) is var itemL && predicate(itemL))
						nextItems.Add(itemL);
					if ((depth + 1, selectorChildRight(item.node)) is var itemR && predicate(itemR))
						nextItems.Add(itemR);
				}

				items = nextItems;
			}
		}

		/// <summary>Perform a breadth-first-search (BFS) on a general parent-child type tree hierarchy.</summary>
		/// <see cref="https://en.wikipedia.org/wiki/Breadth-first_search"/>
		public static System.Collections.Generic.IEnumerable<(int depth, T node)> BreadthFirstSearch<T>(T node, int maxDepth, System.Func<(int depth, T node), bool> predicate, System.Func<T, System.Collections.Generic.IEnumerable<T>> selectorChildNodes)
		{
			if (node is null) throw new System.ArgumentNullException(nameof(node));
			if (maxDepth <= 0) throw new System.ArgumentOutOfRangeException(nameof(maxDepth));
			if (predicate is null) throw new System.ArgumentNullException(nameof(predicate));
			if (selectorChildNodes is null) throw new System.ArgumentNullException(nameof(selectorChildNodes));

			var items = new System.Collections.Generic.List<T> { node };

			for (var depth = 0; items.Count != 0 && depth < maxDepth; depth++)
			{
				var nextItems = new System.Collections.Generic.List<T>();

				foreach (var item in items)
				{
					var element = (depth, item);

					if (predicate(element))
					{
						yield return element;

						nextItems.AddRange(selectorChildNodes(item));
					}
				}

				items = nextItems;
			}
		}

		/// <summary>Perform a depth-first-search (DFS) on a general parent-child type tree hierarchy.</summary>
		/// <see cref="https://en.wikipedia.org/wiki/Depth-first_search"/>
		public static System.Collections.Generic.IEnumerable<(int depth, T node)> DepthFirstSearch<T>(T node, int maxDepth, System.Func<(int depth, T node), bool> predicate, System.Func<T, System.Collections.Generic.IEnumerable<T>> selectorChildNodes)
		{
			if (node is null) throw new System.ArgumentNullException(nameof(node));
			if (maxDepth <= 0) throw new System.ArgumentOutOfRangeException(nameof(maxDepth));
			if (predicate is null) throw new System.ArgumentNullException(nameof(predicate));
			if (selectorChildNodes is null) throw new System.ArgumentNullException(nameof(selectorChildNodes));

			var stack = new System.Collections.Generic.Stack<System.Collections.Generic.Queue<T>>();

			stack.Push(new System.Collections.Generic.Queue<T>(System.Linq.Enumerable.Empty<T>().Append((T)node)));

			while (stack.Count > 0)
			{
				if (stack.Peek().Count > 0)
				{
					var element = (depth: stack.Count - 1, item: stack.Peek().Dequeue());

					if (predicate(element))
					{
						yield return element;

						if (stack.Count < maxDepth)
							stack.Push(new System.Collections.Generic.Queue<T>(selectorChildNodes(element.item)));
					}
				}
				else
					stack.Pop();
			}
		}

		/// <summary>Perform a iterative deepening depth first search (IDDFS) on a tree hierarchy.</summary>
		/// <see cref="https://en.wikipedia.org/wiki/Iterative_deepening_depth-first_search"/>
		public static T IterativeDeepeningDepthFirstSearch<T>(T node, int maxDepth, System.Func<T, bool> predicateGoal, System.Func<T, System.Collections.Generic.IEnumerable<T>> selectorChildNodes)
		{
			if (predicateGoal is null) throw new System.ArgumentNullException(nameof(predicateGoal));
			if (selectorChildNodes is null) throw new System.ArgumentNullException(nameof(selectorChildNodes));

			for (int depth = 0; depth <= maxDepth; depth++)
			{
				var (itemFound, itemsRemaining) = DepthLimitedSearch(node, depth, predicateGoal, selectorChildNodes);

				if (itemFound != null)
					return itemFound;
				else if (!itemsRemaining)
					break;
			}

			return default!;
		}

		/// <summary>Perform a depth limited search (DLS) on a tree hierarchy.</summary>
		/// <seealso cref="https://en.wikipedia.org/wiki/Iterative_deepening_depth-first_search#Algorithm_for_Directed_Graphs"/>
		public static (T itemFound, bool itemsRemaining) DepthLimitedSearch<T>(T node, int depth, System.Func<T, bool> predicateGoal, System.Func<T, System.Collections.Generic.IEnumerable<T>> selectorChildNodes)
		{
			if (predicateGoal is null) throw new System.ArgumentNullException(nameof(predicateGoal));
			if (selectorChildNodes is null) throw new System.ArgumentNullException(nameof(selectorChildNodes));

			if (depth == 0)
			{
				return predicateGoal(node) ? (node, true) : (default!, true);
			}
			else if (depth > 0)
			{
				var anyItemsRemaining = false;

				foreach (T childNode in selectorChildNodes(node))
				{
					var (itemFound, moreItems) = DepthLimitedSearch(childNode, depth - 1, predicateGoal, selectorChildNodes);

					if (itemFound != null)
						return (itemFound, true);
					if (moreItems)
						anyItemsRemaining = true;
				}

				return (default!, anyItemsRemaining);
			}
			else throw new System.ArgumentOutOfRangeException(nameof(depth));
		}
	}
}
