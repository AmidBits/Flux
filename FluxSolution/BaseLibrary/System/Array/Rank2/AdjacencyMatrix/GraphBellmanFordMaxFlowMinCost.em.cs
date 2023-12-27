namespace Flux
{
  public static partial class Reflection
  {
    /// <summary>Returns the maximum flow/minimum cost using the Bellman-Ford algorithm.</summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
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

      double Capacity(int s, int t)
        => capacitySelector(GraphGetState(source, s, t));
      double Cost(int s, int t)
        => costSelector(GraphGetState(source, s, t));

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
  }
}
