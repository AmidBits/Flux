//namespace Flux.DataStructures.Graphs
//{
//  /// <summary>Represents a graph using an adjacency matrix. Unlimited edge combinations and types.</summary>
//  /// https://docs.microsoft.com/en-us/previous-versions/ms379574(v=vs.80)
//  /// https://www.tutorialspoint.com/representation-of-graphs
//  /// https://www.geeksforgeeks.org/graph-data-structure-and-algorithms/
//  /// <see cref="https://en.wikipedia.org/wiki/Graph_(discrete_mathematics)"/>
//  public class AdjacencyMatrix<TKey, TVertexValue, TEdgeValue>
//    : IGraphVertexValue<TKey, TVertexValue>, IGraphDirectedSimple<TKey, TEdgeValue>
//    where TKey : System.IEquatable<TKey>
//    where TVertexValue : System.IEquatable<TVertexValue>
//    where TEdgeValue : System.IEquatable<TEdgeValue>
//  {
//    public AdjacencyMatrix m_am = new AdjacencyMatrix();
//    //#region Graph storage
//    //private TEdgeValue[,] m_edgeValues = new TEdgeValue[0, 0];

//    //private int[,] m_matrix = new int[0, 0];

//    //private readonly System.Collections.Generic.List<TVertexValue> m_vertexValues = new System.Collections.Generic.List<TVertexValue>();
//    private readonly System.Collections.Generic.List<TKey> m_vertices = new System.Collections.Generic.List<TKey>(); // Vertices are kept in a list for indexing in the matrix.
//    //#endregion Graph storage

//    //public TEdgeValue this[int source, int target]
//    //  => m_matrix[source, target] > 0 ? m_edgeValues[source, target] : default!;

//    // IGraphCommon<>

//    public int GetDegree(TKey vertex)
//      => ContainsVertex(vertex, out var vertexIndex) ? m_am.GetDegree(vertexIndex) : 0;
//    public System.Collections.Generic.IEnumerable<TKey> GetNeighbors(TKey vertex)
//    {
//      if (m_vertices.IndexOf(vertex) is var vertexIndex)
//        foreach (var v in System.Linq.Enumerable.Select(m_am.GetNeighbors(vertexIndex), i => m_vertices[i]))
//          yield return v;
//    }
//    public bool IsAdjacent(TKey source, TKey target)
//      => ContainsVertex(source, out var sourceIndex) && ContainsVertex(target, out var targetIndex) && m_am.IsAdjacent(sourceIndex, targetIndex);

//    // IGraphVertex<>
//    public bool AddVertex(TKey vertex)
//      => AddVertex(vertex, default!);
//    public bool ContainsVertex(TKey vertex)
//      => m_vertices.Contains(vertex);
//    //public System.Collections.Generic.ICollection<TKey> GetVertices()
//    //  => m_vertices;
//    public bool RemoveVertex(TKey vertex)
//    {
//      if (ContainsVertex(vertex, out var index))
//      {
//        m_am.RemoveVertex(index);

//        m_vertices.Remove(vertex);

//        return true;
//      }

//      return false;
//    }

//    /// <summary>Determins whether a vertex exists in the graph and if so, returns its index.</summary>
//    private bool ContainsVertex(TKey vertex, out int index)
//      => (index = m_vertices.IndexOf(vertex)) > -1;

//    // IGraphVertexValue<>
//    public bool AddVertex(TKey vertex, TVertexValue value)
//    {
//      if (!ContainsVertex(vertex, out var index))
//      {
//        index = m_vertices.Count; // This will be the next index.

//        m_am.AddVertex(index);
//        m_am.SetVertexValue(index, value);

//        m_vertices.Add(vertex);

//        return true;
//      }

//      return false;
//    }
//    public bool IsVertexValueEqualTo(TKey vertex, TVertexValue value)
//      => TryGetVertexValue(vertex, out var vertexValue) && vertexValue.Equals(value);
//    public bool TryGetVertexValue(TKey vertex, out TVertexValue value)
//    {
//      if (ContainsVertex(vertex, out var index) && TryGetVertexValue(vertex, out value))
//      {
//        return true;
//      }
//      else
//      {
//        value = default!;
//        return false;
//      }
//    }
//    public bool TrySetVertexValue(TKey vertex, TVertexValue value)
//    {
//      if (ContainsVertex(vertex, out var index))
//      {
//        m_am.SetVertexValue(index, value);

//        return true;
//      }

//      return false;
//    }

//    // IGraphDirected<>
//    public System.Collections.Generic.IEnumerable<(TKey keySource, TKey keyTarget, TEdgeValue value)> GetDirectedEdges()
//    {
//      foreach (var vt in m_am.GetEdges())
//        yield return (m_vertices[vt.x], m_vertices[vt.y], (TEdgeValue)(vt.v));
//    }
//    // IGraphDirectedSimple<>
//    public bool AddEdge(TKey source, TKey target, TEdgeValue value)
//    {
//      if (ContainsVertex(source, out var sourceIndex) && ContainsVertex(target, out var targetIndex))
//      {
//        m_am.AddEdge(sourceIndex, targetIndex, value);

//        return true;
//      }

//      return false;
//    }
//    public bool AddEdge(TKey source, TKey target)
//      => AddEdge(source, target, default!);
//    public bool ContainsEdge(TKey source, TKey target)
//      => ContainsVertex(source, out var sourceIndex) && ContainsVertex(target, out var targetIndex) && m_am.EdgeExists(sourceIndex, targetIndex);
//    public bool RemoveEdge(TKey source, TKey target)
//      => ContainsVertex(source, out var sourceIndex) && ContainsVertex(target, out var targetIndex) && m_am.RemoveEdge(sourceIndex, targetIndex);
//    public bool TryGetEdgeValue(TKey source, TKey target, out TEdgeValue value)
//    {
//      if (ContainsVertex(source, out var sourceIndex) && ContainsVertex(target, out var targetIndex) && m_am.TryGetEdgeValue(sourceIndex, targetIndex, out var v))
//      {
//        value = (TEdgeValue)v;
//        return true;
//      }

//      value = default!;
//      return false;
//    }
//    public bool TrySetEdgeValue(TKey source, TKey target, TEdgeValue value)
//    {
//      if (ContainsVertex(source, out var sourceIndex) && ContainsVertex(target, out var targetIndex))
//      {
//        m_am.SetEdgeValue(sourceIndex, targetIndex, value);
//        return true;
//      }

//      return false;
//    }

//    public System.Collections.Generic.IEnumerable<(TKey source, TKey target)> GetEdges()
//      => System.Linq.Enumerable.Select(m_am.GetEdges(), vt => (m_vertices[vt.x], m_vertices[vt.y]));
//    public System.Collections.Generic.IEnumerable<(TKey source, TKey target, TEdgeValue value)> GetEdgesWithValue()
//      => System.Linq.Enumerable.Select(m_am.GetEdges(), vt => (m_vertices[vt.x], m_vertices[vt.y], (TEdgeValue)(vt.v)));

//    public System.Collections.Generic.IEnumerable<TKey> GetVertices()
//      => System.Linq.Enumerable.Select(m_am.GetVertices(), v => (TKey)m_vertices[v]);
//    public System.Collections.Generic.IEnumerable<(TKey vertex, TVertexValue value)> GetVerticesWithValue()
//      => System.Linq.Enumerable.Select(m_am.GetVerticesWithValue(), vt => ((TKey)m_vertices[vt.x], (TVertexValue)(vt.v)));
//    public System.Collections.Generic.IEnumerable<(TKey vertex, TVertexValue value, int degree)> GetVerticesWithValueAndDegree()
//      => System.Linq.Enumerable.Select(m_am.GetVerticesWithValueAndDegree(), vt => ((TKey)m_vertices[vt.x], (TVertexValue)(vt.v), vt.d));

//    public string ToConsoleString()
//    {
//      var sb = new System.Text.StringBuilder();

//      sb.AppendLine(ToString());

//      var m = m_am.Matrix;

//      var l0 = m.GetLength(0);
//      var l1 = m.GetLength(1);

//      var grid = new object[l0 + 1, l1 + 1];

//      for (var i0 = l0 - 1; i0 >= 0; i0--)
//      {
//        grid[i0 + 1, 0] = m_vertices[i0];
//        grid[0, i0 + 1] = m_vertices[i0];

//        for (var i1 = l1 - 1; i1 >= 0; i1--)
//          grid[i0 + 1, i1 + 1] = m[i0, i1];
//      }

//      sb.AppendLine(grid.ToConsoleBlock(uniformWidth: true, centerContent: true));

//      sb.AppendLine();

//      sb.AppendLine(@"Vertices (x, value, degree):");
//      sb.AppendJoin(System.Environment.NewLine, GetVerticesWithValueAndDegree()).AppendLine();

//      sb.AppendLine();

//      sb.AppendLine(@"Edges (x, y, value):");
//      sb.AppendJoin(System.Environment.NewLine, GetEdgesWithValue()).AppendLine();

//      return sb.ToString();
//    }

//    #region Object overrides.
//    public override string ToString()
//    {
//      var sb = new System.Text.StringBuilder();
//      var edgeCount = 0;
//      foreach (var edge in GetDirectedEdges())
//        sb.AppendLine($"#{++edgeCount}: {edge}");
//      sb.Insert(0, $"<{GetType().Name}: ({m_vertices.Count} vertices, {edgeCount} edges)>{System.Environment.NewLine}");
//      return sb.ToString();
//    }
//    #endregion Object overrides.
//  }
//}

///*
//      // Adjacent Matrix.

//      var am = new Flux.Collections.Generic.Graph.AdjacentMatrix<char, int>();

//      am.AddVertex('a');
//      am.AddVertex('b');
//      am.AddVertex('c');
//      am.AddVertex('d');

//      //am.AddDirectedEdge('a', 'b', 1);
//      //am.AddDirectedEdge('a', 'c', 1);
//      //am.AddDirectedEdge('b', 'a', 1);
//      //am.AddDirectedEdge('b', 'c', 1);
//      //am.AddDirectedEdge('c', 'a', 1);
//      //am.AddDirectedEdge('c', 'b', 1);
//      //am.AddDirectedEdge('c', 'd', 1);
//      //am.AddDirectedEdge('d', 'c', 1);

//      am.AddDirectedEdge('a', 'b', 2);
//      am.AddUndirectedEdge('a', 'c', 1);
//      am.AddUndirectedEdge('b', 'c', 1);
//      am.AddUndirectedEdge('c', 'd', 1);

//      am.RemoveUndirectedEdge('c', 'b', 1);

//      System.Console.WriteLine(am.ToConsoleString());

//      foreach (var edge in am.GetEdges())
//        System.Console.WriteLine(edge);
// */
