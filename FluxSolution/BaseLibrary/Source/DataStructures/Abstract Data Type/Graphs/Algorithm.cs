namespace Flux.DataStructure.Graph
{
  public static class Algorithm
  {
    /// <summary>
    /// <para>The Bellman–Ford algorithm is an algorithm that computes shortest paths from a single source vertex to all of the other vertices in a weighted digraph.[1] It is slower than Dijkstra's algorithm for the same problem, but more versatile, as it is capable of handling graphs in which some of the edge weights are negative numbers.</para>
    /// <para>This implementation can handle negative weights.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Bellman%E2%80%93Ford_algorithm"/></para>
    /// </summary>
    /// <param name="vertices"></param>
    /// <param name="edges"></param>
    /// <param name="source"></param>
    /// <returns></returns>
    /// <exception cref="System.InvalidOperationException"></exception>
    public static (double[] Distance, int[] Predecessor) BellmanFordShortestPaths(System.ReadOnlySpan<int> vertices, System.ReadOnlySpan<(int x, int y, double w)> edges, int source)
    {
      var distance = new double[vertices.Length];
      var predecessor = new int[vertices.Length];

      // Step 1: initialize graph
      foreach (var v in vertices)
      {
        distance[v] = double.PositiveInfinity; // Initialize the distance to all vertices to infinity.
        predecessor[v] = -1; // And having a null predecessor.
      }

      distance[source] = 0; // The distance from the source to itself is zero.

      // Step 2: relax edges repeatedly
      for (var i = 0; i < edges.Length; i++)
      {
        foreach (var (u, v, w) in edges)
        {
          if (distance[u] + w < distance[v])
          {
            distance[v] = distance[u] + w;
            predecessor[v] = u;
          }
        }
      }

      // Step 3: check for negative-weight cycles
      foreach (var edge in edges)
      {
        var (u, v, w) = edge;

        if (distance[u] + w < distance[v])
        {
          predecessor[v] = u;

          // A negative cycle exists; find a vertex on the cycle 
          var visited = new bool[vertices.Length];
          visited[v] = true;

          while (!visited[u])
          {
            visited[u] = true;
            u = predecessor[u];
          }

          // u is a vertex in a negative cycle, find the cycle itself
          var ncycle = u;
          v = predecessor[u];

          while (v != u)
          {
            ncycle = (v + ncycle);

            v = predecessor[v];
          }

          throw new System.InvalidOperationException($"Graph contains a negative-weight cycle {ncycle}");
        }
      }

      return (distance, predecessor);
    }

    /// <summary>
    /// <para>Dijkstra's algorithm is an algorithm for finding the shortest paths between nodes in a weighted graph, which may represent, for example, a road network.</para>
    /// <para>This implementation cannot handle negative weights.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Dijkstra%27s_algorithm"/></para>
    /// </summary>
    /// <param name="vertices"></param>
    /// <param name="edges"></param>
    /// <param name="source"></param>
    /// <returns></returns>
    public static (double[] Distance, int[] Predecessor) DijkstraShortestPaths(System.ReadOnlySpan<int> vertices, System.ReadOnlySpan<(int x, int y, double w)> edges, int source)
    {
      var dist = new double[vertices.Length];
      var prev = new int[vertices.Length];

      var q = new System.Collections.Generic.Dictionary<int, double>();

      // Initialize graph.
      foreach (var v in vertices)
      {
        dist[v] = double.PositiveInfinity;
        prev[v] = 0;

        q.Add(v, 0);
      }

      dist[source] = 0; // The distance from the source to itself is zero.

      while (q.Count > 0)
      {
        var u = q.OrderBy(kvp => dist[kvp.Key]).First().Key;
        q.Remove(u);

        var neighbors = new System.Collections.Generic.List<(int v, double w)>();

        for (var i = 0; i < edges.Length; i++)
        {
          var (x, y, w) = edges[i];

          if (u == x && q.ContainsKey(y))
            neighbors.Add((y, w));
        }

        foreach (var (v, w) in neighbors)
        {
          var alt = dist[u] + w;

          if (alt < dist[v]) // If alt is shorter than the currently dist[v] then update graph.
          {
            dist[v] = alt;
            prev[v] = u;

            q[v] = alt;
          }
        }
      }

      return (dist, prev);
    }
  }
}
