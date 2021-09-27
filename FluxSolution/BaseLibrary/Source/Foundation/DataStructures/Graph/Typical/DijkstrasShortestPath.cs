using System.Linq;

namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static System.Collections.Generic.Dictionary<TVertex, double> DijkstraShortestPath<TVertex, TValue>(this DataStructures.Graph.IGraphTypical<TVertex, TValue> graph, TVertex start, System.Func<TValue, double> distanceSelector)
      where TVertex : System.IEquatable<TVertex>
      where TValue : System.IEquatable<TValue>
    {
      System.Collections.Generic.Dictionary<TVertex, double> vertices = new System.Collections.Generic.Dictionary<TVertex, double>();

      foreach (var (vertex, degree) in graph.GetVertices())
      {
        if (vertex.Equals(start))
          vertices.Add(vertex, 0);
        else
          vertices.Add(vertex, double.MaxValue);
      }

      System.Collections.Generic.Dictionary<TVertex, double> distances = new System.Collections.Generic.Dictionary<TVertex, double>();

      while (vertices.Any())
      {
        using var e = vertices.GetEnumerator();

        if (e.MoveNext())
        {
          var vertex = e.Current;

          while (e.MoveNext())
            if (e.Current.Value < vertex.Value)
              vertex = e.Current;

          distances.Add(vertex.Key, vertex.Value);

          vertices.Remove(vertex.Key);

          foreach (var edge in graph.GetEdges().Where(edge => edge.source.Equals(vertex.Key)))
          {
            if (vertices.ContainsKey(edge.target))
            {
              var distance = vertices[edge.target];
              var distanceSum = vertex.Value + distanceSelector(edge.value);

              if (distanceSum < distance)
                vertices[edge.target] = distanceSum;
            }
          }
        }
      }

      return distances;
    }
  }
}
