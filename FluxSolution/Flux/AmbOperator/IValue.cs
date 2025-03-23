namespace Flux.AmbOperator
{
  public interface IValue<T>
  {
    T Value { get; }
    string ToString();
  }
}
