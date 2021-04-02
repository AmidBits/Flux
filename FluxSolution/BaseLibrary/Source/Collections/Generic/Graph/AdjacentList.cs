using System.Linq;

namespace Flux.Collections.Generic.Graph
{
	public class Vertex<TValue>
	{
		private TValue m_value;

		public Vertex(TValue value)
		{
			m_value = value;
		}

		public TValue Value
			=> m_value;
	}

	public class Edge<TValue, TWeight>
		where TValue : System.IEquatable<TWeight>
		where TWeight : System.IComparable<TWeight>, System.IEquatable<TWeight>
	{
		private Vertex<TValue> m_source, m_target;

		private TWeight m_weight;

		public Edge(Vertex<TValue> source, Vertex<TValue> target, TWeight weight)
		{
			m_source = source;
			m_target = target;

			m_weight = weight;
		}

		public TValue Source
			=> m_source.Value;
		public TValue Target
			=> m_target.Value;

		public TWeight Weight
			=> m_weight;
	}

	/// <summary>
	/// Helper data structure for Prim and Dijstra
	/// </summary>
	public class PathCost<TValue, TWeight>
		where TValue : System.IEquatable<TWeight>
		where TWeight : System.IComparable<TWeight>, System.IEquatable<TWeight>
	{
		public Vertex<TValue> m_vertex;
		public TWeight m_cost;

		public PathCost(Vertex<TValue> vertex, TWeight cost)
		{
			m_vertex = vertex;
			m_cost = cost;
		}

		public int CompareTo(object obj)
			=> obj is PathCost<TValue, TWeight> pathCost ? m_cost.CompareTo(pathCost.m_cost) : default;
	}

	/// <summary>Directed weighted Graph representation as a disjoint set of weighted edges. Useful to model shortest path and minimum cost problems.</summary>
	public class AdjacentList<TValue, TWeight>
		where TValue : System.IEquatable<TWeight>
		where TWeight : System.IComparable<TWeight>, System.IEquatable<TWeight>
	{
		public System.Collections.Generic.Dictionary<Vertex<TValue>, System.Collections.Generic.LinkedList<Edge<TValue, TWeight>>> adjacencyList;

		public AdjacentList()
		{
			adjacencyList = new System.Collections.Generic.Dictionary<Vertex<TValue>, System.Collections.Generic.LinkedList<Edge<TValue, TWeight>>>();
		}

		public int VertexesCount => adjacencyList.Count();
		public int EdgesCount { get; private set; }

		public void AddDirectedEdge(Vertex source, Vertex destination, int weight)
		{
			if (!adjacencyList.ContainsKey(source))
				adjacencyList.Add(source, new LinkedList<Edge>());

			if (!adjacencyList.ContainsKey(destination))
				adjacencyList.Add(destination, new LinkedList<Edge>());

			adjacencyList[source].AddLast(new Edge(source, destination, weight));

			EdgesCount++;
		}

		public System.Collections.Generic.List<Edge> KruskalMinimumSpanningTree()
		{
			var mst = new System.Collections.Generic.List<Edge>();

			var vertexes = adjacencyList.Keys;
			// Edges by increasing order of weight
			var edgesByWeight = adjacencyList.Values.SelectMany(e => e).OrderBy(e => e.weight);

			// Use a Disjoint Set to keep track of the connected components
			var spanningTreeUnion = new UnionFind<Vertex<TValue>>(vertexes);

			// A minimum spanning tree has V–1 edges where V is the number of vertices in the given graph
			// Greedy strategy: pick the smallest weight edge that does not cause a cycle in the MST constructed so far
			int minSpanningTreeCount = 0;
			for (var i = 0; i < edgesByWeight.Count()
					&& minSpanningTreeCount < vertexes.Count - 1; i++)
			{
				var edge = edgesByWeight.ElementAt(i);

				var sourceSubset = spanningTreeUnion.Find(edge.source);
				var destinationSubset = spanningTreeUnion.Find(edge.destination);

				if (sourceSubset == destinationSubset) // will cause a cycle
					continue;

				spanningTreeUnion.Union(sourceSubset, destinationSubset);
				mst.Add(edge);

				minSpanningTreeCount++;
			}

			return mst;
		}

		public System.Collections.Generic.List<Edge<TValue,TWeight>> PrimsMinimumSpanningTree()
		{
			var mst = new System.Collections.Generic.Dictionary<Vertex<TValue>, Edge<TValue,TWeight>>();
			if (!adjacencyList.Any())
			{
				return mst.Values.ToList();
			}

			var vertexToMinCost = new System.Collections.Generic.Dictionary<Vertex, PathCost>();

			// Min Heap storing min cost of the spanning tree
			var minHeapNodes = new IndexedHeap<PathCost>(
					new List<PathCost> {
										(vertexToMinCost[adjacencyList.Keys.First()] = new PathCost(adjacencyList.Keys.First(), 0))
			}, (p1, p2) => p1.cost.CompareTo(p2.cost));

			foreach (var vertex in adjacencyList.Keys.Skip(1))
				minHeapNodes.Push(vertexToMinCost[vertex] = new PathCost(vertex, int.MaxValue));

			// Traverse the node by min cost
			// Greedy strategy: Select the edge that minimize the cost for reaching node from its neighbors
			while (minHeapNodes.Any())
			{
				var minNode = minHeapNodes.Pop();

				// Visit all the neighbors and update the corresponding cost
				foreach (var edge in adjacencyList[minNode.vertex])
				{
					// Check that the current cost of reaching the adjacent node is less than the current one
					if (minHeapNodes.Contains(vertexToMinCost[edge.destination]) && edge.weight < vertexToMinCost[edge.destination].cost)
					{
						vertexToMinCost[edge.destination].cost = edge.weight;
						mst[edge.destination] = edge;

						// Sift up the heap starting from the current index (log n)
						minHeapNodes.SiftUp(fromIndex: minHeapNodes.IndexOf(vertexToMinCost[edge.destination]));
					}
				}
			}

			return mst.Values.ToList();
		}

		public List<Edge> DijkstraShortestPath(Vertex source, Vertex destination = default)
		{
			var sp = new Dictionary<Vertex, Edge>();
			if (!adjacencyList.Any())
			{
				return sp.Values.ToList();
			}

			var vertexToPathCost = new Dictionary<Vertex, PathCost>();

			// Min Heap storing accumulated min cost for reaching target node greedily
			var minHeapNodes = new IndexedHeap<PathCost>(
					new List<PathCost> { (vertexToPathCost[source] = new PathCost(source, 0)) },
					compareFunc: (p1, p2) => p1.cost.CompareTo(p2.cost));

			foreach (var vertex in adjacencyList.Keys)
			{
				if (vertex == source)
					continue;

				minHeapNodes.Push(vertexToPathCost[vertex] = new PathCost(vertex, int.MaxValue));
			}

			// Greedy strategy: Select the path that minimized the accumulated cost up to this node
			// Traverse the node by min cost
			while (minHeapNodes.Any())
			{
				var minNode = minHeapNodes.Pop();

				// Visit all the neighbors and update the corresponding cost
				foreach (var edge in adjacencyList[minNode.vertex])
				{
					// Check that the current cost of reaching the adjacent node is less than the current one
					if (minHeapNodes.Contains(vertexToPathCost[edge.destination]) && minNode.cost + edge.weight < vertexToPathCost[edge.destination].cost)
					{
						vertexToPathCost[edge.destination].cost = minNode.cost + edge.weight;
						sp[edge.destination] = edge;

						// Sift up the heap starting from the current index (log n)
						minHeapNodes.SiftUp(fromIndex: minHeapNodes.IndexOf(vertexToPathCost[edge.destination]));
					}
				}

				if (minNode.vertex.Equals(destination))
					break;
			}

			return sp.Values.ToList();
		}
	}
}
