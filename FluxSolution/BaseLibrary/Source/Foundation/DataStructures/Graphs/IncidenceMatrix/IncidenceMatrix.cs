namespace Flux.DataStructures.Graphs
{
  /// <summary>Represents a graph using an adjacency matrix. Unlimited edge combinations and types.</summary>
  /// https://docs.microsoft.com/en-us/previous-versions/ms379574(v=vs.80)
  /// https://www.tutorialspoint.com/representation-of-graphs
  /// https://www.geeksforgeeks.org/graph-data-structure-and-algorithms/
  /// <see cref="https://en.wikipedia.org/wiki/Graph_(discrete_mathematics)"/>
  public class IncidenceMatrix<TKey, TVertexValue, TEdgeValue>
    : IGraphVertexValue<TKey, TVertexValue>, IGraphDirectedSimple<TKey, TEdgeValue>
    where TKey : System.IEquatable<TKey>
    where TVertexValue : System.IEquatable<TVertexValue>
    where TEdgeValue : System.IEquatable<TEdgeValue>
  {
    #region Graph storage
    private TEdgeValue[,] m_edgeValues = new TEdgeValue[0, 0];

    private int[,] m_matrix = new int[0, 0];

    private readonly System.Collections.Generic.List<TVertexValue> m_vertexValues = new System.Collections.Generic.List<TVertexValue>();
    private readonly System.Collections.Generic.List<TKey> m_vertices = new System.Collections.Generic.List<TKey>(); // Vertices are kept in a list for indexing in the matrix.
    #endregion Graph storage

    public TEdgeValue this[int source, int target]
      => m_matrix[source, target] > 0 ? m_edgeValues[source, target] : default!;

    // IGraphVertex<>
    public int GetDegree(TKey vertex)
    {
      var count = 0;

      if (ContainsVertex(vertex, out var vertexIndex))
      {
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
    public System.Collections.Generic.IEnumerable<TKey> GetNeighbors(TKey vertex)
    {
      var vertexIndex = m_vertices.IndexOf(vertex);

      var verticesLength = m_vertices.Count;

      for (var index = 0; index < verticesLength; index++)
        if (m_matrix[vertexIndex, index] > 0)
          yield return m_vertices[index];
    }
    public bool IsAdjacent(TKey source, TKey target)
      => ContainsVertex(source, out var sourceIndex) && ContainsVertex(target, out var targetIndex) && m_matrix[sourceIndex, targetIndex] == 1;

    public bool AddVertex(TKey vertex)
      => AddVertex(vertex, default!);
    public bool ContainsVertex(TKey vertex)
      => m_vertices.Contains(vertex);
    public System.Collections.Generic.ICollection<TKey> GetVertices()
      => m_vertices;
    public bool RemoveVertex(TKey vertex)
    {
      if (ContainsVertex(vertex, out var index))
      {
        m_edgeValues = m_edgeValues.Remove(0, index); // Remove dimension 0 to accomodate the new vertex as a source.
        m_edgeValues = m_edgeValues.Remove(1, index); // Remove dimension 1 to accomodate the new vertex as a target.

        m_matrix = m_matrix.Remove(0, index); // Add dimension 0 to accomodate vertex values.
        m_matrix = m_matrix.Remove(1, index); // Add dimension 1 to accomodate vertex values.

        m_vertexValues.RemoveAt(index);
        m_vertices.RemoveAt(index);

        return true;
      }

      return false;
    }

    /// <summary>Determins whether a vertex exists in the graph and if so, returns its index.</summary>
    private bool ContainsVertex(TKey vertex, out int index)
      => (index = m_vertices.IndexOf(vertex)) > -1;

    // IGraphVertexValue<>
    public bool AddVertex(TKey vertex, TVertexValue value)
    {
      if (!ContainsVertex(vertex))
      {
        var index = m_vertices.Count; // This will be the next index.

        m_vertices.Add(vertex);
        m_vertexValues.Add(value!);

        m_matrix = m_matrix.Insert(0, index, 1, 0); // Add dimension 0 to accomodate the new vertex as a source.
        m_matrix = m_matrix.Insert(1, index, 1, 0); // Add dimension 1 to accomodate the new vertex as a target.

        m_edgeValues = m_edgeValues.Insert(0, index, 1, default!); // Add dimension 0 to accomodate vertex values.
        m_edgeValues = m_edgeValues.Insert(1, index, 1, default!); // Add dimension 1 to accomodate vertex values.

        return true;
      }

      return false;
    }
    public bool IsVertexValueEqualTo(TKey vertex, TVertexValue value)
      => TryGetVertexValue(vertex, out var vertexValue) && vertexValue.Equals(value);
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
    public bool TrySetVertexValue(TKey vertex, TVertexValue value)
    {
      if (ContainsVertex(vertex, out var index))
      {
        m_vertexValues[index] = value;
        return true;
      }

      return false;
    }

    // IGraphDirected<>
    public System.Collections.Generic.IEnumerable<(TKey keySource, TKey keyTarget, TEdgeValue value)> GetDirectedEdges()
    {
      var verticesLength = m_vertices.Count;

      for (var sourceIndex = 0; sourceIndex < verticesLength; sourceIndex++)
        for (var targetIndex = 0; targetIndex < verticesLength; targetIndex++)
          if (m_matrix[sourceIndex, targetIndex] is var matrix && matrix > 0)
            yield return (m_vertices[sourceIndex], m_vertices[targetIndex], m_edgeValues[sourceIndex, targetIndex]);
    }
    // IGraphDirectedSimple<>
    public bool AddSimpleDirectedEdge(TKey source, TKey target, TEdgeValue value)
    {
      if (ContainsVertex(source, out var sourceIndex) && ContainsVertex(target, out var targetIndex))
      {
        m_matrix[sourceIndex, targetIndex] = source.Equals(target) ? 2 : 1;

        TrySetSimpleDirectedEdgeValue(source, target, value);

        return true;
      }

      return false;
    }
    public bool AddSimpleDirectedEdge(TKey source, TKey target)
      => AddSimpleDirectedEdge(source, target, default!);
    public bool ContainsSimpleDirectedEdge(TKey source, TKey target)
      => ContainsVertex(source, out var sourceIndex) && ContainsVertex(target, out var targetIndex) && m_matrix[sourceIndex, targetIndex] > 0;
    public bool ContainsSimpleDirectedEdge(TKey source, TKey target, TEdgeValue value)
      => ContainsVertex(source, out var sourceIndex) && ContainsVertex(target, out var targetIndex) && m_matrix[sourceIndex, targetIndex] > 0 && m_edgeValues[sourceIndex, targetIndex].Equals(value);
    public bool RemoveSimpleDirectedEdge(TKey source, TKey target)
    {
      if (ContainsVertex(source, out var sourceIndex) && ContainsVertex(target, out var targetIndex) && m_matrix[sourceIndex, targetIndex] > 0)
      {
        m_edgeValues[sourceIndex, targetIndex] = default!;

        m_matrix[sourceIndex, targetIndex] = 0;

        return true;
      }

      return false;
    }
    public bool TryGetSimpleDirectedEdgeValue(TKey source, TKey target, out TEdgeValue value)
    {
      if (ContainsVertex(source, out var sourceIndex) && ContainsVertex(target, out var targetIndex))
      {
        value = m_edgeValues[sourceIndex, targetIndex];
        return true;
      }

      value = default!;
      return false;
    }
    public bool TrySetSimpleDirectedEdgeValue(TKey source, TKey target, TEdgeValue value)
    {
      if (ContainsVertex(source, out var sourceIndex) && ContainsVertex(target, out var targetIndex))
      {
        m_edgeValues[sourceIndex, targetIndex] = value;
        return true;
      }

      return false;
    }

    public string ToConsoleString()
    {
      var l0 = m_matrix.GetLength(0);
      var l1 = m_matrix.GetLength(1);

      var grid = new object[l0 + 1, l1 + 1];

      for (var i0 = l0 - 1; i0 >= 0; i0--)
      {
        grid[i0 + 1, 0] = m_vertices[i0];
        grid[0, i0 + 1] = m_vertices[i0];

        for (var i1 = l1 - 1; i1 >= 0; i1--)
          grid[i0 + 1, i1 + 1] = m_matrix[i0, i1];
      }

      return grid.ToConsoleBlock(uniformWidth: true, centerContent: true);
    }

    #region Object overrides.
    public override string ToString()
    {
      var sb = new System.Text.StringBuilder();
      var edgeCount = 0;
      foreach (var edge in GetDirectedEdges())
        sb.AppendLine($"#{++edgeCount}: {edge}");
      sb.Insert(0, $"<{GetType().Name}: ({m_vertices.Count} vertices, {edgeCount} edges)>{System.Environment.NewLine}");
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
