namespace Flux
{
  public static partial class Arrays
  {

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


    /// <summary>Returns the maximum flow/minimum cost using the Bellman-Ford algorithm.</summary>
    /// <param name="y"></param>
    /// <param name="x"></param>
    /// <param name="capacitySelector"></param>
    /// <param name="costSelector"></param>
    public static (double totalFlow, double totalCost) GraphBellmanFordMaxFlowMinCost<T>(this T[,] source, int x, int y, System.Func<object, double> capacitySelector, System.Func<object, double> costSelector)
      where T : System.IEquatable<T>
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

      return GetMaxFlow(x, y);

      // Determine if it is possible to have a flow from the source to target.
      bool Search(int y, int x)
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
              var minValue = distance[y] + pi![y] - pi[i] - costSelector(source.GraphGetState(i, y)); // Obtain the total value.
              if (minValue < distance[i])// If dist[k] is > minimum value, update.
              {
                distance[i] = minValue;
                dad![i] = y;
              }
            }

            if (flow[y, i] < capacitySelector(source.GraphGetState(y, i)))
            {
              var minValue = distance[y] + pi![y] - pi[i] + costSelector(source.GraphGetState(y, i));
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

      // Obtain the maximum Flow.
      (double totalFlow, double totalCost) GetMaxFlow(int y, int x)
      {
        var totalFlow = 0d;
        var totalCost = 0d;

        while (Search(y, x)) // If a path exist from source to target.
        {
          var amt = double.PositiveInfinity; // Set the default amount.

          for (var i = x; i != y; i = dad[i])
            amt = double.Min(amt, flow[i, dad[i]] != 0 ? flow[i, dad[i]] : capacitySelector(source.GraphGetState(dad[i], i)) - flow[dad[i], i]);

          for (var i = x; i != y; i = dad[i])
          {
            if (flow[i, dad[i]] != 0)
            {
              flow[i, dad[i]] -= amt;
              totalCost -= amt * costSelector(source.GraphGetState(i, dad[i]));
            }
            else
            {
              flow[dad[i], i] += amt;
              totalCost += amt * costSelector(source.GraphGetState(dad[i], i));
            }
          }

          totalFlow += amt;
        }

        return (totalFlow, totalCost); // Return pair total flow and cost.
      }
    }
  }
}
