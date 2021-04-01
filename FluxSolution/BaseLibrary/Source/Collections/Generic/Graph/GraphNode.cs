namespace Flux.Collections.Generic
{
	/// <summary>
	/// Represents a node in a graph.  A graph node contains some piece of data, along with a set of
	/// neighbors.  There can be an optional cost between a graph node and each of its neighbors.
	/// </summary>
	/// <typeparam name="T">The type of data stored in the graph node.</typeparam>
	public class GraphNode<T>
		: Node<T>
	{
		private System.Collections.Generic.List<int>? m_costs; // the cost associated with each edge

		public GraphNode()
			: base()
		{ }
		public GraphNode(T value)
			: base(value)
		{ }
		public GraphNode(T value, NodeList<T> neighbors)
			: base(value, neighbors)
		{ }

		/// <summary>Returns the set of neighbors for this graph node.</summary>
		new public NodeList<T> Neighbors
			=> base.Neighbors ??= new NodeList<T>();

		/// <summary>Returns the set of costs for the edges eminating from this graph node.</summary>
		/// <remarks>The k<sup>th</sup> cost (Cost[k]) represents the cost from the graph node to the node represented by its k<sup>th</sup> neighbor (Neighbors[k]).</remarks>
		public System.Collections.Generic.List<int> Costs
			=> m_costs ?? new System.Collections.Generic.List<int>();
	}
}
