namespace Flux.DataStructures.Graphs
{
  /// <summary>Represents a graph using an adjacency matrix. Unlimited edge combinations and types.</summary>
  /// https://docs.microsoft.com/en-us/previous-versions/ms379574(v=vs.80)
  /// https://www.tutorialspoint.com/representation-of-graphs
  /// https://www.geeksforgeeks.org/graph-data-structure-and-algorithms/
  /// <see cref="https://en.wikipedia.org/wiki/Graph_(discrete_mathematics)"/>
  public class AdjacencyList
  {
    private readonly System.Collections.Generic.Dictionary<int, System.Collections.Generic.Dictionary<int, System.Collections.Generic.List<object>>> m_list = new System.Collections.Generic.Dictionary<int, System.Collections.Generic.Dictionary<int, System.Collections.Generic.List<object>>>();

    private readonly System.Collections.Generic.Dictionary<int, object> m_vertexValues = new System.Collections.Generic.Dictionary<int, object>();

    /// <summary>Returns the count of vertices within the adjacency matrix.</summary>
    public int Count
      => m_list.Count;

    /// <summary>Returns the degree of the vertex x. Loops are not counted.</summary>
    public int GetDegree(int x)
    {
      var degree = 0;

      foreach (var sk in m_list.Keys)
        foreach (var tk in m_list[sk].Keys)
          if (sk == x || tk == x)
            degree += sk.Equals(tk) ? 2 : 1;

      return degree;
    }
    /// <summary>Lists all vertices y such that there is an edge from the vertex x to the vertex y. Loops are not considered neighbors.</summary>
    public System.Collections.Generic.IEnumerable<int> GetNeighbors(int x)
      => m_list[x].Keys;
    /// <summary>Tests whether there is an edge from the vertex x to the vertex y.</summary>
    public bool IsAdjacent(int x, int y)
      => m_list.ContainsKey(x) && m_list[x].ContainsKey(y) && m_list[x][y].Count > 0;

    /// <summary>Adds the vertex x, if it is not there.</summary>
    public bool AddVertex(int x)
    {
      if (!m_list.ContainsKey(x))
      {
        m_list.Add(x, new System.Collections.Generic.Dictionary<int, System.Collections.Generic.List<object>>());

        return true;
      }

      return false;
    }
    /// <summary>Adds the vertex x with the value vv, if it is not there.</summary>
    public bool AddVertex(int x, object v)
    {
      if (AddVertex(x))
      {
        SetVertexValue(x, v);

        return true;
      }

      return false;
    }
    /// <summary>Tests whether there is a vertex x.</summary>
    public bool VertexExists(int x)
      => m_list.ContainsKey(x);
    /// <summary>Removes the vertex x, if it is there.</summary>
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

    /// <summary>Returns the value associated with the vertex x. A vertex can exists without a value.</summary>
    public bool TryGetVertexValue(int x, out object v)
      => m_vertexValues.TryGetValue(x, out v!);
    /// <summary>Removes the value for the edge and whether the removal was successful.</summary>
    public bool RemoveVertexValue(int x)
      => m_vertexValues.Remove(x);
    /// <summary>Sets the value associated with the vertex x to v.</summary>
    public void SetVertexValue(int x, object v)
      => m_vertexValues[x] = v;

    /// <summary>Adds the edge from the vertex x to the vertex y with the value ev, if it is not there.</summary>
    public bool AddEdge(int x, int y, object v)
    {
      if (m_list.ContainsKey(x) && m_list.ContainsKey(y))
      {
        if (!m_list[x].ContainsKey(y)) // No matching endpoint exists, so we add it.
          m_list[x].Add(y, new System.Collections.Generic.List<object>());

        m_list[x][y].Add(v);

        return true;
      }

      return false;
    }
    /// <summary>Tests whether there is an existing edge (x, y).</summary>
    public bool EdgesExists(int x, int y)
      => m_list.ContainsKey(x) && m_list[x].ContainsKey(y);
    /// <summary>Tests whether there is an existing edge (x, y) with the value.</summary>
    public bool EdgeExists(int x, int y, object v)
      => EdgesExists(x, y) && m_list[x][y].Contains(v);
    /// <summary>Removes the edge from the vertex x to the vertex y with the value, if it is there. If the value is the only edge, the direction is removed.</summary>
    public bool RemoveEdge(int x, int y, object v)
    {
      if (EdgeExists(x, y, v))
      {
        var rv = m_list[x][y].Remove(v);

        if (m_list[x][y].Count == 0)
          m_list[x].Remove(y);

        return rv;
      }

      return false;
    }
    /// <summary>Removes the edge from the vertex x to the vertex y, if it is there.</summary>
    public bool RemoveEdges(int x, int y)
    {
      if (EdgesExists(x, y))
        return m_list[x].Remove(y);

      return false;
    }

    /// <summary>Creates a new sequence with the shortest path tree, i.e. the shortest paths from the specified origin vertex to all reachable vertices.</summary>
    /// <param name="distanceSelector">Selects the length of the edge (i.e. the distance between the endpoints).</param>
    public System.Collections.Generic.IEnumerable<(int destination, double distance)> GetDijkstraShortestPathTree(int origin, System.Func<object, double> distanceSelector)
    {
      var vertices = System.Linq.Enumerable.ToList(GetVertices());

      var distances = System.Linq.Enumerable.ToDictionary(vertices, v => v, v => v.Equals(origin) ? 0 : double.PositiveInfinity);

      var edges = System.Linq.Enumerable.ToList(GetEdges()); // Cache edges, because we need it while there are available distances.

      while (System.Linq.Enumerable.Any(distances)) // As long as there are nodes available.
      {
        var shortest = System.Linq.Enumerable.First(System.Linq.Enumerable.OrderBy(distances, v => v.Value)); // Get the node with the shortest distance.

        if (shortest.Value < double.PositiveInfinity) // If the distance to the node is less than infinity, it was reachable so it should be returned.
          yield return (shortest.Key, shortest.Value);

        distances.Remove(shortest.Key); // This node is now final, so remove it.

        foreach (var (x, y, v) in System.Linq.Enumerable.Where(edges, e => e.x.Equals(shortest.Key))) // Updates all nodes reachable from the vertex.
        {
          if (distances.TryGetValue(y, out var distanceToEdgeTarget))
          {
            var distanceViaShortest = shortest.Value + distanceSelector(v); // Distance via the current node.

            if (distanceViaShortest < distanceToEdgeTarget) // If the distance via the current node is shorter than the currently recorded distance, replace it.
              distances[y] = distanceViaShortest;
          }
        }
      }
    }

    public System.Collections.Generic.IEnumerable<(int x, int y, object v)> GetEdges()
    {
      foreach (var x in m_list.Keys)
        foreach (var y in m_list[x].Keys)
          foreach (var v in m_list[x][y])
            yield return (x, y, v);
    }

    public System.Collections.Generic.IEnumerable<int> GetVertices()
      => m_list.Keys;
    public System.Collections.Generic.IEnumerable<(int x, object v)> GetVerticesWithValue()
      => System.Linq.Enumerable.Select(GetVertices(), x => (x, TryGetVertexValue(x, out var v) ? v : default!));
    public System.Collections.Generic.IEnumerable<(int x, object v, int d)> GetVerticesWithValueAndDegree()
      => System.Linq.Enumerable.Select(GetVertices(), x => (x, TryGetVertexValue(x, out var v) ? v : default!, GetDegree(x)));

    public string ToConsoleString()
    {
      var sb = new System.Text.StringBuilder();

      sb.AppendLine(ToString());

      sb.AppendLine();

      sb.AppendLine(@"Vertices (x, value, degree):");
      sb.AppendJoin(System.Environment.NewLine, GetVerticesWithValueAndDegree()).AppendLine();

      sb.AppendLine();

      sb.AppendLine(@"Edges (x, y, value):");
      sb.AppendJoin(System.Environment.NewLine, GetEdges()).AppendLine();

      return sb.ToString();
    }
  }
}
