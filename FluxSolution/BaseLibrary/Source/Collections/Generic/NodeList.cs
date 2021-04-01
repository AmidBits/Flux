namespace Flux.Collections.Generic
{
	/// <summary>Represents a collection of Node instances.</summary>
	/// <typeparam name="TValue">The type of data held in the Node instances referenced by this class.</typeparam>
	public class NodeList<TValue>
		//: System.Collections.ObjectModel.Collection<INode<TValue>>
		: System.Collections.Generic.List<Node<TValue>>
	{
		public System.Collections.Generic.IList<Node<TValue>> Items
			=> this;

		public NodeList()
			: base()
		{ }

		public NodeList(int initialSize)
		{
			for (int i = 0; i < initialSize; i++)
				this.Add(default(Node<TValue>)); // Add the specified number of items.
		}

		/// <summary>Searches the NodeList for a Node containing a particular value.</summary>
		public Node<TValue> FindByValue(TValue value)
		{
			foreach (Node<TValue> node in this)
				if (node.Value.Equals(value))
					return node;

			return null;
		}
	}
}
