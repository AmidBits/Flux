namespace Flux.Csp
{
  public interface IDomain<T>
    : System.Collections.IEnumerable
  {
    T InstantiatedValue { get; }

    void Instantiate(out DomainOperationResult result);
    void Instantiate(T value, out DomainOperationResult result);
    void InstantiateLowest(out DomainOperationResult result);

    void Remove(T element, out DomainOperationResult result);
    bool Contains(T element);

    string ToString();
    bool Instantiated();
    T Size();
    T LowerBound { get; }
    T UpperBound { get; }
    IDomain<T> Clone();
  }
}
