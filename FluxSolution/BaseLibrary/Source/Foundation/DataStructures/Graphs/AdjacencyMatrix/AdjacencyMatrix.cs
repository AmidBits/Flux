using System.Linq;

namespace Flux.DataStructures.Graphs
{
  /// <summary>Represents a graph using an adjacency matrix. Unlimited edge combinations and types.</summary>
  /// https://docs.microsoft.com/en-us/previous-versions/ms379574(v=vs.80)
  /// https://www.tutorialspoint.com/representation-of-graphs
  /// https://www.geeksforgeeks.org/graph-data-structure-and-algorithms/
  /// <see cref="https://en.wikipedia.org/wiki/Graph_(discrete_mathematics)"/>
  public class AdjacencyMatrix<TKey, TVertexValue, TEdgeValue>
    where TKey : System.IEquatable<TKey>
    where TVertexValue : System.IEquatable<TVertexValue>
    where TEdgeValue : System.IEquatable<TEdgeValue>
  {
    private TEdgeValue[,] m_edgeValues = new TEdgeValue[0, 0];

    private int[,] m_matrix = new int[0, 0];

    private readonly System.Collections.Generic.List<TVertexValue> m_vertexValues = new System.Collections.Generic.List<TVertexValue>();
    private readonly System.Collections.Generic.List<TKey> m_vertices = new System.Collections.Generic.List<TKey>(); // Vertices are kept in a list for indexing in the matrix.

    public System.Collections.Generic.IReadOnlyCollection<TKey> Vertices
      => m_vertices;

    /// <summary>Returns the degree of the specified vertex. Returns -1 if not found.</summary>
    public int GetDegree(TKey vertex)
    {
      var count = -1;

      if (ContainsVertex(vertex, out var vertexIndex))
      {
        count++;

        for (var index = m_vertices.Count - 1; index >= 0; index--)
        {
          if (index == vertexIndex) count += m_matrix[index, vertexIndex];
          else
          {
            count += m_matrix[index, vertexIndex];
            count += m_matrix[vertexIndex, index];
          }
        }
      }

      return count;
    }
    /// <summary>Creates a new sequence of all vertices that are edge destinations from the specified vertex.</summary>
    public System.Collections.Generic.IEnumerable<TKey> GetNeighbors(TKey vertex)
    {
      var vertexIndex = m_vertices.IndexOf(vertex);

      var verticesLength = m_vertices.Count;

      for (var index = 0; index < verticesLength; index++)
        if (m_matrix[vertexIndex, index] > 0)
          yield return m_vertices[index];
    }
    /// <summary>Determines whether an edge exists from source to target. Returns false if the edge is a loop.</summary>
    public bool IsAdjacent(TKey source, TKey target)
      => ContainsVertex(source, out var sourceIndex) && ContainsVertex(target, out var targetIndex) && m_matrix[sourceIndex, targetIndex] == 1;
    /// <summary>Determines whether there is a looped edge at the vertex.</summary>
    public bool IsLoop(TKey vertex)
      => ContainsVertex(vertex, out var vertexIndex) && m_matrix[vertexIndex, vertexIndex] == 2;

    /// <summary>Adds a vertex with a value to the graph.</summary>
    public void AddVertex(TKey vertex, TVertexValue value)
    {
      if (!ContainsVertex(vertex))
      {
        var index = m_vertices.Count; // This will be the next index.

        m_vertices.Add(vertex);
        m_vertexValues.Add(value!);

        m_matrix = m_matrix.Insert(0, index, true, 0); // Add dimension 0 to accomodate the new vertex as a source.
        m_matrix = m_matrix.Insert(1, index, true, 0); // Add dimension 1 to accomodate the new vertex as a target.

        m_edgeValues = m_edgeValues.Insert(0, index, true, default!); // Add dimension 0 to accomodate vertex values.
        m_edgeValues = m_edgeValues.Insert(1, index, true, default!); // Add dimension 1 to accomodate vertex values.
      }
    }
    /// <summary>Adds a vertex with the default value to the graph.</summary>
    public void AddVertex(TKey vertex)
      => AddVertex(vertex, default!);
    /// <summary>Determins whether a vertex exists in the graph.</summary>
    public bool ContainsVertex(TKey vertex)
      => m_vertices.Contains(vertex);
    private bool ContainsVertex(TKey vertex, out int index)
      => (index = m_vertices.IndexOf(vertex)) > -1;
    /// <summary>Removes a vertex from the graph.</summary>
    public void RemoveVertex(TKey vertex)
    {
      if (ContainsVertex(vertex, out var index))
      {
        m_edgeValues = m_edgeValues.Remove(0, index); // Remove dimension 0 to accomodate the new vertex as a source.
        m_edgeValues = m_edgeValues.Remove(1, index); // Remove dimension 1 to accomodate the new vertex as a target.

        m_matrix = m_matrix.Remove(0, index); // Add dimension 0 to accomodate vertex values.
        m_matrix = m_matrix.Remove(1, index); // Add dimension 1 to accomodate vertex values.

        m_vertexValues.RemoveAt(index);
        m_vertices.RemoveAt(index);
      }
    }

    /// <summary>Determines whether the specified vertex equals the value.</summary>
    public bool IsVertexValueEqualTo(TKey vertex, TVertexValue value)
      => ContainsVertex(vertex, out var index) && m_vertexValues[index].Equals(value);
    /// <summary>Tries to get the value for the specified vertex and returns whether it succeeded.</summary>
    public bool TryGetVertexValue(TKey vertex, out TVertexValue value)
    {
      if (ContainsVertex(vertex, out var index))
      {
        value = m_vertexValues[index];
        return true;
      }
      else
      {
        value = default!;
        return false;
      }
    }
    /// <summary>Tries to set the value for the specified vertex and returns whether it succeeded.</summary>
    public bool TrySetVertexValue(TKey vertex, TVertexValue value)
    {
      if (ContainsVertex(vertex, out var index))
      {
        m_vertexValues[index] = value;
        return true;
      }

      return false;
    }

    /// <summary>Adds a directed edge with a value to the graph.</summary>
    public void AddEdge(TKey source, TKey target, TEdgeValue value)
    {
      if (ContainsVertex(source, out var sourceIndex) && ContainsVertex(target, out var targetIndex))
      {
        m_matrix[sourceIndex, targetIndex] = source.Equals(target) ? 2 : 1;

        TrySetEdgeValue(source, target, value);
      }
    }
    /// <summary>Adds a directed edge with the default value to the graph.</summary>
    public void AddEdge(TKey source, TKey target)
      => AddEdge(source, target, default!);
    /// <summary>Adds a looped edge with a value to the graph.</summary>
    public void AddEdgeAsLoop(TKey vertex, TEdgeValue value)
      => AddEdge(vertex, vertex, value);
    /// <summary>Adds a undirected edge with a value to the graph.</summary>
    public void AddEdgeAsUndirected(TKey source, TKey target, TEdgeValue value)
    {
      AddEdge(source, target, value);
      AddEdge(target, source, value);
    }
    /// <summary>Determines whether an edge exists in the graph.</summary>
    public bool ContainsEdge(TKey source, TKey target)
      => ContainsVertex(source, out var sourceIndex) && ContainsVertex(target, out var targetIndex) && m_matrix[sourceIndex, targetIndex] > 0;
    /// <summary>Determines whether an edge with the specified value exists in the graph.</summary>
    public bool ContainsEdge(TKey source, TKey target, TEdgeValue value)
      => ContainsVertex(source, out var sourceIndex) && ContainsVertex(target, out var targetIndex) && m_matrix[sourceIndex, targetIndex] > 0 && m_edgeValues[sourceIndex, targetIndex].Equals(value);
    /// <summary>Removes an edge from the graph.</summary>
    public void RemoveEdge(TKey source, TKey target)
    {
      if (ContainsVertex(source, out var sourceIndex) && ContainsVertex(target, out var targetIndex))
      {
        m_edgeValues[sourceIndex, targetIndex] = default!;

        m_matrix[sourceIndex, targetIndex] = 0;
      }
    }
    ///// <summary>Removes an edge as if undirected from the graph.</summary>
    //public void RemoveEdgeAsUndirected(TKey source, TKey target)
    //{
    //  RemoveEdge(source, target);
    //  RemoveEdge(target, source);
    //}

    /// <summary>Tries to get the value for the specified edge and returns whether it succeeded.</summary>
    public bool TryGetEdgeValue(TKey source, TKey target, out TEdgeValue value)
    {
      if (ContainsVertex(source, out var sourceIndex) && ContainsVertex(target, out var targetIndex))
      {
        value = m_edgeValues[sourceIndex, targetIndex];
        return true;
      }

      value = default!;
      return false;
    }
    /// <summary>Tries to set the value for the specified edge and returns whether it succeeded.</summary>
    public bool TrySetEdgeValue(TKey source, TKey target, TEdgeValue value)
    {
      if (ContainsVertex(source, out var sourceIndex) && ContainsVertex(target, out var targetIndex))
      {
        m_edgeValues[sourceIndex, targetIndex] = value;
        return true;
      }

      return false;
    }

    /// <summary>Creates a new sequence with the shortest path tree, i.e. the shortest paths from the specified origin vertex to all reachable vertices.</summary>
    /// <param name="distanceSelector">Selects the length of the edge (i.e. the distance between the endpoints).</param>
    public System.Collections.Generic.IEnumerable<(TKey destination, double distance)> GetDijkstraShortestPathTree(TKey origin, System.Func<TEdgeValue, double> distanceSelector)
    {
      var vertices = System.Linq.Enumerable.ToList(Vertices);

      var distances = System.Linq.Enumerable.ToDictionary(vertices, v => v, v => v.Equals(origin) ? 0 : double.PositiveInfinity);

      var edges = System.Linq.Enumerable.ToList(GetEdges()); // Cache edges, because we need it while there are available distances.

      while (System.Linq.Enumerable.Any(distances)) // As long as there are nodes available.
      {
        var shortest = System.Linq.Enumerable.First(System.Linq.Enumerable.OrderBy(distances, v => v.Value)); // Get the node with the shortest distance.

        if (shortest.Value < double.PositiveInfinity) // If the distance to the node is less than infinity, it was reachable so it should be returned.
          yield return (shortest.Key, shortest.Value);

        distances.Remove(shortest.Key); // This node is now final, so remove it.

        foreach (var (source, target, matrix, value) in System.Linq.Enumerable.Where(edges, e => e.source.Equals(shortest.Key))) // Updates all nodes reachable from the vertex.
        {
          if (distances.TryGetValue(target, out var distanceToEdgeTarget))
          {
            var distanceViaShortest = shortest.Value + distanceSelector(value); // Distance via the current node.

            if (distanceViaShortest < distanceToEdgeTarget) // If the distance via the current node is shorter than the currently recorded distance, replace it.
              distances[target] = distanceViaShortest;
          }
        }
      }
    }

    /// <summary>Creates a new sequence with all existing edges.</summary>
    public System.Collections.Generic.IEnumerable<(TKey source, TKey target, int matrix, TEdgeValue value)> GetEdges()
    {
      var verticesLength = m_vertices.Count;

      for (var sourceIndex = 0; sourceIndex < verticesLength; sourceIndex++)
        for (var targetIndex = 0; targetIndex < verticesLength; targetIndex++)
          if (m_matrix[sourceIndex, targetIndex] is var matrix && matrix > 0)
            yield return (m_vertices[sourceIndex], m_vertices[targetIndex], matrix, m_edgeValues[sourceIndex, targetIndex]);
    }

    /// <summary>Creates a new sequence with all vertices and their respective value.</summary>
    public System.Collections.Generic.IEnumerable<(TKey key, TVertexValue value)> GetVerticesWithValue()
      => System.Linq.Enumerable.Select(m_vertices, vk => (vk, m_vertexValues[m_vertices.IndexOf(vk)]));
    /// <summary>Creates a new sequence with all vertices and their respective value and degree.</summary>
    public System.Collections.Generic.IEnumerable<(TKey key, TVertexValue value, int degree)> GetVerticesWithValueAndDegree()
      => System.Linq.Enumerable.Select(m_vertices, vk => (vk, m_vertexValues[m_vertices.IndexOf(vk)], GetDegree(vk)));

    public string ToConsoleString<TResult>(System.Func<int, TResult> weightFormatter)
    {
      if (weightFormatter is null) throw new System.ArgumentNullException(nameof(weightFormatter));

      var l0 = m_matrix.GetLength(0);
      var l1 = m_matrix.GetLength(1);

      var grid = new object[l0 + 1, l1 + 1];

      for (var i0 = l0 - 1; i0 >= 0; i0--)
      {
        grid[i0 + 1, 0] = m_vertices[i0];
        grid[0, i0 + 1] = m_vertices[i0];

        for (var i1 = l1 - 1; i1 >= 0; i1--)
          grid[i0 + 1, i1 + 1] = weightFormatter(m_matrix[i0, i1])!;
      }

      return grid.ToConsoleBlock(uniformWidth: true, centerContent: true);
    }

    #region Object overrides.
    public override string ToString()
    {
      var sb = new System.Text.StringBuilder();
      var index = 0;
      foreach (var edge in GetEdges())
        sb.AppendLine($"#{++index}: {edge}");
      sb.Insert(0, $"<{GetType().Name}: ({Vertices.Count} vertices, {index} edges)>{System.Environment.NewLine}");
      return sb.ToString();
    }
    #endregion Object overrides.
  }
}

/*
      // Adjacent Matrix.

      var am = new Flux.Collections.Generic.Graph.AdjacentMatrix<char, int>();

      am.AddVertex('a');
      am.AddVertex('b');
      am.AddVertex('c');
      am.AddVertex('d');

      //am.AddDirectedEdge('a', 'b', 1);
      //am.AddDirectedEdge('a', 'c', 1);
      //am.AddDirectedEdge('b', 'a', 1);
      //am.AddDirectedEdge('b', 'c', 1);
      //am.AddDirectedEdge('c', 'a', 1);
      //am.AddDirectedEdge('c', 'b', 1);
      //am.AddDirectedEdge('c', 'd', 1);
      //am.AddDirectedEdge('d', 'c', 1);

      am.AddDirectedEdge('a', 'b', 2);
      am.AddUndirectedEdge('a', 'c', 1);
      am.AddUndirectedEdge('b', 'c', 1);
      am.AddUndirectedEdge('c', 'd', 1);

      am.RemoveUndirectedEdge('c', 'b', 1);

      System.Console.WriteLine(am.ToConsoleString());

      foreach (var edge in am.GetEdges())
        System.Console.WriteLine(edge);
 */
