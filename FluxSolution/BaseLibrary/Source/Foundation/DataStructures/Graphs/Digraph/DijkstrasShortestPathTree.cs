using System.Linq;

namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Computes the shortest path from the start vertex to all reachable vertices.</summary>
    /// <param name="distanceSelector">Selects the length of the edge (i.e. the distance between the endpoints).</param>
    public static System.Collections.Generic.Dictionary<TVertex, double> DijkstraShortestPathTree<TVertex, TVertexValue, TEdgeValue>(this DataStructures.Graphs.IDigraph<TVertex, TVertexValue, TEdgeValue> source, TVertex start, System.Func<TEdgeValue, double> distanceSelector)
      where TVertex : System.IEquatable<TVertex>
    {
      var distances = source.GetVertices().ToDictionary(v => v, v => v.Equals(start) ? 0 : double.PositiveInfinity);

      System.Collections.Generic.Dictionary<TVertex, double> spt = new System.Collections.Generic.Dictionary<TVertex, double>(); // Initial shortest path tree is empty.

      var edges = source.GetEdges();

      while (distances.Any()) // As long as there are nodes available.
      {
        var shortest = distances.OrderBy(v => v.Value).First(); // Get the node with the shortest distance.

        if (shortest.Value < double.PositiveInfinity) // If the distance to the node is less than infinity, it was reachable so it should be returned.
          spt.Add(shortest.Key, shortest.Value);

        distances.Remove(shortest.Key); // This node is now final, so remove it.

        foreach (var (from, to, value) in edges.Where(edge => edge.source.Equals(shortest.Key))) // Updates all nodes reachable from the vertex.
        {
          if (distances.TryGetValue(to, out var distanceToEdgeTarget)) // If it's currently in distances.
          {
            var distanceViaShortest = shortest.Value + distanceSelector(value); // Distance via the current node.

            if (distanceViaShortest < distanceToEdgeTarget) // If the distance via the current node is shorter than the currently recorded distance, replace it.
              distances[to] = distanceViaShortest;
          }
        }
      }

      return spt;
    }
  }
}
