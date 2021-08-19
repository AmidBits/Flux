namespace Flux.AmbOps
{
  public interface IValue<T>
  {
    T Value { get; }

    string ToString();
  }
}
