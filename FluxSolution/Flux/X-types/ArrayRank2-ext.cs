namespace Flux
{
  public static partial class ArrayRank2Extensions
  {
    extension<T>(T[,] source)
    {
      /// <summary>
      /// <para>Writes a two-dimensional array as URGF (Unicode tabular data) to the <paramref name="target"/>.</para>
      /// </summary>
      /// <param name="target"></param>
      /// <param name="source"></param>
      public void WriteRank2ToUrgf(System.IO.TextWriter target)
      {
        var length0 = source.GetLength(0);
        var length1 = source.GetLength(1);

        for (var r = 0; r < length0; r++)
        {
          if (r > 0) target.Write((char)UnicodeInformationSeparator.RecordSeparator);

          for (var c = 0; c < length1; c++)
          {
            if (c > 0) target.Write((char)UnicodeInformationSeparator.UnitSeparator);

            target.Write(source[r, c]);
          }
        }
      }
    }

    extension<T>(T[,] source)
      where T : System.IEquatable<T>
    {
      /// <summary>
      /// <para>Asserts the adjacency matrix graph property: two dimensions of symmetrical length.</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="length"></param>
      /// <exception cref="System.ArgumentNullException"></exception>
      /// <exception cref="System.ArgumentException"></exception>
      public void GraphAssertProperty(out int length)
      {
        if (!System.Array.TryHasDimensionalSymmetry(source, out length) || !System.Array.IsRank(source, 2))
        {
          System.ArgumentNullException.ThrowIfNull(source);

          throw new System.ArgumentException($"An asymmetrical array does not represent an adjacency matrix.");
        }
      }

      /// <summary>Returns the maximum flow/minimum cost using the Bellman-Ford algorithm.</summary>
      /// <param name="y"></param>
      /// <param name="x"></param>
      /// <param name="capacitySelector"></param>
      /// <param name="costSelector"></param>
      public (double totalFlow, double totalCost) GraphBellmanFordMaxFlowMinCost(int x, int y, System.Func<object, double> capacitySelector, System.Func<object, double> costSelector)
      {
        GraphAssertProperty(source, out var length);

        System.ArgumentNullException.ThrowIfNull(capacitySelector);
        System.ArgumentNullException.ThrowIfNull(costSelector);

        var vertexCount = length;

        var found = new bool[vertexCount];
        var flow = new double[vertexCount, vertexCount];
        var distance = new double[vertexCount + 1];
        var dad = new int[vertexCount];
        var pi = new double[vertexCount];

        return GraphBellmanFordGetMaxFlow(source, x, y, capacitySelector, costSelector, vertexCount, found, flow, distance, dad, pi);

        /*
    function BellmanFord(list vertices, list edges, vertex source) is

        // This implementation takes in a graph, represented as
        // lists of vertices (represented as integers [0..n-1]) and edges,
        // and fills two arrays (distance and predecessor) holding
        // the shortest path from the source to each vertex

        distance := list of size n
        predecessor := list of size n

        // Step 1: initialize graph
        for each vertex v in vertices do
            // Initialize the distance to all vertices to infinity
            distance[v] := inf
            // And having a null predecessor
            predecessor[v] := null

        // The distance from the source to itself is zero
        distance[source] := 0

        // Step 2: relax edges repeatedly
        repeat |V|-1 times:
            for each edge (u, v) with weight w in edges do
                if distance[u] + w < distance[v] then
                    distance[v] := distance[u] + w
                    predecessor[v] := u

        // Step 3: check for negative-weight cycles
        for each edge (u, v) with weight w in edges do
            if distance[u] + w < distance[v] then
                predecessor[v] := u
                // A negative cycle exists; find a vertex on the cycle 
                visited := list of size n initialized with false
                visited[v] := true
                while not visited[u] do
                    visited[u] := true
                    u := predecessor[u]
                // u is a vertex in a negative cycle, find the cycle itself
                ncycle := [u]
                v := predecessor[u]
                while v != u do
                    ncycle := concatenate([v], ncycle)
                    v := predecessor[v]
                error "Graph contains a negative-weight cycle", ncycle
        return distance, predecessor     
         */
      }

      // BellmanFord helper. Determine if it is possible to have a flow from the source to target.
      private bool GraphBellmanFordSearch(int y, int x, System.Func<object, double> capacitySelector, System.Func<object, double> costSelector, int vertexCount, bool[] found, double[,] flow, double[] distance, int[] dad, double[] pi)
      {
        System.Array.Fill(found!, false); // Initialise found[] to false.

        System.Array.Fill(distance!, double.PositiveInfinity); // Initialise the dist[] to INF.

        distance![y] = 0; // Distance from the source node.

        while (y != vertexCount) // Iterate until source reaches the number of vertices.
        {
          var best = vertexCount;

          found![y] = true;

          for (var i = 0; i < vertexCount; i++)
          {
            if (found![i]) // If already found, continue.
              continue;

            if (flow![i, y] != 0) // Evaluate while flow is still in supply.
            {
              var minValue = distance[y] + pi![y] - pi[i] - costSelector(GraphGetState(source, i, y)); // Obtain the total value.
              if (minValue < distance[i])// If dist[k] is > minimum value, update.
              {
                distance[i] = minValue;
                dad![i] = y;
              }
            }

            if (flow[y, i] < capacitySelector(GraphGetState(source, y, i)))
            {
              var minValue = distance[y] + pi![y] - pi[i] + costSelector(GraphGetState(source, y, i));
              if (minValue < distance[i]) // If dist[k] is > minimum value, update.
              {
                distance[i] = minValue;
                dad![i] = y;
              }
            }

            if (distance[i] < distance[best])
              best = i;
          }

          y = best; // Update src to best for next iteration.
        }

        for (var i = 0; i < vertexCount; i++)
          pi![i] = double.Min(pi[i] + distance[i], double.PositiveInfinity);

        return found![x]; // Return the value obtained at target.
      }

      // BellmanFord helper. Obtain the maximum Flow.
      private (double totalFlow, double totalCost) GraphBellmanFordGetMaxFlow(int y, int x, System.Func<object, double> capacitySelector, System.Func<object, double> costSelector, int vertexCount, bool[] found, double[,] flow, double[] distance, int[] dad, double[] pi)
      {
        var totalFlow = 0d;
        var totalCost = 0d;

        while (GraphBellmanFordSearch(source, y, x, capacitySelector, costSelector, vertexCount, found, flow, distance, dad, pi)) // If a path exist from source to target.
        {
          var amt = double.PositiveInfinity; // Set the default amount.

          for (var i = x; i != y; i = dad[i])
            amt = double.Min(amt, flow[i, dad[i]] != 0 ? flow[i, dad[i]] : capacitySelector(GraphGetState(source, dad[i], i)) - flow[dad[i], i]);

          for (var i = x; i != y; i = dad[i])
          {
            if (flow[i, dad[i]] != 0)
            {
              flow[i, dad[i]] -= amt;
              totalCost -= amt * costSelector(GraphGetState(source, i, dad[i]));
            }
            else
            {
              flow[dad[i], i] += amt;
              totalCost += amt * costSelector(GraphGetState(source, dad[i], i));
            }
          }

          totalFlow += amt;
        }

        return (totalFlow, totalCost); // Return pair total flow and cost.
      }

      /// <summary>Creates a new sequence with the shortest path tree, i.e. the shortest paths from the specified origin vertex to all reachable vertices.</summary>
      /// <param name="distanceSelector">Selects the length of the edge (i.e. the distance between the endpoints).</param>
      public System.Collections.Generic.IEnumerable<(int destination, double distance)> GraphDijkstraShortestPathTree(int origin, System.Func<object, double> distanceSelector, System.Func<int, int, T, bool>? isEdgePredicate = null)
      {
        GraphAssertProperty(source, out var length);

        System.ArgumentNullException.ThrowIfNull(distanceSelector);

        isEdgePredicate ??= (r, c, v) => !v.Equals(default);

        var vertices = GraphGetVertices(source).ToList();

        var distances = vertices.ToDictionary(v => v, v => v.Equals(origin) ? 0 : double.PositiveInfinity);

        var edges = GraphGetEdges(source, isEdgePredicate).ToList(); // Cache edges, because we need it while there are available distances.

        while (distances.Count != 0) // As long as there are nodes available.
        {
          var shortest = distances.OrderBy(v => v.Value).First(); // Get the node with the shortest distance.

          if (shortest.Value < double.PositiveInfinity) // If the distance to the node is less than infinity, it was reachable so it should be returned.
            yield return (shortest.Key, shortest.Value);

          distances.Remove(shortest.Key); // This node is now final, so remove it.

          foreach (var (x, y, v) in edges.Where(e => e.value.Equals(shortest.Key))) // Updates all nodes reachable from the vertex.
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

      /// <summary>
      /// <para></para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="isEdgePredicate"></param>
      /// <returns></returns>
      public System.Collections.Generic.IEnumerable<(int keySource, int keyTarget, T value)> GraphGetEdges(System.Func<int, int, T, bool> isEdgePredicate)
      {
        GraphAssertProperty(source, out var length);

        for (var r = 0; r < length; r++)
          for (var c = 0; c < length; c++)
            if (source[r, c] is var m && isEdgePredicate(r, c, m))
              yield return (r, c, m);
      }

      ///// <summary>If the adjacency matrix value yields the default(T) value, state = 0, otherwise, if the from/to indices are the same. state = 2. and if they are different, state = 1. This "Impl" version bypass argument checks.</summary>
      //private int GraphGetStateImpl(int keyFrom, int keyTo)
      //  => source[keyFrom, keyTo] is var m && m.Equals(default!) ? 0 : keyFrom == keyTo ? 2 : 1;

      /// <summary>If the adjacency matrix value yields the default(T) value, state = 0, otherwise, if the from/to indices are the same. state = 2. and if they are different, state = 1. This version performs all argument checks.</summary>
      public int GraphGetState(int keySource, int keyTarget)
      {
        GraphAssertProperty(source, out var length);

        if (keySource < 0 || keySource >= length) throw new System.ArgumentOutOfRangeException(nameof(keySource));
        if (keyTarget < 0 || keyTarget >= length) throw new System.ArgumentOutOfRangeException(nameof(keyTarget));

        return source[keySource, keyTarget] is var m && m.Equals(default!) ? 0 : keySource == keyTarget ? 2 : 1;

        //return GraphGetStateImpl(source, keySource, keyTarget);
      }

      public int[] GraphGetVertices()
      {
        GraphAssertProperty(source, out var length);

        return System.Linq.Enumerable.Range(0, length).ToArray();
      }
    }
  }
}

#region Create sample file
/*

using var sw = System.IO.File.CreateText(fileName);

var data = new string[][] { new string[] { "A", "B" }, new string[] { "1", "2" } };

Flux.Fx.WriteUrgf(sw, data);

sw.Write((char)UnicodeInformationSeparator.FileSeparator);

data = new string[][] { new string[] { "C", "D" }, new string[] { "3", "4" } };

Flux.Fx.WriteUrgf(sw, data);

sw.Flush();
sw.Close();

*/
#endregion
