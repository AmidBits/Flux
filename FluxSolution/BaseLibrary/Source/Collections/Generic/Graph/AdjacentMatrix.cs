using System.Linq;

namespace Flux.Collections.Generic.Graph
{
  /// <summary>Represents a graph using an adjacency matrix. Unlimited edge combinations and types.</summary>
  /// https://docs.microsoft.com/en-us/previous-versions/ms379574(v=vs.80)
  /// https://www.tutorialspoint.com/representation-of-graphs
  /// https://www.geeksforgeeks.org/graph-data-structure-and-algorithms/
  /// https://en.wikipedia.org/wiki/Graph_(discrete_mathematics)/
  public class AdjacentMatrix<TVertex, TWeight>
    : IGraph<TVertex, TWeight>
    where TVertex : System.IEquatable<TVertex>
    where TWeight : System.IComparable<TWeight>, System.IEquatable<TWeight>
  {
    private readonly System.Collections.Generic.List<TVertex> m_vertices = new System.Collections.Generic.List<TVertex>();

    private TWeight[,] m_weights = new TWeight[0, 0];

    public bool AddVertex(TVertex vertex)
    {
      if (!m_vertices.Contains(vertex))
      {
        var index = m_vertices.Count;

        m_vertices.Add(vertex);

        m_weights = m_weights.Insert(0, index, default!);
        m_weights = m_weights.Insert(1, index, default!);

        return true;
      }
      else // Vertex already exists.
        return false;
    }
    public bool RemoveVertex(TVertex vertex)
    {
      if (m_vertices.Contains(vertex))
      {
        var index = m_vertices.IndexOf(vertex);

        m_weights = m_weights.Remove(0, index);
        m_weights = m_weights.Remove(1, index);

        m_vertices.RemoveAt(index);

        return true;
      }
      else // No vertex to remove.
        return false;
    }

    public System.Collections.Generic.IEnumerable<Vertex<TVertex>> GetVertices()
    {
      for (var row = 0; row < m_vertices.Count; row++)
      {
        var degree = 0;

        for (var column = m_vertices.Count - 1; column >= 0; column--)
          if (!m_weights[row, column].Equals(default!))
            degree++;

        yield return new Vertex<TVertex>(m_vertices[row], degree);
      }
    }

    public System.Collections.Generic.IEnumerable<Edge<TVertex, TWeight>> GetEdges()
    {
      var vertices = GetVertices().ToList();

      for (var row = 0; row < m_vertices.Count; row++)
        for (var column = 0; column < m_vertices.Count; column++)
          if (m_weights[row, column] is var weight && !weight.Equals(default!))
            yield return new Edge<TVertex, TWeight>(vertices[row], vertices[column], weight);
    }

    public void AddDirectedEdge(TVertex source, TVertex target, TWeight weight)
      => SetDirectedEdge(source, target, weight);
    public void AddUndirectedEdge(TVertex source, TVertex target, TWeight weight)
      => SetUndirectedEdge(source, target, weight);

    public bool RemoveDirectedEdge(TVertex source, TVertex target, TWeight weight)
      => ResetDirectedEdge(source, target, weight);
    public bool RemoveUndirectedEdge(TVertex source, TVertex target, TWeight weight)
      => ResetUndirectedEdge(source, target, weight);

    public bool ResetDirectedEdge(TVertex source, TVertex target, TWeight weight)
    {
      var sourceIndex = m_vertices.IndexOf(source);
      var targetIndex = m_vertices.IndexOf(target);

      if (m_weights[sourceIndex, targetIndex].Equals(weight))
      {
        m_weights[sourceIndex, targetIndex] = default!;

        return true;
      }
      else // There is no directed edge.
        return false;
    }
    public bool ResetUndirectedEdge(TVertex source, TVertex target, TWeight weight)
    {
      var sourceIndex = m_vertices.IndexOf(source);
      var targetIndex = m_vertices.IndexOf(target);

      if (m_weights[sourceIndex, targetIndex].Equals(weight) && m_weights[targetIndex, sourceIndex].Equals(weight))
      {
        m_weights[sourceIndex, targetIndex] = default!;
        m_weights[targetIndex, sourceIndex] = default!;

        return true;
      }
      else // There is no undirected edge.
        return false;
    }

    public void SetDirectedEdge(TVertex source, TVertex target, TWeight weight)
    {
      AddVertex(source);
      AddVertex(target);

      m_weights[m_vertices.IndexOf(source), m_vertices.IndexOf(target)] = weight;
    }
    public void SetUndirectedEdge(TVertex source, TVertex target, TWeight weight)
    {
      AddVertex(source);
      AddVertex(target);

      var sourceIndex = m_vertices.IndexOf(source);
      var targetIndex = m_vertices.IndexOf(target);

      m_weights[sourceIndex, targetIndex] = weight;
      m_weights[targetIndex, sourceIndex] = weight;
    }

    public string ToConsoleString()
      => m_weights.ToConsoleString();

    // Overrides.
    public override string ToString()
    {
      var sb = new System.Text.StringBuilder();
      var index = 0;
      foreach (var edge in GetEdges())
        sb.AppendLine($"#{++index}: {edge}");
      sb.Insert(0, $"<{nameof(AdjacentMatrix<TVertex, TWeight>)}: ({GetVertices().Count()} vertices, {index} edges)>{System.Environment.NewLine}");
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
