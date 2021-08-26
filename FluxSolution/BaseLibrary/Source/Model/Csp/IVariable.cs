namespace Flux.Csp
{
  public interface IVariable<T>
    : System.IComparable<IVariable<T>>
  {
    void Instantiate(int depth, out DomainOperationResult result);
    void Instantiate(T value, int depth, out DomainOperationResult result);

    void Backtrack(int fromDepth);
    void Remove(T value, int depth, out DomainOperationResult result);
    void Remove(T value, out DomainOperationResult result);

    void SetState(IState<T> state);

    string ToString();
    string Name { get; }
    T InstantiatedValue { get; }
    bool Instantiated();
    T Size();
    IVariable<T> Clone();
  }
}
