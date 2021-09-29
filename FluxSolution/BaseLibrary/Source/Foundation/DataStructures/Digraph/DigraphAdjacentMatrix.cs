using System.Linq;

namespace Flux.DataStructures.Graph
{
  /// <summary>Represents a graph using an adjacency matrix. Unlimited edge combinations and types.</summary>
  /// https://docs.microsoft.com/en-us/previous-versions/ms379574(v=vs.80)
  /// https://www.tutorialspoint.com/representation-of-graphs
  /// https://www.geeksforgeeks.org/graph-data-structure-and-algorithms/
  /// <see cref="https://en.wikipedia.org/wiki/Graph_(discrete_mathematics)"/>
  public class DigraphAdjacentMatrix<TKey, TVertexValue, TEdgeValue>
    : IDigraph<TKey, TVertexValue, TEdgeValue>, ISimpleGraph<TKey, TVertexValue, TEdgeValue>
    where TKey : notnull
  {
    private readonly System.Collections.Generic.List<TVertexValue> m_vertexValues = new System.Collections.Generic.List<TVertexValue>();
    private readonly System.Collections.Generic.List<TKey> m_vertices = new System.Collections.Generic.List<TKey>(); // Vertices are kept in a list for indexing in the matrix.

    private int[,] m_matrix = new int[0, 0];

    private TEdgeValue[,] m_edgeValues = new TEdgeValue[0, 0];

    public int GetDegree(TKey vertex)
    {
      int count = 0, vertexIndex = m_vertices.IndexOf(vertex), index;

      for (index = m_matrix.GetLength(0) - 1; index >= 0; index--)
        count += m_matrix[index, vertexIndex];
      for (index = m_matrix.GetLength(1) - 1; index >= 0; index--)
        count += m_matrix[vertexIndex, index];

      return count;
    }
    public System.Collections.Generic.IEnumerable<TKey> GetNeighbors(TKey vertex)
    {
      int vertexIndex = m_vertices.IndexOf(vertex), index, length = m_matrix.GetLength(1);

      for (index = 0; index < length; index++)
        if (m_matrix[vertexIndex, index] > 0)
          yield return m_vertices[index];
    }
    public bool IsAdjacent(TKey source, TKey target)
    {
      var sourceIndex = m_vertices.IndexOf(source);
      var targetIndex = m_vertices.IndexOf(target);

      return sourceIndex > -1 && targetIndex > -1 && m_matrix[sourceIndex, targetIndex] == 1;
    }
    public bool IsLoop(TKey vertex)
    {
      var vertexIndex = m_vertices.IndexOf(vertex);

      return vertexIndex > -1 && m_matrix[vertexIndex, vertexIndex] == 2;
    }

    public void AddVertex(TKey vertex)
    {
      if (!ContainsVertex(vertex))
      {
        var index = m_vertices.Count;

        m_vertices.Add(vertex);
        m_vertexValues.Add(default!);

        m_matrix = m_matrix.Insert(0, index, true, 0); // Expand dimension 0 to accomodate the new vertex as a source.
        m_matrix = m_matrix.Insert(1, index, true, 0); // Expand dimension 1 to accomodate the new vertex as a target.

        m_edgeValues = m_edgeValues.Insert(0, index, true, default!); // Expand dimension 0 to accomodate vertex values.
        m_edgeValues = m_edgeValues.Insert(1, index, true, default!); // Expand dimension 1 to accomodate vertex values.
      }
      else throw new System.ArgumentException($"The vertex <{vertex}> already exist.");
    }
    public void AddVertex(TKey vertex, TVertexValue value)
    {
      AddVertex(vertex);
      SetVertexValue(vertex, value);
    }
    public bool ContainsVertex(TKey vertex)
      => m_vertices.Contains(vertex);
    public void RemoveVertex(TKey vertex)
    {
      if (ContainsVertex(vertex))
      {
        var index = m_vertices.IndexOf(vertex);

        m_edgeValues = m_edgeValues.Remove(0, index);
        m_edgeValues = m_edgeValues.Remove(1, index);

        m_matrix = m_matrix.Remove(0, index);
        m_matrix = m_matrix.Remove(1, index);

        m_vertexValues.RemoveAt(index);
        m_vertices.RemoveAt(index);
      }
    }

    public TVertexValue GetVertexValue(TKey vertex)
      => m_vertexValues[m_vertices.IndexOf(vertex)];
    public void SetVertexValue(TKey vertex, TVertexValue value)
      => m_vertexValues[m_vertices.IndexOf(vertex)] = value;

    public System.Collections.Generic.IEnumerable<TKey> GetVertices()
      => m_vertices;

    public void AddEdge(TKey source, TKey target, TEdgeValue value)
    {
      AddEdge(source, target);
      SetEdgeValue(source, target, value);
    }
    public void AddEdge(TKey source, TKey target)
    {
      var sourceIndex = m_vertices.IndexOf(source);
      var targetIndex = m_vertices.IndexOf(target);

      if (sourceIndex > -1 && targetIndex > -1 && m_matrix[sourceIndex, targetIndex] == 0)
      {
        m_matrix[sourceIndex, targetIndex] = source.Equals(target) ? 2 : 1;
      }
      else throw new System.ArgumentException($"The edge <{source}> to <{target}> already exist.");
    }
    public bool ContainsEdge(TKey source, TKey target)
    {
      var sourceIndex = m_vertices.IndexOf(source);
      var targetIndex = m_vertices.IndexOf(target);

      return sourceIndex > -1 && targetIndex > -1 && m_matrix[sourceIndex, targetIndex] > 0;
    }
    public void RemoveEdge(TKey source, TKey target)
    {
      var sourceIndex = m_vertices.IndexOf(source);
      var targetIndex = m_vertices.IndexOf(target);

      if (sourceIndex > -1 && targetIndex > -1 && m_matrix[sourceIndex, targetIndex] != 0)
        m_matrix[sourceIndex, targetIndex] = 0;
    }

    public TEdgeValue GetEdgeValue(TKey source, TKey target)
    {
      var sourceIndex = m_vertices.IndexOf(source);
      var targetIndex = m_vertices.IndexOf(target);

      return m_edgeValues[sourceIndex, targetIndex];
    }
    public void SetEdgeValue(TKey source, TKey target, TEdgeValue value)
    {
      var sourceIndex = m_vertices.IndexOf(source);
      var targetIndex = m_vertices.IndexOf(target);

      m_edgeValues[sourceIndex, targetIndex] = value;
    }

    public System.Collections.Generic.IEnumerable<(TKey source, TKey target, TEdgeValue value)> GetEdges()
    {
      foreach (var source in m_vertices)
      {
        foreach (var target in m_vertices)
        {
          var sourceIndex = m_vertices.IndexOf(source);
          var targetIndex = m_vertices.IndexOf(target);

          if (sourceIndex > -1 && targetIndex > -1 && m_matrix[sourceIndex, targetIndex] > 0)
            yield return (source, target, m_edgeValues[sourceIndex, targetIndex]);
        }
      }
    }

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
      sb.Insert(0, $"<{GetType().Name}: ({GetVertices().Count()} vertices, {index} edges)>{System.Environment.NewLine}");
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
