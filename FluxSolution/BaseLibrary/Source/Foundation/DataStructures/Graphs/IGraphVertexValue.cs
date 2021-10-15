//namespace Flux
//{
//  public static partial class ExtensionMethods
//  {
//    /// <summary>Creates a new sequence with all vertices and their respective value.</summary>
//    public static System.Collections.Generic.IDictionary<TKey, TVertexValue> GetVerticesWithValue<TKey, TVertexValue>(this DataStructures.Graphs.IGraphVertexValue<TKey, TVertexValue> source)
//      where TKey : notnull
//      => System.Linq.Enumerable.ToDictionary(source.GetVertices(), k => k, k => source.TryGetVertexValue(k, out var value) ? value : default!);
//  }

//  namespace DataStructures.Graphs
//  {
//    public interface IGraphVertexValue<TKey, TVertexValue>
//      : IGraphVertex<TKey>
//      where TKey : notnull
//    {
//      /// <summary>Adds a vertex with an associated value to the graph.</summary>
//      bool AddVertex(TKey key, TVertexValue value);

//      /// <summary>Determines whether the specified vertex is set to the associated value.</summary>
//      bool IsVertexValueEqualTo(TKey key, TVertexValue value);

//      /// <summary>Tries to get the associated value for the specified vertex and returns whether it succeeded.</summary>
//      bool TryGetVertexValue(TKey key, out TVertexValue value);
//      /// <summary>Tries to set the associated value for the specified vertex and returns whether it succeeded.</summary>
//      bool TrySetVertexValue(TKey key, TVertexValue value);
//    }
//  }
//}