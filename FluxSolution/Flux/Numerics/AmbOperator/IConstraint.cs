namespace Flux.Numerics.AmbOps
{
  public interface IConstraint
  {
    int AppliesForItems { get; }
    bool Invoke();
  }
}
