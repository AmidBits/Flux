namespace Flux.Collections.Generic
{
	public interface INode<TValue>
	{
		public bool IsEmpty { get; }
		public NodeList<TValue> Neighbors { get; set; }
		public TValue Value { get; set; }
	}

	/// <summary>The Node class represents the base concept of a Node for a tree or graph.  It contains a data item of type T, and a list of neighbors.</summary>
	/// <remarks>None of the classes in this namespace use the Node class directly; they all derive from this class, adding necessary functionality specific to each data structure.</remarks>
	public class Node<TValue>
		: INode<TValue>
	{
		public bool IsEmpty
			=> false;

		private NodeList<TValue> m_neighbors;
		public NodeList<TValue> Neighbors
		{ get => m_neighbors; set => m_neighbors = value; }

		private TValue m_value;
		public TValue Value { get { return m_value; } set { m_value = value; } }

		public Node()
		{ }
		public Node(TValue value)
			: this(value, new NodeList<TValue>())
		{ }
		public Node(TValue value, NodeList<TValue> neighbors)
		{
			m_value = value;
			m_neighbors = neighbors;
		}

		private class EmptyNode
			: INode<TValue>
		{
			public EmptyNode()
			{ }

			public bool IsEmpty
				=> true;
			public NodeList<TValue> Neighbors
			{ get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
			public TValue Value
			{ get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
		}
	}
}
