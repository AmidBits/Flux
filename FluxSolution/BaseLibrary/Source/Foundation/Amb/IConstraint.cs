namespace Flux.AmbOps
{
  public interface IConstraint
  {
    int AppliesForItems { get; }
    bool Invoke();
  }
}
