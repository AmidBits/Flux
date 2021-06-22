namespace Flux.DataStructures.UnionFind
{
  public interface IUnionFind<T>
  {
    bool AreConnected(T value, T otherValue);
    int Find(T value);
    bool Union(T value, T otherValue);
  }
}
