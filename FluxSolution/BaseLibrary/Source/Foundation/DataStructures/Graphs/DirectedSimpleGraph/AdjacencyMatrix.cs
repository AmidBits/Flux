﻿namespace Flux.DataStructures.Graphs
{
  /// <summary>Represents a graph using an adjacency matrix. Unlimited edge combinations and types.</summary>
  /// https://docs.microsoft.com/en-us/previous-versions/ms379574(v=vs.80)
  /// https://www.tutorialspoint.com/representation-of-graphs
  /// https://www.geeksforgeeks.org/graph-data-structure-and-algorithms/
  /// <see cref="https://en.wikipedia.org/wiki/Graph_(discrete_mathematics)"/>
  public class AdjacencyMatrix
  {
    private int[,] m_matrix;

    private readonly System.Collections.Generic.Dictionary<int, object> m_vertexValues;
    private readonly System.Collections.Generic.Dictionary<(int, int), object> m_edgeValues;

    public AdjacencyMatrix(int[,] matrix)
    {
      m_matrix = matrix.IsArraySymmetrical() ? matrix : throw new System.ArgumentOutOfRangeException(nameof(matrix));

      m_vertexValues = new System.Collections.Generic.Dictionary<int, object>();
      m_edgeValues = new System.Collections.Generic.Dictionary<(int, int), object>();
    }
    public AdjacencyMatrix()
      : this(new int[0, 0])
    { }

    /// <summary>Returns the count of vertices within the adjacency matrix.</summary>
    public int Count
      => m_matrix.GetLength(0);

    /// <summary>Returns the basic adjacency matrix.</summary>
    public int[,] Matrix
      => m_matrix;

    /// <summary>Returns the degree of the vertex x. Loops are not counted.</summary>
    public int GetDegree(int x)
    {
      var count = 0;

      for (var i = Count - 1; i >= 0; i--)
      {
        count += m_matrix[x, i];

        if (i != x) // Exclude if the vertex is both source and target.
          count += m_matrix[i, x];
      }

      return count;
    }
    /// <summary>Lists all vertices y such that there is an edge from the vertex x to the vertex y. Loops are not considered neighbors.</summary>
    public System.Collections.Generic.IEnumerable<int> GetNeighbors(int v)
    {
      var count = Count;

      for (var i = 0; i < count; i++)
        if (m_matrix[v, i] is var m && m == 1)
          yield return i;
    }
    /// <summary>Tests whether there is an edge from the vertex x to the vertex y.</summary>
    public bool IsAdjacent(int x, int y)
      => VertexExists(x) && VertexExists(y) && m_matrix[x, y] == 1;

    /// <summary>Adds the vertex x, if it is not there.</summary>
    public bool AddVertex(int x)
    {
      var count = Count;

      if (x >= count)
      {
        for (var i = count; i <= x; i++)
        {
          m_matrix = m_matrix.Insert(0, i, 1, 0); // Add dimension 0 to accomodate the new vertex as a source.
          m_matrix = m_matrix.Insert(1, i, 1, 0); // Add dimension 1 to accomodate the new vertex as a target.
        }

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
      => x >= 0 && x < Count;
    /// <summary>Removes the vertex x, if it is there.</summary>
    public bool RemoveVertex(int x)
    {
      if (VertexExists(x))
      {
        RemoveVertexValue(x);

        m_matrix = m_matrix.Remove(0, x); // Add dimension 0 to accomodate vertex values.
        m_matrix = m_matrix.Remove(1, x); // Add dimension 1 to accomodate vertex values.

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

    /// <summary>Adds the edge from the vertex x to the vertex y, if it is not there.</summary>
    public bool AddEdge(int x, int y)
    {
      if (VertexExists(x) && VertexExists(y) && m_matrix[x, y] == 0)
      {
        m_matrix[x, y] = x != y ? 1 : 2;

        return true;
      }

      return false;
    }
    /// <summary>Adds the edge from the vertex x to the vertex y with the value ev, if it is not there.</summary>
    public bool AddEdge(int x, int y, object v)
    {
      if (AddEdge(x, y))
      {
        SetEdgeValue(x, y, v);

        return true;
      }

      return false;
    }
    /// <summary>Tests whether there is an edge (x, y) available, either directed or a loop.</summary>
    public bool EdgeExists(int x, int y)
      => VertexExists(x) && VertexExists(y) && m_matrix[x, y] > 0;
    /// <summary>Removes the edge from the vertex x to the vertex y, if it is there.</summary>
    public bool RemoveEdge(int x, int y)
    {
      if (EdgeExists(x, y))
      {
        RemoveEdgeValue(x, y);

        m_matrix[x, y] = 0;

        return true;
      }

      return false;
    }

    /// <summary>Returns whether the edge value was found and outputs the value associated if found. An edge can exists without a value.</summary>
    public bool TryGetEdgeValue(int x, int y, out object v)
      => m_edgeValues.TryGetValue((x, y), out v!);
    /// <summary>Removes the value for the edge and returns whether the removal was successful.</summary>
    public bool RemoveEdgeValue(int x, int y)
      => m_edgeValues.Remove((x, y));
    /// <summary>Sets the value associated with the edge (x, y) to v.</summary>
    public void SetEdgeValue(int x, int y, object v)
      => m_edgeValues[(x, y)] = v;

    /// <summary>Returns the maximum flow/minimum cost using the Bellman-Ford algorithm.</summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="capacitySelector"></param>
    /// <param name="costSelector"></param>
    /// <returns></returns>
    public (double totalFlow, double totalCost) GetBellmanFordMaxFlowMinCost(int x, int y, System.Func<object, double> capacitySelector, System.Func<object, double> costSelector)
    {
      if (!VertexExists(x)) throw new System.ArgumentOutOfRangeException(nameof(x));
      if (!VertexExists(y)) throw new System.ArgumentOutOfRangeException(nameof(y));
      if (capacitySelector is null) throw new System.ArgumentNullException(nameof(capacitySelector));
      if (costSelector is null) throw new System.ArgumentNullException(nameof(costSelector));

      var vertexCount = Count;

      var found = new bool[vertexCount];
      var flow = new double[vertexCount, vertexCount];
      var distance = new double[vertexCount + 1];
      var dad = new int[vertexCount];
      var pi = new double[vertexCount];

      double Capacity(int s, int t)
        => capacitySelector(TryGetEdgeValue(s, t, out var v) ? v : default!);
      double Cost(int s, int t)
        => costSelector(TryGetEdgeValue(s, t, out var v) ? v : default!);

      return GetMaxFlow(x, y);

      // Determine if it is possible to have a flow from the source to target.
      bool Search(int source, int target)
      {
        System.Array.Fill(found!, false); // Initialise found[] to false.

        System.Array.Fill(distance!, double.PositiveInfinity); // Initialise the dist[] to INF.

        distance![source] = 0; // Distance from the source node.

        while (source != vertexCount) // Iterate until source reaches the number of vertices.
        {
          var best = vertexCount;

          found![source] = true;

          for (var i = 0; i < vertexCount; i++)
          {
            if (found![i]) // If already found, continue.
              continue;

            if (flow![i, source] != 0) // Evaluate while flow is still in supply.
            {
              var minValue = distance[source] + pi![source] - pi[i] - Cost(i, source); // Obtain the total value.
              if (minValue < distance[i])// If dist[k] is > minimum value, update.
              {
                distance[i] = minValue;
                dad![i] = source;
              }
            }

            if (flow[source, i] < Capacity(source, i))
            {
              var minValue = distance[source] + pi![source] - pi[i] + Cost(source, i);
              if (minValue < distance[i]) // If dist[k] is > minimum value, update.
              {
                distance[i] = minValue;
                dad![i] = source;
              }
            }

            if (distance[i] < distance[best])
              best = i;
          }

          source = best; // Update src to best for next iteration.
        }

        for (var i = 0; i < vertexCount; i++)
          pi![i] = System.Math.Min(pi[i] + distance[i], double.PositiveInfinity);

        return found![target]; // Return the value obtained at target.
      }

      // Obtain the maximum Flow.
      (double totalFlow, double totalCost) GetMaxFlow(int source, int target)
      {
        var totalFlow = 0d;
        var totalCost = 0d;

        while (Search(source, target)) // If a path exist from source to target.
        {
          var amt = double.PositiveInfinity; // Set the default amount.

          for (var i = target; i != source; i = dad[i])
            amt = System.Math.Min(amt, flow[i, dad[i]] != 0 ? flow[i, dad[i]] : Capacity(dad[i], i) - flow[dad[i], i]);

          for (var i = target; i != source; i = dad[i])
          {
            if (flow[i, dad[i]] != 0)
            {
              flow[i, dad[i]] -= amt;
              totalCost -= amt * Cost(i, dad[i]);
            }
            else
            {
              flow[dad[i], i] += amt;
              totalCost += amt * Cost(dad[i], i);
            }
          }

          totalFlow += amt;
        }

        return (totalFlow, totalCost); // Return pair total flow and cost.
      }
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
      var count = Count;

      for (var x = 0; x < count; x++)
        for (var y = 0; y < count; y++)
          if (m_matrix[x, y] > 0)
            yield return (x, y, TryGetEdgeValue(x, y, out var v) ? v : default!);
    }

    public System.Collections.Generic.IEnumerable<int> GetVertices()
    {
      var count = Count;

      for (var i = 0; i < count; i++)
        yield return i;
    }
    public System.Collections.Generic.IEnumerable<(int x, object v)> GetVerticesWithValue()
    {
      var count = Count;

      for (var i = 0; i < count; i++)
        yield return (i, TryGetVertexValue(i, out var vv) ? vv : default!);
    }
    public System.Collections.Generic.IEnumerable<(int x, object v, int d)> GetVerticesWithValueAndDegree()
    {
      var count = Count;

      for (var i = 0; i < count; i++)
        yield return (i, TryGetVertexValue(i, out var v) ? v : default!, GetDegree(i));
    }

    public string ToConsoleString()
    {
      var sb = new System.Text.StringBuilder();

      sb.AppendLine(ToString());

      var l0 = m_matrix.GetLength(0);
      var l1 = m_matrix.GetLength(1);

      var grid = new object[l0 + 1, l1 + 1];

      for (var i0 = l0 - 1; i0 >= 0; i0--)
      {
        var vv = i0; // TryGetVertexValue(i0, out var v) ? v : default!;

        grid[i0 + 1, 0] = vv;
        grid[0, i0 + 1] = vv;

        for (var i1 = l1 - 1; i1 >= 0; i1--)
          grid[i0 + 1, i1 + 1] = m_matrix[i0, i1];
      }

      sb.AppendLine(grid.ToConsoleBlock(uniformWidth: true, centerContent: true));

      sb.AppendLine();

      sb.AppendLine(@"Vertices (x, value, degree):");
      sb.AppendJoin(System.Environment.NewLine, GetVerticesWithValueAndDegree()).AppendLine();

      sb.AppendLine();

      sb.AppendLine(@"Edges (x, y, value):");
      sb.AppendJoin(System.Environment.NewLine, GetEdges()).AppendLine();

      return sb.ToString();
    }

    #region Object overrides.
    public override string ToString()
      => $"<{GetType().Name}: ({Count} vertices, {System.Linq.Enumerable.Count(GetEdges())} edges)>{System.Environment.NewLine}";
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