//namespace Flux
//{
//  public static partial class ExtensionMethods
//  {
//    /// <summary>Creates a new sequence with all vertices and their respective degree.</summary>
//    public static System.Collections.Generic.IDictionary<TKey, int> GetVerticesWithDegree<TKey>(this DataStructures.Graphs.IGraphVertex<TKey> source)
//      where TKey : notnull
//      => System.Linq.Enumerable.ToDictionary(source.GetVertices(), k => k, k => source.GetDegree(k));
//  }

//  namespace DataStructures.Graphs
//  {
//    public interface IGraphVertex<TKey>
//      where TKey : notnull
//    {
//      /// <summary>Returns the degree of the specified vertex.</summary>
//      int GetDegree(TKey key);
//      /// <summary>Creates a new sequence of all vertices that are edge destinations from the specified vertex.</summary>
//      System.Collections.Generic.IEnumerable<TKey> GetNeighbors(TKey key);
//      /// <summary>Determines whether an edge exists from source to target.</summary>
//      bool IsAdjacent(TKey keySource, TKey keyTarget);

//      /// <summary>Adds a vertex to the graph.</summary>
//      bool AddVertex(TKey key);
//      /// <summary>Determins whether a vertex exists in the graph.</summary>
//      bool ContainsVertex(TKey key);
//      /// <summary>Creates a new sequence with all vertices in the graph.</summary>
//      System.Collections.Generic.IEnumerable<TKey> GetVertices();
//      /// <summary>Removes a vertex from the graph, if it exists.</summary>
//      bool RemoveVertex(TKey key);
//    }
//  }
//}
