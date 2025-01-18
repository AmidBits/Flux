namespace Flux
{
  #region Extension methods

  public static partial class Fx
  {
    /// <summary>
    /// <para></para>
    /// </summary>
    /// <typeparam name="TVertexValue"></typeparam>
    /// <typeparam name="TEdgeValue"></typeparam>
    /// <param name="source"></param>
    /// <param name="origin"></param>
    /// <param name="weightSelector"></param>
    /// <returns></returns>
    public static (double[] Distance, int[] Predecessor) BellmanFordShortestPaths<TVertexValue, TEdgeValue>(this DataStructure.IGraph<TVertexValue, TEdgeValue> source, int origin, System.Func<TEdgeValue, double> weightSelector)
    {
      var vertices = source.GetVertices().Select(v => v.x).ToArray();
      var edges = source.GetEdges().Select(edge => (edge.x, edge.y, weightSelector(edge.value))).ToArray();

      return Flux.DataStructure.Graph.Algorithm.BellmanFordShortestPaths(vertices, edges, origin);
    }

    /// <summary>
    /// <para></para>
    /// </summary>
    /// <typeparam name="TVertexValue"></typeparam>
    /// <typeparam name="TEdgeValue"></typeparam>
    /// <param name="source"></param>
    /// <param name="origin"></param>
    /// <param name="weightSelector"></param>
    /// <returns></returns>
    public static (double[] Distance, int[] Predecessor) DijkstraShortestPaths<TVertexValue, TEdgeValue>(this DataStructure.IGraph<TVertexValue, TEdgeValue> source, int origin, System.Func<TEdgeValue, double> weightSelector)
    {
      var vertices = source.GetVertices().Select(v => v.x).ToArray();
      var edges = source.GetEdges().Select(edge => (edge.x, edge.y, weightSelector(edge.value))).ToArray();

      return Flux.DataStructure.Graph.Algorithm.DijkstraShortestPaths(vertices, edges, origin);
    }

    /// <summary>
    /// <para>Creates a new sequence with the shortest path tree, i.e. the shortest paths from the specified origin vertex to all reachable vertices.</para>
    /// </summary>
    /// <param name="distanceSelector">Selects the length of the edge (i.e. the distance between the endpoints).</param>
    /// <see href="https://www.geeksforgeeks.org/dijkstras-shortest-path-algorithm-greedy-algo-7/"/>
    public static System.Collections.Generic.IEnumerable<(int destination, double distance)> GetDijkstraShortestPathTree<TVertexValue, TEdgeValue>(this DataStructure.IGraph<TVertexValue, TEdgeValue> source, int origin, System.Func<TEdgeValue, double> distanceSelector)
    {
      var vertices = source.GetVertices().Select(vt => vt.x).ToList();

      var distances = vertices.ToDictionary(v => v, v => v.Equals(origin) ? 0 : double.PositiveInfinity);

      var edges = source.GetEdges().ToList(); // Cache edges, because we need it while there are available distances.

      while (distances.Count != 0) // As long as there are nodes available.
      {
        var shortest = distances.OrderBy(v => v.Value).First(); // Get the node with the shortest distance.

        if (shortest.Value < double.PositiveInfinity) // If the distance to the node is less than infinity, it was reachable so it should be returned.
          yield return (shortest.Key, shortest.Value);

        distances.Remove(shortest.Key); // This node is now final, so remove it.

        foreach (var (x, y, v) in edges.Where(e => e.x.Equals(shortest.Key))) // Updates all nodes reachable from the vertex.
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

    public static string ToConsoleString<TVertexValue, TEdgeValue>(this DataStructure.IGraph<TVertexValue, TEdgeValue> source)
    {
      var sm = new SpanMaker<char>();

      sm = sm.AppendLine(source.ToString()).AppendLine();

      var v = source.GetVertices().ToList();

      var e = source.GetEdges().ToList();

      sm = sm.AppendLine(@"Vertices (x, value, degree):");
      sm = sm.AppendJoin(System.Environment.NewLine, v).AppendLine();

      sm = sm.AppendLine();

      sm = sm.AppendLine(@"Edges (x, y, value):");
      sm = sm.AppendJoin(System.Environment.NewLine, e).AppendLine();

      return sm.ToString();
    }

    public static DataStructure.IGraph<TVertexValue, TEdgeValue> TransposeToCopy<TVertexValue, TEdgeValue>(this DataStructure.IGraph<TVertexValue, TEdgeValue> source)
    {
      var al = source.CloneEmpty();

      foreach (var (x, value) in source.GetVertices())
        al.AddVertex(x, value);

      foreach (var (x, y, value) in source.GetEdges())
        al.AddEdge(y, x, value);

      return al;
    }
  }

  #endregion // Extension methods

  namespace DataStructure
  {
    /// <summary>
    /// <para></para>
    /// <para><seealso href="https://en.wikipedia.org/wiki/List_of_data_structures#Abstract_data_types"/></para>
    /// </summary>
    /// <typeparam name="TVertexValue"></typeparam>
    /// <typeparam name="TEdgeValue"></typeparam>
    public interface IGraph<TVertexValue, TEdgeValue>
    {
      IGraph<TVertexValue, TEdgeValue> CloneEmpty();

      /// <summary>Adds the vertex <paramref name="x"/>, if it is not there.</summary>
      bool AddVertex(int x);

      /// <summary>Adds the vertex <paramref name="x"/> with the <paramref name="value"/>, if it is not there.</summary>
      bool AddVertex(int x, TVertexValue value);

      /// <summary>Removes vertex <paramref name="x"/>, if it is there.</summary>
      bool RemoveVertex(int x);

      /// <summary>Returns the <paramref name="value"/> associated with vertex <paramref name="x"/>. A vertex can exists without a value.</summary>
      bool TryGetVertexValue(int x, out TVertexValue value);

      /// <summary>Removes the value vertex <paramref name="x"/> and whether the removal was successful.</summary>
      bool RemoveVertexValue(int x);

      /// <summary>Sets the <paramref name="value"/> associated with vertex <paramref name="x"/>.</summary>
      void SetVertexValue(int x, TVertexValue value);

      /// <summary>Adds the edge (<paramref name="x"/>, <paramref name="y"/>) with the <paramref name="value"/>, if it is not there.</summary>
      bool AddEdge(int x, int y, TEdgeValue value);

      /// <summary>Tests whether there is an edge (<paramref name="x"/>, <paramref name="y"/>), either directed or a loop.</summary>
      bool EdgeExists(int x, int y, TEdgeValue value);

      /// <summary>Removes the edge (<paramref name="x"/>, <paramref name="y"/>), if it is there.</summary>
      bool RemoveEdge(int x, int y, TEdgeValue value);

      System.Collections.Generic.IEnumerable<(int x, int y, TEdgeValue value)> GetEdges();

      System.Collections.Generic.IEnumerable<(int x, TVertexValue value)> GetVertices();

      System.Collections.Generic.IEnumerable<(int x, TVertexValue value, int degree)> GetVerticesWithDegree();
    }
  }
}
