namespace Flux.AmbOps
{
#if NET5_0
  public sealed class Constraint
#else
  public record class Constraint
#endif
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
    public int AppliesForItems
       => m_appliesForItems;
    public bool Invoke()
      => m_predicate?.Invoke() ?? default;
    #endregion Implemented interfaces
  }
}
