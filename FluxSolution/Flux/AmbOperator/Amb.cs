﻿namespace Flux.AmbOperator
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
      try
      {
        return Disambiguate(0, 0);
      }
      catch
      {
        return true;
      }
    }

    public void Require(System.Func<bool> predicate)
      => m_constraints.Add(new Constraint(predicate, m_choices.Count));

    public bool RequireFinal(System.Func<bool> predicate)
    {
      Require(predicate);

      return Disambiguate();
    }
  }
}
