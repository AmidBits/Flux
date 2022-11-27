namespace Flux
{
  namespace Loops
  {
    /// <summary>Creates a new sequence.</summary>
    public record class CustomSelectors<TSelf, TResult>
    : INumericSequence<TResult>
    where TSelf : System.Numerics.INumber<TSelf>
    where TResult : System.Numerics.INumber<TResult>
    {
      private System.Func<TSelf> m_initializerSelector;
      private System.Func<TSelf, int, bool> m_conditionSelector;
      private System.Func<TSelf, int, TSelf> m_iteratorSelector;
      private System.Func<TSelf, int, TResult> m_resultSelector;

      public CustomSelectors(System.Func<TSelf> initializerSelector, System.Func<TSelf, int, bool> conditionSelector, System.Func<TSelf, int, TSelf> iteratorSelector, System.Func<TSelf, int, TResult> resultSelector)
      {
        m_initializerSelector = initializerSelector;
        m_conditionSelector = conditionSelector;
        m_iteratorSelector = iteratorSelector;
        m_resultSelector = resultSelector;
      }

      #region Implemented interfaces
      // INumberSequence
      [System.Diagnostics.Contracts.Pure]
      public System.Collections.Generic.IEnumerable<TResult> GetSequence()
      {
        var index = 0;
        for (var current = m_initializerSelector(); m_conditionSelector(current, index); current = m_iteratorSelector(current, index), index++)
          yield return m_resultSelector(current, index);
      }

      // IEnumerable<>
      [System.Diagnostics.Contracts.Pure] public System.Collections.Generic.IEnumerator<TResult> GetEnumerator() => GetSequence().GetEnumerator();
      [System.Diagnostics.Contracts.Pure] System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
      #endregion Implemented interfaces
    }
  }
}
