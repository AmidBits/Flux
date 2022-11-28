namespace Flux.DataStructures.Immutable
{
	public interface IBinaryTree<TValue>
	{
		/// <summary>Determines whether the binary tree is empty.</summary>
		bool IsEmpty { get; }

		/// <summary>The left child of the binary tree.</summary>
		IBinaryTree<TValue> Left { get; }
		/// <summary>The right child of the binary tree.</summary>
		IBinaryTree<TValue> Right { get; }

		/// <summary>The value of the binary tree.</summary>
		TValue Value { get; }
	}
}
