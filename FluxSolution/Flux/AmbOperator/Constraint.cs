namespace Flux.AmbOperator
{
  public sealed class Constraint(System.Func<bool> predicate, int appliesForItems)
    : IConstraint
  {
    #region Implemented interfaces

    // IConstraint
    public int AppliesForItems => appliesForItems;

    public bool Invoke() => predicate?.Invoke() ?? throw new System.NotImplementedException(); // NOTE: coalescing used to be "default"!

    #endregion Implemented interfaces
  }
}
