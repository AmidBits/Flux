namespace Flux.DataStructures.UnionFind
{
  public interface IUnionFind<TKey>
  {
    bool AreConnected(TKey value, TKey otherValue);
    int Find(TKey value);
    bool Union(TKey value, TKey otherValue);
  }
}
