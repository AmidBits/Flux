namespace Flux.AmbOperator
{
  public sealed class Amb
  {
    private readonly System.Collections.Generic.List<IChoices> m_choices = [];
    private readonly System.Collections.Generic.List<IConstraint> m_constraints = [];

    public IValue<T> Choose<T>(params T[] choices)
    {
      var array = new Choices<T>(choices);
      m_choices.Add(array);
      return array;
    }

    private bool Disambiguate(int itemsTracked, int constraintIndex)
    {
      while (constraintIndex < m_constraints.Count && m_constraints[constraintIndex].AppliesForItems <= itemsTracked)
      {
        if (!m_constraints[constraintIndex].Invoke())
          return false;

        constraintIndex++;
      }

      if (itemsTracked == m_choices.Count)
        return true;

      for (var i = 0; i < m_choices[itemsTracked].Length; i++)
      {
        m_choices[itemsTracked].Index = i;

        if (Disambiguate(itemsTracked + 1, constraintIndex))
          return true;
      }

      return false;
    }

    public bool Disambiguate()
    {
      try { return Disambiguate(0, 0); }
      catch { return true; }
    }

    public void Require(System.Func<bool> predicate)
      => m_constraints.Add(new Constraint(predicate, m_choices.Count));

    public bool RequireFinal(System.Func<bool> predicate)
    {
      Require(predicate);

      return Disambiguate();
    }

    // Helper functions:

    #region IEqualityComparer<>

    public void AllEqual<T>(System.Collections.Generic.IEqualityComparer<T> equalityComparer, params System.Collections.Generic.IEnumerable<Flux.AmbOperator.IValue<T>> values)
      => Require(() => values.Counts(v => v.Value, equalityComparer).AllEqual);

    public void AllUnique<T>(System.Collections.Generic.IEqualityComparer<T> equalityComparer, params Flux.AmbOperator.IValue<T>[] values)
      => Require(() => values.Counts(v => v.Value, equalityComparer).IsDistinct);

    //public void IsEqual<T>(Flux.AmbOperator.IValue<T> left, T right, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    //{
    //  equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

    //  Require(() => equalityComparer.Equals(left.Value, right));
    //}

    public void IsEqual<T>(Flux.AmbOperator.IValue<T> left, Flux.AmbOperator.IValue<T> right, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      Require(() => equalityComparer.Equals(left.Value, right.Value));
    }

    public void IsEqualToAll<T>(System.Collections.Generic.IEqualityComparer<T>? equalityComparer, Flux.AmbOperator.IValue<T> value, params Flux.AmbOperator.IValue<T>[] values)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      Require(() => values.Select(iv => iv.Value).All(v => equalityComparer.Equals(value.Value, v)));
    }

    public void IsEqualToAny<T>(System.Collections.Generic.IEqualityComparer<T>? equalityComparer, Flux.AmbOperator.IValue<T> value, params Flux.AmbOperator.IValue<T>[] values)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      Require(() => values.Select(iv => iv.Value).Any(v => equalityComparer.Equals(value.Value, v)));
    }

    #endregion

    #region IComparer<>

    public void AreConsecutive<T>(Flux.AmbOperator.IValue<T> value, Flux.AmbOperator.IValue<T> other)
      where T : System.Numerics.IBinaryInteger<T>
    {
      Require(() => T.Abs(value.Value - other.Value) == T.One);
    }

    public void IsConsecutivelyLessThan<T>(Flux.AmbOperator.IValue<T> value, Flux.AmbOperator.IValue<T> other)
      where T : System.Numerics.IBinaryInteger<T>
    {
      Require(() => (other.Value - value.Value) == T.One);
    }

    public void IsConsecutivelyGreaterThan<T>(Flux.AmbOperator.IValue<T> value, Flux.AmbOperator.IValue<T> other)
      where T : System.Numerics.IBinaryInteger<T>
    {
      Require(() => (value.Value - other.Value) == T.One);
    }

    public void IsLessThan<T>(System.Collections.Generic.IComparer<T>? comparer, Flux.AmbOperator.IValue<T> value, Flux.AmbOperator.IValue<T> other)
    {
      comparer ??= System.Collections.Generic.Comparer<T>.Default;

      Require(() => comparer.Compare(value.Value, other.Value) < 0);
    }

    public void IsLessThanAll<T>(System.Collections.Generic.IComparer<T>? comparer, Flux.AmbOperator.IValue<T> value, params Flux.AmbOperator.IValue<T>[] values)
    {
      comparer ??= System.Collections.Generic.Comparer<T>.Default;

      Require(() => values.Select(iv => iv.Value).All(v => comparer.Compare(value.Value, v) < 0));
    }

    public void IsLessThanAny<T>(System.Collections.Generic.IComparer<T>? comparer, Flux.AmbOperator.IValue<T> value, params Flux.AmbOperator.IValue<T>[] values)
    {
      comparer ??= System.Collections.Generic.Comparer<T>.Default;

      Require(() => values.Select(iv => iv.Value).Any(right => comparer.Compare(value.Value, right) < 0));
    }

    public void IsLessThanOrEqual<T>(System.Collections.Generic.IComparer<T>? comparer, Flux.AmbOperator.IValue<T> value, Flux.AmbOperator.IValue<T> other)
    {
      comparer ??= System.Collections.Generic.Comparer<T>.Default;

      Require(() => comparer.Compare(value.Value, other.Value) <= 0);
    }

    public void IsLessThanOrEqualToAll<T>(System.Collections.Generic.IComparer<T>? comparer, Flux.AmbOperator.IValue<T> value, params Flux.AmbOperator.IValue<T>[] values)
    {
      comparer ??= System.Collections.Generic.Comparer<T>.Default;

      Require(() => values.Select(iv => iv.Value).All(right => comparer.Compare(value.Value, right) < 0));
    }

    public void IsLessThanOrEqualToAny<T>(System.Collections.Generic.IComparer<T>? comparer, Flux.AmbOperator.IValue<T> value, params Flux.AmbOperator.IValue<T>[] values)
    {
      comparer ??= System.Collections.Generic.Comparer<T>.Default;

      Require(() => values.Select(iv => iv.Value).Any(right => comparer.Compare(value.Value, right) < 0));
    }

    public void IsGreaterThan<T>(System.Collections.Generic.IComparer<T>? comparer, Flux.AmbOperator.IValue<T> value, Flux.AmbOperator.IValue<T> other)
    {
      comparer ??= System.Collections.Generic.Comparer<T>.Default;

      Require(() => comparer.Compare(value.Value, other.Value) > 0);
    }

    public void IsGreaterThanAll<T>(System.Collections.Generic.IComparer<T>? comparer, Flux.AmbOperator.IValue<T> value, params Flux.AmbOperator.IValue<T>[] values)
    {
      comparer ??= System.Collections.Generic.Comparer<T>.Default;

      Require(() => values.Select(iv => iv.Value).All(right => comparer.Compare(value.Value, right) > 0));
    }

    public void IsGreaterThanAny<T>(System.Collections.Generic.IComparer<T>? comparer, Flux.AmbOperator.IValue<T> value, params Flux.AmbOperator.IValue<T>[] values)
    {
      comparer ??= System.Collections.Generic.Comparer<T>.Default;

      Require(() => values.Select(iv => iv.Value).All(right => comparer.Compare(value.Value, right) > 0));
    }

    public void IsGreaterThanOrEqual<T>(System.Collections.Generic.IComparer<T>? comparer, Flux.AmbOperator.IValue<T> value, Flux.AmbOperator.IValue<T> other)
    {
      comparer ??= System.Collections.Generic.Comparer<T>.Default;

      Require(() => comparer.Compare(value.Value, other.Value) >= 0);
    }

    public void IsGreaterThanOrEqualToAll<T>(System.Collections.Generic.IComparer<T>? comparer, Flux.AmbOperator.IValue<T> value, params Flux.AmbOperator.IValue<T>[] values)
    {
      comparer ??= System.Collections.Generic.Comparer<T>.Default;

      Require(() => values.Select(iv => iv.Value).All(right => comparer.Compare(value.Value, right) >= 0));
    }

    public void IsGreaterThanOrEqualToAny<T>(System.Collections.Generic.IComparer<T>? comparer, Flux.AmbOperator.IValue<T> value, params Flux.AmbOperator.IValue<T>[] values)
    {
      comparer ??= System.Collections.Generic.Comparer<T>.Default;

      Require(() => values.Select(iv => iv.Value).All(right => comparer.Compare(value.Value, right) >= 0));
    }

    #endregion
  }
}
