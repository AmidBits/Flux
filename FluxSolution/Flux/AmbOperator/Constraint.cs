namespace Flux.AmbOperator
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
    public int AppliesForItems => m_appliesForItems;

    public bool Invoke() => m_predicate?.Invoke() ?? throw new System.NotImplementedException(); // NOTE: coalescing used to be "default"!

    #endregion Implemented interfaces
  }
}
