namespace Flux.AmbOps
{
  public sealed class Constraint
     : IConstraint
  {
    private readonly int m_appliesForItems;
    private readonly System.Func<bool> m_predicate;

    public Constraint(System.Func<bool> predicate, int appliesForItems)
    {
      m_appliesForItems = appliesForItems;
      m_predicate = predicate;
    }

    #region Implemented interfaces
    // IConstraint
    [System.Diagnostics.Contracts.Pure]
    public int AppliesForItems
      => m_appliesForItems;
    [System.Diagnostics.Contracts.Pure]
    public bool Invoke()
      => m_predicate?.Invoke() ?? default;
    #endregion Implemented interfaces
  }
}
