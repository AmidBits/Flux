namespace Flux.DataStructure.Graph
{
  /// <summary>Represents a graph using an adjacency list. Unlimited edge combinations and types.</summary>
  /// https://docs.microsoft.com/en-us/previous-versions/ms379574(v=vs.80)
  /// https://www.tutorialspoint.com/representation-of-graphs
  /// https://www.geeksforgeeks.org/graph-data-structure-and-algorithms/
  /// <see href="https://en.wikipedia.org/wiki/Graph_(discrete_mathematics)"/>
  public sealed class AdjacencyList<TVertexValue, TEdgeValue>
    : IGraph<TVertexValue, TEdgeValue>
    where TVertexValue : notnull
    where TEdgeValue : notnull
  {
    /// <summary>This is the adjacency list (of a directed multigraph) which consists of edges and the values of the edges.</summary>
    private readonly System.Collections.Generic.Dictionary<int, System.Collections.Generic.Dictionary<int, System.Collections.Generic.List<TEdgeValue>>> m_list;

    /// <summary>The values of vertices.</summary>
    private readonly System.Collections.Generic.Dictionary<int, TVertexValue> m_vertexValues;

    public AdjacencyList()
    {
      m_list = new();
      m_vertexValues = new();
    }

    /// <summary>Returns the count of vertices within the adjacency matrix.</summary>
    public int Count => m_list.Count;

    public IGraph<TVertexValue, TEdgeValue> CloneEmpty() => new AdjacencyList<TVertexValue, TEdgeValue>();

    /// <summary>Returns the degree of the vertex x. Loops are not counted.</summary>
    public int GetDegree(int x)
    {
      var degree = 0;

      foreach (var sk in m_list.Keys)
        foreach (var tk in m_list[sk].Keys)
          if (sk == x || tk == x)
            degree += sk == tk ? 2 : 1;

      return degree;
    }

    /// <summary>Lists all vertices y such that there is an edge from the vertex x to the vertex y. Loops are not considered neighbors.</summary>
    public System.Collections.Generic.IEnumerable<int> GetNeighbors(int x) => m_list[x].Keys;

    /// <summary>Tests whether there is an edge (<paramref name="x"/>, <paramref name="y"/>).</summary>
    public bool IsAdjacent(int x, int y) => m_list.ContainsKey(x) && m_list[x].ContainsKey(y); // && m_list[x][y].Count > 0;

    /// <summary>Adds the vertex <paramref name="x"/>, if it is not there.</summary>
    public bool AddVertex(int x)
    {
      if (!m_list.ContainsKey(x))
      {
        m_list.Add(x, new());

        return true;
      }

      return false;
    }

    /// <summary>Adds the vertex <paramref name="x"/> with the <paramref name="value"/>, if it is not there.</summary>
    public bool AddVertex(int x, TVertexValue value)
    {
      if (AddVertex(x))
      {
        SetVertexValue(x, value);

        return true;
      }

      return false;
    }

    /// <summary>Tests whether there is a vertex <paramref name="x"/>.</summary>
    public bool VertexExists(int x) => m_list.ContainsKey(x);

    /// <summary>Removes vertex <paramref name="x"/>, if it is there.</summary>
    public bool RemoveVertex(int x)
    {
      if (VertexExists(x))
      {
        m_vertexValues.Remove(x); // Remove the value of the vertex.

        foreach (var kvp in m_list)
          if (kvp.Value is var targets && targets.ContainsKey(x))
            targets.Remove(x); // Remove any vertex as a target.

        m_list.Remove(x); // Remove vertex as a source.

        return true;
      }

      return false;
    }

    /// <summary>Returns the <paramref name="value"/> associated with vertex <paramref name="x"/>. A vertex can exists without a value.</summary>
    public bool TryGetVertexValue(int x, out TVertexValue value) => m_vertexValues.TryGetValue(x, out value!);

    /// <summary>Removes the value for vertex <paramref name="x"/> and whether the removal was successful.</summary>
    public bool RemoveVertexValue(int x) => m_vertexValues.Remove(x);

    /// <summary>Sets the <paramref name="value"/> associated with the vertex <paramref name="x"/>.</summary>
    public void SetVertexValue(int x, TVertexValue value) => m_vertexValues[x] = value;

    /// <summary>Adds the edge (<paramref name="x"/>, <paramref name="y"/>) with the <paramref name="value"/>, if it is not there.</summary>
    public bool AddEdge(int x, int y, TEdgeValue value)
    {
      if (VertexExists(x) && VertexExists(y)) // Ensure both vertices exist.
      {
        if (!m_list[x].ContainsKey(y)) // If no matching endpoint from x to y exists, we add it.
          m_list[x].Add(y, new());

        m_list[x][y].Add(value);

        return true;
      }

      return false;
    }

    /// <summary>Tests whether there is at least one edge (<paramref name="x"/>, <paramref name="y"/>).</summary>
    public bool EdgesExists(int x, int y) => m_list.ContainsKey(x) && m_list[x].ContainsKey(y);

    /// <summary>Tests whether there is an edge (<paramref name="x"/>, <paramref name="y"/>) with the <paramref name="value"/>.</summary>
    public bool EdgeExists(int x, int y, TEdgeValue value) => EdgesExists(x, y) && m_list[x][y].Contains(value);

    /// <summary>Removes the edge (<paramref name="x"/>, <paramref name="y"/>) with <paramref name="value"/>, if it is there. If the <paramref name="value"/> is the only edge, the direction is removed.</summary>
    public bool RemoveEdge(int x, int y, TEdgeValue value)
    {
      if (EdgeExists(x, y, value))
      {
        var rv = m_list[x][y].Remove(value);

        if (m_list[x][y].Count == 0)
          m_list[x].Remove(y);

        return rv;
      }

      return false;
    }

    /// <summary>Removes all edges (<paramref name="x"/>, <paramref name="y"/>), if any.</summary>
    public bool RemoveEdges(int x, int y)
    {
      if (EdgesExists(x, y))
        return m_list[x].Remove(y);

      return false;
    }

    ///// <summary>Creates a new sequence with the shortest path tree, i.e. the shortest paths from the specified origin vertex to all reachable vertices.</summary>
    ///// <param name="distanceSelector">Selects the length of the edge (i.e. the distance between the endpoints).</param>
    ///// <see href="https://www.geeksforgeeks.org/dijkstras-shortest-path-algorithm-greedy-algo-7/"/>
    //public System.Collections.Generic.IEnumerable<(int destination, double distance)> GetDijkstraShortestPathTree(int origin, System.Func<object, double> distanceSelector)
    //{
    //  var vertices = System.Linq.Enumerable.ToList(GetVertices());

    //  var distances = System.Linq.Enumerable.ToDictionary(vertices, v => v, v => v.Equals(origin) ? 0 : double.PositiveInfinity);

    //  var edges = System.Linq.Enumerable.ToList(GetEdges()); // Cache edges, because we need it while there are available distances.

    //  while (System.Linq.Enumerable.Any(distances)) // As long as there are nodes available.
    //  {
    //    var shortest = System.Linq.Enumerable.First(System.Linq.Enumerable.OrderBy(distances, v => v.Value)); // Get the node with the shortest distance.

    //    if (shortest.Value < double.PositiveInfinity) // If the distance to the node is less than infinity, it was reachable so it should be returned.
    //      yield return (shortest.Key, shortest.Value);

    //    distances.Remove(shortest.Key); // This node is now final, so remove it.

    //    foreach (var (x, y, v) in System.Linq.Enumerable.Where(edges, e => e.x.Equals(shortest.Key))) // Updates all nodes reachable from the vertex.
    //    {
    //      if (distances.TryGetValue(y, out var distanceToEdgeTarget))
    //      {
    //        var distanceViaShortest = shortest.Value + distanceSelector(v); // Distance via the current node.

    //        if (distanceViaShortest < distanceToEdgeTarget) // If the distance via the current node is shorter than the currently recorded distance, replace it.
    //          distances[y] = distanceViaShortest;
    //      }
    //    }
    //  }
    //}

    public System.Collections.Generic.IEnumerable<(int x, int y, TEdgeValue value)> GetEdges()
    {
      foreach (var x in m_list.Keys)
        foreach (var y in m_list[x].Keys)
          foreach (var v in m_list[x][y])
            yield return (x, y, v);
    }

    public System.Collections.Generic.IEnumerable<(int x, TVertexValue value)> GetVertices() => m_list.Keys.Select(x => (x, TryGetVertexValue(x, out var v) ? v : default!));
    public System.Collections.Generic.IEnumerable<(int x, TVertexValue value, int degree)> GetVerticesWithDegree() => m_list.Keys.Select(x => (x, TryGetVertexValue(x, out var v) ? v : default!, GetDegree(x)));

    public override string ToString() => $"{GetType().Name} {{ Vertices = {Count}, Edges = {System.Linq.Enumerable.Count(GetEdges())} }}";
  }
}

/*
  var am = new Flux.DataStructures.Graphs.AdjacencyList<int, int>();

  am.AddVertex(0, 9);
  am.AddVertex(1, 8);
  am.AddVertex(2, 7);
  am.AddVertex(3, 6);

  //am.AddEdge(0, 1, 1);
  //am.AddEdge(0, 2, 1);
  //am.AddEdge(1, 0, 1);
  //am.AddEdge(1, 2, 1);
  //am.AddEdge(2, 0, 1);
  //am.AddEdge(2, 1, 1);
  //am.AddEdge(2, 3, 1);
  //am.AddEdge(3, 2, 1);

  am.AddEdge(0, 1, 2);
  am.AddEdge(0, 2, 1);
  am.AddEdge(1, 2, 4);
  am.AddEdge(2, 3, 1);

  System.Console.WriteLine(am.ToConsoleString());

  var amt = (Flux.DataStructures.Graphs.AdjacencyList<int, int>)am.TransposeToCopy();

  System.Console.WriteLine(amt.ToConsoleString());
 */
