namespace Flux.AmbOps
{
  public sealed class Amb
  {
    interface IChoices
    {
      int Length { get; }
      int Index { get; set; }
    }

    interface IConstraint
    {
      int AppliesForItems { get; }
      bool Invoke();
    }

    public IValue<T> Choose<T>(params T[] choices)
    {
      var array = new ChoiceArray<T>(choices);
      m_itemsChoices.Add(array);
      return array;
    }

    public void Require(System.Func<bool> predicate)
      => m_constraints.Add(new Constraint(predicate, m_itemsChoices.Count));

    public bool RequireFinal(System.Func<bool> predicate)
    {
      Require(predicate);
      return Disambiguate();
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

    System.Collections.Generic.List<IChoices> m_itemsChoices = new System.Collections.Generic.List<IChoices>();
    System.Collections.Generic.List<IConstraint> m_constraints = new System.Collections.Generic.List<IConstraint>();

    void Disambiguate(int itemsTracked, int constraintIndex)
    {
      while (constraintIndex < m_constraints.Count && m_constraints[constraintIndex].AppliesForItems <= itemsTracked)
      {
        if (!m_constraints[constraintIndex].Invoke())
          return;
        constraintIndex++;
      }

      if (itemsTracked == m_itemsChoices.Count)
      {
        throw new System.Exception("Success");
      }

      for (var i = 0; i < m_itemsChoices[itemsTracked].Length; i++)
      {
        m_itemsChoices[itemsTracked].Index = i;
        Disambiguate(itemsTracked + 1, constraintIndex);
      }
    }

    class Constraint
      : IConstraint
    {
      private int AppliesForItems;

      int IConstraint.AppliesForItems
        => AppliesForItems;

      private System.Func<bool> Predicate;

      public Constraint(System.Func<bool> predicate, int appliesForItems)
      {
        Predicate = predicate;
        AppliesForItems = appliesForItems;
      }

      public bool Invoke()
        => Predicate?.Invoke() ?? default;
    }

    class ChoiceArray<T>
      : IChoices, IValue<T>
    {
      private T[] Values;

      public ChoiceArray(params T[] values)
        => Values = values;

      public int Index { get; set; }

      public T Value
        => Values[Index];

      public int Length
        => Values.Length;

      public override string ToString()
        => Value?.ToString() ?? string.Empty;
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
