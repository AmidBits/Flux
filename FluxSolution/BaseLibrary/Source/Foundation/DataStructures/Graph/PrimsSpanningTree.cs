using System.Linq;

namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static System.Collections.Generic.Dictionary<TVertex, double> PrimsSpanningTree<TVertex, TWeight>(this DataStructures.Graph.IGraph<TVertex, TWeight> graph, TVertex start, System.Func<TWeight, double> distanceSelector)
      where TVertex : System.IEquatable<TVertex>
      where TWeight : System.IEquatable<TWeight>
    {
      System.Collections.Generic.Dictionary<DataStructures.Graph.Vertex<TVertex>, double> vertices = new System.Collections.Generic.Dictionary<DataStructures.Graph.Vertex<TVertex>, double>();

      foreach (var vertex in graph.GetVertices())
      {
        if (vertex.Value.Equals(start))
          vertices.Add(vertex, 0);
        else
          vertices.Add(vertex, double.MaxValue);
      }

      System.Collections.Generic.Dictionary<TVertex, double> distances = new System.Collections.Generic.Dictionary<TVertex, double>();

      while (vertices.Any())
      {
        using var e = vertices.Where(v => !distances.ContainsKey(v.Key.Value)).GetEnumerator();

        if (e.MoveNext())
        {
          var vertex = e.Current;

          while (e.MoveNext())
            if (e.Current.Value < vertex.Value)
              vertex = e.Current;

          distances.Add(vertex.Key.Value, vertex.Value);

          foreach (var edge in graph.GetEdges().Where(edge => edge.Source.Value.Equals(vertex.Key.Value)))
          {
            if (vertices.ContainsKey(edge.Target))
            {
              var distance = vertices[edge.Target];
              var distanceSum = distanceSelector(edge.Weight);

              if (distanceSum < distance)
                vertices[edge.Target] = distanceSum;
            }
          }

          //vertices.Remove(vertex.Key);
        }
      }

      return distances;
    }
  }
}
