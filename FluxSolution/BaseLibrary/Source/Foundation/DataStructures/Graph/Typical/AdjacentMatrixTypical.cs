using System.Linq;

namespace Flux.DataStructures.Graph
{
  /// <summary>Represents a graph using an adjacency matrix. Unlimited edge combinations and types.</summary>
  /// https://docs.microsoft.com/en-us/previous-versions/ms379574(v=vs.80)
  /// https://www.tutorialspoint.com/representation-of-graphs
  /// https://www.geeksforgeeks.org/graph-data-structure-and-algorithms/
  /// <see cref="https://en.wikipedia.org/wiki/Graph_(discrete_mathematics)"/>
  public class AdjacentMatrixTypical<TVertex, TValue>
    : IGraphTypical<TVertex, TValue>
    where TVertex : System.IEquatable<TVertex>
    where TValue : System.IEquatable<TValue>
  {
    private readonly System.Collections.Generic.List<TVertex> m_vertices = new System.Collections.Generic.List<TVertex>();
    private readonly System.Collections.Generic.List<TValue> m_valuesOfVertices = new System.Collections.Generic.List<TValue>();

    private int[,] m_matrix = new int[0, 0];

    private TValue[,] m_valuesOfEdges = new TValue[0, 0];

    public bool IsAdjacent(TVertex source, TVertex target)
    {
      var sourceIndex = m_vertices.IndexOf(source);
      var targetIndex = m_vertices.IndexOf(target);

      return sourceIndex > -1 && targetIndex > -1 && m_matrix[sourceIndex, targetIndex] != 0;
    }

    public System.Collections.Generic.IEnumerable<TVertex> GetNeighbors(TVertex source)
    {
      foreach (var matrix in m_matrix.GetElements(0, m_vertices.IndexOf(source)))
        if (matrix.item != 0)
          yield return m_vertices[matrix.index1];
    }

    public void AddVertex(TVertex vertex, TValue value)
    {
      if (!m_vertices.Contains(vertex))
      {
        var index = m_vertices.Count;

        m_vertices.Add(vertex);
        m_valuesOfVertices.Add(value);

        m_matrix = m_matrix.Insert(0, index, true, 0);
        m_matrix = m_matrix.Insert(1, index, true, 0);

        m_valuesOfEdges = m_valuesOfEdges.Insert(0, index, default!);
        m_valuesOfEdges = m_valuesOfEdges.Insert(1, index, default!);
      }
    }
    public void AddVertex(TVertex vertex)
      => AddVertex(vertex, default!);
    //public bool ContainsVertex(TVertex vertex)
    //  => m_vertices.Contains(vertex);
    public void RemoveVertex(TVertex vertex)
    {
      if (m_vertices.Contains(vertex))
      {
        var index = m_vertices.IndexOf(vertex);

        m_valuesOfEdges = m_valuesOfEdges.Remove(0, index);
        m_valuesOfEdges = m_valuesOfEdges.Remove(1, index);

        m_matrix = m_matrix.Remove(0, index);
        m_matrix = m_matrix.Remove(1, index);

        m_valuesOfVertices.RemoveAt(index);
        m_vertices.RemoveAt(index);
      }
    }

    public void AddEdge(TVertex source, TVertex target, TValue value)
    {
      AddVertex(source);
      AddVertex(target);

      m_matrix[m_vertices.IndexOf(source), m_vertices.IndexOf(target)] = source.Equals(target) ? 2 : 1;

      SetEdgeValue(source, target, value);
    }
    public void AddEdge(TVertex source, TVertex target)
      => AddEdge(source, target, default!);
    public void RemoveEdge(TVertex source, TVertex target)
    {
      var sourceIndex = m_vertices.IndexOf(source);
      var targetIndex = m_vertices.IndexOf(target);

      if (sourceIndex > -1 && targetIndex > -1 && m_matrix[sourceIndex, targetIndex] != 0)
        m_matrix[sourceIndex, targetIndex] = 0;
    }

    public TValue GetVertexValue(TVertex vertex)
      => m_valuesOfVertices[m_vertices.IndexOf(vertex)];
    public void SetVertexValue(TVertex vertex, TValue value)
      => m_valuesOfVertices[m_vertices.IndexOf(vertex)] = value;

    public TValue GetEdgeValue(TVertex source, TVertex target)
      => m_valuesOfEdges[m_vertices.IndexOf(source), m_vertices.IndexOf(target)];
    public void SetEdgeValue(TVertex source, TVertex target, TValue value)
      => m_valuesOfEdges[m_vertices.IndexOf(source), m_vertices.IndexOf(target)] = value;

    public System.Collections.Generic.IEnumerable<(TVertex vertex, int degree)> GetVertices()
    {
      foreach (var vertex in m_vertices)
        yield return (vertex, m_matrix.GetElements(0, m_vertices.IndexOf(vertex)).Sum(vt => vt.item));
    }

    public System.Collections.Generic.IEnumerable<(TVertex source, TVertex target, TValue value)> GetEdges()
    {
      foreach (var source in m_vertices)
        foreach (var target in GetNeighbors(source))
          yield return (source, target, GetEdgeValue(source, target));
      //var vertices = GetVertices().ToList();

      //for (var row = 0; row < m_vertices.Count; row++)
      //  for (var column = 0; column < m_vertices.Count; column++)
      //    if (m_matrix[row, column] != 0)
      //      yield return (vertices[row].vertex, vertices[column].vertex, m_valuesOfEdges[row, column]);
    }

    //public void AddDirectedEdge(TVertex source, TVertex target, TWeight weight)
    //  => SetDirectedEdge(source, target, weight);
    //public void AddUndirectedEdge(TVertex source, TVertex target, TWeight weight)
    //  => SetUndirectedEdge(source, target, weight);

    //public bool ContainsDirectedEdge(TVertex source, TVertex target, TWeight weight)
    //  => m_vertices.IndexOf(source) is var sourceIndex && sourceIndex > -1
    //  && m_vertices.IndexOf(target) is var targetIndex && targetIndex > -1
    //  && m_matrix[sourceIndex, targetIndex].Equals(weight);
    //public bool ContainsUndirectedEdge(TVertex source, TVertex target, TWeight weight)
    //  => m_vertices.IndexOf(source) is var sourceIndex && sourceIndex > -1
    //  && m_vertices.IndexOf(target) is var targetIndex && targetIndex > -1
    //  && m_matrix[sourceIndex, targetIndex].Equals(weight)
    //  && m_matrix[targetIndex, sourceIndex].Equals(weight);

    //public bool RemoveDirectedEdge(TVertex source, TVertex target, TWeight weight)
    //=> ResetDirectedEdge(source, target, weight);
    //public bool RemoveUndirectedEdge(TVertex source, TVertex target, TWeight weight)
    //  => ResetUndirectedEdge(source, target, weight);

    //public bool ResetDirectedEdge(TVertex source, TVertex target, TWeight weight)
    //{
    //  var sourceIndex = m_vertices.IndexOf(source);
    //  var targetIndex = m_vertices.IndexOf(target);

    //  if (sourceIndex > -1 && targetIndex > -1 && m_matrix[sourceIndex, targetIndex].Equals(weight))
    //  {
    //    m_matrix[sourceIndex, targetIndex] = default!;

    //    return true;
    //  }
    //  else // There is no directed edge.
    //    return false;
    //}
    //public bool ResetUndirectedEdge(TVertex source, TVertex target, TWeight weight)
    //{
    //  var sourceIndex = m_vertices.IndexOf(source);
    //  var targetIndex = m_vertices.IndexOf(target);

    //  if (sourceIndex > -1 && targetIndex > -1 && m_matrix[sourceIndex, targetIndex].Equals(weight) && m_matrix[targetIndex, sourceIndex].Equals(weight))
    //  {
    //    m_matrix[sourceIndex, targetIndex] = default!;
    //    m_matrix[targetIndex, sourceIndex] = default!;

    //    return true;
    //  }
    //  else // There is no undirected edge.
    //    return false;
    //}

    //public void SetDirectedEdge(TVertex source, TVertex target, TWeight weight)
    //{
    //  AddVertex(source);
    //  AddVertex(target);

    //  m_matrix[m_vertices.IndexOf(source), m_vertices.IndexOf(target)] = weight;
    //}
    //public void SetUndirectedEdge(TVertex source, TVertex target, TWeight weight)
    //{
    //  AddVertex(source);
    //  AddVertex(target);

    //  var sourceIndex = m_vertices.IndexOf(source);
    //  var targetIndex = m_vertices.IndexOf(target);

    //  m_matrix[sourceIndex, targetIndex] = weight;
    //  m_matrix[targetIndex, sourceIndex] = weight;
    //}

    //public System.Collections.Generic.IEnumerable<Vertex<TVertex>> GetVertices()
    //{
    //  for (var row = 0; row < m_vertices.Count; row++)
    //  {
    //    var degree = 0;

    //    for (var column = m_vertices.Count - 1; column >= 0; column--)
    //    {
    //      if (!m_matrix[row, column].Equals(default!))
    //        degree++;
    //      if (!m_matrix[column, row].Equals(default!))
    //        degree++;
    //    }

    //    yield return new Vertex<TVertex>(m_vertices[row], degree);
    //  }
    //}

    //public System.Collections.Generic.IEnumerable<Edge<TVertex, TWeight>> GetEdges()
    //{
    //  var vertices = GetVertices().ToList();

    //  for (var row = 0; row < m_vertices.Count; row++)
    //    for (var column = 0; column < m_vertices.Count; column++)
    //      if (m_matrix[row, column] is var weight && !weight.Equals(default!))
    //        yield return new Edge<TVertex, TWeight>(vertices[row], vertices[column], weight);
    //}

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

    // Overrides.
    public override string ToString()
    {
      var sb = new System.Text.StringBuilder();
      var index = 0;
      foreach (var edge in GetEdges())
        sb.AppendLine($"#{++index}: {edge}");
      sb.Insert(0, $"<{nameof(AdjacentMatrixTypical<TVertex, TValue>)}: ({GetVertices().Count()} vertices, {index} edges)>{System.Environment.NewLine}");
      return sb.ToString();
    }

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
