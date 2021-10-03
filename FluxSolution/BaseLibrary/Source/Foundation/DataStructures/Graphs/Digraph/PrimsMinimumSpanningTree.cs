//using System.Linq;

//namespace Flux
//{
//  public static partial class ExtensionMethods
//  {
//    /// <summary>Computes the shortest path from the start vertex to all reachable vertices.</summary>
//    /// <param name="distanceSelector">Selects the length of the edge (i.e. the distance between the endpoints).</param>
//    // https://www.geeksforgeeks.org/prims-minimum-spanning-tree-mst-greedy-algo-5/
//    public static System.Collections.Generic.Dictionary<TVertex, double> PrimsMinimumSpanningTree<TVertex, TVertexValue, TEdgeValue>(this DataStructures.Graph.IGraphTypical<TVertex, TVertexValue, TEdgeValue> source, TVertex start, System.Func<TEdgeValue, double> distanceSelector)
//      where TVertex : System.IEquatable<TVertex>
//    {
//      var distances = source.GetVertices().ToDictionary(v => v, v => v.Equals(start) ? 0 : double.PositiveInfinity);

//      System.Collections.Generic.Dictionary<TVertex, double> spt = new System.Collections.Generic.Dictionary<TVertex, double>(); // Initial shortest path tree is empty.

//      var edges = source.GetEdges();

//      while (distances.Any()) // As long as there are nodes available.
//      {
//        var shortest = distances.OrderBy(v => v.Value).First(); // Get the node with the shortest distance.

//        spt.Add(shortest.Key, shortest.Value);

//        foreach (var next in edges.Where(edge => edge.source.Equals(shortest.Key))) // Updates all nodes reachable from the vertex.
//        {
//          if (distances.ContainsKey(next.target)) // If it's currently in distances.
//          {
//            var distance = distanceSelector(next.value);

//            if (distance < distances[next.target]) // If the distance via the current node is shorter than the currently recorded distance, replace it.
//              distances[next.target] = distance;
//          }
//        }
//      }

//      return spt;
//    }
//  }
//}
