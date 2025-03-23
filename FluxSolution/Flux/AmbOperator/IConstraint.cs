namespace Flux.AmbOperator
{
  public interface IConstraint
  {
    int AppliesForItems { get; }
    bool Invoke();
  }
}
