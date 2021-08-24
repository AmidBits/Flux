namespace Flux.AmbOps
{
  public sealed class Amb
  {
    private readonly System.Collections.Generic.List<IChoices> m_choices = new System.Collections.Generic.List<IChoices>();
    private readonly System.Collections.Generic.List<IConstraint> m_constraints = new System.Collections.Generic.List<IConstraint>();

    public IValue<T> Choose<T>(params T[] choices)
    {
      var array = new Choices<T>(choices);
      m_choices.Add(array);
      return array;
    }

    private void Disambiguate(int itemsTracked, int constraintIndex)
    {
      while (constraintIndex < m_constraints.Count && m_constraints[constraintIndex].AppliesForItems <= itemsTracked)
      {
        if (!m_constraints[constraintIndex].Invoke())
          return;

        constraintIndex++;
      }

      if (itemsTracked == m_choices.Count)
        throw new System.Exception("Success");

      for (var i = 0; i < m_choices[itemsTracked].Length; i++)
      {
        m_choices[itemsTracked].Index = i;

        Disambiguate(itemsTracked + 1, constraintIndex);
      }
    }
    public bool Disambiguate()
    {
      try
      {
        Disambiguate(0, 0);

        return false;
      }
      catch (System.Exception ex) when (ex.Message == "Success")
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

    class Choices<T>
      : IChoices, IValue<T>
    {
      private readonly T[] Values;

      public Choices(params T[] values)
        => Values = values;

      public T Value
        => Values[Index];

      #region Implemented interfaces
      // IChoices
      public int Index
      { get; set; }
      public int Length
        => Values.Length;
      #endregion Implemented interfaces

      #region Object overrides
      public override string ToString()
        => Value?.ToString() ?? string.Empty;
      #endregion Object overrides
    }

    class Constraint
      : IConstraint
    {
      private readonly int AppliesForItems;
      private readonly System.Func<bool> Predicate;

      public Constraint(System.Func<bool> predicate, int appliesForItems)
      {
        AppliesForItems = appliesForItems;
        Predicate = predicate;
      }

      #region Implemented interfaces
      // IConstraint
      int IConstraint.AppliesForItems
        => AppliesForItems;
      public bool Invoke()
        => Predicate?.Invoke() ?? default;
      #endregion Implemented interfaces
    }
  }
}

/*
  var amb = new Flux.AmbOps.Amb();

  var set1 = amb.Choose("the", "that", "a");
  var set2 = amb.Choose("frog", "tramp", "thing");
  amb.Require(() => set1.Value.Last() == set2.Value[0]);
  var set3 = amb.Choose("walked", "hauled", "treaded", "grows");
  amb.Require(() => set2.Value.Last() == set3.Value[0]);
  var set4 = amb.Choose("slowly", "quickly");
  amb.RequireFinal(() => set3.Value.Last() == set4.Value[0]);

  System.Console.WriteLine($"{set1} {set2} {set3} {set4}");
  System.Console.Read();

  // problem from http://www.randomhacks.net/articles/2005/10/11/amb-operator
  amb = new Flux.AmbOps.Amb();

  var x = amb.Choose(1, 2, 3);
  var y = amb.Choose(4, 5, 6);
  amb.RequireFinal(() => x.Value + y.Value == 8);

  System.Console.WriteLine($"{x} + {y} = 8");
  System.Console.Read();
  System.Console.Read();
*/
