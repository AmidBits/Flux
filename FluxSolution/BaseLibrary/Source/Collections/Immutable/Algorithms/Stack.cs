namespace Flux.Collections.Immutable
{
	public sealed class Stack<TValue>
		: IStack<TValue>
	{
		public static readonly IStack<TValue> Empty = new EmptyStack();

		private readonly TValue m_head;
		private readonly IStack<TValue> m_tail;
		private Stack(TValue head, IStack<TValue> tail)
		{
			m_head = head;
			m_tail = tail;
		}

		// IStack<T>
		public int Count { get; init; }
		public bool IsEmpty => false;
		public TValue Peek() => m_head;
		public IStack<TValue> Pop() => m_tail;
		public IStack<TValue> Push(TValue value) => new Stack<TValue>(value, this) { Count = Count + 1 };
		// IEnumerable<T>
		public System.Collections.Generic.IEnumerator<TValue> GetEnumerator()
		{
			for (IStack<TValue> stack = this; !stack.IsEmpty; stack = stack.Pop())
				yield return stack.Peek();
		}
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

		private sealed class EmptyStack
			: IStack<TValue>
		{
			// IStack
			public int Count => 0;
			public bool IsEmpty => true;
			public TValue Peek() => throw new System.Exception(nameof(EmptyStack));
			public IStack<TValue> Push(TValue value) => new Stack<TValue>(value, this);
			public IStack<TValue> Pop() => throw new System.Exception(nameof(EmptyStack));
			// IEnumerator
			public System.Collections.Generic.IEnumerator<TValue> GetEnumerator() { yield break; }
			System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
		}
	}
}
