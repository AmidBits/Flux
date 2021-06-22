namespace Flux.DataStructures.Immutable
{
	public sealed class BinaryTree<TValue>
		: IBinaryTree<TValue>
	{
		public static readonly IBinaryTree<TValue> Empty = new EmptyBinaryTree();

		private readonly IBinaryTree<TValue> m_left;
		private readonly IBinaryTree<TValue> m_right;
		private readonly TValue m_value;

		public BinaryTree(TValue value, IBinaryTree<TValue> left, IBinaryTree<TValue> right)
		{
			m_left = left ?? Empty;
			m_right = right ?? Empty;
			m_value = value;
		}

		// IBinaryTree<TValue>
		public bool IsEmpty
			=> false;
		public IBinaryTree<TValue> Left
			=> m_left;
		public IBinaryTree<TValue> Right
			=> m_right;
		public TValue Value
			=> m_value;

		public override string ToString()
			=> $"<{GetType().Name}({(Left.IsEmpty ? '-' : 'L')}|{(Right.IsEmpty ? '-' : 'R')}): {m_value}>";

		private sealed class EmptyBinaryTree
			: IBinaryTree<TValue>
		{
			// IBinaryTree<TValue>
			public bool IsEmpty
				=> true;
			public IBinaryTree<TValue> Left
				=> throw new System.Exception(nameof(EmptyBinaryTree));
			public IBinaryTree<TValue> Right
				=> throw new System.Exception(nameof(EmptyBinaryTree));
			public TValue Value
				=> throw new System.Exception(nameof(EmptyBinaryTree));

			public override string ToString()
				=> $"<{GetType().Name}>";
		}
	}
}
