namespace Flux.AmbOps
{
  public class Constraint
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

/*
  private static void AmbTest()
  {
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
  }
*/
