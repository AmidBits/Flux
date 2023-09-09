namespace Flux
{
  public static partial class Looping
  {
#if NET7_0_OR_GREATER

    public static System.Collections.Generic.IEnumerable<TSelf> Custom<TSelf>(System.Func<TSelf> initializer, System.Func<TSelf, int, bool> predicate, System.Func<TSelf, int, TSelf> stepper)
      where TSelf : System.Numerics.INumber<TSelf>
    {
      var index = 0;
      for (var current = initializer(); predicate(current, index); current = stepper(current, index), index++)
        yield return current;
    }

#else
#endif
  }

  namespace Loops
  {
#if NET7_0_OR_GREATER

    ///// <summary>Creates a new sequence.</summary>
    //public record class CustomSelectors<TSelf, TResult>
    //  : NumberSequences.INumericSequence<TResult>
    //  where TSelf : System.Numerics.INumber<TSelf>
    //  where TResult : System.Numerics.INumber<TResult>
    //{
    //  private readonly System.Func<TSelf> m_initializerSelector;
    //  private readonly System.Func<TSelf, int, bool> m_conditionSelector;
    //  private readonly System.Func<TSelf, int, TSelf> m_iteratorSelector;
    //  private readonly System.Func<TSelf, int, TResult> m_resultSelector;

    //  public CustomSelectors(System.Func<TSelf> initializerSelector, System.Func<TSelf, int, bool> conditionSelector, System.Func<TSelf, int, TSelf> iteratorSelector, System.Func<TSelf, int, TResult> resultSelector)
    //  {
    //    m_initializerSelector = initializerSelector;
    //    m_conditionSelector = conditionSelector;
    //    m_iteratorSelector = iteratorSelector;
    //    m_resultSelector = resultSelector;
    //  }

    //  #region Implemented interfaces
    //  // INumberSequence

    //  public System.Collections.Generic.IEnumerable<TResult> GetSequence()
    //  {
    //    var index = 0;
    //    for (var current = m_initializerSelector(); m_conditionSelector(current, index); current = m_iteratorSelector(current, index), index++)
    //      yield return m_resultSelector(current, index);
    //  }

    //  // IEnumerable<>
    //  public System.Collections.Generic.IEnumerator<TResult> GetEnumerator() => GetSequence().GetEnumerator();
    //  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
    //  #endregion Implemented interfaces
    //}

#else

    /// <summary>Creates a new sequence.</summary>
    public record class CustomSelectors<Bogus1, Bogus2>
      : NumberSequences.INumericSequence<System.Numerics.BigInteger>
    {
      private readonly System.Func<System.Numerics.BigInteger> m_initializerSelector;
      private readonly System.Func<System.Numerics.BigInteger, int, bool> m_conditionSelector;
      private readonly System.Func<System.Numerics.BigInteger, int, System.Numerics.BigInteger> m_iteratorSelector;
      private readonly System.Func<System.Numerics.BigInteger, int, System.Numerics.BigInteger> m_resultSelector;

      public CustomSelectors(System.Func<System.Numerics.BigInteger> initializerSelector, System.Func<System.Numerics.BigInteger, int, bool> conditionSelector, System.Func<System.Numerics.BigInteger, int, System.Numerics.BigInteger> iteratorSelector, System.Func<System.Numerics.BigInteger, int, System.Numerics.BigInteger> resultSelector)
      {
        m_initializerSelector = initializerSelector;
        m_conditionSelector = conditionSelector;
        m_iteratorSelector = iteratorSelector;
        m_resultSelector = resultSelector;
      }

      #region Implemented interfaces
      // INumberSequence

      public System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetSequence()
      {
        var index = 0;
        for (var current = m_initializerSelector(); m_conditionSelector(current, index); current = m_iteratorSelector(current, index), index++)
          yield return m_resultSelector(current, index);
      }

      // IEnumerable<>
      public System.Collections.Generic.IEnumerator<System.Numerics.BigInteger> GetEnumerator() => GetSequence().GetEnumerator();
      System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
      #endregion Implemented interfaces
    }

#endif
  }
}
