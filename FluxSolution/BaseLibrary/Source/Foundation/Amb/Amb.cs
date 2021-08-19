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
      var array = new ChoiceArray<T> { Values = choices };
      m_itemsChoices.Add(array);
      return array;
    }

    public void Require(System.Func<bool> predicate)
      => m_constraints.Add(new Constraint { Predicate = predicate, AppliesForItems = m_itemsChoices.Count });

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
      internal int AppliesForItems;
      
      int IConstraint.AppliesForItems 
        => AppliesForItems;

      internal System.Func<bool> Predicate;
      
      public bool Invoke() 
        => Predicate?.Invoke() ?? default;
    }

    class ChoiceArray<T> 
      : IChoices, IValue<T>
    {
      internal T[] Values;

      public int Index { get; set; }

      public T Value { get { return Values[Index]; } }

      public int Length => Values.Length;

      public override string ToString() => Value.ToString();
    }
  }
}
