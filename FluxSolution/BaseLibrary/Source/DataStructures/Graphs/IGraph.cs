namespace Flux
{
  public static partial class Em
  {
    /// <summary>Creates a new sequence with the shortest path tree, i.e. the shortest paths from the specified origin vertex to all reachable vertices.</summary>
    /// <param name="distanceSelector">Selects the length of the edge (i.e. the distance between the endpoints).</param>
    /// <see href="https://www.geeksforgeeks.org/dijkstras-shortest-path-algorithm-greedy-algo-7/"/>
    public static System.Collections.Generic.IEnumerable<(int destination, double distance)> GetDijkstraShortestPathTree<TVertexValue, TEdgeValue>(this DataStructures.IGraph<TVertexValue, TEdgeValue> source, int origin, System.Func<TEdgeValue, double> distanceSelector)
    {
      var vertices = System.Linq.Enumerable.ToList(source.GetVertices());

      var distances = System.Linq.Enumerable.ToDictionary(vertices, v => v, v => v.Equals(origin) ? 0 : double.PositiveInfinity);

      var edges = System.Linq.Enumerable.ToList(source.GetEdges()); // Cache edges, because we need it while there are available distances.

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
  }

  namespace DataStructures
  {
    public interface IGraph<TVertexValue, TEdgeValue>
    {
      System.Collections.Generic.IEnumerable<(int x, int y, TEdgeValue value)> GetEdges();

      System.Collections.Generic.IEnumerable<int> GetVertices();
    }
  }
}
